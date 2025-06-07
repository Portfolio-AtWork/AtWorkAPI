using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AtWork.Domain.Database.Entities
{
    [Table("tb_justificativa")]
    public class TB_Justificativa : BaseEntity
    {
        [Required]
        [Column("id_funcionario")]
        public Guid ID_Funcionario { get; set; }

        [ForeignKey("ID_Funcionario")]
        public TB_Funcionario FuncionarioFK { get; set; } = null!;

        [Required]
        [Column("justificativa")]
        public string Justificativa { get; set; } = "";

        [AllowNull]
        [Column("imagem_justificativa")]
        public byte[]? ImagemJustificativa { get; set; } = null;

        [Required]
        [StringLength(1)]
        [Column("st_status")]
        public string ST_Justificativa { get; set; } = null!;

        [Required]
        [Column("dt_justificativa")]
        public DateTime DT_Justificativa { get; set; }
    }
}
