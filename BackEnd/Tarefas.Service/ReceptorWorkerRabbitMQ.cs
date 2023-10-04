using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;
using System.Threading.Channels;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Interfaces;
using Tarefas.Domain.Repositories;
using Tarefas.Infra;
using Tarefas.Service.Entities;
using Tarefas.Shared;

namespace Tarefas.Service
{
    public class ReceptorWorkerRabbitMQ : IHostedService
    {
        
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ReceptorWorkerRabbitMQ(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var factoryConnection = RabbitFactoryModel.CreateModel();
            _connection = factoryConnection.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                      queue: Settings.QueueName,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null
                  );
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Executando Receptor RabbitMQ");

                _timer = new Timer(CallStart, null, TimeSpan.FromSeconds(Settings.ConsumerWakeTime), TimeSpan.FromSeconds(Settings.ConsumerReloadCheck)) ;

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Receptor RabbitMQ: Erro ao consumir mensagem. Mensagem: {ex.GetBaseException().Message}");
                return Task.CompletedTask;
            }

        }

        private void CallStart(object state)
        {

            Console.WriteLine("Verificando Fila...");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    Console.WriteLine($"Consumindo mensagem: {message}");
                    var tarefa = JsonConvert.DeserializeObject<Tarefa>(message);

                    if (tarefa != null)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var repositorio = scope.ServiceProvider.GetRequiredService<ITarefaRepository>();
                            repositorio.Criar(tarefa);
                        }
                    }

                    _channel.BasicAck(ea.DeliveryTag, false);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro retornando mensagem para fila.");
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };
              _channel.BasicConsume(queue: Settings.QueueName, autoAck: false, consumer: consumer);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Receptor RabbitMQ finalizando");
            return Task.CompletedTask;
        }
    }
}
