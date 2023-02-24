using System;
using UnityEngine;

// Token: 0x02000449 RID: 1097
[AddComponentMenu("KMonoBehaviour/scripts/AnimEventHandler")]
public class AnimEventHandler : KMonoBehaviour
{
	// Token: 0x1400000A RID: 10
	// (add) Token: 0x060017A7 RID: 6055 RVA: 0x0007C11C File Offset: 0x0007A31C
	// (remove) Token: 0x060017A8 RID: 6056 RVA: 0x0007C154 File Offset: 0x0007A354
	private event AnimEventHandler.SetPos onWorkTargetSet;

	// Token: 0x060017A9 RID: 6057 RVA: 0x0007C189 File Offset: 0x0007A389
	public void SetDirty()
	{
		this.isDirty = 2;
	}

	// Token: 0x060017AA RID: 6058 RVA: 0x0007C194 File Offset: 0x0007A394
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (KBatchedAnimTracker kbatchedAnimTracker in base.GetComponentsInChildren<KBatchedAnimTracker>(true))
		{
			if (kbatchedAnimTracker.useTargetPoint)
			{
				this.onWorkTargetSet += kbatchedAnimTracker.SetTarget;
			}
		}
		this.baseOffset = this.animCollider.offset;
		this.instanceIndex = AnimEventHandler.InstanceSequence++;
		this.SetDirty();
	}

	// Token: 0x060017AB RID: 6059 RVA: 0x0007C205 File Offset: 0x0007A405
	protected override void OnForcedCleanUp()
	{
		this.navigator = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x060017AC RID: 6060 RVA: 0x0007C214 File Offset: 0x0007A414
	public HashedString GetContext()
	{
		return this.context;
	}

	// Token: 0x060017AD RID: 6061 RVA: 0x0007C21C File Offset: 0x0007A41C
	public void UpdateWorkTarget(Vector3 pos)
	{
		if (this.onWorkTargetSet != null)
		{
			this.onWorkTargetSet(pos);
		}
	}

	// Token: 0x060017AE RID: 6062 RVA: 0x0007C232 File Offset: 0x0007A432
	public void SetContext(HashedString context)
	{
		this.context = context;
	}

	// Token: 0x060017AF RID: 6063 RVA: 0x0007C23B File Offset: 0x0007A43B
	public void SetTargetPos(Vector3 target_pos)
	{
		this.targetPos = target_pos;
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x0007C244 File Offset: 0x0007A444
	public Vector3 GetTargetPos()
	{
		return this.targetPos;
	}

	// Token: 0x060017B1 RID: 6065 RVA: 0x0007C24C File Offset: 0x0007A44C
	public void ClearContext()
	{
		this.context = default(HashedString);
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x0007C25C File Offset: 0x0007A45C
	public void LateUpdate()
	{
		int num = Time.frameCount % 3;
		int num2 = this.instanceIndex % 3;
		if (num != num2 && this.isDirty <= 0)
		{
			return;
		}
		this.UpdateOffset();
	}

	// Token: 0x060017B3 RID: 6067 RVA: 0x0007C28C File Offset: 0x0007A48C
	public void UpdateOffset()
	{
		Vector3 pivotSymbolPosition = this.controller.GetPivotSymbolPosition();
		Vector3 vector = this.navigator.NavGrid.GetNavTypeData(this.navigator.CurrentNavType).animControllerOffset;
		this.animCollider.offset = new Vector2(this.baseOffset.x + pivotSymbolPosition.x - base.transform.GetPosition().x - vector.x, this.baseOffset.y + pivotSymbolPosition.y - base.transform.GetPosition().y + vector.y);
		this.isDirty = Mathf.Max(0, this.isDirty - 1);
	}

	// Token: 0x04000D1B RID: 3355
	private const int UPDATE_FRAME_RATE = 3;

	// Token: 0x04000D1C RID: 3356
	[MyCmpGet]
	private KBatchedAnimController controller;

	// Token: 0x04000D1D RID: 3357
	[MyCmpGet]
	private KBoxCollider2D animCollider;

	// Token: 0x04000D1E RID: 3358
	[MyCmpGet]
	private Navigator navigator;

	// Token: 0x04000D1F RID: 3359
	private Vector3 targetPos;

	// Token: 0x04000D21 RID: 3361
	public Vector2 baseOffset;

	// Token: 0x04000D22 RID: 3362
	public int isDirty;

	// Token: 0x04000D23 RID: 3363
	private HashedString context;

	// Token: 0x04000D24 RID: 3364
	private int instanceIndex;

	// Token: 0x04000D25 RID: 3365
	private static int InstanceSequence;

	// Token: 0x02001061 RID: 4193
	// (Invoke) Token: 0x060072CE RID: 29390
	private delegate void SetPos(Vector3 pos);
}
