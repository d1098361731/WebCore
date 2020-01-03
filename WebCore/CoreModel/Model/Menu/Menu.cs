using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Table("Sys_Menu")]
    public partial class Menu : IEntity<Guid>
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
        /// 菜单名
        /// </summary>
        [MaxLength(60)]
        [Required]
        public virtual string Name { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [MaxLength(60)]
        [Required]
        public virtual string Label { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(100)]
        public virtual string Icon { get; set; }

        /// <summary>
        ///  路径
        /// </summary>
        [MaxLength(200)]
        public  virtual string Path { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        [Required]
        public virtual int Index { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        [MaxLength(100)]
        public virtual string Tooptip { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public virtual string Remarks { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public virtual Menu ParentMent { get; set; }

        [ForeignKey("ParentMent")]
        public virtual Guid? ParentMentId { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public virtual IEnumerable<Menu> ChildMents { get; set; }
    }
}
