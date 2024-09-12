namespace Tidjma.Extentions;
using Tidjma.Endpoints;
using Tidjma.Data;
using Tidjma.Helpers;
using Tidjma.Repository;
using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;

public static class EndpointExtentions
{
    public static IServiceCollection RegisterDbContext(
            this IServiceCollection services
            )
    {
        services.AddDbContext<TidjmaDbContext>();
        services.AddScoped<DbContext, TidjmaDbContext>();

        return services;
    }

    public static IServiceCollection RegisterValidators(
            this IServiceCollection services
            )
    {
        services.AddScoped<ArticleValidator>();
        services.AddScoped<UserValidator>();
        services.AddScoped<CommentValidator>();
        services.AddScoped<UpdateArticleValidator>();
        services.AddScoped<UpdateUserValidator>();
        services.AddScoped<UpdateCommentValidator>();

        return services;
    }

    public static IServiceCollection RegisterLogger(
            this IServiceCollection services
            )
    {
        services.AddLogging(builder => {
                builder.AddConsole();
                });

        return services;
    }

    public static IServiceCollection RegisterRepositories(
                this IServiceCollection services
            )
    { 
        // later when there is too much repos
        //services.AddScoped<Query>(); 
        
        services.AddScoped<ArticleRepository>();
        services.AddScoped<BlogMapper>();
        services.AddScoped<CommentRepository>();
        services.AddScoped<UserRepository>();

        return services;
    }

    public static IServiceCollection RegisterEndpoints(
                this IServiceCollection services
            )
    {

        services.AddScoped<ITJEndpoint, ArticleEndpoint>();
        services.AddScoped<ITJEndpoint, UserEndpoints>();
        //services.AddScoped<ITJEndpoint, CommentEndpoints>();

        return services;
    }

    // to be reconsidered later
    public static IServiceCollection AddScopedLocalServices<T>(
            this IServiceCollection services,
            Assembly assembly)
    {
        /*
            A list of service descriptors of Types that implement T with a Scoped lifetime
        */
        ServiceDescriptor[] servicesDescriptors = assembly
            .DefinedTypes
            .Where(type => type is {IsAbstract : false, IsInterface : false} &&
                    type.IsAssignableTo(typeof(T)))
            .Select(type => ServiceDescriptor.Scoped(typeof(T), type))
            .ToArray();
            
        // Trying to add those service descriptors
        services.TryAddEnumerable(servicesDescriptors);

        return services;
    }

    public static IApplicationBuilder MapLocalEndpoints( // inject app to services that impl ITJEndpoint
            this WebApplication app
            )
    {
        using IServiceScope? scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IEnumerable<ITJEndpoint>>();

        foreach (var endpoint in context)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
