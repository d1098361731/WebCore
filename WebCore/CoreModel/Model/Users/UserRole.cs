using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
    [Table("Sys_UserRole")]
    public  partial class UserRole : IEntity<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(40)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [ForeignKey("User")]
        public  virtual Guid UserId { get; set; }

        public  User User { get; set; }

        [ForeignKey("Role")]
        public virtual Guid RoleId { get; set; }

        public Role Role { get; set; }

    }
}
