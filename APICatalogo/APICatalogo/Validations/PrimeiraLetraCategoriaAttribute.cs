using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validations
    {
    public class PrimeiraLetraCategoriaAttribute : ValidationAttribute
        {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext) // Propriedade e classe analisada, respectivamente
            {
                var primeiraLetra = value.ToString()[0].ToString();
                if (primeiraLetra != "C")
                    {
                        return new ValidationResult("A primeira letra da categoria tem que ser 'C'");
                    }
                return ValidationResult.Success;
            }
        }       
    }