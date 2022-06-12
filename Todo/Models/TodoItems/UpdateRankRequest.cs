using System.ComponentModel.DataAnnotations;

namespace Todo.Models.TodoItems
{
   public class UpdateRankRequest
   {
      public int TodoItemId { get; set; }
      [Range(0, 100)]
      public int Rank { get; set; }
   }
}
