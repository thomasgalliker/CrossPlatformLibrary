﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Internals;
using CrossPlatformLibrary.Mvvm;

namespace CrossPlatformLibrary.Forms.Validation
{
    public class ViewModelValidation : BindableBase, IValidatable, INotifyDataErrorInfo
    {
        private readonly List<IValidation> validations = new List<IValidation>();
        private readonly Dictionary<string, List<string>> errorMessages = new Dictionary<string, List<string>>();
        private static readonly ReadOnlyCollection<string> EmptyErrorsCollection = new ReadOnlyCollection<string>(new List<string>(0));
        private bool isValidating;

        public ViewModelValidation()
        {
            this.ValidateOnPropertyChange = true;
        }

        /// <summary>
        /// Validates properties when a change is propagated trough <see cref="INotifyPropertyChanged" />.
        /// </summary>
        /// <remarks>
        /// Default value is <code>True</code>.
        /// Property validation is only taking place after the validation has been triggered for the first time.
        /// Use <seealso cref="IsValidAsync"/> to run a validation.
        /// </remarks>
        public bool ValidateOnPropertyChange { get; set; }

        public ViewModelValidation Errors => this;

        public ReadOnlyCollection<string> this[string propertyName]
            => this.errorMessages.ContainsKey(propertyName)
                ? new ReadOnlyCollection<string>(this.errorMessages[propertyName])
                : EmptyErrorsCollection;

        public void AddValidation(IValidation validation)
        {
            this.validations.Add(validation);
        }

        public DelegateValidation AddDelegateValidation(params string[] propertyNames)
        {
            var validation = new DelegateValidation(propertyNames);
            this.AddValidation(validation);

            return validation;
        }

        public PropertyValidation AddValidationFor(string propertyName)
        {
            var validation = new PropertyValidation(propertyName);
            this.AddValidation(validation);

            return validation;
        }

        public bool HasValidations => this.validations.Any();

        private async Task ValidateAll()
        {
            if (!this.HasValidations)
            {
                var errorMessage = $"Cannot find any validation rules. Use method {nameof(this.AddDelegateValidation)} to setup validation rules.";
                this.AddErrorMessageForPropertyInternal("InvalidOperationException", errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            lock (this.errorMessages)
            {
                this.errorMessages.Clear();
            }

            //foreach (var validation in this.validations)
            //{
            //    // TODO: Parallelize validation (thread-safety!)
            //    await this.PerformValidation(validation);
            //}

            var validationTasks = this.validations
                .Select(this.PerformValidation)
                .ToList();

            await Task.WhenAll(validationTasks).ConfigureAwait(false);

            this.UpdateAllValidationEnabledProperties();
        }

        private void UpdateAllValidationEnabledProperties()
        {
            //var propertiesToUpdate = this.validations.Select(pv => pv.PropertyName).ToList();
            //propertiesToUpdate.ForEach(this.OnErrorsChanged);

            this.OnErrorsChanged(string.Empty);
        }

        private async Task ValidateProperty(string propertyName)
        {
            Tracer.Current.Debug($"ValidateProperty(propertyName: \"{propertyName}\")");

            if (this.errorMessages.ContainsKey(propertyName))
            {
                this.errorMessages[propertyName].Clear();

                //foreach (var validation in this.validations.Where(v => v.PropertyNames.Contains(propertyName)))
                //{
                //    // TODO: Parallelize validation (thread-safety!)
                //    await this.PerformValidation(validation);
                //}

                var validationTasks = this.validations
                    .Where(v => v.PropertyNames.Contains(propertyName))
                    .Select(this.PerformValidation)
                    .ToList();

                await Task.WhenAll(validationTasks);

                this.OnErrorsChanged(propertyName);
            }
        }

        private async Task PerformValidation(IValidation validation)
        {
            Tracer.Current.Debug($"PerformValidation(validation: {validation.GetType().GetFormattedName()})");

            var getErrorsTask = validation.GetErrors();
            if (getErrorsTask != null)
            {
                var propertyErrors = await getErrorsTask.ConfigureAwait(false);
                if (propertyErrors != null && propertyErrors.Any())
                {
                    lock (this.errorMessages)
                    {
                        foreach (var propertyError in propertyErrors)
                        {
                            foreach (var message in propertyError.Value)
                            {
                                this.AddErrorMessageForPropertyInternal(propertyError.Key, message);
                            }
                        }
                    }
                }
            }
        }

        private void AddErrorMessageForPropertyInternal(string propertyName, string errorMessage)
        {
            lock (this.errorMessages)
            {
                if (this.errorMessages.ContainsKey(propertyName))
                {
                    this.errorMessages[propertyName].Add(errorMessage);
                }
                else
                {
                    this.errorMessages.Add(propertyName, new List<string> { errorMessage });
                }
            }
        }

        /// <summary>
        ///     Manually add <paramref name="errorMessage" /> for a particular <paramref name="propertyName" />.
        ///     Each call to this method raises a <see cref="ErrorsChanged" /> event.
        /// </summary>
        public void AddErrorMessageForProperty(string propertyName, string errorMessage)
        {
            this.AddErrorMessageForPropertyInternal(propertyName, errorMessage);
            this.OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Manually add <paramref name="errorMessages" /> for multiple properties.
        /// </summary>
        public void AddErrorMessagesForProperty(Dictionary<string, List<string>> errorMessages)
        {
            lock (this.errorMessages)
            {
                foreach (var property in errorMessages)
                {
                    foreach (var errorMessage in property.Value)
                    {
                        this.AddErrorMessageForPropertyInternal(property.Key, errorMessage);
                    }
                }
            }

            this.UpdateAllValidationEnabledProperties();
        }

        #region INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        ///     Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property to retrieve validation errors for; or null or
        ///     <see cref="F:System.String.Empty" />, to retrieve entity-level errors.
        /// </param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (this.errorMessages.ContainsKey(propertyName))
            {
                return this.errorMessages[propertyName];
            }

            return null;
        }

        public virtual Dictionary<string, List<string>> GetErrors()
        {
            return this.errorMessages;
        }

        public bool HasErrors => this.GetErrors().Count > 0;

        private void OnErrorsChanged(string propertyName)
        {
            Tracer.Current.Debug($"OnErrorsChanged(propertyName: \"{propertyName}\")");

            this.RaisePropertyChanged(nameof(this.Errors));
            this.RaisePropertyChanged(nameof(this.HasErrors));
            this.RaisePropertyChanged($"Item[{propertyName}]");

            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

        #region IValidatable implementation

        //public abstract void SetupValidationRules();

        protected virtual void OnValidationErrorOccurred(Dictionary<string, List<string>> occurredErrors)
        {
        }

        public void RaiseOnValidationError()
        {
            this.OnValidationErrorOccurred(this.errorMessages);
        }

        public void ClearErrorMessages()
        {
            this.errorMessages.Clear();
            this.UpdateAllValidationEnabledProperties();
        }

        public async Task<bool> IsValidAsync()
        {
            try
            {
                this.isValidating = true;

                await this.ValidateAll().ConfigureAwait(false);

            }
            finally
            {
                this.isValidating = false;
            }

            return !this.HasErrors;
        }

        #endregion

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            this.HandlePropertyChange(args.PropertyName);
        }

        internal async void HandlePropertyChange(string propertyName)
        {
            if (!(this.isValidating || this.ValidateOnPropertyChange))
            {
                return;
            }

            await this.ValidateProperty(propertyName);
        }

        internal void TrySetContext(object baseViewModel)
        {
            this.validations
                .OfType<IContextAware>()
                .ForEach(v => v.SetContext(baseViewModel));
        }
    }
}