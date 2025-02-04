using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecusrosUD_API.Context;
using RecusrosUD_API.Dtos;
using RecusrosUD_API.Models;
using RecusrosUD_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecusrosUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Usuario>> GetUsuario(long id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);

            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            return Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario nuevo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existe = await _usuarioService.ExisteCorreoAsync(nuevo.Correo);

            if (existe) return BadRequest(new { Message = "Ya existe un usuario registrado con este correo electrónico." });

            await _usuarioService.CreateUsuarioAsync(nuevo);

            return CreatedAtAction(nameof(GetUsuario), new { Id = nuevo.Id }, new { Id = nuevo.Id, Nombre = nuevo.Nombre, Correo = nuevo.Correo, Admin = nuevo.Admin });
        }

        // PUT api/<UsuarioController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Usuario usuarioActualizado)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _usuarioService.UpdateUsuarioAsync(usuarioActualizado);
            return NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var eliminado = await _usuarioService.DeleteUsuarioByIdAsync(id);

            if (eliminado)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
