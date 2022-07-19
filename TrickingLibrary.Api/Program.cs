using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Api.BackgroundServices.VideoEditing;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Dev"));

builder.Services.AddHostedService<VideoEditingBackgroundService>();
builder.Services.AddSingleton(_ => Channel.CreateUnbounded<EditVideoMessage>());
builder.Services.AddSingleton<VideoManager>();


builder.Services.AddCors(options => options.AddPolicy("All", build => build.AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

    if (env.IsDevelopment())
    {
        ctx.Add(new Difficulty { Id = "easy", Name = "Easy", Description = "Easy Test" });
        ctx.Add(new Difficulty { Id = "medium", Name = "Medium", Description = "Medium Test" });
        ctx.Add(new Difficulty { Id = "hard", Name = "Hard", Description = "Hard Test" });
        ctx.Add(new Category { Id = "kick", Name = "Kick", Description = "Kick Test" });
        ctx.Add(new Category { Id = "flip", Name = "Flip", Description = "Flip Test" });
        ctx.Add(new Category { Id = "transition", Name = "Transition", Description = "Transition Test" });
        ctx.Add(new Trick
        {
            Id = "backwards-roll",
            Name = "Backwards Roll",
            Description = "This is a test backwards roll",
            Difficulty = "easy",
            TrickCategories = new List<TrickCategory> { new TrickCategory { CategoryId = "flip" } }
        });
        ctx.Add(new Trick
        {
            Id = "back-flip",
            Name = "Back Flip",
            Description = "This is a test back flip",
            Difficulty = "medium",
            TrickCategories = new List<TrickCategory> { new TrickCategory { CategoryId = "flip" } },
            Prerequisites = new List<TrickRelationship>
            {
                new TrickRelationship { PrerequisiteId = "backwards-roll" }
            }
        });
        ctx.Add(new Submission
        {
            TrickId = "back-flip",
            Description = "Test description, I've tried to go for max height",
            Video = "video.mp4",
            VideoProcessed = true,
        });
        ctx.Add(new Submission
        {
            TrickId = "back-flip",
            Description = "Test description, I've tried to go for min height",
            Video = "video.mp4",
            VideoProcessed = true,
        });
        ctx.SaveChanges();
    }
}

app.UseRouting();
app.MapControllers();
app.UseCors("All");


app.Run();