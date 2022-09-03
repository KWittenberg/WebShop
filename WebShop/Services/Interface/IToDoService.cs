namespace WebShop.Services.Interface;

public interface IToDoService
{
    // ToDoListViewModel
    Task<List<ToDoListViewModel>> GetToDoList();
    Task<List<ToDoListViewModel>> GetToDoListByApplicationUserId(string applicationUserId);
    Task<ToDoListViewModel?> GetToDoListById(int Id);
    Task<ToDoListViewModel?> CreateToDoList(ToDoListBinding model);
    Task<ToDoListViewModel?> UpdateToDoList(ToDoListUpdateBinding model);
    Task<ToDoListViewModel> DeleteToDoList(ToDoListUpdateBinding model);


    // TaskViewModel
    Task<TaskViewModel?> GetTask(int taskId);
    Task<List<TaskViewModel>> GetTasks(int todoListId);
    Task<TaskViewModel?> CreateTask(TaskBinding model);
    Task<TaskViewModel?> UpdateTask(TaskUpdateBinding model);
    Task<TaskViewModel> DeleteTask(TaskUpdateBinding model);

    Task<TaskViewModel?> ChangeTaskStatus(int taskId, bool status);
}