using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000BD1 RID: 3025
public class RailGunSideScreen : SideScreenContent
{
	// Token: 0x06005F21 RID: 24353 RVA: 0x0022CC5C File Offset: 0x0022AE5C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.unitsLabel.text = GameUtil.GetCurrentMassUnit(false);
		this.slider.onDrag += delegate
		{
			this.ReceiveValueFromSlider(this.slider.value);
		};
		this.slider.onPointerDown += delegate
		{
			this.ReceiveValueFromSlider(this.slider.value);
		};
		this.slider.onMove += delegate
		{
			this.ReceiveValueFromSlider(this.slider.value);
		};
		this.numberInput.onEndEdit += delegate
		{
			this.ReceiveValueFromInput(this.numberInput.currentValue);
		};
		this.numberInput.decimalPlaces = 1;
	}

	// Token: 0x06005F22 RID: 24354 RVA: 0x0022CCED File Offset: 0x0022AEED
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.selectedGun)
		{
			this.selectedGun = null;
		}
	}

	// Token: 0x06005F23 RID: 24355 RVA: 0x0022CD09 File Offset: 0x0022AF09
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.selectedGun)
		{
			this.selectedGun = null;
		}
	}

	// Token: 0x06005F24 RID: 24356 RVA: 0x0022CD25 File Offset: 0x0022AF25
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<RailGun>() != null;
	}

	// Token: 0x06005F25 RID: 24357 RVA: 0x0022CD34 File Offset: 0x0022AF34
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.selectedGun = new_target.GetComponent<RailGun>();
		if (this.selectedGun == null)
		{
			global::Debug.LogError("The gameObject received does not contain a RailGun component");
			return;
		}
		this.targetRailgunHEPStorageSubHandle = this.selectedGun.Subscribe(-1837862626, new Action<object>(this.UpdateHEPLabels));
		this.slider.minValue = this.selectedGun.MinLaunchMass;
		this.slider.maxValue = this.selectedGun.MaxLaunchMass;
		this.slider.value = this.selectedGun.launchMass;
		this.unitsLabel.text = GameUtil.GetCurrentMassUnit(false);
		this.numberInput.minValue = this.selectedGun.MinLaunchMass;
		this.numberInput.maxValue = this.selectedGun.MaxLaunchMass;
		this.numberInput.currentValue = Mathf.Max(this.selectedGun.MinLaunchMass, Mathf.Min(this.selectedGun.MaxLaunchMass, this.selectedGun.launchMass));
		this.UpdateMaxCapacityLabel();
		this.numberInput.Activate();
		this.UpdateHEPLabels(null);
	}

	// Token: 0x06005F26 RID: 24358 RVA: 0x0022CE6E File Offset: 0x0022B06E
	public override void ClearTarget()
	{
		if (this.targetRailgunHEPStorageSubHandle != -1 && this.selectedGun != null)
		{
			this.selectedGun.Unsubscribe(this.targetRailgunHEPStorageSubHandle);
			this.targetRailgunHEPStorageSubHandle = -1;
		}
		this.selectedGun = null;
	}

	// Token: 0x06005F27 RID: 24359 RVA: 0x0022CEA8 File Offset: 0x0022B0A8
	public void UpdateHEPLabels(object data = null)
	{
		if (this.selectedGun == null)
		{
			return;
		}
		string text = BUILDINGS.PREFABS.RAILGUN.SIDESCREEN_HEP_REQUIRED;
		text = text.Replace("{current}", this.selectedGun.CurrentEnergy.ToString());
		text = text.Replace("{required}", this.selectedGun.EnergyCost.ToString());
		this.hepStorageInfo.text = text;
	}

	// Token: 0x06005F28 RID: 24360 RVA: 0x0022CF19 File Offset: 0x0022B119
	private void ReceiveValueFromSlider(float newValue)
	{
		this.UpdateMaxCapacity(newValue);
	}

	// Token: 0x06005F29 RID: 24361 RVA: 0x0022CF22 File Offset: 0x0022B122
	private void ReceiveValueFromInput(float newValue)
	{
		this.UpdateMaxCapacity(newValue);
	}

	// Token: 0x06005F2A RID: 24362 RVA: 0x0022CF2B File Offset: 0x0022B12B
	private void UpdateMaxCapacity(float newValue)
	{
		this.selectedGun.launchMass = newValue;
		this.slider.value = newValue;
		this.UpdateMaxCapacityLabel();
	}

	// Token: 0x06005F2B RID: 24363 RVA: 0x0022CF4B File Offset: 0x0022B14B
	private void UpdateMaxCapacityLabel()
	{
		this.numberInput.SetDisplayValue(this.selectedGun.launchMass.ToString());
	}

	// Token: 0x0400411B RID: 16667
	public GameObject content;

	// Token: 0x0400411C RID: 16668
	private RailGun selectedGun;

	// Token: 0x0400411D RID: 16669
	public LocText DescriptionText;

	// Token: 0x0400411E RID: 16670
	[Header("Slider")]
	[SerializeField]
	private KSlider slider;

	// Token: 0x0400411F RID: 16671
	[Header("Number Input")]
	[SerializeField]
	private KNumberInputField numberInput;

	// Token: 0x04004120 RID: 16672
	[SerializeField]
	private LocText unitsLabel;

	// Token: 0x04004121 RID: 16673
	[SerializeField]
	private LocText hepStorageInfo;

	// Token: 0x04004122 RID: 16674
	private int targetRailgunHEPStorageSubHandle = -1;
}
