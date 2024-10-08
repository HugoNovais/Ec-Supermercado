﻿using Ec_Supermercado.Api.DTOs;

namespace Ec_Supermercado.Api.Services.VendaProdutoService
{
    public interface IVendaProdutoService
    {
        Task<IEnumerable<VendaProdutoDTOTwo>> GetVendaProdutos();
        Task<VendaProdutoDTO> GetVendaProduto(int id);
        Task AddVendaProduto (VendaProdutoDTOTwo vendaProduto);
        Task UpdateVendaProduto(VendaProdutoDTO vendaProduto);
        Task DeleteVendaProduto (int id);
    }
}
