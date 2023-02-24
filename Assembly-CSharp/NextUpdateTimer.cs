using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B3D RID: 2877
[AddComponentMenu("KMonoBehaviour/scripts/NextUpdateTimer")]
public class NextUpdateTimer : KMonoBehaviour
{
	// Token: 0x06005916 RID: 22806 RVA: 0x00204297 File Offset: 0x00202497
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.initialAnimScale = this.UpdateAnimController.animScale;
	}

	// Token: 0x06005917 RID: 22807 RVA: 0x002042B0 File Offset: 0x002024B0
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06005918 RID: 22808 RVA: 0x002042B8 File Offset: 0x002024B8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RefreshReleaseTimes();
	}

	// Token: 0x06005919 RID: 22809 RVA: 0x002042C8 File Offset: 0x002024C8
	public void UpdateReleaseTimes(string lastUpdateTime, string nextUpdateTime, string textOverride)
	{
		if (!System.DateTime.TryParse(lastUpdateTime, out this.currentReleaseDate))
		{
			global::Debug.LogWarning("Failed to parse last_update_time: " + lastUpdateTime);
		}
		if (!System.DateTime.TryParse(nextUpdateTime, out this.nextReleaseDate))
		{
			global::Debug.LogWarning("Failed to parse next_update_time: " + nextUpdateTime);
		}
		this.m_releaseTextOverride = textOverride;
		this.RefreshReleaseTimes();
	}

	// Token: 0x0600591A RID: 22810 RVA: 0x00204320 File Offset: 0x00202520
	private void RefreshReleaseTimes()
	{
		TimeSpan timeSpan = this.nextReleaseDate - this.currentReleaseDate;
		TimeSpan timeSpan2 = this.nextReleaseDate - System.DateTime.UtcNow;
		TimeSpan timeSpan3 = System.DateTime.UtcNow - this.currentReleaseDate;
		string text = "4";
		string text2;
		if (!string.IsNullOrEmpty(this.m_releaseTextOverride))
		{
			text2 = this.m_releaseTextOverride;
		}
		else if (timeSpan2.TotalHours < 8.0)
		{
			text2 = UI.DEVELOPMENTBUILDS.UPDATES.TWENTY_FOUR_HOURS;
			text = "4";
		}
		else if (timeSpan2.TotalDays < 1.0)
		{
			text2 = string.Format(UI.DEVELOPMENTBUILDS.UPDATES.FINAL_WEEK, 1);
			text = "3";
		}
		else
		{
			int num = timeSpan2.Days % 7;
			int num2 = (timeSpan2.Days - num) / 7;
			if (num2 <= 0)
			{
				text2 = string.Format(UI.DEVELOPMENTBUILDS.UPDATES.FINAL_WEEK, num);
				text = "2";
			}
			else
			{
				text2 = string.Format(UI.DEVELOPMENTBUILDS.UPDATES.BIGGER_TIMES, num, num2);
				text = "1";
			}
		}
		this.TimerText.text = text2;
		this.UpdateAnimController.Play(text, KAnim.PlayMode.Loop, 1f, 0f);
		float num3 = Mathf.Clamp01((float)(timeSpan3.TotalSeconds / timeSpan.TotalSeconds));
		this.UpdateAnimMeterController.SetPositionPercent(num3);
	}

	// Token: 0x04003C2B RID: 15403
	public LocText TimerText;

	// Token: 0x04003C2C RID: 15404
	public KBatchedAnimController UpdateAnimController;

	// Token: 0x04003C2D RID: 15405
	public KBatchedAnimController UpdateAnimMeterController;

	// Token: 0x04003C2E RID: 15406
	public float initialAnimScale;

	// Token: 0x04003C2F RID: 15407
	public System.DateTime nextReleaseDate;

	// Token: 0x04003C30 RID: 15408
	public System.DateTime currentReleaseDate;

	// Token: 0x04003C31 RID: 15409
	private string m_releaseTextOverride;
}
