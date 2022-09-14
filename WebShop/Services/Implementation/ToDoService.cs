namespace WebShop.Services.Implementation;

public class ToDoService : IToDoService
{
    private readonly ApplicationDbContext db;
    private readonly IMapper mapper;

    public ToDoService(ApplicationDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }


    /// <summary>
    /// GetToDoList
    /// </summary>
    /// <returns></returns>
    public async Task<List<ToDoListViewModel>> GetToDoList()
    {
        var todoList = await db.ToDoList.ToListAsync();
        return todoList.Select(x => mapper.Map<ToDoListViewModel>(x)).ToList();
    }

    /// <summary>
    /// GetToDoListByApplicationUserId
    /// </summary>
    /// <param name="applicationUserId"></param>
    /// <returns></returns>
    public async Task<List<ToDoListViewModel>> GetToDoListByApplicationUserId(string applicationUserId)
    {
        var todoList = await db.ToDoList.Include(x => x.ApplicationUser).Where(x => x.ApplicationUser.Id == applicationUserId).ToListAsync();
        return todoList.Select(x => mapper.Map<ToDoListViewModel>(x)).ToList();
    }

    /// <summary>
    /// GetToDoListById
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<ToDoListViewModel?> GetToDoListById(int Id)
    {
        var todoList = await db.ToDoList.FirstOrDefaultAsync(x => x.Id == Id);
        if (todoList == null) { return null; }
        return mapper.Map<ToDoListViewModel>(todoList);
    }


    /// <summary>
    /// CreateToDoList
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ToDoListViewModel?> CreateToDoList(ToDoListBinding model)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId);
        if (user == null) { return null; }
        var dbo = mapper.Map<ToDoList>(model);
        dbo.ApplicationUser = user;
        db.ToDoList.Add(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<ToDoListViewModel>(dbo);
    }

    /// <summary>
    /// UpdateToDoList
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ToDoListViewModel?> UpdateToDoList(ToDoListUpdateBinding model)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId);
        if (user == null) { return null; }
        var dbo = await db.ToDoList.FindAsync(model.Id);
        mapper.Map(model, dbo);
        dbo.ApplicationUser = user;
        db.ToDoList.Update(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<ToDoListViewModel>(dbo);
    }

    /// <summary>
    /// Delete ToDoList
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ToDoListViewModel> DeleteToDoList(ToDoListUpdateBinding model)
    {
        var user = await db.ApplicationUser.FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId);
        var dbo = await db.ToDoList.FindAsync(model.Id);
        mapper.Map(model, dbo);
        dbo.ApplicationUser = user;
        db.ToDoList.Remove(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<ToDoListViewModel>(dbo);
    }



    /// <summary>
    /// GetTasks
    /// </summary>
    /// <param name="todoListId"></param>
    /// <returns></returns>
    public async Task<List<TaskViewModel>> GetTasks(int todoListId)
    {
        var task = await db.Task
            .Include(x => x.ToDoList).Where(x => x.ToDoList.Id == todoListId /*&& !x.Status*/)
            .OrderByDescending(x=>x.Created)
            .ToListAsync();
        if (task == null) { return new List<TaskViewModel>(); }
        return task.Select(x => mapper.Map<TaskViewModel>(x)).ToList();
    }

    /// <summary>
    /// GetTask
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    public async Task<TaskViewModel?> GetTask(int taskId)
    {
        var task = await db.Task.Include(t => t.ToDoList).FirstOrDefaultAsync(x => x.Id == taskId);
        if (task == null) { return null; }
        return mapper.Map<TaskViewModel>(task);
    }

    /// <summary>
    /// CreateTask
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<TaskViewModel?> CreateTask(TaskBinding model)
    {
        var toDoList = await db.ToDoList.FindAsync(model.ToDoListId);
        if (toDoList == null) { return null; }
        var dbo = mapper.Map<Models.Dbo.Task>(model);
        dbo.ToDoList = toDoList;
        db.Task.Add(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<TaskViewModel>(dbo);
    }

    /// <summary>
    /// UpdateTask
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<TaskViewModel?> UpdateTask(TaskUpdateBinding model)
    {
        var toDoList = await db.ToDoList.FindAsync(model.ToDoListId);
        if (toDoList == null) { return null; }
        var dbo = mapper.Map<Models.Dbo.Task>(model);
        mapper.Map(model, dbo);
        dbo.ToDoList = toDoList;
        db.Task.Update(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<TaskViewModel>(dbo);
    }

    /// <summary>
    /// DeleteTask
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<TaskViewModel> DeleteTask(TaskUpdateBinding model)
    {
        var toDoList = await db.ToDoList.FindAsync(model.ToDoListId);
        if (toDoList == null) { return null; }
        var dbo = await db.Task.FindAsync(model.Id);
        mapper.Map(model, dbo);
        dbo.ToDoList = toDoList;
        db.Task.Remove(dbo);
        await db.SaveChangesAsync();
        return mapper.Map<TaskViewModel>(dbo);
    }

    /// <summary>
    /// ChangeTaskStatus
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<TaskViewModel?> ChangeTaskStatus(int taskId, bool status)
    {
        var task = await db.Task.Include(x => x.ToDoList).FirstOrDefaultAsync(x => x.Id == taskId);
        if (task == null) { return null; }
        task.Status = status;
        await db.SaveChangesAsync();
        return mapper.Map<TaskViewModel>(task);
    }
}