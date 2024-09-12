using Tidjma.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer() // Adding Explorer
    .RegisterValidators() // Adding Validators
    .AddSwaggerGen() // Adding Swagger
    .RegisterLogger() // Adding a Console logger
    .RegisterDbContext() // Adding Postgres DbContext
    .RegisterRepositories() // Services to interact with Database
    .RegisterEndpoints(); // Adding Endpoints

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapLocalEndpoints();

app.Run();
