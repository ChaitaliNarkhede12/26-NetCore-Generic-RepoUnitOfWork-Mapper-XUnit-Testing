using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSS.TCCS.UnitTest.Core
{
    public class AutoMapping
    {
        public IMapper GetMapper(Profile profile)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(profile);
            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
        }
    }
}
