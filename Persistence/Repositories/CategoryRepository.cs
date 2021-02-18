using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IApplicationDbContext _context;
        public CategoryRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public Category GetCategory(int id, string userId)
        {
            var category = _context.Categories
                .Single(x => x.Id == id && ( x.UserId == userId || String.IsNullOrEmpty(x.UserId) ));
            return category;
        }

        public void DeleteCategory(int id, string userId)
        {
            var categoryToDelete = _context.Categories.Single(x => x.Id == id && x.UserId == userId);
            _context.Categories.Remove(categoryToDelete);
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _context.Categories
                .Where(x=>x.UserId==userId || string.IsNullOrEmpty(x.UserId))
                .OrderBy(x => x.Name).ToList();
        }

        public void UpdateCategory(Category category)
        {
            var categoryToUpdate = _context.Categories.Single(x => x.Id == category.Id);
            categoryToUpdate.Name = category.Name; 
        }
    }
}
