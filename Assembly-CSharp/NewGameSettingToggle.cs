using System;
using Klei.CustomSettings;
using UnityEngine;

// Token: 0x02000B3A RID: 2874
public class NewGameSettingToggle : NewGameSettingWidget
{
	// Token: 0x06005901 RID: 22785 RVA: 0x00203E38 File Offset: 0x00202038
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle toggle = this.Toggle;
		toggle.onClick = (System.Action)Delegate.Combine(toggle.onClick, new System.Action(this.ToggleSetting));
	}

	// Token: 0x06005902 RID: 22786 RVA: 0x00203E67 File Offset: 0x00202067
	public void Initialize(ToggleSettingConfig config, NewGameSettingsPanel panel, string disabledDefault)
	{
		base.Initialize(config, panel, disabledDefault);
		this.config = config;
		this.Label.text = config.label;
		this.ToolTip.toolTip = config.tooltip;
	}

	// Token: 0x06005903 RID: 22787 RVA: 0x00203E9C File Offset: 0x0020209C
	public override void Refresh()
	{
		base.Refresh();
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(this.config);
		this.Toggle.ChangeState(this.config.IsOnLevel(currentQualitySetting.id) ? 1 : 0);
		this.ToggleToolTip.toolTip = currentQualitySetting.tooltip;
	}

	// Token: 0x06005904 RID: 22788 RVA: 0x00203EF3 File Offset: 0x002020F3
	public void ToggleSetting()
	{
		if (base.IsEnabled())
		{
			CustomGameSettings.Instance.ToggleSettingLevel(this.config);
			base.RefreshAll();
		}
	}

	// Token: 0x04003C15 RID: 15381
	[SerializeField]
	private LocText Label;

	// Token: 0x04003C16 RID: 15382
	[SerializeField]
	private ToolTip ToolTip;

	// Token: 0x04003C17 RID: 15383
	[SerializeField]
	private MultiToggle Toggle;

	// Token: 0x04003C18 RID: 15384
	[SerializeField]
	private ToolTip ToggleToolTip;

	// Token: 0x04003C19 RID: 15385
	private ToggleSettingConfig config;
}
