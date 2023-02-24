using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005EC RID: 1516
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicGateBuffer : LogicGate, ISingleSliderControl, ISliderControl
{
	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06002681 RID: 9857 RVA: 0x000D0857 File Offset: 0x000CEA57
	// (set) Token: 0x06002682 RID: 9858 RVA: 0x000D0860 File Offset: 0x000CEA60
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

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06002683 RID: 9859 RVA: 0x000D088B File Offset: 0x000CEA8B
	private int DelayAmountTicks
	{
		get
		{
			return Mathf.RoundToInt(this.delayAmount / LogicCircuitManager.ClockTickInterval);
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06002684 RID: 9860 RVA: 0x000D089E File Offset: 0x000CEA9E
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.LOGIC_BUFFER_SIDE_SCREEN.TITLE";
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06002685 RID: 9861 RVA: 0x000D08A5 File Offset: 0x000CEAA5
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.SECOND;
		}
	}

	// Token: 0x06002686 RID: 9862 RVA: 0x000D08B1 File Offset: 0x000CEAB1
	public int SliderDecimalPlaces(int index)
	{
		return 1;
	}

	// Token: 0x06002687 RID: 9863 RVA: 0x000D08B4 File Offset: 0x000CEAB4
	public float GetSliderMin(int index)
	{
		return 0.1f;
	}

	// Token: 0x06002688 RID: 9864 RVA: 0x000D08BB File Offset: 0x000CEABB
	public float GetSliderMax(int index)
	{
		return 200f;
	}

	// Token: 0x06002689 RID: 9865 RVA: 0x000D08C2 File Offset: 0x000CEAC2
	public float GetSliderValue(int index)
	{
		return this.DelayAmount;
	}

	// Token: 0x0600268A RID: 9866 RVA: 0x000D08CA File Offset: 0x000CEACA
	public void SetSliderValue(float value, int index)
	{
		this.DelayAmount = value;
	}

	// Token: 0x0600268B RID: 9867 RVA: 0x000D08D3 File Offset: 0x000CEAD3
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.LOGIC_BUFFER_SIDE_SCREEN.TOOLTIP";
	}

	// Token: 0x0600268C RID: 9868 RVA: 0x000D08DA File Offset: 0x000CEADA
	string ISliderControl.GetSliderTooltip()
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.LOGIC_BUFFER_SIDE_SCREEN.TOOLTIP"), this.DelayAmount);
	}

	// Token: 0x0600268D RID: 9869 RVA: 0x000D08FB File Offset: 0x000CEAFB
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicGateBuffer>(-905833192, LogicGateBuffer.OnCopySettingsDelegate);
	}

	// Token: 0x0600268E RID: 9870 RVA: 0x000D0914 File Offset: 0x000CEB14
	private void OnCopySettings(object data)
	{
		LogicGateBuffer component = ((GameObject)data).GetComponent<LogicGateBuffer>();
		if (component != null)
		{
			this.DelayAmount = component.DelayAmount;
		}
	}

	// Token: 0x0600268F RID: 9871 RVA: 0x000D0944 File Offset: 0x000CEB44
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.meter = new MeterController(component, "meter_target", "meter", Meter.Offset.UserSpecified, Grid.SceneLayer.LogicGatesFront, Vector3.zero, null);
		this.meter.SetPositionPercent(1f);
	}

	// Token: 0x06002690 RID: 9872 RVA: 0x000D0990 File Offset: 0x000CEB90
	private void Update()
	{
		float num;
		if (this.input_was_previously_positive)
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

	// Token: 0x06002691 RID: 9873 RVA: 0x000D09E7 File Offset: 0x000CEBE7
	public override void LogicTick()
	{
		if (!this.input_was_previously_positive && this.delayTicksRemaining > 0)
		{
			this.delayTicksRemaining--;
			if (this.delayTicksRemaining <= 0)
			{
				this.OnDelay();
			}
		}
	}

	// Token: 0x06002692 RID: 9874 RVA: 0x000D0A18 File Offset: 0x000CEC18
	protected override int GetCustomValue(int val1, int val2)
	{
		if (val1 != 0)
		{
			this.input_was_previously_positive = true;
			this.delayTicksRemaining = 0;
			this.meter.SetPositionPercent(0f);
		}
		else if (this.delayTicksRemaining <= 0)
		{
			if (this.input_was_previously_positive)
			{
				this.delayTicksRemaining = this.DelayAmountTicks;
			}
			this.input_was_previously_positive = false;
		}
		if (val1 == 0 && this.delayTicksRemaining <= 0)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x06002693 RID: 9875 RVA: 0x000D0A7C File Offset: 0x000CEC7C
	private void OnDelay()
	{
		if (this.cleaningUp)
		{
			return;
		}
		this.delayTicksRemaining = 0;
		this.meter.SetPositionPercent(1f);
		if (this.outputValueOne == 0)
		{
			return;
		}
		int outputCellOne = base.OutputCellOne;
		if (!(Game.Instance.logicCircuitSystem.GetNetworkForCell(outputCellOne) is LogicCircuitNetwork))
		{
			return;
		}
		this.outputValueOne = 0;
		base.RefreshAnimation();
	}

	// Token: 0x040016CC RID: 5836
	[Serialize]
	private bool input_was_previously_positive;

	// Token: 0x040016CD RID: 5837
	[Serialize]
	private float delayAmount = 5f;

	// Token: 0x040016CE RID: 5838
	[Serialize]
	private int delayTicksRemaining;

	// Token: 0x040016CF RID: 5839
	private MeterController meter;

	// Token: 0x040016D0 RID: 5840
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x040016D1 RID: 5841
	private static readonly EventSystem.IntraObjectHandler<LogicGateBuffer> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicGateBuffer>(delegate(LogicGateBuffer component, object data)
	{
		component.OnCopySettings(data);
	});
}
