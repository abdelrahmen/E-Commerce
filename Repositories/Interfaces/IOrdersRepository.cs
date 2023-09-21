using E_Commerce.Models;

namespace E_Commerce.Repositories.Interfaces
{
	public interface IOrdersRepository
	{
		List<Order> GetUserOrders(string id);

		List<Order> GetUserOrdersWithDetails(string id);

		Order GetOrderWithDetails(int id);

		void ConfirmOrder(string userId);
	}
}
