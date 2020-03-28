namespace NP.Helpers.Extensions
{
    using System;
    using System.ComponentModel.DataAnnotations; // for .net Framework this is a separate package
    using System.Linq;
    using System.Reflection;

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            Validators.ValidateNotNull(enumValue, nameof(enumValue));
            var displayAttribute = enumValue.GetDisplayAttribute(out string valueName) ?? null;
            return displayAttribute?.Name ?? valueName;
        }

        public static string GetDescription(this Enum enumValue)
        {
            Validators.ValidateNotNull(enumValue, nameof(enumValue));
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

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            Validators.ValidateNotNull(enumValue, nameof(enumValue));
            var type = enumValue.GetType();
            var typeInfo = type.GetTypeInfo();
            var memberInfo = typeInfo.GetMember(enumValue.ToString());
            if (memberInfo.Length < 1)
            {
                return null;
            }

            var attributes = memberInfo[0].GetCustomAttributes<TAttribute>();
            var attribute = attributes?.FirstOrDefault();
            return attribute;
        }
    }
}
