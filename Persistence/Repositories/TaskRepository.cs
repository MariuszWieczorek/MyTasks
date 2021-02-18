using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Models;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MyTasks.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly IApplicationDbContext _context;
        public TaskRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        
        /*
        public IEnumerable<Task> Get(string userId,
            bool isExecuted = false,
            int categoryId = 0,
            string title = null)
        */

        public  IEnumerable<Task> Get(string userId, FilterTasks filterTasks)
        {
            var tasks = _context.Tasks
                .Include(x => x.Category)
                .Where(x => x.UserId == userId && x.IsExecuted == filterTasks.IsExecuted);

            if (filterTasks.CategoryId != 0)
                tasks = tasks.Where(x => x.CategoryId == filterTasks.CategoryId);

            if (!string.IsNullOrWhiteSpace(filterTasks.Title))
                tasks = tasks.Where(x => x.Title.Contains(filterTasks.Title));

            return tasks.OrderBy(x => x.Term).ToList();
        }

        // jak użyjemy Single() to gdy zostanie zwrócona inna liczba rekordów niż 1 
        // to zostanie rzucony wyjątek
        // Jak użyjemy First lub FirstOrDefault, to jeżeli nie było by rekordu to zostanie zwrócony null
        public Task Get(int id, string userId)
        {
            var task = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
            return task;
        }

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
        }

        public void Update(Task task)
        {
            var taskToUpdate = _context.Tasks.Single(x => x.Id == task.Id);
            taskToUpdate.Title = task.Title;
            taskToUpdate.Description = task.Description;
            taskToUpdate.Term = task.Term;
            taskToUpdate.IsExecuted = task.IsExecuted;
            taskToUpdate.CategoryId = task.CategoryId;
         }

        public void Finish(int id, string userId)
        {
            var taskToUpdate = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
            taskToUpdate.IsExecuted = true;
        }

        public void Delete(int id, string userId)
        {
            var taskToDelete = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
            _context.Tasks.Remove(taskToDelete);
        }
    }
}
