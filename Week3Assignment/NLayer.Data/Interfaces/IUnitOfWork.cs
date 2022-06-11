using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IProductFeatureRepository ProductFeature{ get; }
        void Complete();
    }
}
