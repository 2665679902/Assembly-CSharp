using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering
{
	// Token: 0x02000C49 RID: 3145
	public class BlockTileRenderer : MonoBehaviour
	{
		// Token: 0x060063F7 RID: 25591 RVA: 0x0025758C File Offset: 0x0025578C
		public static BlockTileRenderer.RenderInfoLayer GetRenderInfoLayer(bool isReplacement, SimHashes element)
		{
			if (isReplacement)
			{
				return BlockTileRenderer.RenderInfoLayer.Replacement;
			}
			if (element == SimHashes.Void)
			{
				return BlockTileRenderer.RenderInfoLayer.UnderConstruction;
			}
			return BlockTileRenderer.RenderInfoLayer.Built;
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x0025759E File Offset: 0x0025579E
		public bool ForceRebuild
		{
			get
			{
				return this.forceRebuild;
			}
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x002575A8 File Offset: 0x002557A8
		public BlockTileRenderer()
		{
			this.forceRebuild = false;
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x0025762C File Offset: 0x0025582C
		public void FreeResources()
		{
			foreach (KeyValuePair<KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>, BlockTileRenderer.RenderInfo> keyValuePair in this.renderInfo)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.FreeResources();
				}
			}
			this.renderInfo.Clear();
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x00257698 File Offset: 0x00255898
		private static bool MatchesDef(GameObject go, BuildingDef def)
		{
			return go != null && go.GetComponent<Building>().Def == def;
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x002576B8 File Offset: 0x002558B8
		public virtual BlockTileRenderer.Bits GetConnectionBits(int x, int y, int query_layer)
		{
			BlockTileRenderer.Bits bits = (BlockTileRenderer.Bits)0;
			GameObject gameObject = Grid.Objects[y * Grid.WidthInCells + x, query_layer];
			BuildingDef buildingDef = ((gameObject != null) ? gameObject.GetComponent<Building>().Def : null);
			if (y > 0)
			{
				int num = (y - 1) * Grid.WidthInCells + x;
				if (x > 0 && BlockTileRenderer.MatchesDef(Grid.Objects[num - 1, query_layer], buildingDef))
				{
					bits |= BlockTileRenderer.Bits.DownLeft;
				}
				if (BlockTileRenderer.MatchesDef(Grid.Objects[num, query_layer], buildingDef))
				{
					bits |= BlockTileRenderer.Bits.Down;
				}
				if (x < Grid.WidthInCells - 1 && BlockTileRenderer.MatchesDef(Grid.Objects[num + 1, query_layer], buildingDef))
				{
					bits |= BlockTileRenderer.Bits.DownRight;
				}
			}
			int num2 = y * Grid.WidthInCells + x;
			if (x > 0 && BlockTileRenderer.MatchesDef(Grid.Objects[num2 - 1, query_layer], buildingDef))
			{
				bits |= BlockTileRenderer.Bits.Left;
			}
			if (x < Grid.WidthInCells - 1 && BlockTileRenderer.MatchesDef(Grid.Objects[num2 + 1, query_layer], buildingDef))
			{
				bits |= BlockTileRenderer.Bits.Right;
			}
			if (y < Grid.HeightInCells - 1)
			{
				int num3 = (y + 1) * Grid.WidthInCells + x;
				if (x > 0 && BlockTileRenderer.MatchesDef(Grid.Objects[num3 - 1, query_layer], buildingDef))
				{
					bits |= BlockTileRenderer.Bits.UpLeft;
				}
				if (BlockTileRenderer.MatchesDef(Grid.Objects[num3, query_layer], buildingDef))
				{
					bits |= BlockTileRenderer.Bits.Up;
				}
				if (x < Grid.WidthInCells + 1 && BlockTileRenderer.MatchesDef(Grid.Objects[num3 + 1, query_layer], buildingDef))
				{
					bits |= BlockTileRenderer.Bits.UpRight;
				}
			}
			return bits;
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x0025782C File Offset: 0x00255A2C
		private bool IsDecorConnectable(GameObject src, GameObject target)
		{
			if (src != null && target != null)
			{
				IBlockTileInfo component = src.GetComponent<IBlockTileInfo>();
				IBlockTileInfo component2 = target.GetComponent<IBlockTileInfo>();
				if (component != null && component2 != null)
				{
					return component.GetBlockTileConnectorID() == component2.GetBlockTileConnectorID();
				}
			}
			return false;
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x00257870 File Offset: 0x00255A70
		public virtual BlockTileRenderer.Bits GetDecorConnectionBits(int x, int y, int query_layer)
		{
			BlockTileRenderer.Bits bits = (BlockTileRenderer.Bits)0;
			GameObject gameObject = Grid.Objects[y * Grid.WidthInCells + x, query_layer];
			if (y > 0)
			{
				int num = (y - 1) * Grid.WidthInCells + x;
				if (x > 0 && Grid.Objects[num - 1, query_layer] != null)
				{
					bits |= BlockTileRenderer.Bits.DownLeft;
				}
				if (Grid.Objects[num, query_layer] != null)
				{
					bits |= BlockTileRenderer.Bits.Down;
				}
				if (x < Grid.WidthInCells - 1 && Grid.Objects[num + 1, query_layer] != null)
				{
					bits |= BlockTileRenderer.Bits.DownRight;
				}
			}
			int num2 = y * Grid.WidthInCells + x;
			if (x > 0 && this.IsDecorConnectable(gameObject, Grid.Objects[num2 - 1, query_layer]))
			{
				bits |= BlockTileRenderer.Bits.Left;
			}
			if (x < Grid.WidthInCells - 1 && this.IsDecorConnectable(gameObject, Grid.Objects[num2 + 1, query_layer]))
			{
				bits |= BlockTileRenderer.Bits.Right;
			}
			if (y < Grid.HeightInCells - 1)
			{
				int num3 = (y + 1) * Grid.WidthInCells + x;
				if (x > 0 && Grid.Objects[num3 - 1, query_layer] != null)
				{
					bits |= BlockTileRenderer.Bits.UpLeft;
				}
				if (Grid.Objects[num3, query_layer] != null)
				{
					bits |= BlockTileRenderer.Bits.Up;
				}
				if (x < Grid.WidthInCells + 1 && Grid.Objects[num3 + 1, query_layer] != null)
				{
					bits |= BlockTileRenderer.Bits.UpRight;
				}
			}
			return bits;
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x002579C8 File Offset: 0x00255BC8
		public void LateUpdate()
		{
			this.Render();
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x002579D0 File Offset: 0x00255BD0
		private void Render()
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			if (GameUtil.IsCapturingTimeLapse())
			{
				vector2I = new Vector2I(0, 0);
				vector2I2 = new Vector2I(Grid.WidthInCells / 16, Grid.HeightInCells / 16);
			}
			else
			{
				GridArea visibleArea = GridVisibleArea.GetVisibleArea();
				vector2I = new Vector2I(visibleArea.Min.x / 16, visibleArea.Min.y / 16);
				vector2I2 = new Vector2I((visibleArea.Max.x + 16 - 1) / 16, (visibleArea.Max.y + 16 - 1) / 16);
			}
			foreach (KeyValuePair<KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>, BlockTileRenderer.RenderInfo> keyValuePair in this.renderInfo)
			{
				BlockTileRenderer.RenderInfo value = keyValuePair.Value;
				for (int i = vector2I.y; i < vector2I2.y; i++)
				{
					for (int j = vector2I.x; j < vector2I2.x; j++)
					{
						value.Rebuild(this, j, i, MeshUtil.vertices, MeshUtil.uvs, MeshUtil.indices, MeshUtil.colours);
						value.Render(j, i);
					}
				}
			}
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x00257B08 File Offset: 0x00255D08
		public Color GetCellColour(int cell, SimHashes element)
		{
			Color white;
			if (cell == this.selectedCell)
			{
				white = this.selectColour;
			}
			else if (cell == this.invalidPlaceCell && element == SimHashes.Void)
			{
				white = this.invalidPlaceColour;
			}
			else if (cell == this.highlightCell)
			{
				white = this.highlightColour;
			}
			else
			{
				white = Color.white;
			}
			return white;
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x00257B5C File Offset: 0x00255D5C
		public static Vector2I GetChunkIdx(int cell)
		{
			Vector2I vector2I = Grid.CellToXY(cell);
			return new Vector2I(vector2I.x / 16, vector2I.y / 16);
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x00257B88 File Offset: 0x00255D88
		public void AddBlock(int renderLayer, BuildingDef def, bool isReplacement, SimHashes element, int cell)
		{
			KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer> keyValuePair = new KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>(def, BlockTileRenderer.GetRenderInfoLayer(isReplacement, element));
			BlockTileRenderer.RenderInfo renderInfo;
			if (!this.renderInfo.TryGetValue(keyValuePair, out renderInfo))
			{
				int num = (int)(isReplacement ? def.ReplacementLayer : def.TileLayer);
				renderInfo = new BlockTileRenderer.RenderInfo(this, num, renderLayer, def, element);
				this.renderInfo[keyValuePair] = renderInfo;
			}
			renderInfo.AddCell(cell);
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x00257BE8 File Offset: 0x00255DE8
		public void RemoveBlock(BuildingDef def, bool isReplacement, SimHashes element, int cell)
		{
			KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer> keyValuePair = new KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>(def, BlockTileRenderer.GetRenderInfoLayer(isReplacement, element));
			BlockTileRenderer.RenderInfo renderInfo;
			if (this.renderInfo.TryGetValue(keyValuePair, out renderInfo))
			{
				renderInfo.RemoveCell(cell);
			}
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x00257C1C File Offset: 0x00255E1C
		public void Rebuild(ObjectLayer layer, int cell)
		{
			foreach (KeyValuePair<KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>, BlockTileRenderer.RenderInfo> keyValuePair in this.renderInfo)
			{
				if (keyValuePair.Key.Key.TileLayer == layer)
				{
					keyValuePair.Value.MarkDirty(cell);
				}
			}
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x00257C8C File Offset: 0x00255E8C
		public void SelectCell(int cell, bool enabled)
		{
			this.UpdateCellStatus(ref this.selectedCell, cell, enabled);
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x00257C9C File Offset: 0x00255E9C
		public void HighlightCell(int cell, bool enabled)
		{
			this.UpdateCellStatus(ref this.highlightCell, cell, enabled);
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x00257CAC File Offset: 0x00255EAC
		public void SetInvalidPlaceCell(int cell, bool enabled)
		{
			this.UpdateCellStatus(ref this.invalidPlaceCell, cell, enabled);
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x00257CBC File Offset: 0x00255EBC
		private void UpdateCellStatus(ref int cell_status, int cell, bool enabled)
		{
			if (enabled)
			{
				if (cell == cell_status)
				{
					return;
				}
				if (cell_status != -1)
				{
					foreach (KeyValuePair<KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>, BlockTileRenderer.RenderInfo> keyValuePair in this.renderInfo)
					{
						keyValuePair.Value.MarkDirtyIfOccupied(cell_status);
					}
				}
				cell_status = cell;
				using (Dictionary<KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>, BlockTileRenderer.RenderInfo>.Enumerator enumerator = this.renderInfo.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>, BlockTileRenderer.RenderInfo> keyValuePair2 = enumerator.Current;
						keyValuePair2.Value.MarkDirtyIfOccupied(cell_status);
					}
					return;
				}
			}
			if (cell_status == cell)
			{
				foreach (KeyValuePair<KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>, BlockTileRenderer.RenderInfo> keyValuePair3 in this.renderInfo)
				{
					keyValuePair3.Value.MarkDirty(cell_status);
				}
				cell_status = -1;
			}
		}

		// Token: 0x04004537 RID: 17719
		[SerializeField]
		private bool forceRebuild;

		// Token: 0x04004538 RID: 17720
		[SerializeField]
		private Color highlightColour = new Color(1.25f, 1.25f, 1.25f, 1f);

		// Token: 0x04004539 RID: 17721
		[SerializeField]
		private Color selectColour = new Color(1.5f, 1.5f, 1.5f, 1f);

		// Token: 0x0400453A RID: 17722
		[SerializeField]
		private Color invalidPlaceColour = Color.red;

		// Token: 0x0400453B RID: 17723
		private const float TILE_ATLAS_WIDTH = 2048f;

		// Token: 0x0400453C RID: 17724
		private const float TILE_ATLAS_HEIGHT = 2048f;

		// Token: 0x0400453D RID: 17725
		private const int chunkEdgeSize = 16;

		// Token: 0x0400453E RID: 17726
		protected Dictionary<KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>, BlockTileRenderer.RenderInfo> renderInfo = new Dictionary<KeyValuePair<BuildingDef, BlockTileRenderer.RenderInfoLayer>, BlockTileRenderer.RenderInfo>();

		// Token: 0x0400453F RID: 17727
		private int selectedCell = -1;

		// Token: 0x04004540 RID: 17728
		private int highlightCell = -1;

		// Token: 0x04004541 RID: 17729
		private int invalidPlaceCell = -1;

		// Token: 0x02001AFD RID: 6909
		public enum RenderInfoLayer
		{
			// Token: 0x0400793D RID: 31037
			Built,
			// Token: 0x0400793E RID: 31038
			UnderConstruction,
			// Token: 0x0400793F RID: 31039
			Replacement
		}

		// Token: 0x02001AFE RID: 6910
		[Flags]
		public enum Bits
		{
			// Token: 0x04007941 RID: 31041
			UpLeft = 128,
			// Token: 0x04007942 RID: 31042
			Up = 64,
			// Token: 0x04007943 RID: 31043
			UpRight = 32,
			// Token: 0x04007944 RID: 31044
			Left = 16,
			// Token: 0x04007945 RID: 31045
			Right = 8,
			// Token: 0x04007946 RID: 31046
			DownLeft = 4,
			// Token: 0x04007947 RID: 31047
			Down = 2,
			// Token: 0x04007948 RID: 31048
			DownRight = 1
		}

		// Token: 0x02001AFF RID: 6911
		protected class RenderInfo
		{
			// Token: 0x06009488 RID: 38024 RVA: 0x0031C030 File Offset: 0x0031A230
			public RenderInfo(BlockTileRenderer renderer, int queryLayer, int renderLayer, BuildingDef def, SimHashes element)
			{
				this.queryLayer = queryLayer;
				this.renderLayer = renderLayer;
				this.rootPosition = new Vector3(0f, 0f, Grid.GetLayerZ(def.SceneLayer));
				this.element = element;
				this.material = new Material(def.BlockTileMaterial);
				if (def.BlockTileIsTransparent)
				{
					this.material.renderQueue = RenderQueues.Liquid;
					this.decorZOffset = Grid.GetLayerZ(Grid.SceneLayer.TileFront) - Grid.GetLayerZ(Grid.SceneLayer.Liquid) - 1f;
				}
				else if (def.SceneLayer == Grid.SceneLayer.TileMain)
				{
					this.material.renderQueue = RenderQueues.BlockTiles;
				}
				this.material.DisableKeyword("ENABLE_SHINE");
				if (element != SimHashes.Void)
				{
					this.material.SetTexture("_MainTex", def.BlockTileAtlas.texture);
					this.material.name = def.BlockTileAtlas.name + "Mat";
					if (def.BlockTileShineAtlas != null)
					{
						this.material.SetTexture("_SpecularTex", def.BlockTileShineAtlas.texture);
						this.material.EnableKeyword("ENABLE_SHINE");
					}
				}
				else
				{
					this.material.SetTexture("_MainTex", def.BlockTilePlaceAtlas.texture);
					this.material.name = def.BlockTilePlaceAtlas.name + "Mat";
				}
				int num = Grid.WidthInCells / 16 + 1;
				int num2 = Grid.HeightInCells / 16 + 1;
				this.meshChunks = new Mesh[num, num2];
				this.dirtyChunks = new bool[num, num2];
				for (int i = 0; i < num2; i++)
				{
					for (int j = 0; j < num; j++)
					{
						this.dirtyChunks[j, i] = true;
					}
				}
				BlockTileDecorInfo blockTileDecorInfo = ((element == SimHashes.Void) ? def.DecorPlaceBlockTileInfo : def.DecorBlockTileInfo);
				if (blockTileDecorInfo)
				{
					this.decorRenderInfo = new BlockTileRenderer.DecorRenderInfo(num, num2, queryLayer, def, blockTileDecorInfo);
				}
				int num3 = def.BlockTileAtlas.items[0].name.Length - 4 - 8;
				int num4 = num3 - 1 - 8;
				this.atlasInfo = new BlockTileRenderer.RenderInfo.AtlasInfo[def.BlockTileAtlas.items.Length];
				for (int k = 0; k < this.atlasInfo.Length; k++)
				{
					TextureAtlas.Item item = def.BlockTileAtlas.items[k];
					string text = item.name.Substring(num4, 8);
					string text2 = item.name.Substring(num3, 8);
					int num5 = Convert.ToInt32(text, 2);
					int num6 = Convert.ToInt32(text2, 2);
					this.atlasInfo[k].requiredConnections = (BlockTileRenderer.Bits)num5;
					this.atlasInfo[k].forbiddenConnections = (BlockTileRenderer.Bits)num6;
					this.atlasInfo[k].uvBox = item.uvBox;
					this.atlasInfo[k].name = item.name;
				}
				this.trimUVSize = new Vector2(0.03125f, 0.03125f);
			}

			// Token: 0x06009489 RID: 38025 RVA: 0x0031C36C File Offset: 0x0031A56C
			public void FreeResources()
			{
				UnityEngine.Object.DestroyImmediate(this.material);
				this.material = null;
				this.atlasInfo = null;
				for (int i = 0; i < this.meshChunks.GetLength(0); i++)
				{
					for (int j = 0; j < this.meshChunks.GetLength(1); j++)
					{
						if (this.meshChunks[i, j] != null)
						{
							UnityEngine.Object.DestroyImmediate(this.meshChunks[i, j]);
							this.meshChunks[i, j] = null;
						}
					}
				}
				this.meshChunks = null;
				this.decorRenderInfo = null;
				this.occupiedCells.Clear();
			}

			// Token: 0x0600948A RID: 38026 RVA: 0x0031C410 File Offset: 0x0031A610
			public void AddCell(int cell)
			{
				int num = 0;
				this.occupiedCells.TryGetValue(cell, out num);
				this.occupiedCells[cell] = num + 1;
				this.MarkDirty(cell);
			}

			// Token: 0x0600948B RID: 38027 RVA: 0x0031C444 File Offset: 0x0031A644
			public void RemoveCell(int cell)
			{
				int num = 0;
				this.occupiedCells.TryGetValue(cell, out num);
				if (num > 1)
				{
					this.occupiedCells[cell] = num - 1;
				}
				else
				{
					this.occupiedCells.Remove(cell);
				}
				this.MarkDirty(cell);
			}

			// Token: 0x0600948C RID: 38028 RVA: 0x0031C48C File Offset: 0x0031A68C
			public void MarkDirty(int cell)
			{
				Vector2I chunkIdx = BlockTileRenderer.GetChunkIdx(cell);
				this.dirtyChunks[chunkIdx.x, chunkIdx.y] = true;
			}

			// Token: 0x0600948D RID: 38029 RVA: 0x0031C4B8 File Offset: 0x0031A6B8
			public void MarkDirtyIfOccupied(int cell)
			{
				if (this.occupiedCells.ContainsKey(cell))
				{
					this.MarkDirty(cell);
				}
			}

			// Token: 0x0600948E RID: 38030 RVA: 0x0031C4D0 File Offset: 0x0031A6D0
			public void Render(int x, int y)
			{
				if (this.meshChunks[x, y] != null)
				{
					Graphics.DrawMesh(this.meshChunks[x, y], this.rootPosition, Quaternion.identity, this.material, this.renderLayer);
				}
				if (this.decorRenderInfo != null)
				{
					this.decorRenderInfo.Render(x, y, this.rootPosition - new Vector3(0f, 0f, 0.5f), this.renderLayer);
				}
			}

			// Token: 0x0600948F RID: 38031 RVA: 0x0031C558 File Offset: 0x0031A758
			public void Rebuild(BlockTileRenderer renderer, int chunk_x, int chunk_y, List<Vector3> vertices, List<Vector2> uvs, List<int> indices, List<Color> colours)
			{
				if (!this.dirtyChunks[chunk_x, chunk_y] && !renderer.ForceRebuild)
				{
					return;
				}
				this.dirtyChunks[chunk_x, chunk_y] = false;
				vertices.Clear();
				uvs.Clear();
				indices.Clear();
				colours.Clear();
				for (int i = chunk_y * 16; i < chunk_y * 16 + 16; i++)
				{
					for (int j = chunk_x * 16; j < chunk_x * 16 + 16; j++)
					{
						int num = i * Grid.WidthInCells + j;
						if (this.occupiedCells.ContainsKey(num))
						{
							BlockTileRenderer.Bits connectionBits = renderer.GetConnectionBits(j, i, this.queryLayer);
							for (int k = 0; k < this.atlasInfo.Length; k++)
							{
								bool flag = (this.atlasInfo[k].requiredConnections & connectionBits) == this.atlasInfo[k].requiredConnections;
								bool flag2 = (this.atlasInfo[k].forbiddenConnections & connectionBits) > (BlockTileRenderer.Bits)0;
								if (flag && !flag2)
								{
									Color cellColour = renderer.GetCellColour(num, this.element);
									this.AddVertexInfo(this.atlasInfo[k], this.trimUVSize, j, i, connectionBits, cellColour, vertices, uvs, indices, colours);
									break;
								}
							}
						}
					}
				}
				Mesh mesh = this.meshChunks[chunk_x, chunk_y];
				if (vertices.Count > 0)
				{
					if (mesh == null)
					{
						mesh = new Mesh();
						mesh.name = "BlockTile";
						this.meshChunks[chunk_x, chunk_y] = mesh;
					}
					mesh.Clear();
					mesh.SetVertices(vertices);
					mesh.SetUVs(0, uvs);
					mesh.SetColors(colours);
					mesh.SetTriangles(indices, 0);
				}
				else if (mesh != null)
				{
					this.meshChunks[chunk_x, chunk_y] = null;
				}
				if (this.decorRenderInfo != null)
				{
					this.decorRenderInfo.Rebuild(renderer, this.occupiedCells, chunk_x, chunk_y, this.decorZOffset, 16, vertices, uvs, colours, indices, this.element);
				}
			}

			// Token: 0x06009490 RID: 38032 RVA: 0x0031C75C File Offset: 0x0031A95C
			private void AddVertexInfo(BlockTileRenderer.RenderInfo.AtlasInfo atlas_info, Vector2 uv_trim_size, int x, int y, BlockTileRenderer.Bits connection_bits, Color color, List<Vector3> vertices, List<Vector2> uvs, List<int> indices, List<Color> colours)
			{
				Vector2 vector = new Vector2((float)x, (float)y);
				Vector2 vector2 = vector + new Vector2(1f, 1f);
				Vector2 vector3 = new Vector2(atlas_info.uvBox.x, atlas_info.uvBox.w);
				Vector2 vector4 = new Vector2(atlas_info.uvBox.z, atlas_info.uvBox.y);
				if ((connection_bits & BlockTileRenderer.Bits.Left) == (BlockTileRenderer.Bits)0)
				{
					vector.x -= 0.25f;
				}
				else
				{
					vector3.x += uv_trim_size.x;
				}
				if ((connection_bits & BlockTileRenderer.Bits.Right) == (BlockTileRenderer.Bits)0)
				{
					vector2.x += 0.25f;
				}
				else
				{
					vector4.x -= uv_trim_size.x;
				}
				if ((connection_bits & BlockTileRenderer.Bits.Up) == (BlockTileRenderer.Bits)0)
				{
					vector2.y += 0.25f;
				}
				else
				{
					vector4.y -= uv_trim_size.y;
				}
				if ((connection_bits & BlockTileRenderer.Bits.Down) == (BlockTileRenderer.Bits)0)
				{
					vector.y -= 0.25f;
				}
				else
				{
					vector3.y += uv_trim_size.y;
				}
				int count = vertices.Count;
				vertices.Add(vector);
				vertices.Add(new Vector2(vector2.x, vector.y));
				vertices.Add(vector2);
				vertices.Add(new Vector2(vector.x, vector2.y));
				uvs.Add(vector3);
				uvs.Add(new Vector2(vector4.x, vector3.y));
				uvs.Add(vector4);
				uvs.Add(new Vector2(vector3.x, vector4.y));
				indices.Add(count);
				indices.Add(count + 1);
				indices.Add(count + 2);
				indices.Add(count);
				indices.Add(count + 2);
				indices.Add(count + 3);
				colours.Add(color);
				colours.Add(color);
				colours.Add(color);
				colours.Add(color);
			}

			// Token: 0x04007949 RID: 31049
			private BlockTileRenderer.RenderInfo.AtlasInfo[] atlasInfo;

			// Token: 0x0400794A RID: 31050
			private bool[,] dirtyChunks;

			// Token: 0x0400794B RID: 31051
			private int queryLayer;

			// Token: 0x0400794C RID: 31052
			private Material material;

			// Token: 0x0400794D RID: 31053
			private int renderLayer;

			// Token: 0x0400794E RID: 31054
			private Mesh[,] meshChunks;

			// Token: 0x0400794F RID: 31055
			private BlockTileRenderer.DecorRenderInfo decorRenderInfo;

			// Token: 0x04007950 RID: 31056
			private Vector2 trimUVSize;

			// Token: 0x04007951 RID: 31057
			private Vector3 rootPosition;

			// Token: 0x04007952 RID: 31058
			private Dictionary<int, int> occupiedCells = new Dictionary<int, int>();

			// Token: 0x04007953 RID: 31059
			private SimHashes element;

			// Token: 0x04007954 RID: 31060
			private float decorZOffset = -1f;

			// Token: 0x04007955 RID: 31061
			private const float scale = 0.5f;

			// Token: 0x04007956 RID: 31062
			private const float core_size = 256f;

			// Token: 0x04007957 RID: 31063
			private const float trim_size = 64f;

			// Token: 0x04007958 RID: 31064
			private const float cell_size = 1f;

			// Token: 0x04007959 RID: 31065
			private const float world_trim_size = 0.25f;

			// Token: 0x0200210A RID: 8458
			private struct AtlasInfo
			{
				// Token: 0x040092EC RID: 37612
				public BlockTileRenderer.Bits requiredConnections;

				// Token: 0x040092ED RID: 37613
				public BlockTileRenderer.Bits forbiddenConnections;

				// Token: 0x040092EE RID: 37614
				public Vector4 uvBox;

				// Token: 0x040092EF RID: 37615
				public string name;
			}
		}

		// Token: 0x02001B00 RID: 6912
		private class DecorRenderInfo
		{
			// Token: 0x06009491 RID: 38033 RVA: 0x0031C96C File Offset: 0x0031AB6C
			public DecorRenderInfo(int num_x_chunks, int num_y_chunks, int query_layer, BuildingDef def, BlockTileDecorInfo decorInfo)
			{
				this.decorInfo = decorInfo;
				this.queryLayer = query_layer;
				this.material = new Material(def.BlockTileMaterial);
				if (def.BlockTileIsTransparent)
				{
					this.material.renderQueue = RenderQueues.Liquid;
				}
				else if (def.SceneLayer == Grid.SceneLayer.TileMain)
				{
					this.material.renderQueue = RenderQueues.BlockTiles;
				}
				this.material.SetTexture("_MainTex", decorInfo.atlas.texture);
				if (decorInfo.atlasSpec != null)
				{
					this.material.SetTexture("_SpecularTex", decorInfo.atlasSpec.texture);
					this.material.EnableKeyword("ENABLE_SHINE");
				}
				else
				{
					this.material.DisableKeyword("ENABLE_SHINE");
				}
				this.meshChunks = new Mesh[num_x_chunks, num_y_chunks];
			}

			// Token: 0x06009492 RID: 38034 RVA: 0x0031CA58 File Offset: 0x0031AC58
			public void FreeResources()
			{
				this.decorInfo = null;
				UnityEngine.Object.DestroyImmediate(this.material);
				this.material = null;
				for (int i = 0; i < this.meshChunks.GetLength(0); i++)
				{
					for (int j = 0; j < this.meshChunks.GetLength(1); j++)
					{
						if (this.meshChunks[i, j] != null)
						{
							UnityEngine.Object.DestroyImmediate(this.meshChunks[i, j]);
							this.meshChunks[i, j] = null;
						}
					}
				}
				this.meshChunks = null;
				this.triangles.Clear();
			}

			// Token: 0x06009493 RID: 38035 RVA: 0x0031CAF3 File Offset: 0x0031ACF3
			public void Render(int x, int y, Vector3 position, int renderLayer)
			{
				if (this.meshChunks[x, y] != null)
				{
					Graphics.DrawMesh(this.meshChunks[x, y], position, Quaternion.identity, this.material, renderLayer);
				}
			}

			// Token: 0x06009494 RID: 38036 RVA: 0x0031CB2C File Offset: 0x0031AD2C
			public void Rebuild(BlockTileRenderer renderer, Dictionary<int, int> occupiedCells, int chunk_x, int chunk_y, float z_offset, int chunkEdgeSize, List<Vector3> vertices, List<Vector2> uvs, List<Color> colours, List<int> indices, SimHashes element)
			{
				vertices.Clear();
				uvs.Clear();
				this.triangles.Clear();
				colours.Clear();
				indices.Clear();
				for (int i = chunk_y * chunkEdgeSize; i < chunk_y * chunkEdgeSize + chunkEdgeSize; i++)
				{
					for (int j = chunk_x * chunkEdgeSize; j < chunk_x * chunkEdgeSize + chunkEdgeSize; j++)
					{
						int num = i * Grid.WidthInCells + j;
						if (occupiedCells.ContainsKey(num))
						{
							Color cellColour = renderer.GetCellColour(num, element);
							BlockTileRenderer.Bits decorConnectionBits = renderer.GetDecorConnectionBits(j, i, this.queryLayer);
							this.AddDecor(j, i, z_offset, decorConnectionBits, cellColour, vertices, uvs, this.triangles, colours);
						}
					}
				}
				if (vertices.Count > 0)
				{
					Mesh mesh = this.meshChunks[chunk_x, chunk_y];
					if (mesh == null)
					{
						mesh = new Mesh();
						mesh.name = "DecorRender";
						this.meshChunks[chunk_x, chunk_y] = mesh;
					}
					this.triangles.Sort((BlockTileRenderer.DecorRenderInfo.TriangleInfo a, BlockTileRenderer.DecorRenderInfo.TriangleInfo b) => a.sortOrder.CompareTo(b.sortOrder));
					for (int k = 0; k < this.triangles.Count; k++)
					{
						indices.Add(this.triangles[k].i0);
						indices.Add(this.triangles[k].i1);
						indices.Add(this.triangles[k].i2);
					}
					mesh.Clear();
					mesh.SetVertices(vertices);
					mesh.SetUVs(0, uvs);
					mesh.SetColors(colours);
					mesh.SetTriangles(indices, 0);
					return;
				}
				this.meshChunks[chunk_x, chunk_y] = null;
			}

			// Token: 0x06009495 RID: 38037 RVA: 0x0031CCE8 File Offset: 0x0031AEE8
			private void AddDecor(int x, int y, float z_offset, BlockTileRenderer.Bits connection_bits, Color colour, List<Vector3> vertices, List<Vector2> uvs, List<BlockTileRenderer.DecorRenderInfo.TriangleInfo> triangles, List<Color> colours)
			{
				for (int i = 0; i < this.decorInfo.decor.Length; i++)
				{
					BlockTileDecorInfo.Decor decor = this.decorInfo.decor[i];
					if (decor.variants != null && decor.variants.Length != 0)
					{
						bool flag = (connection_bits & decor.requiredConnections) == decor.requiredConnections;
						bool flag2 = (connection_bits & decor.forbiddenConnections) > (BlockTileRenderer.Bits)0;
						if (flag && !flag2)
						{
							float num = PerlinSimplexNoise.noise((float)(i + x + connection_bits) * BlockTileRenderer.DecorRenderInfo.simplex_scale.x, (float)(i + y + connection_bits) * BlockTileRenderer.DecorRenderInfo.simplex_scale.y);
							if (num >= decor.probabilityCutoff)
							{
								int num2 = (int)((float)(decor.variants.Length - 1) * num);
								int count = vertices.Count;
								Vector3 vector = new Vector3((float)x, (float)y, z_offset) + decor.variants[num2].offset;
								foreach (Vector3 vector2 in decor.variants[num2].atlasItem.vertices)
								{
									vertices.Add(vector2 + vector);
									colours.Add(colour);
								}
								uvs.AddRange(decor.variants[num2].atlasItem.uvs);
								int[] indices = decor.variants[num2].atlasItem.indices;
								for (int k = 0; k < indices.Length; k += 3)
								{
									triangles.Add(new BlockTileRenderer.DecorRenderInfo.TriangleInfo
									{
										sortOrder = decor.sortOrder,
										i0 = indices[k] + count,
										i1 = indices[k + 1] + count,
										i2 = indices[k + 2] + count
									});
								}
							}
						}
					}
				}
			}

			// Token: 0x0400795A RID: 31066
			private int queryLayer;

			// Token: 0x0400795B RID: 31067
			private BlockTileDecorInfo decorInfo;

			// Token: 0x0400795C RID: 31068
			private Mesh[,] meshChunks;

			// Token: 0x0400795D RID: 31069
			private Material material;

			// Token: 0x0400795E RID: 31070
			private List<BlockTileRenderer.DecorRenderInfo.TriangleInfo> triangles = new List<BlockTileRenderer.DecorRenderInfo.TriangleInfo>();

			// Token: 0x0400795F RID: 31071
			private static Vector2 simplex_scale = new Vector2(92.41f, 87.16f);

			// Token: 0x0200210B RID: 8459
			private struct TriangleInfo
			{
				// Token: 0x040092F0 RID: 37616
				public int sortOrder;

				// Token: 0x040092F1 RID: 37617
				public int i0;

				// Token: 0x040092F2 RID: 37618
				public int i1;

				// Token: 0x040092F3 RID: 37619
				public int i2;
			}
		}
	}
}
