using NLayer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IProductFeatureRepository ProductFeature { get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Products = new ProductRepository(context);
            Categories = new CategoryRepository(context);
            ProductFeature = new ProductFeatureRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
