using System;
using UnityEngine;

// Token: 0x02000770 RID: 1904
public struct FallerComponent
{
	// Token: 0x0600342B RID: 13355 RVA: 0x001188A8 File Offset: 0x00116AA8
	public FallerComponent(Transform transform, Vector2 initial_velocity)
	{
		this.transform = transform;
		this.transformInstanceId = transform.GetInstanceID();
		this.isFalling = false;
		this.initialVelocity = initial_velocity;
		this.partitionerEntry = default(HandleVector<int>.Handle);
		this.solidChangedCB = null;
		this.cellChangedCB = null;
		KCircleCollider2D component = transform.GetComponent<KCircleCollider2D>();
		if (component != null)
		{
			this.offset = component.radius;
			return;
		}
		KCollider2D component2 = transform.GetComponent<KCollider2D>();
		if (component2 != null)
		{
			this.offset = transform.GetPosition().y - component2.bounds.min.y;
			return;
		}
		this.offset = 0f;
	}

	// Token: 0x04002041 RID: 8257
	public Transform transform;

	// Token: 0x04002042 RID: 8258
	public int transformInstanceId;

	// Token: 0x04002043 RID: 8259
	public bool isFalling;

	// Token: 0x04002044 RID: 8260
	public float offset;

	// Token: 0x04002045 RID: 8261
	public Vector2 initialVelocity;

	// Token: 0x04002046 RID: 8262
	public HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04002047 RID: 8263
	public Action<object> solidChangedCB;

	// Token: 0x04002048 RID: 8264
	public System.Action cellChangedCB;
}
