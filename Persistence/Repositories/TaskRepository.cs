using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MyTasks.Persistence.Repositories
{
    public class TaskRepository
    {
        public IEnumerable<Task> Get(string userId,
            bool isExecuted = false,
            int categoryId = 0,
            string title = null)

        {
            throw new NotImplementedException();
        }

        internal Task Get(int id, string userId)
        {
            throw new NotImplementedException();
        }

        internal void Add(Task task)
        {
            throw new NotImplementedException();
        }

        internal void Update(Task task)
        {
            throw new NotImplementedException();
        }

        internal void Finish(int id, string userId)
        {
            throw new NotImplementedException();
        }

        internal void Delete(int id, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
