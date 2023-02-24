using System;
using System.IO;
using KSerialization;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public static class KGameObjectComponentManagerUtil
{
	// Token: 0x060006DA RID: 1754 RVA: 0x0001DDEC File Offset: 0x0001BFEC
	public static void Serialize<MgrType, DataType>(MgrType mgr, GameObject go, BinaryWriter writer) where MgrType : KGameObjectComponentManager<DataType> where DataType : new()
	{
		long position = writer.BaseStream.Position;
		writer.Write(0);
		long position2 = writer.BaseStream.Position;
		HandleVector<int>.Handle handle = mgr.GetHandle(go);
		Serializer.SerializeTypeless(mgr.GetData(handle), writer);
		long position3 = writer.BaseStream.Position;
		long num = position3 - position2;
		writer.BaseStream.Position = position;
		writer.Write((int)num);
		writer.BaseStream.Position = position3;
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0001DE70 File Offset: 0x0001C070
	public static void Deserialize<MgrType, DataType>(MgrType mgr, GameObject go, IReader reader) where MgrType : KGameObjectComponentManager<DataType> where DataType : new()
	{
		HandleVector<int>.Handle handle = mgr.GetHandle(go);
		object obj;
		Deserializer.Deserialize(typeof(DataType), reader, out obj);
		mgr.SetData(handle, (DataType)((object)obj));
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
	public static void Serialize<MgrType, Header, Payload>(MgrType mgr, GameObject go, BinaryWriter writer) where MgrType : KGameObjectSplitComponentManager<Header, Payload> where Header : new() where Payload : new()
	{
		long position = writer.BaseStream.Position;
		writer.Write(0);
		long position2 = writer.BaseStream.Position;
		HandleVector<int>.Handle handle = mgr.GetHandle(go);
		Header header;
		Payload payload;
		mgr.GetData(handle, out header, out payload);
		Serializer.SerializeTypeless(header, writer);
		Serializer.SerializeTypeless(payload, writer);
		long position3 = writer.BaseStream.Position;
		long num = position3 - position2;
		writer.BaseStream.Position = position;
		writer.Write((int)num);
		writer.BaseStream.Position = position3;
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0001DF48 File Offset: 0x0001C148
	public static void Deserialize<MgrType, Header, Payload>(MgrType mgr, GameObject go, IReader reader) where MgrType : KGameObjectSplitComponentManager<Header, Payload> where Header : new() where Payload : new()
	{
		HandleVector<int>.Handle handle = mgr.GetHandle(go);
		object obj;
		Deserializer.Deserialize(typeof(Header), reader, out obj);
		object obj2;
		Deserializer.Deserialize(typeof(Payload), reader, out obj2);
		Payload payload = (Payload)((object)obj2);
		mgr.SetData(handle, (Header)((object)obj), ref payload);
	}
}
