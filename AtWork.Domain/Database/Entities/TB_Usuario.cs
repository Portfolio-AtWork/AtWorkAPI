using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtWork.Domain.Database.Entities
{
    [Table("tb_usuario")]
    public class TB_Usuario : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        [Column("nome")]
        public string Nome { get; set; } = null!;

        [Required]
        [StringLength(255)]
        [Column("login")]
        public string Login { get; set; } = null!;

        [Required]
        [StringLength(255)]
        [Column("senha")]
        public string Senha { get; set; } = null!;

        [Required]
        [StringLength(1)]
        [Column("st_status")]
        public string ST_Status { get; set; } = null!;

        [Required]
        [StringLength(255)]
        [Column("email")]
        public string Email { get; set; } = null!;
    }
}
