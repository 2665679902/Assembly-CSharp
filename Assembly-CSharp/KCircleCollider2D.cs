using System;
using UnityEngine;

// Token: 0x0200048E RID: 1166
public class KCircleCollider2D : KCollider2D
{
	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0008B6FE File Offset: 0x000898FE
	// (set) Token: 0x06001A12 RID: 6674 RVA: 0x0008B706 File Offset: 0x00089906
	public float radius
	{
		get
		{
			return this._radius;
		}
		set
		{
			this._radius = value;
			base.MarkDirty(false);
		}
	}

	// Token: 0x06001A13 RID: 6675 RVA: 0x0008B718 File Offset: 0x00089918
	public override Extents GetExtents()
	{
		Vector3 vector = base.transform.GetPosition() + new Vector3(base.offset.x, base.offset.y, 0f);
		Vector2 vector2 = new Vector2(vector.x - this.radius, vector.y - this.radius);
		Vector2 vector3 = new Vector2(vector.x + this.radius, vector.y + this.radius);
		int num = (int)vector3.x - (int)vector2.x + 1;
		int num2 = (int)vector3.y - (int)vector2.y + 1;
		return new Extents((int)(vector.x - this._radius), (int)(vector.y - this._radius), num, num2);
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0008B7DC File Offset: 0x000899DC
	public override Bounds bounds
	{
		get
		{
			return new Bounds(base.transform.GetPosition() + new Vector3(base.offset.x, base.offset.y, 0f), new Vector3(this._radius * 2f, this._radius * 2f, 0f));
		}
	}

	// Token: 0x06001A15 RID: 6677 RVA: 0x0008B840 File Offset: 0x00089A40
	public override bool Intersects(Vector2 pos)
	{
		Vector3 position = base.transform.GetPosition();
		Vector2 vector = new Vector2(position.x, position.y) + base.offset;
		return (pos - vector).sqrMagnitude <= this._radius * this._radius;
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x0008B898 File Offset: 0x00089A98
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(this.bounds.center, this.radius);
	}

	// Token: 0x04000E93 RID: 3731
	[SerializeField]
	private float _radius;
}
