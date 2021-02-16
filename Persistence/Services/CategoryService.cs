using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Persistence.Services
{
    public class CategoryService
    {
        private readonly UnitOfWork _unitOfWork;
        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Category.GetCategories();
        }

    }
}
