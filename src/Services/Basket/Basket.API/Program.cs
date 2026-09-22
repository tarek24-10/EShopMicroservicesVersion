var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var currentAssembly = typeof(Program).Assembly;

builder.Services.AddCarter(null, configurator =>
{
    var moduleTypes = currentAssembly
        .GetTypes()
        .Where(t => typeof(ICarterModule).IsAssignableFrom(t) && !t.IsAbstract)
        .ToArray();
    configurator.WithModules(moduleTypes);
});

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(currentAssembly);
    configuration.AddOpenBehavior(typeof(ValidationBeahviour<,>));
    configuration.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();

app.Run();
