using DemoApi.Models;
using DemoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
  
  
        [HttpGet("/test")]
        public IActionResult ThrowError()
        {
            throw new Exception("exception thrown");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var created = _service.Create(product);
            return Ok(created);
          
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            if (!_service.Update(id, product))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_service.Delete(id))
                return NotFound();

            return NoContent();
        }
    }
}
