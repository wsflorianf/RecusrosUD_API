using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecusrosUD_API.Dtos;
using RecusrosUD_API.Models;
using RecusrosUD_API.Services;

namespace RecusrosUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoController(RecursoService recursoService, TipoRecursoService tipoRecursoService) : ControllerBase
    {
        private readonly RecursoService _recursoService = recursoService;
        private readonly TipoRecursoService _tipoRecursoService = tipoRecursoService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecursoDto>>> Get()
        {
            var recursos = await _recursoService.GetRecursosAsync();

            return Ok(recursos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecursoDto>> GetRecurso(long id)
        {
            var recurso = await _recursoService.GetRecursoByIdAsync(id);

            if(recurso == null)
            {
                return NotFound("Recurso no encontrado");
            }


            return Ok(recurso);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Recurso nuevo)
        {
            var tipoRecurso = await _tipoRecursoService.GetTipoByIdAsync(nuevo.Id);

            if(tipoRecurso == null)
            {
                return BadRequest("No se encontró el tipo de recurso.");
            }

            var creado = await _recursoService.CreateRecursoAsync(nuevo);


            return CreatedAtAction(nameof(GetRecurso), new { creado.Id }, creado);


        }

    }
}
