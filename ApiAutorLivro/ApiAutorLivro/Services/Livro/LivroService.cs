using ApiAutorLivro.Data;
using ApiAutorLivro.Dto.Autor;
using ApiAutorLivro.Dto.Livro;
using ApiAutorLivro.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAutorLivro.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await _context.Livros.Include(a=> a.Autor).ToListAsync();

                if (livros == null || !livros.Any())
                {
                    resposta.Mensagem = "Não há livros cadastrados!";
                    return resposta;
                }

                resposta.Dados = livros;
                resposta.Mensagem = "Todos os livros foram coletados";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }


        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _context.Livros.Include(a=> a.Autor).FirstOrDefaultAsync(x => x.Id == idLivro);
                if(livro == null)
                {
                    resposta.Mensagem = "Nenhum registro localizado";
                    return resposta;
                }
                resposta.Dados = livro;
                resposta.Mensagem = "Livro localizado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor).Where(livroBanco => livroBanco.Autor.Id == idAutor).ToListAsync();
                if (livro == null || !livro.Any())
                {
                    resposta.Mensagem = "Nenhum registro localizado";
                    return resposta;
                }
                resposta.Dados = livro;
                resposta.Mensagem = $"Todos os livros do autor de ID={idAutor} foram coletados";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);

                if(autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor localiado!";
                    return resposta;
                }

                var livro = new LivroModel()
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor
                };

                _context.Add(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Mensagem = "Livro criado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros
                    .Include(a=>a.Autor)
                    .FirstOrDefaultAsync(x => x.Id == livroEdicaoDto.Id);

                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDto.Autor.Id);
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro de livro localizado";
                    return resposta;
                }
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor localizado";
                    return resposta;
                }

                livro.Titulo = livroEdicaoDto.Titulo;
                livro.Autor = autor;

                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.ToListAsync();
                resposta.Mensagem = "Livro editado com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> RemoverLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado";
                    return resposta;
                }
                _context.Remove(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Mensagem = "Livro removido com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
