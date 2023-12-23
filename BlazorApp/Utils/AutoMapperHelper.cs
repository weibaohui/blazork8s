using System.Collections.Generic;
using AutoMapper;
using k8s.Models;

namespace BlazorApp.Utils;

public static class AutoMapperHelper<S>
{
    public static IList<V1Condition> MapToCondition(IList<S> conditions)
    {

        var config =
            new MapperConfiguration(cfg => cfg.CreateMap<S, V1Condition>()
                .ForMember(dest => dest.ObservedGeneration, opt => opt.Ignore()));
        var mapper = config.CreateMapper();
        return mapper.Map<IList<S>, IList<V1Condition>>(conditions);
    }
}
