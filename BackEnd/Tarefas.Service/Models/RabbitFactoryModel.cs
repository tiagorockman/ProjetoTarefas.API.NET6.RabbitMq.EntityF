using RabbitMQ.Client;
using Tarefas.Shared;

namespace Tarefas.Service.Entities
{
    public class RabbitFactoryModel
    {
        public static ConnectionFactory _factoryConnectionModel = new ConnectionFactory();
        public static ConnectionFactory CreateModel()
        {
            if (_factoryConnectionModel == null)
                return new ConnectionFactory()
                {
                    HostName = Settings.RabbitMQHostName,
                    UserName = Settings.RabbitMQUserName,
                    Password = Settings.RabbitMQPassword,
                    Port = Settings.RabbitMQPort
                };

            return _factoryConnectionModel;
        }
    }
}
