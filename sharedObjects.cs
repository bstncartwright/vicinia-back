using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vicinia
{
    public class sharedObjects
    {


        public class Location
        {
            [JsonProperty(PropertyName = "long")]
            public double longitude { get; set; }

            [JsonProperty(PropertyName = "lat")]
            public double latitiude { get; set; }
        }

        public bool isInProximity(Location location)
        {
            
        }
    }
}


