using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecusrosUD_API.Dtos;
using RecusrosUD_API.Models;
using RecusrosUD_API.Services;

namespace RecusrosUD_API.Controllers
{
    [Controller]
    [Authorize(Policy = "AdminOnly")]
    [Route("api/[controller]")]
    public class TipoController(TipoRecursoService tipoRecursoService, UnidadService unidadService) : ControllerBase
    {
        private readonly TipoRecursoService _tipoRecursoService = tipoRecursoService;
        private readonly UnidadService _unidadService = unidadService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoRecursoDto>>> Get()
        {
            var tipos = await _tipoRecursoService.GetTiposRecursoAsync();

            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoRecursoDto>> GetTipo(long id)
        {
            var tipo = await _tipoRecursoService.GetTipoByIdAsync(id);

            if (tipo == null)
            {
                return NotFound(new { message = "Tipo de recurso no encontrado." });
            }

            return Ok(tipo);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoRecurso nuevo)
        {

            // 1. Obtener la UnidadServicio asociada
            var unidadServicio = await _unidadService.GetUnidadByIdAsync(nuevo.UnidadId);


            if (unidadServicio == null)
            {
                return BadRequest(new { Message = "Unidad de servicio no encontrada." });
            }

            var horarioTipo = JsonConvert.DeserializeObject<Dictionary<string, Dia>>(nuevo.HorarioDisponibilidad);
            var horarioUnidad = JsonConvert.DeserializeObject<Dictionary<string, Dia>>(unidadServicio.HorarioDisponibilidad);

            if (horarioTipo == null) return BadRequest(new {Message = "Hubo un error con el horario recibido." });
            if (horarioUnidad == null) return BadRequest(new { Message = "Hubo un error con el horario de la Unidad de Servicio." });


            // 2. Validar los horarios del TipoRecurso
            foreach (var dia in horarioTipo)
            {
                // Verificar si el día está presente en la UnidadServicio
                if (horarioUnidad.ContainsKey(dia.Key))
                {
                    var unidadDia = horarioUnidad[dia.Key];
                    var tipoRecursoDia = dia.Value;

                    // 5. Verificar si el horario del TipoRecurso está dentro del horario de la UnidadServicio
                    if (!IsHorarioValido(tipoRecursoDia, unidadDia, unidadServicio.TiempoMin))
                    {
                        return BadRequest(new {Message = $"El horario del {dia.Key} no está dentro del rango permitido por la unidad de servicio." });
                    }
                }
                else
                {
                    // Si el día no está presente en la UnidadServicio
                    return BadRequest(new { Message = $"No se encontró horario para el día {dia.Key} en la unidad de servicio." });
                }
            }

            // 6. Si todo es válido, se crea el nuevo TipoRecurso


            var creado = await _tipoRecursoService.CreateTipoRecursoAsync(nuevo);


            return CreatedAtAction(nameof(GetTipo), new { creado.Id }, creado);
        }

        // Función para verificar si el horario del TipoRecurso está dentro del horario de la UnidadServicio
        private static bool IsHorarioValido(Dia tipoRecursoHorario, Dia unidadServicioHorario, TimeSpan tiempoMin)
        {

            // Convertimos los horarios de inicio y fin a TimeSpan para compararlos
            var tipoRecursoInicio = (tipoRecursoHorario.Inicio);
            var tipoRecursoFin = (tipoRecursoHorario.Fin);
            var unidadServicioInicio = (unidadServicioHorario.Inicio);
            var unidadServicioFin = (unidadServicioHorario.Fin);

            if ((unidadServicioInicio + unidadServicioFin + tipoRecursoInicio + tipoRecursoFin).TotalHours == 0)
            {
                Console.WriteLine("Error papa");
                return true;
            }

            // Verificamos que el horario del TipoRecurso esté dentro del rango de la UnidadServicio
            if (tipoRecursoInicio < tipoRecursoFin)
            {
                return tipoRecursoInicio >= unidadServicioInicio && tipoRecursoFin <= unidadServicioFin;
            }

            return false;
        }



    }
}
