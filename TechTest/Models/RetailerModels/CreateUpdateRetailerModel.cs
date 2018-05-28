using System.ComponentModel.DataAnnotations;

namespace TechTestApi.Models
{
    public class CreateUpdateRetailerModel
    {
        [Required]
        public string Name { get; set; }

        public string GroupId { get; set; }

        public string RetailerId { get; set; }
    }
}
