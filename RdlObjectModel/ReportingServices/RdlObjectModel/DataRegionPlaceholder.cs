using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class DataRegionPlaceholder : ReportItem
  {
    public const bool AllowTypeInHeaderFooter = false;

    [XmlIgnore]
    public override bool AllowInHeaderFooter => false;

	  public DataRegionPlaceholder()
    {
    }

    internal DataRegionPlaceholder(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<DataRegionPlaceholder, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name = 1,
        Top = 3,
        Left = 4,
        Height = 5,
        Width = 6,
        ZIndex = 7,
      }
    }
  }
}
