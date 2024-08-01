﻿using Ec_Supermercado.Api.DTOs;
using Ec_Supermercado.Api.Services.VendaService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ec_Supermercado.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {

        private readonly IVendaService _vendaService;

        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendaDTO>>> Get()
        {
            var vendasDto = await _vendaService.GetVendas();
            if (vendasDto == null) return NotFound("Vendas não encontradas");
            return Ok(vendasDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VendaDTO>> Get(int id)
        {
            var vendaDto = await _vendaService.GetVendasById(id);
            if (vendaDto == null) return NotFound("Não foi possível encontrar Venda");
            return Ok(vendaDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VendaDTO vendaDTO)
        {
            if (vendaDTO == null) return BadRequest("Preencha todos os campos");
            await _vendaService.AddUsuario(vendaDTO);
            return Ok(vendaDTO);
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] VendaDTO vendaDTO)
        {
            if (id != vendaDTO.VendaId) return BadRequest();
            if (vendaDTO == null) return BadRequest();
            await _vendaService.Update(vendaDTO);
            return Ok(vendaDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VendaDTO>> Delete(int id)
        {
            var vendaDto = await _vendaService.GetVendasById(id);
            if (vendaDto == null) return NotFound("Não foi possível localizar Venda");
            await _vendaService.Delete(id);
            return Ok(vendaDto);
           
        }
    }
}
