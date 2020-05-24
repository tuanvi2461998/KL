using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration ResisterMappings()
        {
            return new MapperConfiguration(cfg => {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());

            });
        }
    }
}
