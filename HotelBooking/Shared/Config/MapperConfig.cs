using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Config
{
    public static class MapperConfig
    {
        public static E Automapper<T,E>(T source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, E>();
            }, new LoggerFactory());
            var mapper = new Mapper(config);
            return mapper.Map<E>(source);
        }
    }
}
