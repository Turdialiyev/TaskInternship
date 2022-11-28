using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options => { 
    options.AddPolicy(name: "MyProject", policy => {
         policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); }); });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyProject");

app.UseAuthorization();

app.MapControllers();

app.Run();
