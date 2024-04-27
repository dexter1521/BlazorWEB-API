using BlazorCrud.Server.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbcrudTContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddCors(opciones => {

    opciones.AddPolicy("PermitirTodo", app =>
    {
        app.AllowAnyOrigin();
        app.AllowAnyHeader();
        app.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirTodo");

app.UseAuthorization();

app.MapControllers();

app.Run();


// Scaffold-DbContext "Server=DESKTOP-OOGIDH8\SQLEXPRESS;Database=DBCrudT;User Id=sa;Password=lxc8040;Trusted_Connection=true;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f -d
