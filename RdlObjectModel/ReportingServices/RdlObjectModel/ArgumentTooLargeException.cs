using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [Serializable]
  public class ArgumentTooLargeException : ArgumentConstraintException
  {
    public ArgumentTooLargeException(object component, string property, object value, object maximum)
      : base(component, property, value, maximum, string.Format(SRErrors.InvalidParamLessThan, property, maximum))
    {
    }

    protected ArgumentTooLargeException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
