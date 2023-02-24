using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200069B RID: 1691
[AddComponentMenu("KMonoBehaviour/scripts/FactionAlignment")]
public class FactionAlignment : KMonoBehaviour
{
	// Token: 0x17000333 RID: 819
	// (get) Token: 0x06002DDD RID: 11741 RVA: 0x000F14F1 File Offset: 0x000EF6F1
	// (set) Token: 0x06002DDE RID: 11742 RVA: 0x000F14F9 File Offset: 0x000EF6F9
	[MyCmpAdd]
	public Health health { get; private set; }

	// Token: 0x17000334 RID: 820
	// (get) Token: 0x06002DDF RID: 11743 RVA: 0x000F1502 File Offset: 0x000EF702
	// (set) Token: 0x06002DE0 RID: 11744 RVA: 0x000F150A File Offset: 0x000EF70A
	public AttackableBase attackable { get; private set; }

	// Token: 0x06002DE1 RID: 11745 RVA: 0x000F1514 File Offset: 0x000EF714
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.health = base.GetComponent<Health>();
		this.attackable = base.GetComponent<AttackableBase>();
		Components.FactionAlignments.Add(this);
		base.Subscribe<FactionAlignment>(493375141, FactionAlignment.OnRefreshUserMenuDelegate);
		base.Subscribe<FactionAlignment>(2127324410, FactionAlignment.SetPlayerTargetedFalseDelegate);
		if (this.alignmentActive)
		{
			FactionManager.Instance.GetFaction(this.Alignment).Members.Add(this);
		}
		GameUtil.SubscribeToTags<FactionAlignment>(this, FactionAlignment.OnDeadTagAddedDelegate, true);
		this.UpdateStatusItem();
	}

	// Token: 0x06002DE2 RID: 11746 RVA: 0x000F15A2 File Offset: 0x000EF7A2
	protected override void OnPrefabInit()
	{
	}

	// Token: 0x06002DE3 RID: 11747 RVA: 0x000F15A4 File Offset: 0x000EF7A4
	private void OnDeath(object data)
	{
		this.SetAlignmentActive(false);
	}

	// Token: 0x06002DE4 RID: 11748 RVA: 0x000F15B0 File Offset: 0x000EF7B0
	public void SetAlignmentActive(bool active)
	{
		this.SetPlayerTargetable(active);
		this.alignmentActive = active;
		if (active)
		{
			FactionManager.Instance.GetFaction(this.Alignment).Members.Add(this);
			return;
		}
		FactionManager.Instance.GetFaction(this.Alignment).Members.Remove(this);
	}

	// Token: 0x06002DE5 RID: 11749 RVA: 0x000F1607 File Offset: 0x000EF807
	public bool IsAlignmentActive()
	{
		return FactionManager.Instance.GetFaction(this.Alignment).Members.Contains(this);
	}

	// Token: 0x06002DE6 RID: 11750 RVA: 0x000F1624 File Offset: 0x000EF824
	public bool IsPlayerTargeted()
	{
		return this.targeted;
	}

	// Token: 0x06002DE7 RID: 11751 RVA: 0x000F162C File Offset: 0x000EF82C
	public void SetPlayerTargetable(bool state)
	{
		this.targetable = state && this.canBePlayerTargeted;
		if (!state)
		{
			this.SetPlayerTargeted(false);
		}
	}

	// Token: 0x06002DE8 RID: 11752 RVA: 0x000F164A File Offset: 0x000EF84A
	public void SetPlayerTargeted(bool state)
	{
		this.targeted = this.canBePlayerTargeted && state && this.targetable;
		this.UpdateStatusItem();
	}

	// Token: 0x06002DE9 RID: 11753 RVA: 0x000F166C File Offset: 0x000EF86C
	private void UpdateStatusItem()
	{
		this.TogglePrioritizable(this.targeted);
		if (this.targeted)
		{
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.OrderAttack, null);
			return;
		}
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.OrderAttack, false);
	}

	// Token: 0x06002DEA RID: 11754 RVA: 0x000F16C8 File Offset: 0x000EF8C8
	private void TogglePrioritizable(bool enable)
	{
		Prioritizable component = base.GetComponent<Prioritizable>();
		if (component == null || !this.updatePrioritizable)
		{
			return;
		}
		if (enable && !this.hasBeenRegisterInPriority)
		{
			Prioritizable.AddRef(base.gameObject);
			this.hasBeenRegisterInPriority = true;
			return;
		}
		if (component.IsPrioritizable() && this.hasBeenRegisterInPriority)
		{
			Prioritizable.RemoveRef(base.gameObject);
			this.hasBeenRegisterInPriority = false;
		}
	}

	// Token: 0x06002DEB RID: 11755 RVA: 0x000F172E File Offset: 0x000EF92E
	public void SwitchAlignment(FactionManager.FactionID newAlignment)
	{
		this.SetAlignmentActive(false);
		this.Alignment = newAlignment;
		this.SetAlignmentActive(true);
	}

	// Token: 0x06002DEC RID: 11756 RVA: 0x000F1745 File Offset: 0x000EF945
	protected override void OnCleanUp()
	{
		Components.FactionAlignments.Remove(this);
		FactionManager.Instance.GetFaction(this.Alignment).Members.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002DED RID: 11757 RVA: 0x000F1774 File Offset: 0x000EF974
	private void OnRefreshUserMenu(object data)
	{
		if (this.Alignment == FactionManager.FactionID.Duplicant)
		{
			return;
		}
		if (!this.canBePlayerTargeted)
		{
			return;
		}
		if (!this.IsAlignmentActive())
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((!this.targeted) ? new KIconButtonMenu.ButtonInfo("action_attack", UI.USERMENUACTIONS.ATTACK.NAME, delegate
		{
			this.SetPlayerTargeted(true);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.ATTACK.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_attack", UI.USERMENUACTIONS.CANCELATTACK.NAME, delegate
		{
			this.SetPlayerTargeted(false);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCELATTACK.TOOLTIP, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x04001B32 RID: 6962
	[SerializeField]
	public bool canBePlayerTargeted = true;

	// Token: 0x04001B33 RID: 6963
	[SerializeField]
	public bool updatePrioritizable = true;

	// Token: 0x04001B34 RID: 6964
	[Serialize]
	private bool alignmentActive = true;

	// Token: 0x04001B35 RID: 6965
	public FactionManager.FactionID Alignment;

	// Token: 0x04001B36 RID: 6966
	[Serialize]
	private bool targeted;

	// Token: 0x04001B37 RID: 6967
	[Serialize]
	private bool targetable = true;

	// Token: 0x04001B38 RID: 6968
	private bool hasBeenRegisterInPriority;

	// Token: 0x04001B39 RID: 6969
	private static readonly EventSystem.IntraObjectHandler<FactionAlignment> OnDeadTagAddedDelegate = GameUtil.CreateHasTagHandler<FactionAlignment>(GameTags.Dead, delegate(FactionAlignment component, object data)
	{
		component.OnDeath(data);
	});

	// Token: 0x04001B3A RID: 6970
	private static readonly EventSystem.IntraObjectHandler<FactionAlignment> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<FactionAlignment>(delegate(FactionAlignment component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04001B3B RID: 6971
	private static readonly EventSystem.IntraObjectHandler<FactionAlignment> SetPlayerTargetedFalseDelegate = new EventSystem.IntraObjectHandler<FactionAlignment>(delegate(FactionAlignment component, object data)
	{
		component.SetPlayerTargeted(false);
	});
}
