﻿namespace CrossPlatformLibrary.Forms.Validation.Rules
{
    public class RepeatPasswordRule<T> : IValidationRule<T>
    {
        public string Password { get; set; }

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            if(!str.Equals(this.Password))
            {
                return false;
            }

            return true;
        }
    }
}
