using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Models;
using E_Commerce.Repositories.Interfaces;
using E_Commerce.ViewModels;
using System.IO;

namespace E_Commerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductsController(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment webHostEnvironment
            )
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public IActionResult Index()
        {
            var products = productRepository.GetAll();
            return View(products);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var product = productRepository.GetById(id);
            if (product != null)
            {
                return View(product);
            }

            return View("doesNotExist");
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new Product
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    CreatedAt = DateTime.Now,
                    image = UploadedFile(productVM),
                    Price = productVM.Price,
                    CategoryId = productVM.CategoryId,
                };
                productRepository.Add(newProduct);
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "Id", "Name", productVM.CategoryId);
            return View(productVM);
        }

        public IActionResult Manage()
        {
            var products = productRepository.GetAll();
            return View(products);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            var product = productRepository.GetById(id);
            if (product != null)
            {
                var editProductVM = EditProductViewModel.FromProduct(product);
                ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "Id", "Name", product.CategoryId);
                return View(editProductVM);
            }
            return View("doesNotExist");
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var editedProduct = new Product
                {
                    Id = productVM.Id,
                    Name = productVM.Name,
                    CategoryId = productVM.CategoryId,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    CreatedAt = productVM.CreatedAt,
                };
                if (productVM.image == null)
                {
                    editedProduct.image = productRepository.getImageName(productVM.Id);
                }
                else
                {
                    var imageName = productRepository.getImageName(productVM.Id);
                    if (!imageName.Equals("no-image.jpg"))
                    {
                        System.IO.File.Delete(Path.Combine(webHostEnvironment.WebRootPath, "images", "Products", imageName));
                    }
                    editedProduct.image = UploadedFile(productVM);
                }
                productRepository.Edit(editedProduct);
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "Id", "Name", productVM.CategoryId);
            return View(productVM);
        }



        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = productRepository.GetById(id);
            if (product != null)
            {
                return View(product);
            }
            return View("doesNotExist");
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = productRepository.GetById(id);
            if (product != null)
            {
                if (!product.image.Equals("no-image.jpg"))
                {
                    System.IO.File.Delete(Path.Combine(webHostEnvironment.WebRootPath, "images", "Products", product.image));
                }
                productRepository.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private string UploadedFile(IProductViewModelImage model)
        {
            string uniqueFileName = Path.Combine(webHostEnvironment.WebRootPath, "images", "no-image.jpg");

            if (model.image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "Products");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

