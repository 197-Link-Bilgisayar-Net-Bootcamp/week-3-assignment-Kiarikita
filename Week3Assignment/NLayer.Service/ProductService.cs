using Microsoft.EntityFrameworkCore;
using NLayer.Data;
using NLayer.Data.Models;
using NLayer.Service.Dtos;
using NLayer.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service
{
    public class ProductService
    {
        /* Kullanmış olduğumuz mimari yaklaşım, service katmanı/ business logic
         * herhangi bir orm aracından haberi olmasın derse bu bölümü interface e çevirmemiz lazım
         */
        private readonly AppDbContext _context;

        private readonly GenericRepository<Product> _productRepository;
        private readonly GenericRepository<Category> _categoryRepository;
        private readonly GenericRepository<ProductFeature> _productFeatureRepository;

        private readonly UnitOfWork _unitOfWork;
        public ProductService(AppDbContext context, GenericRepository<Product> productRepository, GenericRepository<Category> categoryRepository, GenericRepository<ProductFeature> productFeatureRepository, UnitOfWork unitOfWork)
        {
            _context = context;
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
            this._productFeatureRepository = productFeatureRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Response<List<ProductDto>>> GetAll()
        {
            //eğer await kullanmasaydı bu yapılan işlem sırasında başka işlemlerde de yapılabilir
            var products = await _context.Products.ToListAsync();
            var productDtos = products.Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId
            }).ToList();

            if (!productDtos.Any())
            {
                return new Response<List<ProductDto>>()
                {
                    Data = null,
                    Errors = new List<string>() { "Ürün mevcut değildir." },
                    Status = 404
                };
            }

            return new Response<List<ProductDto>>()
            {
                Data = productDtos,
                Errors = null,
                Status = 200
            };
        }


        public async Task<Response<string>> CreateAll(Category category, Product product, ProductFeature productFeature)
        {
            await _productRepository.Add(product);
            await _categoryRepository.Add(category);
            await _productFeatureRepository.Add(productFeature);
            await _unitOfWork.Commit();

            return new Response<string>();
        }

        public async Task<Response<List<ProductFullModel>>> GetProductFullModel()
        {
            var fullProductList = "sp_full_products";
            var list = await _context.ProductFullModels.FromSqlInterpolated($"exec {fullProductList}").ToListAsync();

            return new Response<List<ProductFullModel>>()
            {
                Data = list,
                Status = 200
            };
        }

        public async Task<Response<List<ProductFullModel>>> GetProductFullModelByLinq()
        {
            var list = await _context.Products.Include(x=>x.Category).Include(x=>x.ProductFeature).Select(x => new ProductFullModel()
            {
                Name = x.Name,
                CategoryName = x.Category.Name,  
                Height = x.ProductFeature.Height,
                Width = x.ProductFeature.Width,
                Price = x.Price
            }).ToListAsync();

            var result = _context.Categories.Join(_context.Products, x => x.Id, y => y.CategoryId, (c, p) => new
            {
                CategoryName = c.Name,
                ProductName = p.Name,   
                ProductPrice = p.Price
            }).ToListAsync();

            var result2 = await (from c in _context.Categories
                                 join p in _context.Products on c.Id equals p.CategoryId
                                 join pf in _context.ProductFeatures on p.Id equals pf.Id
                                 select new
                                 {
                                     CategoryName = c.Name,
                                     ProductName = p.Name,
                                     ProductPrice = p.Price,
                                     Width = pf.Width
                                 }).ToListAsync();

            return new Response<List<ProductFullModel>>()
            {
                Data = list,
                Status = 200
            };
        }


        //public async Task<Response<string>> CreateAll2(Category category, Product product, ProductFeature productFeature)
        //{
        //    product.ProductFeature = productFeature;
        //    category.Products.Add(product);
        //    _context.Categories.Add(category);
        //    await _context.SaveChangesAsync();

        //    return new Response<string>();  
        //}
    }
}
