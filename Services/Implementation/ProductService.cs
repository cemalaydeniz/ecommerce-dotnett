﻿using ecommerce_dotnet.Data;
using ecommerce_dotnet.Models;
using ecommerce_dotnet.Services.Exceptions;
using ecommerce_dotnet.Services.Interfaces;
using ecommerce_dotnet.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ecommerce_dotnet.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;

        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product?> FindAsync(string id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<Product>> FindAllAsync(Expression<Func<Product, bool>> query)
        {
            return await _dbContext.Products.Where(query).ToListAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task RemoveAsync(string id)
        {
            Product? product = await FindAsync(id);
            if (product == null)
                throw new ProductNotFoundException(Constants.Exception.Product.NotFound);

            await RemoveAsync(product);
        }

        public async Task RemoveAsync(Product product)
        {
            product.IsDeleted = true;
            await UpdateAsync(product);
        }
    }
}