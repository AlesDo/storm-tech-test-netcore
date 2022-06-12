namespace Todo.Models.TodoLists
{
   public class TodoListBaseFields
   {
      public int TodoListId { get; set; }
      public bool ShowDone { get; set; }
      public bool OrderByRank { get; set; }
   }
}
