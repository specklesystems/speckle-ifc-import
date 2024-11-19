using System.Reflection;
using Ara3D.IfcParser;
using Ara3D.Utils;
using Microsoft.Extensions.DependencyInjection;
using Speckle.Objects.Geometry;
using Speckle.Sdk.Api;
using Speckle.Sdk.Host;
using Speckle.Sdk.Models;
using Speckle.WebIfc.Importer.Converters;
using Speckle.WebIfc.Importer.Ifc;

namespace Speckle.WebIfc.Importer;
public static class Import
{
  
  private static readonly Uri BaseUri = new Uri("https://api.github.com/");

  public static async Task Ifc(string filePath, string userId, string streamId, string branchName, string commitMessage, string token)
  {
    TypeLoader.Initialize(typeof(Base).Assembly, typeof(Point).Assembly);
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddSpeckleWebIfc();
    serviceCollection.AddMatchingInterfacesAsTransient(Assembly.GetExecutingAssembly());

    var serviceProvider = serviceCollection.BuildServiceProvider();
    var operations = serviceProvider.GetRequiredService<IOperations>();

    var factory = serviceProvider.GetRequiredService<IIfcFactory>();
    Console.WriteLine(factory.Version);

    var model = factory.Open(filePath);

    var graph = IfcGraph.Load(new FilePath(filePath));

    var converter = serviceProvider.GetRequiredService<IGraphConverter>();
    var b = converter.Convert(model, graph);
    var (rootId, _) = await operations.Send2(BaseUri, streamId, token, b);
  }
}
