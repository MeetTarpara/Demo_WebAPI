using Microsoft.AspNetCore.Authorization;


//empty - bcz it is formate that tells .net that there is custome rule called EmailExistsRequirement
public class EmailExistsRequirement : IAuthorizationRequirement
{
}
