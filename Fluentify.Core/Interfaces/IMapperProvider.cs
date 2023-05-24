using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Fluentify.Core.Interfaces
{
    public interface IMapperProvider
    {
        IMapper GetMapper();
    }
}