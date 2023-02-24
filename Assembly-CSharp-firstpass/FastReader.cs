using System;
using System.Text;

// Token: 0x0200009B RID: 155
public class FastReader : IReader
{
	// Token: 0x060005FB RID: 1531 RVA: 0x0001BBBA File Offset: 0x00019DBA
	public FastReader(byte[] bytes)
	{
		this.bytes = bytes;
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0001BBCC File Offset: 0x00019DCC
	public unsafe byte ReadByte()
	{
		byte b;
		fixed (byte* ptr = &this.bytes[this.idx])
		{
			b = *ptr;
		}
		this.idx++;
		return b;
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0001BC00 File Offset: 0x00019E00
	public unsafe sbyte ReadSByte()
	{
		sbyte b;
		fixed (byte* ptr = &this.bytes[this.idx])
		{
			b = *(sbyte*)ptr;
		}
		this.idx++;
		return b;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0001BC34 File Offset: 0x00019E34
	public unsafe ushort ReadUInt16()
	{
		ushort num;
		fixed (byte* ptr = &this.bytes[this.idx])
		{
			num = *(ushort*)ptr;
		}
		this.idx += 2;
		return num;
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0001BC68 File Offset: 0x00019E68
	public unsafe short ReadInt16()
	{
		short num;
		fixed (byte* ptr = &this.bytes[this.idx])
		{
			num = *(short*)ptr;
		}
		this.idx += 2;
		return num;
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0001BC9C File Offset: 0x00019E9C
	public unsafe uint ReadUInt32()
	{
		uint num;
		fixed (byte* ptr = &this.bytes[this.idx])
		{
			num = *(uint*)ptr;
		}
		this.idx += 4;
		return num;
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0001BCD0 File Offset: 0x00019ED0
	public unsafe int ReadInt32()
	{
		int num;
		fixed (byte* ptr = &this.bytes[this.idx])
		{
			num = *(int*)ptr;
		}
		this.idx += 4;
		return num;
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0001BD04 File Offset: 0x00019F04
	public unsafe ulong ReadUInt64()
	{
		ulong num;
		fixed (byte* ptr = &this.bytes[this.idx])
		{
			num = (ulong)(*(long*)ptr);
		}
		this.idx += 8;
		return num;
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0001BD38 File Offset: 0x00019F38
	public unsafe long ReadInt64()
	{
		long num;
		fixed (byte* ptr = &this.bytes[this.idx])
		{
			num = *(long*)ptr;
		}
		this.idx += 8;
		return num;
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0001BD6C File Offset: 0x00019F6C
	public unsafe float ReadSingle()
	{
		float num;
		fixed (byte* ptr = &this.bytes[this.idx])
		{
			num = *(float*)ptr;
		}
		this.idx += 4;
		return num;
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x0001BDA0 File Offset: 0x00019FA0
	public unsafe double ReadDouble()
	{
		double num;
		fixed (byte* ptr = &this.bytes[this.idx])
		{
			num = *(double*)ptr;
		}
		this.idx += 8;
		return num;
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0001BDD4 File Offset: 0x00019FD4
	public char[] ReadChars(int length)
	{
		char[] array = new char[length];
		for (int i = 0; i < length; i++)
		{
			array[i] = (char)this.bytes[this.idx + i];
		}
		this.idx += length;
		return array;
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0001BE18 File Offset: 0x0001A018
	public byte[] ReadBytes(int length)
	{
		byte[] array = new byte[length];
		for (int i = 0; i < length; i++)
		{
			array[i] = this.bytes[this.idx + i];
		}
		this.idx += length;
		return array;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0001BE5C File Offset: 0x0001A05C
	public string ReadKleiString()
	{
		int num = this.ReadInt32();
		string text = null;
		if (num >= 0)
		{
			text = Encoding.UTF8.GetString(this.bytes, this.idx, num);
			this.idx += num;
		}
		return text;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0001BE9D File Offset: 0x0001A09D
	public void SkipBytes(int length)
	{
		this.idx += length;
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001BEAD File Offset: 0x0001A0AD
	public bool IsFinished
	{
		get
		{
			return this.bytes == null || this.idx == this.bytes.Length;
		}
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0001BEC9 File Offset: 0x0001A0C9
	public byte[] RawBytes()
	{
		return this.bytes;
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001BED1 File Offset: 0x0001A0D1
	// (set) Token: 0x0600060D RID: 1549 RVA: 0x0001BED9 File Offset: 0x0001A0D9
	public int Position
	{
		get
		{
			return this.idx;
		}
		set
		{
			this.idx = value;
		}
	}

	// Token: 0x04000594 RID: 1428
	private int idx;

	// Token: 0x04000595 RID: 1429
	private byte[] bytes;
}
