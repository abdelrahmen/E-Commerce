using E_Commerce.Models;

namespace E_Commerce.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();

        Product GetById(int id);

        void Add(Product product);

        void Edit(Product product);

        void Delete(Product product);
    }
}