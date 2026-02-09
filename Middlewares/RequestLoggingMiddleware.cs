public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;


    //for calling next middleware 
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      
        Console.WriteLine($"-------------> Request Path: {context.Request.Path}");

        await _next(context); // Call next middleware

        
        Console.WriteLine($"-------------> Response Status: {context.Response.StatusCode}");
    }
}


// HttpContext contains:
// Request (headers, body, method, path)
// Response (status code, headers)
// User (claims, identity)
// Items (shared data)
// Services