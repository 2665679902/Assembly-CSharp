using System;
using Klei.AI;
using TUNING;

// Token: 0x0200038D RID: 909
public class PartyChore : Chore<PartyChore.StatesInstance>, IWorkerPrioritizable
{
	// Token: 0x0600127D RID: 4733 RVA: 0x00062E74 File Offset: 0x00061074
	public PartyChore(IStateMachineTarget master, Workable chat_workable, Action<Chore> on_complete = null, Action<Chore> on_begin = null, Action<Chore> on_end = null)
		: base(Db.Get().ChoreTypes.Party, master, master.GetComponent<ChoreProvider>(), true, on_complete, on_begin, on_end, PriorityScreen.PriorityClass.high, 5, false, true, 0, false, ReportManager.ReportType.PersonalTime)
	{
		base.smi = new PartyChore.StatesInstance(this);
		base.smi.sm.chitchatlocator.Set(chat_workable, base.smi);
		base.AddPrecondition(ChorePreconditions.instance.CanMoveTo, chat_workable);
		base.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x00062F00 File Offset: 0x00061100
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.partyer.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
		base.smi.sm.partyer.Get(base.smi).gameObject.AddTag(GameTags.Partying);
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x00062F68 File Offset: 0x00061168
	protected override void End(string reason)
	{
		if (base.smi.sm.partyer.Get(base.smi) != null)
		{
			base.smi.sm.partyer.Get(base.smi).gameObject.RemoveTag(GameTags.Partying);
		}
		base.End(reason);
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x00062FC9 File Offset: 0x000611C9
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		return true;
	}

	// Token: 0x040009FE RID: 2558
	public int basePriority = RELAXATION.PRIORITY.SPECIAL_EVENT;

	// Token: 0x040009FF RID: 2559
	public const string specificEffect = "Socialized";

	// Token: 0x04000A00 RID: 2560
	public const string trackingEffect = "RecentlySocialized";

	// Token: 0x02000F7E RID: 3966
	public class States : GameStateMachine<PartyChore.States, PartyChore.StatesInstance, PartyChore>
	{
		// Token: 0x06006FA7 RID: 28583 RVA: 0x002A31C4 File Offset: 0x002A13C4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.stand;
			base.Target(this.partyer);
			this.stand.InitializeStates(this.partyer, this.masterTarget, this.chat, null, null, null);
			this.chat_move.InitializeStates(this.partyer, this.chitchatlocator, this.chat, null, null, null);
			this.chat.ToggleWork<Workable>(this.chitchatlocator, this.success, null, null);
			this.success.Enter(delegate(PartyChore.StatesInstance smi)
			{
				this.partyer.Get(smi).gameObject.GetComponent<Effects>().Add("RecentlyPartied", true);
			}).ReturnSuccess();
		}

		// Token: 0x040054AA RID: 21674
		public StateMachine<PartyChore.States, PartyChore.StatesInstance, PartyChore, object>.TargetParameter partyer;

		// Token: 0x040054AB RID: 21675
		public StateMachine<PartyChore.States, PartyChore.StatesInstance, PartyChore, object>.TargetParameter chitchatlocator;

		// Token: 0x040054AC RID: 21676
		public GameStateMachine<PartyChore.States, PartyChore.StatesInstance, PartyChore, object>.ApproachSubState<IApproachable> stand;

		// Token: 0x040054AD RID: 21677
		public GameStateMachine<PartyChore.States, PartyChore.StatesInstance, PartyChore, object>.ApproachSubState<IApproachable> chat_move;

		// Token: 0x040054AE RID: 21678
		public GameStateMachine<PartyChore.States, PartyChore.StatesInstance, PartyChore, object>.State chat;

		// Token: 0x040054AF RID: 21679
		public GameStateMachine<PartyChore.States, PartyChore.StatesInstance, PartyChore, object>.State success;
	}

	// Token: 0x02000F7F RID: 3967
	public class StatesInstance : GameStateMachine<PartyChore.States, PartyChore.StatesInstance, PartyChore, object>.GameInstance
	{
		// Token: 0x06006FAA RID: 28586 RVA: 0x002A328A File Offset: 0x002A148A
		public StatesInstance(PartyChore master)
			: base(master)
		{
		}
	}
}
