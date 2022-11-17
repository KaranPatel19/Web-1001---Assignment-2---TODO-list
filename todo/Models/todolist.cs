using System.ComponentModel.DataAnnotations;
namespace todo.Models;

public class todolist
{
    public int Id { get; set; }
        [Required]
        public string content { get; set; }



    
}
    