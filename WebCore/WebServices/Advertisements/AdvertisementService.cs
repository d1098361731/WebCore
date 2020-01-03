using AutoMapper;
using Common.Attribute;
using Core.Model;
using CoreRepository;
using CoreRepository.EF;
using CoreServices.Advertisements.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WebServices.Advertisements
{
    public class AdvertisementService:BaseService, IAdvertisementService
    {
        private readonly IEFRepository<Advertisement,int> _dal;
        //private readonly IEFRepository<Advertisement> _dal;
        private readonly IEFRepository<Love,int> _loveDal;
        public AdvertisementService(IEFRepository<Advertisement,int> dal
            ,IEFRepository<Love,int> loveDal
            )
        {
            this._dal = dal;
            this._loveDal = loveDal;
        }

        [UnitWork(true)]
        public AdvertisementOutPut Add(AdvertisementInput input)
        {
            var model = Mapper.Map<Advertisement>(input);
            var data = _dal.Insert(model);
            return  Mapper.Map<AdvertisementOutPut>(data); 
        }

        public void  Delete(AdvertisementInput input)
        {
            var model = Mapper.Map<Advertisement>(input);
            _dal.Delete(model);
        }


        public List<AdvertisementOutPut> Query(int id)
        {
            var query = _dal.Query(x=>x.Id==id);
            var list = query?.ToList();
            if (list != null)
            {
                return  Mapper.Map<List<AdvertisementOutPut>>(list);
            }
            return new List<AdvertisementOutPut>();

        }

        public List<AdvertisementOutPut> GetAll()
        {
            var query = _dal.GetAll();
            if (query != null)
            {
                return Mapper.Map<List<AdvertisementOutPut>>(query.ToList());
            }
            return new List<AdvertisementOutPut>();
        }

        public void  Update(AdvertisementInput input)
        {
            if (input != null)
            {
                var model = Mapper.Map<Advertisement>(input);
                _dal.Update(model);
            }
           
        }
    }
}
