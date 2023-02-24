using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class HugEggStates : GameStateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>
{
	// Token: 0x06000375 RID: 885 RVA: 0x0001B108 File Offset: 0x00019308
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.moving;
		this.root.Enter(new StateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.State.Callback(HugEggStates.SetTarget)).Enter(delegate(HugEggStates.Instance smi)
		{
			if (!HugEggStates.Reserve(smi))
			{
				smi.GoTo(this.behaviourcomplete);
			}
		}).Exit(new StateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.State.Callback(HugEggStates.Unreserve))
			.ToggleStatusItem(CREATURES.STATUSITEMS.HUGEGG.NAME, CREATURES.STATUSITEMS.HUGEGG.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main)
			.OnTargetLost(this.target, this.behaviourcomplete);
		this.moving.MoveTo(new Func<HugEggStates.Instance, int>(HugEggStates.GetClimbableCell), this.hug, this.behaviourcomplete, false);
		this.hug.DefaultState(this.hug.pre).Enter(delegate(HugEggStates.Instance smi)
		{
			smi.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.Front);
		}).Exit(delegate(HugEggStates.Instance smi)
		{
			smi.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.Creatures);
		});
		this.hug.pre.Face(this.target, 0.5f).PlayAnim((HugEggStates.Instance smi) => HugEggStates.GetAnims(smi).pre, KAnim.PlayMode.Once).OnAnimQueueComplete(this.hug.loop);
		this.hug.loop.QueueAnim((HugEggStates.Instance smi) => HugEggStates.GetAnims(smi).loop, true, null).ScheduleGoTo((HugEggStates.Instance smi) => smi.def.hugTime, this.hug.pst);
		this.hug.pst.QueueAnim((HugEggStates.Instance smi) => HugEggStates.GetAnims(smi).pst, false, null).Enter(new StateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.State.Callback(HugEggStates.ApplyEffect)).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete((HugEggStates.Instance smi) => smi.def.behaviourTag, false);
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0001B358 File Offset: 0x00019558
	private static void SetTarget(HugEggStates.Instance smi)
	{
		smi.sm.target.Set(smi.GetSMI<HugMonitor.Instance>().hugTarget, smi, false);
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0001B378 File Offset: 0x00019578
	private static HugEggStates.AnimSet GetAnims(HugEggStates.Instance smi)
	{
		if (!(smi.sm.target.Get(smi).GetComponent<EggIncubator>() != null))
		{
			return smi.def.hugAnims;
		}
		return smi.def.incubatorHugAnims;
	}

	// Token: 0x06000378 RID: 888 RVA: 0x0001B3B0 File Offset: 0x000195B0
	private static bool Reserve(HugEggStates.Instance smi)
	{
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null && !gameObject.HasTag(GameTags.Creatures.ReservedByCreature))
		{
			gameObject.AddTag(GameTags.Creatures.ReservedByCreature);
			return true;
		}
		return false;
	}

	// Token: 0x06000379 RID: 889 RVA: 0x0001B3F4 File Offset: 0x000195F4
	private static void Unreserve(HugEggStates.Instance smi)
	{
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null)
		{
			gameObject.RemoveTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x0600037A RID: 890 RVA: 0x0001B427 File Offset: 0x00019627
	private static int GetClimbableCell(HugEggStates.Instance smi)
	{
		return Grid.PosToCell(smi.sm.target.Get(smi));
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0001B440 File Offset: 0x00019640
	private static void ApplyEffect(HugEggStates.Instance smi)
	{
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null)
		{
			EggIncubator component = gameObject.GetComponent<EggIncubator>();
			if (component != null && component.Occupant != null)
			{
				component.Occupant.GetComponent<Effects>().Add("EggHug", true);
				return;
			}
			if (gameObject.HasTag(GameTags.Egg))
			{
				gameObject.GetComponent<Effects>().Add("EggHug", true);
			}
		}
	}

	// Token: 0x04000241 RID: 577
	public GameStateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.ApproachSubState<EggIncubator> moving;

	// Token: 0x04000242 RID: 578
	public HugEggStates.HugState hug;

	// Token: 0x04000243 RID: 579
	public GameStateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.State behaviourcomplete;

	// Token: 0x04000244 RID: 580
	public StateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.TargetParameter target;

	// Token: 0x02000E74 RID: 3700
	public class AnimSet
	{
		// Token: 0x0400517E RID: 20862
		public string pre;

		// Token: 0x0400517F RID: 20863
		public string loop;

		// Token: 0x04005180 RID: 20864
		public string pst;
	}

	// Token: 0x02000E75 RID: 3701
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06006C4C RID: 27724 RVA: 0x00297C34 File Offset: 0x00295E34
		public Def(Tag behaviourTag)
		{
			this.behaviourTag = behaviourTag;
		}

		// Token: 0x04005181 RID: 20865
		public float hugTime = 15f;

		// Token: 0x04005182 RID: 20866
		public Tag behaviourTag;

		// Token: 0x04005183 RID: 20867
		public HugEggStates.AnimSet hugAnims = new HugEggStates.AnimSet
		{
			pre = "hug_egg_pre",
			loop = "hug_egg_loop",
			pst = "hug_egg_pst"
		};

		// Token: 0x04005184 RID: 20868
		public HugEggStates.AnimSet incubatorHugAnims = new HugEggStates.AnimSet
		{
			pre = "hug_incubator_pre",
			loop = "hug_incubator_loop",
			pst = "hug_incubator_pst"
		};
	}

	// Token: 0x02000E76 RID: 3702
	public new class Instance : GameStateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.GameInstance
	{
		// Token: 0x06006C4D RID: 27725 RVA: 0x00297CB1 File Offset: 0x00295EB1
		public Instance(Chore<HugEggStates.Instance> chore, HugEggStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, def.behaviourTag);
		}
	}

	// Token: 0x02000E77 RID: 3703
	public class HugState : GameStateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.State
	{
		// Token: 0x04005185 RID: 20869
		public GameStateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.State pre;

		// Token: 0x04005186 RID: 20870
		public GameStateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.State loop;

		// Token: 0x04005187 RID: 20871
		public GameStateMachine<HugEggStates, HugEggStates.Instance, IStateMachineTarget, HugEggStates.Def>.State pst;
	}
}
