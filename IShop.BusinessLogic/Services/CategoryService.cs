using IShop.BusinessLogic.Models;
using IShop.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IShop.BusinessLogic.Services
{
    public interface ICategoryService
    {
        void Add(Category category);

        void Update(Category category);

        void Delete(int id);

        List<Category> GetAll();

        CategoryView GetAllProducts(int id);

        Category Get(int id);

        /// <summary>
        /// Get sorted list
        /// </summary>
        /// <param name="sortParam">parameter by which want to sort</param>
        /// <returns>IEnumerable</returns>
        IEnumerable<Category> GetSortedList(string sortParam);
    }

    public class CategoryService : ServiceBase, ICategoryService
    {
        private const string CategoryFilePath = @"\bin\Data\Categories.txt";
        //private const string ProductFilePath = @"\bin\Data\Products.txt";
        private readonly ProductService _productService;

        private List<Category> _categories;

        public CategoryService()
        {
            var savedData = ReadData();

            _categories =
                savedData != null
                ? savedData
                : new List<Category>();
            _productService = new ProductService();
        }

        public void Add(Category category)
        {
            int id = GetMaxId(_categories
                                    .OfType<IIdentifiable>()
                                    .ToList());

            category.Id = id + 1;

            _categories.Add(category);

            SaveChanges();
        }

        public void Delete(int id)
        {
            var category = Get(id);

            if (category != null)
            {
                _categories.Remove(category);
            }

            SaveChanges();
        }

        public Category Get(int id)
        {
            return _categories
                        .FirstOrDefault(x => x.Id == id);
        }

        public List<Category> GetAll()
        {
            return _categories;
        }

        /// <summary>
        /// Get all categories with products
        /// </summary>
        /// <returns>category list</returns>
        public CategoryView GetAllProducts(int id)
        {
            var products = _productService.GetProductsByCategory(id);
            var category = this.Get(id);
            category.products = products;

            return new CategoryView(category);
        }

        public IEnumerable<Category> GetSortedList(string sortParam)
        {
            var propertyInfo = typeof(Category).GetProperty(sortParam);
            return _categories.OrderBy(c => propertyInfo.GetValue(c));
        }

        public void Update(Category category)
        {
            var oldCategory = Get(category.Id);

            oldCategory.Name = category.Name;

            SaveChanges();
        }

        private List<Category> ReadData()
        {
            var data = File.ReadAllText(GetStoragePath(CategoryFilePath));

            return JsonConvert.DeserializeObject<List<Category>>(data);
        }

        private void SaveChanges()
        {
            var data = JsonConvert.SerializeObject(_categories);

            File.WriteAllText(GetStoragePath(CategoryFilePath), data);
        }

        //private List<Product> GetProducts()
        //{
        //    var data = File.ReadAllText(GetStoragePath(ProductFilePath));

        //    return JsonConvert.DeserializeObject<List<Product>>(data);
        //}
    }
}
