﻿using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        List<Category> GetAll(int categoryId);
    }
}
