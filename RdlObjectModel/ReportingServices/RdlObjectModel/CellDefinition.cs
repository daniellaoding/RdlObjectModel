namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class CellDefinition : ReportObject
  {
    public int ColumnIndex
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

    public int RowIndex
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

    public string ParameterName
    {
      get
      {
        return (string) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public CellDefinition()
    {
      ColumnIndex = 0;
      RowIndex = 0;
    }

    internal CellDefinition(IPropertyStore propertyStore)
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
        ColumnIndex,
        RowIndex,
        ParameterName,
      }
    }
  }
}
