using System.Text.Json.Serialization;

namespace ApiAutorLivro.Models
{
    public class AutorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        //[JsonIgnore] é para ignorar na hora do post, assim não sendo necessario colocar livros nesse momento.
        [JsonIgnore]
        public ICollection<LivroModel> Livros { get; set; }
    }
}
