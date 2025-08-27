using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APICatalogo.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace APICatalogo.Domain
{
    [Table("TabelaCategorias")]
    public class Categoria
    {
        [Key] // Data annotation ímplicita na convenção abaixo
        public int CategoriaId { get; set; }
        [Required]
        [StringLength(80)] // Bytes
        [PrimeiraLetraCategoria] // Validação personalizada definida em Validation
        public string? Nome { get; set; } // Nullable
        [Required]
        [StringLength(300)] // Bytes
        public string? ImagemUrl { get; set; }

        public ICollection<Produto>? Produtos { get; set; } // Defino que vai gerenciar 1-N em produtos (não vai entra nas migrations)

       public Categoria()
        {
            Produtos = new Collection<Produto>(); // Inicio a coleção que vai ser o tipo de 1-N 
        }
    }
}
