namespace WebShop.Controllers;

[Route("api/[controller]")]
public class UserApiController : ControllerBase
{
    private readonly IApplicationUserService userService;
    private readonly SignInManager<ApplicationUser> signInManager;
    public UserApiController(IApplicationUserService userService, SignInManager<ApplicationUser> signInManager)
    {
        this.userService = userService;
        this.signInManager = signInManager;
    }



    /// <summary>
    /// Token
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Route("token")]
    [HttpPost]
    public async Task<IActionResult> Token([FromBody] TokenLoginBinding model)
    {
        if (ModelState.IsValid)
        {
            var token = await userService.GetToken(model);
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(new { Msg = "Invalid UserName or Password!", });
            }
            return Ok(new TokenResponse { Token = token });
        }
        return BadRequest();
    }

    /// <summary>
    /// Register
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserBinding model)
    {
        return Ok(await userService.CreateApiUserAsync(model, Roles.User));
    }
}