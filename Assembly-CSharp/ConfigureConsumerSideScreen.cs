using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BA6 RID: 2982
public class ConfigureConsumerSideScreen : SideScreenContent
{
	// Token: 0x06005DD6 RID: 24022 RVA: 0x00225077 File Offset: 0x00223277
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IConfigurableConsumer>() != null;
	}

	// Token: 0x06005DD7 RID: 24023 RVA: 0x00225082 File Offset: 0x00223282
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetProducer = target.GetComponent<IConfigurableConsumer>();
		if (this.settings == null)
		{
			this.settings = this.targetProducer.GetSettingOptions();
		}
		this.PopulateOptions();
	}

	// Token: 0x06005DD8 RID: 24024 RVA: 0x002250B8 File Offset: 0x002232B8
	private void ClearOldOptions()
	{
		if (this.descriptor != null)
		{
			this.descriptor.gameObject.SetActive(false);
		}
		for (int i = 0; i < this.settingToggles.Count; i++)
		{
			this.settingToggles[i].gameObject.SetActive(false);
		}
	}

	// Token: 0x06005DD9 RID: 24025 RVA: 0x00225114 File Offset: 0x00223314
	private void PopulateOptions()
	{
		this.ClearOldOptions();
		for (int i = this.settingToggles.Count; i < this.settings.Length; i++)
		{
			IConfigurableConsumerOption setting = this.settings[i];
			HierarchyReferences component = Util.KInstantiateUI(this.consumptionSettingTogglePrefab, this.consumptionSettingToggleContainer.gameObject, true).GetComponent<HierarchyReferences>();
			this.settingToggles.Add(component);
			component.GetReference<LocText>("Label").text = setting.GetName();
			component.GetReference<Image>("Image").sprite = setting.GetIcon();
			MultiToggle reference = component.GetReference<MultiToggle>("Toggle");
			reference.onClick = (System.Action)Delegate.Combine(reference.onClick, new System.Action(delegate
			{
				this.SelectOption(setting);
			}));
		}
		this.RefreshToggles();
		this.RefreshDetails();
	}

	// Token: 0x06005DDA RID: 24026 RVA: 0x002251FE File Offset: 0x002233FE
	private void SelectOption(IConfigurableConsumerOption option)
	{
		this.targetProducer.SetSelectedOption(option);
		this.RefreshToggles();
		this.RefreshDetails();
	}

	// Token: 0x06005DDB RID: 24027 RVA: 0x00225218 File Offset: 0x00223418
	private void RefreshToggles()
	{
		for (int i = 0; i < this.settingToggles.Count; i++)
		{
			MultiToggle reference = this.settingToggles[i].GetReference<MultiToggle>("Toggle");
			reference.ChangeState((this.settings[i] == this.targetProducer.GetSelectedOption()) ? 1 : 0);
			reference.gameObject.SetActive(true);
		}
	}

	// Token: 0x06005DDC RID: 24028 RVA: 0x0022527C File Offset: 0x0022347C
	private void RefreshDetails()
	{
		if (this.descriptor == null)
		{
			GameObject gameObject = Util.KInstantiateUI(this.settingDescriptorPrefab, this.settingEffectRowsContainer.gameObject, true);
			this.descriptor = gameObject.GetComponent<LocText>();
		}
		IConfigurableConsumerOption selectedOption = this.targetProducer.GetSelectedOption();
		if (selectedOption != null)
		{
			this.descriptor.text = selectedOption.GetDetailedDescription();
			this.selectedOptionNameLabel.text = "<b>" + selectedOption.GetName() + "</b>";
			this.descriptor.gameObject.SetActive(true);
		}
	}

	// Token: 0x06005DDD RID: 24029 RVA: 0x0022530C File Offset: 0x0022350C
	public override int GetSideScreenSortOrder()
	{
		return 1;
	}

	// Token: 0x04004030 RID: 16432
	[SerializeField]
	private RectTransform consumptionSettingToggleContainer;

	// Token: 0x04004031 RID: 16433
	[SerializeField]
	private GameObject consumptionSettingTogglePrefab;

	// Token: 0x04004032 RID: 16434
	[SerializeField]
	private RectTransform settingRequirementRowsContainer;

	// Token: 0x04004033 RID: 16435
	[SerializeField]
	private RectTransform settingEffectRowsContainer;

	// Token: 0x04004034 RID: 16436
	[SerializeField]
	private LocText selectedOptionNameLabel;

	// Token: 0x04004035 RID: 16437
	[SerializeField]
	private GameObject settingDescriptorPrefab;

	// Token: 0x04004036 RID: 16438
	private IConfigurableConsumer targetProducer;

	// Token: 0x04004037 RID: 16439
	private IConfigurableConsumerOption[] settings;

	// Token: 0x04004038 RID: 16440
	private LocText descriptor;

	// Token: 0x04004039 RID: 16441
	private List<HierarchyReferences> settingToggles = new List<HierarchyReferences>();

	// Token: 0x0400403A RID: 16442
	private List<GameObject> requirementRows = new List<GameObject>();
}
