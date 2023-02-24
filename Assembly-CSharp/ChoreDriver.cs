using System;
using STRINGS;

// Token: 0x020003A5 RID: 933
public class ChoreDriver : StateMachineComponent<ChoreDriver.StatesInstance>
{
	// Token: 0x0600132C RID: 4908 RVA: 0x00065D7D File Offset: 0x00063F7D
	public Chore GetCurrentChore()
	{
		return base.smi.GetCurrentChore();
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x00065D8A File Offset: 0x00063F8A
	public bool HasChore()
	{
		return base.smi.GetCurrentChore() != null;
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x00065D9A File Offset: 0x00063F9A
	public void StopChore()
	{
		base.smi.sm.stop.Trigger(base.smi);
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x00065DB8 File Offset: 0x00063FB8
	public void SetChore(Chore.Precondition.Context context)
	{
		Chore currentChore = base.smi.GetCurrentChore();
		if (currentChore != context.chore)
		{
			this.StopChore();
			if (context.chore.IsValid())
			{
				context.chore.PrepareChore(ref context);
				this.context = context;
				base.smi.sm.nextChore.Set(context.chore, base.smi, false);
				return;
			}
			string text = "Null";
			string text2 = "Null";
			if (currentChore != null)
			{
				text = currentChore.GetType().Name;
			}
			if (context.chore != null)
			{
				text2 = context.chore.GetType().Name;
			}
			Debug.LogWarning(string.Concat(new string[] { "Stopping chore ", text, " to start ", text2, " but stopping the first chore cancelled the second one." }));
		}
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x00065E8C File Offset: 0x0006408C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x04000A69 RID: 2665
	[MyCmpAdd]
	private User user;

	// Token: 0x04000A6A RID: 2666
	private Chore.Precondition.Context context;

	// Token: 0x02000FB0 RID: 4016
	public class StatesInstance : GameStateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.GameInstance
	{
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06007034 RID: 28724 RVA: 0x002A63A6 File Offset: 0x002A45A6
		// (set) Token: 0x06007035 RID: 28725 RVA: 0x002A63AE File Offset: 0x002A45AE
		public string masterProperName { get; private set; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06007036 RID: 28726 RVA: 0x002A63B7 File Offset: 0x002A45B7
		// (set) Token: 0x06007037 RID: 28727 RVA: 0x002A63BF File Offset: 0x002A45BF
		public KPrefabID masterPrefabId { get; private set; }

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06007038 RID: 28728 RVA: 0x002A63C8 File Offset: 0x002A45C8
		// (set) Token: 0x06007039 RID: 28729 RVA: 0x002A63D0 File Offset: 0x002A45D0
		public Navigator navigator { get; private set; }

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600703A RID: 28730 RVA: 0x002A63D9 File Offset: 0x002A45D9
		// (set) Token: 0x0600703B RID: 28731 RVA: 0x002A63E1 File Offset: 0x002A45E1
		public Worker worker { get; private set; }

		// Token: 0x0600703C RID: 28732 RVA: 0x002A63EC File Offset: 0x002A45EC
		public StatesInstance(ChoreDriver master)
			: base(master)
		{
			this.masterProperName = base.master.GetProperName();
			this.masterPrefabId = base.master.GetComponent<KPrefabID>();
			this.navigator = base.master.GetComponent<Navigator>();
			this.worker = base.master.GetComponent<Worker>();
			this.choreConsumer = base.GetComponent<ChoreConsumer>();
			ChoreConsumer choreConsumer = this.choreConsumer;
			choreConsumer.choreRulesChanged = (System.Action)Delegate.Combine(choreConsumer.choreRulesChanged, new System.Action(this.OnChoreRulesChanged));
		}

		// Token: 0x0600703D RID: 28733 RVA: 0x002A6478 File Offset: 0x002A4678
		public void BeginChore()
		{
			Chore nextChore = this.GetNextChore();
			Chore chore = base.smi.sm.currentChore.Set(nextChore, base.smi, false);
			if (chore != null && chore.IsPreemptable && chore.driver != null)
			{
				chore.Fail("Preemption!");
			}
			base.smi.sm.nextChore.Set(null, base.smi, false);
			Chore chore2 = chore;
			chore2.onExit = (Action<Chore>)Delegate.Combine(chore2.onExit, new Action<Chore>(this.OnChoreExit));
			chore.Begin(base.master.context);
			base.Trigger(-1988963660, chore);
		}

		// Token: 0x0600703E RID: 28734 RVA: 0x002A652C File Offset: 0x002A472C
		public void EndChore(string reason)
		{
			if (this.GetCurrentChore() != null)
			{
				Chore currentChore = this.GetCurrentChore();
				base.smi.sm.currentChore.Set(null, base.smi, false);
				Chore chore = currentChore;
				chore.onExit = (Action<Chore>)Delegate.Remove(chore.onExit, new Action<Chore>(this.OnChoreExit));
				currentChore.Fail(reason);
				base.Trigger(1745615042, currentChore);
			}
		}

		// Token: 0x0600703F RID: 28735 RVA: 0x002A659B File Offset: 0x002A479B
		private void OnChoreExit(Chore chore)
		{
			base.smi.sm.stop.Trigger(base.smi);
		}

		// Token: 0x06007040 RID: 28736 RVA: 0x002A65B8 File Offset: 0x002A47B8
		public Chore GetNextChore()
		{
			return base.smi.sm.nextChore.Get(base.smi);
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x002A65D5 File Offset: 0x002A47D5
		public Chore GetCurrentChore()
		{
			return base.smi.sm.currentChore.Get(base.smi);
		}

		// Token: 0x06007042 RID: 28738 RVA: 0x002A65F4 File Offset: 0x002A47F4
		private void OnChoreRulesChanged()
		{
			Chore currentChore = this.GetCurrentChore();
			if (currentChore != null && !this.choreConsumer.IsPermittedOrEnabled(currentChore.choreType, currentChore))
			{
				this.EndChore("Permissions changed");
			}
		}

		// Token: 0x0400553B RID: 21819
		private ChoreConsumer choreConsumer;
	}

	// Token: 0x02000FB1 RID: 4017
	public class States : GameStateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver>
	{
		// Token: 0x06007043 RID: 28739 RVA: 0x002A662C File Offset: 0x002A482C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.nochore;
			this.saveHistory = true;
			this.nochore.Update(delegate(ChoreDriver.StatesInstance smi, float dt)
			{
				if (smi.masterPrefabId.IsPrefabID(GameTags.Minion) && !smi.masterPrefabId.HasTag(GameTags.Dead))
				{
					ReportManager.Instance.ReportValue(ReportManager.ReportType.WorkTime, dt, string.Format(UI.ENDOFDAYREPORT.NOTES.TIME_SPENT, DUPLICANTS.CHORES.THINKING.NAME), smi.master.GetProperName());
				}
			}, UpdateRate.SIM_200ms, false).ParamTransition<Chore>(this.nextChore, this.haschore, (ChoreDriver.StatesInstance smi, Chore next_chore) => next_chore != null);
			this.haschore.Enter("BeginChore", delegate(ChoreDriver.StatesInstance smi)
			{
				smi.BeginChore();
			}).Update(delegate(ChoreDriver.StatesInstance smi, float dt)
			{
				if (smi.masterPrefabId.IsPrefabID(GameTags.Minion) && !smi.masterPrefabId.HasTag(GameTags.Dead))
				{
					Chore chore = this.currentChore.Get(smi);
					if (chore == null)
					{
						return;
					}
					if (smi.navigator.IsMoving())
					{
						ReportManager.Instance.ReportValue(ReportManager.ReportType.TravelTime, dt, GameUtil.GetChoreName(chore, null), smi.master.GetProperName());
						return;
					}
					ReportManager.ReportType reportType = chore.GetReportType();
					Workable workable = smi.worker.workable;
					if (workable != null)
					{
						ReportManager.ReportType reportType2 = workable.GetReportType();
						if (reportType != reportType2)
						{
							reportType = reportType2;
						}
					}
					ReportManager.Instance.ReportValue(reportType, dt, string.Format(UI.ENDOFDAYREPORT.NOTES.WORK_TIME, GameUtil.GetChoreName(chore, null)), smi.master.GetProperName());
				}
			}, UpdateRate.SIM_200ms, false).Exit("EndChore", delegate(ChoreDriver.StatesInstance smi)
			{
				smi.EndChore("ChoreDriver.SignalStop");
			})
				.OnSignal(this.stop, this.nochore);
		}

		// Token: 0x0400553C RID: 21820
		public StateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.ObjectParameter<Chore> currentChore;

		// Token: 0x0400553D RID: 21821
		public StateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.ObjectParameter<Chore> nextChore;

		// Token: 0x0400553E RID: 21822
		public StateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.Signal stop;

		// Token: 0x0400553F RID: 21823
		public GameStateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.State nochore;

		// Token: 0x04005540 RID: 21824
		public GameStateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.State haschore;
	}
}
