using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Aifud.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int Id { get; set; }

        [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Informe a descrição")]
        [Display(Name = "Descrição")]
        [MinLength(20, ErrorMessage = "{0} deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "Informe a descrição")]
        [Display(Name = "Descrição")]
        [MinLength(20, ErrorMessage = "{0} deve ter no mínimo {1} caracteres")]
        [MaxLength(400, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(0.01, 999.99, ErrorMessage = "{0} deve ter estar entre {1} e {2}")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho da imagem normal")]
        [StringLength(200, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [AllowNull]
        public string? ImagemUrl { get; set; }

        [Display(Name = "Caminho da imagem miniatura")]
        [StringLength(200, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [AllowNull]
        public string? ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Em estoque?")]
        public bool EmEstoque { get; set; }

        [Display(Name = "Categorias")]
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}
