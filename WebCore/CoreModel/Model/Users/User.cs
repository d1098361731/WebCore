using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Table("Sys_User")]
   public partial class User:IEntity<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(40)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(60)]
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(100)]
        [Required]
        public virtual string Password { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public virtual int State { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(500)]
        public virtual  string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public virtual string Remarks { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        [MaxLength(200)]
        public virtual string Company { get; set; }
    }
}
