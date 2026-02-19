
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public IActionResult Register(Person person)
    {
        var result = _authService.Register(person);
        return Ok(result);
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginDto model)
    {
        var result = _authService.Login(model);

        if (result == null)
            return Unauthorized("Invalid Email or Password");

        return Ok(result);
    }


    [Authorize(Policy = "EmailExists")]
    [HttpGet("persons")]
    public IActionResult GetAllPersons()
    {
        return Ok(_authService.GetAllPersons());
    }


    [HttpDelete("persons/{id}")]
    public IActionResult DeletePerson(int id)
    {
        var result = _authService.DeletePerson(id);

        if (result == null)
            return NotFound("Person not found");

        return Ok(result);
    }
}
