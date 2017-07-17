using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class ReportObjectModelContext
  {
    private Dictionary<string, RdlFunctionDefinition> m_rdlFunctions;

	  internal IComplexRdlCollection Fields { get; private set; }

	  internal IComplexRdlCollection Parameters { get; private set; }

	  internal IComplexRdlCollection ReportItems { get; private set; }

	  internal IComplexRdlCollection DataSources { get; private set; }

	  internal IComplexRdlCollection DataSets { get; private set; }

	  internal ISimpleRdlCollection Globals { get; private set; }

	  internal ISimpleRdlCollection User { get; private set; }

	  internal IComplexRdlCollection Variables { get; private set; }

	  internal ReportObjectModelContext()
    {
      Init();
    }

    private void Init()
    {
      m_rdlFunctions = new Dictionary<string, RdlFunctionDefinition>(StringUtil.CaseInsensitiveComparer);
      AddAggregate(new RdlFunctionDefinition("Sum", typeof (FunctionAggrSum), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Numeric),
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("Avg", typeof (FunctionAggrAvg), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Numeric),
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("Max", typeof (FunctionAggrMax), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("Min", typeof (FunctionAggrMin), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("Count", typeof (FunctionAggrCount), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.VariantOrBinary),
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("CountDistinct", typeof (FunctionAggrCountDistinct), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("CountRows", typeof (FunctionAggrCountRows), new RdlFunctionArg[2]
      {
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("StDev", typeof (FunctionAggrStdev), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Numeric),
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("StDevP", typeof (FunctionAggrStdevp), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Numeric),
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("Var", typeof (FunctionAggrVar), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Numeric),
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("VarP", typeof (FunctionAggrVarp), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Numeric),
        new RdlFunctionArg(false, RdlArgTypes.Scope),
        new RdlFunctionArg(false, RdlArgTypes.Recursive)
      }));
      AddAggregate(new RdlFunctionDefinition("First", typeof (FunctionAggrFirst), new RdlFunctionArg[2]
      {
        new RdlFunctionArg(true, RdlArgTypes.VariantOrBinary),
        new RdlFunctionArg(false, RdlArgTypes.Scope)
      }));
      AddAggregate(new RdlFunctionDefinition("Last", typeof (FunctionAggrLast), new RdlFunctionArg[2]
      {
        new RdlFunctionArg(true, RdlArgTypes.VariantOrBinary),
        new RdlFunctionArg(false, RdlArgTypes.Scope)
      }));
      AddAggregate(new RdlFunctionDefinition("Previous", typeof (FunctionAggrPrev), new RdlFunctionArg[2]
      {
        new RdlFunctionArg(true, RdlArgTypes.VariantOrBinary),
        new RdlFunctionArg(false, RdlArgTypes.Scope)
      }));
      AddAggregate(new RdlFunctionDefinition("RunningValue", typeof (FunctionAggrRunningValue), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.VariantOrBinary),
        new RdlFunctionArg(true, RdlArgTypes.AggregateFunction),
        new RdlFunctionArg(true, RdlArgTypes.Scope)
      }));
      AddAggregate(new RdlFunctionDefinition("RowNumber", typeof (FunctionAggrRowNumber), new RdlFunctionArg[1]
      {
        new RdlFunctionArg(true, RdlArgTypes.Scope)
      }));
      AddAggregate(new RdlFunctionDefinition("Aggregate", typeof (FunctionAggrAggregate), new RdlFunctionArg[2]
      {
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(false, RdlArgTypes.Scope)
      }));
      AddFunction(new RdlFunctionDefinition("Level", typeof (FunctionAggrLevel), new RdlFunctionArg[1]
      {
        new RdlFunctionArg(false, RdlArgTypes.Scope)
      }));
      AddFunction(new RdlFunctionDefinition("InScope", typeof (FunctionAggrInScope), new RdlFunctionArg[1]
      {
        new RdlFunctionArg(true, RdlArgTypes.Scope)
      }));
      AddFunction(new RdlFunctionDefinition("CreateDrillthroughContext", typeof (CreateDrillthroughContext), new RdlFunctionArg[0]));
      AddFunction(new RdlFunctionDefinition("MinValue", typeof (FunctionMinValue), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(false, RdlArgTypes.Variant, true)
      }));
      AddFunction(new RdlFunctionDefinition("MaxValue", typeof (FunctionMaxValue), new RdlFunctionArg[3]
      {
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(false, RdlArgTypes.Variant, true)
      }));
      AddFunction(new RdlFunctionDefinition("Lookup", typeof (FunctionLookup), new RdlFunctionArg[4]
      {
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(true, RdlArgTypes.Scope)
      }));
      AddFunction(new RdlFunctionDefinition("LookupSet", typeof (FunctionLookupSet), new RdlFunctionArg[4]
      {
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(true, RdlArgTypes.Scope)
      }));
      AddFunction(new RdlFunctionDefinition("MultiLookup", typeof (FunctionMultiLookup), new RdlFunctionArg[4]
      {
        new RdlFunctionArg(true, RdlArgTypes.VariantArray),
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(true, RdlArgTypes.Variant),
        new RdlFunctionArg(true, RdlArgTypes.Scope)
      }));
      Fields = new RdlFieldsCollection();
      Parameters = new RdlParametersCollection();
      ReportItems = new RdlReportItemsCollection();
      DataSources = new RdlDataSourcesCollection();
      DataSets = new RdlDataSetsCollection();
      Variables = new RdlVariablesCollection();
      Globals = new RdlGlobalsCollection();
      User = new RdlUsersCollection();
    }

    private void AddAggregate(RdlFunctionDefinition functionDef)
    {
      m_rdlFunctions.Add(functionDef.Name, functionDef);
    }

    private void AddFunction(RdlFunctionDefinition functionDef)
    {
      m_rdlFunctions.Add(functionDef.Name, functionDef);
    }

    internal bool TryMatchRdlFunction(string name, out RdlFunctionDefinition functionDef)
    {
      return m_rdlFunctions.TryGetValue(name, out functionDef);
    }
  }
}
