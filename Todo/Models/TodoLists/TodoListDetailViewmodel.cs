using System.Collections.Generic;
using Todo.Models.TodoItems;
using System.Linq;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }
        public bool ShowDone { get; set; }

        public IEnumerable<TodoItemSummaryViewmodel> FilteredItems => this.ShowDone ?  this.Items : this.Items.Where((item) => !item.IsDone);

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, bool showDone = true)
        {
            Items = items.OrderBy((item) => item.Importance).ToList();
            TodoListId = todoListId;
            Title = title;
            ShowDone = showDone;
        }
    }
}