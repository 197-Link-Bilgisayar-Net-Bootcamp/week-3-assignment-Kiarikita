using Microsoft.EntityFrameworkCore;
using NLayer.Data;
using NLayer.Data.Models;
using NLayer.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            //eğer await kullanmasaydı bu yapılan işlem sırasında başka işlemlerde de yapılabilir
            var products = await _context.Products.ToListAsync();
            return products.Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId
            }).ToList();
        }
    }
}
