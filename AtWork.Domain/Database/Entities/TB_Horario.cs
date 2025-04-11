using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtWork.Domain.Database.Entities
{
    [Table("TB_Horario")]
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
