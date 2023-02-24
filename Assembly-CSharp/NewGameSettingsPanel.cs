using System;
using System.Collections.Generic;
using Klei.CustomSettings;
using UnityEngine;

// Token: 0x02000B3C RID: 2876
[AddComponentMenu("KMonoBehaviour/scripts/NewGameSettingsPanel")]
public class NewGameSettingsPanel : KMonoBehaviour
{
	// Token: 0x0600590C RID: 22796 RVA: 0x00203FDC File Offset: 0x002021DC
	public void SetCloseAction(System.Action onClose)
	{
		if (this.closeButton != null)
		{
			this.closeButton.onClick += onClose;
		}
		if (this.background != null)
		{
			this.background.onClick += onClose;
		}
	}

	// Token: 0x0600590D RID: 22797 RVA: 0x00204014 File Offset: 0x00202214
	public void Init()
	{
		CustomGameSettings.Instance.LoadClusters();
		Global.Instance.modManager.Report(base.gameObject);
		this.settings = CustomGameSettings.Instance;
		this.widgets = new List<NewGameSettingWidget>();
		foreach (KeyValuePair<string, SettingConfig> keyValuePair in this.settings.QualitySettings)
		{
			if ((!keyValuePair.Value.debug_only || DebugHandler.enabled) && (!keyValuePair.Value.editor_only || Application.isEditor) && DlcManager.IsContentActive(keyValuePair.Value.required_content))
			{
				ListSettingConfig listSettingConfig = keyValuePair.Value as ListSettingConfig;
				if (listSettingConfig != null)
				{
					NewGameSettingList newGameSettingList = Util.KInstantiateUI<NewGameSettingList>(this.prefab_cycle_setting, this.content.gameObject, true);
					newGameSettingList.Initialize(listSettingConfig, this, keyValuePair.Value.missing_content_default);
					this.widgets.Add(newGameSettingList);
				}
				else
				{
					ToggleSettingConfig toggleSettingConfig = keyValuePair.Value as ToggleSettingConfig;
					if (toggleSettingConfig != null)
					{
						NewGameSettingToggle newGameSettingToggle = Util.KInstantiateUI<NewGameSettingToggle>(this.prefab_checkbox_setting, this.content.gameObject, true);
						newGameSettingToggle.Initialize(toggleSettingConfig, this, keyValuePair.Value.missing_content_default);
						this.widgets.Add(newGameSettingToggle);
					}
					else
					{
						SeedSettingConfig seedSettingConfig = keyValuePair.Value as SeedSettingConfig;
						if (seedSettingConfig != null)
						{
							NewGameSettingSeed newGameSettingSeed = Util.KInstantiateUI<NewGameSettingSeed>(this.prefab_seed_input_setting, this.content.gameObject, true);
							newGameSettingSeed.Initialize(seedSettingConfig);
							this.widgets.Add(newGameSettingSeed);
						}
					}
				}
			}
		}
		this.Refresh();
	}

	// Token: 0x0600590E RID: 22798 RVA: 0x002041D8 File Offset: 0x002023D8
	public void Refresh()
	{
		foreach (NewGameSettingWidget newGameSettingWidget in this.widgets)
		{
			newGameSettingWidget.Refresh();
		}
		if (this.OnRefresh != null)
		{
			this.OnRefresh();
		}
	}

	// Token: 0x0600590F RID: 22799 RVA: 0x0020423C File Offset: 0x0020243C
	public void ConsumeSettingsCode(string code)
	{
		this.settings.ParseAndApplySettingsCode(code);
	}

	// Token: 0x06005910 RID: 22800 RVA: 0x0020424A File Offset: 0x0020244A
	public void ConsumeStoryTraitsCode(string code)
	{
		this.settings.ParseAndApplyStoryTraitSettingsCode(code);
	}

	// Token: 0x06005911 RID: 22801 RVA: 0x00204258 File Offset: 0x00202458
	public void SetSetting(SettingConfig setting, string level)
	{
		this.settings.SetQualitySetting(setting, level);
	}

	// Token: 0x06005912 RID: 22802 RVA: 0x00204267 File Offset: 0x00202467
	public string GetSetting(SettingConfig setting)
	{
		return this.settings.GetCurrentQualitySetting(setting).id;
	}

	// Token: 0x06005913 RID: 22803 RVA: 0x0020427A File Offset: 0x0020247A
	public string GetSetting(string setting)
	{
		return this.settings.GetCurrentQualitySetting(setting).id;
	}

	// Token: 0x06005914 RID: 22804 RVA: 0x0020428D File Offset: 0x0020248D
	public void Cancel()
	{
	}

	// Token: 0x04003C21 RID: 15393
	[SerializeField]
	private Transform content;

	// Token: 0x04003C22 RID: 15394
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003C23 RID: 15395
	[SerializeField]
	private KButton background;

	// Token: 0x04003C24 RID: 15396
	[Header("Prefab UI Refs")]
	[SerializeField]
	private GameObject prefab_cycle_setting;

	// Token: 0x04003C25 RID: 15397
	[SerializeField]
	private GameObject prefab_slider_setting;

	// Token: 0x04003C26 RID: 15398
	[SerializeField]
	private GameObject prefab_checkbox_setting;

	// Token: 0x04003C27 RID: 15399
	[SerializeField]
	private GameObject prefab_seed_input_setting;

	// Token: 0x04003C28 RID: 15400
	private CustomGameSettings settings;

	// Token: 0x04003C29 RID: 15401
	private List<NewGameSettingWidget> widgets;

	// Token: 0x04003C2A RID: 15402
	public System.Action OnRefresh;
}
