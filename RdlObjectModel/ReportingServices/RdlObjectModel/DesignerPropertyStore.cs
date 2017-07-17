using System;
using System.Collections;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	internal sealed class DesignerPropertyStore : IPropertyStore
	{
		private readonly IPropertyStore m_propertyStore;

		public ReportObject Owner => m_propertyStore.Owner;

		public IContainedObject Parent
		{
			get
			{
				return m_propertyStore.Parent;
			}
			set
			{
				m_propertyStore.Parent = value;
			}
		}

		internal DesignerPropertyStore(IPropertyStore propertyStore)
		{
			m_propertyStore = propertyStore;
		}

		public void RemoveProperty(int propertyIndex)
		{
			m_propertyStore.RemoveProperty(propertyIndex);
		}

		public object GetObject(int propertyIndex)
		{
			return m_propertyStore.GetObject(propertyIndex);
		}

		public T GetObject<T>(int propertyIndex)
		{
			return m_propertyStore.GetObject<T>(propertyIndex);
		}

		public void RemoveObject(int propertyIndex)
		{
			m_propertyStore.RemoveObject(propertyIndex);
		}

		public bool ContainsObject(int propertyIndex)
		{
			return m_propertyStore.ContainsObject(propertyIndex);
		}

		public int GetInteger(int propertyIndex)
		{
			return m_propertyStore.GetInteger(propertyIndex);
		}

		public void RemoveInteger(int propertyIndex)
		{
			m_propertyStore.RemoveInteger(propertyIndex);
		}

		public bool ContainsInteger(int propertyIndex)
		{
			return m_propertyStore.ContainsInteger(propertyIndex);
		}

		public bool GetBoolean(int propertyIndex)
		{
			return m_propertyStore.GetBoolean(propertyIndex);
		}

		public void RemoveBoolean(int propertyIndex)
		{
			m_propertyStore.RemoveBoolean(propertyIndex);
		}

		public bool ContainsBoolean(int propertyIndex)
		{
			return m_propertyStore.ContainsBoolean(propertyIndex);
		}

		public ReportSize GetSize(int propertyIndex)
		{
			return m_propertyStore.GetSize(propertyIndex);
		}

		public void RemoveSize(int propertyIndex)
		{
			m_propertyStore.RemoveSize(propertyIndex);
		}

		public bool ContainsSize(int propertyIndex)
		{
			return m_propertyStore.ContainsSize(propertyIndex);
		}

		public void IterateObjectEntries(VisitPropertyObject visitObject)
		{
			m_propertyStore.IterateObjectEntries(visitObject);
		}

		public void SetObject(int propertyIndex, object value)
		{
			SetObjectCore(propertyIndex, value, true);
		}

		public void SetInteger(int propertyIndex, int value)
		{
			SetIntegerCore(propertyIndex, value, true);
		}

		public void SetBoolean(int propertyIndex, bool value)
		{
			SetBooleanCore(propertyIndex, value, true);
		}

		public void SetSize(int propertyIndex, ReportSize value)
		{
			SetSizeCore(propertyIndex, value, true);
		}

		private object SetObjectCore(int propertyIndex, object value, bool addUndoCommand)
		{
			CheckObjectValue(value);
			object instance = m_propertyStore.GetObject(propertyIndex);
			Type type = value != null ? value.GetType() : null;
			if (instance == null && type != null && type.IsValueType)
				instance = Activator.CreateInstance(type);
			m_propertyStore.SetObject(propertyIndex, value);
			return instance;
		}

		private int SetIntegerCore(int propertyIndex, int value, bool addUndoCommand)
		{
			int integer = m_propertyStore.GetInteger(propertyIndex);
			m_propertyStore.SetInteger(propertyIndex, value);
			return integer;
		}

		private bool SetBooleanCore(int propertyIndex, bool value, bool addUndoCommand)
		{
			bool boolean = m_propertyStore.GetBoolean(propertyIndex);
			m_propertyStore.SetBoolean(propertyIndex, value);
			return boolean;
		}

		private ReportSize SetSizeCore(int propertyIndex, ReportSize value, bool addUndoCommand)
		{
			ReportSize size = m_propertyStore.GetSize(propertyIndex);
			m_propertyStore.SetSize(propertyIndex, value);
			return size;
		}

		private void RaisePropertyChanged(int propertyIndex, object oldValue, object newValue)
		{
			m_propertyStore.Owner.OnPropertyChanged(propertyIndex, oldValue, newValue);
		}

		private void CheckObjectValue(object value)
		{
			if (!(value is ICollection))
				return;
			value.GetType();
		}

		private bool CheckIfRdlCollectionType(Type type)
		{
			for (; type != (Type)null; type = type.BaseType)
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(RdlCollection<>))
					return true;
			}
			return false;
		}

		private abstract class PropertyChangeCommand<T> 
		{
			protected readonly DesignerPropertyStore m_propertyStore;
			protected readonly int m_propertyIndex;
			private T m_value;

			public string Description
			{
				get
				{
					string propertyId = StringUtil.FormatInvariant("Property {0}", (object)m_propertyIndex);
					Type nestedType1 = m_propertyStore.Owner.GetType().GetNestedType("Definition");
					if (nestedType1 != null)
					{
						Type nestedType2 = nestedType1.GetNestedType("Properties");
						if (nestedType2 != null && nestedType2.IsEnum)
							propertyId = Enum.GetName(nestedType2, m_propertyIndex);
					}
					ReportItem owner = m_propertyStore.Owner as ReportItem;
					return string.Format(SRRdl.PropertyChangeCommand, owner != null ? owner.Name : m_propertyStore.Owner.GetType().Name, propertyId);
				}
			}

			internal PropertyChangeCommand(DesignerPropertyStore propertyStore, int propertyIndex, T originalValue)
			{
				m_propertyStore = propertyStore;
				m_propertyIndex = propertyIndex;
				m_value = originalValue;
			}

			public void Execute()
			{
				m_value = SetValue(m_value);
			}

			public void Unexecute()
			{
				Execute();
			}

			protected abstract T SetValue(T value);
		}

		private sealed class ObjectPropertyChangeCommand : PropertyChangeCommand<object>
		{
			internal ObjectPropertyChangeCommand(DesignerPropertyStore propertyStore, int propertyIndex, object originalValue)
				: base(propertyStore, propertyIndex, originalValue)
			{
			}

			protected override object SetValue(object value)
			{
				return m_propertyStore.SetObjectCore(m_propertyIndex, value, false);
			}
		}

		private sealed class IntegerPropertyChangeCommand : PropertyChangeCommand<int>
		{
			internal IntegerPropertyChangeCommand(DesignerPropertyStore propertyStore, int propertyIndex, int originalValue)
				: base(propertyStore, propertyIndex, originalValue)
			{
			}

			protected override int SetValue(int value)
			{
				return m_propertyStore.SetIntegerCore(m_propertyIndex, value, false);
			}
		}

		private sealed class BooleanPropertyChangeCommand : PropertyChangeCommand<bool>
		{
			internal BooleanPropertyChangeCommand(DesignerPropertyStore propertyStore, int propertyIndex, bool originalValue)
				: base(propertyStore, propertyIndex, originalValue)
			{
			}

			protected override bool SetValue(bool value)
			{
				return m_propertyStore.SetBooleanCore(m_propertyIndex, value, false);
			}
		}

		private sealed class SizePropertyChangeCommand : PropertyChangeCommand<ReportSize>
		{
			internal SizePropertyChangeCommand(DesignerPropertyStore propertyStore, int propertyIndex, ReportSize originalValue)
				: base(propertyStore, propertyIndex, originalValue)
			{
			}

			protected override ReportSize SetValue(ReportSize value)
			{
				return m_propertyStore.SetSizeCore(m_propertyIndex, value, false);
			}
		}
	}

}
