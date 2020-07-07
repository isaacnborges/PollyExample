using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPollyExample.Models;

namespace WebPollyExample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/request-exception")]
        public IActionResult WaitAndRetry()
        {
            return View("ErrorPolly", new CustomError
            {
                Titulo = "Http Request Exception",
                Mensagem = "Nenhuma conexão pôde ser feita porque a máquina de destino as recusou ativamente"
            });
        }

        [Route("error/circuit-breaker")]
        public IActionResult CircuitBreaker()
        {
            return View("ErrorPolly", new CustomError 
            {
                Titulo = "Erro Circuit Breaker",
                Mensagem = "Sistema está temporariamente indisponível"
            });
        }
    }
}
