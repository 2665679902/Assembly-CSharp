using System;
using Klei.CustomSettings;
using UnityEngine;

// Token: 0x02000B38 RID: 2872
public class NewGameSettingList : NewGameSettingWidget
{
	// Token: 0x060058F2 RID: 22770 RVA: 0x00203B21 File Offset: 0x00201D21
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.CycleLeft.onClick += this.DoCycleLeft;
		this.CycleRight.onClick += this.DoCycleRight;
	}

	// Token: 0x060058F3 RID: 22771 RVA: 0x00203B57 File Offset: 0x00201D57
	public void Initialize(ListSettingConfig config, NewGameSettingsPanel panel, string disabledDefault)
	{
		base.Initialize(config, panel, disabledDefault);
		this.config = config;
		this.Label.text = config.label;
		this.ToolTip.toolTip = config.tooltip;
	}

	// Token: 0x060058F4 RID: 22772 RVA: 0x00203B8C File Offset: 0x00201D8C
	public override void Refresh()
	{
		base.Refresh();
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(this.config);
		this.ValueLabel.text = currentQualitySetting.label;
		this.ValueToolTip.toolTip = currentQualitySetting.tooltip;
		this.CycleLeft.isInteractable = !this.config.IsFirstLevel(currentQualitySetting.id);
		this.CycleRight.isInteractable = !this.config.IsLastLevel(currentQualitySetting.id);
	}

	// Token: 0x060058F5 RID: 22773 RVA: 0x00203C10 File Offset: 0x00201E10
	private void DoCycleLeft()
	{
		if (base.IsEnabled())
		{
			CustomGameSettings.Instance.CycleSettingLevel(this.config, -1);
			base.RefreshAll();
		}
	}

	// Token: 0x060058F6 RID: 22774 RVA: 0x00203C32 File Offset: 0x00201E32
	private void DoCycleRight()
	{
		if (base.IsEnabled())
		{
			CustomGameSettings.Instance.CycleSettingLevel(this.config, 1);
			base.RefreshAll();
		}
	}

	// Token: 0x04003C08 RID: 15368
	[SerializeField]
	private LocText Label;

	// Token: 0x04003C09 RID: 15369
	[SerializeField]
	private ToolTip ToolTip;

	// Token: 0x04003C0A RID: 15370
	[SerializeField]
	private LocText ValueLabel;

	// Token: 0x04003C0B RID: 15371
	[SerializeField]
	private ToolTip ValueToolTip;

	// Token: 0x04003C0C RID: 15372
	[SerializeField]
	private KButton CycleLeft;

	// Token: 0x04003C0D RID: 15373
	[SerializeField]
	private KButton CycleRight;

	// Token: 0x04003C0E RID: 15374
	private ListSettingConfig config;
}
