using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecusrosUD_API.Dtos;
using RecusrosUD_API.Models;
using RecusrosUD_API.Services;

namespace RecusrosUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController(ReservaService reservaService, RecursoService recursoService) : ControllerBase
    {
        private readonly ReservaService _reservaService = reservaService;
        private readonly RecursoService _recursoService = recursoService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaDto>>> Get()
        {
            var reservas = await _reservaService.GetReservasAsync();

            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaDto>> GetReserva(long id)
        {
            var reserva = await _reservaService.GetReservaByIdAsync(id);

            if (reserva == null)
            {
                return NotFound("Recurso no encontrado");
            }


            return Ok(reserva);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Reserva nuevo)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;

            if (!nuevo.UsuarioId.ToString().Equals(userIdClaim)) return BadRequest(new { Message = "Usuario no válido." });

            if (!(await _reservaService.ValidateReserva(nuevo))) return BadRequest(new { Message = "El horario no es válido." });

            var creado = await _reservaService.CreateReservaAsync(nuevo);

            return CreatedAtAction(nameof(GetReserva), new { creado.Id }, creado);
        }

    }
}
