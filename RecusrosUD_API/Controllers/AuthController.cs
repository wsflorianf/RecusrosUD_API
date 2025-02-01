using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecusrosUD_API.Dtos;
using RecusrosUD_API.Services;

namespace RecusrosUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(AuthService authService, JwtService jwtService) { 
            _authService = authService;
            _jwtService = jwtService;
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto datos)
        {
            var usuario = await _authService.ValidarCredencialesAsync(datos.Correo, datos.Contra);
            if (usuario == null)
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });

            var token = _jwtService.GenerarToken(usuario.Correo, (int)usuario.Id, usuario.Admin);            

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = false,  // 🔹 No accesible por JavaScript
                Secure = true,    // 🔹 Solo en HTTPS (en producción)
                SameSite = SameSiteMode.None, // 🔹 Protege contra ataques CSRF
                Expires = DateTime.UtcNow.AddHours(4) // 🔹 Expira en 4 horas
            });
            
            return Ok(new {Id = usuario.Id, Nombre=usuario.Nombre, Correo=usuario.Correo, Admin = usuario.Admin });
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Append("jwt", "", new CookieOptions
            {
                HttpOnly = false,  // 🔹 No accesible por JavaScript
                Secure = true,    // 🔹 Solo en HTTPS (en producción)
                SameSite = SameSiteMode.None, // 🔹 Protege contra ataques CSRF
                Expires = DateTime.UtcNow.AddMonths(-1) // 🔹 Expira en 1 minuto
            });
            return Ok(new { Message = "Sesión cerrada correctamente" });
        }



    }
    }
