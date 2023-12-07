﻿using ecommerce_dotnet.DTOs.Product;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(JsonResponse.Error(Constants.Response.General.BadRequest));

            Product? product = await _productService.FindAsync(id);
            if (product == null)
                return BadRequest(JsonResponse.Error(Constants.Response.Product.ProductNotFound));

            return Ok(JsonResponse.Data(true, product));
        }

        [Authorize(Roles = Constants.Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody]UpdateProductModel updateProductModel)
        {
            if (string.IsNullOrEmpty(id) || updateProductModel == null)
                return BadRequest(JsonResponse.Error(Constants.Response.General.BadRequest));

            if (updateProductModel.Name == null && updateProductModel.Price == null && updateProductModel.Description == null)
                return Ok(JsonResponse.Success(Constants.Response.Product.ProductNoChange));

            Product? product = await _productService.FindAsync(id);
            if (product == null)
                return BadRequest(JsonResponse.Error(Constants.Response.Product.ProductNotFound));

            if (updateProductModel.Name != null) product.Name = updateProductModel.Name;
            if (updateProductModel.Price != null) product.Price = updateProductModel.Price.Value;
            if (updateProductModel.Description != null) product.Description = updateProductModel.Description;

            await _productService.UpdateAsync(product);

            return Ok(JsonResponse.Success(Constants.Response.Product.ProductUpdated));
        }

        [Authorize(Roles = Constants.Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(JsonResponse.Error(Constants.Response.General.BadRequest));

            await _productService.RemoveAsync(id);

            return Ok(JsonResponse.Success(Constants.Response.Product.ProductDeleted));
        }
    }
}