using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.Models.TodoItems;
using Todo.Models.TodoLists;
using Xunit;

namespace Todo.Tests
{
   public class TodoListDetailViewModelTests
   {
      [Fact]
      public void TodoListDetailsViewModel_SortsItemsByImportance()
      {
         TodoList todoList = new TestTodoListBuilder(new IdentityUser("alice@example.com"), "shopping")
                    .WithItem("bread", Importance.High)
                    .WithItem("water", Importance.Medium)
                    .WithItem("cake", Importance.Low)
                    .WithItem("pasta", Importance.High)
                    .WithItem("fruits", Importance.Medium)
                    .WithItem("sweets", Importance.Low)
                    .Build();

         TodoListDetailViewmodel todoListDetailViewmodel = new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, todoList.Items.Select((todoItem) => new TodoItemSummaryViewmodel(todoItem.TodoItemId, todoItem.Title, todoItem.IsDone, new UserSummaryViewmodel("alice", "alice@example.com"), todoItem.Importance)).ToList());

         todoListDetailViewmodel.Items.Select((item) => item.Importance).Should().BeInAscendingOrder();
      }

      [Fact]
      public void TodoListDetailsViewModel_FilteredItemsRemovesDoneWhenShowDoneIsFalse()
      {
         TodoList todoList = new TestTodoListBuilder(new IdentityUser("alice@example.com"), "shopping")
                    .WithItem("bread", Importance.High)
                    .WithItem("water", Importance.Medium)
                    .WithItem("cake", Importance.Low)
                    .WithItem("pasta", Importance.High)
                    .WithItem("fruits", Importance.Medium)
                    .WithItem("sweets", Importance.Low)
                    .Build();

         TodoListDetailViewmodel todoListDetailViewmodel = new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, todoList.Items.Select((todoItem, index) => new TodoItemSummaryViewmodel(todoItem.TodoItemId, todoItem.Title, index % 2 == 0, new UserSummaryViewmodel("alice", "alice@example.com"), todoItem.Importance)).ToList(), false);

         todoListDetailViewmodel.FilteredItems.Should().HaveCount(3);
      }
   }
}
