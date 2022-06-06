using System.Collections.Generic;

namespace BandAPI.Services
{
    public interface IPropertyMappigService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidMappingExistis<TSource, TDestination>(string fields);
    }
}