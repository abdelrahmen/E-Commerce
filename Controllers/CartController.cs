using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartRepository cartRepository;
		private readonly UserManager<User> userManager;

		public CartController(ICartRepository cartRepository, UserManager<User> userManager)
		{
			this.cartRepository = cartRepository;
			this.userManager = userManager;
		}

		// GET: Cart
		[Authorize]
		public async Task<IActionResult> Index()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var cartItems = cartRepository.GetUserCartItems(userId);
			return View(cartItems);
		}

		[HttpPost]
		[Authorize]
		public IActionResult AddToCart(CartItem item)
		{
			item.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			cartRepository.AddToCart(item);
			return RedirectToAction("Index", "Products");
		}

		[Authorize]
		public IActionResult EmptyCart()
		{
			var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			cartRepository.DeleteAllUserCartItems(UserId);
			return RedirectToAction("Index");
		}

		// GET: Cart/Edit/5

		[Authorize]
		[HttpGet]
        public IActionResult Edit(int id)
        {
            var cartItem = cartRepository.GetCartItem(id);
            if (cartItem != null)
            {
                return View(cartItem);
            }
            return View("DoesNotExist");
        }
		[Authorize]
		[HttpPost]
		public IActionResult Edit(CartItem item) 
		{
			cartRepository.EditCartItems(item);
			return RedirectToAction("Index");
		}


        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int id)
		{
			var cartItem = cartRepository.GetCartItem(id);
			if (cartItem != null)
			{
				cartRepository.DeleteUserCartItem(cartItem);
			}
			return RedirectToAction("Index");
		}
	}
}
