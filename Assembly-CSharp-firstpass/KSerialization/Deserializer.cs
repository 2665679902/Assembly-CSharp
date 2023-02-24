using System;

namespace KSerialization
{
	// Token: 0x02000506 RID: 1286
	public class Deserializer
	{
		// Token: 0x06003714 RID: 14100 RVA: 0x0007A772 File Offset: 0x00078972
		public Deserializer(IReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x0007A781 File Offset: 0x00078981
		public bool Deserialize(object obj)
		{
			return Deserializer.Deserialize(obj, this.reader);
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x0007A790 File Offset: 0x00078990
		public static bool Deserialize(object obj, IReader reader)
		{
			string text = reader.ReadKleiString();
			Type type = obj.GetType();
			return type.GetKTypeString() == text && Deserializer.DeserializeTypeless(type, obj, reader);
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x0007A7C4 File Offset: 0x000789C4
		public static bool DeserializeTypeless(Type type, object obj, IReader reader)
		{
			DeserializationMapping deserializationMapping = Manager.GetDeserializationMapping(type);
			bool flag = false;
			try
			{
				flag = deserializationMapping.Deserialize(obj, reader);
			}
			catch (Exception ex)
			{
				string text = string.Format("Exception occurred while attempting to deserialize object {0}({1}).\n{2}", obj, obj.GetType(), ex.ToString());
				DebugLog.Output(DebugLog.Level.Error, text);
				throw new Exception(text, ex);
			}
			return flag;
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x0007A820 File Offset: 0x00078A20
		public static bool DeserializeTypeless(object obj, IReader reader)
		{
			DeserializationMapping deserializationMapping = Manager.GetDeserializationMapping(obj.GetType());
			bool flag;
			try
			{
				flag = deserializationMapping.Deserialize(obj, reader);
			}
			catch (Exception ex)
			{
				string text = string.Format("Exception occurred while attempting to deserialize object {0}({1}).\n{2}", obj, obj.GetType(), ex.ToString());
				DebugLog.Output(DebugLog.Level.Error, text);
				throw new Exception(text, ex);
			}
			return flag;
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x0007A880 File Offset: 0x00078A80
		public static bool Deserialize(Type type, IReader reader, out object result)
		{
			DeserializationMapping deserializationMapping = Manager.GetDeserializationMapping(type);
			bool flag;
			try
			{
				object obj = Activator.CreateInstance(type);
				flag = deserializationMapping.Deserialize(obj, reader);
				result = obj;
			}
			catch (Exception ex)
			{
				string text = string.Format("Exception occurred while attempting to deserialize into object of type {0}.\n{1}", type.ToString(), ex.ToString());
				DebugLog.Output(DebugLog.Level.Error, text);
				throw new Exception(text, ex);
			}
			return flag;
		}

		// Token: 0x040013DF RID: 5087
		public IReader reader;
	}
}
