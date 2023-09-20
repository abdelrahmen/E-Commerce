using E_Commerce.Models;

namespace E_Commerce.Repositories.Interfaces
{
	public interface ICartRepository
	{
		CartItem? GetCartItem(int id);

		List<CartItem> GetUserCartItems(String id);

		void AddToCart(CartItem item);

		void DeleteUserCartItem(CartItem cartItem);

		void DeleteAllUserCartItems(string userId);

		void EditCartItems(CartItem item);

	}
}
