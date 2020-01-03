using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Model
{
    /// <summary>
    /// 博客
    /// </summary>
    [Table("BlogArticles")]
    public class BlogArticle : IEntity<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// 这里之所以没用RootEntity，是想保持和之前的数据库一致，主键是bID，不是Id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Required]
        [MaxLength(32)]
        public virtual string bsubmitter { get; set; }

        /// <summary>
        /// 标题blog
        /// </summary>
        public virtual string btitle { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public virtual string bcategory { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataType(DataType.Text)]
        public virtual string bcontent { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public virtual int btraffic { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public virtual int bcommentNum { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public virtual DateTime bUpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime bCreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.Text)]
        public virtual string bRemark { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public virtual bool? IsDeleted { get; set; }
    }
}
