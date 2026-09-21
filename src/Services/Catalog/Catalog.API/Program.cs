var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR(configuration =>
configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddCarter(null, configurator =>
{
    var moduleTypes = typeof(Program).Assembly
        .GetTypes()
        .Where(t => typeof(ICarterModule).IsAssignableFrom(t) && !t.IsAbstract)
        .ToArray();

    configurator.WithModules(moduleTypes);
});


var app = builder.Build();

app.MapCarter();

// Configure the HTTP request pipeline.

app.Run();
