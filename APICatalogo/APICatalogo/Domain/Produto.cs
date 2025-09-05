using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace APICatalogo.Domain
{
    [Table("TabelaProdutos")]
    public class Produto : IValidatableObject
    {
        [Key]
        public int ProdutoId {  get; set; }

        [RegularExpression(@"^\d{13}$", ErrorMessage = "Formato de EAN-13 inválido!")]
        public int? ean13 { get; set; } // Código numérico 
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(80)]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(300)]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O preço é obrigatório")]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "A url da imagem é obrigatória")]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; } // FK
        [JsonIgnore]
        public Categoria? categoria { get; set; } // Propriedade de navegação (definido que é mapeado pra categoria); não precisa serializar

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) // Essa validação vale somente para essa model
        {
            if (this.Estoque <= 0)
            {
                // yeld devolve um erro por vez.
                yield return new ValidationResult("O estoque tem que ser maior do que 0",
                new[] { nameof(this.Estoque) }
                );
            }
        }
    }
}
