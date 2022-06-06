using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace BandAPI.Helpers
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source, string fileds)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var objectList = new List<ExpandoObject>();

            //PropertyInfo contains information on obj property, so in list we will have the information for each
            //property of the object in expando object            
            var propertyInfoList = new List<PropertyInfo>();
            if (string.IsNullOrWhiteSpace(fileds))
            {
                //return property infos for the TSource we are passing in and it return information for the public, as well as on intance properties
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                var fieldsAfterSplit = fileds.Split(",");
                foreach (var field in fieldsAfterSplit)
                {
                    var propertyName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo == null)
                        throw new Exception(propertyName.ToString() + " was not found.");

                    propertyInfoList.Add(propertyInfo);

                }
            }

            foreach (TSource sourceObject in source)
            {
                var dataShapeObject = new ExpandoObject();

                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(sourceObject);

                    //add new property value with the name of property from property info object and the value that we just got
                    ((IDictionary<string, object>)dataShapeObject)
                    .Add(propertyInfo.Name, propertyValue);
                }
                //add new expando object to expendo object list
                objectList.Add(dataShapeObject);
            }
            return objectList;

        }
    }
}
