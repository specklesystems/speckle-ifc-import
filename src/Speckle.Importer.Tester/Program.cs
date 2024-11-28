using Ara3D.IfcParser;
using Ara3D.Utils;
using Speckle.WebIfc.Importer;

var serviceProvider = Import.GetServiceProvider();

var graph = IfcGraph.Load(
  new FilePath(
    "C:\\Users\\adam\\Git\\speckle-server\\packages\\fileimport-service\\ifc-dotnet\\ifcs\\small.ifc"
  )
);

Console.WriteLine(graph.Document.RawInstances.Length);
