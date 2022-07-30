namespace WebShop.Controllers;

[Authorize(Roles = Roles.Admin)]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        this.roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var roles = this.roleManager.Roles.ToList();
        return View(roles);
    }
    
    /// <summary>
    /// Details
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Details(string? id)
    {
        var role = await this.roleManager.FindByIdAsync(id);
        if (role == null) { return NotFound(); }
        return View(role);
    }


    /// <summary>
    /// Upsert
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Upsert(string id)
    {
        if (String.IsNullOrEmpty(id))
        {
            return View();
        }
        else
        {
            //update
            var objFromDb = this.roleManager.Roles.FirstOrDefault(u => u.Id == id);
            return View(objFromDb);
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(IdentityRole roleObj)
    {
        if (await this.roleManager.RoleExistsAsync(roleObj.Name))
        {
            //error
            TempData["error"] = "Role already exists.";
            return RedirectToAction(nameof(Index));
        }
        if (string.IsNullOrEmpty(roleObj.Id))
        {
            //create
            await this.roleManager.CreateAsync(new IdentityRole() { Name = roleObj.Name });
            TempData["success"] = "Role created successfully";
        }
        else
        {
            //update
            var objRoleFromDb = this.roleManager.Roles.FirstOrDefault(u => u.Id == roleObj.Id);
            if (objRoleFromDb == null)
            {
                TempData["error"] = "Role not found.";
                return RedirectToAction(nameof(Index));
            }
            objRoleFromDb.Name = roleObj.Name;
            objRoleFromDb.NormalizedName = roleObj.Name.ToUpper();
            var result = await this.roleManager.UpdateAsync(objRoleFromDb);
            TempData["success"] = "Role updated successfully";
        }
        return RedirectToAction(nameof(Index));

    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Delete(string id)
    {
        if (String.IsNullOrEmpty(id))
        {
            return View();
        }
        else
        {
            //update
            var objFromDb = this.roleManager.Roles.FirstOrDefault(u => u.Id == id);
            return View(objFromDb);
        }
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var objFromDb = this.roleManager.Roles.FirstOrDefault(u => u.Id == id);
        if (objFromDb == null)
        {
            TempData["error"] = "Role not found.";
            return RedirectToAction(nameof(Index));
        }
        var userRolesForThisRole = this.roleManager.Roles.Where(u => u.Id == id).Count();
        //if (userRolesForThisRole > 0)
        //{
        //    TempData["error"] = "Cannot delete this role, since there are users assigned to this role.";
        //    return RedirectToAction(nameof(Index));
        //}
        await this.roleManager.DeleteAsync(objFromDb);
        TempData["success"] = "Role deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}