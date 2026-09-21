var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var currentAssembly = typeof(Program).Assembly;

builder.Services.AddMediatR(configuration => { 
configuration.RegisterServicesFromAssembly(currentAssembly);
configuration.AddOpenBehavior(typeof(ValidationBeahviour<,>));
});

builder.Services.AddCarter(null, configurator =>
{
    var moduleTypes = currentAssembly
        .GetTypes()
        .Where(t => typeof(ICarterModule).IsAssignableFrom(t) && !t.IsAbstract)
        .ToArray();

    configurator.WithModules(moduleTypes);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database"));
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(currentAssembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
