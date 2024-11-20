using Ara3D.IfcParser;
using Speckle.InterfaceGenerator;
using Speckle.Sdk.Models;
using Speckle.WebIfc.Importer.Ifc;

namespace Speckle.WebIfc.Importer.Converters;

[GenerateAutoInterface]
public class GraphConverter(INodeConverter nodeConverter) : IGraphConverter
{
  public Base Convert(IfcModel model, IfcGraph graph)
  {
    var b = new Base();
    var children = graph.GetSources().Select(x => nodeConverter.Convert(model, x)).ToList();
    b["elements"] = children;
    return b;
  }
}
