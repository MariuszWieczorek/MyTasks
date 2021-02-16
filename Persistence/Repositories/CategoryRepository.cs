using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.OrderBy(x => x.Name).ToList();
        }
    }
}
