using ApiAggregation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container with Startup class.
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline with Startup class.
startup.Configure(app, app.Environment);

app.Run();