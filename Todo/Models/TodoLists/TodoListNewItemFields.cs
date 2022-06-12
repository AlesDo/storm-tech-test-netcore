using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
   public class TodoListNewItemFields
   {
      public TodoItemEditFields TodoItemEditFields { get; } = new TodoItemEditFields();
      public TodoListBaseFields TodoListDetailViewmodel { get; } = new TodoListBaseFields();
   }
}
