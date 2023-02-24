using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class SymbolInstanceGpuData
{
	// Token: 0x17000059 RID: 89
	// (get) Token: 0x060001AF RID: 431 RVA: 0x00009DE5 File Offset: 0x00007FE5
	private SymbolInstanceGpuData.SymbolInstance[] symbolInstances
	{
		get
		{
			return this.symbolInstancesConverter.symbolInstances;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x060001B0 RID: 432 RVA: 0x00009DF2 File Offset: 0x00007FF2
	// (set) Token: 0x060001B1 RID: 433 RVA: 0x00009DFA File Offset: 0x00007FFA
	public int version { get; private set; }

	// Token: 0x060001B2 RID: 434 RVA: 0x00009E04 File Offset: 0x00008004
	public SymbolInstanceGpuData(int symbol_count)
	{
		this.symbolCount = symbol_count;
		this.symbolInstancesConverter = new SymbolInstanceGpuData.SymbolInstanceToByteConverter
		{
			bytes = new byte[8 * symbol_count * 4]
		};
		for (int i = 0; i < symbol_count; i++)
		{
			this.symbolInstances[i].isVisible = 1f;
			this.symbolInstances[i].symbolIndex = -1f;
			this.symbolInstances[i].scale = 1f;
			this.symbolInstances[i].unused = 1f;
			this.symbolInstances[i].color = Color.white;
		}
		this.MarkDirty();
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00009EC0 File Offset: 0x000080C0
	private void MarkDirty()
	{
		int num = this.version + 1;
		this.version = num;
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00009EE0 File Offset: 0x000080E0
	public void SetVisible(int symbol_idx, bool is_visible)
	{
		DebugUtil.Assert(symbol_idx < this.symbolCount);
		float num = 0f;
		if (is_visible)
		{
			num = 1f;
		}
		if (this.symbolInstances[symbol_idx].isVisible != num)
		{
			this.symbolInstances[symbol_idx].isVisible = num;
			this.MarkDirty();
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00009F36 File Offset: 0x00008136
	public bool IsVisible(int symbol_idx)
	{
		DebugUtil.Assert(symbol_idx < this.symbolCount);
		return this.symbolInstances[symbol_idx].isVisible > 0.5f;
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00009F5E File Offset: 0x0000815E
	public void SetSymbolScale(int symbol_index, float scale)
	{
		DebugUtil.Assert(symbol_index < this.symbolCount);
		if (this.symbolInstances[symbol_index].scale != scale)
		{
			this.symbolInstances[symbol_index].scale = scale;
			this.MarkDirty();
		}
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x00009F9C File Offset: 0x0000819C
	public void SetSymbolTint(int symbol_index, Color color)
	{
		DebugUtil.Assert(symbol_index < this.symbolCount);
		if (this.symbolInstances[symbol_index].color != color)
		{
			this.symbolInstances[symbol_index].color = color;
			this.MarkDirty();
		}
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00009FE8 File Offset: 0x000081E8
	public void WriteToTexture(NativeArray<byte> data, int data_idx, int instance_idx)
	{
		NativeArray<byte>.Copy(this.symbolInstancesConverter.bytes, 0, data, data_idx, this.symbolCount * 8 * 4);
	}

	// Token: 0x040000A6 RID: 166
	public const int FLOATS_PER_SYMBOL_INSTANCE = 8;

	// Token: 0x040000A7 RID: 167
	private SymbolInstanceGpuData.SymbolInstanceToByteConverter symbolInstancesConverter;

	// Token: 0x040000A8 RID: 168
	private int symbolCount;

	// Token: 0x02000971 RID: 2417
	[StructLayout(LayoutKind.Explicit)]
	public struct SymbolInstance
	{
		// Token: 0x040020B8 RID: 8376
		[FieldOffset(0)]
		public float symbolIndex;

		// Token: 0x040020B9 RID: 8377
		[FieldOffset(4)]
		public float isVisible;

		// Token: 0x040020BA RID: 8378
		[FieldOffset(8)]
		public float scale;

		// Token: 0x040020BB RID: 8379
		[FieldOffset(12)]
		public float unused;

		// Token: 0x040020BC RID: 8380
		[FieldOffset(16)]
		public Color color;
	}

	// Token: 0x02000972 RID: 2418
	[StructLayout(LayoutKind.Explicit)]
	public struct SymbolInstanceToByteConverter
	{
		// Token: 0x040020BD RID: 8381
		[FieldOffset(0)]
		public byte[] bytes;

		// Token: 0x040020BE RID: 8382
		[FieldOffset(0)]
		public SymbolInstanceGpuData.SymbolInstance[] symbolInstances;
	}
}
