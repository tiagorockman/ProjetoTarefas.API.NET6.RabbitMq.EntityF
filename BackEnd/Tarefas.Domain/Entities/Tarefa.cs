namespace Tarefas.Domain.Entities
{
    public class Tarefa : Entity
    {

        public Tarefa(string descricao, DateTime data, bool status)
        {
            Descricao = descricao;
            Data = data;
            Status = status;
        } 

        public string Descricao { get; private set; }
        public DateTime Data { get; private set; }
        public bool Status { get; private  set; }

        public void MarkComoFeita()
        {
            Status = true;
        }

        public void MarkComoNaoFeita()
        {
            Status = false;
        }

        public void AtualizaDescricao(string descricao)
        {
            if(!Descricao.Equals(descricao))
                Descricao = descricao;
        }

        internal void AtualizaData(DateTime data)
        {
           if(!Data.Equals(data))
                Data = data;
        }

        internal void AtualizaStatus(bool status)
        {
            this.Status = status;
        }
    }
}
