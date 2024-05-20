using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public CinemaController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um cinema ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um cinema</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ConsultaCinemaPorId), new { id = cinema.Id }, cinema);
        }

        [HttpGet]

        public IEnumerable<ReadCinemaDto> ConsultaTodosCinemas([FromQuery] int? enderecoId = null)
        {
            if(enderecoId == null)
            {
                return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
            }

            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM cinemas where cinemas.EnderecoId = {enderecoId}").ToList());
            
        }

        [HttpGet("{id}")]

        public IActionResult ConsultaCinemaPorId(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) return NotFound();
            var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return Ok(cinemaDto);
        }

        [HttpPut("{id}")]

        public IActionResult AtualizarCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) return NotFound();
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]

        public IActionResult DeletaCinema(int id)
        {
            var cinema = _context.Filmes.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) return NotFound();
            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
