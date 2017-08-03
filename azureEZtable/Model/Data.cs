// copyright msg systems ag
// Tobias Hoppenthaler - tobias.hoppenthaler@msg.group
using System;
using AppServiceHelpers.Models;

namespace Quickmail.Model
{
    public class GpsData : EntityData
    {//Latitude,Longitude,Accuracy,User,Route,Timestamp
		
        public string Latitude { get; set; }

		public string Longitude { get; set; }

		public string Accuracy { get; set; }

		public string User { get; set; }

		public string Route { get; set; }

		public string Timestamp { get; set; }

	}
}
