using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ReportSection : ReportObject
  {
    public Body Body
    {
      get
      {
        return (Body) PropertyStore.GetObject(0);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Body");
        PropertyStore.SetObject(0, value);
      }
    }

    public ReportSize Width
    {
      get
      {
        return PropertyStore.GetSize(1);
      }
      set
      {
        PropertyStore.SetSize(1, value);
      }
    }

    public Page Page
    {
      get
      {
        return (Page) PropertyStore.GetObject(2);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Page");
        PropertyStore.SetObject(2, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ValidEnumValues("ReportItemDataElementOutputTypes")]
    [DefaultValue(DataElementOutputTypes.Auto)]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(4);
      }
      set
      {
        PropertyStore.SetInteger(4, (int) value);
      }
    }

    public ReportSection()
    {
    }

    internal ReportSection(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Width = Constants.DefaultZeroSize;
      Body = new Body();
      Page = new Page();
    }

    internal class Definition : DefinitionStore<ReportSection, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Body,
        Width,
        Page,
        DataElementName,
        DataElementOutput,
      }
    }
  }
}
