using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecusrosUD_API.Context;
using RecusrosUD_API.Models;

namespace RecusrosUD_API.Services
{
    public class UsuarioService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
        private readonly PasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<bool> ExisteCorreoAsync(string correo)
        {
            return await _context.Usuarios.AnyAsync(u => u.Correo == correo);
        }

        public async Task<Usuario> CreateUsuarioAsync(Usuario nuevoUsuario)
        {
            
            nuevoUsuario.Contra = _passwordHasher.HashPassword(nuevoUsuario, nuevoUsuario.Contra);

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return nuevoUsuario;
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(long id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<bool> UpdateUsuarioAsync(Usuario usuarioActualizado) {

            var anterior = await _context.Usuarios.FindAsync(usuarioActualizado.Id);

            if(anterior == null) throw new Exception("No se encontró el usuario a actualizar.");

            _context.Entry(anterior).State = EntityState.Detached;

            _context.Entry(usuarioActualizado).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUsuarioByIdAsync(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
