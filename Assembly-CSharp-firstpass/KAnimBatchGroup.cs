using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class KAnimBatchGroup
{
	// Token: 0x060000C9 RID: 201 RVA: 0x000052C4 File Offset: 0x000034C4
	public static void FinalizeTextureCache()
	{
		KAnimBatchGroup.cache.Finalise();
	}

	// Token: 0x060000CA RID: 202 RVA: 0x000052D0 File Offset: 0x000034D0
	private Material CreateMaterial(KAnimBatchGroup.MaterialType material_type)
	{
		Material material = null;
		switch (material_type)
		{
		case KAnimBatchGroup.MaterialType.Simple:
			material = new Material(Shader.Find("Klei/AnimationSimple"));
			goto IL_74;
		case KAnimBatchGroup.MaterialType.UI:
			material = new Material(Shader.Find("Klei/BatchedAnimationUI"));
			goto IL_74;
		case KAnimBatchGroup.MaterialType.Overlay:
			global::Debug.LogError("MaterialType.Overlay no longer supported.");
			goto IL_74;
		case KAnimBatchGroup.MaterialType.Human:
			material = new Material(Shader.Find("Klei/BatchedAnimationHuman"));
			goto IL_74;
		}
		material = new Material(Shader.Find("Klei/BatchedAnimation"));
		IL_74:
		material.name = "Material:" + this.batchID.ToString();
		material.SetFloat(KAnimBatchGroup.ShaderProperty_SYMBOLS_PER_BUILD, (float)this.data.maxSymbolsPerBuild);
		material.SetFloat(KAnimBatchGroup.ShaderProperty_ANIM_TEXTURE_START_OFFSET, (float)(this.data.animDataStartOffset / 4));
		material.SetFloat(KAnimBatchGroup.ShaderProperty_SYMBOL_OVERRIDES_PER_BUILD, (float)this.data.symbolFrameInstances.Count);
		return material;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000053C4 File Offset: 0x000035C4
	public Material GetMaterial(KAnimBatchGroup.MaterialType material_type)
	{
		if (this.materials[(int)material_type] == null)
		{
			this.materials[(int)material_type] = this.CreateMaterial(material_type);
		}
		return this.materials[(int)material_type];
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060000CC RID: 204 RVA: 0x000053FA File Offset: 0x000035FA
	// (set) Token: 0x060000CD RID: 205 RVA: 0x00005402 File Offset: 0x00003602
	public int maxGroupSize { get; private set; }

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060000CE RID: 206 RVA: 0x0000540B File Offset: 0x0000360B
	// (set) Token: 0x060000CF RID: 207 RVA: 0x00005413 File Offset: 0x00003613
	public Mesh mesh { get; private set; }

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000541C File Offset: 0x0000361C
	// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005424 File Offset: 0x00003624
	public HashedString batchID { get; private set; }

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000542D File Offset: 0x0000362D
	// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005435 File Offset: 0x00003635
	public KBatchGroupData data { get; private set; }

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000543E File Offset: 0x0000363E
	// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005446 File Offset: 0x00003646
	public KAnimBatchGroup.KAnimBatchTextureCache.Entry buildAndAnimTex { get; private set; }

	// Token: 0x060000D6 RID: 214 RVA: 0x00005450 File Offset: 0x00003650
	public KAnimBatchGroup(HashedString id)
	{
		this.data = KAnimBatchManager.Instance().GetBatchGroupData(id);
		this.materials = new Material[6];
		this.batchID = id;
		KAnimGroupFile.Group group = KAnimGroupFile.GetGroup(id);
		if (group == null)
		{
			return;
		}
		this.maxGroupSize = group.maxGroupSize;
		if (this.maxGroupSize <= 0)
		{
			this.maxGroupSize = 30;
		}
		this.SetupMeshData();
		this.InitBuildAndAnimTex();
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060000D7 RID: 215 RVA: 0x000054C8 File Offset: 0x000036C8
	public bool InitOK
	{
		get
		{
			return this.size.x > 0;
		}
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x000054D8 File Offset: 0x000036D8
	public void FreeResources()
	{
		if (this.buildAndAnimTex != null)
		{
			KAnimBatchGroup.cache.Free(this.buildAndAnimTex);
			this.buildAndAnimTex = null;
		}
		for (int i = 0; i < 6; i++)
		{
			if (this.materials[i] != null)
			{
				UnityEngine.Object.Destroy(this.materials[i]);
				this.materials[i] = null;
			}
		}
		if (this.mesh != null)
		{
			UnityEngine.Object.Destroy(this.mesh);
			this.mesh = null;
		}
		if (this.data != null)
		{
			this.data.FreeResources();
		}
		this.data = null;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00005570 File Offset: 0x00003770
	public static Vector2I GetBestTextureSize(int cost)
	{
		int num = MathUtil.RoundToNextPowerOfTwo(Mathf.CeilToInt(Mathf.Sqrt((float)cost)));
		int num2 = (cost - 1) / num + 1;
		int num3 = 16;
		num2 = Mathf.CeilToInt((float)num2 / (float)num3) * num3;
		return new Vector2I(num, num2);
	}

	// Token: 0x060000DA RID: 218 RVA: 0x000055B0 File Offset: 0x000037B0
	private void SetupMeshData()
	{
		global::Debug.Assert(this.maxGroupSize > 0, "Group size must be >0");
		this.maxGroupSize = Mathf.Min(this.maxGroupSize, 30);
		this.mesh = this.BuildMesh(this.maxGroupSize * this.data.maxVisibleSymbols);
		int num = Mathf.CeilToInt((float)(this.maxGroupSize * 28) / 4f);
		this.size = KAnimBatchGroup.GetBestTextureSize(num);
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00005623 File Offset: 0x00003823
	private int GetBuildDataSize()
	{
		return Mathf.CeilToInt((float)(this.data.GetBuildSymbolFrameCount() * 16) / 4f);
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00005640 File Offset: 0x00003840
	private int GetAnimDataSize()
	{
		int num = 4;
		List<KAnim.Anim.Frame> animFrames = this.data.GetAnimFrames();
		if (animFrames.Count == 0)
		{
			num += this.data.symbolFrameInstances.Count * 4;
			num += this.data.symbolFrameInstances.Count * 12;
		}
		else
		{
			num += animFrames.Count * 4;
			List<KAnim.Anim.FrameElement> animFrameElements = this.data.GetAnimFrameElements();
			num += animFrameElements.Count * 12;
		}
		return Mathf.CeilToInt((float)num / 4f);
	}

	// Token: 0x060000DD RID: 221 RVA: 0x000056C0 File Offset: 0x000038C0
	public void InitBuildAndAnimTex()
	{
		int num = this.GetBuildDataSize() + this.GetAnimDataSize();
		Vector2I bestTextureSize = KAnimBatchGroup.GetBestTextureSize(num);
		this.buildAndAnimTex = KAnimBatchGroup.cache.Get(bestTextureSize.x, bestTextureSize.y, KAnimBatchGroup.ShaderProperty_buildAndAnimTex, KAnimBatchGroup.ShaderProperty_BUILD_AND_ANIM_TEXTURE_SIZE);
		this.buildAndAnimTex.name = "BuildAndAnimData:" + this.batchID.ToString();
		if (num > this.buildAndAnimTex.width * this.buildAndAnimTex.height)
		{
			global::Debug.LogErrorFormat("Texture is the wrong size! {0} <= {1}", new object[]
			{
				num,
				this.buildAndAnimTex.width * this.buildAndAnimTex.height
			});
		}
		NativeArray<float> floatDataPointer = this.buildAndAnimTex.GetFloatDataPointer();
		int num2 = this.data.WriteBuildData(this.data.symbolFrameInstances, floatDataPointer);
		this.data.WriteAnimData(num2, floatDataPointer);
		this.buildAndAnimTex.texture.Apply(false, true);
	}

	// Token: 0x060000DE RID: 222 RVA: 0x000057C8 File Offset: 0x000039C8
	private Mesh BuildMesh(int numQuads)
	{
		Mesh mesh = new Mesh();
		int[] array = new int[numQuads * 6];
		for (int i = 0; i < numQuads; i++)
		{
			int num = i * 6;
			int num2 = i * 4;
			array[num] = num2;
			array[num + 1] = num2 + 1;
			array[num + 2] = num2 + 2;
			array[num + 3] = num2 + 1;
			array[num + 4] = num2 + 2;
			array[num + 5] = num2 + 3;
		}
		Vector3[] array2 = new Vector3[numQuads * 4];
		Vector3[] array3 = new Vector3[numQuads * 4];
		for (int j = 0; j < numQuads; j++)
		{
			int num3 = j * 4;
			array2[num3] = Vector3.zero;
			array2[num3 + 1] = Vector3.zero;
			array2[num3 + 2] = Vector3.zero;
			array2[num3 + 3] = Vector3.zero;
			int num4 = j / this.data.maxVisibleSymbols;
			int num5 = this.data.maxVisibleSymbols - j % this.data.maxVisibleSymbols - 1;
			array3[num3] = new Vector3((float)num4, (float)num5, 0f);
			array3[num3 + 1] = new Vector3((float)num4, (float)num5, 1f);
			array3[num3 + 2] = new Vector3((float)num4, (float)num5, 2f);
			array3[num3 + 3] = new Vector3((float)num4, (float)num5, 3f);
		}
		mesh.name = "BatchGroup:" + this.batchID.ToString();
		mesh.vertices = array2;
		mesh.SetUVs(0, array3);
		mesh.SetIndices(array, MeshTopology.Triangles, 0);
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));
		return mesh;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00005996 File Offset: 0x00003B96
	public KAnimBatchGroup.KAnimBatchTextureCache.Entry CreateTexture(string name, int width, int height, int texture_property_id, int texture_size_property_id)
	{
		DebugUtil.Assert(width > 0 && height > 0);
		KAnimBatchGroup.KAnimBatchTextureCache.Entry entry = KAnimBatchGroup.cache.Get(width, height, texture_property_id, texture_size_property_id);
		entry.name = name;
		return entry;
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x000059C0 File Offset: 0x00003BC0
	public KAnimBatchGroup.KAnimBatchTextureCache.Entry CreateTexture()
	{
		if (this.size.x <= 0)
		{
			global::Debug.LogErrorFormat("Need to init AnimBatchGroup [{0}] first!", new object[] { this.batchID });
		}
		return this.CreateTexture("InstanceData:" + this.batchID.ToString(), this.size.x, this.size.y, KAnimBatchGroup.ShaderProperty_instanceTex, KAnimBatchGroup.ShaderProperty_INSTANCE_TEXTURE_SIZE);
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00005A3D File Offset: 0x00003C3D
	public void FreeTexture(KAnimBatchGroup.KAnimBatchTextureCache.Entry entry)
	{
		KAnimBatchGroup.cache.Free(entry);
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00005A4C File Offset: 0x00003C4C
	public void GetDataTextures(MaterialPropertyBlock matProperties, KAnimBatch.AtlasList atlases)
	{
		if (this.buildAndAnimTex != null)
		{
			this.buildAndAnimTex.SetTextureAndSize(matProperties);
		}
		matProperties.SetFloat(KAnimBatchGroup.ShaderProperty_ANIM_TEXTURE_START_OFFSET, (float)(this.data.animDataStartOffset / 4));
		for (int i = 0; i < this.data.textures.Count; i++)
		{
			atlases.Add(this.data.textures[i]);
		}
	}

	// Token: 0x04000032 RID: 50
	public static int ShaderProperty_SYMBOLS_PER_BUILD = Shader.PropertyToID("SYMBOLS_PER_BUILD");

	// Token: 0x04000033 RID: 51
	public static int ShaderProperty_ANIM_TEXTURE_START_OFFSET = Shader.PropertyToID("ANIM_TEXTURE_START_OFFSET");

	// Token: 0x04000034 RID: 52
	public static int ShaderProperty_SYMBOL_OVERRIDES_PER_BUILD = Shader.PropertyToID("SYMBOL_OVERRIDES_PER_BUILD");

	// Token: 0x04000035 RID: 53
	private static Color ResetColor = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04000036 RID: 54
	private static KAnimBatchGroup.KAnimBatchTextureCache cache = new KAnimBatchGroup.KAnimBatchTextureCache();

	// Token: 0x04000037 RID: 55
	public int batchCount;

	// Token: 0x0400003A RID: 58
	private Vector2I size = new Vector2I(0, 0);

	// Token: 0x0400003E RID: 62
	private static int ShaderProperty_BUILD_AND_ANIM_TEXTURE_SIZE = Shader.PropertyToID("BUILD_AND_ANIM_TEXTURE_SIZE");

	// Token: 0x0400003F RID: 63
	private static int ShaderProperty_buildAndAnimTex = Shader.PropertyToID("buildAndAnimTex");

	// Token: 0x04000040 RID: 64
	private static int ShaderProperty_INSTANCE_TEXTURE_SIZE = Shader.PropertyToID("INSTANCE_TEXTURE_SIZE");

	// Token: 0x04000041 RID: 65
	private static int ShaderProperty_instanceTex = Shader.PropertyToID("instanceTex");

	// Token: 0x04000042 RID: 66
	private Material[] materials;

	// Token: 0x0200095D RID: 2397
	public class KAnimBatchTextureCache
	{
		// Token: 0x060052BD RID: 21181 RVA: 0x0009AF5C File Offset: 0x0009915C
		public KAnimBatchGroup.KAnimBatchTextureCache.Entry Get(int float4s_width, int float4s_height, int texture_property_id, int texture_size_property_id)
		{
			Vector2I vector2I = new Vector2I(float4s_width, float4s_height);
			List<KAnimBatchGroup.KAnimBatchTextureCache.Entry> list = null;
			if (!this.unused.TryGetValue(vector2I, out list))
			{
				list = new List<KAnimBatchGroup.KAnimBatchTextureCache.Entry>();
				this.unused.Add(vector2I, list);
			}
			KAnimBatchGroup.KAnimBatchTextureCache.Entry entry;
			if (list.Count > 0)
			{
				int num = list.Count - 1;
				entry = list[num];
				list.RemoveAt(num);
			}
			else
			{
				entry = new KAnimBatchGroup.KAnimBatchTextureCache.Entry(float4s_width, float4s_height);
			}
			entry.texturePropertyId = texture_property_id;
			entry.textureSizePropertyId = texture_size_property_id;
			List<KAnimBatchGroup.KAnimBatchTextureCache.Entry> list2 = null;
			if (!this.inuse.TryGetValue(vector2I, out list2))
			{
				list2 = new List<KAnimBatchGroup.KAnimBatchTextureCache.Entry>();
				this.inuse.Add(vector2I, list2);
			}
			list2.Add(entry);
			entry.cacheIndex = list2.Count - 1;
			return entry;
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x0009B014 File Offset: 0x00099214
		public void Free(KAnimBatchGroup.KAnimBatchTextureCache.Entry entry)
		{
			Vector2I vector2I = new Vector2I(entry.texture.width, entry.texture.height);
			int cacheIndex = entry.cacheIndex;
			entry.cacheIndex = -1;
			List<KAnimBatchGroup.KAnimBatchTextureCache.Entry> list = null;
			if (this.inuse.TryGetValue(vector2I, out list))
			{
				int num = list.Count - 1;
				if (num != cacheIndex)
				{
					KAnimBatchGroup.KAnimBatchTextureCache.Entry entry2 = list[num];
					entry2.cacheIndex = cacheIndex;
					list[cacheIndex] = entry2;
					list[num] = null;
					list.RemoveAt(num);
				}
			}
			List<KAnimBatchGroup.KAnimBatchTextureCache.Entry> list2 = null;
			if (this.unused.TryGetValue(vector2I, out list2))
			{
				list2.Add(entry);
			}
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x0009B0B4 File Offset: 0x000992B4
		public void Finalise()
		{
			foreach (KeyValuePair<Vector2I, List<KAnimBatchGroup.KAnimBatchTextureCache.Entry>> keyValuePair in this.inuse)
			{
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					UnityEngine.Object.Destroy(keyValuePair.Value[i].texture);
				}
			}
			this.inuse.Clear();
			foreach (KeyValuePair<Vector2I, List<KAnimBatchGroup.KAnimBatchTextureCache.Entry>> keyValuePair2 in this.unused)
			{
				for (int j = 0; j < keyValuePair2.Value.Count; j++)
				{
					UnityEngine.Object.Destroy(keyValuePair2.Value[j].texture);
				}
			}
			this.unused.Clear();
		}

		// Token: 0x04002061 RID: 8289
		private Dictionary<Vector2I, List<KAnimBatchGroup.KAnimBatchTextureCache.Entry>> unused = new Dictionary<Vector2I, List<KAnimBatchGroup.KAnimBatchTextureCache.Entry>>();

		// Token: 0x04002062 RID: 8290
		private Dictionary<Vector2I, List<KAnimBatchGroup.KAnimBatchTextureCache.Entry>> inuse = new Dictionary<Vector2I, List<KAnimBatchGroup.KAnimBatchTextureCache.Entry>>();

		// Token: 0x02000B41 RID: 2881
		public class Entry
		{
			// Token: 0x17000F02 RID: 3842
			// (get) Token: 0x060058A8 RID: 22696 RVA: 0x000A4C7C File Offset: 0x000A2E7C
			// (set) Token: 0x060058A9 RID: 22697 RVA: 0x000A4C84 File Offset: 0x000A2E84
			public Texture2D texture { get; private set; }

			// Token: 0x060058AA RID: 22698 RVA: 0x000A4C90 File Offset: 0x000A2E90
			public Entry(int float4s_width, int float4s_height)
			{
				DebugUtil.DevAssert(float4s_width > 0 && MathUtil.IsPowerOfTwo(float4s_width), string.Format("Width ({0}) must be a power of two in order for fast indexing of the texture to work!", float4s_width), null);
				this.texture = new Texture2D(float4s_width, float4s_height, TextureFormat.RGBAFloat, false);
				this.texture.wrapMode = TextureWrapMode.Clamp;
				this.texture.filterMode = FilterMode.Point;
				this.texture.anisoLevel = 0;
				int num = float4s_width * float4s_height;
				NativeArray<Color> rawTextureData = this.texture.GetRawTextureData<Color>();
				for (int i = 0; i < num; i++)
				{
					rawTextureData[i] = KAnimBatchGroup.ResetColor;
				}
				this.texture.Apply();
			}

			// Token: 0x060058AB RID: 22699 RVA: 0x000A4D38 File Offset: 0x000A2F38
			public void SetTextureAndSize(MaterialPropertyBlock property_block)
			{
				DebugUtil.DevAssert(this.width > 0 && MathUtil.IsPowerOfTwo(this.width), string.Format("Width ({0}) must be a power of two in order for fast indexing of the texture to work!", this.width), null);
				Vector2I vector2I = MathUtil.PowerOfTwoToMaskAndShift(this.width);
				property_block.SetTexture(this.texturePropertyId, this.texture);
				property_block.SetVector(this.textureSizePropertyId, new Vector4(this.texelSize.x, this.texelSize.y, (float)vector2I.x, (float)vector2I.y));
			}

			// Token: 0x060058AC RID: 22700 RVA: 0x000A4DCA File Offset: 0x000A2FCA
			public NativeArray<byte> GetDataPointer()
			{
				return this.texture.GetRawTextureData<byte>();
			}

			// Token: 0x060058AD RID: 22701 RVA: 0x000A4DD7 File Offset: 0x000A2FD7
			public NativeArray<float> GetFloatDataPointer()
			{
				return this.texture.GetRawTextureData<float>();
			}

			// Token: 0x060058AE RID: 22702 RVA: 0x000A4DE4 File Offset: 0x000A2FE4
			public void Apply()
			{
				this.texture.Apply();
			}

			// Token: 0x17000F03 RID: 3843
			// (get) Token: 0x060058AF RID: 22703 RVA: 0x000A4DF1 File Offset: 0x000A2FF1
			public int width
			{
				get
				{
					return this.texture.width;
				}
			}

			// Token: 0x17000F04 RID: 3844
			// (get) Token: 0x060058B0 RID: 22704 RVA: 0x000A4DFE File Offset: 0x000A2FFE
			public int height
			{
				get
				{
					return this.texture.height;
				}
			}

			// Token: 0x17000F05 RID: 3845
			// (get) Token: 0x060058B1 RID: 22705 RVA: 0x000A4E0B File Offset: 0x000A300B
			public Vector2 texelSize
			{
				get
				{
					return this.texture.texelSize;
				}
			}

			// Token: 0x17000F06 RID: 3846
			// (get) Token: 0x060058B2 RID: 22706 RVA: 0x000A4E18 File Offset: 0x000A3018
			// (set) Token: 0x060058B3 RID: 22707 RVA: 0x000A4E25 File Offset: 0x000A3025
			public string name
			{
				get
				{
					return this.texture.name;
				}
				set
				{
					this.texture.name = value;
				}
			}

			// Token: 0x04002686 RID: 9862
			public int texturePropertyId;

			// Token: 0x04002687 RID: 9863
			public int textureSizePropertyId;

			// Token: 0x04002688 RID: 9864
			public int cacheIndex = -1;
		}
	}

	// Token: 0x0200095E RID: 2398
	public enum RendererType
	{
		// Token: 0x04002064 RID: 8292
		Default,
		// Token: 0x04002065 RID: 8293
		UI,
		// Token: 0x04002066 RID: 8294
		StaticBatch,
		// Token: 0x04002067 RID: 8295
		DontRender,
		// Token: 0x04002068 RID: 8296
		AnimOnly
	}

	// Token: 0x0200095F RID: 2399
	public enum MaterialType
	{
		// Token: 0x0400206A RID: 8298
		Default,
		// Token: 0x0400206B RID: 8299
		Simple,
		// Token: 0x0400206C RID: 8300
		Placer,
		// Token: 0x0400206D RID: 8301
		UI,
		// Token: 0x0400206E RID: 8302
		Overlay,
		// Token: 0x0400206F RID: 8303
		Human,
		// Token: 0x04002070 RID: 8304
		NumMaterials
	}
}
