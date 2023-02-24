using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005FA RID: 1530
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicTimeOfDaySensor : Switch, ISaveLoadable, ISim200ms
{
	// Token: 0x0600279D RID: 10141 RVA: 0x000D3B2C File Offset: 0x000D1D2C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicTimeOfDaySensor>(-905833192, LogicTimeOfDaySensor.OnCopySettingsDelegate);
	}

	// Token: 0x0600279E RID: 10142 RVA: 0x000D3B48 File Offset: 0x000D1D48
	private void OnCopySettings(object data)
	{
		LogicTimeOfDaySensor component = ((GameObject)data).GetComponent<LogicTimeOfDaySensor>();
		if (component != null)
		{
			this.startTime = component.startTime;
			this.duration = component.duration;
		}
	}

	// Token: 0x0600279F RID: 10143 RVA: 0x000D3B82 File Offset: 0x000D1D82
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
	}

	// Token: 0x060027A0 RID: 10144 RVA: 0x000D3BB8 File Offset: 0x000D1DB8
	public void Sim200ms(float dt)
	{
		float currentCycleAsPercentage = GameClock.Instance.GetCurrentCycleAsPercentage();
		bool flag = false;
		if (currentCycleAsPercentage >= this.startTime && currentCycleAsPercentage < this.startTime + this.duration)
		{
			flag = true;
		}
		if (currentCycleAsPercentage < this.startTime + this.duration - 1f)
		{
			flag = true;
		}
		this.SetState(flag);
	}

	// Token: 0x060027A1 RID: 10145 RVA: 0x000D3C0C File Offset: 0x000D1E0C
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x060027A2 RID: 10146 RVA: 0x000D3C1B File Offset: 0x000D1E1B
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x000D3C3C File Offset: 0x000D1E3C
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x060027A4 RID: 10148 RVA: 0x000D3CC4 File Offset: 0x000D1EC4
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x0400175C RID: 5980
	[SerializeField]
	[Serialize]
	public float startTime;

	// Token: 0x0400175D RID: 5981
	[SerializeField]
	[Serialize]
	public float duration = 1f;

	// Token: 0x0400175E RID: 5982
	private bool wasOn;

	// Token: 0x0400175F RID: 5983
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001760 RID: 5984
	private static readonly EventSystem.IntraObjectHandler<LogicTimeOfDaySensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicTimeOfDaySensor>(delegate(LogicTimeOfDaySensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
