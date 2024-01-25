using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aifud.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [StringLength(100, ErrorMessage = "Tamanho máximo é 100 caracteres")]
        [Required(ErrorMessage ="Informe o nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [StringLength(200, ErrorMessage = "Tamanho máximo é 200 caracteres")]
        [Required(ErrorMessage = "Informe a descrição")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public IEnumerable<Lanche> Lanches { get; set; }
    }
}
