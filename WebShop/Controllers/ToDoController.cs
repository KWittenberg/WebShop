namespace WebShop.Controllers;

[Authorize]
public class ToDoController : Controller
{
    private readonly IToDoService toDoService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;

    public ToDoController(IToDoService toDoService, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        this.toDoService = toDoService;
        this.userManager = userManager;
        this.mapper = mapper;
    }


    public async Task<IActionResult> Index()
    {
        var userId = this.userManager.GetUserId(User);
        var todoLists = await toDoService.GetToDoListByApplicationUserId(userId);
        return View(todoLists);
    }

    /// <summary>
    /// Details
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Details(int id)
    {
        ViewBag.ToDoListId = id;
        CreateTaskViewModel model = new CreateTaskViewModel();
        model.Tasks = await toDoService.GetTasks(id);
        model.ToListId = id;
        return View(model);
    }



    /// <summary>
    /// CreateToDoList
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> CreateToDoList()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateToDoList(ToDoListBinding model)
    {
        var userId = this.userManager.GetUserId(User);
        model.ApplicationUserId = userId;
        var todoLists = await toDoService.CreateToDoList(model);
        TempData["success"] = "ToDo List create successfully";
        return RedirectToAction("Index");
    }

    /// <summary>
    /// EditToDoList
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> EditToDoList(int id)
    {
        var toDoList = await toDoService.GetToDoListById(id);
        var model = mapper.Map<ToDoListUpdateBinding>(toDoList);
        return View(toDoList);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditToDoList(ToDoListUpdateBinding model)
    {
        var userId = this.userManager.GetUserId(User);
        model.ApplicationUserId = userId;
        var todoLists = await toDoService.UpdateToDoList(model);
        TempData["success"] = "ToDo List update successfully";
        return RedirectToAction("Index");
    }

    /// <summary>
    /// Delete ToDoList
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> DeleteToDoList(int id)
    {
        var todoLists = await toDoService.GetToDoListById(id);
        var model = mapper.Map<ToDoListUpdateBinding>(todoLists);
        return View(todoLists);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteToDoList(ToDoListUpdateBinding model)
    {
        await toDoService.DeleteToDoList(model);
        TempData["success"] = "ToDo List deleted successfully";
        return RedirectToAction(nameof(Index));
    }



    /// <summary>
    /// DetailsTask
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> DetailsTask(int id)
    {
        var task = await toDoService.GetTask(id);
        return View(task);
    }

    /// <summary>
    /// CreateTask
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> CreateTask(int id)
    {
        return View(new TaskBinding { ToDoListId = id });
    }
    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskBinding model)
    {
        var task = await toDoService.CreateTask(model);
        TempData["success"] = "Task create successfully";
        return RedirectToAction("Details", new { id = task.ToDoList.Id });
    }

    /// <summary>
    /// EditTask
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> EditTask(int id)
    {
        var task = await toDoService.GetTask(id);
        var model = mapper.Map<TaskUpdateBinding>(task);
        return View(task);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditTask(TaskUpdateBinding model)
    {
        var task = await toDoService.UpdateTask(model);
        TempData["success"] = "Task update successfully";
        return RedirectToAction("Details", new { id = task.ToDoList.Id });
    }

    /// <summary>
    /// DeleteTask
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await toDoService.GetTask(id);
        var model = mapper.Map<TaskUpdateBinding>(task);
        return View(task);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTask(TaskUpdateBinding model)
    {
        var task = await toDoService.DeleteTask(model);
        TempData["success"] = "Task deleted successfully";
        return RedirectToAction("Details", new { id = task.ToDoList.Id });
    }


    /// <summary>
    /// ChangeTaskStatus
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<IActionResult> ChangeTaskStatus(int id, bool status)
    {
        var task = await toDoService.ChangeTaskStatus(id, status);
        return RedirectToAction("Details", new { id = task.ToDoList.Id });
    }
}