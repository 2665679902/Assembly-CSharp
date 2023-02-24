using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008F0 RID: 2288
[AddComponentMenu("KMonoBehaviour/Workable/RoleStation")]
public class RoleStation : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x060041D8 RID: 16856 RVA: 0x00172B0C File Offset: 0x00170D0C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.synchronizeAnims = true;
		this.UpdateStatusItemDelegate = new Action<object>(this.UpdateSkillPointAvailableStatusItem);
	}

	// Token: 0x060041D9 RID: 16857 RVA: 0x00172B30 File Offset: 0x00170D30
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.RoleStations.Add(this);
		this.smi = new RoleStation.RoleStationSM.Instance(this);
		this.smi.StartSM();
		base.SetWorkTime(7.53f);
		this.resetProgressOnStop = true;
		this.subscriptions.Add(Game.Instance.Subscribe(-1523247426, this.UpdateStatusItemDelegate));
		this.subscriptions.Add(Game.Instance.Subscribe(1505456302, this.UpdateStatusItemDelegate));
		this.UpdateSkillPointAvailableStatusItem(null);
	}

	// Token: 0x060041DA RID: 16858 RVA: 0x00172BC0 File Offset: 0x00170DC0
	protected override void OnStopWork(Worker worker)
	{
		Telepad.StatesInstance statesInstance = this.GetSMI<Telepad.StatesInstance>();
		statesInstance.sm.idlePortal.Trigger(statesInstance);
	}

	// Token: 0x060041DB RID: 16859 RVA: 0x00172BE8 File Offset: 0x00170DE8
	private void UpdateSkillPointAvailableStatusItem(object data = null)
	{
		foreach (object obj in Components.MinionResumes)
		{
			MinionResume minionResume = (MinionResume)obj;
			if (!minionResume.HasTag(GameTags.Dead) && minionResume.TotalSkillPointsGained - minionResume.SkillsMastered > 0)
			{
				if (this.skillPointAvailableStatusItem == Guid.Empty)
				{
					this.skillPointAvailableStatusItem = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.SkillPointsAvailable, null);
				}
				return;
			}
		}
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.SkillPointsAvailable, false);
		this.skillPointAvailableStatusItem = Guid.Empty;
	}

	// Token: 0x060041DC RID: 16860 RVA: 0x00172CB4 File Offset: 0x00170EB4
	private Chore CreateWorkChore()
	{
		return new WorkChore<RoleStation>(Db.Get().ChoreTypes.LearnSkill, this, null, true, null, null, null, false, null, false, true, Assets.GetAnim("anim_hat_kanim"), false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, false);
	}

	// Token: 0x060041DD RID: 16861 RVA: 0x00172CF5 File Offset: 0x00170EF5
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		worker.GetComponent<MinionResume>().SkillLearned();
	}

	// Token: 0x060041DE RID: 16862 RVA: 0x00172D09 File Offset: 0x00170F09
	private void OnSelectRolesClick()
	{
		DetailsScreen.Instance.Show(false);
		ManagementMenu.Instance.ToggleSkills();
	}

	// Token: 0x060041DF RID: 16863 RVA: 0x00172D20 File Offset: 0x00170F20
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		foreach (int num in this.subscriptions)
		{
			Game.Instance.Unsubscribe(num);
		}
		Components.RoleStations.Remove(this);
	}

	// Token: 0x060041E0 RID: 16864 RVA: 0x00172D88 File Offset: 0x00170F88
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		return base.GetDescriptors(go);
	}

	// Token: 0x04002BE6 RID: 11238
	private Chore chore;

	// Token: 0x04002BE7 RID: 11239
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x04002BE8 RID: 11240
	[MyCmpAdd]
	private Operational operational;

	// Token: 0x04002BE9 RID: 11241
	private RoleStation.RoleStationSM.Instance smi;

	// Token: 0x04002BEA RID: 11242
	private Guid skillPointAvailableStatusItem;

	// Token: 0x04002BEB RID: 11243
	private Action<object> UpdateStatusItemDelegate;

	// Token: 0x04002BEC RID: 11244
	private List<int> subscriptions = new List<int>();

	// Token: 0x020016BA RID: 5818
	public class RoleStationSM : GameStateMachine<RoleStation.RoleStationSM, RoleStation.RoleStationSM.Instance, RoleStation>
	{
		// Token: 0x06008857 RID: 34903 RVA: 0x002F5334 File Offset: 0x002F3534
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.unoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (RoleStation.RoleStationSM.Instance smi) => smi.GetComponent<Operational>().IsOperational);
			this.operational.ToggleChore((RoleStation.RoleStationSM.Instance smi) => smi.master.CreateWorkChore(), this.unoperational);
		}

		// Token: 0x04006AB5 RID: 27317
		public GameStateMachine<RoleStation.RoleStationSM, RoleStation.RoleStationSM.Instance, RoleStation, object>.State unoperational;

		// Token: 0x04006AB6 RID: 27318
		public GameStateMachine<RoleStation.RoleStationSM, RoleStation.RoleStationSM.Instance, RoleStation, object>.State operational;

		// Token: 0x020020A9 RID: 8361
		public new class Instance : GameStateMachine<RoleStation.RoleStationSM, RoleStation.RoleStationSM.Instance, RoleStation, object>.GameInstance
		{
			// Token: 0x0600A49B RID: 42139 RVA: 0x003482B7 File Offset: 0x003464B7
			public Instance(RoleStation master)
				: base(master)
			{
			}
		}
	}
}
