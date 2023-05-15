using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Alpari.Models.Services;

namespace WebApi.Alpari.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoriesController(CategoryRepository category)
        {
            _categoryRepository = category;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryRepository.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_categoryRepository.Find(id));
        }

        [HttpPut]
        public IActionResult Put(CategoryDto categoryDto)
        {
            return Ok(_categoryRepository.Edit(categoryDto));
        }

        [HttpPost]
        public IActionResult Post(string Name)
        {
            var result = _categoryRepository.AddCategory(Name);
            return Created(Url.Action(nameof(Get), "Categories", new { Id= result },Request.Scheme),tr);
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            return Ok(_categoryRepository.Delete(Id));
        }

    }
}
