using Common.Attribute;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreServices.Menus.Dtos
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [AutoMap(typeof(Menu))]
    public class MenuOutPut
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public virtual string Label { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public virtual string Icon { get; set; }

        /// <summary>
        ///  路径
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public virtual int Index { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        public virtual string Tooptip { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remarks { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public virtual Menu ParentMent { get; set; }

        public virtual Guid ParentMentId { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public virtual IEnumerable<Menu> ChildMents { get; set; }
    }
}
