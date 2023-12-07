using ecommerce_dotnet.DTOs.Product;
using ecommerce_dotnet.Models;
using ecommerce_dotnet.Services.Interfaces;
using ecommerce_dotnet.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ecommerce_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = Constants.Roles.Admin)]
        [HttpPost("new")]
        public async Task<IActionResult> NewProduct([FromBody]NewProductModel newProductModel)
        {
            if (newProductModel == null || newProductModel.Name == null || newProductModel.Price < 0)
                return BadRequest(JsonResponse.Error(Constants.Response.General.BadRequest));

            await _productService.AddAsync(new Product()
            {
                Name = newProductModel.Name,
                Price = newProductModel.Price,
                Description = newProductModel.Description
            });

            return StatusCode((int)HttpStatusCode.Created, JsonResponse.Success(Constants.Response.Product.NewProduct));
        }
    }
}
