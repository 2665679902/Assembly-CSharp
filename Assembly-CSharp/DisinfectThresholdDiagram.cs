using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020009FE RID: 2558
public class DisinfectThresholdDiagram : MonoBehaviour
{
	// Token: 0x06004CCC RID: 19660 RVA: 0x001B0770 File Offset: 0x001AE970
	private void Start()
	{
		this.inputField.minValue = 0f;
		this.inputField.maxValue = (float)DisinfectThresholdDiagram.MAX_VALUE;
		this.inputField.currentValue = (float)SaveGame.Instance.minGermCountForDisinfect;
		this.inputField.SetDisplayValue(SaveGame.Instance.minGermCountForDisinfect.ToString());
		this.inputField.onEndEdit += delegate
		{
			this.ReceiveValueFromInput(this.inputField.currentValue);
		};
		this.inputField.decimalPlaces = 1;
		this.inputField.Activate();
		this.slider.minValue = 0f;
		this.slider.maxValue = (float)(DisinfectThresholdDiagram.MAX_VALUE / DisinfectThresholdDiagram.SLIDER_CONVERSION);
		this.slider.wholeNumbers = true;
		this.slider.value = (float)(SaveGame.Instance.minGermCountForDisinfect / DisinfectThresholdDiagram.SLIDER_CONVERSION);
		this.slider.onReleaseHandle += this.OnReleaseHandle;
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
			this.OnReleaseHandle();
		};
		this.unitsLabel.SetText(UI.OVERLAYS.DISEASE.DISINFECT_THRESHOLD_DIAGRAM.UNITS);
		this.minLabel.SetText(UI.OVERLAYS.DISEASE.DISINFECT_THRESHOLD_DIAGRAM.MIN_LABEL);
		this.maxLabel.SetText(UI.OVERLAYS.DISEASE.DISINFECT_THRESHOLD_DIAGRAM.MAX_LABEL);
		this.thresholdPrefix.SetText(UI.OVERLAYS.DISEASE.DISINFECT_THRESHOLD_DIAGRAM.THRESHOLD_PREFIX);
		this.toolTip.OnToolTip = delegate
		{
			this.toolTip.ClearMultiStringTooltip();
			if (SaveGame.Instance.enableAutoDisinfect)
			{
				this.toolTip.AddMultiStringTooltip(UI.OVERLAYS.DISEASE.DISINFECT_THRESHOLD_DIAGRAM.TOOLTIP.ToString().Replace("{NumberOfGerms}", SaveGame.Instance.minGermCountForDisinfect.ToString()), null);
			}
			else
			{
				this.toolTip.AddMultiStringTooltip(UI.OVERLAYS.DISEASE.DISINFECT_THRESHOLD_DIAGRAM.TOOLTIP_DISABLED.ToString(), null);
			}
			return "";
		};
		this.disabledImage.gameObject.SetActive(!SaveGame.Instance.enableAutoDisinfect);
		this.toggle.isOn = SaveGame.Instance.enableAutoDisinfect;
		this.toggle.onValueChanged += this.OnClickToggle;
	}

	// Token: 0x06004CCD RID: 19661 RVA: 0x001B095C File Offset: 0x001AEB5C
	private void OnReleaseHandle()
	{
		float num = (float)((int)this.slider.value * DisinfectThresholdDiagram.SLIDER_CONVERSION);
		SaveGame.Instance.minGermCountForDisinfect = (int)num;
		this.inputField.SetDisplayValue(num.ToString());
	}

	// Token: 0x06004CCE RID: 19662 RVA: 0x001B099C File Offset: 0x001AEB9C
	private void ReceiveValueFromSlider(float new_value)
	{
		SaveGame.Instance.minGermCountForDisinfect = (int)new_value * DisinfectThresholdDiagram.SLIDER_CONVERSION;
		this.inputField.SetDisplayValue((new_value * (float)DisinfectThresholdDiagram.SLIDER_CONVERSION).ToString());
	}

	// Token: 0x06004CCF RID: 19663 RVA: 0x001B09D6 File Offset: 0x001AEBD6
	private void ReceiveValueFromInput(float new_value)
	{
		this.slider.value = new_value / (float)DisinfectThresholdDiagram.SLIDER_CONVERSION;
		SaveGame.Instance.minGermCountForDisinfect = (int)new_value;
	}

	// Token: 0x06004CD0 RID: 19664 RVA: 0x001B09F7 File Offset: 0x001AEBF7
	private void OnClickToggle(bool new_value)
	{
		SaveGame.Instance.enableAutoDisinfect = new_value;
		this.disabledImage.gameObject.SetActive(!SaveGame.Instance.enableAutoDisinfect);
	}

	// Token: 0x0400329B RID: 12955
	[SerializeField]
	private KNumberInputField inputField;

	// Token: 0x0400329C RID: 12956
	[SerializeField]
	private KSlider slider;

	// Token: 0x0400329D RID: 12957
	[SerializeField]
	private LocText minLabel;

	// Token: 0x0400329E RID: 12958
	[SerializeField]
	private LocText maxLabel;

	// Token: 0x0400329F RID: 12959
	[SerializeField]
	private LocText unitsLabel;

	// Token: 0x040032A0 RID: 12960
	[SerializeField]
	private LocText thresholdPrefix;

	// Token: 0x040032A1 RID: 12961
	[SerializeField]
	private ToolTip toolTip;

	// Token: 0x040032A2 RID: 12962
	[SerializeField]
	private KToggle toggle;

	// Token: 0x040032A3 RID: 12963
	[SerializeField]
	private Image disabledImage;

	// Token: 0x040032A4 RID: 12964
	private static int MAX_VALUE = 1000000;

	// Token: 0x040032A5 RID: 12965
	private static int SLIDER_CONVERSION = 1000;
}
