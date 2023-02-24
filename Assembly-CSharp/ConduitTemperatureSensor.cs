using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200059C RID: 1436
[SerializationConfig(MemberSerialization.OptIn)]
public class ConduitTemperatureSensor : ConduitThresholdSensor, IThresholdSwitch
{
	// Token: 0x0600234F RID: 9039 RVA: 0x000BEE10 File Offset: 0x000BD010
	private void GetContentsTemperature(out float temperature, out bool hasMass)
	{
		int num = Grid.PosToCell(this);
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			ConduitFlow.ConduitContents contents = Conduit.GetFlowManager(this.conduitType).GetContents(num);
			temperature = contents.temperature;
			hasMass = contents.mass > 0f;
			return;
		}
		SolidConduitFlow flowManager = SolidConduit.GetFlowManager();
		SolidConduitFlow.ConduitContents contents2 = flowManager.GetContents(num);
		Pickupable pickupable = flowManager.GetPickupable(contents2.pickupableHandle);
		if (pickupable != null && pickupable.PrimaryElement.Mass > 0f)
		{
			temperature = pickupable.PrimaryElement.Temperature;
			hasMass = true;
			return;
		}
		temperature = 0f;
		hasMass = false;
	}

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x06002350 RID: 9040 RVA: 0x000BEEB0 File Offset: 0x000BD0B0
	public override float CurrentValue
	{
		get
		{
			float num;
			bool flag;
			this.GetContentsTemperature(out num, out flag);
			if (flag)
			{
				this.lastValue = num;
			}
			return this.lastValue;
		}
	}

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x06002351 RID: 9041 RVA: 0x000BEED7 File Offset: 0x000BD0D7
	public float RangeMin
	{
		get
		{
			return this.rangeMin;
		}
	}

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x06002352 RID: 9042 RVA: 0x000BEEDF File Offset: 0x000BD0DF
	public float RangeMax
	{
		get
		{
			return this.rangeMax;
		}
	}

	// Token: 0x06002353 RID: 9043 RVA: 0x000BEEE7 File Offset: 0x000BD0E7
	public float GetRangeMinInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMin, false);
	}

	// Token: 0x06002354 RID: 9044 RVA: 0x000BEEF5 File Offset: 0x000BD0F5
	public float GetRangeMaxInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMax, false);
	}

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06002355 RID: 9045 RVA: 0x000BEF03 File Offset: 0x000BD103
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.TEMPERATURESWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06002356 RID: 9046 RVA: 0x000BEF0A File Offset: 0x000BD10A
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE;
		}
	}

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06002357 RID: 9047 RVA: 0x000BEF11 File Offset: 0x000BD111
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06002358 RID: 9048 RVA: 0x000BEF1D File Offset: 0x000BD11D
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06002359 RID: 9049 RVA: 0x000BEF29 File Offset: 0x000BD129
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedTemperature(value, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, units, false);
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x000BEF35 File Offset: 0x000BD135
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x000BEF3D File Offset: 0x000BD13D
	public float ProcessedInputValue(float input)
	{
		return GameUtil.GetTemperatureConvertedToKelvin(input);
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x000BEF48 File Offset: 0x000BD148
	public LocString ThresholdValueUnits()
	{
		LocString locString = null;
		switch (GameUtil.temperatureUnit)
		{
		case GameUtil.TemperatureUnit.Celsius:
			locString = UI.UNITSUFFIXES.TEMPERATURE.CELSIUS;
			break;
		case GameUtil.TemperatureUnit.Fahrenheit:
			locString = UI.UNITSUFFIXES.TEMPERATURE.FAHRENHEIT;
			break;
		case GameUtil.TemperatureUnit.Kelvin:
			locString = UI.UNITSUFFIXES.TEMPERATURE.KELVIN;
			break;
		}
		return locString;
	}

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x0600235D RID: 9053 RVA: 0x000BEF88 File Offset: 0x000BD188
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x0600235E RID: 9054 RVA: 0x000BEF8B File Offset: 0x000BD18B
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x0600235F RID: 9055 RVA: 0x000BEF90 File Offset: 0x000BD190
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return new NonLinearSlider.Range[]
			{
				new NonLinearSlider.Range(25f, 260f),
				new NonLinearSlider.Range(50f, 400f),
				new NonLinearSlider.Range(12f, 1500f),
				new NonLinearSlider.Range(13f, 10000f)
			};
		}
	}

	// Token: 0x0400144B RID: 5195
	public float rangeMin;

	// Token: 0x0400144C RID: 5196
	public float rangeMax = 373.15f;

	// Token: 0x0400144D RID: 5197
	[Serialize]
	private float lastValue;
}
