using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence;
using MyTasks.Persistence.Extensions;
using MyTasks.Persistence.Repositories;
using MyTasks.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MyTasks.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {

        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;

        public TaskController(ITaskService task, ICategoryService category)
        {
            _taskService = task;
            _categoryService = category;
        }

        #region Tasks ----------------------------------------------------------------
        public IActionResult Tasks()
        {
            var userId = User.GetUserId();

            var vm = new TasksViewModel()
            {
                FilterTasks = new FilterTasks(),
                Tasks = _taskService.Get(userId, new FilterTasks()),
                Categories = _categoryService.GetCategories(userId)
            };

            return View(vm);
        }
        
        // akcja wywoływana w widoku Tasks po kliknięciu na submit służącym do filtrowania zadań
        [HttpPost]
        public IActionResult Tasks(TasksViewModel viewModel)
        {
            var userId = User.GetUserId();
            var tasks = _taskService.Get(userId, viewModel.FilterTasks);
    
            return PartialView("_TasksTablePartial",tasks);
        }
        #endregion

        #region Task: edycja/dodawanie/usuwanie zadania -----------------------

        // Wejście na ekran dodawania/edycji zadania z przycisku dodaj nowe zadanie
        // lub po kliknięciu na link na liście zadań służący do edycji zadania
        
        [HttpGet]
        public IActionResult Task(int id = 0)
        {
            var userId = User.GetUserId();

            var task = id == 0 ?
                new Task { Id = 0, UserId = userId, Term = DateTime.Now } :
                _taskService.Get(id, userId);

            var vm = new TaskViewModel()
            {
                Task = task,
                Categories = _categoryService.GetCategories(userId),
                Heading = id == 0 ?
                 "nowe zadanie" :
                 "edycja zadania"
            };

            return View(vm);
        }

        // Wciśnięcie przycisku typu submit na ekranie edycji/dodania zadania
        // Po którym nastąpi przekazanie zawartości pól na formularzu 
        // i wywołanie metod w repozytorium dodania lub aktualizacji zadania

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Task(Task task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new TaskViewModel()
                {
                    Task = task,
                    Categories = _categoryService.GetCategories(userId),
                    Heading = task.Id == 0 ?
                     "nowe zadanie" :
                     "edycja zadania"
                };
                
                return View("Task", vm);
            }

            if (task.Id == 0)
                _taskService.Add(task);
            else
                _taskService.Update(task);


            return RedirectToAction("Tasks", "Task");
        }

        // po kliknięciu przycisku deleteTask
        // zostanie wywołany ajax
        // który wywoła akcję Delete typu Post w kontrolerze Task
        // i to jest ta akcja

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Delete(id, userId);
            }
            catch (Exception ex)
            {
                // logowanie do pliku
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }

        // po kliknięciu przycisku finishTask
        // zostanie wywołany ajax
        // który wywoła akcję Delete typu Post w kontrolerze Task
        // i to jest ta akcja
        
        [HttpPost]
        public IActionResult Finish(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Finish(id, userId);
            }
            catch (Exception ex)
            {
                // logowanie do pliku
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }

        #endregion

        #region Category: przeglądanie, edycja/dodawanie/usuwanie kategorii ------------------

        public IActionResult Categories()
        {
            var userId = User.GetUserId();
            var categories = _categoryService.GetCategories(userId);

            return View(categories);
        }
        
         public IActionResult Category(int id)
        {
            var userId = User.GetUserId();
            var category = id == 0 ?
                new Category { Id = 0, UserId = userId, Name = string.Empty } :
                _categoryService.GetCategory(id, userId);

            var vm = new CategoryViewModel()
            {
                Category = category,
                Heading = ""
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Category(Category category)
        {
            var userId = User.GetUserId();
            category.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new CategoryViewModel()
                {
                    Category = category,
                    Heading = category.Id == 0 ?
                     "nowa kategoria" :
                     "edycja kategorii"
                };

                return View("Category", vm);
            }

            if (category.Id == 0)
                _categoryService.AddCategory(category);
            else
                _categoryService.UpdateCategory(category);


            return RedirectToAction("Categories", "Task");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _categoryService.DeleteCategory(id, userId);
            }
            catch (Exception ex)
            {
                // logowanie do pliku
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }
        #endregion

    }
}
