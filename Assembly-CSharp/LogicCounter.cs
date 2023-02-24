using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005E3 RID: 1507
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicCounter : Switch, ISaveLoadable
{
	// Token: 0x060025D7 RID: 9687 RVA: 0x000CCA51 File Offset: 0x000CAC51
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicCounter>(-905833192, LogicCounter.OnCopySettingsDelegate);
	}

	// Token: 0x060025D8 RID: 9688 RVA: 0x000CCA6C File Offset: 0x000CAC6C
	private void OnCopySettings(object data)
	{
		LogicCounter component = ((GameObject)data).GetComponent<LogicCounter>();
		if (component != null)
		{
			this.maxCount = component.maxCount;
			this.resetCountAtMax = component.resetCountAtMax;
			this.advancedMode = component.advancedMode;
		}
	}

	// Token: 0x060025D9 RID: 9689 RVA: 0x000CCAB4 File Offset: 0x000CACB4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (System.Action)Delegate.Combine(logicCircuitManager.onLogicTick, new System.Action(this.LogicTick));
		if (this.maxCount == 0)
		{
			this.maxCount = 10;
		}
		base.Subscribe<LogicCounter>(-801688580, LogicCounter.OnLogicValueChangedDelegate);
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.meter = new MeterController(component, "meter_target", component.FlipY ? "meter_dn" : "meter_up", Meter.Offset.UserSpecified, Grid.SceneLayer.LogicGatesFront, Vector3.zero, null);
		this.UpdateMeter();
	}

	// Token: 0x060025DA RID: 9690 RVA: 0x000CCB79 File Offset: 0x000CAD79
	protected override void OnCleanUp()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (System.Action)Delegate.Remove(logicCircuitManager.onLogicTick, new System.Action(this.LogicTick));
	}

	// Token: 0x060025DB RID: 9691 RVA: 0x000CCBA6 File Offset: 0x000CADA6
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x060025DC RID: 9692 RVA: 0x000CCBB5 File Offset: 0x000CADB5
	public void UpdateLogicCircuit()
	{
		if (this.receivedFirstSignal)
		{
			base.GetComponent<LogicPorts>().SendSignal(LogicCounter.OUTPUT_PORT_ID, this.switchedOn ? 1 : 0);
		}
	}

	// Token: 0x060025DD RID: 9693 RVA: 0x000CCBDC File Offset: 0x000CADDC
	public void UpdateMeter()
	{
		float num = (float)(this.advancedMode ? (this.currentCount % this.maxCount) : this.currentCount);
		if (num == 10f)
		{
			num = 0f;
		}
		this.meter.SetPositionPercent(num / 10f);
	}

	// Token: 0x060025DE RID: 9694 RVA: 0x000CCC28 File Offset: 0x000CAE28
	public void UpdateVisualState(bool force = false)
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (!this.receivedFirstSignal)
		{
			component.Play("off", KAnim.PlayMode.Once, 1f, 0f);
			return;
		}
		if (this.wasOn != this.switchedOn || force)
		{
			int num = (this.switchedOn ? 4 : 0) + (this.wasResetting ? 2 : 0) + (this.wasIncrementing ? 1 : 0);
			this.wasOn = this.switchedOn;
			component.Play("on_" + num.ToString(), KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x060025DF RID: 9695 RVA: 0x000CCCD0 File Offset: 0x000CAED0
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == LogicCounter.INPUT_PORT_ID)
		{
			int newValue = logicValueChanged.newValue;
			this.receivedFirstSignal = true;
			if (LogicCircuitNetwork.IsBitActive(0, newValue))
			{
				if (!this.wasIncrementing)
				{
					this.wasIncrementing = true;
					if (!this.wasResetting)
					{
						if (this.currentCount == this.maxCount || this.currentCount >= 10)
						{
							this.currentCount = 0;
						}
						this.currentCount++;
						this.UpdateMeter();
						this.SetCounterState();
						if (this.currentCount == this.maxCount && this.resetCountAtMax)
						{
							this.resetRequested = true;
						}
					}
				}
			}
			else
			{
				this.wasIncrementing = false;
			}
		}
		else
		{
			if (!(logicValueChanged.portID == LogicCounter.RESET_PORT_ID))
			{
				return;
			}
			int newValue2 = logicValueChanged.newValue;
			this.receivedFirstSignal = true;
			if (LogicCircuitNetwork.IsBitActive(0, newValue2))
			{
				if (!this.wasResetting)
				{
					this.wasResetting = true;
					this.ResetCounter();
				}
			}
			else
			{
				this.wasResetting = false;
			}
		}
		this.UpdateVisualState(true);
		this.UpdateLogicCircuit();
	}

	// Token: 0x060025E0 RID: 9696 RVA: 0x000CCDE8 File Offset: 0x000CAFE8
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x060025E1 RID: 9697 RVA: 0x000CCE3B File Offset: 0x000CB03B
	public void ResetCounter()
	{
		this.resetRequested = false;
		this.currentCount = 0;
		this.SetCounterState();
		this.UpdateVisualState(true);
		this.UpdateMeter();
		this.UpdateLogicCircuit();
	}

	// Token: 0x060025E2 RID: 9698 RVA: 0x000CCE64 File Offset: 0x000CB064
	public void LogicTick()
	{
		if (this.resetRequested)
		{
			this.ResetCounter();
		}
		if (this.pulsingActive)
		{
			this.pulseTicksRemaining--;
			if (this.pulseTicksRemaining <= 0)
			{
				this.pulsingActive = false;
				this.SetState(false);
				this.UpdateVisualState(false);
				this.UpdateMeter();
				this.UpdateLogicCircuit();
			}
		}
	}

	// Token: 0x060025E3 RID: 9699 RVA: 0x000CCEC0 File Offset: 0x000CB0C0
	public void SetCounterState()
	{
		this.SetState(this.advancedMode ? (this.currentCount % this.maxCount == 0) : (this.currentCount == this.maxCount));
		if (this.advancedMode && this.currentCount % this.maxCount == 0)
		{
			this.pulsingActive = true;
			this.pulseTicksRemaining = 2;
		}
	}

	// Token: 0x0400161A RID: 5658
	[Serialize]
	public int maxCount;

	// Token: 0x0400161B RID: 5659
	[Serialize]
	public int currentCount;

	// Token: 0x0400161C RID: 5660
	[Serialize]
	public bool resetCountAtMax;

	// Token: 0x0400161D RID: 5661
	[Serialize]
	public bool advancedMode;

	// Token: 0x0400161E RID: 5662
	private bool wasOn;

	// Token: 0x0400161F RID: 5663
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001620 RID: 5664
	private static readonly EventSystem.IntraObjectHandler<LogicCounter> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicCounter>(delegate(LogicCounter component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001621 RID: 5665
	private static readonly EventSystem.IntraObjectHandler<LogicCounter> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicCounter>(delegate(LogicCounter component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001622 RID: 5666
	public static readonly HashedString INPUT_PORT_ID = new HashedString("LogicCounterInput");

	// Token: 0x04001623 RID: 5667
	public static readonly HashedString RESET_PORT_ID = new HashedString("LogicCounterReset");

	// Token: 0x04001624 RID: 5668
	public static readonly HashedString OUTPUT_PORT_ID = new HashedString("LogicCounterOutput");

	// Token: 0x04001625 RID: 5669
	private bool resetRequested;

	// Token: 0x04001626 RID: 5670
	[Serialize]
	private bool wasResetting;

	// Token: 0x04001627 RID: 5671
	[Serialize]
	private bool wasIncrementing;

	// Token: 0x04001628 RID: 5672
	[Serialize]
	public bool receivedFirstSignal;

	// Token: 0x04001629 RID: 5673
	private bool pulsingActive;

	// Token: 0x0400162A RID: 5674
	private const int pulseLength = 1;

	// Token: 0x0400162B RID: 5675
	private int pulseTicksRemaining;

	// Token: 0x0400162C RID: 5676
	private MeterController meter;
}
