using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMicroservices.Services.Order.Application.Mapping
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazyMapper = new Lazy<IMapper>(() => {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();

            });

            return config.CreateMapper();
        
        });

        public static IMapper Mapper
        {
            get { return lazyMapper.Value; }
        }
    }
}
