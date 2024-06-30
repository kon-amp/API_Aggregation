using ApiAggregation;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container with Startup class.
Startup? startup = new(builder.Configuration);
startup.ConfigureServices(builder.Services);

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline with Startup class.
startup.Configure(app, app.Environment);

app.Run();