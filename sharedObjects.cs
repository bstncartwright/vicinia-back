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
            public Location(double longitude, double latitude)
            {
                Longitude = longitude;
                Latitiude = latitude;
            }

            [JsonProperty(PropertyName = "long")]
            public double Longitude { get; set; }

            [JsonProperty(PropertyName = "lat")]
            public double Latitiude { get; set; }
        }

        public class Message
        {
            [JsonProperty(PropertyName = "id")]
            public string messageId { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string username { get; set; }

            [JsonProperty(PropertyName = "text")]
            public string messageText { get; set; }

            [JsonProperty(PropertyName = "time")]
            public DateTime timeOfMessage { get; set; }

            [JsonProperty(PropertyName = "location")]
            public Location messageLocation { get; set; }

            public Message(string name, string text, DateTime time, Location location)
            {
                messageId = null;
                username = name;
                messageText = text;
                timeOfMessage = time;
                messageLocation = location;
            }
        }

        
    }
}


