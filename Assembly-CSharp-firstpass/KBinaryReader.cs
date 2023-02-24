using System;
using System.IO;
using System.Text;

// Token: 0x0200009C RID: 156
public class KBinaryReader : BinaryReader, IReader
{
	// Token: 0x0600060E RID: 1550 RVA: 0x0001BEE2 File Offset: 0x0001A0E2
	public KBinaryReader(Stream stream)
		: base(stream)
	{
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0001BEEB File Offset: 0x0001A0EB
	public void SkipBytes(int length)
	{
		this.ReadBytes(length);
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001BEF5 File Offset: 0x0001A0F5
	public bool IsFinished
	{
		get
		{
			return this.BaseStream.Position == this.BaseStream.Length;
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001BF0F File Offset: 0x0001A10F
	public int Position
	{
		get
		{
			return (int)this.BaseStream.Position;
		}
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0001BF20 File Offset: 0x0001A120
	public string ReadKleiString()
	{
		string text = null;
		int num = this.ReadInt32();
		if (num >= 0)
		{
			byte[] array = this.ReadBytes(num);
			text = Encoding.UTF8.GetString(array, 0, num);
		}
		return text;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0001BF54 File Offset: 0x0001A154
	public byte[] RawBytes()
	{
		long position = this.BaseStream.Position;
		this.BaseStream.Position = 0L;
		byte[] array = this.ReadBytes((int)this.BaseStream.Length);
		this.BaseStream.Position = position;
		return array;
	}
}
