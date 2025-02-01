using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecusrosUD_API.Models;
using RecusrosUD_API.Services;

namespace RecusrosUD_API.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadController: ControllerBase
    {

        private readonly UnidadService _unidadService;

        public UnidadController(UnidadService unidadService)
        {
            _unidadService = unidadService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadServicio>>> Get()
        {

            var unidades = await _unidadService.GetUnidadesAsync();

            return Ok(unidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUnidad(long id)
        {
            var unidad = await _unidadService.GetUnidadByIdAsync(id);

            if (unidad == null)
            {
                return NotFound(new { message = "Unidad de Servicio no encontrada" });
            }

            return Ok(unidad);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UnidadServicio nuevo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unidadService.CreateUnidadAsync(nuevo);

            return CreatedAtAction(nameof(GetUnidad), new { nuevo.Id }, nuevo);
        }


    }
}
