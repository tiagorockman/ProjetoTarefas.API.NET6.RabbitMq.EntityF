using Microsoft.EntityFrameworkCore;
using Tarefas.Domain.Handlers;
using Tarefas.Domain.Interfaces;
using Tarefas.Domain.Repositories;
using Tarefas.Infra;
using Tarefas.Infra.Contexts;
using Tarefas.Service;
using Tarefas.Shared;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();



#region Setting
Settings.InicializaStrings(builder.Configuration);

#endregion Setting

//Injecao dependencia Banco de dados em memoria
//builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("DataBase"));

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("dbTarefa")));


builder.Services.AddTransient<ITarefaRepository, TarefaRepository>();
builder.Services.AddTransient<TarefaHandler, TarefaHandler>();
builder.Services.AddTransient<IFactoryRabbitMQService, FactoryRabbitMQService>();

//worker
builder.Host.ConfigureServices(services => services.AddHostedService<ReceptorWorkerRabbitMQ>());

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
