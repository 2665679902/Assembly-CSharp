using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using KSerialization;

// Token: 0x02000B20 RID: 2848
[SerializationConfig(MemberSerialization.OptIn)]
public class SerializedList<ItemType>
{
	// Token: 0x17000652 RID: 1618
	// (get) Token: 0x060057CE RID: 22478 RVA: 0x001FD4E6 File Offset: 0x001FB6E6
	public int Count
	{
		get
		{
			return this.items.Count;
		}
	}

	// Token: 0x060057CF RID: 22479 RVA: 0x001FD4F3 File Offset: 0x001FB6F3
	public IEnumerator<ItemType> GetEnumerator()
	{
		return this.items.GetEnumerator();
	}

	// Token: 0x17000653 RID: 1619
	public ItemType this[int idx]
	{
		get
		{
			return this.items[idx];
		}
	}

	// Token: 0x060057D1 RID: 22481 RVA: 0x001FD513 File Offset: 0x001FB713
	public void Add(ItemType item)
	{
		this.items.Add(item);
	}

	// Token: 0x060057D2 RID: 22482 RVA: 0x001FD521 File Offset: 0x001FB721
	public void Remove(ItemType item)
	{
		this.items.Remove(item);
	}

	// Token: 0x060057D3 RID: 22483 RVA: 0x001FD530 File Offset: 0x001FB730
	public void RemoveAt(int idx)
	{
		this.items.RemoveAt(idx);
	}

	// Token: 0x060057D4 RID: 22484 RVA: 0x001FD53E File Offset: 0x001FB73E
	public bool Contains(ItemType item)
	{
		return this.items.Contains(item);
	}

	// Token: 0x060057D5 RID: 22485 RVA: 0x001FD54C File Offset: 0x001FB74C
	public void Clear()
	{
		this.items.Clear();
	}

	// Token: 0x060057D6 RID: 22486 RVA: 0x001FD55C File Offset: 0x001FB75C
	[OnSerializing]
	private void OnSerializing()
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(this.items.Count);
		foreach (ItemType itemType in this.items)
		{
			binaryWriter.WriteKleiString(itemType.GetType().FullName);
			long position = binaryWriter.BaseStream.Position;
			binaryWriter.Write(0);
			long position2 = binaryWriter.BaseStream.Position;
			Serializer.SerializeTypeless(itemType, binaryWriter);
			long position3 = binaryWriter.BaseStream.Position;
			long num = position3 - position2;
			binaryWriter.BaseStream.Position = position;
			binaryWriter.Write((int)num);
			binaryWriter.BaseStream.Position = position3;
		}
		memoryStream.Flush();
		this.serializationBuffer = memoryStream.ToArray();
	}

	// Token: 0x060057D7 RID: 22487 RVA: 0x001FD65C File Offset: 0x001FB85C
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.serializationBuffer == null)
		{
			return;
		}
		FastReader fastReader = new FastReader(this.serializationBuffer);
		int num = fastReader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			string text = fastReader.ReadKleiString();
			int num2 = fastReader.ReadInt32();
			int position = fastReader.Position;
			Type type = Type.GetType(text);
			if (type == null)
			{
				DebugUtil.LogWarningArgs(new object[] { "Type no longer exists: " + text });
				fastReader.SkipBytes(num2);
			}
			else
			{
				ItemType itemType;
				if (typeof(ItemType) != type)
				{
					itemType = (ItemType)((object)Activator.CreateInstance(type));
				}
				else
				{
					itemType = default(ItemType);
				}
				Deserializer.DeserializeTypeless(itemType, fastReader);
				if (fastReader.Position != position + num2)
				{
					DebugUtil.LogWarningArgs(new object[]
					{
						"Expected to be at offset",
						position + num2,
						"but was only at offset",
						fastReader.Position,
						". Skipping to catch up."
					});
					fastReader.SkipBytes(position + num2 - fastReader.Position);
				}
				this.items.Add(itemType);
			}
		}
	}

	// Token: 0x04003B70 RID: 15216
	[Serialize]
	private byte[] serializationBuffer;

	// Token: 0x04003B71 RID: 15217
	private List<ItemType> items = new List<ItemType>();
}
