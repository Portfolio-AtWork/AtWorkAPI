using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AtWork.Domain.Database.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("tb_grupo")]
    public class TB_Grupo : BaseEntity
    {
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
