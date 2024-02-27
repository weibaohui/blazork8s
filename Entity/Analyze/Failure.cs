using System.Collections.Generic;

namespace Entity.Analyze;

public class Failure
{
    public string           Text          { get; set; }
    public IList<Sensitive> Sensitive     { get; set; }
}
