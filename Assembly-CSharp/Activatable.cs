using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000448 RID: 1096
[AddComponentMenu("KMonoBehaviour/Workable/Activatable")]
public class Activatable : Workable, ISidescreenButtonControl
{
	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x06001798 RID: 6040 RVA: 0x0007BE97 File Offset: 0x0007A097
	public bool IsActivated
	{
		get
		{
			return this.activated;
		}
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x0007BE9F File Offset: 0x0007A09F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.activatedFlag = new Operational.Flag("activated", this.ActivationFlagType);
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x0007BEBD File Offset: 0x0007A0BD
	protected override void OnSpawn()
	{
		this.UpdateFlag();
		if (this.awaitingActivation && this.activateChore == null)
		{
			this.CreateChore();
		}
	}

	// Token: 0x0600179B RID: 6043 RVA: 0x0007BEDB File Offset: 0x0007A0DB
	protected override void OnCompleteWork(Worker worker)
	{
		this.activated = true;
		if (this.onActivate != null)
		{
			this.onActivate();
		}
		this.awaitingActivation = false;
		this.UpdateFlag();
		Prioritizable.RemoveRef(base.gameObject);
		base.OnCompleteWork(worker);
	}

	// Token: 0x0600179C RID: 6044 RVA: 0x0007BF18 File Offset: 0x0007A118
	private void UpdateFlag()
	{
		base.GetComponent<Operational>().SetFlag(this.activatedFlag, this.activated);
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.DuplicantActivationRequired, !this.activated, null);
		base.Trigger(-1909216579, this.IsActivated);
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x0007BF78 File Offset: 0x0007A178
	private void CreateChore()
	{
		if (this.activateChore != null)
		{
			return;
		}
		Prioritizable.AddRef(base.gameObject);
		this.activateChore = new WorkChore<Activatable>(Db.Get().ChoreTypes.Toggle, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		if (!string.IsNullOrEmpty(this.requiredSkillPerk))
		{
			this.shouldShowSkillPerkStatusItem = true;
			this.requireMinionToWork = true;
			this.UpdateStatusItem(null);
		}
	}

	// Token: 0x0600179E RID: 6046 RVA: 0x0007BFE7 File Offset: 0x0007A1E7
	private void CancelChore()
	{
		if (this.activateChore == null)
		{
			return;
		}
		this.activateChore.Cancel("User cancelled");
		this.activateChore = null;
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x0600179F RID: 6047 RVA: 0x0007C00C File Offset: 0x0007A20C
	public string SidescreenButtonText
	{
		get
		{
			if (this.activateChore != null)
			{
				return this.textOverride.IsValid ? this.textOverride.CancelText : UI.USERMENUACTIONS.ACTIVATEBUILDING.ACTIVATE_CANCEL;
			}
			return this.textOverride.IsValid ? this.textOverride.Text : UI.USERMENUACTIONS.ACTIVATEBUILDING.ACTIVATE;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0007C06C File Offset: 0x0007A26C
	public string SidescreenButtonTooltip
	{
		get
		{
			if (this.activateChore != null)
			{
				return this.textOverride.IsValid ? this.textOverride.CancelToolTip : UI.USERMENUACTIONS.ACTIVATEBUILDING.TOOLTIP_CANCEL;
			}
			return this.textOverride.IsValid ? this.textOverride.ToolTip : UI.USERMENUACTIONS.ACTIVATEBUILDING.TOOLTIP_ACTIVATE;
		}
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x0007C0CA File Offset: 0x0007A2CA
	public bool SidescreenEnabled()
	{
		return !this.activated;
	}

	// Token: 0x060017A2 RID: 6050 RVA: 0x0007C0D5 File Offset: 0x0007A2D5
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		this.textOverride = text;
	}

	// Token: 0x060017A3 RID: 6051 RVA: 0x0007C0DE File Offset: 0x0007A2DE
	public void OnSidescreenButtonPressed()
	{
		if (this.activateChore == null)
		{
			this.CreateChore();
		}
		else
		{
			this.CancelChore();
		}
		this.awaitingActivation = this.activateChore != null;
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x0007C105 File Offset: 0x0007A305
	public bool SidescreenButtonInteractable()
	{
		return !this.activated;
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x0007C110 File Offset: 0x0007A310
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x04000D13 RID: 3347
	public Operational.Flag.Type ActivationFlagType;

	// Token: 0x04000D14 RID: 3348
	private Operational.Flag activatedFlag;

	// Token: 0x04000D15 RID: 3349
	[Serialize]
	private bool activated;

	// Token: 0x04000D16 RID: 3350
	[Serialize]
	private bool awaitingActivation;

	// Token: 0x04000D17 RID: 3351
	private Guid statusItem;

	// Token: 0x04000D18 RID: 3352
	private Chore activateChore;

	// Token: 0x04000D19 RID: 3353
	public System.Action onActivate;

	// Token: 0x04000D1A RID: 3354
	[SerializeField]
	private ButtonMenuTextOverride textOverride;
}
