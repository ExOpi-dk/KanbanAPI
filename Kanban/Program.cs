using Kanban.Contexts;
using Kanban.Repositories;
using Kanban.Services;
using Kanban.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IServiceCollection services = builder.Services;

services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddOpenApi();
services.AddDbContext<KanbanContext>();
services.AddTransient<IRepository<User>, Repository<User>>();
services.AddTransient<IRepository<Board>, Repository<Board>>();
services.AddTransient<IRepository<Story>, Repository<Story>>();
services.AddTransient<IRepository<Status>, Repository<Status>>();
services.AddTransient<IService<User>, Service<User>>();
services.AddTransient<IService<Board>, Service<Board>>();
services.AddTransient<IService<Story>, Service<Story>>();
services.AddTransient<IService<Status>, Service<Status>>();

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
