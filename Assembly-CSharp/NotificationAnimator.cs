using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B1E RID: 2846
public class NotificationAnimator : MonoBehaviour
{
	// Token: 0x060057C2 RID: 22466 RVA: 0x001FD29A File Offset: 0x001FB49A
	public void Begin(bool startOffset = true)
	{
		this.Reset();
		this.animating = true;
		if (startOffset)
		{
			this.layoutElement.minWidth = 100f;
			return;
		}
		this.layoutElement.minWidth = 1f;
		this.speed = -10f;
	}

	// Token: 0x060057C3 RID: 22467 RVA: 0x001FD2D8 File Offset: 0x001FB4D8
	private void Reset()
	{
		this.bounceCount = 2;
		this.layoutElement = base.GetComponent<LayoutElement>();
		this.layoutElement.minWidth = 0f;
		this.speed = 1f;
	}

	// Token: 0x060057C4 RID: 22468 RVA: 0x001FD308 File Offset: 0x001FB508
	public void Stop()
	{
		this.Reset();
		this.animating = false;
	}

	// Token: 0x060057C5 RID: 22469 RVA: 0x001FD318 File Offset: 0x001FB518
	private void LateUpdate()
	{
		if (!this.animating)
		{
			return;
		}
		this.layoutElement.minWidth -= this.speed;
		this.speed += 0.5f;
		if (this.layoutElement.minWidth <= 0f)
		{
			if (this.bounceCount > 0)
			{
				this.bounceCount--;
				this.speed = -this.speed / Mathf.Pow(2f, (float)(2 - this.bounceCount));
				this.layoutElement.minWidth = -this.speed;
				return;
			}
			this.layoutElement.minWidth = 0f;
			this.Stop();
		}
	}

	// Token: 0x04003B66 RID: 15206
	private const float START_SPEED = 1f;

	// Token: 0x04003B67 RID: 15207
	private const float ACCELERATION = 0.5f;

	// Token: 0x04003B68 RID: 15208
	private const float BOUNCE_DAMPEN = 2f;

	// Token: 0x04003B69 RID: 15209
	private const int BOUNCE_COUNT = 2;

	// Token: 0x04003B6A RID: 15210
	private const float OFFSETX = 100f;

	// Token: 0x04003B6B RID: 15211
	private float speed = 1f;

	// Token: 0x04003B6C RID: 15212
	private int bounceCount = 2;

	// Token: 0x04003B6D RID: 15213
	private LayoutElement layoutElement;

	// Token: 0x04003B6E RID: 15214
	[SerializeField]
	private bool animating = true;
}
