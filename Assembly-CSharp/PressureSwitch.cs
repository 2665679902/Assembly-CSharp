using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000629 RID: 1577
[SerializationConfig(MemberSerialization.OptIn)]
public class PressureSwitch : CircuitSwitch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x0600295A RID: 10586 RVA: 0x000DA8D8 File Offset: 0x000D8AD8
	public void Sim200ms(float dt)
	{
		int num = Grid.PosToCell(this);
		if (this.sampleIdx < 8)
		{
			float num2 = (Grid.Element[num].IsState(this.desiredState) ? Grid.Mass[num] : 0f);
			this.samples[this.sampleIdx] = num2;
			this.sampleIdx++;
			return;
		}
		this.sampleIdx = 0;
		float currentValue = this.CurrentValue;
		if (this.activateAboveThreshold)
		{
			if ((currentValue > this.threshold && !base.IsSwitchedOn) || (currentValue <= this.threshold && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((currentValue > this.threshold && base.IsSwitchedOn) || (currentValue <= this.threshold && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x0600295B RID: 10587 RVA: 0x000DA9A0 File Offset: 0x000D8BA0
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x0600295C RID: 10588 RVA: 0x000DA9F3 File Offset: 0x000D8BF3
	// (set) Token: 0x0600295D RID: 10589 RVA: 0x000DA9FB File Offset: 0x000D8BFB
	public float Threshold
	{
		get
		{
			return this.threshold;
		}
		set
		{
			this.threshold = value;
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x0600295E RID: 10590 RVA: 0x000DAA04 File Offset: 0x000D8C04
	// (set) Token: 0x0600295F RID: 10591 RVA: 0x000DAA0C File Offset: 0x000D8C0C
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateAboveThreshold;
		}
		set
		{
			this.activateAboveThreshold = value;
		}
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06002960 RID: 10592 RVA: 0x000DAA18 File Offset: 0x000D8C18
	public float CurrentValue
	{
		get
		{
			float num = 0f;
			for (int i = 0; i < 8; i++)
			{
				num += this.samples[i];
			}
			return num / 8f;
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06002961 RID: 10593 RVA: 0x000DAA49 File Offset: 0x000D8C49
	public float RangeMin
	{
		get
		{
			return this.rangeMin;
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06002962 RID: 10594 RVA: 0x000DAA51 File Offset: 0x000D8C51
	public float RangeMax
	{
		get
		{
			return this.rangeMax;
		}
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x000DAA59 File Offset: 0x000D8C59
	public float GetRangeMinInputField()
	{
		if (this.desiredState != Element.State.Gas)
		{
			return this.rangeMin;
		}
		return this.rangeMin * 1000f;
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x000DAA77 File Offset: 0x000D8C77
	public float GetRangeMaxInputField()
	{
		if (this.desiredState != Element.State.Gas)
		{
			return this.rangeMax;
		}
		return this.rangeMax * 1000f;
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06002965 RID: 10597 RVA: 0x000DAA95 File Offset: 0x000D8C95
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TITLE;
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06002966 RID: 10598 RVA: 0x000DAA9C File Offset: 0x000D8C9C
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE;
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x06002967 RID: 10599 RVA: 0x000DAAA3 File Offset: 0x000D8CA3
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06002968 RID: 10600 RVA: 0x000DAAAF File Offset: 0x000D8CAF
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x000DAABC File Offset: 0x000D8CBC
	public string Format(float value, bool units)
	{
		GameUtil.MetricMassFormat metricMassFormat;
		if (this.desiredState == Element.State.Gas)
		{
			metricMassFormat = GameUtil.MetricMassFormat.Gram;
		}
		else
		{
			metricMassFormat = GameUtil.MetricMassFormat.Kilogram;
		}
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, metricMassFormat, units, "{0:0.#}");
	}

	// Token: 0x0600296A RID: 10602 RVA: 0x000DAAE8 File Offset: 0x000D8CE8
	public float ProcessedSliderValue(float input)
	{
		if (this.desiredState == Element.State.Gas)
		{
			input = Mathf.Round(input * 1000f) / 1000f;
		}
		else
		{
			input = Mathf.Round(input);
		}
		return input;
	}

	// Token: 0x0600296B RID: 10603 RVA: 0x000DAB12 File Offset: 0x000D8D12
	public float ProcessedInputValue(float input)
	{
		if (this.desiredState == Element.State.Gas)
		{
			input /= 1000f;
		}
		return input;
	}

	// Token: 0x0600296C RID: 10604 RVA: 0x000DAB27 File Offset: 0x000D8D27
	public LocString ThresholdValueUnits()
	{
		return GameUtil.GetCurrentMassUnit(this.desiredState == Element.State.Gas);
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x0600296D RID: 10605 RVA: 0x000DAB37 File Offset: 0x000D8D37
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x0600296E RID: 10606 RVA: 0x000DAB3A File Offset: 0x000D8D3A
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x0600296F RID: 10607 RVA: 0x000DAB3D File Offset: 0x000D8D3D
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x0400185E RID: 6238
	[SerializeField]
	[Serialize]
	private float threshold;

	// Token: 0x0400185F RID: 6239
	[SerializeField]
	[Serialize]
	private bool activateAboveThreshold = true;

	// Token: 0x04001860 RID: 6240
	public float rangeMin;

	// Token: 0x04001861 RID: 6241
	public float rangeMax = 1f;

	// Token: 0x04001862 RID: 6242
	public Element.State desiredState = Element.State.Gas;

	// Token: 0x04001863 RID: 6243
	private const int WINDOW_SIZE = 8;

	// Token: 0x04001864 RID: 6244
	private float[] samples = new float[8];

	// Token: 0x04001865 RID: 6245
	private int sampleIdx;
}
