using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020005E2 RID: 1506
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicClusterLocationSensor : Switch, ISaveLoadable, ISim200ms
{
	// Token: 0x1700020A RID: 522
	// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000CC685 File Offset: 0x000CA885
	public bool ActiveInSpace
	{
		get
		{
			return this.activeInSpace;
		}
	}

	// Token: 0x060025C8 RID: 9672 RVA: 0x000CC68D File Offset: 0x000CA88D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicClusterLocationSensor>(-905833192, LogicClusterLocationSensor.OnCopySettingsDelegate);
	}

	// Token: 0x060025C9 RID: 9673 RVA: 0x000CC6A8 File Offset: 0x000CA8A8
	private void OnCopySettings(object data)
	{
		LogicClusterLocationSensor component = ((GameObject)data).GetComponent<LogicClusterLocationSensor>();
		if (component != null)
		{
			this.activeLocations.Clear();
			for (int i = 0; i < component.activeLocations.Count; i++)
			{
				this.SetLocationEnabled(component.activeLocations[i], true);
			}
			this.activeInSpace = component.activeInSpace;
		}
	}

	// Token: 0x060025CA RID: 9674 RVA: 0x000CC70A File Offset: 0x000CA90A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
	}

	// Token: 0x060025CB RID: 9675 RVA: 0x000CC73D File Offset: 0x000CA93D
	public void SetLocationEnabled(AxialI location, bool setting)
	{
		if (!setting)
		{
			this.activeLocations.Remove(location);
			return;
		}
		if (!this.activeLocations.Contains(location))
		{
			this.activeLocations.Add(location);
		}
	}

	// Token: 0x060025CC RID: 9676 RVA: 0x000CC76A File Offset: 0x000CA96A
	public void SetSpaceEnabled(bool setting)
	{
		this.activeInSpace = setting;
	}

	// Token: 0x060025CD RID: 9677 RVA: 0x000CC774 File Offset: 0x000CA974
	public void Sim200ms(float dt)
	{
		bool flag = this.CheckCurrentLocationSelected();
		this.SetState(flag);
	}

	// Token: 0x060025CE RID: 9678 RVA: 0x000CC790 File Offset: 0x000CA990
	private bool CheckCurrentLocationSelected()
	{
		AxialI myWorldLocation = base.gameObject.GetMyWorldLocation();
		return this.activeLocations.Contains(myWorldLocation) || (this.activeInSpace && this.CheckInEmptySpace());
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x000CC7CC File Offset: 0x000CA9CC
	private bool CheckInEmptySpace()
	{
		bool flag = true;
		AxialI myWorldLocation = base.gameObject.GetMyWorldLocation();
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			if (!worldContainer.IsModuleInterior && worldContainer.GetMyWorldLocation() == myWorldLocation)
			{
				flag = false;
				break;
			}
		}
		return flag;
	}

	// Token: 0x060025D0 RID: 9680 RVA: 0x000CC840 File Offset: 0x000CAA40
	public bool CheckLocationSelected(AxialI location)
	{
		return this.activeLocations.Contains(location);
	}

	// Token: 0x060025D1 RID: 9681 RVA: 0x000CC84E File Offset: 0x000CAA4E
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x060025D2 RID: 9682 RVA: 0x000CC85D File Offset: 0x000CAA5D
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x060025D3 RID: 9683 RVA: 0x000CC87C File Offset: 0x000CAA7C
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			AxialI myWorldLocation = base.gameObject.GetMyWorldLocation();
			bool flag = true;
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				if (!worldContainer.IsModuleInterior && worldContainer.GetMyWorldLocation() == myWorldLocation)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				component.Play(this.switchedOn ? "on_space_pre" : "on_space_pst", KAnim.PlayMode.Once, 1f, 0f);
				component.Queue(this.switchedOn ? "on_space" : "off_space", KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			component.Play(this.switchedOn ? "on_asteroid_pre" : "on_asteroid_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on_asteroid" : "off_asteroid", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x060025D4 RID: 9684 RVA: 0x000CC9C8 File Offset: 0x000CABC8
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x04001615 RID: 5653
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001616 RID: 5654
	[Serialize]
	private List<AxialI> activeLocations = new List<AxialI>();

	// Token: 0x04001617 RID: 5655
	[Serialize]
	private bool activeInSpace = true;

	// Token: 0x04001618 RID: 5656
	private bool wasOn;

	// Token: 0x04001619 RID: 5657
	private static readonly EventSystem.IntraObjectHandler<LogicClusterLocationSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicClusterLocationSensor>(delegate(LogicClusterLocationSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
