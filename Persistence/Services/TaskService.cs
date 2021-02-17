using MyTasks.Core;
using MyTasks.Core.Models;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MyTasks.Persistence.Services
{
    public class TaskService : ITaskService 
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /*
         * public IEnumerable<Task> Get(string userId,
            bool isExecuted = false,
            int categoryId = 0,
            string title = null)
        */

        public IEnumerable<Task> Get(string userId,FilterTasks filterTasks)
        {
            return _unitOfWork.Task.Get(userId, filterTasks);
        }

        public Task Get(int id, string userId)
        {
            return _unitOfWork.Task.Get(id, userId);
        }

        public void Add(Task task)
        {
            _unitOfWork.Task.Add(task);
            _unitOfWork.Complete();
        }

        public void Update(Task task)
        {
            _unitOfWork.Task.Update(task);
            _unitOfWork.Complete();
        }

        public void Finish(int id, string userId)
        {
            _unitOfWork.Task.Finish(id, userId);
            // metoda wysyłająca maila
            // ...
            // dodatkowa modyfikacja danych
            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.Task.Delete(id, userId);
            _unitOfWork.Complete();
        }


    }
}
