using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Core.Model;
using CoreServices.Advertisements.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebServices.Advertisements;

namespace WebCore.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementServices ;
        public ValuesController(IAdvertisementService advertisementServices)
        {
            this._advertisementServices = advertisementServices;
        }
        // GET api/values
        /// <summary>
        /// Get获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Advertisements")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<AdvertisementOutPut>> Get()
        {
            return _advertisementServices.GetAll();
        }

        // GET api/values/5
        /// <summary>
        /// Get获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public List<AdvertisementOutPut> Get(int id)
        {
           
            return _advertisementServices.Query(id);
        }

        /// <summary>
        /// Post   上传Advertisement
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AdvertisementOutPut> PostAdvertisement(AdvertisementInput product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return _advertisementServices.Add(product);
        }

        /// <summary>
        /// Post   上传PostLove
        /// </summary>
        /// <param name="love"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> PostLove(Love love)
        {
            if (love == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return "PostLove" + love.Name + love.Age;
        }

        // PUT api/values/5
        /// <summary>
        /// Put  请求
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Response.Body.Write(Encoding.UTF8.GetBytes("Put" + id));
        }

        // DELETE api/values/5
        /// <summary>
        /// Delete  请求
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Response.Body.Write(Encoding.UTF8.GetBytes("Delete" + id));
        }

        [HttpPost]
        [AllowAnonymous]
        public  string  TestPost(DataSet body)
        {
            string setXml = body.GetXml();
            string setJson = JsonConvert.SerializeObject(body);
            return "Post成功";
        }
        [HttpGet]
        [AllowAnonymous]
        public string TestGetNoParam()
        {
            return "{Code:200,Mag:"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"}";
        }

        [HttpGet]
        [AllowAnonymous]
        public string TestGetParam(string  aa,string bb)
        {
            return aa+"Get2成功" + bb;
        }
    }
}
