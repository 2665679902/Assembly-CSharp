using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ObjectCloner
{
	// Token: 0x020004B7 RID: 1207
	public static class SerializingCloner
	{
		// Token: 0x060033B0 RID: 13232 RVA: 0x00070804 File Offset: 0x0006EA04
		public static T Copy<T>(T obj)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, obj);
				memoryStream.Position = 0L;
				t = (T)((object)binaryFormatter.Deserialize(memoryStream));
			}
			return t;
		}
	}
}
