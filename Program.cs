using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using GlobalLinkAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// --- DbContext ---
builder.Services.AddDbContext<GlobalLinkDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Services ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GlobalLink API",
        Version = "v1",
        Description = "API de gestão de ONGs, Empresas, Doações e Necessidades"
    });
});

// --- CORS (apenas para dev) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// --- Pipeline ---

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GlobalLink API V1");
    });
}

app.UseHttpsRedirection();

// CORS
app.UseCors("AllowAll");

app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
