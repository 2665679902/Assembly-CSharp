using System;
using System.Collections;
using KSerialization;
using UnityEngine;

// Token: 0x020005F8 RID: 1528
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicSwitch : Switch, IPlayerControlledToggle, ISim33ms
{
	// Token: 0x0600276A RID: 10090 RVA: 0x000D340A File Offset: 0x000D160A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicSwitch>(-905833192, LogicSwitch.OnCopySettingsDelegate);
	}

	// Token: 0x0600276B RID: 10091 RVA: 0x000D3424 File Offset: 0x000D1624
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.wasOn = this.switchedOn;
		this.UpdateLogicCircuit();
		base.GetComponent<KBatchedAnimController>().Play(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x0600276C RID: 10092 RVA: 0x000D3478 File Offset: 0x000D1678
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x0600276D RID: 10093 RVA: 0x000D3480 File Offset: 0x000D1680
	private void OnCopySettings(object data)
	{
		LogicSwitch component = ((GameObject)data).GetComponent<LogicSwitch>();
		if (component != null && this.switchedOn != component.switchedOn)
		{
			this.switchedOn = component.switchedOn;
			this.UpdateVisualization();
			this.UpdateLogicCircuit();
		}
	}

	// Token: 0x0600276E RID: 10094 RVA: 0x000D34C8 File Offset: 0x000D16C8
	protected override void Toggle()
	{
		base.Toggle();
		this.UpdateVisualization();
		this.UpdateLogicCircuit();
	}

	// Token: 0x0600276F RID: 10095 RVA: 0x000D34DC File Offset: 0x000D16DC
	private void UpdateVisualization()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (this.wasOn != this.switchedOn)
		{
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
		}
		this.wasOn = this.switchedOn;
	}

	// Token: 0x06002770 RID: 10096 RVA: 0x000D355E File Offset: 0x000D175E
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x06002771 RID: 10097 RVA: 0x000D357C File Offset: 0x000D177C
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSwitchStatusActive : Db.Get().BuildingStatusItems.LogicSwitchStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x06002772 RID: 10098 RVA: 0x000D35CF File Offset: 0x000D17CF
	public void Sim33ms(float dt)
	{
		if (this.ToggleRequested)
		{
			this.Toggle();
			this.ToggleRequested = false;
			this.GetSelectable().SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
		}
	}

	// Token: 0x06002773 RID: 10099 RVA: 0x000D3603 File Offset: 0x000D1803
	public void SetFirstFrameCallback(System.Action ffCb)
	{
		this.firstFrameCallback = ffCb;
		base.StartCoroutine(this.RunCallback());
	}

	// Token: 0x06002774 RID: 10100 RVA: 0x000D3619 File Offset: 0x000D1819
	private IEnumerator RunCallback()
	{
		yield return null;
		if (this.firstFrameCallback != null)
		{
			this.firstFrameCallback();
			this.firstFrameCallback = null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x06002775 RID: 10101 RVA: 0x000D3628 File Offset: 0x000D1828
	public void ToggledByPlayer()
	{
		this.Toggle();
	}

	// Token: 0x06002776 RID: 10102 RVA: 0x000D3630 File Offset: 0x000D1830
	public bool ToggledOn()
	{
		return this.switchedOn;
	}

	// Token: 0x06002777 RID: 10103 RVA: 0x000D3638 File Offset: 0x000D1838
	public KSelectable GetSelectable()
	{
		return base.GetComponent<KSelectable>();
	}

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06002778 RID: 10104 RVA: 0x000D3640 File Offset: 0x000D1840
	public string SideScreenTitleKey
	{
		get
		{
			return "STRINGS.BUILDINGS.PREFABS.LOGICSWITCH.SIDESCREEN_TITLE";
		}
	}

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06002779 RID: 10105 RVA: 0x000D3647 File Offset: 0x000D1847
	// (set) Token: 0x0600277A RID: 10106 RVA: 0x000D364F File Offset: 0x000D184F
	public bool ToggleRequested { get; set; }

	// Token: 0x04001749 RID: 5961
	public static readonly HashedString PORT_ID = "LogicSwitch";

	// Token: 0x0400174A RID: 5962
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x0400174B RID: 5963
	private static readonly EventSystem.IntraObjectHandler<LogicSwitch> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicSwitch>(delegate(LogicSwitch component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0400174C RID: 5964
	private bool wasOn;

	// Token: 0x0400174D RID: 5965
	private System.Action firstFrameCallback;
}
