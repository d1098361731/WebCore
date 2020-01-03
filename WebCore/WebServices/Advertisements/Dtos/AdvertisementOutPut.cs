using AutoMapper;
using Common.Attribute;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreServices.Advertisements.Dtos
{
    /// <summary>
    /// 输出广告
    /// </summary>
    [AutoMap(typeof(Advertisement))]
    public class AdvertisementOutPut
    {
        /// <summary>
        /// 主键
        /// </summary>
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
