namespace Tidjma.Endpoints;
using Tidjma.Contracts;
using Tidjma.Repository;
using Tidjma.Helpers;
using Microsoft.AspNetCore.Mvc;

public class UserEndpoints : ITJEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder? users = // later to be added in ServiceConfigurations
            app.MapGroup("users");

        users.MapGet("/",
                ([FromServices] UserRepository repository) =>
                {
                    return Results.Ok(repository.List());
                });

        users.MapGet("/{id}",
                ([FromRoute] int id,
                 [FromServices] UserRepository repository) =>
                {
                    var user = repository.Get(id);

                    return user is null ? Results.NotFound($"could not find user with id {id}") : Results.Ok(user);
                });

        users.MapPut("/{id}",
                ([FromRoute] int id,
                 [FromBody] UpdateUserDTO updatedUser,
                 [FromServices] UserRepository repository,
                 [FromServices] UpdateUserValidator validator) =>
                {
                    if (id != updatedUser.Id) {return Results.BadRequest("id's are not identical");}
                    var validation = validator.Validate(updatedUser);
                    if (validation.IsValid)
                    {
                        return repository.Update(updatedUser, id) ? Results.Created() : Results.BadRequest("something went wrong");
                    }

                    return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
                });

        users.MapPost("/",
                ([FromBody] CreateUserDTO newUser,
                 [FromServices] UserRepository repository,
                 [FromServices] UserValidator validator) =>
                {
                    var validation = validator.Validate(newUser);
                    if (validation.IsValid)
                    {
                        return repository.Create(newUser) ? Results.Created() : Results.BadRequest("something went wrong");
                    }

                    return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
                });

        users.MapDelete("/{id}",
                ([FromRoute] int id,
                 [FromServices] UserRepository repository) =>
                {
                    return repository.Delete(id) ? Results.Ok("asset deleted") : Results.NotFound($"could not find user with id {id}");
                });
    }
}
