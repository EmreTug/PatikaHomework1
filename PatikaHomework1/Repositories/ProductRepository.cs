using PatikaHomework1.Interfaces;
using PatikaHomework1.Models;

namespace PatikaHomework1.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly PatikaHomework1Context _context;

        public ProductRepository(PatikaHomework1Context context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts(string name, int? pageNumber, int? pageSize)
        {
            var products = _context.Products
                .Where(p => string.IsNullOrEmpty(name) || p.Name.Contains(name))
                .Skip((pageNumber ?? 0) * (pageSize ?? int.MaxValue))
                .Take(pageSize ?? int.MaxValue)
                .ToList();


            return products;
        }

        public Product GetProduct(long id)
        {
            return _context.Products.Find(id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }

}
