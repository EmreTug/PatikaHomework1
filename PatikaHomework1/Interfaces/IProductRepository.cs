using PatikaHomework1.Models;

namespace PatikaHomework1.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(string name, int? pageNumber, int? pageSize);
        Product GetProduct(long id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(long id);
    }

}
