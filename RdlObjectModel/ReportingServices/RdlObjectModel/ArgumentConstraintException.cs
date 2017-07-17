using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [Serializable]
  public class ArgumentConstraintException : ArgumentException
  {
	  public object Component { get; }

	  public string Property { get; }

	  public object Value { get; }

	  public object Constraint { get; }

	  public ArgumentConstraintException(object component, string property, object value, object constraint, string message)
      : base(message, property)
    {
      Component = component;
      Property = property;
      Value = value;
      Constraint = constraint;
    }

    protected ArgumentConstraintException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
