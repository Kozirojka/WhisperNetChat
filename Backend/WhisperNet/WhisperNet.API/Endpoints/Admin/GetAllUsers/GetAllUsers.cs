using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using WhisperNet.API.Interfaces;
using WhisperNet.Application.Chat.Admin.GetAllUsers;

namespace WhisperNet.API.Endpoints.Admin.GetAllUsers;

public class GetAllUsers : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/admin/users", HandleGetAllUsers).RequireAuthorization("Admin").WithTags("Admin");
    }

    private static async Task<IResult> HandleGetAllUsers(IMediator _mediator)
    {

        var GetAllUser = new GetUsersQuery();
        var result = await _mediator.Send(GetAllUser);

        if (result == null)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(result);
    }
}