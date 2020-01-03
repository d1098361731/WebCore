using Core.Model;
using CoreServices.Advertisements.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace WebServices.Advertisements
{
    public interface IAdvertisementService:IService
    {
        AdvertisementOutPut Add(AdvertisementInput model);
        void Delete(AdvertisementInput model);
        void Update(AdvertisementInput model);
        List<AdvertisementOutPut> Query(int id);
        List<AdvertisementOutPut> GetAll();
    }
}
