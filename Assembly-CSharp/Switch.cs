using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000654 RID: 1620
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Switch")]
public class Switch : KMonoBehaviour, ISaveLoadable, IToggleHandler
{
	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000E4307 File Offset: 0x000E2507
	public bool IsSwitchedOn
	{
		get
		{
			return this.switchedOn;
		}
	}

	// Token: 0x14000012 RID: 18
	// (add) Token: 0x06002B5E RID: 11102 RVA: 0x000E4310 File Offset: 0x000E2510
	// (remove) Token: 0x06002B5F RID: 11103 RVA: 0x000E4348 File Offset: 0x000E2548
	public event Action<bool> OnToggle;

	// Token: 0x06002B60 RID: 11104 RVA: 0x000E437D File Offset: 0x000E257D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.switchedOn = this.defaultState;
	}

	// Token: 0x06002B61 RID: 11105 RVA: 0x000E4394 File Offset: 0x000E2594
	protected override void OnSpawn()
	{
		this.openToggleIndex = this.openSwitch.SetTarget(this);
		if (this.OnToggle != null)
		{
			this.OnToggle(this.switchedOn);
		}
		if (this.manuallyControlled)
		{
			base.Subscribe<Switch>(493375141, Switch.OnRefreshUserMenuDelegate);
		}
		this.UpdateSwitchStatus();
	}

	// Token: 0x06002B62 RID: 11106 RVA: 0x000E43EB File Offset: 0x000E25EB
	public void HandleToggle()
	{
		this.Toggle();
	}

	// Token: 0x06002B63 RID: 11107 RVA: 0x000E43F3 File Offset: 0x000E25F3
	public bool IsHandlerOn()
	{
		return this.switchedOn;
	}

	// Token: 0x06002B64 RID: 11108 RVA: 0x000E43FB File Offset: 0x000E25FB
	private void OnMinionToggle()
	{
		if (!DebugHandler.InstantBuildMode)
		{
			this.openSwitch.Toggle(this.openToggleIndex);
			return;
		}
		this.Toggle();
	}

	// Token: 0x06002B65 RID: 11109 RVA: 0x000E441C File Offset: 0x000E261C
	protected virtual void Toggle()
	{
		this.SetState(!this.switchedOn);
	}

	// Token: 0x06002B66 RID: 11110 RVA: 0x000E4430 File Offset: 0x000E2630
	protected virtual void SetState(bool on)
	{
		if (this.switchedOn != on)
		{
			this.switchedOn = on;
			this.UpdateSwitchStatus();
			if (this.OnToggle != null)
			{
				this.OnToggle(this.switchedOn);
			}
			if (this.manuallyControlled)
			{
				Game.Instance.userMenu.Refresh(base.gameObject);
			}
		}
	}

	// Token: 0x06002B67 RID: 11111 RVA: 0x000E448C File Offset: 0x000E268C
	protected virtual void OnRefreshUserMenu(object data)
	{
		LocString locString = (this.switchedOn ? BUILDINGS.PREFABS.SWITCH.TURN_OFF : BUILDINGS.PREFABS.SWITCH.TURN_ON);
		LocString locString2 = (this.switchedOn ? BUILDINGS.PREFABS.SWITCH.TURN_OFF_TOOLTIP : BUILDINGS.PREFABS.SWITCH.TURN_ON_TOOLTIP);
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_power", locString, new System.Action(this.OnMinionToggle), global::Action.ToggleEnabled, null, null, null, locString2, true), 1f);
	}

	// Token: 0x06002B68 RID: 11112 RVA: 0x000E4508 File Offset: 0x000E2708
	protected virtual void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.SwitchStatusActive : Db.Get().BuildingStatusItems.SwitchStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x040019AB RID: 6571
	[SerializeField]
	public bool manuallyControlled = true;

	// Token: 0x040019AC RID: 6572
	[SerializeField]
	public bool defaultState = true;

	// Token: 0x040019AD RID: 6573
	[Serialize]
	protected bool switchedOn = true;

	// Token: 0x040019AE RID: 6574
	[MyCmpAdd]
	private Toggleable openSwitch;

	// Token: 0x040019AF RID: 6575
	private int openToggleIndex;

	// Token: 0x040019B1 RID: 6577
	private static readonly EventSystem.IntraObjectHandler<Switch> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Switch>(delegate(Switch component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
