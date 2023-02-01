using PatikaHomework1.Interfaces;
using PatikaHomework1.Models;
using PatikaHomework1.ViewModels;

namespace PatikaHomework1.Repositories
{
    // Ürünle ilgili verilerin yönetildiği repositori sınıfı
    public class ProductRepository : IProductRepository
    {
        // Veri tabanı nesnesi
        private readonly PatikaHomework1Context _context;

        // Constructor
        public ProductRepository(PatikaHomework1Context context)
        {
            _context = context;
        }

        // Ürünlerin listelendiği metod
        public IEnumerable<ProductModel> GetProducts(string name, int? pageNumber, int? pageSize)
        {
            // Ürün adına göre filtreleme yapıldı ve sayfalama yapıldı
            var products = _context.Products
                .Where(p => string.IsNullOrEmpty(name) || p.Name.Contains(name))
                .Skip((pageNumber ?? 0) * (pageSize ?? int.MaxValue))
                .Take(pageSize ?? int.MaxValue)
                .ToList();
            var vm = products.Select(pr => new ProductModel { Description = pr.Description, Name = pr.Name,Id=pr.Id });
            // Filtrelenen ve sayfalanmış ürün listesi döndürülür
            return vm;
        }

        // ID'si verilen ürünün getirildiği metod
        public ProductModel GetProduct(long id)
        {
            Product product = _context.Products.Find(id);
            return new ProductModel { Name=product.Name, Description=product.Description,Id=product.Id};
            // Ürün ID'sine göre veritabanından ürün getirilir
            
        }

        // Ürün ekleme metodu
        public void AddProduct(ProductModel product)
        {
            // Ürün veritabanına eklenir
            _context.Products.Add(new Product { Name=product.Name,Description=product.Description});
            // Veritabanı güncellenir
            _context.SaveChanges();
        }

        // Ürün güncelleme metodu
        public void UpdateProduct(ProductModel product)
        {
            // Veritabanındaki ürün güncellenir
            _context.Products.Update(new Product { Name = product.Name, Description = product.Description,Id=product.Id });
            // Veritabanı güncellenir
            _context.SaveChanges();
        }

        // Ürün silme metodu
        public void DeleteProduct(long id)
        {
            // Veritabanındaki ürün ID'sine göre getirilir
            var product = _context.Products.Find(id);
            // Veritabanından ürün silinir
            _context.Products.Remove(product);
            // Veritabanı güncellenir
            _context.SaveChanges();
        }
    }
}
