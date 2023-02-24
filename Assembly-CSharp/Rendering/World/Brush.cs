using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering.World
{
	// Token: 0x02000C4A RID: 3146
	public class Brush
	{
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x0600640A RID: 25610 RVA: 0x00257DC8 File Offset: 0x00255FC8
		// (set) Token: 0x0600640B RID: 25611 RVA: 0x00257DD0 File Offset: 0x00255FD0
		public int Id { get; private set; }

		// Token: 0x0600640C RID: 25612 RVA: 0x00257DDC File Offset: 0x00255FDC
		public Brush(int id, string name, Material material, Mask mask, List<Brush> active_brushes, List<Brush> dirty_brushes, int width_in_tiles, MaterialPropertyBlock property_block)
		{
			this.Id = id;
			this.material = material;
			this.mask = mask;
			this.mesh = new DynamicMesh(name, new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, 0f)));
			this.activeBrushes = active_brushes;
			this.dirtyBrushes = dirty_brushes;
			this.layer = LayerMask.NameToLayer("World");
			this.widthInTiles = width_in_tiles;
			this.propertyBlock = property_block;
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x00257E6A File Offset: 0x0025606A
		public void Add(int tile_idx)
		{
			this.tiles.Add(tile_idx);
			if (!this.dirty)
			{
				this.dirtyBrushes.Add(this);
				this.dirty = true;
			}
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x00257E94 File Offset: 0x00256094
		public void Remove(int tile_idx)
		{
			this.tiles.Remove(tile_idx);
			if (!this.dirty)
			{
				this.dirtyBrushes.Add(this);
				this.dirty = true;
			}
		}

		// Token: 0x0600640F RID: 25615 RVA: 0x00257EBE File Offset: 0x002560BE
		public void SetMaskOffset(int offset)
		{
			this.mask.SetOffset(offset);
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x00257ECC File Offset: 0x002560CC
		public void Refresh()
		{
			bool flag = this.mesh.Meshes.Length != 0;
			int count = this.tiles.Count;
			int num = count * 4;
			int num2 = count * 6;
			this.mesh.Reserve(num, num2);
			if (this.mesh.SetTriangles)
			{
				int num3 = 0;
				for (int i = 0; i < count; i++)
				{
					this.mesh.AddTriangle(num3);
					this.mesh.AddTriangle(2 + num3);
					this.mesh.AddTriangle(1 + num3);
					this.mesh.AddTriangle(1 + num3);
					this.mesh.AddTriangle(2 + num3);
					this.mesh.AddTriangle(3 + num3);
					num3 += 4;
				}
			}
			foreach (int num4 in this.tiles)
			{
				float num5 = (float)(num4 % this.widthInTiles);
				float num6 = (float)(num4 / this.widthInTiles);
				float num7 = 0f;
				this.mesh.AddVertex(new Vector3(num5 - 0.5f, num6 - 0.5f, num7));
				this.mesh.AddVertex(new Vector3(num5 + 0.5f, num6 - 0.5f, num7));
				this.mesh.AddVertex(new Vector3(num5 - 0.5f, num6 + 0.5f, num7));
				this.mesh.AddVertex(new Vector3(num5 + 0.5f, num6 + 0.5f, num7));
			}
			if (this.mesh.SetUVs)
			{
				for (int j = 0; j < count; j++)
				{
					this.mesh.AddUV(this.mask.UV0);
					this.mesh.AddUV(this.mask.UV1);
					this.mesh.AddUV(this.mask.UV2);
					this.mesh.AddUV(this.mask.UV3);
				}
			}
			this.dirty = false;
			this.mesh.Commit();
			if (this.mesh.Meshes.Length != 0)
			{
				if (!flag)
				{
					this.activeBrushes.Add(this);
					return;
				}
			}
			else if (flag)
			{
				this.activeBrushes.Remove(this);
			}
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x00258128 File Offset: 0x00256328
		public void Render()
		{
			Vector3 vector = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.Ground));
			this.mesh.Render(vector, Quaternion.identity, this.material, this.layer, this.propertyBlock);
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x00258170 File Offset: 0x00256370
		public void SetMaterial(Material material, MaterialPropertyBlock property_block)
		{
			this.material = material;
			this.propertyBlock = property_block;
		}

		// Token: 0x04004543 RID: 17731
		private bool dirty;

		// Token: 0x04004544 RID: 17732
		private Material material;

		// Token: 0x04004545 RID: 17733
		private int layer;

		// Token: 0x04004546 RID: 17734
		private HashSet<int> tiles = new HashSet<int>();

		// Token: 0x04004547 RID: 17735
		private List<Brush> activeBrushes;

		// Token: 0x04004548 RID: 17736
		private List<Brush> dirtyBrushes;

		// Token: 0x04004549 RID: 17737
		private int widthInTiles;

		// Token: 0x0400454A RID: 17738
		private Mask mask;

		// Token: 0x0400454B RID: 17739
		private DynamicMesh mesh;

		// Token: 0x0400454C RID: 17740
		private MaterialPropertyBlock propertyBlock;
	}
}
