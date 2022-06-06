using System;
using System.Collections.Generic;

namespace BandAPI.Services
{
    public class PropertyMappingValue
    {

        //collection of resource props one single resource prop will mapp to
        public IEnumerable<string> DestinationProperties { get; set; }
        //allow us to revert sort order
        public bool Revert { get; set; }
        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
        {
            DestinationProperties = destinationProperties??
                throw new ArgumentNullException(nameof(destinationProperties));
            Revert = revert;
        }


    }
}
