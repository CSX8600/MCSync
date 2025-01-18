using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;

namespace MesaSuite.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static T Cast<T>(this object anObject, T defaultIfNullOrException = default(T))
        {
            if (anObject == null)
            {
                return defaultIfNullOrException;
            }

            try
            {
                if (anObject is JArray jArray)
                {
                    return jArray.ToObject<T>();
                }

                return (T)Convert.ChangeType(anObject, typeof(T));
            }
            catch
            {
                return defaultIfNullOrException;
            }
        }

        public static T ShallowClone<T>(this T anObject) where T : new()
        {
            T clone = new T();
            foreach (var property in anObject.GetType().GetProperties().Where(p => !(p.PropertyType is IEnumerable) && p.CanWrite))
            {
                property.SetValue(clone, property.GetValue(anObject));
            }

            return clone;
        }
    }
}
