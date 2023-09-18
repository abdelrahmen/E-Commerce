using E_Commerce.Models;

namespace E_Commerce.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();

        Product? GetById(int id);

        void Add(Product product);

        Product? Edit(Product product);

        void Delete(int id);

        string getImageName(int id);
    }
}