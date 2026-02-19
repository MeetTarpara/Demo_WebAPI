using DemoApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly CompanyDBContext _context;
    private readonly IConfiguration _config;
    private readonly PasswordHasher<Person> _passwordHasher;

    public AuthService(CompanyDBContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
        _passwordHasher = new PasswordHasher<Person>();
    }

    public object Register(Person person)
    {
        var user = _context.Persons
        .FirstOrDefault(x => x.Email == person.Email);

        if (user == null)
        {
            //HASH PASSWORD
            person.Password = _passwordHasher.HashPassword(person, person.Password);

            _context.Persons.Add(person);
            _context.SaveChanges();

            return new { message = "User Registered Successfully" };
        }

        return new { message = "User Already exist with same mail" };
    }


    public object Login(LoginDto model)
    {
        var user = _context.Persons
            .FirstOrDefault(x => x.Email == model.Email);

        if (user == null)
            return null;

        // Verify Password
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

        if (result == PasswordVerificationResult.Failed)
            return null;

        var token = GenerateToken(user);

        return new { token, user.Email };
    }

    public List<Person> GetAllPersons()
    {
        return _context.Persons.ToList();
    }

    public string DeletePerson(int id)
    {
        var person = _context.Persons.Find(id);

        if (person == null)
            return null;

        _context.Persons.Remove(person);
        _context.SaveChanges();

        return "Person deleted successfully";
    }

    private string GenerateToken(Person user)
    {
        //secret key
        var key = _config["Jwt:Key"];

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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
}
