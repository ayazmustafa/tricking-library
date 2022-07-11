
using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("All",build =>
    {
        build.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Dev"));

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseCors("All");


app.Run();