using System;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000B8E RID: 2958
public class ActiveRangeSideScreen : SideScreenContent
{
	// Token: 0x06005CEE RID: 23790 RVA: 0x0021F66B File Offset: 0x0021D86B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06005CEF RID: 23791 RVA: 0x0021F674 File Offset: 0x0021D874
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.activateValueLabel.maxValue = this.target.MaxValue;
		this.activateValueLabel.minValue = this.target.MinValue;
		this.deactivateValueLabel.maxValue = this.target.MaxValue;
		this.deactivateValueLabel.minValue = this.target.MinValue;
		this.activateValueSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnActivateValueChanged));
		this.deactivateValueSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnDeactivateValueChanged));
	}

	// Token: 0x06005CF0 RID: 23792 RVA: 0x0021F718 File Offset: 0x0021D918
	private void OnActivateValueChanged(float new_value)
	{
		this.target.ActivateValue = new_value;
		if (this.target.ActivateValue < this.target.DeactivateValue)
		{
			this.target.ActivateValue = this.target.DeactivateValue;
			this.activateValueSlider.value = this.target.ActivateValue;
		}
		this.activateValueLabel.SetDisplayValue(this.target.ActivateValue.ToString());
		this.RefreshTooltips();
	}

	// Token: 0x06005CF1 RID: 23793 RVA: 0x0021F79C File Offset: 0x0021D99C
	private void OnDeactivateValueChanged(float new_value)
	{
		this.target.DeactivateValue = new_value;
		if (this.target.DeactivateValue > this.target.ActivateValue)
		{
			this.target.DeactivateValue = this.activateValueSlider.value;
			this.deactivateValueSlider.value = this.target.DeactivateValue;
		}
		this.deactivateValueLabel.SetDisplayValue(this.target.DeactivateValue.ToString());
		this.RefreshTooltips();
	}

	// Token: 0x06005CF2 RID: 23794 RVA: 0x0021F820 File Offset: 0x0021DA20
	private void RefreshTooltips()
	{
		this.activateValueSlider.GetComponentInChildren<ToolTip>().SetSimpleTooltip(string.Format(this.target.ActivateTooltip, this.activateValueSlider.value, this.deactivateValueSlider.value));
		this.deactivateValueSlider.GetComponentInChildren<ToolTip>().SetSimpleTooltip(string.Format(this.target.DeactivateTooltip, this.deactivateValueSlider.value, this.activateValueSlider.value));
	}

	// Token: 0x06005CF3 RID: 23795 RVA: 0x0021F8AD File Offset: 0x0021DAAD
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IActivationRangeTarget>() != null;
	}

	// Token: 0x06005CF4 RID: 23796 RVA: 0x0021F8B8 File Offset: 0x0021DAB8
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<IActivationRangeTarget>();
		if (this.target == null)
		{
			global::Debug.LogError("The gameObject received does not contain a IActivationRangeTarget component");
			return;
		}
		this.activateLabel.text = this.target.ActivateSliderLabelText;
		this.deactivateLabel.text = this.target.DeactivateSliderLabelText;
		this.activateValueLabel.Activate();
		this.deactivateValueLabel.Activate();
		this.activateValueSlider.onValueChanged.RemoveListener(new UnityAction<float>(this.OnActivateValueChanged));
		this.activateValueSlider.minValue = this.target.MinValue;
		this.activateValueSlider.maxValue = this.target.MaxValue;
		this.activateValueSlider.value = this.target.ActivateValue;
		this.activateValueSlider.wholeNumbers = this.target.UseWholeNumbers;
		this.activateValueSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnActivateValueChanged));
		this.activateValueLabel.SetDisplayValue(this.target.ActivateValue.ToString());
		this.activateValueLabel.onEndEdit += delegate
		{
			float activateValue = this.target.ActivateValue;
			float.TryParse(this.activateValueLabel.field.text, out activateValue);
			this.OnActivateValueChanged(activateValue);
			this.activateValueSlider.value = activateValue;
		};
		this.deactivateValueSlider.onValueChanged.RemoveListener(new UnityAction<float>(this.OnDeactivateValueChanged));
		this.deactivateValueSlider.minValue = this.target.MinValue;
		this.deactivateValueSlider.maxValue = this.target.MaxValue;
		this.deactivateValueSlider.value = this.target.DeactivateValue;
		this.deactivateValueSlider.wholeNumbers = this.target.UseWholeNumbers;
		this.deactivateValueSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnDeactivateValueChanged));
		this.deactivateValueLabel.SetDisplayValue(this.target.DeactivateValue.ToString());
		this.deactivateValueLabel.onEndEdit += delegate
		{
			float deactivateValue = this.target.DeactivateValue;
			float.TryParse(this.deactivateValueLabel.field.text, out deactivateValue);
			this.OnDeactivateValueChanged(deactivateValue);
			this.deactivateValueSlider.value = deactivateValue;
		};
		this.RefreshTooltips();
	}

	// Token: 0x06005CF5 RID: 23797 RVA: 0x0021FACA File Offset: 0x0021DCCA
	public override string GetTitle()
	{
		if (this.target != null)
		{
			return this.target.ActivationRangeTitleText;
		}
		return UI.UISIDESCREENS.ACTIVATION_RANGE_SIDE_SCREEN.NAME;
	}

	// Token: 0x04003F87 RID: 16263
	private IActivationRangeTarget target;

	// Token: 0x04003F88 RID: 16264
	[SerializeField]
	private KSlider activateValueSlider;

	// Token: 0x04003F89 RID: 16265
	[SerializeField]
	private KSlider deactivateValueSlider;

	// Token: 0x04003F8A RID: 16266
	[SerializeField]
	private LocText activateLabel;

	// Token: 0x04003F8B RID: 16267
	[SerializeField]
	private LocText deactivateLabel;

	// Token: 0x04003F8C RID: 16268
	[Header("Number Input")]
	[SerializeField]
	private KNumberInputField activateValueLabel;

	// Token: 0x04003F8D RID: 16269
	[SerializeField]
	private KNumberInputField deactivateValueLabel;
}
