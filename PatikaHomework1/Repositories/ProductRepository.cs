using PatikaHomework1.Interfaces;
using PatikaHomework1.Models;

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
        public IEnumerable<Product> GetProducts(string name, int? pageNumber, int? pageSize)
        {
            // Ürün adına göre filtreleme yapıldı ve sayfalama yapıldı
            var products = _context.Products
                .Where(p => string.IsNullOrEmpty(name) || p.Name.Contains(name))
                .Skip((pageNumber ?? 0) * (pageSize ?? int.MaxValue))
                .Take(pageSize ?? int.MaxValue)
                .ToList();

            // Filtrelenen ve sayfalanmış ürün listesi döndürülür
            return products;
        }

        // ID'si verilen ürünün getirildiği metod
        public Product GetProduct(long id)
        {
            // Ürün ID'sine göre veritabanından ürün getirilir
            return _context.Products.Find(id);
        }

        // Ürün ekleme metodu
        public void AddProduct(Product product)
        {
            // Ürün veritabanına eklenir
            _context.Products.Add(product);
            // Veritabanı güncellenir
            _context.SaveChanges();
        }

        // Ürün güncelleme metodu
        public void UpdateProduct(Product product)
        {
            // Veritabanındaki ürün güncellenir
            _context.Products.Update(product);
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
