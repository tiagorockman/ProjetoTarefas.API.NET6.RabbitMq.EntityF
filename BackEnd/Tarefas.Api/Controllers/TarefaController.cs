using Microsoft.AspNetCore.Mvc;
using Tarefas.Domain.Commands;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Handlers;
using Tarefas.Domain.Repositories;

namespace Tarefas.Api.Controllers
{
    [ApiController]
    [Route("v1/tarefas")]
    public class TarefaController : ControllerBase
    {
      
        private readonly ILogger<TarefaController> _logger;

        public TarefaController(ILogger<TarefaController> logger)
        {
            _logger = logger;
        }
  

        [HttpGet]
        [Route("")]
        public IActionResult GetAll([FromServices] ITarefaRepository repository)
        {
            var result = repository.SelectionaTodas();

            if (!result.Any())
                return NoContent();

            var mappedResult = TarefaDTO.MapTarefa(result);


            return Ok(mappedResult);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]CreateTarefaCommand command, [FromServices] TarefaHandler handler)
        {
            try { 
                var result = handler.Handle(command);
                
                if (!result.Success)
                    return BadRequest(result);

                return Accepted(result);

            }catch(Exception ex)
            {
                _logger.LogError($"Erro Tarefa Post>: {ex.Message}");
                return new StatusCodeResult(500);
            }
           
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody] UpdateTarefaCommand command, [FromServices] TarefaHandler handler)
        {
            try
            {
                var result = handler.Handle(command);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro Tarefa Atualizacao>: {ex.Message}");
                return new StatusCodeResult(500);

            }
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult Get(Guid Id, [FromServices] ITarefaRepository repository)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Id.ToString()))
                    BadRequest("Id não informado");

                var result = repository.SelecionaPorId(Id);

                if (result is null)
                    return NoContent();

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro Tarefa Get Por Id>: {ex.Message}");
                return new StatusCodeResult(500);

            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(Guid Id, [FromServices] ITarefaRepository repository)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Id.ToString()))
                    BadRequest("Id não informado");

                var result = repository.Delete(Id);

                if (!result)
                    return BadRequest();

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro Tarefa Delete>: {ex.Message}");
                return new StatusCodeResult(500);

            }
        }

        [HttpPut]
        [Route("marcar-como-feito")]
        public IActionResult MarkComoFeito([FromBody] MarkTarefaComoFeitaCommand command, [FromServices] TarefaHandler handler)
        {
            try
            {
              
                var result = handler.Handle(command);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro Tarefa Marcacao true>: {ex.Message}");
                return new StatusCodeResult(500);

            }
        }

        [HttpPut]
        [Route("marcar-como-nao-feito")]
        public IActionResult MarkComoNaoFeito([FromBody] MarkTarefaComoNaoFeitaCommand command, [FromServices] TarefaHandler handler)
        {
            try
            {

                var result = handler.Handle(command);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro Tarefa Marcacao False: {ex.Message}");
                return new StatusCodeResult(500);

            }
        }

        [HttpGet]
        [Route("feitas-hoje")]
        public IActionResult TarefasFeitasHoje([FromServices] ITarefaRepository repository)
        {
            try
            {

                var result = repository.SelecionaPorData(DateTime.Now.Date, true);

                if (!result.Any())
                    return NoContent();

                var mappedResult = TarefaDTO.MapTarefa(result);

                return Ok(mappedResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro Tarefas Feitas hoje: {ex.Message}");
                return new StatusCodeResult(500);

            }
        }

        [HttpGet]
        [Route("nao-feitas-hoje")]
        public IActionResult TarefasNaoFeitasHoje([FromServices] ITarefaRepository repository)
        {
            try
            {

                var result = repository.SelecionaPorData(DateTime.Now.Date, false);

                if (!result.Any())
                    return NoContent();

                var mappedResult = TarefaDTO.MapTarefa(result);

                return Ok(mappedResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro Tarefas Não Feitas hoje: {ex.Message}");
                return new StatusCodeResult(500);

            }
        }

        [HttpGet]
        [Route("nao-feitas-amanha")]
        public IActionResult TarefasNaoFeitasAmanha([FromServices] ITarefaRepository repository)
        {
            try
            {

                var result = repository.SelecionaPorData(DateTime.Now.Date.AddDays(1), false);

                if (!result.Any())
                    return NoContent();

                var mappedResult = TarefaDTO.MapTarefa(result); 

                return Ok(mappedResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro Tarefas Não Feitas Amanhã: {ex.Message}");
                return new StatusCodeResult(500);

            }
        }
    }
}