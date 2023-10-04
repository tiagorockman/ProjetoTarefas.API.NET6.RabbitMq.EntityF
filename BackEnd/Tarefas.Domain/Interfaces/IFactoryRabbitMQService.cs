using Tarefas.Domain.Entities;

namespace Tarefas.Domain.Interfaces
{
    public interface IFactoryRabbitMQService
    {
        (bool, string) PublicMessage(Tarefa tarefa);
    }
}