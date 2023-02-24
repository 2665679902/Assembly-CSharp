using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class WidgetTransition : MonoBehaviour
{
	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00017D2C File Offset: 0x00015F2C
	private CanvasGroup CanvasGroup
	{
		get
		{
			if (!(this.canvasGroup == null))
			{
				return this.canvasGroup;
			}
			return this.canvasGroup = base.gameObject.FindOrAddUnityComponent<CanvasGroup>();
		}
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00017D62 File Offset: 0x00015F62
	public void SetTransitionType(WidgetTransition.TransitionType transitionType)
	{
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00017D64 File Offset: 0x00015F64
	public void StartTransition()
	{
		if (this.fadingIn)
		{
			return;
		}
		this.CanvasGroup.alpha = 0f;
		this.fadingIn = true;
		base.enabled = true;
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x00017D8D File Offset: 0x00015F8D
	public void StopTransition()
	{
		if (this.fadingIn)
		{
			this.fadingIn = false;
			base.enabled = false;
		}
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x00017DA8 File Offset: 0x00015FA8
	private void Update()
	{
		if (this.fadingIn)
		{
			float num = this.CanvasGroup.alpha;
			num += 6f * Time.unscaledDeltaTime;
			if (num >= 1f)
			{
				num = 1f;
			}
			if (num == 1f)
			{
				this.fadingIn = false;
				base.enabled = false;
			}
			this.CanvasGroup.alpha = num;
		}
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x00017E07 File Offset: 0x00016007
	private void OnDisable()
	{
		this.StopTransition();
	}

	// Token: 0x04000500 RID: 1280
	private const float OFFSETX = 50f;

	// Token: 0x04000501 RID: 1281
	private const float SLIDE_SPEED = 7f;

	// Token: 0x04000502 RID: 1282
	private const float FADEIN_SPEED = 6f;

	// Token: 0x04000503 RID: 1283
	private bool fadingIn;

	// Token: 0x04000504 RID: 1284
	private CanvasGroup canvasGroup;

	// Token: 0x020009C8 RID: 2504
	public enum TransitionType
	{
		// Token: 0x040021E5 RID: 8677
		SlideFromRight,
		// Token: 0x040021E6 RID: 8678
		SlideFromLeft,
		// Token: 0x040021E7 RID: 8679
		FadeOnly,
		// Token: 0x040021E8 RID: 8680
		SlideFromTop
	}
}
