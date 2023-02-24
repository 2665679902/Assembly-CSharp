﻿using System;
using UnityEngine;

namespace Rendering.World
{
	// Token: 0x02000C52 RID: 3154
	public class LiquidTileOverlayRenderer : TileRenderer
	{
		// Token: 0x0600643F RID: 25663 RVA: 0x00258EE6 File Offset: 0x002570E6
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			ShaderReloader.Register(new System.Action(this.OnShadersReloaded));
		}

		// Token: 0x06006440 RID: 25664 RVA: 0x00258F00 File Offset: 0x00257100
		protected override Mask[] GetMasks()
		{
			return new Mask[]
			{
				new Mask(this.Atlas, 0, false, false, false, false),
				new Mask(this.Atlas, 0, false, true, false, false),
				new Mask(this.Atlas, 1, false, false, false, false)
			};
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x00258F58 File Offset: 0x00257158
		public void OnShadersReloaded()
		{
			foreach (Element element in ElementLoader.elements)
			{
				if (element.IsLiquid && element.substance != null && element.substance.material != null)
				{
					Material material = new Material(element.substance.material);
					this.InitAlphaMaterial(material, element);
					int idx = element.substance.idx;
					for (int i = 0; i < this.Masks.Length; i++)
					{
						int num = idx * this.Masks.Length + i;
						element.substance.RefreshPropertyBlock();
						this.Brushes[num].SetMaterial(material, element.substance.propertyBlock);
					}
				}
			}
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x00259044 File Offset: 0x00257244
		public override void LoadBrushes()
		{
			this.Brushes = new Brush[ElementLoader.elements.Count * this.Masks.Length];
			foreach (Element element in ElementLoader.elements)
			{
				if (element.IsLiquid && element.substance != null && element.substance.material != null)
				{
					Material material = new Material(element.substance.material);
					this.InitAlphaMaterial(material, element);
					int idx = element.substance.idx;
					for (int i = 0; i < this.Masks.Length; i++)
					{
						int num = idx * this.Masks.Length + i;
						element.substance.RefreshPropertyBlock();
						this.Brushes[num] = new Brush(num, element.id.ToString(), material, this.Masks[i], this.ActiveBrushes, this.DirtyBrushes, this.TileGridWidth, element.substance.propertyBlock);
					}
				}
			}
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x00259184 File Offset: 0x00257384
		private void InitAlphaMaterial(Material alpha_material, Element element)
		{
			alpha_material.name = element.name;
			alpha_material.renderQueue = RenderQueues.BlockTiles + element.substance.idx;
			alpha_material.EnableKeyword("ALPHA");
			alpha_material.DisableKeyword("OPAQUE");
			alpha_material.SetTexture("_AlphaTestMap", this.Atlas.texture);
			alpha_material.SetInt("_SrcAlpha", 5);
			alpha_material.SetInt("_DstAlpha", 10);
			alpha_material.SetInt("_ZWrite", 0);
			alpha_material.SetColor("_Colour", element.substance.colour);
		}

		// Token: 0x06006444 RID: 25668 RVA: 0x00259220 File Offset: 0x00257420
		private bool RenderLiquid(int cell, int cell_above)
		{
			bool flag = false;
			if (Grid.Element[cell].IsSolid)
			{
				Element element = Grid.Element[cell_above];
				if (element.IsLiquid && element.substance.material != null)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x00259264 File Offset: 0x00257464
		private void SetBrushIdx(int i, ref Tile tile, int substance_idx, LiquidTileOverlayRenderer.LiquidConnections connections, Brush[] brush_array, int[] brush_grid)
		{
			if (connections == LiquidTileOverlayRenderer.LiquidConnections.Empty)
			{
				brush_grid[tile.Idx * 4 + i] = -1;
				return;
			}
			Brush brush = brush_array[substance_idx * tile.MaskCount + connections - LiquidTileOverlayRenderer.LiquidConnections.Left];
			brush.Add(tile.Idx);
			brush_grid[tile.Idx * 4 + i] = brush.Id;
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x002592BC File Offset: 0x002574BC
		public override void MarkDirty(ref Tile tile, Brush[] brush_array, int[] brush_grid)
		{
			if (!this.RenderLiquid(tile.TileCells.Cell0, tile.TileCells.Cell2))
			{
				if (this.RenderLiquid(tile.TileCells.Cell1, tile.TileCells.Cell3))
				{
					this.SetBrushIdx(1, ref tile, Grid.Element[tile.TileCells.Cell3].substance.idx, LiquidTileOverlayRenderer.LiquidConnections.Right, brush_array, brush_grid);
				}
				return;
			}
			if (this.RenderLiquid(tile.TileCells.Cell1, tile.TileCells.Cell3))
			{
				this.SetBrushIdx(0, ref tile, Grid.Element[tile.TileCells.Cell2].substance.idx, LiquidTileOverlayRenderer.LiquidConnections.Both, brush_array, brush_grid);
				return;
			}
			this.SetBrushIdx(0, ref tile, Grid.Element[tile.TileCells.Cell2].substance.idx, LiquidTileOverlayRenderer.LiquidConnections.Left, brush_array, brush_grid);
		}

		// Token: 0x02001B01 RID: 6913
		private enum LiquidConnections
		{
			// Token: 0x04007961 RID: 31073
			Left = 1,
			// Token: 0x04007962 RID: 31074
			Right,
			// Token: 0x04007963 RID: 31075
			Both,
			// Token: 0x04007964 RID: 31076
			Empty = 128
		}
	}
}
