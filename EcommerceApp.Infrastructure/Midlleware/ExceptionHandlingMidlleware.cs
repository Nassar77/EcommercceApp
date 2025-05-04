using EcommerceApp.Application.Services.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceApp.Infrastructure.Midlleware;
public class ExceptionHandlingMidlleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex)

        {
            var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMidlleware>>();
            context.Response.ContentType = "application/json";
            if (ex.InnerException is SqlException  innerException )
            {
                logger.LogError(innerException,"Sql Exceptioln");
                switch(innerException.Number)
                {
                    case 2627:
                        context.Response.StatusCode=StatusCodes.Status409Conflict; 
                        await context.Response.WriteAsync("Uniqe constrint violation");
                        break;
                    case 515:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("Can not insert null");
                        break;
                    case 547:
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        await context.Response.WriteAsync("Foreign key constrint violation");
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync("An error occured  while processing your request.");
                        break;
                }
            }else
            {
                logger.LogError(ex, "Related EfCore Exceptioln");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An error occured  while saving the entity changes.");
            }
        }
        catch(Exception ex) 
        {
            var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMidlleware>>();
            logger.LogError(ex, "Unknown Exceptioln");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync($"An error occured: {ex.Message}.");
        }
    }

}
