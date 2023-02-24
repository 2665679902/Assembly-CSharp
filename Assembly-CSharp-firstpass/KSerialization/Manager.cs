using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace KSerialization
{
	// Token: 0x02000507 RID: 1287
	public class Manager
	{
		// Token: 0x0600371A RID: 14106 RVA: 0x0007A8E4 File Offset: 0x00078AE4
		public static void Initialize()
		{
			Manager.assemblies = AppDomain.CurrentDomain.GetAssemblies();
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x0007A8F8 File Offset: 0x00078AF8
		public static Type GetType(string type_name)
		{
			Type type = Type.GetType(type_name);
			if (type == null)
			{
				Assembly[] array = Manager.assemblies;
				for (int i = 0; i < array.Length; i++)
				{
					type = array[i].GetType(type_name);
					if (type != null)
					{
						break;
					}
				}
			}
			if (type == null)
			{
				DebugLog.Output(DebugLog.Level.Warning, "Failed to find type named: " + type_name);
			}
			return type;
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x0007A958 File Offset: 0x00078B58
		public static TypeInfo GetTypeInfo(Type type)
		{
			TypeInfo typeInfo;
			if (!Manager.typeInfoMap.TryGetValue(type, out typeInfo))
			{
				typeInfo = Manager.EncodeTypeInfo(type);
			}
			return typeInfo;
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x0007A97C File Offset: 0x00078B7C
		public static SerializationTemplate GetSerializationTemplate(Type type)
		{
			if (type == null)
			{
				throw new InvalidOperationException("Invalid type encountered when serializing");
			}
			SerializationTemplate serializationTemplate = null;
			if (!Manager.serializationTemplatesByType.TryGetValue(type, out serializationTemplate))
			{
				serializationTemplate = new SerializationTemplate(type);
				Manager.serializationTemplatesByType[type] = serializationTemplate;
				Manager.serializationTemplatesByTypeName[type.GetKTypeString()] = serializationTemplate;
			}
			return serializationTemplate;
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x0007A9D4 File Offset: 0x00078BD4
		public static SerializationTemplate GetSerializationTemplate(string type_name)
		{
			if (type_name == null || type_name == "")
			{
				throw new InvalidOperationException("Invalid type name encountered when serializing");
			}
			SerializationTemplate serializationTemplate = null;
			if (!Manager.serializationTemplatesByTypeName.TryGetValue(type_name, out serializationTemplate))
			{
				Type type = Manager.GetType(type_name);
				if (type != null)
				{
					serializationTemplate = new SerializationTemplate(type);
					Manager.serializationTemplatesByType[type] = serializationTemplate;
					Manager.serializationTemplatesByTypeName[type_name] = serializationTemplate;
				}
			}
			return serializationTemplate;
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x0007AA40 File Offset: 0x00078C40
		public static DeserializationTemplate GetDeserializationTemplate(Type type)
		{
			DeserializationTemplate deserializationTemplate = null;
			Manager.deserializationTemplatesByType.TryGetValue(type, out deserializationTemplate);
			return deserializationTemplate;
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x0007AA60 File Offset: 0x00078C60
		public static DeserializationTemplate GetDeserializationTemplate(string type_name)
		{
			DeserializationTemplate deserializationTemplate = null;
			Manager.deserializationTemplatesByTypeName.TryGetValue(type_name, out deserializationTemplate);
			return deserializationTemplate;
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x0007AA80 File Offset: 0x00078C80
		public static void SerializeDirectory(BinaryWriter writer)
		{
			writer.Write(Manager.serializationTemplatesByTypeName.Count);
			foreach (KeyValuePair<string, SerializationTemplate> keyValuePair in Manager.serializationTemplatesByTypeName)
			{
				string key = keyValuePair.Key;
				SerializationTemplate value = keyValuePair.Value;
				try
				{
					writer.WriteKleiString(key);
					value.SerializeTemplate(writer);
				}
				catch (Exception ex)
				{
					DebugUtil.LogErrorArgs(new object[]
					{
						"Error serializing template " + key + "\n",
						ex.Message,
						ex.StackTrace
					});
				}
			}
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x0007AB40 File Offset: 0x00078D40
		public static void DeserializeDirectory(IReader reader)
		{
			Manager.deserializationTemplatesByTypeName.Clear();
			Manager.deserializationTemplatesByType.Clear();
			Manager.deserializationMappings.Clear();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				try
				{
					DeserializationTemplate deserializationTemplate = new DeserializationTemplate(text, reader);
					Manager.deserializationTemplatesByTypeName[text] = deserializationTemplate;
					Type type = Manager.GetType(text);
					if (type != null)
					{
						Manager.deserializationTemplatesByType[type] = deserializationTemplate;
					}
				}
				catch (Exception ex)
				{
					string text2 = string.Concat(new string[] { "Error deserializing template ", text, "\n", ex.Message, "\n", ex.StackTrace });
					Debug.LogError(text2);
					throw new Exception(text2, ex);
				}
			}
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x0007AC1C File Offset: 0x00078E1C
		public static void Clear()
		{
			Manager.serializationTemplatesByTypeName.Clear();
			Manager.serializationTemplatesByType.Clear();
			Manager.deserializationTemplatesByTypeName.Clear();
			Manager.deserializationTemplatesByType.Clear();
			Manager.deserializationMappings.Clear();
			Manager.typeInfoMap.Clear();
			Helper.ClearTypeInfoMask();
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x0007AC6A File Offset: 0x00078E6A
		public static bool HasDeserializationMapping(Type type)
		{
			return Manager.GetDeserializationTemplate(type) != null && Manager.GetSerializationTemplate(type) != null;
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x0007AC80 File Offset: 0x00078E80
		public static DeserializationMapping GetDeserializationMapping(Type type)
		{
			DeserializationTemplate deserializationTemplate = Manager.GetDeserializationTemplate(type);
			if (deserializationTemplate == null)
			{
				throw new ArgumentException("Tried to deserialize a class named: " + type.GetKTypeString() + " but no such class exists");
			}
			SerializationTemplate serializationTemplate = Manager.GetSerializationTemplate(type);
			if (serializationTemplate == null)
			{
				throw new ArgumentException("Tried to deserialize into a class named: " + type.GetKTypeString() + " but no such class exists");
			}
			return Manager.GetMapping(deserializationTemplate, serializationTemplate);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x0007ACDC File Offset: 0x00078EDC
		public static DeserializationMapping GetDeserializationMapping(string type_name)
		{
			DeserializationTemplate deserializationTemplate = Manager.GetDeserializationTemplate(type_name);
			if (deserializationTemplate == null)
			{
				throw new ArgumentException("Tried to deserialize a class named: " + type_name + " but no such class exists");
			}
			SerializationTemplate serializationTemplate = Manager.GetSerializationTemplate(type_name);
			if (serializationTemplate == null)
			{
				throw new ArgumentException("Tried to deserialize into a class named: " + type_name + " but no such class exists");
			}
			return Manager.GetMapping(deserializationTemplate, serializationTemplate);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x0007AD30 File Offset: 0x00078F30
		private static DeserializationMapping GetMapping(DeserializationTemplate dtemplate, SerializationTemplate stemplate)
		{
			KeyValuePair<SerializationTemplate, DeserializationMapping> keyValuePair;
			DeserializationMapping deserializationMapping;
			if (Manager.deserializationMappings.TryGetValue(dtemplate, out keyValuePair))
			{
				deserializationMapping = keyValuePair.Value;
			}
			else
			{
				deserializationMapping = new DeserializationMapping(dtemplate, stemplate);
				keyValuePair = new KeyValuePair<SerializationTemplate, DeserializationMapping>(stemplate, deserializationMapping);
				Manager.deserializationMappings[dtemplate] = keyValuePair;
			}
			return deserializationMapping;
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x0007AD78 File Offset: 0x00078F78
		private static TypeInfo EncodeTypeInfo(Type type)
		{
			TypeInfo typeInfo = new TypeInfo();
			typeInfo.type = type;
			typeInfo.info = Helper.EncodeSerializationType(type);
			if (type.IsGenericType)
			{
				typeInfo.genericTypeArgs = type.GetGenericArguments();
				typeInfo.subTypes = new TypeInfo[typeInfo.genericTypeArgs.Length];
				for (int i = 0; i < typeInfo.genericTypeArgs.Length; i++)
				{
					typeInfo.subTypes[i] = Manager.GetTypeInfo(typeInfo.genericTypeArgs[i]);
				}
			}
			else if (typeof(Array).IsAssignableFrom(type))
			{
				Type elementType = type.GetElementType();
				typeInfo.subTypes = new TypeInfo[1];
				typeInfo.subTypes[0] = Manager.GetTypeInfo(elementType);
			}
			return typeInfo;
		}

		// Token: 0x040013E0 RID: 5088
		private static Dictionary<string, SerializationTemplate> serializationTemplatesByTypeName = new Dictionary<string, SerializationTemplate>();

		// Token: 0x040013E1 RID: 5089
		private static Dictionary<string, DeserializationTemplate> deserializationTemplatesByTypeName = new Dictionary<string, DeserializationTemplate>();

		// Token: 0x040013E2 RID: 5090
		private static Dictionary<Type, SerializationTemplate> serializationTemplatesByType = new Dictionary<Type, SerializationTemplate>();

		// Token: 0x040013E3 RID: 5091
		private static Dictionary<Type, DeserializationTemplate> deserializationTemplatesByType = new Dictionary<Type, DeserializationTemplate>();

		// Token: 0x040013E4 RID: 5092
		private static Dictionary<DeserializationTemplate, KeyValuePair<SerializationTemplate, DeserializationMapping>> deserializationMappings = new Dictionary<DeserializationTemplate, KeyValuePair<SerializationTemplate, DeserializationMapping>>();

		// Token: 0x040013E5 RID: 5093
		private static Dictionary<Type, TypeInfo> typeInfoMap = new Dictionary<Type, TypeInfo>();

		// Token: 0x040013E6 RID: 5094
		private static Assembly[] assemblies = null;
	}
}
