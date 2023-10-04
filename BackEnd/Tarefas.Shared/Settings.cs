using Microsoft.Extensions.Configuration;

namespace Tarefas.Shared
{
    public static class Settings
    {
        public static string RabbitMQHostName { get; private set; }
        public static string RabbitMQUserName { get;private set; }
        public static string RabbitMQPassword { get;private set; }
        public static int RabbitMQPort { get;private set; }
        public static string QueueName { get; private set; }
        public static int ConsumerWakeTime { get; private set; }
        public static int ConsumerReloadCheck { get; private set; }

        public static void InicializaStrings(IConfiguration configuration)
        {
            Settings.RabbitMQHostName = $"{configuration["RabbitMQHostName"]}";
            Settings.RabbitMQUserName = $"{configuration["RabbitMQUserName"]}";
            Settings.RabbitMQPassword = $"{configuration["RabbitMQPassword"]}";
            Settings.RabbitMQPort = Convert.ToInt32($"{configuration["RabbitMQPort"]}");
            Settings.QueueName = $"{configuration["QueueName"]}";
            Settings.ConsumerWakeTime = Convert.ToInt32($"{configuration["ConsumerWakeTime"]}");
            Settings.ConsumerReloadCheck = Convert.ToInt32($"{configuration["ConsumerReloadCheck"]}");

        }
    }
}