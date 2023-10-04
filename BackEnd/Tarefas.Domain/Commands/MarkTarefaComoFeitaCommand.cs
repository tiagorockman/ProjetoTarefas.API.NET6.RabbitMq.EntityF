using FluentValidator;
using FluentValidator.Validation;
using Tarefas.Domain.Commands.Contracts;

namespace Tarefas.Domain.Commands
{
    public class MarkTarefaComoFeitaCommand : Notifiable, ICommand
    {
        public MarkTarefaComoFeitaCommand()
        {
             
        }

        public MarkTarefaComoFeitaCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .IsNotNull(Id, "Id", "Id não pode ser nulo")
            );
        }
    }
}
