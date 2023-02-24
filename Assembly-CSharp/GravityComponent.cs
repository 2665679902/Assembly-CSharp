using System;
using UnityEngine;

// Token: 0x02000798 RID: 1944
public struct GravityComponent
{
	// Token: 0x0600368E RID: 13966 RVA: 0x00130748 File Offset: 0x0012E948
	public GravityComponent(Transform transform, System.Action on_landed, Vector2 initial_velocity, bool land_on_fake_floors, bool mayLeaveWorld)
	{
		this.transform = transform;
		this.elapsedTime = 0f;
		this.velocity = initial_velocity;
		this.onLanded = on_landed;
		this.landOnFakeFloors = land_on_fake_floors;
		this.mayLeaveWorld = mayLeaveWorld;
		KCollider2D component = transform.GetComponent<KCollider2D>();
		this.extents = GravityComponent.GetExtents(component);
		this.bottomYOffset = GravityComponent.GetGroundOffset(component);
	}

	// Token: 0x0600368F RID: 13967 RVA: 0x001307A4 File Offset: 0x0012E9A4
	public static float GetGroundOffset(KCollider2D collider)
	{
		if (collider != null)
		{
			return collider.bounds.extents.y - collider.offset.y;
		}
		return 0f;
	}

	// Token: 0x06003690 RID: 13968 RVA: 0x001307E0 File Offset: 0x0012E9E0
	public static Vector2 GetExtents(KCollider2D collider)
	{
		if (collider != null)
		{
			return collider.bounds.extents;
		}
		return Vector2.zero;
	}

	// Token: 0x06003691 RID: 13969 RVA: 0x0013080F File Offset: 0x0012EA0F
	public static Vector2 GetOffset(KCollider2D collider)
	{
		if (collider != null)
		{
			return collider.offset;
		}
		return Vector2.zero;
	}

	// Token: 0x0400245B RID: 9307
	public Transform transform;

	// Token: 0x0400245C RID: 9308
	public Vector2 velocity;

	// Token: 0x0400245D RID: 9309
	public float elapsedTime;

	// Token: 0x0400245E RID: 9310
	public System.Action onLanded;

	// Token: 0x0400245F RID: 9311
	public bool landOnFakeFloors;

	// Token: 0x04002460 RID: 9312
	public bool mayLeaveWorld;

	// Token: 0x04002461 RID: 9313
	public Vector2 extents;

	// Token: 0x04002462 RID: 9314
	public float bottomYOffset;
}
