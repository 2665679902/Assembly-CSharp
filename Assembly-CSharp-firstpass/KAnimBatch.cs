using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class KAnimBatch
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000092 RID: 146 RVA: 0x00004769 File Offset: 0x00002969
	// (set) Token: 0x06000093 RID: 147 RVA: 0x00004771 File Offset: 0x00002971
	public int id { get; private set; }

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000094 RID: 148 RVA: 0x0000477A File Offset: 0x0000297A
	public bool dirty
	{
		get
		{
			return this.dirtySet.Count > 0;
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000095 RID: 149 RVA: 0x0000478A File Offset: 0x0000298A
	public int dirtyCount
	{
		get
		{
			return this.dirtySet.Count;
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000096 RID: 150 RVA: 0x00004797 File Offset: 0x00002997
	// (set) Token: 0x06000097 RID: 151 RVA: 0x0000479F File Offset: 0x0000299F
	public bool active { get; private set; }

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000098 RID: 152 RVA: 0x000047A8 File Offset: 0x000029A8
	public int size
	{
		get
		{
			return this.controllers.Count;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000099 RID: 153 RVA: 0x000047B5 File Offset: 0x000029B5
	// (set) Token: 0x0600009A RID: 154 RVA: 0x000047BD File Offset: 0x000029BD
	public Vector3 position { get; private set; }

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x0600009B RID: 155 RVA: 0x000047C6 File Offset: 0x000029C6
	// (set) Token: 0x0600009C RID: 156 RVA: 0x000047CE File Offset: 0x000029CE
	public int layer { get; private set; }

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x0600009D RID: 157 RVA: 0x000047D7 File Offset: 0x000029D7
	public List<KAnimConverter.IAnimConverter> Controllers
	{
		get
		{
			return this.controllers;
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x0600009E RID: 158 RVA: 0x000047DF File Offset: 0x000029DF
	// (set) Token: 0x0600009F RID: 159 RVA: 0x000047E7 File Offset: 0x000029E7
	public KAnimBatchGroup.MaterialType materialType { get; private set; }

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060000A0 RID: 160 RVA: 0x000047F0 File Offset: 0x000029F0
	// (set) Token: 0x060000A1 RID: 161 RVA: 0x000047F8 File Offset: 0x000029F8
	public HashedString batchGroup { get; private set; }

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004801 File Offset: 0x00002A01
	// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004809 File Offset: 0x00002A09
	public BatchSet batchset { get; private set; }

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004812 File Offset: 0x00002A12
	// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000481A File Offset: 0x00002A1A
	public KAnimBatchGroup group { get; private set; }

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004823 File Offset: 0x00002A23
	// (set) Token: 0x060000A7 RID: 167 RVA: 0x0000482B File Offset: 0x00002A2B
	public int writtenLastFrame { get; private set; }

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004834 File Offset: 0x00002A34
	// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000483C File Offset: 0x00002A3C
	public MaterialPropertyBlock matProperties { get; private set; }

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060000AA RID: 170 RVA: 0x00004845 File Offset: 0x00002A45
	// (set) Token: 0x060000AB RID: 171 RVA: 0x0000484D File Offset: 0x00002A4D
	public KAnimBatchGroup.KAnimBatchTextureCache.Entry dataTex { get; private set; }

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060000AC RID: 172 RVA: 0x00004856 File Offset: 0x00002A56
	// (set) Token: 0x060000AD RID: 173 RVA: 0x0000485E File Offset: 0x00002A5E
	public KAnimBatchGroup.KAnimBatchTextureCache.Entry symbolInstanceTex { get; private set; }

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060000AE RID: 174 RVA: 0x00004867 File Offset: 0x00002A67
	// (set) Token: 0x060000AF RID: 175 RVA: 0x0000486F File Offset: 0x00002A6F
	public KAnimBatchGroup.KAnimBatchTextureCache.Entry symbolOverrideInfoTex { get; private set; }

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004878 File Offset: 0x00002A78
	// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004880 File Offset: 0x00002A80
	public bool isSetup { get; private set; }

	// Token: 0x060000B2 RID: 178 RVA: 0x0000488C File Offset: 0x00002A8C
	public KAnimBatch(KAnimBatchGroup group, int layer, float z, KAnimBatchGroup.MaterialType material_type)
	{
		this.id = KAnimBatch.nextBatchId++;
		this.active = true;
		this.group = group;
		this.layer = layer;
		this.batchGroup = group.batchID;
		this.materialType = material_type;
		this.matProperties = new MaterialPropertyBlock();
		this.atlases = new KAnimBatch.AtlasList(0, KAnimBatchManager.MaxAtlasesByMaterialType[(int)this.materialType]);
		this.position = new Vector3(0f, 0f, z);
		this.symbolInstanceSlots = new KAnimBatch.SymbolInstanceSlot[group.maxGroupSize];
		this.symbolOverrideInfoSlots = new KAnimBatch.SymbolOverrideInfoSlot[group.maxGroupSize];
		this.isSetup = false;
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00004960 File Offset: 0x00002B60
	public void DestroyTex()
	{
		if (this.dataTex != null)
		{
			this.group.FreeTexture(this.dataTex);
			this.dataTex = null;
		}
		if (this.symbolInstanceTex != null)
		{
			this.group.FreeTexture(this.symbolInstanceTex);
			this.symbolInstanceTex = null;
		}
		if (this.symbolOverrideInfoTex != null)
		{
			this.group.FreeTexture(this.symbolOverrideInfoTex);
			this.symbolOverrideInfoTex = null;
		}
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x000049D0 File Offset: 0x00002BD0
	public void Init()
	{
		this.dataTex = this.group.CreateTexture();
		if (this.dataTex == null)
		{
			global::Debug.LogErrorFormat("Got null data texture from AnimBatchGroup [{0}]", new object[] { this.batchGroup });
		}
		Vector2I bestTextureSize = KAnimBatchGroup.GetBestTextureSize(this.group.data.maxSymbolsPerBuild * this.group.maxGroupSize * 8);
		this.symbolInstanceTex = this.group.CreateTexture("SymbolInstanceTex", bestTextureSize.x, bestTextureSize.y, KAnimBatch.ShaderProperty_symbolInstanceTex, KAnimBatch.ShaderProperty_SYMBOL_INSTANCE_TEXTURE_SIZE);
		int width = this.dataTex.width;
		if (width == 0)
		{
			global::Debug.LogWarning(string.Concat(new string[]
			{
				"Empty group [",
				this.group.batchID.ToString(),
				"] ",
				this.batchset.idx.ToString(),
				" (probably just anims)"
			}));
			return;
		}
		NativeArray<float> floatDataPointer = this.dataTex.GetFloatDataPointer();
		for (int i = 0; i < width * width; i++)
		{
			floatDataPointer[i * 4] = -1f;
			floatDataPointer[i * 4 + 1] = 0f;
			floatDataPointer[i * 4 + 2] = 0f;
			floatDataPointer[i * 4 + 3] = 0f;
		}
		this.isSetup = true;
		if (this.matProperties == null)
		{
			this.matProperties = new MaterialPropertyBlock();
		}
		this.dataTex.SetTextureAndSize(this.matProperties);
		this.symbolInstanceTex.SetTextureAndSize(this.matProperties);
		this.group.GetDataTextures(this.matProperties, this.atlases);
		this.atlases.Apply(this.matProperties);
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00004B9D File Offset: 0x00002D9D
	public void Clear()
	{
		this.DestroyTex();
		this.controllers.Clear();
		this.dirtySet.Clear();
		this.batchset = null;
		this.group = null;
		this.matProperties = null;
		this.dataTex = null;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00004BD8 File Offset: 0x00002DD8
	public void SetBatchSet(BatchSet newBatchSet)
	{
		if (this.batchset != null && this.batchset != newBatchSet)
		{
			this.batchset.RemoveBatch(this);
		}
		this.batchset = newBatchSet;
		if (this.batchset != null)
		{
			this.position = new Vector3((float)(this.batchset.idx.x * 32), (float)(this.batchset.idx.y * 32), this.position.z);
			this.active = this.batchset.active;
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00004C60 File Offset: 0x00002E60
	public bool Register(KAnimConverter.IAnimConverter controller)
	{
		if (this.dataTex == null || !this.isSetup)
		{
			this.Init();
		}
		if (!this.controllers.Contains(controller))
		{
			this.controllers.Add(controller);
			this.controllersToIdx[controller] = this.controllers.Count - 1;
			this.currentOffset += 28;
		}
		this.AddToDirty(this.controllers.IndexOf(controller));
		KAnimBatch batch = controller.GetBatch();
		if (batch != null)
		{
			batch.Deregister(controller);
		}
		controller.SetBatch(this);
		return true;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00004CF0 File Offset: 0x00002EF0
	public void OverrideZ(float z)
	{
		this.position = new Vector3(this.position.x, this.position.y, z);
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00004D14 File Offset: 0x00002F14
	public void SetLayer(int layer)
	{
		this.layer = layer;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00004D20 File Offset: 0x00002F20
	public void Deregister(KAnimConverter.IAnimConverter controller)
	{
		if (App.IsExiting)
		{
			return;
		}
		if (this.controllers.IndexOf(controller) >= 0)
		{
			if (!this.controllers.Remove(controller))
			{
				global::Debug.LogError("Failed to remove controller [" + controller.GetName() + "]");
			}
			controller.SetBatch(null);
			this.currentOffset -= 28;
			this.currentOffset = Mathf.Max(0, this.currentOffset);
			NativeArray<float> floatDataPointer = this.dataTex.GetFloatDataPointer();
			for (int i = 0; i < 28; i++)
			{
				floatDataPointer[this.currentOffset + i] = -1f;
			}
			this.dataTex.Apply();
			this.currentOffset = 28 * this.controllers.Count;
			this.ClearDirty();
			this.controllersToIdx.Clear();
			for (int j = 0; j < this.controllers.Count; j++)
			{
				this.controllersToIdx[this.controllers[j]] = j;
				this.AddToDirty(j);
			}
		}
		else
		{
			global::Debug.LogError("Deregister called for [" + controller.GetName() + "] but its not in this batch ");
		}
		if (this.controllers.Count == 0)
		{
			this.batchset.RemoveBatch(this);
			this.DestroyTex();
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00004E64 File Offset: 0x00003064
	private void ClearDirty()
	{
		this.needsWrite = false;
		this.dirtySet.Clear();
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00004E78 File Offset: 0x00003078
	private void AddToDirty(int dirtyIdx)
	{
		if (!this.dirtySet.Contains(dirtyIdx))
		{
			this.dirtySet.Add(dirtyIdx);
		}
		this.batchset.SetDirty();
		this.needsWrite = true;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00004EA6 File Offset: 0x000030A6
	public void Activate()
	{
		this.active = true;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00004EAF File Offset: 0x000030AF
	public void Deactivate()
	{
		this.active = false;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00004EB8 File Offset: 0x000030B8
	public void SetDirty(KAnimConverter.IAnimConverter controller)
	{
		int num;
		if (!this.controllersToIdx.TryGetValue(controller, out num))
		{
			global::Debug.LogError("Setting controller [" + controller.GetName() + "] to dirty but its not in this batch");
			return;
		}
		this.AddToDirty(num);
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00004EF7 File Offset: 0x000030F7
	private void WriteBatchedAnimInstanceData(int index, KAnimConverter.IAnimConverter controller, NativeArray<byte> data)
	{
		controller.GetBatchInstanceData().WriteToTexture(data, index * 112, index);
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00004F0C File Offset: 0x0000310C
	private bool WriteSymbolInstanceData(int index, KAnimConverter.IAnimConverter controller, NativeArray<byte> data)
	{
		bool flag = false;
		KAnimBatch.SymbolInstanceSlot symbolInstanceSlot = this.symbolInstanceSlots[index];
		if (symbolInstanceSlot.symbolInstanceData != controller.symbolInstanceGpuData || symbolInstanceSlot.dataVersion != controller.symbolInstanceGpuData.version)
		{
			controller.symbolInstanceGpuData.WriteToTexture(data, index * 8 * this.group.data.maxSymbolsPerBuild * 4, index);
			symbolInstanceSlot.symbolInstanceData = controller.symbolInstanceGpuData;
			symbolInstanceSlot.dataVersion = controller.symbolInstanceGpuData.version;
			this.symbolInstanceSlots[index] = symbolInstanceSlot;
			flag = true;
		}
		return flag;
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00004F9C File Offset: 0x0000319C
	private bool WriteSymbolOverrideInfoTex(int index, KAnimConverter.IAnimConverter controller, NativeArray<byte> data)
	{
		bool flag = false;
		KAnimBatch.SymbolOverrideInfoSlot symbolOverrideInfoSlot = this.symbolOverrideInfoSlots[index];
		if (symbolOverrideInfoSlot.symbolOverrideInfo != controller.symbolOverrideInfoGpuData || symbolOverrideInfoSlot.dataVersion != controller.symbolOverrideInfoGpuData.version)
		{
			controller.symbolOverrideInfoGpuData.WriteToTexture(data, index * 12 * this.group.data.maxSymbolFrameInstancesPerbuild * 4, index);
			symbolOverrideInfoSlot.symbolOverrideInfo = controller.symbolOverrideInfoGpuData;
			symbolOverrideInfoSlot.dataVersion = controller.symbolOverrideInfoGpuData.version;
			this.symbolOverrideInfoSlots[index] = symbolOverrideInfoSlot;
			flag = true;
		}
		return flag;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000502C File Offset: 0x0000322C
	public int UpdateDirty(int frame)
	{
		if (!this.needsWrite)
		{
			return 0;
		}
		if (this.dataTex == null || !this.isSetup)
		{
			this.Init();
		}
		this.writtenLastFrame = 0;
		bool flag = false;
		bool flag2 = false;
		NativeArray<byte> dataPointer = this.dataTex.GetDataPointer();
		NativeArray<byte> dataPointer2 = this.symbolInstanceTex.GetDataPointer();
		if (this.dirtySet.Count > 0)
		{
			foreach (int num in this.dirtySet)
			{
				KAnimConverter.IAnimConverter animConverter = this.controllers[num];
				if (animConverter != null && animConverter as UnityEngine.Object != null)
				{
					this.WriteBatchedAnimInstanceData(num, animConverter, dataPointer);
					bool flag3 = this.WriteSymbolInstanceData(num, animConverter, dataPointer2);
					flag = flag || flag3;
					if (animConverter.ApplySymbolOverrides())
					{
						if (this.symbolOverrideInfoTex == null)
						{
							Vector2I bestTextureSize = KAnimBatchGroup.GetBestTextureSize(this.group.data.maxSymbolFrameInstancesPerbuild * this.group.maxGroupSize * 12);
							this.symbolOverrideInfoTex = this.group.CreateTexture("SymbolOverrideInfoTex", bestTextureSize.x, bestTextureSize.y, KAnimBatch.ShaderProperty_symbolOverrideInfoTex, KAnimBatch.ShaderProperty_SYMBOL_OVERRIDE_INFO_TEXTURE_SIZE);
							this.symbolOverrideInfoTex.SetTextureAndSize(this.matProperties);
							this.matProperties.SetFloat(KAnimBatch.ShaderProperty_SUPPORTS_SYMBOL_OVERRIDING, 1f);
						}
						NativeArray<byte> dataPointer3 = this.symbolOverrideInfoTex.GetDataPointer();
						bool flag4 = this.WriteSymbolOverrideInfoTex(num, animConverter, dataPointer3);
						flag2 = flag2 || flag4;
					}
					int writtenLastFrame = this.writtenLastFrame;
					this.writtenLastFrame = writtenLastFrame + 1;
				}
			}
			if (this.writtenLastFrame != 0)
			{
				this.ClearDirty();
			}
			else
			{
				global::Debug.LogError("dirtySet not written");
			}
		}
		this.dataTex.Apply();
		if (flag)
		{
			this.symbolInstanceTex.Apply();
		}
		if (flag2)
		{
			this.symbolOverrideInfoTex.Apply();
		}
		return this.writtenLastFrame;
	}

	// Token: 0x04000014 RID: 20
	private List<KAnimConverter.IAnimConverter> controllers = new List<KAnimConverter.IAnimConverter>();

	// Token: 0x04000015 RID: 21
	private Dictionary<KAnimConverter.IAnimConverter, int> controllersToIdx = new Dictionary<KAnimConverter.IAnimConverter, int>();

	// Token: 0x04000016 RID: 22
	private List<int> dirtySet = new List<int>();

	// Token: 0x04000025 RID: 37
	private static int nextBatchId;

	// Token: 0x04000026 RID: 38
	private int currentOffset;

	// Token: 0x04000027 RID: 39
	private static int ShaderProperty_SYMBOL_INSTANCE_TEXTURE_SIZE = Shader.PropertyToID("SYMBOL_INSTANCE_TEXTURE_SIZE");

	// Token: 0x04000028 RID: 40
	private static int ShaderProperty_symbolInstanceTex = Shader.PropertyToID("symbolInstanceTex");

	// Token: 0x04000029 RID: 41
	private static int ShaderProperty_SYMBOL_OVERRIDE_INFO_TEXTURE_SIZE = Shader.PropertyToID("SYMBOL_OVERRIDE_INFO_TEXTURE_SIZE");

	// Token: 0x0400002A RID: 42
	private static int ShaderProperty_symbolOverrideInfoTex = Shader.PropertyToID("symbolOverrideInfoTex");

	// Token: 0x0400002B RID: 43
	public static int ShaderProperty_SUPPORTS_SYMBOL_OVERRIDING = Shader.PropertyToID("SUPPORTS_SYMBOL_OVERRIDING");

	// Token: 0x0400002C RID: 44
	public static int ShaderProperty_ANIM_TEXTURE_START_OFFSET = Shader.PropertyToID("ANIM_TEXTURE_START_OFFSET");

	// Token: 0x0400002D RID: 45
	private KAnimBatch.SymbolInstanceSlot[] symbolInstanceSlots;

	// Token: 0x0400002E RID: 46
	private KAnimBatch.SymbolOverrideInfoSlot[] symbolOverrideInfoSlots;

	// Token: 0x0400002F RID: 47
	public KAnimBatch.AtlasList atlases;

	// Token: 0x04000030 RID: 48
	private bool needsWrite;

	// Token: 0x0200095A RID: 2394
	public struct SymbolInstanceSlot
	{
		// Token: 0x04002059 RID: 8281
		public SymbolInstanceGpuData symbolInstanceData;

		// Token: 0x0400205A RID: 8282
		public int dataVersion;
	}

	// Token: 0x0200095B RID: 2395
	public struct SymbolOverrideInfoSlot
	{
		// Token: 0x0400205B RID: 8283
		public SymbolOverrideInfoGpuData symbolOverrideInfo;

		// Token: 0x0400205C RID: 8284
		public int dataVersion;
	}

	// Token: 0x0200095C RID: 2396
	public class AtlasList
	{
		// Token: 0x060052B5 RID: 21173 RVA: 0x0009AD2F File Offset: 0x00098F2F
		public AtlasList(int start_idx, int max)
		{
			this.startIdx = start_idx;
			this.maxAtlases = max;
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x0009AD50 File Offset: 0x00098F50
		public int Add(Texture2D atlas)
		{
			DebugUtil.Assert(atlas != null, "KAnimBatch Atlas is null");
			DebugUtil.Assert(this.atlases.Count < this.maxAtlases);
			int num = this.atlases.IndexOf(atlas);
			if (num == -1)
			{
				num = this.atlases.Count;
				this.atlases.Add(atlas);
			}
			return num + this.startIdx;
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x0009ADB8 File Offset: 0x00098FB8
		public void Apply(MaterialPropertyBlock material_property_block)
		{
			bool flag = false;
			for (int i = 0; i < this.atlases.Count; i++)
			{
				int num = this.startIdx + i;
				if (num >= this.maxAtlases)
				{
					flag = true;
				}
				else
				{
					Texture2D texture2D = this.atlases[i];
					Texture2D texture = StreamedTextures.GetTexture(texture2D.name);
					material_property_block.SetTexture(KAnimBatchManager.AtlasNames[num], (texture != null) ? texture : texture2D);
				}
			}
			if (flag && !KAnimBatch.AtlasList.reported_overflow)
			{
				string text = "Atlas overflow: (startIndex=" + this.startIdx.ToString() + ")\n";
				int num2 = 0;
				foreach (Texture2D texture2D2 in this.atlases)
				{
					text = string.Concat(new string[]
					{
						text,
						(this.startIdx + num2).ToString(),
						": ",
						texture2D2.name,
						"\n"
					});
					num2++;
				}
				global::Debug.LogWarning(text);
				KAnimBatch.AtlasList.reported_overflow = true;
			}
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x0009AEF0 File Offset: 0x000990F0
		public void Clear(int start_idx)
		{
			this.atlases.Clear();
			this.startIdx = start_idx;
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x0009AF04 File Offset: 0x00099104
		public int GetAtlasIdx(Texture2D atlas)
		{
			for (int i = 0; i < this.atlases.Count; i++)
			{
				if (this.atlases[i] == atlas)
				{
					return i + this.startIdx;
				}
			}
			return -1;
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x060052BA RID: 21178 RVA: 0x0009AF45 File Offset: 0x00099145
		public int Count
		{
			get
			{
				return this.atlases.Count;
			}
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x0009AF52 File Offset: 0x00099152
		public List<Texture2D> GetTextures()
		{
			return this.atlases;
		}

		// Token: 0x0400205D RID: 8285
		private List<Texture2D> atlases = new List<Texture2D>();

		// Token: 0x0400205E RID: 8286
		private int startIdx;

		// Token: 0x0400205F RID: 8287
		private int maxAtlases;

		// Token: 0x04002060 RID: 8288
		private static bool reported_overflow;
	}
}
