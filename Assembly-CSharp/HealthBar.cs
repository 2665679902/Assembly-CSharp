using System;
using UnityEngine;

// Token: 0x02000AB1 RID: 2737
public class HealthBar : ProgressBar
{
	// Token: 0x17000635 RID: 1589
	// (get) Token: 0x060053D8 RID: 21464 RVA: 0x001E7D90 File Offset: 0x001E5F90
	private bool ShouldShow
	{
		get
		{
			return this.showTimer > 0f || base.PercentFull < this.alwaysShowThreshold;
		}
	}

	// Token: 0x060053D9 RID: 21465 RVA: 0x001E7DAF File Offset: 0x001E5FAF
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.barColor = ProgressBarsConfig.Instance.GetBarColor("HealthBar");
		base.gameObject.SetActive(this.ShouldShow);
	}

	// Token: 0x060053DA RID: 21466 RVA: 0x001E7DDD File Offset: 0x001E5FDD
	public void OnChange()
	{
		base.enabled = true;
		this.showTimer = this.maxShowTime;
	}

	// Token: 0x060053DB RID: 21467 RVA: 0x001E7DF4 File Offset: 0x001E5FF4
	public override void Update()
	{
		base.Update();
		if (Time.timeScale > 0f)
		{
			this.showTimer = Mathf.Max(0f, this.showTimer - Time.unscaledDeltaTime);
		}
		if (!this.ShouldShow)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060053DC RID: 21468 RVA: 0x001E7E43 File Offset: 0x001E6043
	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	// Token: 0x060053DD RID: 21469 RVA: 0x001E7E4C File Offset: 0x001E604C
	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	// Token: 0x060053DE RID: 21470 RVA: 0x001E7E58 File Offset: 0x001E6058
	public override void OnOverlayChanged(object data = null)
	{
		if (!this.autoHide)
		{
			return;
		}
		if ((HashedString)data == OverlayModes.None.ID)
		{
			if (!base.gameObject.activeSelf && this.ShouldShow)
			{
				base.enabled = true;
				base.gameObject.SetActive(true);
				return;
			}
		}
		else if (base.gameObject.activeSelf)
		{
			base.enabled = false;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04003902 RID: 14594
	private float showTimer;

	// Token: 0x04003903 RID: 14595
	private float maxShowTime = 10f;

	// Token: 0x04003904 RID: 14596
	private float alwaysShowThreshold = 0.8f;
}
