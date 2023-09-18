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
using E_Commerce.ViewModels;

namespace E_Commerce.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoriesController(ICategoryRepository CategoryRepository)
		{
			_categoryRepository = CategoryRepository;
		}

		// GET: Categories
		//for all users
		public IActionResult Index()
		{
			var categories = _categoryRepository.GetAll();
			return View(categories);
		}

		// GET: Categories/Manage
		public IActionResult Manage()
		{
			var categories = _categoryRepository.GetAll();
			return View(categories);
		}

		// GET: Categories/Details/5
		public IActionResult Details(int id)
		{
			var category = _categoryRepository.GetById(id);
			if (category != null)
			{
				return View(category);
			}
			return View("doesNotExist");
		}

		// GET: Categories/Create
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CategoryViewModel categoryVM)
		{
			if (ModelState.IsValid)
			{
				var category = new Category
				{
					Name = categoryVM.Name,
					Description = categoryVM.Description,
					CreatedAt = DateTime.Now,
				};
				_categoryRepository.AddCategory(category);
				return View(categoryVM);
			}
			return View(ModelState);
		}

		// GET: Categories/Edit/5
		public IActionResult Edit(int id)
		{
			var category = _categoryRepository.GetById(id);
			return View(category);
		}

		// POST: Categories/Edit/5

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, Category category)
		{
			if (ModelState.IsValid)
			{
				_categoryRepository.Edit(category);
			}
			return View(category);
		}

		// GET: Categories/Delete/5
		public IActionResult Delete(int? id)
		{
			var category = _categoryRepository.GetById(id.Value);
			if(category != null)
			{
				return View(category);
			}
			return View("doesNotExist");
		}

		// POST: Categories/Delete/5
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			_categoryRepository.Delete(id);
			return RedirectToAction("index");
		}
	
		public IActionResult CategoryProducts(int id)
		{
			var categoryWithProducts = _categoryRepository.GetById(id);
			return View(categoryWithProducts);
		}

	}
}
