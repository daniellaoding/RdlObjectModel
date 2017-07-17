namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class CodeObject : ReportObject
  {
	  public string Code { get; set; }

	  public CodeObject()
    {
    }

    public CodeObject(string code)
    {
      Code = code;
    }
  }
}
