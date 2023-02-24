using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003BA RID: 954
public class SuperProductive : GameStateMachine<SuperProductive, SuperProductive.Instance>
{
	// Token: 0x060013B8 RID: 5048 RVA: 0x0006868C File Offset: 0x0006688C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.TagTransition(GameTags.Overjoyed, this.overjoyed, false);
		this.overjoyed.TagTransition(GameTags.Overjoyed, this.neutral, true).ToggleStatusItem(Db.Get().DuplicantStatusItems.BeingProductive, null).Enter(delegate(SuperProductive.Instance smi)
		{
			if (PopFXManager.Instance != null)
			{
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, DUPLICANTS.TRAITS.SUPERPRODUCTIVE.NAME, smi.master.transform, new Vector3(0f, 0.5f, 0f), 1.5f, false, false);
			}
			smi.fx = new SuperProductiveFX.Instance(smi.GetComponent<KMonoBehaviour>(), new Vector3(0f, 0f, -0.1f));
			smi.fx.StartSM();
		})
			.Exit(delegate(SuperProductive.Instance smi)
			{
				smi.fx.sm.destroyFX.Trigger(smi.fx);
			})
			.DefaultState(this.overjoyed.idle);
		this.overjoyed.idle.EventTransition(GameHashes.StartWork, this.overjoyed.working, null);
		this.overjoyed.working.ScheduleGoTo(0.33f, this.overjoyed.superProductive);
		this.overjoyed.superProductive.Enter(delegate(SuperProductive.Instance smi)
		{
			Worker component = smi.GetComponent<Worker>();
			if (component != null && component.state == Worker.State.Working)
			{
				Workable workable = component.workable;
				if (workable != null)
				{
					float num = workable.WorkTimeRemaining;
					if (workable.GetComponent<Diggable>() != null)
					{
						num = Diggable.GetApproximateDigTime(Grid.PosToCell(workable));
					}
					if (num > 1f && smi.ShouldSkipWork() && component.InstantlyFinish())
					{
						smi.ReactSuperProductive();
						smi.fx.sm.wasProductive.Trigger(smi.fx);
					}
				}
			}
			smi.GoTo(this.overjoyed.idle);
		});
	}

	// Token: 0x04000AB8 RID: 2744
	public GameStateMachine<SuperProductive, SuperProductive.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x04000AB9 RID: 2745
	public SuperProductive.OverjoyedStates overjoyed;

	// Token: 0x02000FE2 RID: 4066
	public class OverjoyedStates : GameStateMachine<SuperProductive, SuperProductive.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040055B7 RID: 21943
		public GameStateMachine<SuperProductive, SuperProductive.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x040055B8 RID: 21944
		public GameStateMachine<SuperProductive, SuperProductive.Instance, IStateMachineTarget, object>.State working;

		// Token: 0x040055B9 RID: 21945
		public GameStateMachine<SuperProductive, SuperProductive.Instance, IStateMachineTarget, object>.State superProductive;
	}

	// Token: 0x02000FE3 RID: 4067
	public new class Instance : GameStateMachine<SuperProductive, SuperProductive.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060070CF RID: 28879 RVA: 0x002A78B3 File Offset: 0x002A5AB3
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x060070D0 RID: 28880 RVA: 0x002A78BC File Offset: 0x002A5ABC
		public bool ShouldSkipWork()
		{
			return UnityEngine.Random.Range(0f, 100f) <= TRAITS.JOY_REACTIONS.SUPER_PRODUCTIVE.INSTANT_SUCCESS_CHANCE;
		}

		// Token: 0x060070D1 RID: 28881 RVA: 0x002A78D8 File Offset: 0x002A5AD8
		public void ReactSuperProductive()
		{
			ReactionMonitor.Instance smi = base.gameObject.GetSMI<ReactionMonitor.Instance>();
			if (smi != null)
			{
				smi.AddSelfEmoteReactable(base.gameObject, "SuperProductive", Db.Get().Emotes.Minion.ProductiveCheer, true, Db.Get().ChoreTypes.EmoteHighPriority, 0f, 1f, 1f, 0f, null);
			}
		}

		// Token: 0x040055BA RID: 21946
		public SuperProductiveFX.Instance fx;
	}
}
