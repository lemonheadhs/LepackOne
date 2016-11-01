using AutoMapper;
using LepackOne.Extension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;

namespace LepackOne.Extension.Mapping
{
    public static class MappingConfiguration
    {
        public static void Initialize(IConfiguration config, ApplicationContext applicationContext)
        {
            config.CreateMap<AttainmentDTO, Attainment>()
                .ForMember(dest => dest.IsTarget, opts => opts.MapFrom(src => "Y".Equals(src.IsTarget, StringComparison.OrdinalIgnoreCase)))
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.DegreeLevel, 
                    opts => opts.MapFrom(
                        src => string.IsNullOrEmpty(src.DegreeLevel) 
                            || src.DegreeLevel.Equals("N/A", StringComparison.OrdinalIgnoreCase) 
                            ? "N/A" : src.DegreeLevel));
        }
    }
}
