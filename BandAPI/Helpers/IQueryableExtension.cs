using BandAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace BandAPI.Helpers
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, Dictionary<string, PropertyMappingValue> mappingDictionary)
        {

            var orderByString = "";

            //sorting on strings; for that we will be using dynamic link we installed as nuget
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            if(mappingDictionary==null)
                throw new ArgumentNullException(nameof(mappingDictionary));
            
            if (string.IsNullOrWhiteSpace(orderBy))
                return source;

            //argumens (string separated with comma)
            var orderBySplit=orderBy.Split(",");

            foreach (var ordeByClause in orderBySplit)
            {
                var trimmedOrderBy = ordeByClause.Trim();
                var orderDesc = trimmedOrderBy.EndsWith(" desc");

                //argument may containd asc or desc we need to strip ti from it in order to get the actual name of the property
                //meaning the orderBy can have "name desc", and we don't want the " desc" because it is not part of name of property
                //beacuse of that we are searching for the index of first index of space
                var indexOfSpace = trimmedOrderBy.IndexOf(" ");

                //if indextOfSpace is -1, meaning there is no space (tha is just the property name), return trimmedOrderBy, 
                //else remove space and everythig after it which leaves us only property name
                var propertyName = indexOfSpace == -1 ? trimmedOrderBy : trimmedOrderBy.Remove(indexOfSpace);

                //if this property is not part of it means that mapping for this porperty name doesn't exist
                if (!mappingDictionary.ContainsKey(propertyName))
                    throw new ArgumentException("Mapping does not exist for " + propertyName);

                var propertyMappingValue = mappingDictionary[propertyName];
                if (propertyMappingValue == null)
                    throw new ArgumentNullException(nameof(propertyMappingValue));

                //chech if the order need to be reverted from asc to desc and vice versa
                //
                foreach (var destination in propertyMappingValue.DestinationProperties.Reverse())
                {
                    //check sorting order 
                    if (propertyMappingValue.Revert)
                        orderDesc = !orderDesc; //if it is - reverte the order

                   //create order by string 
                   //argument separated by comma + asc or desc + 
                    orderByString = orderByString +
                        (!string.IsNullOrWhiteSpace(orderByString) ? "," : "") + destination +
                        (orderDesc ? " descending " : " ascending ");
                }

            }
            return source.OrderBy(orderByString);

        }
    }
}
