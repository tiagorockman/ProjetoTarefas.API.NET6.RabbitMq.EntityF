using Tarefas.Domain.Commands;
using Tarefas.Domain.Commands.Contracts;

namespace Tarefas.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        GenericCommandResult Handle(T command);
    }
}
