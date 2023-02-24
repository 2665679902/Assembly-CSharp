using System;
using UnityEngine;

// Token: 0x02000B9B RID: 2971
public class CapacityControlSideScreen : SideScreenContent
{
	// Token: 0x06005D67 RID: 23911 RVA: 0x00221DBC File Offset: 0x0021FFBC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.unitsLabel.text = this.target.CapacityUnits;
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

	// Token: 0x06005D68 RID: 23912 RVA: 0x00221E52 File Offset: 0x00220052
	public override bool IsValidForTarget(GameObject target)
	{
		return !target.GetComponent<IUserControlledCapacity>().IsNullOrDestroyed();
	}

	// Token: 0x06005D69 RID: 23913 RVA: 0x00221E64 File Offset: 0x00220064
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<IUserControlledCapacity>();
		if (this.target == null)
		{
			global::Debug.LogError("The gameObject received does not contain a IThresholdSwitch component");
			return;
		}
		this.slider.minValue = this.target.MinCapacity;
		this.slider.maxValue = this.target.MaxCapacity;
		this.slider.value = this.target.UserMaxCapacity;
		this.slider.GetComponentInChildren<ToolTip>();
		this.unitsLabel.text = this.target.CapacityUnits;
		this.numberInput.minValue = this.target.MinCapacity;
		this.numberInput.maxValue = this.target.MaxCapacity;
		this.numberInput.currentValue = Mathf.Max(this.target.MinCapacity, Mathf.Min(this.target.MaxCapacity, this.target.UserMaxCapacity));
		this.numberInput.Activate();
		this.UpdateMaxCapacityLabel();
	}

	// Token: 0x06005D6A RID: 23914 RVA: 0x00221F80 File Offset: 0x00220180
	private void ReceiveValueFromSlider(float newValue)
	{
		this.UpdateMaxCapacity(newValue);
	}

	// Token: 0x06005D6B RID: 23915 RVA: 0x00221F89 File Offset: 0x00220189
	private void ReceiveValueFromInput(float newValue)
	{
		this.UpdateMaxCapacity(newValue);
	}

	// Token: 0x06005D6C RID: 23916 RVA: 0x00221F92 File Offset: 0x00220192
	private void UpdateMaxCapacity(float newValue)
	{
		this.target.UserMaxCapacity = newValue;
		this.slider.value = newValue;
		this.UpdateMaxCapacityLabel();
	}

	// Token: 0x06005D6D RID: 23917 RVA: 0x00221FB4 File Offset: 0x002201B4
	private void UpdateMaxCapacityLabel()
	{
		this.numberInput.SetDisplayValue(this.target.UserMaxCapacity.ToString());
	}

	// Token: 0x04003FD8 RID: 16344
	private IUserControlledCapacity target;

	// Token: 0x04003FD9 RID: 16345
	[Header("Slider")]
	[SerializeField]
	private KSlider slider;

	// Token: 0x04003FDA RID: 16346
	[Header("Number Input")]
	[SerializeField]
	private KNumberInputField numberInput;

	// Token: 0x04003FDB RID: 16347
	[SerializeField]
	private LocText unitsLabel;
}
