﻿using Ec_Supermercado.Api.DTOs;
using Ec_Supermercado.Api.Services.CategoriaService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ec_Supermercado.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTOTwo>>> Get()
        {
            var categoriaDto = await _service.GetCategorias();
            if (categoriaDto == null) { return NotFound("Categorias não foram encontradas"); }
            return Ok(categoriaDto);
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetProdutosCategorias()
        {
            var categoriaDto = await _service.GetProdutosPorCategoria();
            if (categoriaDto == null)
            {
                return NotFound("Não foi possível localizar categorias");
            }
            return Ok(categoriaDto);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoriaDto = await _service.GetCategoriaById(id);

            if (categoriaDto == null) { return  BadRequest("Não foi possível encontrar categoria!"); }
            return Ok(categoriaDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriaDTO categoriaDto)
        {
            if (categoriaDto == null) return BadRequest("Preencha todos os campos!");

            await _service.AddCategoria(categoriaDto);

            return Ok(categoriaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoryId) return BadRequest();
            if (categoriaDto == null) return BadRequest();

            await _service.UpdateCategoria(categoriaDto);
            return Ok(categoriaDto);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var categoriaDto = _service.GetCategoriaById(id);
            if(categoriaDto == null) return NotFound("Não foi possível localizar categoria");

            await _service.DeleteCategoria(id);
            return Ok(categoriaDto);
        }

    }
}
