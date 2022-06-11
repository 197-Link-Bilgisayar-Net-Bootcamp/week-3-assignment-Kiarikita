using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Interfaces
{
    public interface IProductFeatureRepository : IGenericRepository<ProductFeature>
    {
        List<ProductFeature> GetAll(int productFeatureId);
    }
}
