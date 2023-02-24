﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace KSerialization
{
	// Token: 0x0200050A RID: 1290
	public class DeserializationMapping
	{
		// Token: 0x06003739 RID: 14137 RVA: 0x0007BBA8 File Offset: 0x00079DA8
		public DeserializationMapping(DeserializationTemplate in_template, SerializationTemplate out_template)
		{
			this.template = in_template;
			foreach (DeserializationTemplate.SerializedInfo serializedInfo in in_template.serializedMembers)
			{
				DeserializationMapping.DeserializationInfo deserializationInfo = default(DeserializationMapping.DeserializationInfo);
				deserializationInfo.valid = false;
				for (int i = 0; i < out_template.serializableFields.Count; i++)
				{
					if (out_template.serializableFields[i].field.Name == serializedInfo.name)
					{
						TypeInfo typeInfo = out_template.serializableFields[i].typeInfo;
						if (serializedInfo.typeInfo.Equals(typeInfo))
						{
							deserializationInfo.field = out_template.serializableFields[i].field;
							deserializationInfo.typeInfo = serializedInfo.typeInfo;
							deserializationInfo.valid = true;
							break;
						}
					}
				}
				if (!deserializationInfo.valid)
				{
					for (int j = 0; j < out_template.serializableProperties.Count; j++)
					{
						if (out_template.serializableProperties[j].property.Name == serializedInfo.name)
						{
							TypeInfo typeInfo2 = out_template.serializableProperties[j].typeInfo;
							if (serializedInfo.typeInfo.Equals(typeInfo2))
							{
								PropertyInfo property = out_template.serializableProperties[j].property;
								deserializationInfo.property = property;
								deserializationInfo.typeInfo = serializedInfo.typeInfo;
								deserializationInfo.valid = true;
								break;
							}
						}
					}
				}
				deserializationInfo.valid = deserializationInfo.valid && deserializationInfo.typeInfo.type != null;
				if (deserializationInfo.valid)
				{
					deserializationInfo.typeInfo.BuildGenericArgs();
				}
				else
				{
					deserializationInfo.typeInfo = serializedInfo.typeInfo;
				}
				if (deserializationInfo.typeInfo.type == null)
				{
					DebugLog.Output(DebugLog.Level.Warning, string.Format("Tried to deserialize field '{0}' on type {1} but it no longer exists", serializedInfo.name, in_template.typeName));
				}
				this.deserializationInfo.Add(deserializationInfo);
			}
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x0007BDE4 File Offset: 0x00079FE4
		public bool Deserialize(object obj, IReader reader)
		{
			if (obj == null)
			{
				throw new ArgumentException("obj cannot be null");
			}
			if (this.template.onDeserializing != null)
			{
				this.template.onDeserializing.Invoke(obj, null);
			}
			foreach (DeserializationMapping.DeserializationInfo deserializationInfo in this.deserializationInfo)
			{
				if (deserializationInfo.valid)
				{
					if (deserializationInfo.field != null)
					{
						try
						{
							object value = deserializationInfo.field.GetValue(obj);
							object obj2 = this.ReadValue(deserializationInfo.typeInfo, reader, value);
							deserializationInfo.field.SetValue(obj, obj2);
							continue;
						}
						catch (Exception ex)
						{
							string text = string.Format("Exception occurred while attempting to deserialize field {0}({1}) on object {2}({3}).\n{4}", new object[]
							{
								deserializationInfo.field,
								deserializationInfo.field.FieldType,
								obj,
								obj.GetType(),
								ex.ToString()
							});
							DebugLog.Output(DebugLog.Level.Error, text);
							throw new Exception(text, ex);
						}
					}
					if (deserializationInfo.property != null)
					{
						try
						{
							object value2 = deserializationInfo.property.GetValue(obj, null);
							object obj3 = this.ReadValue(deserializationInfo.typeInfo, reader, value2);
							deserializationInfo.property.SetValue(obj, obj3, null);
							continue;
						}
						catch (Exception ex2)
						{
							string text2 = string.Format("Exception occurred while attempting to deserialize property {0}({1}) on object {2}({3}).\n{4}", new object[]
							{
								deserializationInfo.property,
								deserializationInfo.property.PropertyType,
								obj,
								obj.GetType(),
								ex2.ToString()
							});
							DebugLog.Output(DebugLog.Level.Error, text2);
							throw new Exception(text2, ex2);
						}
					}
					throw new Exception("????");
				}
				SerializationTypeInfo serializationTypeInfo = deserializationInfo.typeInfo.info & SerializationTypeInfo.VALUE_MASK;
				if (serializationTypeInfo != SerializationTypeInfo.UserDefined)
				{
					switch (serializationTypeInfo)
					{
					case SerializationTypeInfo.Array:
					case SerializationTypeInfo.Dictionary:
					case SerializationTypeInfo.List:
					case SerializationTypeInfo.HashSet:
					case SerializationTypeInfo.Queue:
					{
						int num = reader.ReadInt32();
						if (reader.ReadInt32() > -1)
						{
							reader.SkipBytes(num);
							continue;
						}
						continue;
					}
					case SerializationTypeInfo.Pair:
						break;
					default:
						this.SkipValue(serializationTypeInfo, reader);
						continue;
					}
				}
				int num2 = reader.ReadInt32();
				if (num2 > 0)
				{
					reader.SkipBytes(num2);
				}
			}
			if (this.template.customDeserialize != null)
			{
				this.template.customDeserialize.Invoke(obj, new object[] { reader });
			}
			if (this.template.onDeserialized != null)
			{
				this.template.onDeserialized.Invoke(obj, null);
			}
			return true;
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x0007C0B4 File Offset: 0x0007A2B4
		private object ReadValue(TypeInfo type_info, IReader reader, object base_value)
		{
			object obj = null;
			SerializationTypeInfo serializationTypeInfo = type_info.info & SerializationTypeInfo.VALUE_MASK;
			Type type = type_info.type;
			switch (serializationTypeInfo)
			{
			case SerializationTypeInfo.UserDefined:
				if (reader.ReadInt32() >= 0)
				{
					Type type2 = type_info.type;
					if (base_value == null)
					{
						if (type2.GetConstructor(Type.EmptyTypes) != null)
						{
							obj = Activator.CreateInstance(type2);
						}
						else
						{
							obj = FormatterServices.GetUninitializedObject(type2);
						}
					}
					else
					{
						obj = base_value;
					}
					Manager.GetDeserializationMapping(type2).Deserialize(obj, reader);
				}
				break;
			case SerializationTypeInfo.SByte:
				obj = reader.ReadSByte();
				break;
			case SerializationTypeInfo.Byte:
				obj = reader.ReadByte();
				break;
			case SerializationTypeInfo.Boolean:
				obj = reader.ReadByte() == 1;
				break;
			case SerializationTypeInfo.Int16:
				obj = reader.ReadInt16();
				break;
			case SerializationTypeInfo.UInt16:
				obj = reader.ReadUInt16();
				break;
			case SerializationTypeInfo.Int32:
				obj = reader.ReadInt32();
				break;
			case SerializationTypeInfo.UInt32:
				obj = reader.ReadUInt32();
				break;
			case SerializationTypeInfo.Int64:
				obj = reader.ReadInt64();
				break;
			case SerializationTypeInfo.UInt64:
				obj = reader.ReadUInt64();
				break;
			case SerializationTypeInfo.Single:
				obj = reader.ReadSingle();
				break;
			case SerializationTypeInfo.Double:
				obj = reader.ReadDouble();
				break;
			case SerializationTypeInfo.String:
				obj = reader.ReadKleiString();
				break;
			case SerializationTypeInfo.Enumeration:
			{
				int num = reader.ReadInt32();
				obj = Enum.ToObject(type_info.type, num);
				break;
			}
			case SerializationTypeInfo.Vector2I:
				obj = reader.ReadVector2I();
				break;
			case SerializationTypeInfo.Vector2:
				obj = reader.ReadVector2();
				break;
			case SerializationTypeInfo.Vector3:
				obj = reader.ReadVector3();
				break;
			case SerializationTypeInfo.Array:
			{
				reader.ReadInt32();
				int num2 = reader.ReadInt32();
				if (num2 >= 0)
				{
					obj = Activator.CreateInstance(type, new object[] { num2 });
					Array array = obj as Array;
					TypeInfo typeInfo = type_info.subTypes[0];
					if (Helper.IsPOD(typeInfo.info))
					{
						this.ReadArrayFast(array, typeInfo, reader);
					}
					else if (Helper.IsValueType(typeInfo.info))
					{
						DeserializationMapping deserializationMapping = Manager.GetDeserializationMapping(typeInfo.type);
						object obj2 = Activator.CreateInstance(typeInfo.type);
						for (int i = 0; i < num2; i++)
						{
							deserializationMapping.Deserialize(obj2, reader);
							array.SetValue(obj2, i);
						}
					}
					else
					{
						for (int j = 0; j < num2; j++)
						{
							object obj3 = this.ReadValue(typeInfo, reader, null);
							array.SetValue(obj3, j);
						}
					}
				}
				break;
			}
			case SerializationTypeInfo.Pair:
				if (reader.ReadInt32() >= 0)
				{
					TypeInfo typeInfo2 = type_info.subTypes[0];
					TypeInfo typeInfo3 = type_info.subTypes[1];
					object obj4 = this.ReadValue(typeInfo2, reader, null);
					object obj5 = this.ReadValue(typeInfo3, reader, null);
					obj = Activator.CreateInstance(type_info.genericInstantiationType, new object[] { obj4, obj5 });
				}
				break;
			case SerializationTypeInfo.Dictionary:
			{
				reader.ReadInt32();
				int num3 = reader.ReadInt32();
				if (num3 >= 0)
				{
					obj = Activator.CreateInstance(type_info.genericInstantiationType);
					IDictionary dictionary = obj as IDictionary;
					TypeInfo typeInfo4 = type_info.subTypes[1];
					Array array2 = Array.CreateInstance(typeInfo4.type, num3);
					for (int k = 0; k < num3; k++)
					{
						object obj6 = this.ReadValue(typeInfo4, reader, null);
						array2.SetValue(obj6, k);
					}
					TypeInfo typeInfo5 = type_info.subTypes[0];
					Array array3 = Array.CreateInstance(typeInfo5.type, num3);
					for (int l = 0; l < num3; l++)
					{
						object obj7 = this.ReadValue(typeInfo5, reader, null);
						array3.SetValue(obj7, l);
					}
					for (int m = 0; m < num3; m++)
					{
						dictionary.Add(array3.GetValue(m), array2.GetValue(m));
					}
				}
				break;
			}
			case SerializationTypeInfo.List:
			{
				reader.ReadInt32();
				int num4 = reader.ReadInt32();
				if (num4 >= 0)
				{
					TypeInfo typeInfo6 = type_info.subTypes[0];
					Array array4 = Array.CreateInstance(typeInfo6.type, num4);
					if (Helper.IsPOD(typeInfo6.info))
					{
						this.ReadArrayFast(array4, typeInfo6, reader);
					}
					else if (Helper.IsValueType(typeInfo6.info))
					{
						DeserializationMapping deserializationMapping2 = Manager.GetDeserializationMapping(typeInfo6.type);
						object obj8 = Activator.CreateInstance(typeInfo6.type);
						for (int n = 0; n < num4; n++)
						{
							deserializationMapping2.Deserialize(obj8, reader);
							array4.SetValue(obj8, n);
						}
					}
					else
					{
						for (int num5 = 0; num5 < num4; num5++)
						{
							object obj9 = this.ReadValue(typeInfo6, reader, null);
							array4.SetValue(obj9, num5);
						}
					}
					obj = Activator.CreateInstance(type_info.genericInstantiationType, new object[] { array4 });
				}
				break;
			}
			case SerializationTypeInfo.HashSet:
			{
				reader.ReadInt32();
				int num6 = reader.ReadInt32();
				if (num6 >= 0)
				{
					TypeInfo typeInfo7 = type_info.subTypes[0];
					Array array5 = Array.CreateInstance(typeInfo7.type, num6);
					if (Helper.IsValueType(typeInfo7.info))
					{
						DeserializationMapping deserializationMapping3 = Manager.GetDeserializationMapping(typeInfo7.type);
						object obj10 = Activator.CreateInstance(typeInfo7.type);
						for (int num7 = 0; num7 < num6; num7++)
						{
							deserializationMapping3.Deserialize(obj10, reader);
							array5.SetValue(obj10, num7);
						}
					}
					else
					{
						for (int num8 = 0; num8 < num6; num8++)
						{
							object obj11 = this.ReadValue(typeInfo7, reader, null);
							array5.SetValue(obj11, num8);
						}
					}
					obj = Activator.CreateInstance(type_info.genericInstantiationType, new object[] { array5 });
				}
				break;
			}
			case SerializationTypeInfo.Queue:
			{
				reader.ReadInt32();
				int num9 = reader.ReadInt32();
				if (num9 >= 0)
				{
					TypeInfo typeInfo8 = type_info.subTypes[0];
					Array array6 = Array.CreateInstance(typeInfo8.type, num9);
					if (Helper.IsPOD(typeInfo8.info))
					{
						this.ReadArrayFast(array6, typeInfo8, reader);
					}
					else if (Helper.IsValueType(typeInfo8.info))
					{
						DeserializationMapping deserializationMapping4 = Manager.GetDeserializationMapping(typeInfo8.type);
						object obj12 = Activator.CreateInstance(typeInfo8.type);
						for (int num10 = 0; num10 < num9; num10++)
						{
							deserializationMapping4.Deserialize(obj12, reader);
							array6.SetValue(obj12, num10);
						}
					}
					else
					{
						for (int num11 = 0; num11 < num9; num11++)
						{
							object obj13 = this.ReadValue(typeInfo8, reader, null);
							array6.SetValue(obj13, num11);
						}
					}
					obj = Activator.CreateInstance(type_info.genericInstantiationType, new object[] { array6 });
				}
				break;
			}
			case SerializationTypeInfo.Colour:
				obj = reader.ReadColour();
				break;
			default:
				throw new ArgumentException("unknown type");
			}
			return obj;
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x0007C750 File Offset: 0x0007A950
		private void ReadArrayFast(Array dest_array, TypeInfo elem_type_info, IReader reader)
		{
			byte[] array = reader.RawBytes();
			int position = reader.Position;
			int length = dest_array.Length;
			int num;
			switch (elem_type_info.info)
			{
			case SerializationTypeInfo.SByte:
			case SerializationTypeInfo.Byte:
				num = length;
				goto IL_75;
			case SerializationTypeInfo.Int16:
			case SerializationTypeInfo.UInt16:
				num = length * 2;
				goto IL_75;
			case SerializationTypeInfo.Int32:
			case SerializationTypeInfo.UInt32:
			case SerializationTypeInfo.Single:
				num = length * 4;
				goto IL_75;
			case SerializationTypeInfo.Int64:
			case SerializationTypeInfo.UInt64:
			case SerializationTypeInfo.Double:
				num = length * 8;
				goto IL_75;
			}
			throw new Exception("unknown pod type");
			IL_75:
			Buffer.BlockCopy(array, position, dest_array, 0, num);
			reader.SkipBytes(num);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x0007C7E4 File Offset: 0x0007A9E4
		private void SkipValue(SerializationTypeInfo type_info, IReader reader)
		{
			switch (type_info)
			{
			case SerializationTypeInfo.SByte:
			case SerializationTypeInfo.Byte:
			case SerializationTypeInfo.Boolean:
				reader.SkipBytes(1);
				return;
			case SerializationTypeInfo.Int16:
			case SerializationTypeInfo.UInt16:
				reader.SkipBytes(2);
				return;
			case SerializationTypeInfo.Int32:
			case SerializationTypeInfo.UInt32:
			case SerializationTypeInfo.Single:
			case SerializationTypeInfo.Enumeration:
				reader.SkipBytes(4);
				return;
			case SerializationTypeInfo.Int64:
			case SerializationTypeInfo.UInt64:
			case SerializationTypeInfo.Double:
			case SerializationTypeInfo.Vector2I:
			case SerializationTypeInfo.Vector2:
				reader.SkipBytes(8);
				return;
			case SerializationTypeInfo.String:
			{
				int num = reader.ReadInt32();
				if (num > 0)
				{
					reader.SkipBytes(num);
					return;
				}
				return;
			}
			case SerializationTypeInfo.Vector3:
				reader.SkipBytes(12);
				return;
			case SerializationTypeInfo.Colour:
				reader.SkipBytes(4);
				return;
			}
			throw new ArgumentException("Unhandled type. Not sure how to skip by");
		}

		// Token: 0x040013F3 RID: 5107
		private DeserializationTemplate template;

		// Token: 0x040013F4 RID: 5108
		private List<DeserializationMapping.DeserializationInfo> deserializationInfo = new List<DeserializationMapping.DeserializationInfo>();

		// Token: 0x02000B1E RID: 2846
		private struct DeserializationInfo
		{
			// Token: 0x04002632 RID: 9778
			public bool valid;

			// Token: 0x04002633 RID: 9779
			public FieldInfo field;

			// Token: 0x04002634 RID: 9780
			public PropertyInfo property;

			// Token: 0x04002635 RID: 9781
			public TypeInfo typeInfo;
		}
	}
}
