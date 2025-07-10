using VLite.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Windows Service support for MessageBox suppression
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "VLiteAPI";
});

// Configure for non-interactive mode when running as service
builder.Services.Configure<ConsoleLifetimeOptions>(options =>
{
    options.SuppressStatusMessages = true;
});

// Add services to the container
builder.Services.AddVLiteServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseVLiteMiddleware();

app.Run();
