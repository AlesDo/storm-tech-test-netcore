using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
   public class TodoListWithAddViewModel
   {
      public TodoListDetailViewmodel TodoListDetailViewmodel { get; }
      public TodoItemEditFields TodoItemEditFields { get; } = new TodoItemEditFields();

      public TodoListWithAddViewModel(TodoListDetailViewmodel todoListDetailViewmodel)
      {
         TodoListDetailViewmodel = todoListDetailViewmodel;
      }
   }
}
