using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private static readonly IList<Produto> _produtos =
            new List<Produto>
            {
                new Produto(Guid.NewGuid(), 1, "produto1", 11),
                new Produto(Guid.NewGuid(), 2, "produto2", 22)
            };

        [HttpPost]
        public IActionResult Post([FromBody] Produto request)
        {
            try
            {
                var produto = new Produto(Guid.NewGuid(), request.Codigo, request.Descricao, request.Valor);
                _produtos.Add(produto);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Produto request)
        {
            var produto = _produtos.FirstOrDefault(x => x.Id == request.Id);
            if (produto != null)
            {
                _produtos.Remove(produto);

                produto
                    .InformarCodigo(request.Codigo)
                    .InformarDescricao(request.Descricao)
                    .InformarValor(request.Valor);

                _produtos.Add(produto);

                return Ok(produto);
            }

            return NotFound();
        }

        [HttpDelete, Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var produto = _produtos.FirstOrDefault(x => x.Id == id);
            if (produto != null)
            {
                _produtos.Remove(produto);
                return Ok(true);
            }
            else
                return NotFound();
        }

        [HttpGet, Route("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            var produto = _produtos.FirstOrDefault(x => x.Id == id);
            if (produto != null)
                return new ObjectResult(produto);
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_produtos.OrderBy(x => x.Codigo).ToList());
    }
}
