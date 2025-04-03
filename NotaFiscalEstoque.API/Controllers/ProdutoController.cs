using Microsoft.AspNetCore.Mvc;
using NotaFiscalEstoque.Application.DTOs;
using NotaFiscalEstoque.Application.Interfaces;

namespace NotaFiscalEstoque.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController(IProdutoService produtoService, ILogger<ProdutoController> logger) : ControllerBase
{
    private readonly IProdutoService _produtoService = produtoService;
    private readonly ILogger<ProdutoController> _logger = logger;

    [HttpGet]
    public IActionResult GetProdutos()
    {
        try
        {
            IEnumerable<ProdutoDTO> produtos = _produtoService.GetProdutos();

            return Ok(produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetProdutos_Exception");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{Id:int}")]
    public IActionResult GetProduto([FromRoute] int Id)
    {
        try
        {
            ProdutoDTO? produto = _produtoService.GetById(Id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetProduto_Exception");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public IActionResult PostProduto(ProdutoDTO produtoDTO)
    {
        try
        {
            ProdutoDTO? novoProdutoDTO = _produtoService.Create(produtoDTO);

            if (novoProdutoDTO == null)
                return BadRequest("Produto não foi criado.");

            return CreatedAtAction(nameof(GetProduto), new { Id = novoProdutoDTO.Id }, novoProdutoDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PostProduto_Exception");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPatch("{Id:int}")]
    public IActionResult PatchProduto([FromRoute] int Id, ProdutoDTO produtoDTO)
    {
        try
        {
            ProdutoDTO? produtoDbDTO = _produtoService.GetById(Id);

            if (produtoDbDTO == null)
                return NotFound();

            if (produtoDbDTO.Id != produtoDTO.Id)
                return BadRequest("Produto não condiz com o id do objeto.");

            produtoDbDTO = _produtoService.Update(Id, produtoDTO);

            if (produtoDbDTO == null)
                return BadRequest("Produto não foi atualizado");

            return Ok(produtoDbDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PatchProduto_Exception");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{Id:int}")]
    public IActionResult DeleteProduto([FromRoute] int Id)
    {
        try
        {
            ProdutoDTO? produtoDTO = _produtoService.GetById(Id);

            if (produtoDTO == null)
                return NotFound();

            bool deletado = _produtoService.Remove(Id);

            if (deletado)
                return Ok("Deletado com sucesso!");

            return StatusCode(500, "Não foi possivel deletar!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteProduto_Exception");
            return StatusCode(500, ex.Message);
        }
    }
}
