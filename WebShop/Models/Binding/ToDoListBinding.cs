namespace WebShop.Models.Binding;

public class ToDoListBinding : ToDoListBase
{
    public string ApplicationUserId { get; set; }
}

public class ToDoListUpdateBinding : ToDoListBinding
{
    public int Id { get; set; }
}