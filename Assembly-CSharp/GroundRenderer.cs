using System;
using System.Collections.Generic;
using ProcGen;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020009E0 RID: 2528
[AddComponentMenu("KMonoBehaviour/scripts/GroundRenderer")]
public class GroundRenderer : KMonoBehaviour
{
	// Token: 0x06004B8B RID: 19339 RVA: 0x001A845C File Offset: 0x001A665C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ShaderReloader.Register(new System.Action(this.OnShadersReloaded));
		this.OnShadersReloaded();
		this.masks.Initialize();
		SubWorld.ZoneType[] array = (SubWorld.ZoneType[])Enum.GetValues(typeof(SubWorld.ZoneType));
		this.biomeMasks = new GroundMasks.BiomeMaskData[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			SubWorld.ZoneType zoneType = array[i];
			this.biomeMasks[i] = this.GetBiomeMask(zoneType);
		}
	}

	// Token: 0x06004B8C RID: 19340 RVA: 0x001A84D8 File Offset: 0x001A66D8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.size = new Vector2I((Grid.WidthInCells + 16 - 1) / 16, (Grid.HeightInCells + 16 - 1) / 16);
		this.dirtyChunks = new bool[this.size.x, this.size.y];
		this.worldChunks = new GroundRenderer.WorldChunk[this.size.x, this.size.y];
		for (int i = 0; i < this.size.y; i++)
		{
			for (int j = 0; j < this.size.x; j++)
			{
				this.worldChunks[j, i] = new GroundRenderer.WorldChunk(j, i);
				this.dirtyChunks[j, i] = true;
			}
		}
	}

	// Token: 0x06004B8D RID: 19341 RVA: 0x001A85A0 File Offset: 0x001A67A0
	public void Render(Vector2I vis_min, Vector2I vis_max, bool forceVisibleRebuild = false)
	{
		if (!base.enabled)
		{
			return;
		}
		int num = LayerMask.NameToLayer("World");
		Vector2I vector2I = new Vector2I(vis_min.x / 16, vis_min.y / 16);
		Vector2I vector2I2 = new Vector2I((vis_max.x + 16 - 1) / 16, (vis_max.y + 16 - 1) / 16);
		for (int i = vector2I.y; i < vector2I2.y; i++)
		{
			for (int j = vector2I.x; j < vector2I2.x; j++)
			{
				GroundRenderer.WorldChunk worldChunk = this.worldChunks[j, i];
				if (this.dirtyChunks[j, i] || forceVisibleRebuild)
				{
					this.dirtyChunks[j, i] = false;
					worldChunk.Rebuild(this.biomeMasks, this.elementMaterials);
				}
				worldChunk.Render(num);
			}
		}
		this.RebuildDirtyChunks();
	}

	// Token: 0x06004B8E RID: 19342 RVA: 0x001A867F File Offset: 0x001A687F
	public void RenderAll()
	{
		this.Render(new Vector2I(0, 0), new Vector2I(this.worldChunks.GetLength(0) * 16, this.worldChunks.GetLength(1) * 16), true);
	}

	// Token: 0x06004B8F RID: 19343 RVA: 0x001A86B4 File Offset: 0x001A68B4
	private void RebuildDirtyChunks()
	{
		for (int i = 0; i < this.dirtyChunks.GetLength(1); i++)
		{
			for (int j = 0; j < this.dirtyChunks.GetLength(0); j++)
			{
				if (this.dirtyChunks[j, i])
				{
					this.dirtyChunks[j, i] = false;
					this.worldChunks[j, i].Rebuild(this.biomeMasks, this.elementMaterials);
				}
			}
		}
	}

	// Token: 0x06004B90 RID: 19344 RVA: 0x001A872C File Offset: 0x001A692C
	public void MarkDirty(int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2I vector2I2 = new Vector2I(vector2I.x / 16, vector2I.y / 16);
		this.dirtyChunks[vector2I2.x, vector2I2.y] = true;
		bool flag = vector2I.x % 16 == 0 && vector2I2.x > 0;
		bool flag2 = vector2I.x % 16 == 15 && vector2I2.x < this.size.x - 1;
		bool flag3 = vector2I.y % 16 == 0 && vector2I2.y > 0;
		bool flag4 = vector2I.y % 16 == 15 && vector2I2.y < this.size.y - 1;
		if (flag)
		{
			this.dirtyChunks[vector2I2.x - 1, vector2I2.y] = true;
			if (flag3)
			{
				this.dirtyChunks[vector2I2.x - 1, vector2I2.y - 1] = true;
			}
			if (flag4)
			{
				this.dirtyChunks[vector2I2.x - 1, vector2I2.y + 1] = true;
			}
		}
		if (flag3)
		{
			this.dirtyChunks[vector2I2.x, vector2I2.y - 1] = true;
		}
		if (flag4)
		{
			this.dirtyChunks[vector2I2.x, vector2I2.y + 1] = true;
		}
		if (flag2)
		{
			this.dirtyChunks[vector2I2.x + 1, vector2I2.y] = true;
			if (flag3)
			{
				this.dirtyChunks[vector2I2.x + 1, vector2I2.y - 1] = true;
			}
			if (flag4)
			{
				this.dirtyChunks[vector2I2.x + 1, vector2I2.y + 1] = true;
			}
		}
	}

	// Token: 0x06004B91 RID: 19345 RVA: 0x001A88E0 File Offset: 0x001A6AE0
	private Vector2I GetChunkIdx(int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		return new Vector2I(vector2I.x / 16, vector2I.y / 16);
	}

	// Token: 0x06004B92 RID: 19346 RVA: 0x001A890C File Offset: 0x001A6B0C
	private GroundMasks.BiomeMaskData GetBiomeMask(SubWorld.ZoneType zone_type)
	{
		GroundMasks.BiomeMaskData biomeMaskData = null;
		string text = zone_type.ToString().ToLower();
		this.masks.biomeMasks.TryGetValue(text, out biomeMaskData);
		return biomeMaskData;
	}

	// Token: 0x06004B93 RID: 19347 RVA: 0x001A8944 File Offset: 0x001A6B44
	private void InitOpaqueMaterial(Material material, Element element)
	{
		material.name = element.id.ToString() + "_opaque";
		material.renderQueue = RenderQueues.WorldOpaque;
		material.EnableKeyword("OPAQUE");
		material.DisableKeyword("ALPHA");
		this.ConfigureMaterialShine(material);
		material.SetInt("_SrcAlpha", 1);
		material.SetInt("_DstAlpha", 0);
		material.SetInt("_ZWrite", 1);
		material.SetTexture("_AlphaTestMap", Texture2D.whiteTexture);
	}

	// Token: 0x06004B94 RID: 19348 RVA: 0x001A89D0 File Offset: 0x001A6BD0
	private void InitAlphaMaterial(Material material, Element element)
	{
		material.name = element.id.ToString() + "_alpha";
		material.renderQueue = RenderQueues.WorldTransparent;
		material.EnableKeyword("ALPHA");
		material.DisableKeyword("OPAQUE");
		this.ConfigureMaterialShine(material);
		material.SetTexture("_AlphaTestMap", this.masks.maskAtlas.texture);
		material.SetInt("_SrcAlpha", 5);
		material.SetInt("_DstAlpha", 10);
		material.SetInt("_ZWrite", 0);
	}

	// Token: 0x06004B95 RID: 19349 RVA: 0x001A8A68 File Offset: 0x001A6C68
	private void ConfigureMaterialShine(Material material)
	{
		if (material.GetTexture("_ShineMask") != null)
		{
			material.DisableKeyword("MATTE");
			material.EnableKeyword("SHINY");
			return;
		}
		material.EnableKeyword("MATTE");
		material.DisableKeyword("SHINY");
	}

	// Token: 0x06004B96 RID: 19350 RVA: 0x001A8AB8 File Offset: 0x001A6CB8
	[ContextMenu("Reload Shaders")]
	public void OnShadersReloaded()
	{
		this.FreeMaterials();
		foreach (Element element in ElementLoader.elements)
		{
			if (element.IsSolid)
			{
				if (element.substance.material == null)
				{
					DebugUtil.LogErrorArgs(new object[] { element.name, "must have material associated with it in the substance table" });
				}
				Material material = new Material(element.substance.material);
				this.InitOpaqueMaterial(material, element);
				Material material2 = new Material(material);
				this.InitAlphaMaterial(material2, element);
				GroundRenderer.Materials materials = new GroundRenderer.Materials(material, material2);
				this.elementMaterials[element.id] = materials;
			}
		}
		if (this.worldChunks != null)
		{
			for (int i = 0; i < this.dirtyChunks.GetLength(1); i++)
			{
				for (int j = 0; j < this.dirtyChunks.GetLength(0); j++)
				{
					this.dirtyChunks[j, i] = true;
				}
			}
			GroundRenderer.WorldChunk[,] array = this.worldChunks;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int k = array.GetLowerBound(0); k <= upperBound; k++)
			{
				for (int l = array.GetLowerBound(1); l <= upperBound2; l++)
				{
					GroundRenderer.WorldChunk worldChunk = array[k, l];
					worldChunk.Clear();
					worldChunk.Rebuild(this.biomeMasks, this.elementMaterials);
				}
			}
		}
	}

	// Token: 0x06004B97 RID: 19351 RVA: 0x001A8C50 File Offset: 0x001A6E50
	public void FreeResources()
	{
		this.FreeMaterials();
		this.elementMaterials.Clear();
		this.elementMaterials = null;
		if (this.worldChunks != null)
		{
			GroundRenderer.WorldChunk[,] array = this.worldChunks;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					GroundRenderer.WorldChunk worldChunk = array[i, j];
					worldChunk.FreeResources();
				}
			}
			this.worldChunks = null;
		}
	}

	// Token: 0x06004B98 RID: 19352 RVA: 0x001A8CD8 File Offset: 0x001A6ED8
	private void FreeMaterials()
	{
		foreach (GroundRenderer.Materials materials in this.elementMaterials.Values)
		{
			UnityEngine.Object.Destroy(materials.opaque);
			UnityEngine.Object.Destroy(materials.alpha);
		}
		this.elementMaterials.Clear();
	}

	// Token: 0x0400317E RID: 12670
	[SerializeField]
	private GroundMasks masks;

	// Token: 0x0400317F RID: 12671
	private GroundMasks.BiomeMaskData[] biomeMasks;

	// Token: 0x04003180 RID: 12672
	private Dictionary<SimHashes, GroundRenderer.Materials> elementMaterials = new Dictionary<SimHashes, GroundRenderer.Materials>();

	// Token: 0x04003181 RID: 12673
	private bool[,] dirtyChunks;

	// Token: 0x04003182 RID: 12674
	private GroundRenderer.WorldChunk[,] worldChunks;

	// Token: 0x04003183 RID: 12675
	private const int ChunkEdgeSize = 16;

	// Token: 0x04003184 RID: 12676
	private Vector2I size;

	// Token: 0x020017EB RID: 6123
	[Serializable]
	private struct Materials
	{
		// Token: 0x06008C5D RID: 35933 RVA: 0x003021AC File Offset: 0x003003AC
		public Materials(Material opaque, Material alpha)
		{
			this.opaque = opaque;
			this.alpha = alpha;
		}

		// Token: 0x04006E5D RID: 28253
		public Material opaque;

		// Token: 0x04006E5E RID: 28254
		public Material alpha;
	}

	// Token: 0x020017EC RID: 6124
	private class ElementChunk
	{
		// Token: 0x06008C5E RID: 35934 RVA: 0x003021BC File Offset: 0x003003BC
		public ElementChunk(SimHashes element, Dictionary<SimHashes, GroundRenderer.Materials> materials)
		{
			this.element = element;
			GroundRenderer.Materials materials2 = materials[element];
			this.alpha = new GroundRenderer.ElementChunk.RenderData(materials2.alpha);
			this.opaque = new GroundRenderer.ElementChunk.RenderData(materials2.opaque);
			this.Clear();
		}

		// Token: 0x06008C5F RID: 35935 RVA: 0x00302206 File Offset: 0x00300406
		public void Clear()
		{
			this.opaque.Clear();
			this.alpha.Clear();
			this.tileCount = 0;
		}

		// Token: 0x06008C60 RID: 35936 RVA: 0x00302225 File Offset: 0x00300425
		public void AddOpaqueQuad(int x, int y, GroundMasks.UVData uvs)
		{
			this.opaque.AddQuad(x, y, uvs);
			this.tileCount++;
		}

		// Token: 0x06008C61 RID: 35937 RVA: 0x00302243 File Offset: 0x00300443
		public void AddAlphaQuad(int x, int y, GroundMasks.UVData uvs)
		{
			this.alpha.AddQuad(x, y, uvs);
			this.tileCount++;
		}

		// Token: 0x06008C62 RID: 35938 RVA: 0x00302261 File Offset: 0x00300461
		public void Build()
		{
			this.opaque.Build();
			this.alpha.Build();
		}

		// Token: 0x06008C63 RID: 35939 RVA: 0x0030227C File Offset: 0x0030047C
		public void Render(int layer, int element_idx)
		{
			float num = Grid.GetLayerZ(Grid.SceneLayer.Ground);
			num -= 0.0001f * (float)element_idx;
			this.opaque.Render(new Vector3(0f, 0f, num), layer);
			this.alpha.Render(new Vector3(0f, 0f, num), layer);
		}

		// Token: 0x06008C64 RID: 35940 RVA: 0x003022D4 File Offset: 0x003004D4
		public void FreeResources()
		{
			this.alpha.FreeResources();
			this.opaque.FreeResources();
			this.alpha = null;
			this.opaque = null;
		}

		// Token: 0x04006E5F RID: 28255
		public SimHashes element;

		// Token: 0x04006E60 RID: 28256
		private GroundRenderer.ElementChunk.RenderData alpha;

		// Token: 0x04006E61 RID: 28257
		private GroundRenderer.ElementChunk.RenderData opaque;

		// Token: 0x04006E62 RID: 28258
		public int tileCount;

		// Token: 0x020020DE RID: 8414
		private class RenderData
		{
			// Token: 0x0600A55C RID: 42332 RVA: 0x00349978 File Offset: 0x00347B78
			public RenderData(Material material)
			{
				this.material = material;
				this.mesh = new Mesh();
				this.mesh.MarkDynamic();
				this.mesh.name = "ElementChunk";
				this.pos = new List<Vector3>();
				this.uv = new List<Vector2>();
				this.indices = new List<int>();
			}

			// Token: 0x0600A55D RID: 42333 RVA: 0x003499D9 File Offset: 0x00347BD9
			public void ClearMesh()
			{
				if (this.mesh != null)
				{
					this.mesh.Clear();
					UnityEngine.Object.DestroyImmediate(this.mesh);
					this.mesh = null;
				}
			}

			// Token: 0x0600A55E RID: 42334 RVA: 0x00349A08 File Offset: 0x00347C08
			public void Clear()
			{
				if (this.mesh != null)
				{
					this.mesh.Clear();
				}
				if (this.pos != null)
				{
					this.pos.Clear();
				}
				if (this.uv != null)
				{
					this.uv.Clear();
				}
				if (this.indices != null)
				{
					this.indices.Clear();
				}
			}

			// Token: 0x0600A55F RID: 42335 RVA: 0x00349A67 File Offset: 0x00347C67
			public void FreeResources()
			{
				this.ClearMesh();
				this.Clear();
				this.pos = null;
				this.uv = null;
				this.indices = null;
				this.material = null;
			}

			// Token: 0x0600A560 RID: 42336 RVA: 0x00349A91 File Offset: 0x00347C91
			public void Build()
			{
				this.mesh.SetVertices(this.pos);
				this.mesh.SetUVs(0, this.uv);
				this.mesh.SetTriangles(this.indices, 0);
			}

			// Token: 0x0600A561 RID: 42337 RVA: 0x00349AC8 File Offset: 0x00347CC8
			public void AddQuad(int x, int y, GroundMasks.UVData uvs)
			{
				int count = this.pos.Count;
				this.indices.Add(count);
				this.indices.Add(count + 1);
				this.indices.Add(count + 3);
				this.indices.Add(count);
				this.indices.Add(count + 3);
				this.indices.Add(count + 2);
				this.pos.Add(new Vector3((float)x + -0.5f, (float)y + -0.5f, 0f));
				this.pos.Add(new Vector3((float)x + 1f + -0.5f, (float)y + -0.5f, 0f));
				this.pos.Add(new Vector3((float)x + -0.5f, (float)y + 1f + -0.5f, 0f));
				this.pos.Add(new Vector3((float)x + 1f + -0.5f, (float)y + 1f + -0.5f, 0f));
				this.uv.Add(uvs.bl);
				this.uv.Add(uvs.br);
				this.uv.Add(uvs.tl);
				this.uv.Add(uvs.tr);
			}

			// Token: 0x0600A562 RID: 42338 RVA: 0x00349C24 File Offset: 0x00347E24
			public void Render(Vector3 position, int layer)
			{
				if (this.pos.Count != 0)
				{
					Graphics.DrawMesh(this.mesh, position, Quaternion.identity, this.material, layer, null, 0, null, ShadowCastingMode.Off, false, null, false);
				}
			}

			// Token: 0x04009268 RID: 37480
			public Material material;

			// Token: 0x04009269 RID: 37481
			public Mesh mesh;

			// Token: 0x0400926A RID: 37482
			public List<Vector3> pos;

			// Token: 0x0400926B RID: 37483
			public List<Vector2> uv;

			// Token: 0x0400926C RID: 37484
			public List<int> indices;
		}
	}

	// Token: 0x020017ED RID: 6125
	private struct WorldChunk
	{
		// Token: 0x06008C65 RID: 35941 RVA: 0x003022FA File Offset: 0x003004FA
		public WorldChunk(int x, int y)
		{
			this.chunkX = x;
			this.chunkY = y;
			this.elementChunks = new List<GroundRenderer.ElementChunk>();
		}

		// Token: 0x06008C66 RID: 35942 RVA: 0x00302315 File Offset: 0x00300515
		public void Clear()
		{
			this.elementChunks.Clear();
		}

		// Token: 0x06008C67 RID: 35943 RVA: 0x00302324 File Offset: 0x00300524
		private static void InsertSorted(Element element, Element[] array, int size)
		{
			int num = (int)element.id;
			for (int i = 0; i < size; i++)
			{
				Element element2 = array[i];
				if (element2.id > (SimHashes)num)
				{
					array[i] = element;
					element = element2;
					num = (int)element2.id;
				}
			}
			array[size] = element;
		}

		// Token: 0x06008C68 RID: 35944 RVA: 0x00302364 File Offset: 0x00300564
		public void Rebuild(GroundMasks.BiomeMaskData[] biomeMasks, Dictionary<SimHashes, GroundRenderer.Materials> materials)
		{
			foreach (GroundRenderer.ElementChunk elementChunk in this.elementChunks)
			{
				elementChunk.Clear();
			}
			Vector2I vector2I = new Vector2I(this.chunkX * 16, this.chunkY * 16);
			Vector2I vector2I2 = new Vector2I(Math.Min(Grid.WidthInCells, (this.chunkX + 1) * 16), Math.Min(Grid.HeightInCells, (this.chunkY + 1) * 16));
			for (int i = vector2I.y; i < vector2I2.y; i++)
			{
				int num = Math.Max(0, i - 1);
				int num2 = i;
				for (int j = vector2I.x; j < vector2I2.x; j++)
				{
					int num3 = Math.Max(0, j - 1);
					int num4 = j;
					int num5 = num * Grid.WidthInCells + num3;
					int num6 = num * Grid.WidthInCells + num4;
					int num7 = num2 * Grid.WidthInCells + num3;
					int num8 = num2 * Grid.WidthInCells + num4;
					GroundRenderer.WorldChunk.elements[0] = Grid.Element[num5];
					GroundRenderer.WorldChunk.elements[1] = Grid.Element[num6];
					GroundRenderer.WorldChunk.elements[2] = Grid.Element[num7];
					GroundRenderer.WorldChunk.elements[3] = Grid.Element[num8];
					GroundRenderer.WorldChunk.substances[0] = ((Grid.RenderedByWorld[num5] && GroundRenderer.WorldChunk.elements[0].IsSolid) ? GroundRenderer.WorldChunk.elements[0].substance.idx : (-1));
					GroundRenderer.WorldChunk.substances[1] = ((Grid.RenderedByWorld[num6] && GroundRenderer.WorldChunk.elements[1].IsSolid) ? GroundRenderer.WorldChunk.elements[1].substance.idx : (-1));
					GroundRenderer.WorldChunk.substances[2] = ((Grid.RenderedByWorld[num7] && GroundRenderer.WorldChunk.elements[2].IsSolid) ? GroundRenderer.WorldChunk.elements[2].substance.idx : (-1));
					GroundRenderer.WorldChunk.substances[3] = ((Grid.RenderedByWorld[num8] && GroundRenderer.WorldChunk.elements[3].IsSolid) ? GroundRenderer.WorldChunk.elements[3].substance.idx : (-1));
					GroundRenderer.WorldChunk.uniqueElements[0] = GroundRenderer.WorldChunk.elements[0];
					GroundRenderer.WorldChunk.InsertSorted(GroundRenderer.WorldChunk.elements[1], GroundRenderer.WorldChunk.uniqueElements, 1);
					GroundRenderer.WorldChunk.InsertSorted(GroundRenderer.WorldChunk.elements[2], GroundRenderer.WorldChunk.uniqueElements, 2);
					GroundRenderer.WorldChunk.InsertSorted(GroundRenderer.WorldChunk.elements[3], GroundRenderer.WorldChunk.uniqueElements, 3);
					int num9 = -1;
					int biomeIdx = GroundRenderer.WorldChunk.GetBiomeIdx(i * Grid.WidthInCells + j);
					GroundMasks.BiomeMaskData biomeMaskData = biomeMasks[biomeIdx];
					if (biomeMaskData == null)
					{
						biomeMaskData = biomeMasks[3];
					}
					for (int k = 0; k < GroundRenderer.WorldChunk.uniqueElements.Length; k++)
					{
						Element element = GroundRenderer.WorldChunk.uniqueElements[k];
						if (element.IsSolid)
						{
							int idx = element.substance.idx;
							if (idx != num9)
							{
								num9 = idx;
								int num10 = (((GroundRenderer.WorldChunk.substances[2] >= idx) ? 1 : 0) << 3) | (((GroundRenderer.WorldChunk.substances[3] >= idx) ? 1 : 0) << 2) | (((GroundRenderer.WorldChunk.substances[0] >= idx) ? 1 : 0) << 1) | ((GroundRenderer.WorldChunk.substances[1] >= idx) ? 1 : 0);
								if (num10 > 0)
								{
									GroundMasks.UVData[] variationUVs = biomeMaskData.tiles[num10].variationUVs;
									float staticRandom = GroundRenderer.WorldChunk.GetStaticRandom(j, i);
									int num11 = Mathf.Min(variationUVs.Length - 1, (int)((float)variationUVs.Length * staticRandom));
									GroundMasks.UVData uvdata = variationUVs[num11 % variationUVs.Length];
									GroundRenderer.ElementChunk elementChunk2 = this.GetElementChunk(element.id, materials);
									if (num10 == 15)
									{
										elementChunk2.AddOpaqueQuad(j, i, uvdata);
									}
									else
									{
										elementChunk2.AddAlphaQuad(j, i, uvdata);
									}
								}
							}
						}
					}
				}
			}
			foreach (GroundRenderer.ElementChunk elementChunk3 in this.elementChunks)
			{
				elementChunk3.Build();
			}
			for (int l = this.elementChunks.Count - 1; l >= 0; l--)
			{
				if (this.elementChunks[l].tileCount == 0)
				{
					int num12 = this.elementChunks.Count - 1;
					this.elementChunks[l] = this.elementChunks[num12];
					this.elementChunks.RemoveAt(num12);
				}
			}
		}

		// Token: 0x06008C69 RID: 35945 RVA: 0x003027C0 File Offset: 0x003009C0
		private GroundRenderer.ElementChunk GetElementChunk(SimHashes elementID, Dictionary<SimHashes, GroundRenderer.Materials> materials)
		{
			GroundRenderer.ElementChunk elementChunk = null;
			for (int i = 0; i < this.elementChunks.Count; i++)
			{
				if (this.elementChunks[i].element == elementID)
				{
					elementChunk = this.elementChunks[i];
					break;
				}
			}
			if (elementChunk == null)
			{
				elementChunk = new GroundRenderer.ElementChunk(elementID, materials);
				this.elementChunks.Add(elementChunk);
			}
			return elementChunk;
		}

		// Token: 0x06008C6A RID: 35946 RVA: 0x00302820 File Offset: 0x00300A20
		private static int GetBiomeIdx(int cell)
		{
			if (!Grid.IsValidCell(cell))
			{
				return 0;
			}
			int num = 3;
			if (global::World.Instance != null && global::World.Instance.zoneRenderData != null)
			{
				global::World.Instance.zoneRenderData.GetSubWorldZoneType(cell);
			}
			return num;
		}

		// Token: 0x06008C6B RID: 35947 RVA: 0x0030285D File Offset: 0x00300A5D
		private static float GetStaticRandom(int x, int y)
		{
			return PerlinSimplexNoise.noise((float)x * GroundRenderer.WorldChunk.NoiseScale.x, (float)y * GroundRenderer.WorldChunk.NoiseScale.y);
		}

		// Token: 0x06008C6C RID: 35948 RVA: 0x00302880 File Offset: 0x00300A80
		public void Render(int layer)
		{
			for (int i = 0; i < this.elementChunks.Count; i++)
			{
				GroundRenderer.ElementChunk elementChunk = this.elementChunks[i];
				elementChunk.Render(layer, ElementLoader.FindElementByHash(elementChunk.element).substance.idx);
			}
		}

		// Token: 0x06008C6D RID: 35949 RVA: 0x003028CC File Offset: 0x00300ACC
		public void FreeResources()
		{
			foreach (GroundRenderer.ElementChunk elementChunk in this.elementChunks)
			{
				elementChunk.FreeResources();
			}
			this.elementChunks.Clear();
			this.elementChunks = null;
		}

		// Token: 0x04006E63 RID: 28259
		public readonly int chunkX;

		// Token: 0x04006E64 RID: 28260
		public readonly int chunkY;

		// Token: 0x04006E65 RID: 28261
		private List<GroundRenderer.ElementChunk> elementChunks;

		// Token: 0x04006E66 RID: 28262
		private static Element[] elements = new Element[4];

		// Token: 0x04006E67 RID: 28263
		private static Element[] uniqueElements = new Element[4];

		// Token: 0x04006E68 RID: 28264
		private static int[] substances = new int[4];

		// Token: 0x04006E69 RID: 28265
		private static Vector2 NoiseScale = new Vector3(1f, 1f);
	}
}
