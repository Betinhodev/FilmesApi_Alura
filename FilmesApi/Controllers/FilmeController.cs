using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();

        [HttpPost]
        public void AdicionaFilme([FromBody] Filme filme)
        {
            filmes.Add(filme);
            Console.WriteLine(filme.Titulo);
            Console.WriteLine(filme.Duracao);
        }
    }
}
