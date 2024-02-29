using System.Collections.Generic;

namespace Entity.Analyze;

public class Failure
{
    public string Text          { get; set; }
    public string KubernetesDoc { get; set; }
    public string KubernetesDocField { get; set; }
    public IList<Sensitive> Sensitive     { get; set; }
}
