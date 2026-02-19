using System;
using System.Reflection;


namespace UI.Utils
{
    public static class EnumExtensions
    {
        public static string GetLocName(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = fieldInfo?.GetCustomAttribute<LocNameAttribute>();

            return attribute?.Key ?? value.ToString();
        }
    }

}

