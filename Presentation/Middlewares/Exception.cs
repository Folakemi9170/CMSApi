using System.Net;                               
using System.Text.Json;                          
using Microsoft.AspNetCore.Mvc;                   
using Microsoft.EntityFrameworkCore;                         

namespace CMSApi.Presentation.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;      
            _logger = logger;  
            _env = env;        
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception for request {Method} {Path}", context.Request.Method, context.Request.Path);

                await WriteProblemDetailsAsync(context, ex);
            }
        }


        private async Task WriteProblemDetailsAsync(HttpContext context, Exception exception)
        {

            var (status, title) = exception switch
            {
                
                ArgumentException => (HttpStatusCode.BadRequest, "Invalid request"),
                KeyNotFoundException => (HttpStatusCode.NotFound, "Resource not found"),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized"),
                DbUpdateException => (HttpStatusCode.BadRequest, "Database update failed"),
                NotImplementedException => (HttpStatusCode.NotImplemented, "Not implemented"),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
            };

            var problem = new ProblemDetails
            {
                Status = (int)status,          
                Title = title,                 
                Type = $"https://httpstatuses.io/{(int)status}",
                Instance = context.TraceIdentifier
            };

           
            if (_env.IsDevelopment())
            {
                problem.Extensions["detail"] = exception.Message;              
                problem.Extensions["stackTrace"] = exception.StackTrace;       
                if (exception is DbUpdateException dbEx && dbEx.InnerException is not null)
                { 
                    problem.Extensions["inner"] = dbEx.InnerException.Message;
                }
            }

            
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problem.Status ?? (int)HttpStatusCode.InternalServerError;

            var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });

            await context.Response.WriteAsync(json);
        }
    }
}
