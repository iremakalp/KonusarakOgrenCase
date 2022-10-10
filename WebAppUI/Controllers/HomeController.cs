using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppUI.Models;

namespace WebAppUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        ProductListModel _productListModel;
        public HomeController(IProductService productService, ProductListModel productListModel)
        {
            _productService = productService;
            _productListModel = productListModel;
        }

        public IActionResult Index()
        {
            var result = _productService.GetDetails();
            _productListModel.ProductDetails = result.Data;
            if (result.Success)
            {
                return View(_productListModel);
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            var result = _productService.GetDetails();
           
            if (result.Success)
            {
                _productListModel.ProductDetails= result.Data.Where(b => b.BrandName == "Samsung").ToList();
                return View(_productListModel);
            }
            return View();
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Customer()
        {
            var result = _productService.GetDetails();
            _productListModel.ProductDetails = result.Data;
            if (result.Success)
            {
                return View(_productListModel);
            }
            return View();
        }

        [Authorize(Roles ="SysAdmin")]
        public IActionResult SysAdmin()
        {
            return View();
        }
    }
}
