using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
    [Table("Sys_RoleMenu")]
    public partial class RoleMenu : IEntity<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(40)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [ForeignKey("Menu")]
        public virtual Guid MenuId { get; set; }

        public Menu Menu { get; set; }

        [ForeignKey("Role")]
        public virtual Guid RoleId { get; set; }

        public Role Role { get; set; }

    }
}
