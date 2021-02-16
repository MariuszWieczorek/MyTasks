using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Core.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
