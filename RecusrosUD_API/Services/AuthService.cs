using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecusrosUD_API.Context;
using RecusrosUD_API.Models;

namespace RecusrosUD_API.Services
{
    public class AuthService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
        private readonly PasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();

        public async Task<Usuario?> ValidarCredencialesAsync(string correo, string contra)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null)
                return null;

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contra, contra);

            return resultado == PasswordVerificationResult.Success ? usuario : null;
        }
    }
}
