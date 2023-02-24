using System;
using UnityEngine;

// Token: 0x02000391 RID: 913
public class ReactEmoteChore : Chore<ReactEmoteChore.StatesInstance>
{
	// Token: 0x06001286 RID: 4742 RVA: 0x00063204 File Offset: 0x00061404
	public ReactEmoteChore(IStateMachineTarget target, ChoreType chore_type, EmoteReactable reactable, HashedString emote_kanim, HashedString[] emote_anims, KAnim.PlayMode play_mode, Func<StatusItem> get_status_item)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.AddPrecondition(ChorePreconditions.instance.IsMoving, null);
		base.AddPrecondition(ChorePreconditions.instance.IsOffLadder, null);
		base.AddPrecondition(ChorePreconditions.instance.NotInTube, null);
		base.AddPrecondition(ChorePreconditions.instance.IsAwake, null);
		this.getStatusItem = get_status_item;
		base.smi = new ReactEmoteChore.StatesInstance(this, target.gameObject, reactable, emote_kanim, emote_anims, play_mode);
	}

	// Token: 0x06001287 RID: 4743 RVA: 0x00063290 File Offset: 0x00061490
	protected override StatusItem GetStatusItem()
	{
		if (this.getStatusItem == null)
		{
			return base.GetStatusItem();
		}
		return this.getStatusItem();
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x000632AC File Offset: 0x000614AC
	public override string ToString()
	{
		HashedString hashedString;
		if (base.smi.emoteKAnim.IsValid)
		{
			string text = "ReactEmoteChore<";
			hashedString = base.smi.emoteKAnim;
			return text + hashedString.ToString() + ">";
		}
		string text2 = "ReactEmoteChore<";
		hashedString = base.smi.emoteAnims[0];
		return text2 + hashedString.ToString() + ">";
	}

	// Token: 0x04000A02 RID: 2562
	private Func<StatusItem> getStatusItem;

	// Token: 0x02000F87 RID: 3975
	public class StatesInstance : GameStateMachine<ReactEmoteChore.States, ReactEmoteChore.StatesInstance, ReactEmoteChore, object>.GameInstance
	{
		// Token: 0x06006FC2 RID: 28610 RVA: 0x002A3AF0 File Offset: 0x002A1CF0
		public StatesInstance(ReactEmoteChore master, GameObject emoter, EmoteReactable reactable, HashedString emote_kanim, HashedString[] emote_anims, KAnim.PlayMode mode)
			: base(master)
		{
			this.emoteKAnim = emote_kanim;
			this.emoteAnims = emote_anims;
			this.mode = mode;
			base.sm.reactable.Set(reactable, base.smi, false);
			base.sm.emoter.Set(emoter, base.smi, false);
		}

		// Token: 0x040054C1 RID: 21697
		public HashedString[] emoteAnims;

		// Token: 0x040054C2 RID: 21698
		public HashedString emoteKAnim;

		// Token: 0x040054C3 RID: 21699
		public KAnim.PlayMode mode = KAnim.PlayMode.Once;
	}

	// Token: 0x02000F88 RID: 3976
	public class States : GameStateMachine<ReactEmoteChore.States, ReactEmoteChore.StatesInstance, ReactEmoteChore>
	{
		// Token: 0x06006FC3 RID: 28611 RVA: 0x002A3B58 File Offset: 0x002A1D58
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			base.Target(this.emoter);
			this.root.ToggleThought((ReactEmoteChore.StatesInstance smi) => this.reactable.Get(smi).thought).ToggleExpression((ReactEmoteChore.StatesInstance smi) => this.reactable.Get(smi).expression).ToggleAnims((ReactEmoteChore.StatesInstance smi) => smi.emoteKAnim)
				.ToggleThought(Db.Get().Thoughts.Unhappy, null)
				.PlayAnims((ReactEmoteChore.StatesInstance smi) => smi.emoteAnims, (ReactEmoteChore.StatesInstance smi) => smi.mode)
				.OnAnimQueueComplete(null)
				.Enter(delegate(ReactEmoteChore.StatesInstance smi)
				{
					smi.master.GetComponent<Facing>().Face(Grid.CellToPos(this.reactable.Get(smi).sourceCell));
				});
		}

		// Token: 0x040054C4 RID: 21700
		public StateMachine<ReactEmoteChore.States, ReactEmoteChore.StatesInstance, ReactEmoteChore, object>.TargetParameter emoter;

		// Token: 0x040054C5 RID: 21701
		public StateMachine<ReactEmoteChore.States, ReactEmoteChore.StatesInstance, ReactEmoteChore, object>.ObjectParameter<EmoteReactable> reactable;
	}
}
