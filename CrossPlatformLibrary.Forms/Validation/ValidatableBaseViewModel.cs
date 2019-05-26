﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Forms.Mvvm;

namespace CrossPlatformLibrary.Forms.Validation
{
    public abstract class ValidatableBaseViewModel : BaseViewModel, IValidatable, INotifyDataErrorInfo
    {
        private readonly List<PropertyValidation> validations = new List<PropertyValidation>();
        private readonly IDictionary<string, IList<string>> errorMessages = new Dictionary<string, IList<string>>();
        private static readonly ReadOnlyCollection<string> EmptyErrorsCollection = new ReadOnlyCollection<string>(new List<string>());

        public ValidatableBaseViewModel Errors
        {
            get { return this; }
        }

        public ReadOnlyCollection<string> this[string propertyName]
        {
            get
            {
                return this.errorMessages.ContainsKey(propertyName) ? new ReadOnlyCollection<string>(this.errorMessages[propertyName]) : EmptyErrorsCollection;
            }
        }

        ////protected PropertyValidation AddValidationFor(Expression<Func<object>> expression)
        ////{
        ////    return this.AddValidationFor(ExpressionUtility.GetPropertyName(expression));
        ////}

        protected PropertyValidation AddValidationFor(string propertyName)
        {
            var validation = new PropertyValidation(propertyName);
            this.validations.Add(validation);

            return validation;
        }

        private void ValidateAll()
        {
            this.errorMessages.Clear();

            if (!this.validations.Any())
            {
                this.AddErrorMessageForProperty("InvalidOperationException", "ValidateAll cannot find any validation rules. Use AddValidationFor method to setup validation rules.");
                throw new InvalidOperationException("ValidateAll cannot find any validation rules. Use AddValidationFor method to setup validation rules.");
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

        ////private void ValidateProperty<T>(Expression<Func<T>> expression)
        ////{
        ////    this.ValidateProperty(ExpressionUtility.GetPropertyName(expression));
        ////}

        private void ValidateProperty(string propertyName)
        {
            if (this.errorMessages.ContainsKey(propertyName))
            {
                this.errorMessages[propertyName].Clear();

                this.validations.Where(v => v.PropertyName == propertyName).ForEach(this.PerformValidation);
                this.OnErrorsChanged(propertyName);
            }
        }

        private void PerformValidation(PropertyValidation validation)
        {
            if (validation.IsInvalid())
            {
                this.AddErrorMessageForProperty(validation.PropertyName, validation.GetErrorMessage());
            }
        }

        private void AddErrorMessageForProperty(string propertyName, string errorMessage)
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
            this.OnPropertyChanged(nameof(this.Errors));
            this.OnPropertyChanged(nameof(this.HasErrors));
            this.OnPropertyChanged($"Item[{propertyName}]");

            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

        #region IValidatable implementation

        public abstract void SetupValidationRules();

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

        #region ObservableObject method overrides

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            this.ValidateProperty(propertyName);
        }
        #endregion
    }
}