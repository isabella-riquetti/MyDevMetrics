using System;
using System.Collections.Generic;
using System.Reflection;

namespace CombinedCodingStats.Helper
{
    public static class ObjectExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            _SetValueToExplicitProperties(ref someObject, someObjectType, source);
            
            return someObject;
        }

        private static void _SetValueToExplicitProperties<T>(ref T someObject, Type someObjectType, IDictionary<string, object> source)
        {
            foreach (var item in source)
            {
                var property = someObjectType
                    .GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    property.SetValue(someObject, item.Value, null);
                }
            }
        }
    }
}
