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
        public bool OrderByRank { get; set; }

        public IEnumerable<TodoItemSummaryViewmodel> FilteredItems => this.ApplySort(this.ApplyFilter(this.Items));

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, bool showDone = true, bool orderByRank = false)
        {
            Items = items.OrderBy((item) => item.Importance).ToList();
            TodoListId = todoListId;
            Title = title;
            ShowDone = showDone;
            OrderByRank = orderByRank;
        }

        private IEnumerable<TodoItemSummaryViewmodel> ApplySort(IEnumerable<TodoItemSummaryViewmodel> todoItemSummaryViewmodels)
        {
            return this.OrderByRank ? todoItemSummaryViewmodels.OrderBy((item) => item.Rank) : todoItemSummaryViewmodels;
        }

        private IEnumerable<TodoItemSummaryViewmodel> ApplyFilter(IEnumerable<TodoItemSummaryViewmodel> todoItemSummaryViewmodels)
        {
            return this.ShowDone ? todoItemSummaryViewmodels : todoItemSummaryViewmodels.Where((item) => !item.IsDone);
        }
   }
}