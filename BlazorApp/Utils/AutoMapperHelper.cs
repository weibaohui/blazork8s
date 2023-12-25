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
                .ForMember(dest => dest.ObservedGeneration, opt => opt.Ignore())
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                })
            );
        var mapper = config.CreateMapper();
        return mapper.Map<IList<S>, IList<V1Condition>>(conditions);
    }

    public static IList<V1ServicePort> MapToServicePorts(IList<S> subSetPorts)
    {
        // NodePort    = nodePort;
        // TargetPort  = targetPort;

        var config =
            new MapperConfiguration(cfg => cfg.CreateMap< S,V1ServicePort>()
                .ForMember(dest => dest.NodePort, opt => opt.Ignore())
                .ForMember(dest => dest.TargetPort, opt => opt.Ignore())
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                })
            );
        var mapper = config.CreateMapper();
        return mapper.Map<IList<S>, IList<V1ServicePort>>(subSetPorts);
    }
    public static IList<V1ObjectReference> MapToObjectReference(IList<S> owners)
    {
        var config =
            new MapperConfiguration(cfg => cfg.CreateMap< S,V1ObjectReference>()
                .ForMember(dest => dest.FieldPath, opt => opt.Ignore())
                .ForMember(dest => dest.NamespaceProperty, opt => opt.Ignore())
                .ForMember(dest => dest.ResourceVersion, opt => opt.Ignore())
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                })
            );
        var mapper = config.CreateMapper();
        return mapper.Map<IList<S>, IList<V1ObjectReference>>(owners);
    }

    public static V1RollingUpdateDeployment MapToRollingUpdate(S up)
    {
        var config =
            new MapperConfiguration(cfg => cfg.CreateMap< S,V1RollingUpdateDeployment>()
                // .ForMember(dest => dest.ResourceVersion, opt => opt.Ignore())
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                })
            );
        var mapper = config.CreateMapper();
        return mapper.Map<S, V1RollingUpdateDeployment>(up);
    }
}
