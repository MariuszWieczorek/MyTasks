using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Core.Services
{
    public interface ITaskService
    {
        IEnumerable<Task> Get(string userId,
            bool isExecuted = false,
            int categoryId = 0,
            string title = null);

        Task Get(int id, string userId);
        void Add(Task task);
        void Update(Task task);
        void Finish(int id, string userId);
        void Delete(int id, string userId);

    }
}
