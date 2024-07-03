using System.ComponentModel.DataAnnotations;

namespace ZWave.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
            return attribute == null ? value.ToString() : attribute.Name;
        }

        public static IEnumerable<string> GetDisplayNames<T>() where T : Enum
        {
            var enumType = typeof(T);
            var enumValues = Enum.GetValues(enumType).Cast<T>();

            foreach (var value in enumValues)
            {
                var field = enumType.GetField(value.ToString());
                var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
                yield return attribute != null ? attribute.Name : value.ToString();
            }
        }

        public static T GetEnumValue<T>(string displayName) where T : Enum
        {
            var enumType = typeof(T);
            var enumValues = Enum.GetValues(enumType).Cast<T>();

            foreach (var value in enumValues)
            {
                var field = enumType.GetField(value.ToString());
                var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
                if (attribute != null && attribute.Name == displayName)
                    return value;
            }
            throw new ArgumentException($"No enum member with display name '{displayName}' found in {enumType.Name}.");
        }
    }
}
