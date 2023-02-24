using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace KSerialization
{
	// Token: 0x02000508 RID: 1288
	public class SerializationTemplate
	{
		// Token: 0x0600372B RID: 14123 RVA: 0x0007AE7C File Offset: 0x0007907C
		private MemberSerialization GetSerializationConfig(Type type)
		{
			MemberSerialization memberSerialization = MemberSerialization.Invalid;
			Type type2 = null;
			while (type != typeof(object))
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(SerializationConfig), false);
				int i = 0;
				while (i < customAttributes.Length)
				{
					Attribute attribute = (Attribute)customAttributes[i];
					if (attribute is SerializationConfig)
					{
						SerializationConfig serializationConfig = attribute as SerializationConfig;
						if (serializationConfig.MemberSerialization != memberSerialization && memberSerialization != MemberSerialization.Invalid)
						{
							string text = "Found conflicting serialization configurations on type " + type2.ToString() + " and " + type.ToString();
							Debug.LogError(text);
							throw new ArgumentException(text);
						}
						memberSerialization = serializationConfig.MemberSerialization;
						type2 = type.BaseType;
						break;
					}
					else
					{
						i++;
					}
				}
				type = type.BaseType;
			}
			if (memberSerialization == MemberSerialization.Invalid)
			{
				memberSerialization = MemberSerialization.OptOut;
			}
			return memberSerialization;
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x0007AF38 File Offset: 0x00079138
		public SerializationTemplate(Type type)
		{
			this.serializableType = type;
			this.typeInfo = Manager.GetTypeInfo(type);
			type.GetSerializationMethods(typeof(OnSerializingAttribute), typeof(OnSerializedAttribute), typeof(CustomSerialize), out this.onSerializing, out this.onSerialized, out this.customSerialize);
			MemberSerialization serializationConfig = this.GetSerializationConfig(type);
			if (serializationConfig == MemberSerialization.OptOut)
			{
				while (type != typeof(object))
				{
					this.AddPublicFields(type);
					this.AddPublicProperties(type);
					type = type.BaseType;
				}
				return;
			}
			if (serializationConfig != MemberSerialization.OptIn)
			{
				return;
			}
			while (type != typeof(object))
			{
				this.AddOptInFields(type);
				this.AddOptInProperties(type);
				type = type.BaseType;
			}
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x0007B00C File Offset: 0x0007920C
		public override string ToString()
		{
			string text = "Template: " + this.serializableType.ToString() + "\n";
			foreach (SerializationTemplate.SerializationField serializationField in this.serializableFields)
			{
				text = text + "\t" + serializationField.ToString() + "\n";
			}
			return text;
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x0007B094 File Offset: 0x00079294
		private void AddPublicFields(Type type)
		{
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
			{
				this.AddValidField(fieldInfo);
			}
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x0007B0C4 File Offset: 0x000792C4
		private void AddOptInFields(Type type)
		{
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				foreach (object obj in fieldInfo.GetCustomAttributes(false))
				{
					if (obj != null && obj is Serialize)
					{
						this.AddValidField(fieldInfo);
					}
				}
			}
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x0007B120 File Offset: 0x00079320
		private void AddValidField(FieldInfo field)
		{
			object[] customAttributes = field.GetCustomAttributes(typeof(NonSerializedAttribute), false);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				this.serializableFields.Add(new SerializationTemplate.SerializationField
				{
					field = field,
					typeInfo = Manager.GetTypeInfo(field.FieldType)
				});
			}
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x0007B17C File Offset: 0x0007937C
		private void AddPublicProperties(Type type)
		{
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
			{
				this.AddValidProperty(propertyInfo);
			}
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x0007B1AC File Offset: 0x000793AC
		private void AddOptInProperties(Type type)
		{
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				foreach (object obj in propertyInfo.GetCustomAttributes(false))
				{
					if (obj != null && obj is Serialize)
					{
						this.AddValidProperty(propertyInfo);
					}
				}
			}
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x0007B208 File Offset: 0x00079408
		private void AddValidProperty(PropertyInfo property)
		{
			if (property.GetIndexParameters().Length != 0)
			{
				return;
			}
			object[] customAttributes = property.GetCustomAttributes(typeof(NonSerializedAttribute), false);
			if ((customAttributes == null || customAttributes.Length == 0) && property.GetSetMethod() != null)
			{
				this.serializableProperties.Add(new SerializationTemplate.SerializationProperty
				{
					property = property,
					typeInfo = Manager.GetTypeInfo(property.PropertyType)
				});
			}
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x0007B27C File Offset: 0x0007947C
		public void SerializeTemplate(BinaryWriter writer)
		{
			writer.Write(this.serializableFields.Count);
			writer.Write(this.serializableProperties.Count);
			foreach (SerializationTemplate.SerializationField serializationField in this.serializableFields)
			{
				writer.WriteKleiString(serializationField.field.Name);
				Type fieldType = serializationField.field.FieldType;
				this.WriteType(writer, fieldType);
			}
			foreach (SerializationTemplate.SerializationProperty serializationProperty in this.serializableProperties)
			{
				writer.WriteKleiString(serializationProperty.property.Name);
				Type propertyType = serializationProperty.property.PropertyType;
				this.WriteType(writer, propertyType);
			}
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x0007B374 File Offset: 0x00079574
		private void WriteType(BinaryWriter writer, Type type)
		{
			SerializationTypeInfo serializationTypeInfo = Helper.EncodeSerializationType(type);
			writer.Write((byte)serializationTypeInfo);
			if (type.IsGenericType)
			{
				if (Helper.IsUserDefinedType(serializationTypeInfo))
				{
					writer.WriteKleiString(type.GetKTypeString());
				}
				Type[] genericArguments = type.GetGenericArguments();
				writer.Write((byte)genericArguments.Length);
				for (int i = 0; i < genericArguments.Length; i++)
				{
					this.WriteType(writer, genericArguments[i]);
				}
				return;
			}
			if (Helper.IsArray(serializationTypeInfo))
			{
				Type elementType = type.GetElementType();
				this.WriteType(writer, elementType);
				return;
			}
			if (type.IsEnum || Helper.IsUserDefinedType(serializationTypeInfo))
			{
				writer.WriteKleiString(type.GetKTypeString());
			}
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x0007B40C File Offset: 0x0007960C
		public void SerializeData(object obj, BinaryWriter writer)
		{
			if (this.onSerializing != null)
			{
				this.onSerializing.Invoke(obj, null);
			}
			foreach (SerializationTemplate.SerializationField serializationField in this.serializableFields)
			{
				try
				{
					object value = serializationField.field.GetValue(obj);
					writer.WriteValue(serializationField.typeInfo, value);
				}
				catch (Exception ex)
				{
					string text = string.Format("Error occurred while serializing field {0} on template {1}", serializationField.field.Name, this.serializableType.Name);
					Debug.LogError(text);
					throw new ArgumentException(text, ex);
				}
			}
			foreach (SerializationTemplate.SerializationProperty serializationProperty in this.serializableProperties)
			{
				try
				{
					object value2 = serializationProperty.property.GetValue(obj, null);
					writer.WriteValue(serializationProperty.typeInfo, value2);
				}
				catch (Exception ex2)
				{
					string text2 = string.Format("Error occurred while serializing property {0} on template {1}", serializationProperty.property.Name, this.serializableType.Name);
					Debug.LogError(text2);
					throw new ArgumentException(text2, ex2);
				}
			}
			if (this.customSerialize != null)
			{
				this.customSerialize.Invoke(obj, new object[] { writer });
			}
			if (this.onSerialized != null)
			{
				this.onSerialized.Invoke(obj, null);
			}
		}

		// Token: 0x040013E7 RID: 5095
		public Type serializableType;

		// Token: 0x040013E8 RID: 5096
		public TypeInfo typeInfo;

		// Token: 0x040013E9 RID: 5097
		public List<SerializationTemplate.SerializationField> serializableFields = new List<SerializationTemplate.SerializationField>();

		// Token: 0x040013EA RID: 5098
		public List<SerializationTemplate.SerializationProperty> serializableProperties = new List<SerializationTemplate.SerializationProperty>();

		// Token: 0x040013EB RID: 5099
		public MethodInfo onSerializing;

		// Token: 0x040013EC RID: 5100
		public MethodInfo onSerialized;

		// Token: 0x040013ED RID: 5101
		public MethodInfo customSerialize;

		// Token: 0x02000B1B RID: 2843
		public struct SerializationField
		{
			// Token: 0x0400262C RID: 9772
			public FieldInfo field;

			// Token: 0x0400262D RID: 9773
			public TypeInfo typeInfo;
		}

		// Token: 0x02000B1C RID: 2844
		public struct SerializationProperty
		{
			// Token: 0x0400262E RID: 9774
			public PropertyInfo property;

			// Token: 0x0400262F RID: 9775
			public TypeInfo typeInfo;
		}
	}
}
