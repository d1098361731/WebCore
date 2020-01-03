using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Advertisements")]
    public  class Advertisement: IEntity<int>
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        /// <summary>
        /// 广告图片
        /// </summary>
        public virtual string ImgUrl { get; set; }

        /// <summary>
        /// 广告标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 广告链接
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime Createdate { get; set; } = DateTime.Now;
    }
}
