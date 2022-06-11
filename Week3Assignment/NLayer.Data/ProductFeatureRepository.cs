using NLayer.Data.Interfaces;
using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data
{
    public class ProductFeatureRepository : GenericRepository<ProductFeature>, IProductFeatureRepository
    {
        public ProductFeatureRepository(AppDbContext context) : base(context)
        {
        }

        public ProductFeature Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductFeature> GetAll(int productFeatureId)
        {
            return _context.ProductFeatures.Where(x => x.Id == productFeatureId)
                 .ToList();
        }
    }
}
