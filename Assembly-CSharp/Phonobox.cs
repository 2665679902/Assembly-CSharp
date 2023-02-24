using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000889 RID: 2185
[SerializationConfig(MemberSerialization.OptIn)]
public class Phonobox : StateMachineComponent<Phonobox.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06003EA8 RID: 16040 RVA: 0x0015E4B4 File Offset: 0x0015C6B4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		GameScheduler.Instance.Schedule("Scheduling Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, true);
		}, null, null);
		this.workables = new PhonoboxWorkable[this.choreOffsets.Length];
		this.chores = new Chore[this.choreOffsets.Length];
		for (int i = 0; i < this.workables.Length; i++)
		{
			Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(this), this.choreOffsets[i]), Grid.SceneLayer.Move);
			GameObject gameObject = ChoreHelpers.CreateLocator("PhonoboxWorkable", vector);
			KSelectable kselectable = gameObject.AddOrGet<KSelectable>();
			kselectable.SetName(this.GetProperName());
			kselectable.IsSelectable = false;
			PhonoboxWorkable phonoboxWorkable = gameObject.AddOrGet<PhonoboxWorkable>();
			phonoboxWorkable.owner = this;
			this.workables[i] = phonoboxWorkable;
		}
	}

	// Token: 0x06003EA9 RID: 16041 RVA: 0x0015E59C File Offset: 0x0015C79C
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

	// Token: 0x06003EAA RID: 16042 RVA: 0x0015E5F0 File Offset: 0x0015C7F0
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
		WorkChore<PhonoboxWorkable> workChore = new WorkChore<PhonoboxWorkable>(relax, stateMachineTarget, choreProvider, flag, action, action2, new Action<Chore>(this.OnSocialChoreEnd), false, recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
		workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, workable);
		return workChore;
	}

	// Token: 0x06003EAB RID: 16043 RVA: 0x0015E658 File Offset: 0x0015C858
	private void OnSocialChoreEnd(Chore chore)
	{
		if (base.gameObject.HasTag(GameTags.Operational))
		{
			this.UpdateChores(true);
		}
	}

	// Token: 0x06003EAC RID: 16044 RVA: 0x0015E674 File Offset: 0x0015C874
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

	// Token: 0x06003EAD RID: 16045 RVA: 0x0015E6D3 File Offset: 0x0015C8D3
	public void AddWorker(Worker player)
	{
		this.players.Add(player);
		base.smi.sm.playerCount.Set(this.players.Count, base.smi, false);
	}

	// Token: 0x06003EAE RID: 16046 RVA: 0x0015E70A File Offset: 0x0015C90A
	public void RemoveWorker(Worker player)
	{
		this.players.Remove(player);
		base.smi.sm.playerCount.Set(this.players.Count, base.smi, false);
	}

	// Token: 0x06003EAF RID: 16047 RVA: 0x0015E744 File Offset: 0x0015C944
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		Effect.AddModifierDescriptions(base.gameObject, list, "Danced", true);
		return list;
	}

	// Token: 0x040028FF RID: 10495
	public const string SPECIFIC_EFFECT = "Danced";

	// Token: 0x04002900 RID: 10496
	public const string TRACKING_EFFECT = "RecentlyDanced";

	// Token: 0x04002901 RID: 10497
	public CellOffset[] choreOffsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(-1, 0),
		new CellOffset(1, 0),
		new CellOffset(-2, 0),
		new CellOffset(2, 0)
	};

	// Token: 0x04002902 RID: 10498
	private PhonoboxWorkable[] workables;

	// Token: 0x04002903 RID: 10499
	private Chore[] chores;

	// Token: 0x04002904 RID: 10500
	private HashSet<Worker> players = new HashSet<Worker>();

	// Token: 0x04002905 RID: 10501
	private static string[] building_anims = new string[] { "working_loop", "working_loop2", "working_loop3" };

	// Token: 0x02001642 RID: 5698
	public class States : GameStateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox>
	{
		// Token: 0x06008729 RID: 34601 RVA: 0x002F0EDC File Offset: 0x002EF0DC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.unoperational.Enter(delegate(Phonobox.StatesInstance smi)
			{
				smi.SetActive(false);
			}).TagTransition(GameTags.Operational, this.operational, false).PlayAnim("off");
			this.operational.TagTransition(GameTags.Operational, this.unoperational, true).Enter("CreateChore", delegate(Phonobox.StatesInstance smi)
			{
				smi.master.UpdateChores(true);
			}).Exit("CancelChore", delegate(Phonobox.StatesInstance smi)
			{
				smi.master.UpdateChores(false);
			})
				.DefaultState(this.operational.stopped);
			this.operational.stopped.Enter(delegate(Phonobox.StatesInstance smi)
			{
				smi.SetActive(false);
			}).ParamTransition<int>(this.playerCount, this.operational.pre, (Phonobox.StatesInstance smi, int p) => p > 0).PlayAnim("on");
			this.operational.pre.PlayAnim("working_pre").OnAnimQueueComplete(this.operational.playing);
			this.operational.playing.Enter(delegate(Phonobox.StatesInstance smi)
			{
				smi.SetActive(true);
			}).ScheduleGoTo(25f, this.operational.song_end).ParamTransition<int>(this.playerCount, this.operational.post, (Phonobox.StatesInstance smi, int p) => p == 0)
				.PlayAnim(new Func<Phonobox.StatesInstance, string>(Phonobox.States.GetPlayAnim), KAnim.PlayMode.Loop);
			this.operational.song_end.ParamTransition<int>(this.playerCount, this.operational.bridge, (Phonobox.StatesInstance smi, int p) => p > 0).ParamTransition<int>(this.playerCount, this.operational.post, (Phonobox.StatesInstance smi, int p) => p == 0);
			this.operational.bridge.PlayAnim("working_trans").OnAnimQueueComplete(this.operational.playing);
			this.operational.post.PlayAnim("working_pst").OnAnimQueueComplete(this.operational.stopped);
		}

		// Token: 0x0600872A RID: 34602 RVA: 0x002F1194 File Offset: 0x002EF394
		public static string GetPlayAnim(Phonobox.StatesInstance smi)
		{
			int num = UnityEngine.Random.Range(0, Phonobox.building_anims.Length);
			return Phonobox.building_anims[num];
		}

		// Token: 0x04006947 RID: 26951
		public StateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox, object>.IntParameter playerCount;

		// Token: 0x04006948 RID: 26952
		public GameStateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox, object>.State unoperational;

		// Token: 0x04006949 RID: 26953
		public Phonobox.States.OperationalStates operational;

		// Token: 0x02002085 RID: 8325
		public class OperationalStates : GameStateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox, object>.State
		{
			// Token: 0x040090E3 RID: 37091
			public GameStateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox, object>.State stopped;

			// Token: 0x040090E4 RID: 37092
			public GameStateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox, object>.State pre;

			// Token: 0x040090E5 RID: 37093
			public GameStateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox, object>.State bridge;

			// Token: 0x040090E6 RID: 37094
			public GameStateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox, object>.State playing;

			// Token: 0x040090E7 RID: 37095
			public GameStateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox, object>.State song_end;

			// Token: 0x040090E8 RID: 37096
			public GameStateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox, object>.State post;
		}
	}

	// Token: 0x02001643 RID: 5699
	public class StatesInstance : GameStateMachine<Phonobox.States, Phonobox.StatesInstance, Phonobox, object>.GameInstance
	{
		// Token: 0x0600872C RID: 34604 RVA: 0x002F11BE File Offset: 0x002EF3BE
		public StatesInstance(Phonobox smi)
			: base(smi)
		{
			this.operational = base.master.GetComponent<Operational>();
		}

		// Token: 0x0600872D RID: 34605 RVA: 0x002F11D8 File Offset: 0x002EF3D8
		public void SetActive(bool active)
		{
			this.operational.SetActive(this.operational.IsOperational && active, false);
		}

		// Token: 0x0400694A RID: 26954
		private FetchChore chore;

		// Token: 0x0400694B RID: 26955
		private Operational operational;
	}
}
