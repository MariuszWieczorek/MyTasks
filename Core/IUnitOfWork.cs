using MyTasks.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Core
{
    public interface IUnitOfWork
    {
        TaskRepository Task { get; }
        CategoryRepository Category { get; }
        void Complete();
    }
}
