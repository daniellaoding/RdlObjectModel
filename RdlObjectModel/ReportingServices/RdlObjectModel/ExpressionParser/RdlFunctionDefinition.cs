using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class RdlFunctionDefinition
  {
	  internal Type NodeType { get; }

	  internal string Name { get; }

	  internal RdlFunctionArg[] Args { get; }

	  internal RdlFunctionDefinition(string name, Type nodeType, params RdlFunctionArg[] args)
    {
      Name = name;
      NodeType = nodeType;
      Args = args;
    }
  }
}
