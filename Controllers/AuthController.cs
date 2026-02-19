using DemoApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly CompanyDBContext _context;
    private readonly IConfiguration _config;

    public AuthController(CompanyDBContext context, IConfiguration config)
    {
        _context = context;
        _config=config;
    }


    [HttpPost("register")]
    public IActionResult Register(Person person)
    {
         var user = _context.Persons
            .FirstOrDefault(x => x.Email == person.Email);
        if(user==null){
            _context.Persons.Add(person);
            _context.SaveChanges();

            return Ok(new { message = "User Registered Successfully" });
        }
         return Ok(new { message = "User Already exist with same mail" });
        
       
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto model)
    {
        if (model == null)
            return BadRequest("Invalid request");

        var user = _context.Persons
            .FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

        if (user == null)
            return Unauthorized("Invalid Email or Password");

        var token = GenerateToken(user);

        return Ok(new { token,user.Email });
    }


    private string GenerateToken(Person user)
    {
        // secret key
        var key = _config["Jwt:Key"];


        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        // secret key -> security key
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "MyWebApi",          
            audience: "MyAngularApp",    
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        // JWT object → Converted into text string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    [Authorize]
    [HttpGet("persons")]
    public IActionResult GetAllPersons()
    {
        var persons = _context.Persons.ToList();
        return Ok(persons);
    }


}
