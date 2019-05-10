
using System.ComponentModel.DataAnnotations;

namespace IShop.Domain.Models
{
    public class Product : IIdentifiable
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}
