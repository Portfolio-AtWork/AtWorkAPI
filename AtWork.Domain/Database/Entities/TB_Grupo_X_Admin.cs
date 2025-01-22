using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtWork.Domain.Database.Entities
{
    [Table("tb_grupo_x_admin")]
    public class TB_Grupo_X_Admin : BaseEntity
    {
        [Required]
        [Column("id_usuario")]
        public Guid ID_Usuario { get; set; }

        [ForeignKey("ID_Usuario")]
        public TB_Usuario UsuarioFK { get; set; } = null!;

        [Required]
        [Column("id_grupo")]
        public Guid ID_Grupo { get; set; }

        [ForeignKey("ID_Grupo")]
        public TB_Grupo? GrupoFK { get; set; }
    }
}
