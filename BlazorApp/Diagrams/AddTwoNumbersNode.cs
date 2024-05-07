using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace BlazorApp.Diagrams;

public class AddTwoNumbersNode(Point? position = null) : NodeModel(position)
{
    public double FirstNumber { get; set; }
    public double SecondNumber { get; set; }
}
