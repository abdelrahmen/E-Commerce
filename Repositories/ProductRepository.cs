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

        public List<Product> GetAll()
        {
            return context.Product.Include(p => p.Category).ToList();
        }

        public void Add(Product product)
        {
            context.Product.Add(product);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = context.Product.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                context.Product.Remove(product);
                context.SaveChanges();
            }
        }

        public Product? Edit(Product product)
        {
            var productToEdit = context.Product.FirstOrDefault(p => p.Id == product.Id);
            if (productToEdit != null)
            {
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                productToEdit.Description = product.Description;
                productToEdit.CategoryId = product.CategoryId;
                productToEdit.image = product.image;
                context.SaveChanges();
            }
            return productToEdit;
        }


        public Product? GetById(int id)
        {
            var product = context.Product
                .Include(p => p.Category)
                .FirstOrDefault(m => m.Id == id);
            return product;
        }

        public string getImageName(int id)
        {
            return context.Product.FirstOrDefault (p => p.Id == id)?.image;
        }
    }
}
