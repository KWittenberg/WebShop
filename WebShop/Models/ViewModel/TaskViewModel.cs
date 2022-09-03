namespace WebShop.Models.ViewModel;

public class TaskViewModel : TaskBase
{
    public int Id { get; set; }
    public ToDoListViewModel ToDoList { get; set; }
    public int ToDoListId { get; set; }
    public DateTime Created { get; set; }
}

public class CreateTaskViewModel
{
    public List<TaskViewModel> Tasks { get; set; }
    public int ToListId { get; set; }
}