using System;
using Klei.AI;
using UnityEngine;

// Token: 0x0200037F RID: 895
public class EmoteChore : Chore<EmoteChore.StatesInstance>
{
	// Token: 0x06001239 RID: 4665 RVA: 0x00061670 File Offset: 0x0005F870
	public EmoteChore(IStateMachineTarget target, ChoreType chore_type, Emote emote, int emoteIterations = 1, Func<StatusItem> get_status_item = null)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new EmoteChore.StatesInstance(this, target.gameObject, emote, KAnim.PlayMode.Once, emoteIterations, false);
		this.getStatusItem = get_status_item;
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x000616B8 File Offset: 0x0005F8B8
	public EmoteChore(IStateMachineTarget target, ChoreType chore_type, Emote emote, KAnim.PlayMode play_mode, int emoteIterations = 1, bool flip_x = false)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new EmoteChore.StatesInstance(this, target.gameObject, emote, play_mode, emoteIterations, flip_x);
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x000616F8 File Offset: 0x0005F8F8
	public EmoteChore(IStateMachineTarget target, ChoreType chore_type, HashedString animFile, HashedString[] anims, Func<StatusItem> get_status_item = null)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new EmoteChore.StatesInstance(this, target.gameObject, animFile, anims, KAnim.PlayMode.Once, false);
		this.getStatusItem = get_status_item;
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x00061740 File Offset: 0x0005F940
	public EmoteChore(IStateMachineTarget target, ChoreType chore_type, HashedString animFile, HashedString[] anims, KAnim.PlayMode play_mode, bool flip_x = false)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new EmoteChore.StatesInstance(this, target.gameObject, animFile, anims, play_mode, flip_x);
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x00061780 File Offset: 0x0005F980
	protected override StatusItem GetStatusItem()
	{
		if (this.getStatusItem == null)
		{
			return base.GetStatusItem();
		}
		return this.getStatusItem();
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x0006179C File Offset: 0x0005F99C
	public override string ToString()
	{
		if (base.smi.animFile != null)
		{
			return "EmoteChore<" + base.smi.animFile.GetData().name + ">";
		}
		string text = "EmoteChore<";
		HashedString hashedString = base.smi.emoteAnims[0];
		return text + hashedString.ToString() + ">";
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x0006180F File Offset: 0x0005FA0F
	public void PairReactable(SelfEmoteReactable reactable)
	{
		this.reactable = reactable;
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x00061818 File Offset: 0x0005FA18
	protected new virtual void End(string reason)
	{
		if (this.reactable != null)
		{
			this.reactable.PairEmote(null);
			this.reactable.Cleanup();
			this.reactable = null;
		}
		base.End(reason);
	}

	// Token: 0x040009E9 RID: 2537
	private Func<StatusItem> getStatusItem;

	// Token: 0x040009EA RID: 2538
	private SelfEmoteReactable reactable;

	// Token: 0x02000F5D RID: 3933
	public class StatesInstance : GameStateMachine<EmoteChore.States, EmoteChore.StatesInstance, EmoteChore, object>.GameInstance
	{
		// Token: 0x06006F4A RID: 28490 RVA: 0x002A051C File Offset: 0x0029E71C
		public StatesInstance(EmoteChore master, GameObject emoter, Emote emote, KAnim.PlayMode mode, int emoteIterations, bool flip_x)
			: base(master)
		{
			this.mode = mode;
			this.animFile = emote.AnimSet;
			emote.CollectStepAnims(out this.emoteAnims, emoteIterations);
			base.sm.emoter.Set(emoter, base.smi, false);
		}

		// Token: 0x06006F4B RID: 28491 RVA: 0x002A0574 File Offset: 0x0029E774
		public StatesInstance(EmoteChore master, GameObject emoter, HashedString animFile, HashedString[] anims, KAnim.PlayMode mode, bool flip_x)
			: base(master)
		{
			this.mode = mode;
			this.animFile = Assets.GetAnim(animFile);
			this.emoteAnims = anims;
			base.sm.emoter.Set(emoter, base.smi, false);
		}

		// Token: 0x04005437 RID: 21559
		public KAnimFile animFile;

		// Token: 0x04005438 RID: 21560
		public HashedString[] emoteAnims;

		// Token: 0x04005439 RID: 21561
		public KAnim.PlayMode mode = KAnim.PlayMode.Once;
	}

	// Token: 0x02000F5E RID: 3934
	public class States : GameStateMachine<EmoteChore.States, EmoteChore.StatesInstance, EmoteChore>
	{
		// Token: 0x06006F4C RID: 28492 RVA: 0x002A05C4 File Offset: 0x0029E7C4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			base.Target(this.emoter);
			this.root.ToggleAnims((EmoteChore.StatesInstance smi) => smi.animFile).PlayAnims((EmoteChore.StatesInstance smi) => smi.emoteAnims, (EmoteChore.StatesInstance smi) => smi.mode).ScheduleGoTo(10f, this.finish)
				.OnAnimQueueComplete(this.finish);
			this.finish.ReturnSuccess();
		}

		// Token: 0x0400543A RID: 21562
		public StateMachine<EmoteChore.States, EmoteChore.StatesInstance, EmoteChore, object>.TargetParameter emoter;

		// Token: 0x0400543B RID: 21563
		public GameStateMachine<EmoteChore.States, EmoteChore.StatesInstance, EmoteChore, object>.State finish;
	}
}
