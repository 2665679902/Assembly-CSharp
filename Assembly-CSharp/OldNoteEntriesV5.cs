using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

// Token: 0x020004B1 RID: 1201
public class OldNoteEntriesV5
{
	// Token: 0x06001B5E RID: 7006 RVA: 0x0009174C File Offset: 0x0008F94C
	public void Deserialize(BinaryReader reader)
	{
		int num = reader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			OldNoteEntriesV5.NoteStorageBlock noteStorageBlock = default(OldNoteEntriesV5.NoteStorageBlock);
			noteStorageBlock.Deserialize(reader);
			this.storageBlocks.Add(noteStorageBlock);
		}
	}

	// Token: 0x04000F4A RID: 3914
	public List<OldNoteEntriesV5.NoteStorageBlock> storageBlocks = new List<OldNoteEntriesV5.NoteStorageBlock>();

	// Token: 0x020010E9 RID: 4329
	[StructLayout(LayoutKind.Explicit)]
	public struct NoteEntry
	{
		// Token: 0x04005913 RID: 22803
		[FieldOffset(0)]
		public int reportEntryId;

		// Token: 0x04005914 RID: 22804
		[FieldOffset(4)]
		public int noteHash;

		// Token: 0x04005915 RID: 22805
		[FieldOffset(8)]
		public float value;
	}

	// Token: 0x020010EA RID: 4330
	[StructLayout(LayoutKind.Explicit)]
	public struct NoteEntryArray
	{
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060074DD RID: 29917 RVA: 0x002B454F File Offset: 0x002B274F
		public int StructSizeInBytes
		{
			get
			{
				return Marshal.SizeOf(typeof(OldNoteEntriesV5.NoteEntry));
			}
		}

		// Token: 0x04005916 RID: 22806
		[FieldOffset(0)]
		public byte[] bytes;

		// Token: 0x04005917 RID: 22807
		[FieldOffset(0)]
		public OldNoteEntriesV5.NoteEntry[] structs;
	}

	// Token: 0x020010EB RID: 4331
	public struct NoteStorageBlock
	{
		// Token: 0x060074DE RID: 29918 RVA: 0x002B4560 File Offset: 0x002B2760
		public void Deserialize(BinaryReader reader)
		{
			this.entryCount = reader.ReadInt32();
			this.entries.bytes = reader.ReadBytes(this.entries.StructSizeInBytes * this.entryCount);
		}

		// Token: 0x04005918 RID: 22808
		public int entryCount;

		// Token: 0x04005919 RID: 22809
		public OldNoteEntriesV5.NoteEntryArray entries;
	}
}
