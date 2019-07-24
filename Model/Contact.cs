using System.ComponentModel.DataAnnotations;

namespace asp_dotnet_core_angular.Model
{
    public class Contact
    {
    public int ContactId { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    [StringLength(255)]
    public string Email { get; set; }
    [Required]
    [StringLength(255)]
    public string Phone { get; set; }
    }
}