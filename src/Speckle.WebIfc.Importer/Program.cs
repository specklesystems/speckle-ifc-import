using System.Reflection; 
using Microsoft.Extensions.DependencyInjection; 
using Speckle.Objects.Geometry; 
using Speckle.Sdk.Host; 
using Speckle.Sdk.Models;
using Speckle.WebIfc.Importer;
using Speckle.WebIfc.Importer.Converters;
using Speckle.WebIfc.Importer.Ifc;

TypeLoader.Initialize(typeof(Base).Assembly, typeof(Point).Assembly); 
var serviceCollection = new ServiceCollection(); 
serviceCollection.AddSpeckleWebIfc(); 
serviceCollection.AddMatchingInterfacesAsTransient(Assembly.GetExecutingAssembly()); 
 
var serviceProvider = serviceCollection.BuildServiceProvider(); 
 
var factory = serviceProvider.GetRequiredService<IIfcFactory>(); 
Console.WriteLine(factory.Version); 
 
var model = factory.Open("C:\\Users\\adam\\Git\\engine_web-ifc\\examples\\example.ifc"); 
 
var converter = serviceProvider.GetRequiredService<IModelConverter>(); 
var b = converter.Convert(model); 
Console.WriteLine(b.GetTotalChildrenCount());
