using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class DataSource : ReportObject, INamedObject
  {
    private DataSourceCredentialRetrievalEnum m_credentialRetrievalEnum = DataSourceCredentialRetrievalEnum.None;
    private bool m_shared;
    private bool m_windowsCredentials;
    private Guid m_dataSourceID;
    private bool m_shouldSaveCredentials;
    private bool m_isModified;
    private bool m_isModifiedSincePreview;
    private bool m_credentialsInitialized;
    private bool m_showHiddenDataSets;
    private bool m_model;
    private bool m_impersonateUser;

    [XmlAttribute]
    public string Name
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

    [DefaultValue(false)]
    public bool Transaction
    {
      get
      {
        return PropertyStore.GetBoolean(1);
      }
      set
      {
        PropertyStore.SetBoolean(1, value);
      }
    }

    public ConnectionProperties ConnectionProperties
    {
      get
      {
        return (ConnectionProperties) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [DefaultValue("")]
    public string DataSourceReference
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

    [DefaultValue(SecurityTypeEnum.Unknown)]
    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public SecurityTypeEnum SecurityType
    {
      get
      {
        if (m_shared)
          return SecurityTypeEnum.Unknown;
        if (m_credentialRetrievalEnum == DataSourceCredentialRetrievalEnum.Integrated)
          return SecurityTypeEnum.Integrated;
        if (m_credentialRetrievalEnum == DataSourceCredentialRetrievalEnum.Prompt || m_credentialRetrievalEnum == DataSourceCredentialRetrievalEnum.Store)
          return m_windowsCredentials ? SecurityTypeEnum.Windows : SecurityTypeEnum.DataBase;
        return m_credentialRetrievalEnum == DataSourceCredentialRetrievalEnum.None ? SecurityTypeEnum.None : SecurityTypeEnum.Unknown;
      }
      set
      {
        m_windowsCredentials = false;
        switch (value)
        {
          case SecurityTypeEnum.None:
            m_credentialRetrievalEnum = DataSourceCredentialRetrievalEnum.None;
            break;
          case SecurityTypeEnum.DataBase:
            m_credentialRetrievalEnum = DataSourceCredentialRetrievalEnum.Prompt;
            break;
          case SecurityTypeEnum.Windows:
            m_credentialRetrievalEnum = DataSourceCredentialRetrievalEnum.Prompt;
            m_windowsCredentials = true;
            break;
          case SecurityTypeEnum.Integrated:
            m_credentialRetrievalEnum = DataSourceCredentialRetrievalEnum.Integrated;
            break;
        }
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public Guid DataSourceID
    {
      get
      {
        if (m_dataSourceID == Guid.Empty)
          m_dataSourceID = Guid.NewGuid();
        return m_dataSourceID;
      }
      set
      {
        m_dataSourceID = value;
      }
    }

    [XmlIgnore]
    internal DataSourceCredentialRetrievalEnum CredentialRetrievalEnum
    {
      get
      {
        return m_credentialRetrievalEnum;
      }
      set
      {
        SavePropertyValue("DataSourceCredentialRetrievalEnum", m_credentialRetrievalEnum, (DataSourceCredentialRetrievalEnum newValue, out DataSourceCredentialRetrievalEnum oldValue) =>
        {
	        oldValue = m_credentialRetrievalEnum;
	        m_credentialRetrievalEnum = newValue;
        });
        m_credentialRetrievalEnum = value;
      }
    }

    [XmlIgnore]
    public bool ImpersonateUser
    {
      get
      {
        return m_impersonateUser;
      }
      set
      {
        SavePropertyValue("ImpersonateUser", m_impersonateUser, (bool newValue, out bool oldValue) =>
        {
	        oldValue = m_impersonateUser;
	        m_impersonateUser = newValue;
        });
        m_impersonateUser = value;
      }
    }

    [XmlIgnore]
    public bool WindowsCredentials
    {
      get
      {
        return m_windowsCredentials;
      }
      set
      {
        SavePropertyValue("WindowsCredentials", m_windowsCredentials, (bool newValue, out bool oldValue) =>
        {
	        oldValue = m_windowsCredentials;
	        m_windowsCredentials = newValue;
        });
        m_windowsCredentials = value;
      }
    }
    
    public DataSource()
    {
    }

    internal DataSource(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override object DeepClone()
    {
      DataSource dataSource = (DataSource) base.DeepClone();
      dataSource.m_shouldSaveCredentials = m_shouldSaveCredentials;
      dataSource.m_isModified = m_isModified;
      dataSource.m_isModifiedSincePreview = m_isModifiedSincePreview;
      dataSource.m_credentialsInitialized = m_credentialsInitialized;
      dataSource.m_credentialRetrievalEnum = m_credentialRetrievalEnum;
      dataSource.m_shared = m_shared;
      dataSource.m_model = m_model;
      dataSource.m_showHiddenDataSets = m_showHiddenDataSets;
      dataSource.m_windowsCredentials = m_windowsCredentials;
      dataSource.m_impersonateUser = m_impersonateUser;
      return dataSource;
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      if (CheckVisitedAndUpdate(this, visitedList))
        return true;
      DataSource dataSource = rdlObj as DataSource;
      return dataSource != null && SecurityType == dataSource.SecurityType && (Transaction == dataSource.Transaction && string.Equals(DataSourceReference, dataSource.DataSourceReference, StringComparison.OrdinalIgnoreCase)) && SemanticCompare(ConnectionProperties, dataSource.ConnectionProperties, visitedList);
    }

    internal class Definition : DefinitionStore<DataSource, Definition.Properties>
    {
      internal enum Properties
      {
        Name,
        Transaction,
        ConnectionProperties,
        DataSourceReference,
      }
    }
  }
}
