using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000894 RID: 2196
public class JungleGasPlant : StateMachineComponent<JungleGasPlant.StatesInstance>
{
	// Token: 0x06003EF7 RID: 16119 RVA: 0x0015FDD2 File Offset: 0x0015DFD2
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003EF8 RID: 16120 RVA: 0x0015FDE5 File Offset: 0x0015DFE5
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x0400294F RID: 10575
	[MyCmpReq]
	private ReceptacleMonitor rm;

	// Token: 0x04002950 RID: 10576
	[MyCmpReq]
	private Growing growing;

	// Token: 0x04002951 RID: 10577
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x04002952 RID: 10578
	[MyCmpReq]
	private ElementEmitter elementEmitter;

	// Token: 0x02001656 RID: 5718
	public class StatesInstance : GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.GameInstance
	{
		// Token: 0x06008765 RID: 34661 RVA: 0x002F2307 File Offset: 0x002F0507
		public StatesInstance(JungleGasPlant master)
			: base(master)
		{
		}
	}

	// Token: 0x02001657 RID: 5719
	public class States : GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant>
	{
		// Token: 0x06008766 RID: 34662 RVA: 0x002F2310 File Offset: 0x002F0510
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.alive.seed_grow;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.Enter(delegate(JungleGasPlant.StatesInstance smi)
			{
				if (smi.master.rm.Replanted && !this.alive.ForceUpdateStatus(smi.master.gameObject))
				{
					smi.GoTo(this.blocked_from_growing);
					return;
				}
				smi.GoTo(this.alive.seed_grow);
			});
			this.dead.ToggleStatusItem(CREATURES.STATUSITEMS.DEAD.NAME, CREATURES.STATUSITEMS.DEAD.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).Enter(delegate(JungleGasPlant.StatesInstance smi)
			{
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
			});
			this.blocked_from_growing.ToggleStatusItem(Db.Get().MiscStatusItems.RegionIsBlocked, null).TagTransition(GameTags.Entombed, this.alive.seed_grow, true).EventTransition(GameHashes.TooColdWarning, this.alive.seed_grow, null)
				.EventTransition(GameHashes.TooHotWarning, this.alive.seed_grow, null)
				.TagTransition(GameTags.Uprooted, this.dead, false);
			this.alive.InitializeStates(this.masterTarget, this.dead);
			this.alive.seed_grow.QueueAnim("seed_grow", false, null).EventTransition(GameHashes.AnimQueueComplete, this.alive.idle, null).EventTransition(GameHashes.Wilt, this.alive.wilting, (JungleGasPlant.StatesInstance smi) => smi.master.wiltCondition.IsWilting());
			this.alive.idle.EventTransition(GameHashes.Wilt, this.alive.wilting, (JungleGasPlant.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).EventTransition(GameHashes.Grow, this.alive.grown, (JungleGasPlant.StatesInstance smi) => smi.master.growing.IsGrown()).PlayAnim("idle_loop", KAnim.PlayMode.Loop);
			this.alive.grown.DefaultState(this.alive.grown.pre).EventTransition(GameHashes.Wilt, this.alive.wilting, (JungleGasPlant.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).Enter(delegate(JungleGasPlant.StatesInstance smi)
			{
				smi.master.elementEmitter.SetEmitting(true);
			})
				.Exit(delegate(JungleGasPlant.StatesInstance smi)
				{
					smi.master.elementEmitter.SetEmitting(false);
				});
			this.alive.grown.pre.PlayAnim("grow", KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.grown.idle);
			this.alive.grown.idle.PlayAnim("idle_bloom_loop", KAnim.PlayMode.Loop);
			this.alive.wilting.pre.DefaultState(this.alive.wilting.pre).PlayAnim("wilt_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.wilting.idle)
				.EventTransition(GameHashes.WiltRecover, this.alive.wilting.pst, (JungleGasPlant.StatesInstance smi) => !smi.master.wiltCondition.IsWilting());
			this.alive.wilting.idle.PlayAnim("idle_wilt_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.WiltRecover, this.alive.wilting.pst, (JungleGasPlant.StatesInstance smi) => !smi.master.wiltCondition.IsWilting());
			this.alive.wilting.pst.PlayAnim("wilt_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.idle);
		}

		// Token: 0x04006970 RID: 26992
		public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State blocked_from_growing;

		// Token: 0x04006971 RID: 26993
		public JungleGasPlant.States.AliveStates alive;

		// Token: 0x04006972 RID: 26994
		public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State dead;

		// Token: 0x02002093 RID: 8339
		public class AliveStates : GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.PlantAliveSubState
		{
			// Token: 0x0400912A RID: 37162
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State seed_grow;

			// Token: 0x0400912B RID: 37163
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State idle;

			// Token: 0x0400912C RID: 37164
			public JungleGasPlant.States.WiltingState wilting;

			// Token: 0x0400912D RID: 37165
			public JungleGasPlant.States.GrownState grown;

			// Token: 0x0400912E RID: 37166
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State destroy;
		}

		// Token: 0x02002094 RID: 8340
		public class GrownState : GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State
		{
			// Token: 0x0400912F RID: 37167
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State pre;

			// Token: 0x04009130 RID: 37168
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State idle;
		}

		// Token: 0x02002095 RID: 8341
		public class WiltingState : GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State
		{
			// Token: 0x04009131 RID: 37169
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State pre;

			// Token: 0x04009132 RID: 37170
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State idle;

			// Token: 0x04009133 RID: 37171
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State pst;
		}
	}
}
