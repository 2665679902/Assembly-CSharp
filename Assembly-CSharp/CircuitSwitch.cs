using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000585 RID: 1413
[SerializationConfig(MemberSerialization.OptIn)]
public class CircuitSwitch : Switch, IPlayerControlledToggle, ISim33ms
{
	// Token: 0x0600226C RID: 8812 RVA: 0x000BA998 File Offset: 0x000B8B98
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<CircuitSwitch>(-905833192, CircuitSwitch.OnCopySettingsDelegate);
	}

	// Token: 0x0600226D RID: 8813 RVA: 0x000BA9B4 File Offset: 0x000B8BB4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.CircuitOnToggle;
		int num = Grid.PosToCell(base.transform.GetPosition());
		GameObject gameObject = Grid.Objects[num, (int)this.objectLayer];
		Wire wire = ((gameObject != null) ? gameObject.GetComponent<Wire>() : null);
		if (wire == null)
		{
			this.wireConnectedGUID = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoWireConnected, null);
		}
		this.AttachWire(wire);
		this.wasOn = this.switchedOn;
		this.UpdateCircuit(true);
		base.GetComponent<KBatchedAnimController>().Play(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x0600226E RID: 8814 RVA: 0x000BAA84 File Offset: 0x000B8C84
	protected override void OnCleanUp()
	{
		if (this.attachedWire != null)
		{
			this.UnsubscribeFromWire(this.attachedWire);
		}
		bool switchedOn = this.switchedOn;
		this.switchedOn = true;
		this.UpdateCircuit(false);
		this.switchedOn = switchedOn;
	}

	// Token: 0x0600226F RID: 8815 RVA: 0x000BAAC8 File Offset: 0x000B8CC8
	private void OnCopySettings(object data)
	{
		CircuitSwitch component = ((GameObject)data).GetComponent<CircuitSwitch>();
		if (component != null)
		{
			this.switchedOn = component.switchedOn;
			this.UpdateCircuit(true);
		}
	}

	// Token: 0x06002270 RID: 8816 RVA: 0x000BAB00 File Offset: 0x000B8D00
	public bool IsConnected()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		GameObject gameObject = Grid.Objects[num, (int)this.objectLayer];
		return gameObject != null && gameObject.GetComponent<IDisconnectable>() != null;
	}

	// Token: 0x06002271 RID: 8817 RVA: 0x000BAB44 File Offset: 0x000B8D44
	private void CircuitOnToggle(bool on)
	{
		this.UpdateCircuit(true);
	}

	// Token: 0x06002272 RID: 8818 RVA: 0x000BAB50 File Offset: 0x000B8D50
	public void AttachWire(Wire wire)
	{
		if (wire == this.attachedWire)
		{
			return;
		}
		if (this.attachedWire != null)
		{
			this.UnsubscribeFromWire(this.attachedWire);
		}
		this.attachedWire = wire;
		if (this.attachedWire != null)
		{
			this.SubscribeToWire(this.attachedWire);
			this.UpdateCircuit(true);
			this.wireConnectedGUID = base.GetComponent<KSelectable>().RemoveStatusItem(this.wireConnectedGUID, false);
			return;
		}
		if (this.wireConnectedGUID == Guid.Empty)
		{
			this.wireConnectedGUID = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoWireConnected, null);
		}
	}

	// Token: 0x06002273 RID: 8819 RVA: 0x000BABFA File Offset: 0x000B8DFA
	private void OnWireDestroyed(object data)
	{
		if (this.attachedWire != null)
		{
			this.attachedWire.Unsubscribe(1969584890, new Action<object>(this.OnWireDestroyed));
		}
	}

	// Token: 0x06002274 RID: 8820 RVA: 0x000BAC26 File Offset: 0x000B8E26
	private void OnWireStateChanged(object data)
	{
		this.UpdateCircuit(true);
	}

	// Token: 0x06002275 RID: 8821 RVA: 0x000BAC30 File Offset: 0x000B8E30
	private void SubscribeToWire(Wire wire)
	{
		wire.Subscribe(1969584890, new Action<object>(this.OnWireDestroyed));
		wire.Subscribe(-1735440190, new Action<object>(this.OnWireStateChanged));
		wire.Subscribe(774203113, new Action<object>(this.OnWireStateChanged));
	}

	// Token: 0x06002276 RID: 8822 RVA: 0x000BAC88 File Offset: 0x000B8E88
	private void UnsubscribeFromWire(Wire wire)
	{
		wire.Unsubscribe(1969584890, new Action<object>(this.OnWireDestroyed));
		wire.Unsubscribe(-1735440190, new Action<object>(this.OnWireStateChanged));
		wire.Unsubscribe(774203113, new Action<object>(this.OnWireStateChanged));
	}

	// Token: 0x06002277 RID: 8823 RVA: 0x000BACDC File Offset: 0x000B8EDC
	private void UpdateCircuit(bool should_update_anim = true)
	{
		if (this.attachedWire != null)
		{
			if (this.switchedOn)
			{
				this.attachedWire.Connect();
			}
			else
			{
				this.attachedWire.Disconnect();
			}
		}
		if (should_update_anim && this.wasOn != this.switchedOn)
		{
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
			Game.Instance.userMenu.Refresh(base.gameObject);
		}
		this.wasOn = this.switchedOn;
	}

	// Token: 0x06002278 RID: 8824 RVA: 0x000BADA3 File Offset: 0x000B8FA3
	public void Sim33ms(float dt)
	{
		if (this.ToggleRequested)
		{
			this.Toggle();
			this.ToggleRequested = false;
			this.GetSelectable().SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
		}
	}

	// Token: 0x06002279 RID: 8825 RVA: 0x000BADD7 File Offset: 0x000B8FD7
	public void ToggledByPlayer()
	{
		this.Toggle();
	}

	// Token: 0x0600227A RID: 8826 RVA: 0x000BADDF File Offset: 0x000B8FDF
	public bool ToggledOn()
	{
		return this.switchedOn;
	}

	// Token: 0x0600227B RID: 8827 RVA: 0x000BADE7 File Offset: 0x000B8FE7
	public KSelectable GetSelectable()
	{
		return base.GetComponent<KSelectable>();
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x0600227C RID: 8828 RVA: 0x000BADEF File Offset: 0x000B8FEF
	public string SideScreenTitleKey
	{
		get
		{
			return "STRINGS.BUILDINGS.PREFABS.SWITCH.SIDESCREEN_TITLE";
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x0600227D RID: 8829 RVA: 0x000BADF6 File Offset: 0x000B8FF6
	// (set) Token: 0x0600227E RID: 8830 RVA: 0x000BADFE File Offset: 0x000B8FFE
	public bool ToggleRequested { get; set; }

	// Token: 0x040013DE RID: 5086
	[SerializeField]
	public ObjectLayer objectLayer;

	// Token: 0x040013DF RID: 5087
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x040013E0 RID: 5088
	private static readonly EventSystem.IntraObjectHandler<CircuitSwitch> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<CircuitSwitch>(delegate(CircuitSwitch component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x040013E1 RID: 5089
	private Wire attachedWire;

	// Token: 0x040013E2 RID: 5090
	private Guid wireConnectedGUID;

	// Token: 0x040013E3 RID: 5091
	private bool wasOn;
}
