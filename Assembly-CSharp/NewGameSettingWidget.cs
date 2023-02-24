using System;
using Klei.CustomSettings;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B3B RID: 2875
public abstract class NewGameSettingWidget : KMonoBehaviour
{
	// Token: 0x06005906 RID: 22790 RVA: 0x00203F1C File Offset: 0x0020211C
	protected virtual void Initialize(SettingConfig config, NewGameSettingsPanel panel, string disabledDefault)
	{
		this.config = config;
		this.panel = panel;
		this.disabledDefault = disabledDefault;
	}

	// Token: 0x06005907 RID: 22791 RVA: 0x00203F34 File Offset: 0x00202134
	public virtual void Refresh()
	{
		bool flag = this.ShouldBeEnabled();
		if (flag == this.widget_enabled)
		{
			return;
		}
		this.widget_enabled = flag;
		if (this.IsEnabled())
		{
			this.BG.color = this.enabledColor;
			CustomGameSettings.Instance.SetQualitySetting(this.config, this.config.GetDefaultLevelId());
			return;
		}
		CustomGameSettings.Instance.SetQualitySetting(this.config, this.disabledDefault);
		this.BG.color = this.disabledColor;
	}

	// Token: 0x06005908 RID: 22792 RVA: 0x00203FB5 File Offset: 0x002021B5
	protected void RefreshAll()
	{
		this.panel.Refresh();
	}

	// Token: 0x06005909 RID: 22793 RVA: 0x00203FC2 File Offset: 0x002021C2
	protected bool IsEnabled()
	{
		return this.widget_enabled;
	}

	// Token: 0x0600590A RID: 22794 RVA: 0x00203FCA File Offset: 0x002021CA
	private bool ShouldBeEnabled()
	{
		return true;
	}

	// Token: 0x04003C1A RID: 15386
	[SerializeField]
	private Image BG;

	// Token: 0x04003C1B RID: 15387
	[SerializeField]
	private Color enabledColor;

	// Token: 0x04003C1C RID: 15388
	[SerializeField]
	private Color disabledColor;

	// Token: 0x04003C1D RID: 15389
	private SettingConfig config;

	// Token: 0x04003C1E RID: 15390
	private NewGameSettingsPanel panel;

	// Token: 0x04003C1F RID: 15391
	private string disabledDefault;

	// Token: 0x04003C20 RID: 15392
	private bool widget_enabled = true;
}
