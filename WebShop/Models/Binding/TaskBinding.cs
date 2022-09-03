namespace WebShop.Models.Binding;

public class TaskBinding : TaskBase
{
    public int ToDoListId { get; set; }
}

public class TaskUpdateBinding : TaskBinding
{
    public int Id { get; set; }
}