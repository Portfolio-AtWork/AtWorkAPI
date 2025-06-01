using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AtWork.Domain.Database.Entities
{
    [Table("tb_justificativa")]
    public class TB_Justificativa : BaseEntity
    {
        [Required]
        [Column("id_usuario")]
        public Guid ID_Usuario { get; set; }

        [ForeignKey("ID_Usuario")]
        public TB_Usuario UsuarioFK { get; set; } = null!;

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
    }
}
