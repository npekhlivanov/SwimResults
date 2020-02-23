namespace NP.Helpers.Extensions
{
    using System;
    using System.Globalization;

    public static class ToStringExtensions
    {
        public static string ToStringInvariant(this int value)
        {
            return value.ToString(NumberFormatInfo.InvariantInfo);
        }

        public static string ToStringInvariant(this decimal value)
        {
            return value.ToString(NumberFormatInfo.InvariantInfo);
        }
        public static string ToStringInvariant(this decimal value, string format)
        {
            return value.ToString(format, NumberFormatInfo.InvariantInfo);
        }

        public static string ToStringInvariant(this bool value)
        {
            return value ? bool.TrueString : bool.FalseString;
        }

        public static string ToShortDateStringInvariant(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
        }

        public static string ToStringInvariant(this TimeSpan value, string format)
        {
            return value.ToString(format, DateTimeFormatInfo.InvariantInfo);
        }

        public static string ToStringInvariant(this double value)
        {
            return value.ToString(NumberFormatInfo.InvariantInfo);
        }

        public static string ToStringInvariant(this double value, string format)
        {
            return value.ToString(format, NumberFormatInfo.InvariantInfo);
        }

        public static string ToInvariant(this string value)
        {
            return value;
            //var formattable = System.Runtime.CompilerServices.FormattableStringFactory.Create(value);
            //return formattable.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringPrecision(this decimal value, int numberDecimalDigits)
        {
            NumberFormatInfo setPrecision = CultureInfo.InvariantCulture.NumberFormat;
            setPrecision.NumberDecimalDigits = numberDecimalDigits;

            return value.ToString(setPrecision);
        }
    }
}