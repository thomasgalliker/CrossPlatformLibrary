using CrossPlatformLibrary.Extensions;
using System;
using System.Globalization;
using CrossPlatformLibrary.Forms.Mvvm;

namespace SampleApp.ViewModels
{
    public class LanguageViewModel : BaseViewModel, IEquatable<LanguageViewModel>
    {
        public LanguageViewModel(CultureInfo cultureInfo)
        {
            this.Dto = cultureInfo;
            this.NativeName = cultureInfo.NativeName.ToUpperFirst();
            this.DisplayName = cultureInfo.DisplayName.ToUpperFirst();
        }

        public CultureInfo Dto { get; }

        public string NativeName { get; }

        public string DisplayName { get; }

        public bool Equals(LanguageViewModel other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(this.Dto, other.Dto);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((LanguageViewModel)obj);
        }

        public override int GetHashCode()
        {
            return (this.Dto != null ? this.Dto.GetHashCode() : 0);
        }
    }
}