using System;
using UnityEngine;

// Token: 0x02000A80 RID: 2688
public class DateTime : KScreen
{
	// Token: 0x06005235 RID: 21045 RVA: 0x001DAE55 File Offset: 0x001D9055
	public static void DestroyInstance()
	{
		global::DateTime.Instance = null;
	}

	// Token: 0x06005236 RID: 21046 RVA: 0x001DAE5D File Offset: 0x001D905D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		global::DateTime.Instance = this;
	}

	// Token: 0x06005237 RID: 21047 RVA: 0x001DAE6B File Offset: 0x001D906B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.tooltip.OnComplexToolTip = new ToolTip.ComplexTooltipDelegate(SaveGame.Instance.GetColonyToolTip);
	}

	// Token: 0x06005238 RID: 21048 RVA: 0x001DAE8E File Offset: 0x001D908E
	private void Update()
	{
		if (GameClock.Instance != null && this.displayedDayCount != GameUtil.GetCurrentCycle())
		{
			this.text.text = this.Days();
			this.displayedDayCount = GameUtil.GetCurrentCycle();
		}
	}

	// Token: 0x06005239 RID: 21049 RVA: 0x001DAEC8 File Offset: 0x001D90C8
	private string Days()
	{
		return GameUtil.GetCurrentCycle().ToString();
	}

	// Token: 0x04003772 RID: 14194
	public static global::DateTime Instance;

	// Token: 0x04003773 RID: 14195
	public LocText day;

	// Token: 0x04003774 RID: 14196
	private int displayedDayCount = -1;

	// Token: 0x04003775 RID: 14197
	[SerializeField]
	private LocText text;

	// Token: 0x04003776 RID: 14198
	[SerializeField]
	private ToolTip tooltip;

	// Token: 0x04003777 RID: 14199
	[SerializeField]
	private TextStyleSetting tooltipstyle_Days;

	// Token: 0x04003778 RID: 14200
	[SerializeField]
	private TextStyleSetting tooltipstyle_Playtime;

	// Token: 0x04003779 RID: 14201
	[SerializeField]
	public KToggle scheduleToggle;
}
