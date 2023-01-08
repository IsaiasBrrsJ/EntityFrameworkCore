using ApiBasica.DataContext;
using ApiBasica.Models.DTO;
using ApiBasica.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBasica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly Context _context;
        public ClienteController([FromRoute] Context context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AdcionarCliente")]
        public async Task<IActionResult> InsertCliente([FromBody] ClienteDTO requestCliente)
        {
            try
            {
                var cliente = new Cliente
                {
                    CPF = requestCliente.CPF,
                    DataCadastro = DateTime.Now,
                    Nome = requestCliente.Nome,
                };

                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();

                return Ok(cliente);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("BuscarCliente/{id}")]
        public async Task<IActionResult> SelectCliente([FromRoute] int id)
        {
            try
            {
                var cliente = await _context.Clientes.FirstAsync(x => x.Id == id);

                if (cliente is null)
                    return NotFound(new { NotFound = "Cliente não encontrado" });


                return Ok(cliente);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
