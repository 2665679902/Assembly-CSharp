using System;
using UnityEngine;

namespace Rendering.World
{
	// Token: 0x02000C4E RID: 3150
	public struct Mask
	{
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06006425 RID: 25637 RVA: 0x00258647 File Offset: 0x00256847
		// (set) Token: 0x06006426 RID: 25638 RVA: 0x0025864F File Offset: 0x0025684F
		public Vector2 UV0 { readonly get; private set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06006427 RID: 25639 RVA: 0x00258658 File Offset: 0x00256858
		// (set) Token: 0x06006428 RID: 25640 RVA: 0x00258660 File Offset: 0x00256860
		public Vector2 UV1 { readonly get; private set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06006429 RID: 25641 RVA: 0x00258669 File Offset: 0x00256869
		// (set) Token: 0x0600642A RID: 25642 RVA: 0x00258671 File Offset: 0x00256871
		public Vector2 UV2 { readonly get; private set; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600642B RID: 25643 RVA: 0x0025867A File Offset: 0x0025687A
		// (set) Token: 0x0600642C RID: 25644 RVA: 0x00258682 File Offset: 0x00256882
		public Vector2 UV3 { readonly get; private set; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600642D RID: 25645 RVA: 0x0025868B File Offset: 0x0025688B
		// (set) Token: 0x0600642E RID: 25646 RVA: 0x00258693 File Offset: 0x00256893
		public bool IsOpaque { readonly get; private set; }

		// Token: 0x0600642F RID: 25647 RVA: 0x0025869C File Offset: 0x0025689C
		public Mask(TextureAtlas atlas, int texture_idx, bool transpose, bool flip_x, bool flip_y, bool is_opaque)
		{
			this = default(Mask);
			this.atlas = atlas;
			this.texture_idx = texture_idx;
			this.transpose = transpose;
			this.flip_x = flip_x;
			this.flip_y = flip_y;
			this.atlas_offset = 0;
			this.IsOpaque = is_opaque;
			this.Refresh();
		}

		// Token: 0x06006430 RID: 25648 RVA: 0x002586EA File Offset: 0x002568EA
		public void SetOffset(int offset)
		{
			this.atlas_offset = offset;
			this.Refresh();
		}

		// Token: 0x06006431 RID: 25649 RVA: 0x002586FC File Offset: 0x002568FC
		public void Refresh()
		{
			int num = this.atlas_offset * 4 + this.atlas_offset;
			if (num + this.texture_idx >= this.atlas.items.Length)
			{
				num = 0;
			}
			Vector4 uvBox = this.atlas.items[num + this.texture_idx].uvBox;
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			Vector2 zero3 = Vector2.zero;
			Vector2 zero4 = Vector2.zero;
			if (this.transpose)
			{
				float num2 = uvBox.x;
				float num3 = uvBox.z;
				if (this.flip_x)
				{
					num2 = uvBox.z;
					num3 = uvBox.x;
				}
				zero.x = num2;
				zero2.x = num2;
				zero3.x = num3;
				zero4.x = num3;
				float num4 = uvBox.y;
				float num5 = uvBox.w;
				if (this.flip_y)
				{
					num4 = uvBox.w;
					num5 = uvBox.y;
				}
				zero.y = num4;
				zero2.y = num5;
				zero3.y = num4;
				zero4.y = num5;
			}
			else
			{
				float num6 = uvBox.x;
				float num7 = uvBox.z;
				if (this.flip_x)
				{
					num6 = uvBox.z;
					num7 = uvBox.x;
				}
				zero.x = num6;
				zero2.x = num7;
				zero3.x = num6;
				zero4.x = num7;
				float num8 = uvBox.y;
				float num9 = uvBox.w;
				if (this.flip_y)
				{
					num8 = uvBox.w;
					num9 = uvBox.y;
				}
				zero.y = num9;
				zero2.y = num9;
				zero3.y = num8;
				zero4.y = num8;
			}
			this.UV0 = zero;
			this.UV1 = zero2;
			this.UV2 = zero3;
			this.UV3 = zero4;
		}

		// Token: 0x04004572 RID: 17778
		private TextureAtlas atlas;

		// Token: 0x04004573 RID: 17779
		private int texture_idx;

		// Token: 0x04004574 RID: 17780
		private bool transpose;

		// Token: 0x04004575 RID: 17781
		private bool flip_x;

		// Token: 0x04004576 RID: 17782
		private bool flip_y;

		// Token: 0x04004577 RID: 17783
		private int atlas_offset;

		// Token: 0x04004578 RID: 17784
		private const int TILES_PER_SET = 4;
	}
}
