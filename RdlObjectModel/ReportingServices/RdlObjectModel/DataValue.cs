using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class DataValue : ReportObject, IDataScopeService, IDataCell, IContainedObject
  {
    [ReportExpressionDefaultValue]
    public ReportExpression Name
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

    public ReportExpression Value
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public DataValue()
    {
    }

    internal DataValue(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    IEnumerable<IDataScope> IDataScopeService.GetDataScopesFor(IContainedObject obj)
    {
      if (!IsChildOf(obj, this))
        throw new InvalidOperationException();
      return GetContainingDataScopes();
    }

    internal class Definition : DefinitionStore<DataValue, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Value,
      }
    }
  }
}
