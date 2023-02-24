using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class SymbolOverrideInfoGpuData
{
	// Token: 0x1700005B RID: 91
	// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000A007 File Offset: 0x00008207
	private SymbolOverrideInfoGpuData.SymbolOverrideInfo[] symbolOverrideInfos
	{
		get
		{
			return this.symbolOverrideInfoConverter.symbolOverrideInfos;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x060001BA RID: 442 RVA: 0x0000A014 File Offset: 0x00008214
	// (set) Token: 0x060001BB RID: 443 RVA: 0x0000A01C File Offset: 0x0000821C
	public int version { get; private set; }

	// Token: 0x060001BC RID: 444 RVA: 0x0000A028 File Offset: 0x00008228
	public SymbolOverrideInfoGpuData(int symbol_count)
	{
		this.symbolCount = symbol_count;
		this.symbolOverrideInfoConverter = new SymbolOverrideInfoGpuData.SymbolOverrideInfoToByteConverter
		{
			bytes = new byte[12 * symbol_count * 4]
		};
		for (int i = 0; i < symbol_count; i++)
		{
			this.symbolOverrideInfos[i].atlas = 0f;
		}
		this.MarkDirty();
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000A08C File Offset: 0x0000828C
	private void MarkDirty()
	{
		int num = this.version + 1;
		this.version = num;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000A0AC File Offset: 0x000082AC
	public void SetSymbolOverrideInfo(int symbol_start_idx, int symbol_num_frames, int atlas_idx, KBatchGroupData source_data, int source_start_idx, int source_num_frames)
	{
		for (int i = 0; i < symbol_num_frames; i++)
		{
			int num = symbol_start_idx + i;
			int num2 = source_start_idx + Math.Min(source_num_frames - 1, i);
			KAnim.Build.SymbolFrameInstance symbolFrameInstance = source_data.symbolFrameInstances[num2];
			SymbolOverrideInfoGpuData.SymbolOverrideInfo[] symbolOverrideInfos = this.symbolOverrideInfos;
			int num3 = num;
			symbolOverrideInfos[num3].atlas = (float)atlas_idx;
			symbolOverrideInfos[num3].isoverriden = 1f;
			symbolOverrideInfos[num3].bboxMin = symbolFrameInstance.symbolFrame.bboxMin;
			symbolOverrideInfos[num3].bboxMax = symbolFrameInstance.symbolFrame.bboxMax;
			symbolOverrideInfos[num3].uvMin = symbolFrameInstance.symbolFrame.uvMin;
			symbolOverrideInfos[num3].uvMax = symbolFrameInstance.symbolFrame.uvMax;
		}
		this.MarkDirty();
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000A154 File Offset: 0x00008354
	public void SetSymbolOverrideInfo(int symbol_idx, ref KAnim.Build.SymbolFrameInstance symbol_frame_instance)
	{
		SymbolOverrideInfoGpuData.SymbolOverrideInfo[] symbolOverrideInfos = this.symbolOverrideInfos;
		symbolOverrideInfos[symbol_idx].atlas = (float)symbol_frame_instance.buildImageIdx;
		symbolOverrideInfos[symbol_idx].isoverriden = 1f;
		symbolOverrideInfos[symbol_idx].bboxMin = symbol_frame_instance.symbolFrame.bboxMin;
		symbolOverrideInfos[symbol_idx].bboxMax = symbol_frame_instance.symbolFrame.bboxMax;
		symbolOverrideInfos[symbol_idx].uvMin = symbol_frame_instance.symbolFrame.uvMin;
		symbolOverrideInfos[symbol_idx].uvMax = symbol_frame_instance.symbolFrame.uvMax;
		this.MarkDirty();
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000A1CE File Offset: 0x000083CE
	public void WriteToTexture(NativeArray<byte> data, int data_idx, int instance_idx)
	{
		DebugUtil.Assert(instance_idx * this.symbolCount * 12 * 4 == data_idx);
		NativeArray<byte>.Copy(this.symbolOverrideInfoConverter.bytes, 0, data, data_idx, this.symbolCount * 12 * 4);
	}

	// Token: 0x040000AA RID: 170
	public const int FLOATS_PER_SYMBOL_OVERRIDE_INFO = 12;

	// Token: 0x040000AB RID: 171
	private SymbolOverrideInfoGpuData.SymbolOverrideInfoToByteConverter symbolOverrideInfoConverter;

	// Token: 0x040000AC RID: 172
	private int symbolCount;

	// Token: 0x02000973 RID: 2419
	[StructLayout(LayoutKind.Explicit)]
	public struct SymbolOverrideInfo
	{
		// Token: 0x040020BF RID: 8383
		[FieldOffset(0)]
		public float atlas;

		// Token: 0x040020C0 RID: 8384
		[FieldOffset(4)]
		public float isoverriden;

		// Token: 0x040020C1 RID: 8385
		[FieldOffset(8)]
		public float unused1;

		// Token: 0x040020C2 RID: 8386
		[FieldOffset(12)]
		public float unused2;

		// Token: 0x040020C3 RID: 8387
		[FieldOffset(16)]
		public Vector2 bboxMin;

		// Token: 0x040020C4 RID: 8388
		[FieldOffset(24)]
		public Vector2 bboxMax;

		// Token: 0x040020C5 RID: 8389
		[FieldOffset(32)]
		public Vector2 uvMin;

		// Token: 0x040020C6 RID: 8390
		[FieldOffset(40)]
		public Vector2 uvMax;
	}

	// Token: 0x02000974 RID: 2420
	[StructLayout(LayoutKind.Explicit)]
	public struct SymbolOverrideInfoToByteConverter
	{
		// Token: 0x040020C7 RID: 8391
		[FieldOffset(0)]
		public byte[] bytes;

		// Token: 0x040020C8 RID: 8392
		[FieldOffset(0)]
		public SymbolOverrideInfoGpuData.SymbolOverrideInfo[] symbolOverrideInfos;
	}
}
