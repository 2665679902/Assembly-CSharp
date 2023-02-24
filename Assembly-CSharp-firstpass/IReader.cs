using System;

// Token: 0x0200009A RID: 154
public interface IReader
{
	// Token: 0x060005EA RID: 1514
	byte ReadByte();

	// Token: 0x060005EB RID: 1515
	sbyte ReadSByte();

	// Token: 0x060005EC RID: 1516
	short ReadInt16();

	// Token: 0x060005ED RID: 1517
	ushort ReadUInt16();

	// Token: 0x060005EE RID: 1518
	int ReadInt32();

	// Token: 0x060005EF RID: 1519
	uint ReadUInt32();

	// Token: 0x060005F0 RID: 1520
	long ReadInt64();

	// Token: 0x060005F1 RID: 1521
	ulong ReadUInt64();

	// Token: 0x060005F2 RID: 1522
	float ReadSingle();

	// Token: 0x060005F3 RID: 1523
	double ReadDouble();

	// Token: 0x060005F4 RID: 1524
	char[] ReadChars(int length);

	// Token: 0x060005F5 RID: 1525
	byte[] ReadBytes(int length);

	// Token: 0x060005F6 RID: 1526
	void SkipBytes(int length);

	// Token: 0x060005F7 RID: 1527
	string ReadKleiString();

	// Token: 0x060005F8 RID: 1528
	byte[] RawBytes();

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x060005F9 RID: 1529
	int Position { get; }

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x060005FA RID: 1530
	bool IsFinished { get; }
}
