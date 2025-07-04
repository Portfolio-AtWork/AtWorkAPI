﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AtWork.Domain.Database.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("tb_funcionario")]
    public class TB_Funcionario : BaseEntity
    {
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
        [Column("id_usuario")]
        public Guid ID_Usuario { get; set; }

        [ForeignKey("ID_Usuario")]
        public TB_Usuario UsuarioFK { get; set; } = null!;

        [Column("id_grupo")]
        public Guid? ID_Grupo { get; set; }

        [ForeignKey("ID_Grupo")]
        public TB_Grupo? GrupoFK { get; set; }

        [Required]
        [StringLength(255)]
        [Column("email")]
        public string Email { get; set; } = null!;
    }
}
