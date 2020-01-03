using AutoMapper;
using Core.Model;
using CoreServices.Advertisements.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebServices;

namespace WebCore.Profile
{
    public class AutoMapProfile: AutoMapper.Profile
    {

        public AutoMapProfile()
        {
            Type[] types = GetTypes();
            if (types != null && types.Length > 0)
            {
                foreach (var type in types)
                {
                    CreateMap(type);
                }
            }
        }

        private Type[]    GetTypes()
        {
            var types = Assembly.GetAssembly(typeof(BaseService)).GetTypes();
            return types;
        }

        private   void CreateMap(Type type)
        {
            if (type != null)
            {
                foreach (var autoMapAttr in type.GetCustomAttributes())
                {
                    if (autoMapAttr == null)
                        continue;
                    if (autoMapAttr is Common.Attribute.AutoMapAttribute)
                    {
                        var attr = autoMapAttr as Common.Attribute.AutoMapAttribute;
                        CreateMap(type, attr.Type);
                        CreateMap(attr.Type, type);
                    }
                    if (autoMapAttr is Common.Attribute.AutoMapToAttribute)
                    {
                        var attr = autoMapAttr as Common.Attribute.AutoMapToAttribute;
                        CreateMap(type, attr.Type);
                    }
                    if (autoMapAttr is Common.Attribute.AutoMapFormAttribute)
                    {
                        var attr = autoMapAttr as Common.Attribute.AutoMapFormAttribute;
                        CreateMap(attr.Type, type);
                    }
                }
            }
        }
    }
}
