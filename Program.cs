using Microsoft.EntityFrameworkCore;
using ZipCodeApi.Data;

var builder = WebApplication.CreateBuilder(args);


// Register SQLite DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Data/zipcodes.db"));

// Add controllers and API tools
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS for local frontend access
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Enable Swagger UI for API testing
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection(); // Enables redirect to HTTPS if setup
app.UseStaticFiles(); // Serves HTML/CSS/JS from wwwroot
app.UseRouting();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html"); // Single-page app fallback

app.Run();