using ApiAutorLivro.Dto.Autor;
using ApiAutorLivro.Dto.Livro;
using ApiAutorLivro.Models;
using ApiAutorLivro.Services.Autor;
using ApiAutorLivro.Services.Livro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAutorLivro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;

        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }


        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livroInterface.ListarLivros();
            return Ok(livros);
        }

        [HttpGet("BuscarLivroPorId/{idLivro}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorId(int idLivro)
        {
            var livro = await _livroInterface.BuscarLivroPorId(idLivro);
            return Ok(livro);
        }

        [HttpGet("BuscarLivroPorIdAutor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            var livros = await _livroInterface.BuscarLivroPorIdAutor(idAutor);
            return Ok(livros);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var livros = await _livroInterface.CriarLivro(livroCriacaoDto);
            return Ok(livros);
        }

        [HttpPut("EditarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            var livros = await _livroInterface.EditarLivro(livroEdicaoDto);
            return Ok(livros);
        }

        [HttpDelete("RemoverLivro/{idLivro}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> RemoverLivro(int idLivro)
        {
            var livros = await _livroInterface.RemoverLivro(idLivro);
            return Ok(livros);
        }

    }
}
