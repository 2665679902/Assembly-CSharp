using System;
using UnityEngine;

// Token: 0x02000BE3 RID: 3043
[Serializable]
public class SliderSet
{
	// Token: 0x06005FDF RID: 24543 RVA: 0x0023146C File Offset: 0x0022F66C
	public void SetupSlider(int index)
	{
		this.index = index;
		this.valueSlider.onReleaseHandle += delegate
		{
			this.valueSlider.value = Mathf.Round(this.valueSlider.value * 10f) / 10f;
			this.ReceiveValueFromSlider();
		};
		this.valueSlider.onDrag += delegate
		{
			this.ReceiveValueFromSlider();
		};
		this.valueSlider.onMove += delegate
		{
			this.ReceiveValueFromSlider();
		};
		this.valueSlider.onPointerDown += delegate
		{
			this.ReceiveValueFromSlider();
		};
		this.numberInput.onEndEdit += delegate
		{
			this.ReceiveValueFromInput();
		};
	}

	// Token: 0x06005FE0 RID: 24544 RVA: 0x002314F4 File Offset: 0x0022F6F4
	public void SetTarget(ISliderControl target)
	{
		this.target = target;
		ToolTip component = this.valueSlider.handleRect.GetComponent<ToolTip>();
		if (component != null)
		{
			component.SetSimpleTooltip(target.GetSliderTooltip());
		}
		this.unitsLabel.text = target.SliderUnits;
		this.minLabel.text = target.GetSliderMin(this.index).ToString() + target.SliderUnits;
		this.maxLabel.text = target.GetSliderMax(this.index).ToString() + target.SliderUnits;
		this.numberInput.minValue = target.GetSliderMin(this.index);
		this.numberInput.maxValue = target.GetSliderMax(this.index);
		this.numberInput.decimalPlaces = target.SliderDecimalPlaces(this.index);
		this.numberInput.field.characterLimit = Mathf.FloorToInt(1f + Mathf.Log10(this.numberInput.maxValue + (float)this.numberInput.decimalPlaces));
		Vector2 sizeDelta = this.numberInput.GetComponent<RectTransform>().sizeDelta;
		sizeDelta.x = (float)((this.numberInput.field.characterLimit + 1) * 10);
		this.numberInput.GetComponent<RectTransform>().sizeDelta = sizeDelta;
		this.valueSlider.minValue = target.GetSliderMin(this.index);
		this.valueSlider.maxValue = target.GetSliderMax(this.index);
		this.valueSlider.value = target.GetSliderValue(this.index);
		this.SetValue(target.GetSliderValue(this.index));
		if (this.index == 0)
		{
			this.numberInput.Activate();
		}
	}

	// Token: 0x06005FE1 RID: 24545 RVA: 0x002316BC File Offset: 0x0022F8BC
	private void ReceiveValueFromSlider()
	{
		float num = this.valueSlider.value;
		if (this.numberInput.decimalPlaces != -1)
		{
			float num2 = Mathf.Pow(10f, (float)this.numberInput.decimalPlaces);
			num = Mathf.Round(num * num2) / num2;
		}
		this.SetValue(num);
	}

	// Token: 0x06005FE2 RID: 24546 RVA: 0x0023170C File Offset: 0x0022F90C
	private void ReceiveValueFromInput()
	{
		float num = this.numberInput.currentValue;
		if (this.numberInput.decimalPlaces != -1)
		{
			float num2 = Mathf.Pow(10f, (float)this.numberInput.decimalPlaces);
			num = Mathf.Round(num * num2) / num2;
		}
		this.valueSlider.value = num;
		this.SetValue(num);
	}

	// Token: 0x06005FE3 RID: 24547 RVA: 0x00231768 File Offset: 0x0022F968
	private void SetValue(float value)
	{
		float num = value;
		if (num > this.target.GetSliderMax(this.index))
		{
			num = this.target.GetSliderMax(this.index);
		}
		else if (num < this.target.GetSliderMin(this.index))
		{
			num = this.target.GetSliderMin(this.index);
		}
		this.UpdateLabel(num);
		this.target.SetSliderValue(num, this.index);
		ToolTip component = this.valueSlider.handleRect.GetComponent<ToolTip>();
		if (component != null)
		{
			component.SetSimpleTooltip(this.target.GetSliderTooltip());
		}
	}

	// Token: 0x06005FE4 RID: 24548 RVA: 0x0023180C File Offset: 0x0022FA0C
	private void UpdateLabel(float value)
	{
		float num = Mathf.Round(value * 10f) / 10f;
		this.numberInput.SetDisplayValue(num.ToString());
	}

	// Token: 0x040041A5 RID: 16805
	public KSlider valueSlider;

	// Token: 0x040041A6 RID: 16806
	public KNumberInputField numberInput;

	// Token: 0x040041A7 RID: 16807
	public LocText unitsLabel;

	// Token: 0x040041A8 RID: 16808
	public LocText minLabel;

	// Token: 0x040041A9 RID: 16809
	public LocText maxLabel;

	// Token: 0x040041AA RID: 16810
	[NonSerialized]
	public int index;

	// Token: 0x040041AB RID: 16811
	private ISliderControl target;
}
