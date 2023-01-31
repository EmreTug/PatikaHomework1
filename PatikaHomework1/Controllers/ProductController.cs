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
        // IProductRepository interface'inden �rnek olu�turuldu
        private readonly IProductRepository _repository;


        // Constructor ile IProductRepository �rne�i al�nd�
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: api/products
        // �r�n listesi �a��r�l�r, isme, sayfa numaras�na ve sayfa boyutuna g�re filtreleme yap�labilir
        [HttpGet]
        public IActionResult GetProducts(string name, int? pageNumber, int? pageSize)
        {
            // �r�nler IProductRepository arac�l���yla �a�r�ld�
            var products = _repository.GetProducts(name, pageNumber, pageSize);

            // E�er �r�n bulunamazsa NotFound() d�nd�r�l�r
            if (!products.Any())
                return NotFound();

            // �r�nler bulunursa Ok(products) d�nd�r�l�r
            return Ok(products);
        }


        // GET: api/products/5
        // Verilen id de�erine sahip �r�n �a�r�l�r
        [HttpGet("{id}")]
        public IActionResult GetProduct(long id)
        {
            // �r�n IProductRepository arac�l���yla �a�r�ld�
            var product = _repository.GetProduct(id);

            // E�er �r�n bulunamazsa NotFound() d�nd�r�l�r
            if (product == null)
                return NotFound();

            // �r�n bulunursa Ok(product) d�nd�r�l�r
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            // E�er model ge�erli de�ilse, kullan�c�ya hatal� request d�n
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // �r�n� veritaban�na ekle
            _repository.AddProduct(product);

            // Ba�ar�l� bir �ekilde ekleme i�leminden sonra, eklenen �r�ne ait bilgileri d�nd�r
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // PUT: api/products/5
        // �r�n� g�ncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            // E�er model ge�erli de�ilse 400 Bad Request d�nd�r
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // G�ncellenecek �r�n� repository'den al
            var productToUpdate = _repository.GetProduct(id);
            // E�er �r�n yoksa 404 Not Found d�nd�r
            if (productToUpdate == null)
                return NotFound();

            // Asenkron bir �ekilde 1 saniye bekle
            await Task.Delay(1000);
            // �r�n id'sini g�ncelle
            product.Id = id;

            // �r�n� repository'de g�ncelle
            _repository.UpdateProduct(product);

            // ��lem tamamland�, 204 No Content d�nd�r
            return NoContent();
        }

        // PATCH: api/products/5
        // �r�n� k�smi olarak g�ncelleme
        [HttpPatch("{id}")]
        public IActionResult UpdateProductPartial(int id, [FromBody] JsonPatchDocument<Product> productPatch)
        {
            // E�er model ge�erli de�ilse 400 Bad Request d�nd�r
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // G�ncellenecek �r�n� repository'den al
            var productToUpdate = _repository.GetProduct(id);
            // E�er �r�n yoksa 404 Not Found d�nd�r
            if (productToUpdate == null)
                return NotFound();

            // �r�n� k�smi olarak g�ncelle
            productPatch.ApplyTo(productToUpdate);
            // E�er g�ncellenen model ge�erli de�ilse 400 Bad Request d�nd�r
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // �r�n� repository'de g�ncelle
            _repository.UpdateProduct(productToUpdate);

            // ��lem tamamland�, 204 No Content d�nd�r
            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            // �r�n bulunamad�ysa, "Not Found" d�nd�r
            var product = _repository.GetProduct(id);
            if (product == null)
                return NotFound();

            // �r�n� sil
            _repository.DeleteProduct(id);

            // ��erik yoksa "No Content" d�nd�r
            return NoContent();
        }
    }

}