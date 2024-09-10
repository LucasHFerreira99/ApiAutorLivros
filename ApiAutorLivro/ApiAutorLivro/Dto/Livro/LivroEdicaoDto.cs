using ApiAutorLivro.Dto.Vinculo;
using ApiAutorLivro.Models;

namespace ApiAutorLivro.Dto.Livro
{
    public class LivroEdicaoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public AutorVinculoDto Autor { get; set; }
    }
}
