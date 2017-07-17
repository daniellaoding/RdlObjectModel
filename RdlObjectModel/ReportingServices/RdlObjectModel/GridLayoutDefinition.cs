using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class GridLayoutDefinition : ReportObject
  {
    public const int MaxNumberOfRows = 10000;
    public const int MaxNumberOfColumns = 8;
    public const int MaxNumberOfConsecutiveEmptyRows = 20;

    public int NumberOfColumns
    {
      get
      {
        return PropertyStore.GetInteger(0);
      }
      set
      {
        PropertyStore.SetInteger(0, value);
      }
    }

    public int NumberOfRows
    {
      get
      {
        return PropertyStore.GetInteger(1);
      }
      set
      {
        PropertyStore.SetInteger(1, value);
      }
    }

    [XmlElement(typeof (RdlCollection<CellDefinition>))]
    public IList<CellDefinition> CellDefinitions
    {
      get
      {
        return PropertyStore.GetObject<RdlCollection<CellDefinition>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public GridLayoutDefinition()
    {
      CellDefinitions = new RdlCollection<CellDefinition>();
      NumberOfColumns = 4;
      NumberOfRows = 2;
    }

    internal GridLayoutDefinition(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<GridLayoutDefinition, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        NumberOfColumns,
        NumberOfRows,
        CellDefinitions,
      }
    }
  }
}
