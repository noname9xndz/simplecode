using System;
using System.Threading.Tasks;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [AllowAnonymous]
        [HttpGet("product/list-all")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("product/product-details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var customerViewModel = await _productAppService.GetById(id.Value);

            if (customerViewModel == null) return NotFound();

            return View(customerViewModel);
        }

        [HttpGet("product/register-new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("product/register-new")]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(productViewModel);
            
            if (ResponseHasErrors(await _productAppService.Register(productViewModel)))
                return View(productViewModel);

            ViewBag.Sucesso = "product Registered!";

            return View(productViewModel);
        }

        
        [HttpGet("product/edit-product/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var customerViewModel = await _productAppService.GetById(id.Value);

            if (customerViewModel == null) return NotFound();

            return View(customerViewModel);
        }

        [HttpPost("product/edit-product/{id:guid}")]
        public async Task<IActionResult> Edit(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(productViewModel);
            
            if (ResponseHasErrors(await _productAppService.Update(productViewModel)))
                return View(productViewModel);

            ViewBag.Sucesso = "product Updated!";

            return View(productViewModel);
        }

        [HttpGet("product/remove-product/{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var customerViewModel = await _productAppService.GetById(id.Value);

            if (customerViewModel == null) return NotFound();

            return View(customerViewModel);
        }

        [HttpPost("product/remove-product/{id:guid}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (ResponseHasErrors(await _productAppService.Remove(id)))
                return View(await _productAppService.GetById(id));

            ViewBag.Sucesso = "product Removed!";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet("product/product-history/{id:guid}")]
        public async Task<JsonResult> History(Guid id)
        {
            var customerHistoryData = await _productAppService.GetAllHistory(id);
            return Json(customerHistoryData);
        }
    }
}
