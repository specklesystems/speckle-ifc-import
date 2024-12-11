using System.Diagnostics;
using Ara3D.IfcParser;
using Ara3D.Utils;
using Microsoft.Extensions.DependencyInjection;
using Speckle.Importer.Tester;
using Speckle.Sdk.Serialisation.V2.Send;
using Speckle.WebIfc.Importer;
using Speckle.WebIfc.Importer.Converters;
using Speckle.WebIfc.Importer.Ifc;

var serviceProvider = Import.GetServiceProvider();

var filePath = new FilePath(
  "C:\\Users\\adam\\Git\\speckle-server\\packages\\fileimport-service\\ifc-dotnet\\ifcs\\20210221PRIMARK.ifc"
);

var ifcFactory = serviceProvider.GetRequiredService<IIfcFactory>();
var stopwatch = Stopwatch.StartNew();

Console.WriteLine($"Opening with WebIFC: {filePath}");
var model = ifcFactory.Open(filePath);
var ms = stopwatch.ElapsedMilliseconds;
Console.WriteLine($"Opened with WebIFC: {ms} ms");

var graph = IfcGraph.Load(new FilePath(filePath));
var ms2 = stopwatch.ElapsedMilliseconds;
Console.WriteLine($"Loaded with StepParser: {ms2 - ms} ms");

var converter = serviceProvider.GetRequiredService<IGraphConverter>();
var b = converter.Convert(model, graph);
ms = ms2;
ms2 = stopwatch.ElapsedMilliseconds;
Console.WriteLine($"Converted to Speckle Bases: {ms2 - ms} ms");
var objects = new Dictionary<string, string>();

var process2 = new SerializeProcess(
  new Progress(true),
  new DummySendCacheManager(objects),
  new DummyServerObjectManager(),
  new BaseChildFinder(new BasePropertyGatherer()),
  new ObjectSerializerFactory(new BasePropertyGatherer()),
  new SerializeProcessOptions(true, true, true, true)
);
Console.ReadLine();
var (rootId, _) = await process2.Serialize(b, default).ConfigureAwait(false);
Console.WriteLine(rootId);
ms2 = stopwatch.ElapsedMilliseconds;
Console.WriteLine($"Converted to JSON: {ms2 - ms} ms");

Console.ReadLine();
