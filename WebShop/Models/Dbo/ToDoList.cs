namespace WebShop.Models.Dbo;

public class ToDoList : ToDoListBase, IEntity
{
    [Key]
    public int Id { get; set; }
    public ICollection<Task> Task { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}