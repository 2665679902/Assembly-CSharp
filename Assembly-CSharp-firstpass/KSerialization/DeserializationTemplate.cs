using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;

namespace KSerialization
{
	// Token: 0x02000509 RID: 1289
	public class DeserializationTemplate
	{
		// Token: 0x06003737 RID: 14135 RVA: 0x0007B5A8 File Offset: 0x000797A8
		public DeserializationTemplate(string template_type_name, IReader reader)
		{
			this.typeName = template_type_name;
			DebugLog.Output(DebugLog.Level.Info, "Loading Deserialization Template: " + template_type_name);
			Type type = Manager.GetType(template_type_name);
			if (type != null)
			{
				type.GetSerializationMethods(typeof(OnDeserializingAttribute), typeof(OnDeserializedAttribute), typeof(CustomDeserialize), out this.onDeserializing, out this.onDeserialized, out this.customDeserialize);
			}
			int num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				DebugLog.Output(DebugLog.Level.Info, "Field " + i.ToString());
				string text = reader.ReadKleiString();
				DebugLog.Output(DebugLog.Level.Info, "Field " + i.ToString() + " == " + text);
				TypeInfo typeInfo = this.ReadType(reader);
				if (typeInfo.type == null)
				{
					string text2 = string.Format("Unknown type encountered while dserializing template {0} field {1} ({2}) at offset {3}", new object[] { template_type_name, i, text, reader.Position });
					DebugLog.Output(DebugLog.Level.Warning, text2);
				}
				this.serializedMembers.Add(new DeserializationTemplate.SerializedInfo
				{
					name = text,
					typeInfo = typeInfo
				});
			}
			for (int j = 0; j < num2; j++)
			{
				DebugLog.Output(DebugLog.Level.Info, "Property " + j.ToString());
				string text3 = reader.ReadKleiString();
				DebugLog.Output(DebugLog.Level.Info, "Property " + j.ToString() + " == " + text3);
				TypeInfo typeInfo2 = this.ReadType(reader);
				if (typeInfo2.type == null)
				{
					string text4 = string.Format("Unknown type encountered while dserializing template {0} property {1} ({2}) at offset {3}", new object[] { template_type_name, j, text3, reader.Position });
					DebugLog.Output(DebugLog.Level.Info, text4);
				}
				this.serializedMembers.Add(new DeserializationTemplate.SerializedInfo
				{
					name = text3,
					typeInfo = typeInfo2
				});
			}
			DebugLog.Output(DebugLog.Level.Info, "Finished loading template " + template_type_name);
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x0007B7D8 File Offset: 0x000799D8
		private TypeInfo ReadType(IReader reader)
		{
			TypeInfo typeInfo = new TypeInfo();
			byte b = reader.ReadByte();
			typeInfo.info = (SerializationTypeInfo)b;
			SerializationTypeInfo serializationTypeInfo = typeInfo.info & SerializationTypeInfo.VALUE_MASK;
			if (!Helper.IsGenericType(typeInfo.info))
			{
				switch (serializationTypeInfo)
				{
				case SerializationTypeInfo.UserDefined:
				case SerializationTypeInfo.Enumeration:
				{
					string text = reader.ReadKleiString();
					typeInfo.type = Manager.GetType(text);
					return typeInfo;
				}
				case SerializationTypeInfo.SByte:
					typeInfo.type = typeof(sbyte);
					return typeInfo;
				case SerializationTypeInfo.Byte:
					typeInfo.type = typeof(byte);
					return typeInfo;
				case SerializationTypeInfo.Boolean:
					typeInfo.type = typeof(bool);
					return typeInfo;
				case SerializationTypeInfo.Int16:
					typeInfo.type = typeof(short);
					return typeInfo;
				case SerializationTypeInfo.UInt16:
					typeInfo.type = typeof(ushort);
					return typeInfo;
				case SerializationTypeInfo.Int32:
					typeInfo.type = typeof(int);
					return typeInfo;
				case SerializationTypeInfo.UInt32:
					typeInfo.type = typeof(uint);
					return typeInfo;
				case SerializationTypeInfo.Int64:
					typeInfo.type = typeof(long);
					return typeInfo;
				case SerializationTypeInfo.UInt64:
					typeInfo.type = typeof(ulong);
					return typeInfo;
				case SerializationTypeInfo.Single:
					typeInfo.type = typeof(float);
					return typeInfo;
				case SerializationTypeInfo.Double:
					typeInfo.type = typeof(double);
					return typeInfo;
				case SerializationTypeInfo.String:
					typeInfo.type = typeof(string);
					return typeInfo;
				case SerializationTypeInfo.Vector2I:
					typeInfo.type = typeof(Vector2I);
					return typeInfo;
				case SerializationTypeInfo.Vector2:
					typeInfo.type = typeof(Vector2);
					return typeInfo;
				case SerializationTypeInfo.Vector3:
					typeInfo.type = typeof(Vector3);
					return typeInfo;
				case SerializationTypeInfo.Array:
					typeInfo.subTypes = new TypeInfo[1];
					typeInfo.subTypes[0] = this.ReadType(reader);
					if (typeInfo.subTypes[0].type != null)
					{
						typeInfo.type = typeInfo.subTypes[0].type.MakeArrayType();
						return typeInfo;
					}
					typeInfo.type = null;
					return typeInfo;
				case SerializationTypeInfo.Colour:
					typeInfo.type = typeof(Color);
					return typeInfo;
				}
				throw new ArgumentException("unknown type");
			}
			Type type = null;
			if (serializationTypeInfo != SerializationTypeInfo.UserDefined)
			{
				switch (serializationTypeInfo)
				{
				case SerializationTypeInfo.Pair:
					type = typeof(KeyValuePair<, >);
					break;
				case SerializationTypeInfo.Dictionary:
					type = typeof(Dictionary<, >);
					break;
				case SerializationTypeInfo.List:
					type = typeof(List<>);
					break;
				case SerializationTypeInfo.HashSet:
					type = typeof(HashSet<>);
					break;
				case SerializationTypeInfo.Queue:
					type = typeof(Queue<>);
					break;
				default:
					throw new ArgumentException("unknown type");
				}
			}
			else
			{
				string text2 = reader.ReadKleiString();
				typeInfo.type = Manager.GetType(text2);
			}
			byte b2 = reader.ReadByte();
			Type[] array = new Type[(int)b2];
			typeInfo.subTypes = new TypeInfo[(int)b2];
			for (int i = 0; i < (int)b2; i++)
			{
				typeInfo.subTypes[i] = this.ReadType(reader);
				array[i] = typeInfo.subTypes[i].type;
			}
			if (type != null)
			{
				if (array == null || Array.IndexOf<Type>(array, null) != -1)
				{
					typeInfo.type = null;
					return typeInfo;
				}
				typeInfo.type = type.MakeGenericType(array);
			}
			else if (typeInfo.type != null)
			{
				Type[] genericArguments = typeInfo.type.GetGenericArguments();
				if (genericArguments.Length != (int)b2)
				{
					throw new InvalidOperationException("User defined generic type mismatch");
				}
				for (int j = 0; j < (int)b2; j++)
				{
					if (array[j] != genericArguments[j])
					{
						throw new InvalidOperationException("User defined generic type mismatch");
					}
				}
			}
			return typeInfo;
		}

		// Token: 0x040013EE RID: 5102
		public string typeName;

		// Token: 0x040013EF RID: 5103
		public MethodInfo onDeserializing;

		// Token: 0x040013F0 RID: 5104
		public MethodInfo onDeserialized;

		// Token: 0x040013F1 RID: 5105
		public MethodInfo customDeserialize;

		// Token: 0x040013F2 RID: 5106
		public List<DeserializationTemplate.SerializedInfo> serializedMembers = new List<DeserializationTemplate.SerializedInfo>();

		// Token: 0x02000B1D RID: 2845
		public struct SerializedInfo
		{
			// Token: 0x04002630 RID: 9776
			public string name;

			// Token: 0x04002631 RID: 9777
			public TypeInfo typeInfo;
		}
	}
}
