using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PatikaHomework1.Interfaces;
using PatikaHomework1.Models;

namespace PatikaHomework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: api/products
        [HttpGet]
        public IActionResult GetProducts(string name, int? pageNumber, int? pageSize)
        {
            var products = _repository.GetProducts(name, pageNumber, pageSize);

            if (!products.Any())
                return NotFound();

            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(long id)
        {
            var product = _repository.GetProduct(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.AddProduct(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task< IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productToUpdate = _repository.GetProduct(id);
            if (productToUpdate == null)
                return NotFound();
            await Task.Delay(1000);
            product.Id = id;
          
            _repository.UpdateProduct(product);

            return NoContent();
        }

        // PATCH: api/products/5
        [HttpPatch("{id}")]
        public IActionResult UpdateProductPartial(int id, [FromBody] JsonPatchDocument<Product> productPatch)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productToUpdate = _repository.GetProduct(id);
            if (productToUpdate == null)
                return NotFound();

            productPatch.ApplyTo(productToUpdate);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.UpdateProduct(productToUpdate);

            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _repository.GetProduct(id);
            if (product == null)
                return NotFound();

            _repository.DeleteProduct(id);

            return NoContent();
        }
    }

}