using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtWork.Domain.Entities
{
    [Table("tb_grupo")]
    public class TB_Grupo : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        [Column("nome")]
        public string Nome { get; set; } = null!;

        [Required]
        [StringLength(1)]
        [Column("st_status")]
        public string ST_Status { get; set; } = null!;
    }
}
