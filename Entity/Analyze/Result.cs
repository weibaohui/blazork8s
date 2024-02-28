using System;
using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace Entity.Analyze;

public class Result : IKubernetesObject<V1ObjectMeta>
{
    public string         ApiVersion    { get; set; }
    public string         Kind          { get; set; }
    public IList<Failure> Error         { get; set; }
    public string         ParentObject  { get; set; }

    public V1ObjectMeta Metadata { get; set; }

    public static Result NewResult()
    {
        return new Result()
        {
            ApiVersion = "result.analyze.blazork8s.github.com/v1",
            Kind       = "Result",
            Metadata = new V1ObjectMeta()
            {
                CreationTimestamp = DateTime.Now,
            }
        };
    }
}
