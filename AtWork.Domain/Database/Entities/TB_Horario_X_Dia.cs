using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtWork.Domain.Database.Entities
{
    [Table("tb_horario_x_dia")]
    public class TB_Horario_X_Dia : BaseEntity
    {
        [Required]
        [Column("id_horario")]
        public Guid ID_Horario { get; set; }

        [ForeignKey("ID_Horario")]
        public TB_Horario HorarioFK { get; set; } = null!;

        [Required]
        [Column("dia_da_semana")]
        public string Dia_Da_Semana { get; set; }

        [Required]
        [Column("hora_inicio")]
        public TimeOnly Hora_Inicio { get; set; }

        [Required]
        [Column("hora_final")]
        public TimeOnly Hora_Final { get; set; }

        [Required]
        [StringLength(1)]
        [Column("st_status")]
        public string ST_Status { get; set; }
    }
}
