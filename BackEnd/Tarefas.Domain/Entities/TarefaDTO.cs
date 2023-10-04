namespace Tarefas.Domain.Entities
{
    public class TarefaDTO
    {
        public TarefaDTO(string descricao, DateTime data, bool status, Guid id)
        {
            Id = id;
            Descricao = descricao;
            Data = data;
            Status = status;
        }

        public Guid Id { get; set; }
        public string Descricao { get;  set; }
        public DateTime Data { get;  set; }
        public bool Status { get;  set; }
      

        public static IEnumerable<TarefaDTO> MapTarefa(IEnumerable<Tarefa> tarefas)
        {
             List<TarefaDTO> dTOs = new List<TarefaDTO>();
            foreach (var tarefa in tarefas)
            {
                dTOs.Add(new TarefaDTO(tarefa.Descricao, ConvertTimezone(tarefa.Data), tarefa.Status, tarefa.Id));
            }
            return dTOs;
        }

        private static DateTime ConvertTimezone(DateTime data)
        {
            TimeZoneInfo brTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            DateTime brasilDateTime = TimeZoneInfo.ConvertTimeFromUtc(data, brTimeZone);
            return brasilDateTime;
        }
    }
}
