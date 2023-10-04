using FluentValidator;
using Tarefas.Domain.Commands;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Handlers.Contracts;
using Tarefas.Domain.Interfaces;
using Tarefas.Domain.Repositories;

namespace Tarefas.Domain.Handlers
{
    public class TarefaHandler : Notifiable, 
        IHandler<CreateTarefaCommand>, IHandler<MarkTarefaComoFeitaCommand>, 
        IHandler<MarkTarefaComoNaoFeitaCommand>, IHandler<UpdateTarefaCommand>
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IFactoryRabbitMQService _rabbitMQService;

        public TarefaHandler(ITarefaRepository tarefaRepository, IFactoryRabbitMQService rabbitMQService)
        {
            _tarefaRepository = tarefaRepository;
            _rabbitMQService = rabbitMQService;
        }

        public GenericCommandResult Handle(CreateTarefaCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                    return new GenericCommandResult(false, "Erro: ", command.Notifications);

                var tarefa = new Tarefa(command.Descricao, command.Data, false);

                (bool status, string message) =  _rabbitMQService.PublicMessage(tarefa);

                if(!status)
                    return new GenericCommandResult(false, "Erro: ", message);

                return new GenericCommandResult(true, "Tarefa enviada para processamento", tarefa);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public GenericCommandResult Handle(MarkTarefaComoFeitaCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                    return new GenericCommandResult(false, "Erro: ", command.Notifications);

                var tarefa = _tarefaRepository.SelecionaPorId(command.Id);

                tarefa.MarkComoFeita();

                _tarefaRepository.Atualizar(tarefa);

                return new GenericCommandResult(true, "Tarefa salva", tarefa);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Erro_exeption: ", new { Erro = ex.Message });
            }
        }

        public GenericCommandResult Handle(MarkTarefaComoNaoFeitaCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                    return new GenericCommandResult(false, "Erro: ", command.Notifications);

                var tarefa = _tarefaRepository.SelecionaPorId(command.Id);

                tarefa.MarkComoNaoFeita();

                _tarefaRepository.Atualizar(tarefa);

                return new GenericCommandResult(true, "Tarefa salva", tarefa);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Erro_exeption: ", new { Erro = ex.Message });
            }
        }

        public GenericCommandResult Handle(UpdateTarefaCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                    return new GenericCommandResult(false, "Erro: ", command.Notifications);

                Tarefa tarefa = _tarefaRepository.SelecionaPorId(command.Id);
                if (tarefa is null)
                    return new GenericCommandResult(false, "Erro: ", new { Erro = "Não encontrado" });

                tarefa.AtualizaDescricao(command.Descricao);
                tarefa.AtualizaData(command.Data);
                tarefa.AtualizaStatus(command.Status);

                _tarefaRepository.Atualizar(tarefa);
                return new GenericCommandResult(true, "Tarefa salva", tarefa);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Erro_exeption: ", new { Erro = ex.Message });
            }
        }
    }
}
