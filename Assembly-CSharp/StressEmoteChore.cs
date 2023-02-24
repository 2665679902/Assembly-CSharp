using System;
using UnityEngine;

// Token: 0x02000397 RID: 919
public class StressEmoteChore : Chore<StressEmoteChore.StatesInstance>
{
	// Token: 0x06001299 RID: 4761 RVA: 0x00063920 File Offset: 0x00061B20
	public StressEmoteChore(IStateMachineTarget target, ChoreType chore_type, HashedString emote_kanim, HashedString[] emote_anims, KAnim.PlayMode play_mode, Func<StatusItem> get_status_item)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.AddPrecondition(ChorePreconditions.instance.IsMoving, null);
		base.AddPrecondition(ChorePreconditions.instance.IsOffLadder, null);
		base.AddPrecondition(ChorePreconditions.instance.NotInTube, null);
		base.AddPrecondition(ChorePreconditions.instance.IsAwake, null);
		this.getStatusItem = get_status_item;
		base.smi = new StressEmoteChore.StatesInstance(this, target.gameObject, emote_kanim, emote_anims, play_mode);
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x000639AA File Offset: 0x00061BAA
	protected override StatusItem GetStatusItem()
	{
		if (this.getStatusItem == null)
		{
			return base.GetStatusItem();
		}
		return this.getStatusItem();
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x000639C8 File Offset: 0x00061BC8
	public override string ToString()
	{
		HashedString hashedString;
		if (base.smi.emoteKAnim.IsValid)
		{
			string text = "StressEmoteChore<";
			hashedString = base.smi.emoteKAnim;
			return text + hashedString.ToString() + ">";
		}
		string text2 = "StressEmoteChore<";
		hashedString = base.smi.emoteAnims[0];
		return text2 + hashedString.ToString() + ">";
	}

	// Token: 0x04000A07 RID: 2567
	private Func<StatusItem> getStatusItem;

	// Token: 0x02000F96 RID: 3990
	public class StatesInstance : GameStateMachine<StressEmoteChore.States, StressEmoteChore.StatesInstance, StressEmoteChore, object>.GameInstance
	{
		// Token: 0x06006FFE RID: 28670 RVA: 0x002A52BB File Offset: 0x002A34BB
		public StatesInstance(StressEmoteChore master, GameObject emoter, HashedString emote_kanim, HashedString[] emote_anims, KAnim.PlayMode mode)
			: base(master)
		{
			this.emoteKAnim = emote_kanim;
			this.emoteAnims = emote_anims;
			this.mode = mode;
			base.sm.emoter.Set(emoter, base.smi, false);
		}

		// Token: 0x040054F0 RID: 21744
		public HashedString[] emoteAnims;

		// Token: 0x040054F1 RID: 21745
		public HashedString emoteKAnim;

		// Token: 0x040054F2 RID: 21746
		public KAnim.PlayMode mode = KAnim.PlayMode.Once;
	}

	// Token: 0x02000F97 RID: 3991
	public class States : GameStateMachine<StressEmoteChore.States, StressEmoteChore.StatesInstance, StressEmoteChore>
	{
		// Token: 0x06006FFF RID: 28671 RVA: 0x002A52FC File Offset: 0x002A34FC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			base.Target(this.emoter);
			this.root.ToggleAnims((StressEmoteChore.StatesInstance smi) => smi.emoteKAnim).ToggleThought(Db.Get().Thoughts.Unhappy, null).PlayAnims((StressEmoteChore.StatesInstance smi) => smi.emoteAnims, (StressEmoteChore.StatesInstance smi) => smi.mode)
				.OnAnimQueueComplete(null);
		}

		// Token: 0x040054F3 RID: 21747
		public StateMachine<StressEmoteChore.States, StressEmoteChore.StatesInstance, StressEmoteChore, object>.TargetParameter emoter;
	}
}
