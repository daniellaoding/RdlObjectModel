
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class SortExpression : ReportObject
  {
    public ReportExpression Value
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [DefaultValue(SortDirections.Ascending)]
    public SortDirections Direction
    {
      get
      {
        return (SortDirections) PropertyStore.GetInteger(1);
      }
      set
      {
        PropertyStore.SetInteger(1, (int) value);
      }
    }

    public SortExpression()
    {
    }

    internal SortExpression(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public SortExpression(ReportExpression value, SortDirections direction)
    {
      Value = value;
      Direction = direction;
    }

    internal class Definition : DefinitionStore<SortExpression, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Value,
        Direction,
      }
    }
  }
}
