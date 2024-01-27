using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aifud.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [MinLength(3)]
        [MaxLength(20)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [MinLength(3)]
        [MaxLength(60)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [Display(Name = "Endereço")]
        [MaxLength(100)]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [Display(Name = "Número")]
        [MaxLength(10)]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [MaxLength(20)]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [StringLength(8)]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [MaxLength(20)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Informe a {0}")]
        [MaxLength(60)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o {0}")]
        [StringLength(11)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }


        [Required(ErrorMessage = "Informe o {0}")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "Formato inválido")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total do pedido")]
        public decimal PedidoTotal { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Total de itens do pedido")]
        public int TotalItensPedido { get; set; }

        [Required]
        [Display(Name= "Data do pedido")]
        [DisplayFormat(DataFormatString ="{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }

        
        [Display(Name = "Data envio do pedido")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PedidoEntregue { get; set; }

        public List<PedidoDetalhe>? PedidoItens { get; set; }
    }
}
