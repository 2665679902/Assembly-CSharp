using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005FB RID: 1531
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicTimerSensor : Switch, ISaveLoadable, ISim33ms
{
	// Token: 0x060027A7 RID: 10151 RVA: 0x000D3D46 File Offset: 0x000D1F46
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicTimerSensor>(-905833192, LogicTimerSensor.OnCopySettingsDelegate);
	}

	// Token: 0x060027A8 RID: 10152 RVA: 0x000D3D60 File Offset: 0x000D1F60
	private void OnCopySettings(object data)
	{
		LogicTimerSensor component = ((GameObject)data).GetComponent<LogicTimerSensor>();
		if (component != null)
		{
			this.onDuration = component.onDuration;
			this.offDuration = component.offDuration;
			this.timeElapsedInCurrentState = component.timeElapsedInCurrentState;
			this.displayCyclesMode = component.displayCyclesMode;
		}
	}

	// Token: 0x060027A9 RID: 10153 RVA: 0x000D3DB2 File Offset: 0x000D1FB2
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
	}

	// Token: 0x060027AA RID: 10154 RVA: 0x000D3DE8 File Offset: 0x000D1FE8
	public void Sim33ms(float dt)
	{
		if (this.onDuration == 0f && this.offDuration == 0f)
		{
			return;
		}
		this.timeElapsedInCurrentState += dt;
		bool flag = base.IsSwitchedOn;
		if (flag)
		{
			if (this.timeElapsedInCurrentState >= this.onDuration)
			{
				flag = false;
				this.timeElapsedInCurrentState -= this.onDuration;
			}
		}
		else if (this.timeElapsedInCurrentState >= this.offDuration)
		{
			flag = true;
			this.timeElapsedInCurrentState -= this.offDuration;
		}
		this.SetState(flag);
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x000D3E77 File Offset: 0x000D2077
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x060027AC RID: 10156 RVA: 0x000D3E86 File Offset: 0x000D2086
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x060027AD RID: 10157 RVA: 0x000D3EA4 File Offset: 0x000D20A4
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

	// Token: 0x060027AE RID: 10158 RVA: 0x000D3F2C File Offset: 0x000D212C
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x060027AF RID: 10159 RVA: 0x000D3F7F File Offset: 0x000D217F
	public void ResetTimer()
	{
		this.SetState(true);
		this.OnSwitchToggled(true);
		this.timeElapsedInCurrentState = 0f;
	}

	// Token: 0x04001761 RID: 5985
	[Serialize]
	public float onDuration = 10f;

	// Token: 0x04001762 RID: 5986
	[Serialize]
	public float offDuration = 10f;

	// Token: 0x04001763 RID: 5987
	[Serialize]
	public bool displayCyclesMode;

	// Token: 0x04001764 RID: 5988
	private bool wasOn;

	// Token: 0x04001765 RID: 5989
	[SerializeField]
	[Serialize]
	public float timeElapsedInCurrentState;

	// Token: 0x04001766 RID: 5990
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001767 RID: 5991
	private static readonly EventSystem.IntraObjectHandler<LogicTimerSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicTimerSensor>(delegate(LogicTimerSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
