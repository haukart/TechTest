using System.ComponentModel.DataAnnotations;

namespace TechTestApi.Models
{
    public class CreateUpdateGroupModel
    {
        public string GroupId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
