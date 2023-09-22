using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using E_Commerce.Repositories.Interfaces;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IOrdersRepository ordersRepository;
        private readonly ICartRepository cartRepository;

        public OrdersController(AppDbContext context, IOrdersRepository ordersRepository, ICartRepository cartRepository)
        {
            _context = context;
            this.ordersRepository = ordersRepository;
            this.cartRepository = cartRepository;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = ordersRepository.GetUserOrdersWithDetails(userId);
            return View(orders);
        }

        // GET: Orders/Details/5
        public IActionResult Details(int id)
        {
            var order = ordersRepository.GetOrderWithDetails(id);
            if (order != null)
            {
                return View(order);
            }
            return View("DoesNotExist");
        }

        // GET: Orders/PlaceOrder
        [HttpGet]
        public IActionResult PlaceOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = cartRepository.GetUserCartItems(userId);
            ViewData["total"] = cartItems.Select(c => c.Product.Price * c.Quantity).Sum() + 10;

            return View(cartItems);
        }

        // Get: Orders/PlaceOrderConfirmed
        public async Task<IActionResult> PlaceOrderConfirmed()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ordersRepository.ConfirmOrder(userId);
            return RedirectToAction("Index");
        }

        //Get: Orders/Manage
        [Authorize(Roles ="Admin,SuperAdmin")]
        public IActionResult Manage()
        {
            var orders = ordersRepository.GetAllOrders();
            return View(orders);
        }

		//Post: Orders/AdminConfirmOrder
		[Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public IActionResult AdminConfirmOrder(int id)
        {
            ordersRepository.AdminConfirmOrder(id);
            return RedirectToAction("Manage");
        }
	}
}
