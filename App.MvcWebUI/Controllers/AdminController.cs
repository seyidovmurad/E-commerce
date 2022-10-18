using App.Business.Abstract;
using App.Entities.Concrete;
using App.MvcWebUI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace App.MvcWebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = _productService.GetAll(),
                IsAdmin = HttpContext.User.FindFirst(ClaimTypes.Role).Value == "Admin"
            };
            return View(model);
        }

        public IActionResult Add()
        {
            var model = new AddEditProductViewModel
            {
                Product = new Product(),
                Categories = _categoryService.GetAll(),
                IsActionAdd = true
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if(ModelState.IsValid)
            {
                _productService.Add(product);
                TempData.Add("message", "Product Added Successfully");
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int productId)
        {
            var model = new AddEditProductViewModel
            {
                Product = _productService.GetById(productId),
                Categories = _categoryService.GetAll(),
                IsActionAdd = false
            };
            return View("Add", model);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if(ModelState.IsValid)
            {
                _productService.Update(product);
                TempData.Add("message", "Product updated successfully");
                return RedirectToAction("Index");
            }
            return View("Add");
        }

        public IActionResult Delete(int productId)
        {
            try
            {
                _productService.Delete(productId);
                TempData.Add("message", "Product deleted successfully");
            }
            catch
            {
                TempData.Add("message", "Something went wrong!");
            }
            return RedirectToAction("Index");
        }
    }
}
