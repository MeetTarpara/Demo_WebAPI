using DemoApi.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public class EmailExistsHandler : AuthorizationHandler<EmailExistsRequirement>
{
    private readonly CompanyDBContext _context;

    public EmailExistsHandler(CompanyDBContext context)
    {
        _context = context;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        EmailExistsRequirement requirement)
    {
        // Get email from token claims
        var email = context.User.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return Task.CompletedTask;
        }

        // Check in database
        var userExists = _context.Persons.Any(x => x.Email == email);
        Console.WriteLine(email);
        if (userExists)
        {
            context.Succeed(requirement);
        }

        // Access automatically becomes 403 Forbidden
        return Task.CompletedTask;
    }
}
