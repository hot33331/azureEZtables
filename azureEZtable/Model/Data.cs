// copyright msg systems ag
// Tobias Hoppenthaler - tobias.hoppenthaler@msg.group
using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace azureEZtable.Model
{
    public class Data 
    {//Latitude,Longitude,Accuracy,User,Route,Timestamp
		[JsonProperty(PropertyName = "latitude")]
        public string Latitude { get; set; }
        [JsonProperty(PropertyName = "longitude")]
		public string Longitude { get; set; }
        [JsonProperty(PropertyName = "accuracy")]
		public string Accuracy { get; set; }
        [JsonProperty(PropertyName = "user")]
		public string User { get; set; }
        [JsonProperty(PropertyName = "route")]
		public string Route { get; set; }
        [JsonProperty(PropertyName = "timestamp")]
		public string Timestamp { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

		[Version]
		public string Version { get; set; }

	}
}
