using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext context;

        public CartRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void AddToCart(CartItem item)
        {
            var existingItem = context.CartItem.FirstOrDefault(c => (c.UserId == item.UserId && c.ProductId == item.ProductId));
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                context.CartItem.Add(item);
            }
            context.SaveChanges();
        }

        public List<CartItem> GetUserCartItems(string id)
        {
            return context.CartItem.Include(c => c.Product).Where(c => c.UserId == id).ToList();
        }

        public void DeleteUserCartItem(CartItem cartItem)
        {
            context.CartItem.Remove(cartItem);
            context.SaveChanges();
        }

        public void DeleteAllUserCartItems(string userId)
        {
            context.CartItem.Where(c => c.UserId == userId).ExecuteDelete();
        }

        public CartItem? GetCartItem(int id)
        {
            return context.CartItem.Include(c => c.Product).FirstOrDefault(c => c.Id == id);
        }

        public void EditCartItems(CartItem item)
        {
            var itemToEdit = context.CartItem.FirstOrDefault(c => c.Id == item.Id);
            if (itemToEdit != null)
            {
                itemToEdit.Quantity = item.Quantity;
                context.SaveChanges();
            }
        }

    }
}
