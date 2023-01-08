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
    public class VeiculoController : ControllerBase
    {
        private readonly Context _context;
        public VeiculoController([FromServices] Context context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AlugarVeiculo/{idCliente}")]
        public async Task<IActionResult> InsertVeiculo([FromBody] VeiculoDTO requestVeiculo, [FromRoute] int idCliente)
        {
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == idCliente);

                if (cliente is null) { return NotFound(new {notFound = "Cliente não encontrado"}); }


                var veiculo = new Veiculo
                {
                    Cliente = cliente,
                    DataEntrada = DateTime.Now,
                    NomeCarro = requestVeiculo.NomeCarro,
                    Placa = requestVeiculo.Placa,
                };


                await _context.Veiculos.AddAsync(veiculo);
                await _context.SaveChangesAsync();


                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("RegistrarSaidaVeiculo/{idVeiculo}")]
        public async Task<IActionResult> UpdateVeiculo([FromRoute] int idVeiculo)
        {
            try
            {
                var veiculo = await _context.Veiculos.FirstOrDefaultAsync(x =>x.Id == idVeiculo);

                if (veiculo is null) { return NotFound(); }

                veiculo.DataSaida = DateTime.Now;

                _context.Veiculos.Update(veiculo);
                await _context.SaveChangesAsync();

                return Ok(new { sucesso = "Saída do veículo registrada sucesso" });
            }
            catch
            {
                return BadRequest();
            }
        }
        
    }
}
