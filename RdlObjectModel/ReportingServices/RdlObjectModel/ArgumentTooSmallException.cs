using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [Serializable]
  public class ArgumentTooSmallException : ArgumentConstraintException
  {
    public ArgumentTooSmallException(object component, string property, object value, object minimum)
      : base(component, property, value, minimum, string.Format(SRErrors.InvalidParamGreaterThan,property, minimum))
    {
    }

    protected ArgumentTooSmallException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
