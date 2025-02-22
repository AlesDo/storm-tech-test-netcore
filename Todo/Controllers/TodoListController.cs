﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoLists;
using Todo.Services;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserStore<IdentityUser> userStore;

        public TodoListController(ApplicationDbContext dbContext, IUserStore<IdentityUser> userStore)
        {
            this.dbContext = dbContext;
            this.userStore = userStore;
        }

        public IActionResult Index()
        {
            var userId = User.Id();
            var todoLists = dbContext.RelevantTodoLists(userId);
            var viewmodel = TodoListIndexViewmodelFactory.Create(todoLists);
            return View(viewmodel);
        }

        public IActionResult Detail(int todoListId, bool showDone = true, bool orderByRank = false)
        {
            var todoList = dbContext.SingleTodoList(todoListId);
            var viewmodel = TodoListDetailViewmodelFactory.Create(todoList);
            viewmodel.ShowDone = showDone;
            viewmodel.OrderByRank = orderByRank;
            return View(new TodoListWithAddViewModel(viewmodel));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TodoListFields());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoListFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            var currentUser = await userStore.FindByIdAsync(User.Id(), CancellationToken.None);

            var todoList = new TodoList(currentUser, fields.Title);

            await dbContext.AddAsync(todoList);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Create", "TodoItem", new {todoList.TodoListId});
        }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> CreateItem(TodoListNewItemFields todoListWithAddViewmodel)
      {
         if (!ModelState.IsValid) { return View(todoListWithAddViewmodel); }

         var item = new TodoItem(todoListWithAddViewmodel.TodoListDetailViewmodel.TodoListId, todoListWithAddViewmodel.TodoItemEditFields.ResponsiblePartyId, todoListWithAddViewmodel.TodoItemEditFields.Title, todoListWithAddViewmodel.TodoItemEditFields.Importance, todoListWithAddViewmodel.TodoItemEditFields.Rank);

         await dbContext.AddAsync(item);
         await dbContext.SaveChangesAsync();

         return RedirectToAction("Detail", todoListWithAddViewmodel.TodoListDetailViewmodel);
      }
   }
}