
using System.ComponentModel.DataAnnotations;

namespace IShop.Domain.Models
{
    public class Category : IIdentifiable
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
