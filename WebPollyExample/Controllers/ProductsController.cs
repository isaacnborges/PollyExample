using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPollyExample.Clients;
using WebPollyExample.Models;

namespace WebPollyExample.Controllers
{
    public class ProductsController : Controller
    {
        public async Task<IActionResult> Index([FromServices] ProdutoApiClient client)
        {
            var response = await client.SendRequest();
            var products = JsonConvert.DeserializeObject<List<Product>>(response);
            return View(products);
        }
    }
}
