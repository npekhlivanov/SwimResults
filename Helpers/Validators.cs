using System;

namespace NP.Helpers
{
    public static class Validators
    {
        public static T ValidateNotNull<T>(this T value, string valueName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(valueName);
            }

            return value;
        }
    }
}
