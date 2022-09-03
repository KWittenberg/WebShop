namespace WebShop.Models.Dbo;

public class Task : TaskBase, IEntity
{
    [Key]
    public int Id { get; set; }
    public ToDoList ToDoList { get; set; }
    public DateTime Created { get; set; }
}