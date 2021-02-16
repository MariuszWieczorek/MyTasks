using Microsoft.EntityFrameworkCore;
using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MyTasks.Persistence.Repositories
{
    public class TaskRepository
    {

        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Task> Get(string userId,
            bool isExecuted = false,
            int categoryId = 0,
            string title = null)

        {
            var tasks = _context.Tasks
                .Include(x => x.Category)
                .Where(x => x.UserId == userId && x.IsExecuted == isExecuted);

            if (categoryId != 0)
                tasks = tasks.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                tasks = tasks.Where(x => x.Title.Contains(title));

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
            _context.Add(task);
            _context.SaveChanges();
        }

        public void Update(Task task)
        {
            var taskToUpdate = _context.Tasks.Single(x => x.Id == task.Id);
            taskToUpdate.Title = task.Title;
            taskToUpdate.Description = task.Description;
            taskToUpdate.Term = task.Term;
            taskToUpdate.IsExecuted = task.IsExecuted;
            taskToUpdate.CategoryId = task.CategoryId;

            _context.SaveChanges();
        }

        public void Finish(int id, string userId)
        {
            var taskToUpdate = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
            taskToUpdate.IsExecuted = true;
            _context.SaveChanges();
        }

        public void Delete(int id, string userId)
        {
            var taskToDelete = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
            _context.Remove(taskToDelete);
            _context.SaveChanges();
        }
    }
}
