using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public void Edit(Product product)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return context.Product.Include(p => p.Category).ToList();
        }

        public Product GetById(int id)
        {
            var product = context.Product
                .Include(p => p.Category)
                .FirstOrDefault(m => m.Id == id);
            return product;
        }
    }
}
