using System;
using UnityEngine;

// Token: 0x0200048D RID: 1165
public class KBoxCollider2D : KCollider2D
{
	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0008B436 File Offset: 0x00089636
	// (set) Token: 0x06001A0B RID: 6667 RVA: 0x0008B43E File Offset: 0x0008963E
	public Vector2 size
	{
		get
		{
			return this._size;
		}
		set
		{
			this._size = value;
			base.MarkDirty(false);
		}
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x0008B450 File Offset: 0x00089650
	public override Extents GetExtents()
	{
		Vector3 vector = base.transform.GetPosition() + new Vector3(base.offset.x, base.offset.y, 0f);
		Vector2 vector2 = this.size * 0.9999f;
		Vector2 vector3 = new Vector2(vector.x - vector2.x * 0.5f, vector.y - vector2.y * 0.5f);
		Vector2 vector4 = new Vector2(vector.x + vector2.x * 0.5f, vector.y + vector2.y * 0.5f);
		Vector2I vector2I = new Vector2I((int)vector3.x, (int)vector3.y);
		Vector2I vector2I2 = new Vector2I((int)vector4.x, (int)vector4.y);
		int num = vector2I2.x - vector2I.x + 1;
		int num2 = vector2I2.y - vector2I.y + 1;
		return new Extents(vector2I.x, vector2I.y, num, num2);
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x0008B55C File Offset: 0x0008975C
	public override bool Intersects(Vector2 intersect_pos)
	{
		Vector3 vector = base.transform.GetPosition() + new Vector3(base.offset.x, base.offset.y, 0f);
		Vector2 vector2 = new Vector2(vector.x - this.size.x * 0.5f, vector.y - this.size.y * 0.5f);
		Vector2 vector3 = new Vector2(vector.x + this.size.x * 0.5f, vector.y + this.size.y * 0.5f);
		return intersect_pos.x >= vector2.x && intersect_pos.x <= vector3.x && intersect_pos.y >= vector2.y && intersect_pos.y <= vector3.y;
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x06001A0E RID: 6670 RVA: 0x0008B648 File Offset: 0x00089848
	public override Bounds bounds
	{
		get
		{
			return new Bounds(base.transform.GetPosition() + new Vector3(base.offset.x, base.offset.y, 0f), new Vector3(this._size.x, this._size.y, 0f));
		}
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x0008B6AC File Offset: 0x000898AC
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(this.bounds.center, new Vector3(this._size.x, this._size.y, 0f));
	}

	// Token: 0x04000E92 RID: 3730
	[SerializeField]
	private Vector2 _size;
}
