using Kanban.Contexts;
using Kanban.Repositories;
using Kanban.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IServiceCollection services = builder.Services;

services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddOpenApi();
services.AddDbContext<KanbanContext>();
services.AddTransient<IUserRepository, UserRepository>();
services.AddTransient<IStoryRepository, StoryRepository>();
services.AddTransient<IUserService, UserService>();
services.AddTransient<IStoryService, StoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
