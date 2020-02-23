namespace NP.Helpers.Extensions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null)
            {
                throw new ArgumentNullException(nameof(enumValue));
            }

            var displayAttribute = enumValue.GetDisplayAttribute(out string valueName) ?? null;
            return displayAttribute?.Name ?? valueName;
        }

        public static string GetDescription(this Enum enumValue)
        {
            if (enumValue == null)
            {
                throw new ArgumentNullException(nameof(enumValue));
            }

            var displayAttribute = enumValue.GetDisplayAttribute(out string valueName) ?? null;
            return displayAttribute?.Description ?? valueName;
        }

        private static DisplayAttribute GetDisplayAttribute(this Enum enumValue, out string valueName)
        {
            var enumType = enumValue.GetType();
            valueName = Enum.GetName(enumType, enumValue);// enumValue.ToString();

            var memberInfo = enumType.GetMember(valueName);
            var attrs = memberInfo.Length > 0 ? memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false) : null;
            var displayAttribute = attrs?.Length > 0 ? (DisplayAttribute)attrs[0] : null;

            //var fieldInfo = enumType.GetField(valueName);
            //var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>() ?? null;
            return displayAttribute;
        }
    }
}
