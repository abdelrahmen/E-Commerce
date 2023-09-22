using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext context;

        public OrdersRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void ConfirmOrder(string userId)
        {
            var cartItems = context.CartItem.Include(c => c.Product).Where(c => c.UserId == userId).ToList();
            if (cartItems != null && cartItems.Count > 0)
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var totalWithNoTax = cartItems.Select(c => c.Product.Price * c.Quantity).Sum();
                        var order = new Order
                        {
                            UserId = userId,
                            CreatedAt = DateTime.Now,
                            Amount = totalWithNoTax,
                            Total = totalWithTax(totalWithNoTax),
                        };
                        context.Order.Add(order);
                        context.SaveChanges();
                        foreach (var item in cartItems)
                        {
                            var orderDetails = new OrderDetail
                            {
                                OrderId = order.Id,
                                ProductId = item.ProductId,
                                Price = item.Product.Price,
                                Quantity = item.Quantity,
                                Total = item.Quantity * item.Product.Price,
                            };
                            context.OrderDetail.Add(orderDetails);
                            context.CartItem.Remove(item);
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public Order GetOrderWithDetails(int id)
        {
            var order = context.Order.Include(o => o.OrderDetails).ThenInclude(od => od.Product).FirstOrDefault(o => o.Id == id);
            return order;
        }

        public List<Order> GetUserOrders(string id)
        {
            var orders = context.Order.Where(o => o.UserId == id).ToList();
            return orders;
        }
        public List<Order> GetUserOrdersWithDetails(string id)
        {
            var orders = context.Order
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .Where(o => o.UserId == id).ToList();
            return orders;
        }

        private decimal totalWithTax(decimal total) => total + 10;
    }
}
