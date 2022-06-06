using BandAPI.Dtos;
using BandAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BandAPI.Services
{
    public class PropertyMappigService : IPropertyMappigService
    {
        private Dictionary<string, PropertyMappingValue> _bandPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
        {
                {"Id",new PropertyMappingValue(new List<string>(){"Id"}) },
                {"Name",new PropertyMappingValue(new List<string>(){"Name"}) },
                {"MainGenre",new PropertyMappingValue(new List<string>(){"MainGenre"}) },
                {"FoundedYearsAgo",new PropertyMappingValue(new List<string>(){"Founded"},true) },
        };

        private IList<IPropertyMappingMarker> _propertyMappings = new List<IPropertyMappingMarker>();

        public PropertyMappigService()
        {
            _propertyMappings.Add(new PropertyMapping<BandDto, Band>(_bandPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMApping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMApping.Count() == 1)
                return matchingMApping.First().MappingDictionary;

            throw new Exception("No mapping was found.");
        }

        public bool ValidMappingExistis<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();
            //no mapping needs to exists if orderBy clause wa snot supplied. by default is sortd by name

            if (string.IsNullOrWhiteSpace(fields))
                return true;

            var fieldsAfterSplit = fields.Split(",");
            foreach (var f in fieldsAfterSplit)
            {
                var trimmedFiled = f.Trim();
                var indexOfSpace = trimmedFiled.IndexOf(" ");
                var propertyName= indexOfSpace==-1? trimmedFiled: trimmedFiled.Remove(indexOfSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                    return false; //mapping doens't exist

            }
            return true;
        }
    }
}
