using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Report : ReportObject, IDataScopeService, IDataScope, IContainedObject, IReportData
  {
    public const string DefaultFontFamilyDefault = "Arial";
    private SizeTypes m_reportUnitType;
	  private Guid m_reportID;

    [XmlAttribute]
    [DefaultValue("")]
    public string MustUnderstand
    {
      get
      {
        return (string) PropertyStore.GetObject(24);
      }
      set
      {
        PropertyStore.SetObject(24, value);
      }
    }

    [DefaultValue("")]
    public string Description
    {
      get
      {
        return (string) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition/defaultfontfamily")]
    [DefaultValue("Arial")]
    public string DefaultFontFamily
    {
      get
      {
        return (string) PropertyStore.GetObject(23);
      }
      set
      {
        PropertyStore.SetObject(23, value);
      }
    }

    [DefaultValue("")]
    public string Author
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

    public ReportExpression<int> AutoRefresh
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression InitialPageName
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(21);
      }
      set
      {
        PropertyStore.SetObject(21, value);
      }
    }

    [XmlElement(typeof (RdlCollection<DataSource>))]
    public IList<DataSource> DataSources
    {
      get
      {
        return (IList<DataSource>) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [XmlElement(typeof (RdlCollection<DataSet>))]
    public IList<DataSet> DataSets
    {
      get
      {
        return (IList<DataSet>) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [XmlIgnore]
    public virtual Body Body
    {
      get
      {
        return GetFirstSection("Body").Body;
      }
      set
      {
        GetFirstSection("Body").Body = value;
      }
    }

    [XmlElement(typeof (RdlCollection<ReportSection>))]
    public IList<ReportSection> ReportSections
    {
      get
      {
        return (IList<ReportSection>) PropertyStore.GetObject(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ReportParameter>))]
    public IList<ReportParameter> ReportParameters
    {
      get
      {
        return (IList<ReportParameter>) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [XmlElement(typeof (ReportParametersLayout))]
    public ReportParametersLayout ReportParametersLayout
    {
      get
      {
        return PropertyStore.GetObject<ReportParametersLayout>(22);
      }
      set
      {
        PropertyStore.SetObject(22, value);
      }
    }

    [XmlElement(typeof (RdlCollection<CustomProperty>))]
    public IList<CustomProperty> CustomProperties
    {
      get
      {
        return (IList<CustomProperty>) PropertyStore.GetObject(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [DefaultValue("")]
    public string Code
    {
      get
      {
        return (string) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [XmlIgnore]
    public virtual ReportSize Width
    {
      get
      {
        return GetFirstSection("Width").Width;
      }
      set
      {
        GetFirstSection("Width").Width = value;
      }
    }

    [XmlIgnore]
    public virtual Page Page
    {
      get
      {
        return GetFirstSection("Page").Page;
      }
      set
      {
        GetFirstSection("Page").Page = value;
      }
    }

    [XmlElement(typeof (RdlCollection<EmbeddedImage>))]
    public IList<EmbeddedImage> EmbeddedImages
    {
      get
      {
        return (IList<EmbeddedImage>) PropertyStore.GetObject(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Language
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [XmlArrayItem("CodeModule", typeof (string))]
    [XmlElement(typeof (RdlCollection<string>))]
    public IList<string> CodeModules
    {
      get
      {
        return (IList<string>) PropertyStore.GetObject(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [XmlElement(typeof (RdlCollection<Class>))]
    public IList<Class> Classes
    {
      get
      {
        return (IList<Class>) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [XmlElement(typeof (RdlCollection<Variable>))]
    public IList<Variable> Variables
    {
      get
      {
        return (IList<Variable>) PropertyStore.GetObject(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [DefaultValue(false)]
    public bool DeferVariableEvaluation
    {
      get
      {
        return PropertyStore.GetBoolean(14);
      }
      set
      {
        PropertyStore.SetBoolean(14, value);
      }
    }

    [DefaultValue(false)]
    public bool ConsumeContainerWhitespace
    {
      get
      {
        return PropertyStore.GetBoolean(15);
      }
      set
      {
        PropertyStore.SetBoolean(15, value);
      }
    }

    [DefaultValue("")]
    public string DataTransform
    {
      get
      {
        return (string) PropertyStore.GetObject(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [DefaultValue("")]
    public string DataSchema
    {
      get
      {
        return (string) PropertyStore.GetObject(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(18);
      }
      set
      {
        PropertyStore.SetObject(18, value);
      }
    }

    [DefaultValue(DataElementStyles.Attribute)]
    [ValidEnumValues("ReportDataElementOutputTypes")]
    public DataElementStyles DataElementStyle
    {
      get
      {
        return (DataElementStyles) PropertyStore.GetInteger(19);
      }
      set
      {
        ((EnumProperty) DefinitionStore<Report, Definition.Properties>.GetProperty(19)).Validate(this, (int) value);
        PropertyStore.SetInteger(19, (int) value);
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public SizeTypes ReportUnitType
    {
      get
      {
        return m_reportUnitType;
      }
      set
      {
        if (m_reportUnitType == value)
          return;
        SavePropertyValue("ReportUnitType", m_reportUnitType, (SizeTypes newValue, out SizeTypes oldValue) =>
        {
	        oldValue = m_reportUnitType;
	        m_reportUnitType = newValue;
        });
        m_reportUnitType = value;
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue(false)]
    public bool ReportTemplate { get; set; }

	  [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue("")]
    public string ReportServerUrl { get; set; }

	  [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue("")]
    public string ExpressionDialog { get; set; }

	  [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public Guid ReportID
    {
      get
      {
        if (m_reportID == Guid.Empty)
          m_reportID = Guid.NewGuid();
        return m_reportID;
      }
      set
      {
        m_reportID = value;
      }
    }

    [XmlChildAttribute("Description", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string DescriptionLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    string IDataScope.Name => null;

	  Group IDataScope.Group => null;

	  public Report()
    {
    }

    internal Report(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
	  ReportSections = new RdlCollection<ReportSection> {new ReportSection()};
	  DataSources = new RdlCollection<DataSource>();
      DataSets = new RdlCollection<DataSet>();
      ReportParameters = new RdlCollection<ReportParameter>();
      ReportParametersLayout = new ReportParametersLayout();
      CustomProperties = new RdlCollection<CustomProperty>();
      EmbeddedImages = new RdlCollection<EmbeddedImage>();
      CodeModules = new RdlCollection<string>();
      Classes = new RdlCollection<Class>();
      Variables = new RdlCollection<Variable>();
      DataElementStyle = DataElementStyles.Attribute;
    }

    private ReportSection GetFirstSection(string propertyName)
    {
      IList<ReportSection> reportSections = ReportSections;
      if (reportSections == null || reportSections.Count == 0)
        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Cannot access {0} when no sections are defined", new object[1]
        {
          propertyName
        }));
      return reportSections[0];
    }

    public DataSet GetDataSetByName(string name)
    {
      foreach (DataSet dataSet in DataSets)
      {
        if (StringUtil.CompareClsCompliantIdentifiers(dataSet.Name, name) == 0)
          return dataSet;
      }
      return null;
    }

    public DataSource GetDataSourceByName(string name)
    {
      foreach (DataSource dataSource in DataSources)
      {
        if (StringUtil.CompareClsCompliantIdentifiers(dataSource.Name, name) == 0)
          return dataSource;
      }
      return null;
    }

    public EmbeddedImage GetEmbeddedImageByName(string name)
    {
      foreach (EmbeddedImage embeddedImage in EmbeddedImages)
      {
        if (StringUtil.CompareClsCompliantIdentifiers(embeddedImage.Name, name) == 0)
          return embeddedImage;
      }
      return null;
    }

    public ReportParameter GetReportParameterByName(string name)
    {
      foreach (ReportParameter reportParameter in ReportParameters)
      {
        if (StringUtil.CompareClsCompliantIdentifiers(reportParameter.Name, name) == 0)
          return reportParameter;
      }
      return null;
    }

    public static Report Load(string path)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentNullException("path");
      using (FileStream fileStream = File.OpenRead(path))
        return Load(fileStream);
    }

    public static Report Load(byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException("bytes");
      using (MemoryStream memoryStream = new MemoryStream(bytes))
        return Load(memoryStream);
    }

    public static Report Load(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException("stream");
      return new RdlSerializer().Deserialize(stream);
    }

    public IEnumerable<DataSet> GetDataSetsByDataSourceName(string dataSourceName)
    {
      foreach (DataSet dataSet in DataSets)
      {
        if (dataSet.Query != null && StringUtil.CompareClsCompliantIdentifiers(dataSet.Query.DataSourceName, dataSourceName) == 0)
          yield return dataSet;
      }
    }

    public static string GetClsCompliantIdentifier(string candidate)
    {
      return StringUtil.GetClsCompliantIdentifier(candidate, "ID");
    }

    public string GenerateDataSetName(string candidate)
    {
      return GenerateItemName(candidate, new Converter<string, DataSet>(GetDataSetByName));
    }

    public string GenerateItemName<T>(string candidate, Converter<string, T> getItemByName)
    {
      return GenerateItemName(candidate, typeof (T).Name, itemName => (object) getItemByName(itemName) != null);
    }

    public string GenerateItemName(string candidate, string baseName, Predicate<string> itemExists)
    {
      if (!string.IsNullOrEmpty(candidate))
        candidate = GetClsCompliantIdentifier(candidate);
      return StringUtil.GenerateUniqueName(candidate, baseName, itemExists);
    }

    public IEnumerable<IDataScope> GetDataScopesFor(IContainedObject obj)
    {
      if (!IsChildOf(obj, this))
        throw new InvalidOperationException();
      if (obj is DataRegion)
      {
        string dataSetName = ((DataRegion) obj).DataSetName;
        IDataScope dataSetScope = GetDataSetByName(dataSetName);
        if (dataSetScope != null)
        {
          yield return dataSetScope;
          yield break;
        }
      }
      if (!(obj is DataSet) && DataSets != null && DataSets.Count == 1)
      {
        yield return DataSets[0];
      }
      else
      {
        foreach (IDataScope dataScope in GetDataScopesForDefaultImpl(obj))
          yield return dataScope;
      }
    }

    internal class Definition : DefinitionStore<Report, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Description,
        DescriptionLocID,
        Author,
        AutoRefresh,
        DataSources,
        DataSets,
        ReportParameters,
        CustomProperties,
        Code,
        EmbeddedImages,
        Language,
        CodeModules,
        Classes,
        Variables,
        DeferVariableEvaluation,
        ConsumeContainerWhitespace,
        DataTransform,
        DataSchema,
        DataElementName,
        DataElementStyle,
        ReportSections,
        InitialPageName,
        ReportParametersLayout,
        DefaultFontFamily,
        MustUnderstand,
        PropertyCount,
      }
    }
  }
}
