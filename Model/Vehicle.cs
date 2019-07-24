using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace asp_dotnet_core_angular.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<VehicleFeature> Features { get; set; }
        public ICollection<Photo> Photos {get;set;}

        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
            Photos = new Collection<Photo>();
        }
    }
}