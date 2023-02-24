using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000926 RID: 2342
[AddComponentMenu("KMonoBehaviour/scripts/SmartReservoir")]
public class SmartReservoir : KMonoBehaviour, IActivationRangeTarget, ISim200ms
{
	// Token: 0x170004D3 RID: 1235
	// (get) Token: 0x0600446C RID: 17516 RVA: 0x0018256A File Offset: 0x0018076A
	public float PercentFull
	{
		get
		{
			return this.storage.MassStored() / this.storage.Capacity();
		}
	}

	// Token: 0x0600446D RID: 17517 RVA: 0x00182583 File Offset: 0x00180783
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<SmartReservoir>(-801688580, SmartReservoir.OnLogicValueChangedDelegate);
		base.Subscribe<SmartReservoir>(-592767678, SmartReservoir.UpdateLogicCircuitDelegate);
	}

	// Token: 0x0600446E RID: 17518 RVA: 0x001825AD File Offset: 0x001807AD
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<SmartReservoir>(-905833192, SmartReservoir.OnCopySettingsDelegate);
	}

	// Token: 0x0600446F RID: 17519 RVA: 0x001825C6 File Offset: 0x001807C6
	public void Sim200ms(float dt)
	{
		this.UpdateLogicCircuit(null);
	}

	// Token: 0x06004470 RID: 17520 RVA: 0x001825D0 File Offset: 0x001807D0
	private void UpdateLogicCircuit(object data)
	{
		float num = this.PercentFull * 100f;
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
		bool flag = this.activated;
		this.logicPorts.SendSignal(SmartReservoir.PORT_ID, flag ? 1 : 0);
	}

	// Token: 0x06004471 RID: 17521 RVA: 0x00182634 File Offset: 0x00180834
	private void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == SmartReservoir.PORT_ID)
		{
			this.SetLogicMeter(LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue));
		}
	}

	// Token: 0x06004472 RID: 17522 RVA: 0x0018266C File Offset: 0x0018086C
	private void OnCopySettings(object data)
	{
		SmartReservoir component = ((GameObject)data).GetComponent<SmartReservoir>();
		if (component != null)
		{
			this.ActivateValue = component.ActivateValue;
			this.DeactivateValue = component.DeactivateValue;
		}
	}

	// Token: 0x06004473 RID: 17523 RVA: 0x001826A6 File Offset: 0x001808A6
	public void SetLogicMeter(bool on)
	{
		if (this.logicMeter != null)
		{
			this.logicMeter.SetPositionPercent(on ? 1f : 0f);
		}
	}

	// Token: 0x170004D4 RID: 1236
	// (get) Token: 0x06004474 RID: 17524 RVA: 0x001826CA File Offset: 0x001808CA
	// (set) Token: 0x06004475 RID: 17525 RVA: 0x001826D3 File Offset: 0x001808D3
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

	// Token: 0x170004D5 RID: 1237
	// (get) Token: 0x06004476 RID: 17526 RVA: 0x001826E4 File Offset: 0x001808E4
	// (set) Token: 0x06004477 RID: 17527 RVA: 0x001826ED File Offset: 0x001808ED
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

	// Token: 0x170004D6 RID: 1238
	// (get) Token: 0x06004478 RID: 17528 RVA: 0x001826FE File Offset: 0x001808FE
	public float MinValue
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170004D7 RID: 1239
	// (get) Token: 0x06004479 RID: 17529 RVA: 0x00182705 File Offset: 0x00180905
	public float MaxValue
	{
		get
		{
			return 100f;
		}
	}

	// Token: 0x170004D8 RID: 1240
	// (get) Token: 0x0600447A RID: 17530 RVA: 0x0018270C File Offset: 0x0018090C
	public bool UseWholeNumbers
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170004D9 RID: 1241
	// (get) Token: 0x0600447B RID: 17531 RVA: 0x0018270F File Offset: 0x0018090F
	public string ActivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.SMARTRESERVOIR.DEACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x170004DA RID: 1242
	// (get) Token: 0x0600447C RID: 17532 RVA: 0x0018271B File Offset: 0x0018091B
	public string DeactivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.SMARTRESERVOIR.ACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x170004DB RID: 1243
	// (get) Token: 0x0600447D RID: 17533 RVA: 0x00182727 File Offset: 0x00180927
	public string ActivationRangeTitleText
	{
		get
		{
			return BUILDINGS.PREFABS.SMARTRESERVOIR.SIDESCREEN_TITLE;
		}
	}

	// Token: 0x170004DC RID: 1244
	// (get) Token: 0x0600447E RID: 17534 RVA: 0x00182733 File Offset: 0x00180933
	public string ActivateSliderLabelText
	{
		get
		{
			return BUILDINGS.PREFABS.SMARTRESERVOIR.SIDESCREEN_DEACTIVATE;
		}
	}

	// Token: 0x170004DD RID: 1245
	// (get) Token: 0x0600447F RID: 17535 RVA: 0x0018273F File Offset: 0x0018093F
	public string DeactivateSliderLabelText
	{
		get
		{
			return BUILDINGS.PREFABS.SMARTRESERVOIR.SIDESCREEN_ACTIVATE;
		}
	}

	// Token: 0x04002D9F RID: 11679
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04002DA0 RID: 11680
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002DA1 RID: 11681
	[Serialize]
	private int activateValue;

	// Token: 0x04002DA2 RID: 11682
	[Serialize]
	private int deactivateValue = 100;

	// Token: 0x04002DA3 RID: 11683
	[Serialize]
	private bool activated;

	// Token: 0x04002DA4 RID: 11684
	[MyCmpGet]
	private LogicPorts logicPorts;

	// Token: 0x04002DA5 RID: 11685
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04002DA6 RID: 11686
	private MeterController logicMeter;

	// Token: 0x04002DA7 RID: 11687
	public static readonly HashedString PORT_ID = "SmartReservoirLogicPort";

	// Token: 0x04002DA8 RID: 11688
	private static readonly EventSystem.IntraObjectHandler<SmartReservoir> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<SmartReservoir>(delegate(SmartReservoir component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04002DA9 RID: 11689
	private static readonly EventSystem.IntraObjectHandler<SmartReservoir> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<SmartReservoir>(delegate(SmartReservoir component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04002DAA RID: 11690
	private static readonly EventSystem.IntraObjectHandler<SmartReservoir> UpdateLogicCircuitDelegate = new EventSystem.IntraObjectHandler<SmartReservoir>(delegate(SmartReservoir component, object data)
	{
		component.UpdateLogicCircuit(data);
	});
}
