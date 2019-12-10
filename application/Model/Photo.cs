using System.ComponentModel.DataAnnotations;

namespace asp_dotnet_core_angular.Model
{
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        public int vehicleId { get; set; }
    }
}