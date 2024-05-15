using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos;

    public class UpdateFilmeDto
    {
        [Required(ErrorMessage = "O título do filme é obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O gênero do filme é obrigatório")]
        [StringLength(30, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres")]
        public string Genero { get; set; }
        [Required]
        [Range(10, 600, ErrorMessage = "A duração deve ser entre 10 e 600 minutos.")]
        public int Duracao { get; set; }
    }

