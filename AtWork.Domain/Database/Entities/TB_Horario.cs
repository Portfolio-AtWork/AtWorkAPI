using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AtWork.Domain.Database.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("tb_horario")]
    public class TB_Horario : BaseEntity
    {
        [Required]
        [Column("id_usuario")]
        public Guid ID_Usuario { get; set; }

        [ForeignKey("ID_Usuario")]
        public TB_Usuario UsuarioFK { get; set; } = null!;

        [Required]
        [StringLength(1)]
        [Column("st_status")]
        public string ST_Status { get; set; }
    }
}
