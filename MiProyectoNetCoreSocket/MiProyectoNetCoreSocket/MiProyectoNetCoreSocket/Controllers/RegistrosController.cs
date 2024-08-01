// Controllers/RegistrosController.cs
using Microsoft.AspNetCore.Mvc;
using MiProyectoNetCoreSocket.Models;
using System.Threading.Tasks;

namespace MiProyectoNetCoreSocket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrosController : ControllerBase
    {
        private readonly DbAab78eBdreportesContext _context;

        public RegistrosController(DbAab78eBdreportesContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearRegistro([FromBody] Registro registro)
        {
            if (registro == null)
            {
                return BadRequest();
            }

            _context.Registros.Add(registro);
            await _context.SaveChangesAsync();

            return Ok(registro);
        }
    }
}
