using MyTasks.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Persistence
{
    public class UnitOfWork
    {
        // readonly przy polu oznacza, że jego wartość
        // możemy zmienić tylko w konstruktorze
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Task = new TaskRepository(_context);
            Category = new CategoryRepository(_context);
        }

        // obiekty repozytoryjne 
        public TaskRepository Task { get; set; }
        public CategoryRepository Category { get; set; }

        // na koniec metoda zapisująca zmiany
        public void Complete()
        {
            _context.SaveChanges();
        }
    }

}
