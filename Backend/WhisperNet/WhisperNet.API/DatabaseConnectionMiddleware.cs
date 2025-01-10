using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class DatabaseConnectionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<DatabaseConnectionMiddleware> _logger;
    private readonly DbContext _dbContext;

    public DatabaseConnectionMiddleware(RequestDelegate next, ILogger<DatabaseConnectionMiddleware> logger, DbContext dbContext)
    {
        _next = next;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Перевірка підключення до бази даних
            if (!await _dbContext.Database.CanConnectAsync())
            {
                _logger.LogError("Cannot connect to the database.");
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Service unavailable. Cannot connect to the database.");
                return;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database connection failed.");
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Service unavailable. Database connection error.");
            return;
        }

        // Якщо все добре, передаємо запит далі
        await _next(context);
    }
}