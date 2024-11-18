using Ara3D.IfcParser;
using Ara3D.Logging;
using Ara3D.StepParser;
using Ara3D.Utils;
using Speckle.WebIfc.Importer.Ifc;

namespace Speckle.WebIfc.Importer;

public class IfcFile
{
  public IfcGraph Graph;
  public StepDocument Document => Graph.Document;
  public IfcModel Model;
  public IntPtr ApiPtr;
  public FilePath FilePath;

  public IfcFile(FilePath fp, ILogger? logger = null)
  {
    if (!File.Exists(fp))
      throw new FileNotFoundException($"File not found: {fp}");
    FilePath = fp;

    logger?.Log($"Starting load of {fp.GetFileName()}");
      LoadNonGeometryData(logger);
    logger?.Log($"Completed loading {fp.GetFileName()}");
  }


  private void LoadNonGeometryData(ILogger? logger)
  {
    logger?.Log($"Loading IFC data");
    Graph = IfcGraph.Load(FilePath, logger);
    logger?.Log($"Completed loading IFC data");
  }

  public static IfcFile Load(FilePath fp, ILogger logger = null)
    => new IfcFile(fp, logger);

}
