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
    public class ProductsController : ControllerBase
    {
        private static readonly IList<Product> _products =
            new List<Product>
            {
                new Product(Guid.NewGuid(), 1, "product1", 11),
                new Product(Guid.NewGuid(), 2, "product2", 22)
            };

        [HttpPost]
        public IActionResult Post([FromBody] Product request)
        {
            try
            {
                var produto = new Product(Guid.NewGuid(), request.Code, request.Description, request.Price);
                _products.Add(produto);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product request)
        {
            var produto = _products.FirstOrDefault(x => x.Id == request.Id);
            if (produto != null)
            {
                _products.Remove(produto);

                produto
                    .SetCode(request.Code)
                    .SetDescription(request.Description)
                    .SetPrice(request.Price);

                _products.Add(produto);

                return Ok(produto);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var produto = _products.FirstOrDefault(x => x.Id == id);
            if (produto != null)
            {
                _products.Remove(produto);
                return Ok(true);
            }
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            var produto = _products.FirstOrDefault(x => x.Id == id);
            if (produto != null)
                return new ObjectResult(produto);
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_products.OrderBy(x => x.Code).ToList());
    }
}
