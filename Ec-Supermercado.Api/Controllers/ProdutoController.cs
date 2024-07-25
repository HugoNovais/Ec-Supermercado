﻿using Ec_Supermercado.Api.DTOs;
using Ec_Supermercado.Api.Pagination.Produto;
using Ec_Supermercado.Api.Services.ProdutoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ec_Supermercado.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParams produtoParameters)
        {
            var produtosDto = await _produtoService.GetParamsProduto(produtoParameters.PageNumber, produtoParameters.PageSize);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
        {
            var produtoDto = await _produtoService.GetProdutos();

            if (produtoDto == null) return NotFound("Não foi possível achar produtos");

            return Ok(produtoDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            var produtoDto = await _produtoService.GetProdutoById(id);
            if (produtoDto == null) return NotFound("Não foi possível achar o produto");

            return Ok(produtoDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDto)
        {
            if (produtoDto == null) return BadRequest("preencha todos o campos");
            if (produtoDto.CategoryId == null) return NotFound("Categoria não existe!");

            await _produtoService.AddProduto(produtoDto);
            return Ok(produtoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProdutoDTO produtoDto)
        {
            if (produtoDto != null) return NotFound("Não foi possível achar o produto");
            if (produtoDto == null) return BadRequest();

            await _produtoService.UpdateProduto(produtoDto);
            return Ok(produtoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produtoDto = await _produtoService.GetProdutoById(id);
            if (produtoDto != null) return NotFound("Não foi possível achar o produto");
            await _produtoService.DeleteProduto(id);
            return Ok(produtoDto);
        }
    }
}
