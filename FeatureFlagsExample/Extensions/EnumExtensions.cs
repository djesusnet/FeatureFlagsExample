using System.ComponentModel;

namespace FeatureFlagsExample.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var enumType = value.GetType();

        var fieldInfo = enumType.GetField(value.ToString());

        var descriptionAttribute =
            (DescriptionAttribute) Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

        if (descriptionAttribute != null)
        {
            return descriptionAttribute.Description;
        }
        else
        {
            return value.ToString();
        }
    }

}