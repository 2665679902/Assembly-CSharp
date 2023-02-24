using System;
using System.Diagnostics;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200057A RID: 1402
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{name}")]
public class BatterySmart : Battery, IActivationRangeTarget
{
	// Token: 0x0600220D RID: 8717 RVA: 0x000B908F File Offset: 0x000B728F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<BatterySmart>(-905833192, BatterySmart.OnCopySettingsDelegate);
	}

	// Token: 0x0600220E RID: 8718 RVA: 0x000B90A8 File Offset: 0x000B72A8
	private void OnCopySettings(object data)
	{
		BatterySmart component = ((GameObject)data).GetComponent<BatterySmart>();
		if (component != null)
		{
			this.ActivateValue = component.ActivateValue;
			this.DeactivateValue = component.DeactivateValue;
		}
	}

	// Token: 0x0600220F RID: 8719 RVA: 0x000B90E2 File Offset: 0x000B72E2
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.CreateLogicMeter();
		base.Subscribe<BatterySmart>(-801688580, BatterySmart.OnLogicValueChangedDelegate);
		base.Subscribe<BatterySmart>(-592767678, BatterySmart.UpdateLogicCircuitDelegate);
	}

	// Token: 0x06002210 RID: 8720 RVA: 0x000B9112 File Offset: 0x000B7312
	private void CreateLogicMeter()
	{
		this.logicMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "logicmeter_target", "logicmeter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
	}

	// Token: 0x06002211 RID: 8721 RVA: 0x000B9137 File Offset: 0x000B7337
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		this.UpdateLogicCircuit(null);
	}

	// Token: 0x06002212 RID: 8722 RVA: 0x000B9148 File Offset: 0x000B7348
	private void UpdateLogicCircuit(object data)
	{
		float num = (float)Mathf.RoundToInt(base.PercentFull * 100f);
		if (this.activated)
		{
			if (num >= (float)this.deactivateValue)
			{
				this.activated = false;
			}
		}
		else if (num <= (float)this.activateValue)
		{
			this.activated = true;
		}
		bool isOperational = this.operational.IsOperational;
		bool flag = this.activated && isOperational;
		this.logicPorts.SendSignal(BatterySmart.PORT_ID, flag ? 1 : 0);
	}

	// Token: 0x06002213 RID: 8723 RVA: 0x000B91C0 File Offset: 0x000B73C0
	private void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == BatterySmart.PORT_ID)
		{
			this.SetLogicMeter(LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue));
		}
	}

	// Token: 0x06002214 RID: 8724 RVA: 0x000B91F8 File Offset: 0x000B73F8
	public void SetLogicMeter(bool on)
	{
		if (this.logicMeter != null)
		{
			this.logicMeter.SetPositionPercent(on ? 1f : 0f);
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x06002215 RID: 8725 RVA: 0x000B921C File Offset: 0x000B741C
	// (set) Token: 0x06002216 RID: 8726 RVA: 0x000B9225 File Offset: 0x000B7425
	public float ActivateValue
	{
		get
		{
			return (float)this.deactivateValue;
		}
		set
		{
			this.deactivateValue = (int)value;
			this.UpdateLogicCircuit(null);
		}
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x06002217 RID: 8727 RVA: 0x000B9236 File Offset: 0x000B7436
	// (set) Token: 0x06002218 RID: 8728 RVA: 0x000B923F File Offset: 0x000B743F
	public float DeactivateValue
	{
		get
		{
			return (float)this.activateValue;
		}
		set
		{
			this.activateValue = (int)value;
			this.UpdateLogicCircuit(null);
		}
	}

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x06002219 RID: 8729 RVA: 0x000B9250 File Offset: 0x000B7450
	public float MinValue
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x0600221A RID: 8730 RVA: 0x000B9257 File Offset: 0x000B7457
	public float MaxValue
	{
		get
		{
			return 100f;
		}
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x0600221B RID: 8731 RVA: 0x000B925E File Offset: 0x000B745E
	public bool UseWholeNumbers
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x0600221C RID: 8732 RVA: 0x000B9261 File Offset: 0x000B7461
	public string ActivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.BATTERYSMART.DEACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x0600221D RID: 8733 RVA: 0x000B926D File Offset: 0x000B746D
	public string DeactivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.BATTERYSMART.ACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x0600221E RID: 8734 RVA: 0x000B9279 File Offset: 0x000B7479
	public string ActivationRangeTitleText
	{
		get
		{
			return BUILDINGS.PREFABS.BATTERYSMART.SIDESCREEN_TITLE;
		}
	}

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x0600221F RID: 8735 RVA: 0x000B9285 File Offset: 0x000B7485
	public string ActivateSliderLabelText
	{
		get
		{
			return BUILDINGS.PREFABS.BATTERYSMART.SIDESCREEN_DEACTIVATE;
		}
	}

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x06002220 RID: 8736 RVA: 0x000B9291 File Offset: 0x000B7491
	public string DeactivateSliderLabelText
	{
		get
		{
			return BUILDINGS.PREFABS.BATTERYSMART.SIDESCREEN_ACTIVATE;
		}
	}

	// Token: 0x040013AD RID: 5037
	public static readonly HashedString PORT_ID = "BatterySmartLogicPort";

	// Token: 0x040013AE RID: 5038
	[Serialize]
	private int activateValue;

	// Token: 0x040013AF RID: 5039
	[Serialize]
	private int deactivateValue = 100;

	// Token: 0x040013B0 RID: 5040
	[Serialize]
	private bool activated;

	// Token: 0x040013B1 RID: 5041
	[MyCmpGet]
	private LogicPorts logicPorts;

	// Token: 0x040013B2 RID: 5042
	private MeterController logicMeter;

	// Token: 0x040013B3 RID: 5043
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x040013B4 RID: 5044
	private static readonly EventSystem.IntraObjectHandler<BatterySmart> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<BatterySmart>(delegate(BatterySmart component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x040013B5 RID: 5045
	private static readonly EventSystem.IntraObjectHandler<BatterySmart> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<BatterySmart>(delegate(BatterySmart component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x040013B6 RID: 5046
	private static readonly EventSystem.IntraObjectHandler<BatterySmart> UpdateLogicCircuitDelegate = new EventSystem.IntraObjectHandler<BatterySmart>(delegate(BatterySmart component, object data)
	{
		component.UpdateLogicCircuit(data);
	});
}
