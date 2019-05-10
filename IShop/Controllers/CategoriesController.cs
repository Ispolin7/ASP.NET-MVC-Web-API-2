using IShop.BusinessLogic.Services;
using IShop.Domain.Models;
using System.Linq;
using System.Web.Http;

namespace IShop.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private ICategoryService _categoryService = new CategoryService();

        [HttpGet]
        public IHttpActionResult GatAll([FromUri] string sortParam = "Id")
        {
            return Ok(_categoryService.GetSortedList(sortParam));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var category = _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody] Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                return BadRequest("Can't be empty");
            }
            _categoryService.Add(category);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody] Category category)
        {
            _categoryService.Update(category);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/products")]
        public IHttpActionResult GatRelationships([FromUri] int id)
        {
            var category = _categoryService.GetAllProducts(id);
            return Ok(category);
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search(string name)
        {
            var categories = _categoryService.GetAll();
            categories = categories.Where(c => c.Name.ToLower().Contains(name)).ToList();
            return Ok(categories);
        }

        [HttpGet]
        [Route("count")]
        public IHttpActionResult ProductsCount()
        {
            return Ok(_categoryService.GetCountedProducts());
        }
    }
}
