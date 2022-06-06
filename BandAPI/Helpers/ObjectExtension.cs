using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace BandAPI.Helpers
{
    public static class ObjectExtension
    {
        public static ExpandoObject ShapeData<TSource>(this TSource source, string fields)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var dataShapeObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                //return property infos for the TSource we are passing in and it return information for the public, as well as on intance properties
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                //looping through property infos
                foreach (var propertyInfo in propertyInfos)
                {
                    var propertyValue = propertyInfo.GetValue(source);

                    //add new property value with the name of property from property info object and the value that we just got
                    ((IDictionary<string, object>)dataShapeObject)
                    .Add(propertyInfo.Name, propertyValue);
                }
                return dataShapeObject;

            }

            var fieldsAfterSplit = fields.Split(",");
            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                    throw new Exception(propertyName.ToString() + " was not found.");

                var propertyValue = propertyInfo.GetValue(source);

                //add new property value with the name of property from property info object and the value that we just got
                ((IDictionary<string, object>)dataShapeObject)
                .Add(propertyInfo.Name, propertyValue);
            }



            return dataShapeObject;
        }
    }
}
