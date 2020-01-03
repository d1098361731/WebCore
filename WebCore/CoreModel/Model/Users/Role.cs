using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("Sys_Role")]
    public partial class Role : IEntity<Guid>
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
        /// 名称
        /// </summary>
        public  virtual string Name { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public virtual string Label { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int State { get; set; }

    }
}
