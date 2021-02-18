using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Core.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories(string userId);
        void AddCategory(Category category);
        Category GetCategory(int id, string userId);
        void UpdateCategory(Category category);
        void DeleteCategory(int id, string userId);
    }
}
