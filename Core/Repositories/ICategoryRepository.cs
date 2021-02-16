using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Core.Repositories
{
    interface ICategoryRepository
    {
        public IEnumerable<Category> GetCategories();
    }
}
