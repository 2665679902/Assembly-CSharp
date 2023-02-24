using System;
using System.IO;

namespace KSerialization
{
	// Token: 0x02000505 RID: 1285
	public class Serializer
	{
		// Token: 0x06003710 RID: 14096 RVA: 0x0007A70E File Offset: 0x0007890E
		public Serializer(BinaryWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x0007A71D File Offset: 0x0007891D
		public void Serialize(object obj)
		{
			Serializer.Serialize(obj, this.writer);
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x0007A72C File Offset: 0x0007892C
		public static void Serialize(object obj, BinaryWriter writer)
		{
			SerializationTemplate serializationTemplate = Manager.GetSerializationTemplate(obj.GetType());
			string ktypeString = obj.GetType().GetKTypeString();
			writer.WriteKleiString(ktypeString);
			serializationTemplate.SerializeData(obj, writer);
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x0007A75E File Offset: 0x0007895E
		public static void SerializeTypeless(object obj, BinaryWriter writer)
		{
			Manager.GetSerializationTemplate(obj.GetType()).SerializeData(obj, writer);
		}

		// Token: 0x040013DE RID: 5086
		private BinaryWriter writer;
	}
}
