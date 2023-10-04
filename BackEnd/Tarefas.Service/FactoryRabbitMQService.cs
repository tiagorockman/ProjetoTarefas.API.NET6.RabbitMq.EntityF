using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Interfaces;
using Tarefas.Domain.Repositories;
using Tarefas.Service.Entities;
using Tarefas.Shared;

namespace Tarefas.Service
{
    public class FactoryRabbitMQService : IFactoryRabbitMQService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public FactoryRabbitMQService()
        {
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
        public (bool, string) PublicMessage(Tarefa tarefa)
        {
            if (tarefa is null)
                return (false, "Erro: Tarefa vazia");

            try
            {
               
                _channel.QueueDeclare(
                        queue: Settings.QueueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                var stringMessage = JsonConvert.SerializeObject(tarefa);
                var bytes = Encoding.UTF8.GetBytes(stringMessage);

                _channel.BasicPublish(
                    exchange: "",
                    routingKey: Settings.QueueName,
                    basicProperties: null,
                    body: bytes
                    );

                return (true, "Sucesso");

            
            }catch (Exception ex)
            {
                throw new Exception($"Erro ao enviar para Fila no RabbitMQ {ex.GetBaseException().Message}");
            }
        }
    }
}
