using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtWork.Domain.Database.Entities
{
    [Table("tb_ponto")]
    public class TB_Ponto : BaseEntity
    {
        [Required]
        [Column("id_funcionario")]
        public Guid ID_Funcionario { get; set; }

        [ForeignKey("ID_Funcionario")]
        public TB_Funcionario FuncionarioFK { get; set; } = null!;

        [Required]
        [Column("dt_ponto")]
        public DateTime DT_Ponto { get; set; }

        [Required]
        [StringLength(1)]
        [Column("st_ponto")]
        public string ST_Ponto { get; set; } = null!;

        [StringLength(1)]
        [Column("tp_ponto")]
        public string TP_Ponto { get; set; } = null!;
    }
}
