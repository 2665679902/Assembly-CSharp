using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000568 RID: 1384
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/BuildingEnabledButton")]
public class BuildingEnabledButton : KMonoBehaviour, ISaveLoadable, IToggleHandler
{
	// Token: 0x17000197 RID: 407
	// (get) Token: 0x0600216B RID: 8555 RVA: 0x000B62DB File Offset: 0x000B44DB
	// (set) Token: 0x0600216C RID: 8556 RVA: 0x000B6300 File Offset: 0x000B4500
	public bool IsEnabled
	{
		get
		{
			return this.Operational != null && this.Operational.GetFlag(BuildingEnabledButton.EnabledFlag);
		}
		set
		{
			this.Operational.SetFlag(BuildingEnabledButton.EnabledFlag, value);
			Game.Instance.userMenu.Refresh(base.gameObject);
			this.buildingEnabled = value;
			base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.BuildingDisabled, !this.buildingEnabled, null);
			base.Trigger(1088293757, this.buildingEnabled);
		}
	}

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x0600216D RID: 8557 RVA: 0x000B6375 File Offset: 0x000B4575
	public bool WaitingForDisable
	{
		get
		{
			return this.IsEnabled && this.Toggleable.IsToggleQueued(this.ToggleIdx);
		}
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x000B6392 File Offset: 0x000B4592
	protected override void OnPrefabInit()
	{
		this.ToggleIdx = this.Toggleable.SetTarget(this);
		base.Subscribe<BuildingEnabledButton>(493375141, BuildingEnabledButton.OnRefreshUserMenuDelegate);
	}

	// Token: 0x0600216F RID: 8559 RVA: 0x000B63B7 File Offset: 0x000B45B7
	protected override void OnSpawn()
	{
		this.IsEnabled = this.buildingEnabled;
		if (this.queuedToggle)
		{
			this.OnMenuToggle();
		}
	}

	// Token: 0x06002170 RID: 8560 RVA: 0x000B63D3 File Offset: 0x000B45D3
	public void HandleToggle()
	{
		this.queuedToggle = false;
		Prioritizable.RemoveRef(base.gameObject);
		this.OnToggle();
	}

	// Token: 0x06002171 RID: 8561 RVA: 0x000B63ED File Offset: 0x000B45ED
	public bool IsHandlerOn()
	{
		return this.IsEnabled;
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x000B63F5 File Offset: 0x000B45F5
	private void OnToggle()
	{
		this.IsEnabled = !this.IsEnabled;
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x000B641C File Offset: 0x000B461C
	private void OnMenuToggle()
	{
		if (!this.Toggleable.IsToggleQueued(this.ToggleIdx))
		{
			if (this.IsEnabled)
			{
				base.Trigger(2108245096, "BuildingDisabled");
			}
			this.queuedToggle = true;
			Prioritizable.AddRef(base.gameObject);
		}
		else
		{
			this.queuedToggle = false;
			Prioritizable.RemoveRef(base.gameObject);
		}
		this.Toggleable.Toggle(this.ToggleIdx);
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x000B64A0 File Offset: 0x000B46A0
	private void OnRefreshUserMenu(object data)
	{
		bool isEnabled = this.IsEnabled;
		bool flag = this.Toggleable.IsToggleQueued(this.ToggleIdx);
		KIconButtonMenu.ButtonInfo buttonInfo;
		if ((isEnabled && !flag) || (!isEnabled && flag))
		{
			buttonInfo = new KIconButtonMenu.ButtonInfo("action_building_disabled", UI.USERMENUACTIONS.ENABLEBUILDING.NAME, new System.Action(this.OnMenuToggle), global::Action.ToggleEnabled, null, null, null, UI.USERMENUACTIONS.ENABLEBUILDING.TOOLTIP, true);
		}
		else
		{
			buttonInfo = new KIconButtonMenu.ButtonInfo("action_building_disabled", UI.USERMENUACTIONS.ENABLEBUILDING.NAME_OFF, new System.Action(this.OnMenuToggle), global::Action.ToggleEnabled, null, null, null, UI.USERMENUACTIONS.ENABLEBUILDING.TOOLTIP_OFF, true);
		}
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x04001334 RID: 4916
	[MyCmpAdd]
	private Toggleable Toggleable;

	// Token: 0x04001335 RID: 4917
	[MyCmpReq]
	private Operational Operational;

	// Token: 0x04001336 RID: 4918
	private int ToggleIdx;

	// Token: 0x04001337 RID: 4919
	[Serialize]
	private bool buildingEnabled = true;

	// Token: 0x04001338 RID: 4920
	[Serialize]
	private bool queuedToggle;

	// Token: 0x04001339 RID: 4921
	public static readonly Operational.Flag EnabledFlag = new Operational.Flag("building_enabled", Operational.Flag.Type.Functional);

	// Token: 0x0400133A RID: 4922
	private static readonly EventSystem.IntraObjectHandler<BuildingEnabledButton> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<BuildingEnabledButton>(delegate(BuildingEnabledButton component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
