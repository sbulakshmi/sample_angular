using System.ComponentModel.DataAnnotations;

namespace asp_dotnet_core_angular.Model
{
    public class Feature
    {
        public int Id { get; set; }
        [StringLength(200)]
        [Required]
        public string Name { get; set; }

    }
}