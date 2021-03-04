using MyTasks.Core;
using MyTasks.Core.Repositories;
using MyTasks.Persistence.Repositories;

namespace MyTasks.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        // readonly przy polu oznacza, że jego wartość
        // możemy zmienić tylko w konstruktorze
        private readonly IApplicationDbContext _context;
        
        public UnitOfWork(IApplicationDbContext context, ICategoryRepository category, ITaskRepository task)
        {
            _context = context;
            Task = task;            // new TaskRepository(context);
            Category = category;    // new CategoryRepository(context);
        }

        // obiekty repozytoryjne 
        public ITaskRepository Task { get; }
        public ICategoryRepository Category { get; }

        // na koniec metoda zapisująca zmiany
        public void Complete()
        {
            _context.SaveChanges();
        }
    }


}
