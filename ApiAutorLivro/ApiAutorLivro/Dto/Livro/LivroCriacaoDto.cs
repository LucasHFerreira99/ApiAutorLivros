using ApiAutorLivro.Dto.Vinculo;
using ApiAutorLivro.Models;

namespace ApiAutorLivro.Dto.Livro
{
    public class LivroCriacaoDto
    {
        public string Titulo { get; set; }
        public AutorVinculoDto Autor { get; set; }
    }
}
