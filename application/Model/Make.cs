using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace asp_dotnet_core_angular.Model
{
    public class Make
    {
        public int Id { get; set; }
        [StringLength(200)]
        [Required]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }
        public Make()
        {
            Models = new Collection<Model>();
        }
    }
}