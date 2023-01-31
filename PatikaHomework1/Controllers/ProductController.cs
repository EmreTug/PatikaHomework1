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
        // IProductRepository interface'inden örnek oluþturuldu
        private readonly IProductRepository _repository;


        // Constructor ile IProductRepository örneði alýndý
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: api/products
        // Ürün listesi çaðýrýlýr, isme, sayfa numarasýna ve sayfa boyutuna göre filtreleme yapýlabilir
        [HttpGet]
        public IActionResult GetProducts(string name, int? pageNumber, int? pageSize)
        {
            // Ürünler IProductRepository aracýlýðýyla çaðrýldý
            var products = _repository.GetProducts(name, pageNumber, pageSize);

            // Eðer ürün bulunamazsa NotFound() döndürülür
            if (!products.Any())
                return NotFound();

            // Ürünler bulunursa Ok(products) döndürülür
            return Ok(products);
        }


        // GET: api/products/5
        // Verilen id deðerine sahip ürün çaðrýlýr
        [HttpGet("{id}")]
        public IActionResult GetProduct(long id)
        {
            // Ürün IProductRepository aracýlýðýyla çaðrýldý
            var product = _repository.GetProduct(id);

            // Eðer ürün bulunamazsa NotFound() döndürülür
            if (product == null)
                return NotFound();

            // Ürün bulunursa Ok(product) döndürülür
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            // Eðer model geçerli deðilse, kullanýcýya hatalý request dön
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ürünü veritabanýna ekle
            _repository.AddProduct(product);

            // Baþarýlý bir þekilde ekleme iþleminden sonra, eklenen ürüne ait bilgileri döndür
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // PUT: api/products/5
        // Ürünü güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            // Eðer model geçerli deðilse 400 Bad Request döndür
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Güncellenecek ürünü repository'den al
            var productToUpdate = _repository.GetProduct(id);
            // Eðer ürün yoksa 404 Not Found döndür
            if (productToUpdate == null)
                return NotFound();

            // Asenkron bir þekilde 1 saniye bekle
            await Task.Delay(1000);
            // Ürün id'sini güncelle
            product.Id = id;

            // Ürünü repository'de güncelle
            _repository.UpdateProduct(product);

            // Ýþlem tamamlandý, 204 No Content döndür
            return NoContent();
        }

        // PATCH: api/products/5
        // Ürünü kýsmi olarak güncelleme
        [HttpPatch("{id}")]
        public IActionResult UpdateProductPartial(int id, [FromBody] JsonPatchDocument<Product> productPatch)
        {
            // Eðer model geçerli deðilse 400 Bad Request döndür
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Güncellenecek ürünü repository'den al
            var productToUpdate = _repository.GetProduct(id);
            // Eðer ürün yoksa 404 Not Found döndür
            if (productToUpdate == null)
                return NotFound();

            // Ürünü kýsmi olarak güncelle
            productPatch.ApplyTo(productToUpdate);
            // Eðer güncellenen model geçerli deðilse 400 Bad Request döndür
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ürünü repository'de güncelle
            _repository.UpdateProduct(productToUpdate);

            // Ýþlem tamamlandý, 204 No Content döndür
            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            // Ürün bulunamadýysa, "Not Found" döndür
            var product = _repository.GetProduct(id);
            if (product == null)
                return NotFound();

            // Ürünü sil
            _repository.DeleteProduct(id);

            // Ýçerik yoksa "No Content" döndür
            return NoContent();
        }
    }

}