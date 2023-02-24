using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x020003DE RID: 990
public class ReactionMonitor : GameStateMachine<ReactionMonitor, ReactionMonitor.Instance, IStateMachineTarget, ReactionMonitor.Def>
{
	// Token: 0x06001489 RID: 5257 RVA: 0x0006C624 File Offset: 0x0006A824
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		base.serializable = StateMachine.SerializeType.Never;
		this.root.EventHandler(GameHashes.DestinationReached, delegate(ReactionMonitor.Instance smi)
		{
			smi.ClearLastReaction();
		}).EventHandler(GameHashes.NavigationFailed, delegate(ReactionMonitor.Instance smi)
		{
			smi.ClearLastReaction();
		});
		this.idle.Enter("ClearReactable", delegate(ReactionMonitor.Instance smi)
		{
			this.reactable.Set(null, smi, false);
		}).TagTransition(GameTags.Dead, this.dead, false);
		this.reacting.Enter("Reactable.Begin", delegate(ReactionMonitor.Instance smi)
		{
			this.reactable.Get(smi).Begin(smi.gameObject);
		}).Enter(delegate(ReactionMonitor.Instance smi)
		{
			smi.master.Trigger(-909573545, null);
		}).Enter("Reactable.AddChorePreventionTag", delegate(ReactionMonitor.Instance smi)
		{
			if (this.reactable.Get(smi).preventChoreInterruption)
			{
				smi.GetComponent<KPrefabID>().AddTag(GameTags.PreventChoreInterruption, false);
			}
		})
			.Update("Reactable.Update", delegate(ReactionMonitor.Instance smi, float dt)
			{
				this.reactable.Get(smi).Update(dt);
			}, UpdateRate.SIM_200ms, false)
			.Exit(delegate(ReactionMonitor.Instance smi)
			{
				smi.master.Trigger(824899998, null);
			})
			.Exit("Reactable.End", delegate(ReactionMonitor.Instance smi)
			{
				this.reactable.Get(smi).End();
			})
			.Exit("Reactable.RemoveChorePreventionTag", delegate(ReactionMonitor.Instance smi)
			{
				if (this.reactable.Get(smi).preventChoreInterruption)
				{
					smi.GetComponent<KPrefabID>().RemoveTag(GameTags.PreventChoreInterruption);
				}
			})
			.EventTransition(GameHashes.NavigationFailed, this.idle, null)
			.TagTransition(GameTags.Dying, this.dead, false)
			.TagTransition(GameTags.Dead, this.dead, false);
		this.dead.DoNothing();
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x0006C7C5 File Offset: 0x0006A9C5
	private static bool ShouldReact(ReactionMonitor.Instance smi)
	{
		return smi.ImmediateReactable != null;
	}

	// Token: 0x04000B8B RID: 2955
	public GameStateMachine<ReactionMonitor, ReactionMonitor.Instance, IStateMachineTarget, ReactionMonitor.Def>.State idle;

	// Token: 0x04000B8C RID: 2956
	public GameStateMachine<ReactionMonitor, ReactionMonitor.Instance, IStateMachineTarget, ReactionMonitor.Def>.State reacting;

	// Token: 0x04000B8D RID: 2957
	public GameStateMachine<ReactionMonitor, ReactionMonitor.Instance, IStateMachineTarget, ReactionMonitor.Def>.State dead;

	// Token: 0x04000B8E RID: 2958
	public StateMachine<ReactionMonitor, ReactionMonitor.Instance, IStateMachineTarget, ReactionMonitor.Def>.ObjectParameter<Reactable> reactable;

	// Token: 0x02000FFC RID: 4092
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005623 RID: 22051
		public ObjectLayer ReactionLayer;
	}

	// Token: 0x02000FFD RID: 4093
	public new class Instance : GameStateMachine<ReactionMonitor, ReactionMonitor.Instance, IStateMachineTarget, ReactionMonitor.Def>.GameInstance
	{
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06007117 RID: 28951 RVA: 0x002A85A1 File Offset: 0x002A67A1
		// (set) Token: 0x06007118 RID: 28952 RVA: 0x002A85A9 File Offset: 0x002A67A9
		public Reactable ImmediateReactable { get; private set; }

		// Token: 0x06007119 RID: 28953 RVA: 0x002A85B2 File Offset: 0x002A67B2
		public Instance(IStateMachineTarget master, ReactionMonitor.Def def)
			: base(master, def)
		{
			this.animController = base.GetComponent<KBatchedAnimController>();
			this.lastReactTimes = new Dictionary<HashedString, float>();
			this.oneshotReactables = new List<Reactable>();
		}

		// Token: 0x0600711A RID: 28954 RVA: 0x002A85E9 File Offset: 0x002A67E9
		public bool CanReact(Emote e)
		{
			return this.animController != null && e.IsValidForController(this.animController);
		}

		// Token: 0x0600711B RID: 28955 RVA: 0x002A8608 File Offset: 0x002A6808
		public bool TryReact(Reactable reactable, float clockTime, Navigator.ActiveTransition transition = null)
		{
			if (reactable == null)
			{
				return false;
			}
			float num;
			if ((this.lastReactTimes.TryGetValue(reactable.id, out num) && num == this.lastReaction) || clockTime - num < reactable.localCooldown)
			{
				return false;
			}
			if (!reactable.CanBegin(base.gameObject, transition))
			{
				return false;
			}
			this.lastReactTimes[reactable.id] = clockTime;
			base.sm.reactable.Set(reactable, base.smi, false);
			base.smi.GoTo(base.sm.reacting);
			return true;
		}

		// Token: 0x0600711C RID: 28956 RVA: 0x002A8698 File Offset: 0x002A6898
		public void PollForReactables(Navigator.ActiveTransition transition)
		{
			if (this.IsReacting())
			{
				return;
			}
			for (int i = this.oneshotReactables.Count - 1; i >= 0; i--)
			{
				Reactable reactable = this.oneshotReactables[i];
				if (reactable.IsExpired())
				{
					reactable.Cleanup();
					this.oneshotReactables.RemoveAt(i);
				}
			}
			Vector2I vector2I = Grid.CellToXY(Grid.PosToCell(base.smi.gameObject));
			ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[(int)base.def.ReactionLayer];
			ListPool<ScenePartitionerEntry, ReactionMonitor>.PooledList pooledList = ListPool<ScenePartitionerEntry, ReactionMonitor>.Allocate();
			GameScenePartitioner.Instance.GatherEntries(vector2I.x, vector2I.y, 1, 1, scenePartitionerLayer, pooledList);
			float num = float.NaN;
			float time = GameClock.Instance.GetTime();
			for (int j = 0; j < pooledList.Count; j++)
			{
				Reactable reactable2 = pooledList[j].obj as Reactable;
				if (this.TryReact(reactable2, time, transition))
				{
					num = time;
					break;
				}
			}
			this.lastReaction = num;
			pooledList.Recycle();
		}

		// Token: 0x0600711D RID: 28957 RVA: 0x002A879D File Offset: 0x002A699D
		public void ClearLastReaction()
		{
			this.lastReaction = float.NaN;
		}

		// Token: 0x0600711E RID: 28958 RVA: 0x002A87AC File Offset: 0x002A69AC
		public void StopReaction()
		{
			for (int i = this.oneshotReactables.Count - 1; i >= 0; i--)
			{
				if (base.sm.reactable.Get(base.smi) == this.oneshotReactables[i])
				{
					this.oneshotReactables[i].Cleanup();
					this.oneshotReactables.RemoveAt(i);
					break;
				}
			}
			base.smi.GoTo(base.sm.idle);
		}

		// Token: 0x0600711F RID: 28959 RVA: 0x002A882A File Offset: 0x002A6A2A
		public bool IsReacting()
		{
			return base.smi.IsInsideState(base.sm.reacting);
		}

		// Token: 0x06007120 RID: 28960 RVA: 0x002A8844 File Offset: 0x002A6A44
		public SelfEmoteReactable AddSelfEmoteReactable(GameObject target, HashedString reactionId, Emote emote, bool isOneShot, ChoreType choreType, float globalCooldown = 0f, float localCooldown = 20f, float lifeSpan = float.NegativeInfinity, float maxInitialDelay = 0f, List<Reactable.ReactablePrecondition> emotePreconditions = null)
		{
			if (!this.CanReact(emote))
			{
				return null;
			}
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(target, reactionId, choreType, globalCooldown, localCooldown, lifeSpan, maxInitialDelay);
			selfEmoteReactable.SetEmote(emote);
			int num = 0;
			while (emotePreconditions != null && num < emotePreconditions.Count)
			{
				selfEmoteReactable.AddPrecondition(emotePreconditions[num]);
				num++;
			}
			if (isOneShot)
			{
				this.AddOneshotReactable(selfEmoteReactable);
			}
			return selfEmoteReactable;
		}

		// Token: 0x06007121 RID: 28961 RVA: 0x002A88A8 File Offset: 0x002A6AA8
		public SelfEmoteReactable AddSelfEmoteReactable(GameObject target, string reactionId, string emoteAnim, bool isOneShot, ChoreType choreType, float globalCooldown = 0f, float localCooldown = 20f, float maxTriggerTime = float.NegativeInfinity, float maxInitialDelay = 0f, List<Reactable.ReactablePrecondition> emotePreconditions = null)
		{
			Emote emote = new Emote(null, reactionId, new EmoteStep[]
			{
				new EmoteStep
				{
					anim = "react"
				}
			}, emoteAnim);
			return this.AddSelfEmoteReactable(target, reactionId, emote, isOneShot, choreType, globalCooldown, localCooldown, maxTriggerTime, maxInitialDelay, emotePreconditions);
		}

		// Token: 0x06007122 RID: 28962 RVA: 0x002A88F8 File Offset: 0x002A6AF8
		public void AddOneshotReactable(SelfEmoteReactable reactable)
		{
			if (reactable == null)
			{
				return;
			}
			this.oneshotReactables.Add(reactable);
		}

		// Token: 0x06007123 RID: 28963 RVA: 0x002A890C File Offset: 0x002A6B0C
		public void CancelOneShotReactable(SelfEmoteReactable cancel_target)
		{
			for (int i = this.oneshotReactables.Count - 1; i >= 0; i--)
			{
				Reactable reactable = this.oneshotReactables[i];
				if (cancel_target == reactable)
				{
					reactable.Cleanup();
					this.oneshotReactables.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06007124 RID: 28964 RVA: 0x002A8958 File Offset: 0x002A6B58
		public void CancelOneShotReactables(Emote reactionEmote)
		{
			for (int i = this.oneshotReactables.Count - 1; i >= 0; i--)
			{
				EmoteReactable emoteReactable = this.oneshotReactables[i] as EmoteReactable;
				if (emoteReactable != null && emoteReactable.emote == reactionEmote)
				{
					emoteReactable.Cleanup();
					this.oneshotReactables.RemoveAt(i);
				}
			}
		}

		// Token: 0x04005625 RID: 22053
		private KBatchedAnimController animController;

		// Token: 0x04005626 RID: 22054
		private float lastReaction = float.NaN;

		// Token: 0x04005627 RID: 22055
		private Dictionary<HashedString, float> lastReactTimes;

		// Token: 0x04005628 RID: 22056
		private List<Reactable> oneshotReactables;
	}
}
