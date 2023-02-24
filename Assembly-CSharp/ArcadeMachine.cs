using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000549 RID: 1353
[SerializationConfig(MemberSerialization.OptIn)]
public class ArcadeMachine : StateMachineComponent<ArcadeMachine.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06002040 RID: 8256 RVA: 0x000B0448 File Offset: 0x000AE648
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("Scheduling Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, true);
		}, null, null);
		this.workables = new ArcadeMachineWorkable[this.choreOffsets.Length];
		this.chores = new Chore[this.choreOffsets.Length];
		for (int i = 0; i < this.workables.Length; i++)
		{
			Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(this), this.choreOffsets[i]), Grid.SceneLayer.Move);
			GameObject gameObject = ChoreHelpers.CreateLocator("ArcadeMachineWorkable", vector);
			ArcadeMachineWorkable arcadeMachineWorkable = gameObject.AddOrGet<ArcadeMachineWorkable>();
			KSelectable kselectable = gameObject.AddOrGet<KSelectable>();
			kselectable.SetName(this.GetProperName());
			kselectable.IsSelectable = false;
			int player_index = i;
			ArcadeMachineWorkable arcadeMachineWorkable2 = arcadeMachineWorkable;
			arcadeMachineWorkable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(arcadeMachineWorkable2.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(delegate(Workable workable, Workable.WorkableEvent ev)
			{
				this.OnWorkableEvent(player_index, ev);
			}));
			arcadeMachineWorkable.overrideAnims = this.overrideAnims[i];
			arcadeMachineWorkable.workAnims = this.workAnims[i];
			this.workables[i] = arcadeMachineWorkable;
			this.workables[i].owner = this;
		}
		base.smi.StartSM();
	}

	// Token: 0x06002041 RID: 8257 RVA: 0x000B058C File Offset: 0x000AE78C
	protected override void OnCleanUp()
	{
		this.UpdateChores(false);
		for (int i = 0; i < this.workables.Length; i++)
		{
			if (this.workables[i])
			{
				Util.KDestroyGameObject(this.workables[i]);
				this.workables[i] = null;
			}
		}
		base.OnCleanUp();
	}

	// Token: 0x06002042 RID: 8258 RVA: 0x000B05E0 File Offset: 0x000AE7E0
	private Chore CreateChore(int i)
	{
		Workable workable = this.workables[i];
		ChoreType relax = Db.Get().ChoreTypes.Relax;
		IStateMachineTarget stateMachineTarget = workable;
		ChoreProvider choreProvider = null;
		bool flag = true;
		Action<Chore> action = null;
		Action<Chore> action2 = null;
		ScheduleBlockType recreation = Db.Get().ScheduleBlockTypes.Recreation;
		WorkChore<ArcadeMachineWorkable> workChore = new WorkChore<ArcadeMachineWorkable>(relax, stateMachineTarget, choreProvider, flag, action, action2, new Action<Chore>(this.OnSocialChoreEnd), false, recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
		workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, workable);
		return workChore;
	}

	// Token: 0x06002043 RID: 8259 RVA: 0x000B0648 File Offset: 0x000AE848
	private void OnSocialChoreEnd(Chore chore)
	{
		if (base.gameObject.HasTag(GameTags.Operational))
		{
			this.UpdateChores(true);
		}
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x000B0664 File Offset: 0x000AE864
	public void UpdateChores(bool update = true)
	{
		for (int i = 0; i < this.choreOffsets.Length; i++)
		{
			Chore chore = this.chores[i];
			if (update)
			{
				if (chore == null || chore.isComplete)
				{
					this.chores[i] = this.CreateChore(i);
				}
			}
			else if (chore != null)
			{
				chore.Cancel("locator invalidated");
				this.chores[i] = null;
			}
		}
	}

	// Token: 0x06002045 RID: 8261 RVA: 0x000B06C4 File Offset: 0x000AE8C4
	public void OnWorkableEvent(int player, Workable.WorkableEvent ev)
	{
		if (ev == Workable.WorkableEvent.WorkStarted)
		{
			this.players.Add(player);
		}
		else
		{
			this.players.Remove(player);
		}
		base.smi.sm.playerCount.Set(this.players.Count, base.smi, false);
	}

	// Token: 0x06002046 RID: 8262 RVA: 0x000B071C File Offset: 0x000AE91C
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		Effect.AddModifierDescriptions(base.gameObject, list, "PlayedArcade", true);
		return list;
	}

	// Token: 0x0400128D RID: 4749
	public CellOffset[] choreOffsets = new CellOffset[]
	{
		new CellOffset(-1, 0),
		new CellOffset(1, 0)
	};

	// Token: 0x0400128E RID: 4750
	private ArcadeMachineWorkable[] workables;

	// Token: 0x0400128F RID: 4751
	private Chore[] chores;

	// Token: 0x04001290 RID: 4752
	public HashSet<int> players = new HashSet<int>();

	// Token: 0x04001291 RID: 4753
	public KAnimFile[][] overrideAnims = new KAnimFile[][]
	{
		new KAnimFile[] { Assets.GetAnim("anim_interacts_arcade_cabinet_playerone_kanim") },
		new KAnimFile[] { Assets.GetAnim("anim_interacts_arcade_cabinet_playertwo_kanim") }
	};

	// Token: 0x04001292 RID: 4754
	public HashedString[][] workAnims = new HashedString[][]
	{
		new HashedString[] { "working_pre", "working_loop_one_p" },
		new HashedString[] { "working_pre", "working_loop_two_p" }
	};

	// Token: 0x02001176 RID: 4470
	public class States : GameStateMachine<ArcadeMachine.States, ArcadeMachine.StatesInstance, ArcadeMachine>
	{
		// Token: 0x060076A4 RID: 30372 RVA: 0x002B8730 File Offset: 0x002B6930
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.unoperational.Enter(delegate(ArcadeMachine.StatesInstance smi)
			{
				smi.SetActive(false);
			}).TagTransition(GameTags.Operational, this.operational, false).PlayAnim("off");
			this.operational.TagTransition(GameTags.Operational, this.unoperational, true).Enter("CreateChore", delegate(ArcadeMachine.StatesInstance smi)
			{
				smi.master.UpdateChores(true);
			}).Exit("CancelChore", delegate(ArcadeMachine.StatesInstance smi)
			{
				smi.master.UpdateChores(false);
			})
				.DefaultState(this.operational.stopped);
			this.operational.stopped.Enter(delegate(ArcadeMachine.StatesInstance smi)
			{
				smi.SetActive(false);
			}).PlayAnim("on").ParamTransition<int>(this.playerCount, this.operational.pre, (ArcadeMachine.StatesInstance smi, int p) => p > 0);
			this.operational.pre.Enter(delegate(ArcadeMachine.StatesInstance smi)
			{
				smi.SetActive(true);
			}).PlayAnim("working_pre").OnAnimQueueComplete(this.operational.playing);
			this.operational.playing.PlayAnim(new Func<ArcadeMachine.StatesInstance, string>(this.GetPlayingAnim), KAnim.PlayMode.Loop).ParamTransition<int>(this.playerCount, this.operational.post, (ArcadeMachine.StatesInstance smi, int p) => p == 0).ParamTransition<int>(this.playerCount, this.operational.playing_coop, (ArcadeMachine.StatesInstance smi, int p) => p > 1);
			this.operational.playing_coop.PlayAnim(new Func<ArcadeMachine.StatesInstance, string>(this.GetPlayingAnim), KAnim.PlayMode.Loop).ParamTransition<int>(this.playerCount, this.operational.post, (ArcadeMachine.StatesInstance smi, int p) => p == 0).ParamTransition<int>(this.playerCount, this.operational.playing, (ArcadeMachine.StatesInstance smi, int p) => p == 1);
			this.operational.post.PlayAnim("working_pst").OnAnimQueueComplete(this.operational.stopped);
		}

		// Token: 0x060076A5 RID: 30373 RVA: 0x002B89F4 File Offset: 0x002B6BF4
		private string GetPlayingAnim(ArcadeMachine.StatesInstance smi)
		{
			bool flag = smi.master.players.Contains(0);
			bool flag2 = smi.master.players.Contains(1);
			if (flag && !flag2)
			{
				return "working_loop_one_p";
			}
			if (flag2 && !flag)
			{
				return "working_loop_two_p";
			}
			return "working_loop_coop_p";
		}

		// Token: 0x04005ADC RID: 23260
		public StateMachine<ArcadeMachine.States, ArcadeMachine.StatesInstance, ArcadeMachine, object>.IntParameter playerCount;

		// Token: 0x04005ADD RID: 23261
		public GameStateMachine<ArcadeMachine.States, ArcadeMachine.StatesInstance, ArcadeMachine, object>.State unoperational;

		// Token: 0x04005ADE RID: 23262
		public ArcadeMachine.States.OperationalStates operational;

		// Token: 0x02001F83 RID: 8067
		public class OperationalStates : GameStateMachine<ArcadeMachine.States, ArcadeMachine.StatesInstance, ArcadeMachine, object>.State
		{
			// Token: 0x04008C0C RID: 35852
			public GameStateMachine<ArcadeMachine.States, ArcadeMachine.StatesInstance, ArcadeMachine, object>.State stopped;

			// Token: 0x04008C0D RID: 35853
			public GameStateMachine<ArcadeMachine.States, ArcadeMachine.StatesInstance, ArcadeMachine, object>.State pre;

			// Token: 0x04008C0E RID: 35854
			public GameStateMachine<ArcadeMachine.States, ArcadeMachine.StatesInstance, ArcadeMachine, object>.State playing;

			// Token: 0x04008C0F RID: 35855
			public GameStateMachine<ArcadeMachine.States, ArcadeMachine.StatesInstance, ArcadeMachine, object>.State playing_coop;

			// Token: 0x04008C10 RID: 35856
			public GameStateMachine<ArcadeMachine.States, ArcadeMachine.StatesInstance, ArcadeMachine, object>.State post;
		}
	}

	// Token: 0x02001177 RID: 4471
	public class StatesInstance : GameStateMachine<ArcadeMachine.States, ArcadeMachine.StatesInstance, ArcadeMachine, object>.GameInstance
	{
		// Token: 0x060076A7 RID: 30375 RVA: 0x002B8A4A File Offset: 0x002B6C4A
		public StatesInstance(ArcadeMachine smi)
			: base(smi)
		{
			this.operational = base.master.GetComponent<Operational>();
		}

		// Token: 0x060076A8 RID: 30376 RVA: 0x002B8A64 File Offset: 0x002B6C64
		public void SetActive(bool active)
		{
			this.operational.SetActive(this.operational.IsOperational && active, false);
		}

		// Token: 0x04005ADF RID: 23263
		private Operational operational;
	}
}
