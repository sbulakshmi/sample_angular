using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace asp_dotnet_core_angular.Controllers.Resources
{
    public class SaveVehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        public ContactResource Contact { get; set; }
        public ICollection<int> Features { get; set; }

        public SaveVehicleResource()
        {
            this.Features = new Collection<int>();
        }
    }
}