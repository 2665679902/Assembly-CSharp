using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005ED RID: 1517
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicGateFilter : LogicGate, ISingleSliderControl, ISliderControl
{
	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06002696 RID: 9878 RVA: 0x000D0B0D File Offset: 0x000CED0D
	// (set) Token: 0x06002697 RID: 9879 RVA: 0x000D0B18 File Offset: 0x000CED18
	public float DelayAmount
	{
		get
		{
			return this.delayAmount;
		}
		set
		{
			this.delayAmount = value;
			int delayAmountTicks = this.DelayAmountTicks;
			if (this.delayTicksRemaining > delayAmountTicks)
			{
				this.delayTicksRemaining = delayAmountTicks;
			}
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06002698 RID: 9880 RVA: 0x000D0B43 File Offset: 0x000CED43
	private int DelayAmountTicks
	{
		get
		{
			return Mathf.RoundToInt(this.delayAmount / LogicCircuitManager.ClockTickInterval);
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06002699 RID: 9881 RVA: 0x000D0B56 File Offset: 0x000CED56
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.LOGIC_FILTER_SIDE_SCREEN.TITLE";
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x0600269A RID: 9882 RVA: 0x000D0B5D File Offset: 0x000CED5D
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.SECOND;
		}
	}

	// Token: 0x0600269B RID: 9883 RVA: 0x000D0B69 File Offset: 0x000CED69
	public int SliderDecimalPlaces(int index)
	{
		return 1;
	}

	// Token: 0x0600269C RID: 9884 RVA: 0x000D0B6C File Offset: 0x000CED6C
	public float GetSliderMin(int index)
	{
		return 0.1f;
	}

	// Token: 0x0600269D RID: 9885 RVA: 0x000D0B73 File Offset: 0x000CED73
	public float GetSliderMax(int index)
	{
		return 200f;
	}

	// Token: 0x0600269E RID: 9886 RVA: 0x000D0B7A File Offset: 0x000CED7A
	public float GetSliderValue(int index)
	{
		return this.DelayAmount;
	}

	// Token: 0x0600269F RID: 9887 RVA: 0x000D0B82 File Offset: 0x000CED82
	public void SetSliderValue(float value, int index)
	{
		this.DelayAmount = value;
	}

	// Token: 0x060026A0 RID: 9888 RVA: 0x000D0B8B File Offset: 0x000CED8B
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.LOGIC_FILTER_SIDE_SCREEN.TOOLTIP";
	}

	// Token: 0x060026A1 RID: 9889 RVA: 0x000D0B92 File Offset: 0x000CED92
	string ISliderControl.GetSliderTooltip()
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.LOGIC_FILTER_SIDE_SCREEN.TOOLTIP"), this.DelayAmount);
	}

	// Token: 0x060026A2 RID: 9890 RVA: 0x000D0BB3 File Offset: 0x000CEDB3
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicGateFilter>(-905833192, LogicGateFilter.OnCopySettingsDelegate);
	}

	// Token: 0x060026A3 RID: 9891 RVA: 0x000D0BCC File Offset: 0x000CEDCC
	private void OnCopySettings(object data)
	{
		LogicGateFilter component = ((GameObject)data).GetComponent<LogicGateFilter>();
		if (component != null)
		{
			this.DelayAmount = component.DelayAmount;
		}
	}

	// Token: 0x060026A4 RID: 9892 RVA: 0x000D0BFC File Offset: 0x000CEDFC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.meter = new MeterController(component, "meter_target", "meter", Meter.Offset.UserSpecified, Grid.SceneLayer.LogicGatesFront, Vector3.zero, null);
		this.meter.SetPositionPercent(0f);
	}

	// Token: 0x060026A5 RID: 9893 RVA: 0x000D0C48 File Offset: 0x000CEE48
	private void Update()
	{
		float num;
		if (this.input_was_previously_negative)
		{
			num = 0f;
		}
		else if (this.delayTicksRemaining > 0)
		{
			num = (float)(this.DelayAmountTicks - this.delayTicksRemaining) / (float)this.DelayAmountTicks;
		}
		else
		{
			num = 1f;
		}
		this.meter.SetPositionPercent(num);
	}

	// Token: 0x060026A6 RID: 9894 RVA: 0x000D0C9F File Offset: 0x000CEE9F
	public override void LogicTick()
	{
		if (!this.input_was_previously_negative && this.delayTicksRemaining > 0)
		{
			this.delayTicksRemaining--;
			if (this.delayTicksRemaining <= 0)
			{
				this.OnDelay();
			}
		}
	}

	// Token: 0x060026A7 RID: 9895 RVA: 0x000D0CD0 File Offset: 0x000CEED0
	protected override int GetCustomValue(int val1, int val2)
	{
		if (val1 == 0)
		{
			this.input_was_previously_negative = true;
			this.delayTicksRemaining = 0;
			this.meter.SetPositionPercent(1f);
		}
		else if (this.delayTicksRemaining <= 0)
		{
			if (this.input_was_previously_negative)
			{
				this.delayTicksRemaining = this.DelayAmountTicks;
			}
			this.input_was_previously_negative = false;
		}
		if (val1 != 0 && this.delayTicksRemaining <= 0)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x060026A8 RID: 9896 RVA: 0x000D0D34 File Offset: 0x000CEF34
	private void OnDelay()
	{
		if (this.cleaningUp)
		{
			return;
		}
		this.delayTicksRemaining = 0;
		this.meter.SetPositionPercent(0f);
		if (this.outputValueOne == 1)
		{
			return;
		}
		int outputCellOne = base.OutputCellOne;
		if (!(Game.Instance.logicCircuitSystem.GetNetworkForCell(outputCellOne) is LogicCircuitNetwork))
		{
			return;
		}
		this.outputValueOne = 1;
		base.RefreshAnimation();
	}

	// Token: 0x040016D2 RID: 5842
	[Serialize]
	private bool input_was_previously_negative;

	// Token: 0x040016D3 RID: 5843
	[Serialize]
	private float delayAmount = 5f;

	// Token: 0x040016D4 RID: 5844
	[Serialize]
	private int delayTicksRemaining;

	// Token: 0x040016D5 RID: 5845
	private MeterController meter;

	// Token: 0x040016D6 RID: 5846
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x040016D7 RID: 5847
	private static readonly EventSystem.IntraObjectHandler<LogicGateFilter> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicGateFilter>(delegate(LogicGateFilter component, object data)
	{
		component.OnCopySettings(data);
	});
}
