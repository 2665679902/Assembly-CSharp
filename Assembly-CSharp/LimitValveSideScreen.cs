using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000BC0 RID: 3008
public class LimitValveSideScreen : SideScreenContent
{
	// Token: 0x06005E83 RID: 24195 RVA: 0x002289C4 File Offset: 0x00226BC4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.resetButton.onClick += this.ResetCounter;
		this.limitSlider.onReleaseHandle += this.OnReleaseHandle;
		this.limitSlider.onDrag += delegate
		{
			this.ReceiveValueFromSlider(this.limitSlider.value);
		};
		this.limitSlider.onPointerDown += delegate
		{
			this.ReceiveValueFromSlider(this.limitSlider.value);
		};
		this.limitSlider.onMove += delegate
		{
			this.ReceiveValueFromSlider(this.limitSlider.value);
			this.OnReleaseHandle();
		};
		this.numberInput.onEndEdit += delegate
		{
			this.ReceiveValueFromInput(this.numberInput.currentValue);
		};
		this.numberInput.decimalPlaces = 3;
	}

	// Token: 0x06005E84 RID: 24196 RVA: 0x00228A6D File Offset: 0x00226C6D
	public void OnReleaseHandle()
	{
		this.targetLimitValve.Limit = this.targetLimit;
	}

	// Token: 0x06005E85 RID: 24197 RVA: 0x00228A80 File Offset: 0x00226C80
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LimitValve>() != null;
	}

	// Token: 0x06005E86 RID: 24198 RVA: 0x00228A90 File Offset: 0x00226C90
	public override void SetTarget(GameObject target)
	{
		this.targetLimitValve = target.GetComponent<LimitValve>();
		if (this.targetLimitValve == null)
		{
			global::Debug.LogError("The target object does not have a LimitValve component.");
			return;
		}
		if (this.targetLimitValveSubHandle != -1)
		{
			base.Unsubscribe(this.targetLimitValveSubHandle);
		}
		this.targetLimitValveSubHandle = this.targetLimitValve.Subscribe(-1722241721, new Action<object>(this.UpdateAmountLabel));
		this.limitSlider.minValue = 0f;
		this.limitSlider.maxValue = 100f;
		this.limitSlider.SetRanges(this.targetLimitValve.GetRanges());
		this.limitSlider.value = this.limitSlider.GetPercentageFromValue(this.targetLimitValve.Limit);
		this.numberInput.minValue = 0f;
		this.numberInput.maxValue = this.targetLimitValve.maxLimitKg;
		this.numberInput.Activate();
		if (this.targetLimitValve.displayUnitsInsteadOfMass)
		{
			this.minLimitLabel.text = GameUtil.GetFormattedUnits(0f, GameUtil.TimeSlice.None, true, "");
			this.maxLimitLabel.text = GameUtil.GetFormattedUnits(this.targetLimitValve.maxLimitKg, GameUtil.TimeSlice.None, true, "");
			this.numberInput.SetDisplayValue(GameUtil.GetFormattedUnits(Mathf.Max(0f, this.targetLimitValve.Limit), GameUtil.TimeSlice.None, false, LimitValveSideScreen.FLOAT_FORMAT));
			this.unitsLabel.text = UI.UNITSUFFIXES.UNITS;
			this.toolTip.enabled = true;
			this.toolTip.SetSimpleTooltip(UI.UISIDESCREENS.LIMIT_VALVE_SIDE_SCREEN.SLIDER_TOOLTIP_UNITS);
		}
		else
		{
			this.minLimitLabel.text = GameUtil.GetFormattedMass(0f, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}");
			this.maxLimitLabel.text = GameUtil.GetFormattedMass(this.targetLimitValve.maxLimitKg, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}");
			this.numberInput.SetDisplayValue(GameUtil.GetFormattedMass(Mathf.Max(0f, this.targetLimitValve.Limit), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, false, LimitValveSideScreen.FLOAT_FORMAT));
			this.unitsLabel.text = GameUtil.GetCurrentMassUnit(false);
			this.toolTip.enabled = false;
		}
		this.UpdateAmountLabel(null);
	}

	// Token: 0x06005E87 RID: 24199 RVA: 0x00228CCC File Offset: 0x00226ECC
	private void UpdateAmountLabel(object obj = null)
	{
		if (this.targetLimitValve.displayUnitsInsteadOfMass)
		{
			string formattedUnits = GameUtil.GetFormattedUnits(this.targetLimitValve.Amount, GameUtil.TimeSlice.None, true, LimitValveSideScreen.FLOAT_FORMAT);
			this.amountLabel.text = string.Format(UI.UISIDESCREENS.LIMIT_VALVE_SIDE_SCREEN.AMOUNT, formattedUnits);
			return;
		}
		string formattedMass = GameUtil.GetFormattedMass(this.targetLimitValve.Amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, LimitValveSideScreen.FLOAT_FORMAT);
		this.amountLabel.text = string.Format(UI.UISIDESCREENS.LIMIT_VALVE_SIDE_SCREEN.AMOUNT, formattedMass);
	}

	// Token: 0x06005E88 RID: 24200 RVA: 0x00228D4E File Offset: 0x00226F4E
	private void ResetCounter()
	{
		this.targetLimitValve.ResetAmount();
	}

	// Token: 0x06005E89 RID: 24201 RVA: 0x00228D5C File Offset: 0x00226F5C
	private void ReceiveValueFromSlider(float sliderPercentage)
	{
		float num = this.limitSlider.GetValueForPercentage(sliderPercentage);
		num = (float)Mathf.RoundToInt(num);
		this.UpdateLimitValue(num);
	}

	// Token: 0x06005E8A RID: 24202 RVA: 0x00228D85 File Offset: 0x00226F85
	private void ReceiveValueFromInput(float input)
	{
		this.UpdateLimitValue(input);
		this.targetLimitValve.Limit = this.targetLimit;
	}

	// Token: 0x06005E8B RID: 24203 RVA: 0x00228DA0 File Offset: 0x00226FA0
	private void UpdateLimitValue(float newValue)
	{
		this.targetLimit = newValue;
		this.limitSlider.value = this.limitSlider.GetPercentageFromValue(newValue);
		if (this.targetLimitValve.displayUnitsInsteadOfMass)
		{
			this.numberInput.SetDisplayValue(GameUtil.GetFormattedUnits(newValue, GameUtil.TimeSlice.None, false, LimitValveSideScreen.FLOAT_FORMAT));
			return;
		}
		this.numberInput.SetDisplayValue(GameUtil.GetFormattedMass(newValue, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, false, LimitValveSideScreen.FLOAT_FORMAT));
	}

	// Token: 0x040040AC RID: 16556
	public static readonly string FLOAT_FORMAT = "{0:0.#####}";

	// Token: 0x040040AD RID: 16557
	private LimitValve targetLimitValve;

	// Token: 0x040040AE RID: 16558
	[Header("State")]
	[SerializeField]
	private LocText amountLabel;

	// Token: 0x040040AF RID: 16559
	[SerializeField]
	private KButton resetButton;

	// Token: 0x040040B0 RID: 16560
	[Header("Slider")]
	[SerializeField]
	private NonLinearSlider limitSlider;

	// Token: 0x040040B1 RID: 16561
	[SerializeField]
	private LocText minLimitLabel;

	// Token: 0x040040B2 RID: 16562
	[SerializeField]
	private LocText maxLimitLabel;

	// Token: 0x040040B3 RID: 16563
	[SerializeField]
	private ToolTip toolTip;

	// Token: 0x040040B4 RID: 16564
	[Header("Input Field")]
	[SerializeField]
	private KNumberInputField numberInput;

	// Token: 0x040040B5 RID: 16565
	[SerializeField]
	private LocText unitsLabel;

	// Token: 0x040040B6 RID: 16566
	private float targetLimit;

	// Token: 0x040040B7 RID: 16567
	private int targetLimitValveSubHandle = -1;
}
