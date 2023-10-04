using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Repositories;
using Tarefas.Infra.Contexts;

namespace Tarefas.Infra
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly DataContext _context;
        public TarefaRepository(DataContext context)
        {
            _context = context;
        }

        public void Atualizar(Tarefa tarefa)
        {
            _context.Entry(tarefa).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Criar(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
        }

        public IEnumerable<Tarefa> SelecionaFeitas()
        {
            return _context.Tarefas
               .AsNoTracking()
               .Where(t => t.Status == true)
               .OrderByDescending(t => t.Data);
        }

        public IEnumerable<Tarefa> SelecionaNaoFeitas()
        {
            return _context.Tarefas
                .AsNoTracking()
                .Where(t => t.Status == false)
                .OrderByDescending(t => t.Data);
        }

        public Tarefa SelecionaPorId(Guid id)
        {
            return _context.Tarefas.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Tarefa> SelecionaPorData(DateTime datafiltro, bool status = true)
        {
            return _context.Tarefas
               .AsNoTracking()
               .Where(t => t.Data.Date == datafiltro.Date && t.Status == status)
               .OrderByDescending(t => t.Data);
        }

        public IEnumerable<Tarefa> SelectionaTodas()
        {
            return _context.Tarefas
               .AsNoTracking()
               .OrderByDescending(t => t.Data);
        }

        public bool Delete(Guid id)
        {
            var tarefa = (Tarefa)SelecionaPorId(id);
            if(tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
