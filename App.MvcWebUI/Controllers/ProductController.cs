using App.Business.Abstract;
using App.Entities.Concrete;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace App.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        private ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(int page=1,int category=0, int desc=0, string param = "")
        {
            int pageSize = 10;

            var propertyInfo = typeof(Product).GetProperty(param ?? "");
            List<Product> products = desc == 0 ? _productService.GetByCategory(category) : desc > 0 ? _productService.GetByCategory(category).OrderBy(p => propertyInfo.GetValue(p, null)).ToList() : _productService.GetByCategory(category).OrderByDescending(p => propertyInfo.GetValue(p, null)).ToList();
           
            var model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount=(int)Math.Ceiling(products.Count/(double)pageSize),
                PageSize=pageSize,
                CurrentCategory=category,
                CurrentPage=page,
                Desc = desc,
                Param = param
            };
            return View(model);
        }

        public IActionResult Delete(int category)
        {
            _categoryService.Delete(category);
            
            return Redirect("/product/index");
        }

        public IActionResult Add(string categoryName)
        {
            _categoryService.Add(new Category { CategoryName = categoryName });
            return Redirect("/product/index");
        }
    }
}
