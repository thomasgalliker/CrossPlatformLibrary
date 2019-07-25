using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Mvvm;

namespace CrossPlatformLibrary.Forms.Validation
{
    public class ViewModelValidation : BindableBase, IValidatable, INotifyDataErrorInfo
    {
        private readonly List<PropertyValidation> validations = new List<PropertyValidation>();
        private readonly IDictionary<string, IList<string>> errorMessages = new Dictionary<string, IList<string>>();
        private static readonly ReadOnlyCollection<string> EmptyErrorsCollection = new ReadOnlyCollection<string>(new List<string>(0));

        public ViewModelValidation()
        {
        }

        public ViewModelValidation Errors => this;

        public ReadOnlyCollection<string> this[string propertyName]
        {
            get
            {
                return this.errorMessages.ContainsKey(propertyName) ? new ReadOnlyCollection<string>(this.errorMessages[propertyName]) : EmptyErrorsCollection;
            }
        }

        public PropertyValidation AddValidationFor(string propertyName)
        {
            var validation = new PropertyValidation(propertyName);
            this.validations.Add(validation);

            return validation;
        }

        public bool HasValidations => this.validations.Any();

        private void ValidateAll()
        {
            this.errorMessages.Clear();

            if (!this.HasValidations)
            {
                var errorMessage = $"{nameof(this.ValidateAll)} cannot find any validation rules. Use method {nameof(this.AddValidationFor)} to setup validation rules.";
                this.AddErrorMessageForPropertyInternal("InvalidOperationException", errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            this.validations.ForEach(this.PerformValidation);
            this.UpdateAllValidationEnabledProperties();
        }

        private void UpdateAllValidationEnabledProperties()
        {
            //var propertiesToUpdate = this.validations.Select(pv => pv.PropertyName).ToList();
            //propertiesToUpdate.ForEach(this.OnErrorsChanged);

            this.OnErrorsChanged(string.Empty);
        }

        internal void ValidateProperty(string propertyName)
        {
            if (this.errorMessages.ContainsKey(propertyName))
            {
                this.errorMessages[propertyName].Clear();

                this.validations.Where(v => v.PropertyName == propertyName).ForEach(this.PerformValidation);
                this.OnErrorsChanged(propertyName);
            }
        }

        private void PerformValidation(PropertyValidation propertyValidation)
        {
            if (propertyValidation.IsInvalid())
            {
                this.AddErrorMessageForPropertyInternal(propertyValidation.PropertyName, propertyValidation.GetErrorMessage());
            }
        }

        private void AddErrorMessageForPropertyInternal(string propertyName, string errorMessage)
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

        /// <summary>
        /// Manually add <paramref name="errorMessage"/> for a particular <paramref name="propertyName"/>.
        /// Each call to this method raises a <see cref="ErrorsChanged"/> event.
        /// </summary>
        public void AddErrorMessageForProperty(string propertyName, string errorMessage)
        {
            this.AddErrorMessageForPropertyInternal(propertyName, errorMessage);
            this.OnErrorsChanged(propertyName);
        }

        #region INotifyDataErrorInfo
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or null or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
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

        public bool HasErrors
        {
            get { return this.GetErrors().Count > 0; }
        }

        private void OnErrorsChanged(string propertyName)
        {
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

        public bool IsValid()
        {
            this.ValidateAll();

            return !this.HasErrors;
        }
        #endregion

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            this.ValidateProperty(args.PropertyName);

        }

        internal void SetContext(object baseViewModel)
        {
            this.validations.ForEach(v => v.SetContext(baseViewModel));
        }
    }
}