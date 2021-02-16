﻿using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Core.Repositories
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetCategories();
    }
}
