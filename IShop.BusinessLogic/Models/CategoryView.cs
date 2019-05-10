using IShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IShop.BusinessLogic.Models
{
    public class CategoryView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> products;

        public CategoryView(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            products = category.products;
        }
    }
}