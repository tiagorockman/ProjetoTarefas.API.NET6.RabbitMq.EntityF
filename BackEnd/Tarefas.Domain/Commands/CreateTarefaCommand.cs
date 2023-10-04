using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Globalization;
using Tarefas.Domain.Commands.Contracts;

namespace Tarefas.Domain.Commands
{
    public class CreateTarefaCommand : Notifiable, ICommand
    {
        public CreateTarefaCommand() { }

        public CreateTarefaCommand(string descricao, DateTime data)
        {
            Descricao = descricao;
            Data = data;
        }

        public string Descricao { get; set; }
        public DateTime Data { get; set; }

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
