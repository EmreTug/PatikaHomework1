using PatikaHomework1.Models;
using PatikaHomework1.ViewModels;

namespace PatikaHomework1.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> GetProducts(string name, int? pageNumber, int? pageSize);
        ProductModel GetProduct(long id);
        void AddProduct(ProductModel product);
        void UpdateProduct(ProductModel product);
        void DeleteProduct(long id);
    }

}
