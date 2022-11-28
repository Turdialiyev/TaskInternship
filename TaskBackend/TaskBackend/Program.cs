using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Repositories;
using Task.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("Task");
});

builder.Services.AddCors(options => { 
    options.AddPolicy(name: "MyProject", policy => {
         policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); }); });

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGuessService, GuessService>();

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
