using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Mvvm;

namespace CrossPlatformLibrary.Forms.Validation
{
    public class ViewModelValidation : BindableBase, IValidatable, INotifyDataErrorInfo
    {
        private readonly List<IValidation> validations = new List<IValidation>();
        private readonly IDictionary<string, IList<string>> errorMessages = new Dictionary<string, IList<string>>();
        private static readonly ReadOnlyCollection<string> EmptyErrorsCollection = new ReadOnlyCollection<string>(new List<string>(0));

        public ViewModelValidation()
        {
        }

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
            this.errorMessages.Clear();

            if (!this.HasValidations)
            {
                var errorMessage = $"Cannot find any validation rules. Use method {nameof(this.AddDelegateValidation)} to setup validation rules.";
                this.AddErrorMessageForPropertyInternal("InvalidOperationException", errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            //foreach (var validation in this.validations)
            //{
            //    // TODO: Parallelize validation (thread-safety!)
            //    await this.PerformValidation(validation);
            //}

            var validationTasks = this.validations
                .Select(this.PerformValidation)
                .ToList();

            await Task.WhenAll(validationTasks);

            this.UpdateAllValidationEnabledProperties();
        }

        private void UpdateAllValidationEnabledProperties()
        {
            //var propertiesToUpdate = this.validations.Select(pv => pv.PropertyName).ToList();
            //propertiesToUpdate.ForEach(this.OnErrorsChanged);

            this.OnErrorsChanged(string.Empty);
        }

        internal async Task ValidateProperty(string propertyName)
        {
            Debug.WriteLine($"ValidateProperty(propertyName: \"{propertyName}\")");

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
            Debug.WriteLine($"PerformValidation(validation: {validation.GetType().GetFormattedName()})");

            var propertyErrors = await validation.GetErrors();
            if (propertyErrors != null && propertyErrors.Any())
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
            foreach (var property in errorMessages)
            {
                foreach (var errorMessage in property.Value)
                {
                    this.AddErrorMessageForPropertyInternal(property.Key, errorMessage);
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

        public virtual IDictionary<string, IList<string>> GetErrors()
        {
            return this.errorMessages;
        }

        public bool HasErrors => this.GetErrors().Count > 0;

        private void OnErrorsChanged(string propertyName)
        {
            Debug.WriteLine($"OnErrorsChanged(propertyName: \"{propertyName}\")");

            this.RaisePropertyChanged(nameof(this.Errors));
            this.RaisePropertyChanged(nameof(this.HasErrors));
            this.RaisePropertyChanged($"Item[{propertyName}]");

            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

        #region IValidatable implementation

        //public abstract void SetupValidationRules();

        protected virtual void OnValidationErrorOccurred(IDictionary<string, IList<string>> occurredErrors)
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
            await this.ValidateAll();

            return !this.HasErrors;
        }

        #endregion

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            this.ValidateProperty(args.PropertyName);
        }

        internal void TrySetContext(object baseViewModel)
        {
            this.validations
                .OfType<IContextAware>()
                .ForEach(v => v.SetContext(baseViewModel));
        }
    }
}