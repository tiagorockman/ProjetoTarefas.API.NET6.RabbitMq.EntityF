using FluentValidator;
using FluentValidator.Validation;
using Tarefas.Domain.Commands.Contracts;

namespace Tarefas.Domain.Commands
{
    public class UpdateTarefaCommand : Notifiable, ICommand
    {
        public UpdateTarefaCommand() { }

        public UpdateTarefaCommand(Guid id, string descricao, DateTime data, bool status)
        {
            Id = id;
            Descricao = descricao;
            Data = data;
            Status = status;
        }

        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public bool Status { get; set; }

        public void Validate()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMaxLen(Descricao, 1000, "Descrição", "Tamanho máximo ultrapassado.")
                .IsNotNull(Data, "Data", "Data não pode ser nula")
            );
        }
    }
}
