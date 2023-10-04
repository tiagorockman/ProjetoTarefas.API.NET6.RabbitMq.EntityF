using Tarefas.Domain.Entities;

namespace Tarefas.Domain.Repositories
{
    public interface ITarefaRepository
    {
        void Criar(Tarefa tarefa);
        void Atualizar(Tarefa tarefa);
        Tarefa SelecionaPorId(Guid id);
        IEnumerable<Tarefa> SelectionaTodas();
        IEnumerable<Tarefa> SelecionaFeitas();
        IEnumerable<Tarefa> SelecionaNaoFeitas();
        IEnumerable<Tarefa> SelecionaPorData(DateTime datafiltro, bool status = true);
        bool Delete(Guid id);
    }
}
