using System;
using System.Linq;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B7C RID: 2940
[AddComponentMenu("KMonoBehaviour/scripts/ScheduleMinionWidget")]
public class ScheduleMinionWidget : KMonoBehaviour
{
	// Token: 0x17000674 RID: 1652
	// (get) Token: 0x06005C6D RID: 23661 RVA: 0x0021CC77 File Offset: 0x0021AE77
	// (set) Token: 0x06005C6E RID: 23662 RVA: 0x0021CC7F File Offset: 0x0021AE7F
	public Schedulable schedulable { get; private set; }

	// Token: 0x06005C6F RID: 23663 RVA: 0x0021CC88 File Offset: 0x0021AE88
	public void ChangeAssignment(Schedule targetSchedule, Schedulable schedulable)
	{
		DebugUtil.LogArgs(new object[]
		{
			"Assigning",
			schedulable,
			"from",
			ScheduleManager.Instance.GetSchedule(schedulable).name,
			"to",
			targetSchedule.name
		});
		ScheduleManager.Instance.GetSchedule(schedulable).Unassign(schedulable);
		targetSchedule.Assign(schedulable);
	}

	// Token: 0x06005C70 RID: 23664 RVA: 0x0021CCF0 File Offset: 0x0021AEF0
	public void Setup(Schedulable schedulable)
	{
		this.schedulable = schedulable;
		IAssignableIdentity component = schedulable.GetComponent<IAssignableIdentity>();
		this.portrait.SetIdentityObject(component, true);
		this.label.text = component.GetProperName();
		MinionIdentity minionIdentity = component as MinionIdentity;
		StoredMinionIdentity storedMinionIdentity = component as StoredMinionIdentity;
		this.RefreshWidgetWorldData();
		if (minionIdentity != null)
		{
			Traits component2 = minionIdentity.GetComponent<Traits>();
			if (component2.HasTrait("NightOwl"))
			{
				this.nightOwlIcon.SetActive(true);
			}
			else if (component2.HasTrait("EarlyBird"))
			{
				this.earlyBirdIcon.SetActive(true);
			}
		}
		else if (storedMinionIdentity != null)
		{
			if (storedMinionIdentity.traitIDs.Contains("NightOwl"))
			{
				this.nightOwlIcon.SetActive(true);
			}
			else if (storedMinionIdentity.traitIDs.Contains("EarlyBird"))
			{
				this.earlyBirdIcon.SetActive(true);
			}
		}
		this.dropDown.Initialize(ScheduleManager.Instance.GetSchedules().Cast<IListableOption>(), new Action<IListableOption, object>(this.OnDropEntryClick), null, new Action<DropDownEntry, object>(this.DropEntryRefreshAction), false, schedulable);
	}

	// Token: 0x06005C71 RID: 23665 RVA: 0x0021CE00 File Offset: 0x0021B000
	public void RefreshWidgetWorldData()
	{
		this.worldContainer.SetActive(DlcManager.IsExpansion1Active());
		MinionIdentity minionIdentity = this.schedulable.GetComponent<IAssignableIdentity>() as MinionIdentity;
		if (minionIdentity == null)
		{
			return;
		}
		if (DlcManager.IsExpansion1Active())
		{
			WorldContainer myWorld = minionIdentity.GetMyWorld();
			string text = myWorld.GetComponent<ClusterGridEntity>().Name;
			Image componentInChildren = this.worldContainer.GetComponentInChildren<Image>();
			componentInChildren.sprite = myWorld.GetComponent<ClusterGridEntity>().GetUISprite();
			componentInChildren.SetAlpha((ClusterManager.Instance.activeWorld == myWorld) ? 1f : 0.7f);
			if (ClusterManager.Instance.activeWorld != myWorld)
			{
				text = string.Concat(new string[]
				{
					"<color=",
					Constants.NEUTRAL_COLOR_STR,
					">",
					text,
					"</color>"
				});
			}
			this.worldContainer.GetComponentInChildren<LocText>().SetText(text);
		}
	}

	// Token: 0x06005C72 RID: 23666 RVA: 0x0021CEE8 File Offset: 0x0021B0E8
	private void OnDropEntryClick(IListableOption option, object obj)
	{
		Schedule schedule = (Schedule)option;
		this.ChangeAssignment(schedule, this.schedulable);
	}

	// Token: 0x06005C73 RID: 23667 RVA: 0x0021CF0C File Offset: 0x0021B10C
	private void DropEntryRefreshAction(DropDownEntry entry, object obj)
	{
		Schedule schedule = (Schedule)entry.entryData;
		if (((Schedulable)obj).GetSchedule() == schedule)
		{
			entry.label.text = string.Format(UI.SCHEDULESCREEN.SCHEDULE_DROPDOWN_ASSIGNED, schedule.name);
			entry.button.isInteractable = false;
		}
		else
		{
			entry.label.text = schedule.name;
			entry.button.isInteractable = true;
		}
		entry.gameObject.GetComponent<HierarchyReferences>().GetReference<RectTransform>("worldContainer").gameObject.SetActive(false);
	}

	// Token: 0x06005C74 RID: 23668 RVA: 0x0021CFA0 File Offset: 0x0021B1A0
	public void SetupBlank(Schedule schedule)
	{
		this.label.text = UI.SCHEDULESCREEN.SCHEDULE_DROPDOWN_BLANK;
		this.dropDown.Initialize(Components.LiveMinionIdentities.Items.Cast<IListableOption>(), new Action<IListableOption, object>(this.OnBlankDropEntryClick), new Func<IListableOption, IListableOption, object, int>(this.BlankDropEntrySort), new Action<DropDownEntry, object>(this.BlankDropEntryRefreshAction), false, schedule);
		Components.LiveMinionIdentities.OnAdd += this.OnLivingMinionsChanged;
		Components.LiveMinionIdentities.OnRemove += this.OnLivingMinionsChanged;
	}

	// Token: 0x06005C75 RID: 23669 RVA: 0x0021D02E File Offset: 0x0021B22E
	private void OnLivingMinionsChanged(MinionIdentity minion)
	{
		this.dropDown.ChangeContent(Components.LiveMinionIdentities.Items.Cast<IListableOption>());
	}

	// Token: 0x06005C76 RID: 23670 RVA: 0x0021D04C File Offset: 0x0021B24C
	private void OnBlankDropEntryClick(IListableOption option, object obj)
	{
		Schedule schedule = (Schedule)obj;
		MinionIdentity minionIdentity = (MinionIdentity)option;
		if (minionIdentity == null || minionIdentity.HasTag(GameTags.Dead))
		{
			return;
		}
		this.ChangeAssignment(schedule, minionIdentity.GetComponent<Schedulable>());
	}

	// Token: 0x06005C77 RID: 23671 RVA: 0x0021D08C File Offset: 0x0021B28C
	private void BlankDropEntryRefreshAction(DropDownEntry entry, object obj)
	{
		Schedule schedule = (Schedule)obj;
		MinionIdentity minionIdentity = (MinionIdentity)entry.entryData;
		WorldContainer myWorld = minionIdentity.GetMyWorld();
		entry.gameObject.GetComponent<HierarchyReferences>().GetReference<RectTransform>("worldContainer").gameObject.SetActive(DlcManager.IsExpansion1Active());
		Image reference = entry.gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("worldIcon");
		reference.sprite = myWorld.GetComponent<ClusterGridEntity>().GetUISprite();
		reference.SetAlpha((ClusterManager.Instance.activeWorld == myWorld) ? 1f : 0.7f);
		string text = myWorld.GetComponent<ClusterGridEntity>().Name;
		if (ClusterManager.Instance.activeWorld != myWorld)
		{
			text = string.Concat(new string[]
			{
				"<color=",
				Constants.NEUTRAL_COLOR_STR,
				">",
				text,
				"</color>"
			});
		}
		entry.gameObject.GetComponent<HierarchyReferences>().GetReference<LocText>("worldLabel").SetText(text);
		if (schedule.IsAssigned(minionIdentity.GetComponent<Schedulable>()))
		{
			entry.label.text = string.Format(UI.SCHEDULESCREEN.SCHEDULE_DROPDOWN_ASSIGNED, minionIdentity.GetProperName());
			entry.button.isInteractable = false;
		}
		else
		{
			entry.label.text = minionIdentity.GetProperName();
			entry.button.isInteractable = true;
		}
		Traits component = minionIdentity.GetComponent<Traits>();
		entry.gameObject.GetComponent<HierarchyReferences>().GetReference<RectTransform>("NightOwlIcon").gameObject.SetActive(component.HasTrait("NightOwl"));
		entry.gameObject.GetComponent<HierarchyReferences>().GetReference<RectTransform>("EarlyBirdIcon").gameObject.SetActive(component.HasTrait("EarlyBird"));
	}

	// Token: 0x06005C78 RID: 23672 RVA: 0x0021D240 File Offset: 0x0021B440
	private int BlankDropEntrySort(IListableOption a, IListableOption b, object obj)
	{
		Schedule schedule = (Schedule)obj;
		MinionIdentity minionIdentity = (MinionIdentity)a;
		MinionIdentity minionIdentity2 = (MinionIdentity)b;
		bool flag = schedule.IsAssigned(minionIdentity.GetComponent<Schedulable>());
		bool flag2 = schedule.IsAssigned(minionIdentity2.GetComponent<Schedulable>());
		if (flag && !flag2)
		{
			return -1;
		}
		if (!flag && flag2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06005C79 RID: 23673 RVA: 0x0021D28D File Offset: 0x0021B48D
	protected override void OnCleanUp()
	{
		Components.LiveMinionIdentities.OnAdd -= this.OnLivingMinionsChanged;
		Components.LiveMinionIdentities.OnRemove -= this.OnLivingMinionsChanged;
	}

	// Token: 0x04003F26 RID: 16166
	[SerializeField]
	private CrewPortrait portrait;

	// Token: 0x04003F27 RID: 16167
	[SerializeField]
	private DropDown dropDown;

	// Token: 0x04003F28 RID: 16168
	[SerializeField]
	private LocText label;

	// Token: 0x04003F29 RID: 16169
	[SerializeField]
	private GameObject nightOwlIcon;

	// Token: 0x04003F2A RID: 16170
	[SerializeField]
	private GameObject earlyBirdIcon;

	// Token: 0x04003F2B RID: 16171
	[SerializeField]
	private GameObject worldContainer;
}
