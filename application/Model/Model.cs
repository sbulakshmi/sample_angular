using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_dotnet_core_angular.Model
{
 [Table("Models")]   
 public class Model
    {
        public int Id { get; set; }
        [StringLength(200)]
        [Required]
         public string Name { get; set; }

        public Make Make { get; set; }
        public int MakeId { get; set; }
    }
}