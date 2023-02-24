using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000892 RID: 2194
public class EvilFlower : StateMachineComponent<EvilFlower.StatesInstance>
{
	// Token: 0x06003EEF RID: 16111 RVA: 0x0015FB2C File Offset: 0x0015DD2C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<EvilFlower>(1309017699, EvilFlower.SetReplantedTrueDelegate);
	}

	// Token: 0x06003EF0 RID: 16112 RVA: 0x0015FB45 File Offset: 0x0015DD45
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003EF1 RID: 16113 RVA: 0x0015FB58 File Offset: 0x0015DD58
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x04002941 RID: 10561
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x04002942 RID: 10562
	[MyCmpReq]
	private EntombVulnerable entombVulnerable;

	// Token: 0x04002943 RID: 10563
	public bool replanted;

	// Token: 0x04002944 RID: 10564
	public EffectorValues positive_decor_effect = new EffectorValues
	{
		amount = 1,
		radius = 5
	};

	// Token: 0x04002945 RID: 10565
	public EffectorValues negative_decor_effect = new EffectorValues
	{
		amount = -1,
		radius = 5
	};

	// Token: 0x04002946 RID: 10566
	private static readonly EventSystem.IntraObjectHandler<EvilFlower> SetReplantedTrueDelegate = new EventSystem.IntraObjectHandler<EvilFlower>(delegate(EvilFlower component, object data)
	{
		component.replanted = true;
	});

	// Token: 0x02001653 RID: 5715
	public class StatesInstance : GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower, object>.GameInstance
	{
		// Token: 0x0600875B RID: 34651 RVA: 0x002F1FDB File Offset: 0x002F01DB
		public StatesInstance(EvilFlower smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001654 RID: 5716
	public class States : GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower>
	{
		// Token: 0x0600875C RID: 34652 RVA: 0x002F1FE4 File Offset: 0x002F01E4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.grow;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.dead.ToggleStatusItem(CREATURES.STATUSITEMS.DEAD.NAME, CREATURES.STATUSITEMS.DEAD.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).TriggerOnEnter(GameHashes.BurstEmitDisease, null).ToggleTag(GameTags.PreventEmittingDisease)
				.Enter(delegate(EvilFlower.StatesInstance smi)
				{
					GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
					smi.master.Trigger(1623392196, null);
					smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
					UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
					smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
				});
			this.blocked_from_growing.ToggleStatusItem(Db.Get().MiscStatusItems.RegionIsBlocked, null).EventTransition(GameHashes.EntombedChanged, this.alive, (EvilFlower.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject)).EventTransition(GameHashes.TooColdWarning, this.alive, (EvilFlower.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.EventTransition(GameHashes.TooHotWarning, this.alive, (EvilFlower.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.TagTransition(GameTags.Uprooted, this.dead, false);
			this.grow.Enter(delegate(EvilFlower.StatesInstance smi)
			{
				if (smi.master.replanted && !this.alive.ForceUpdateStatus(smi.master.gameObject))
				{
					smi.GoTo(this.blocked_from_growing);
				}
			}).PlayAnim("grow_seed", KAnim.PlayMode.Once).EventTransition(GameHashes.AnimQueueComplete, this.alive, null);
			this.alive.InitializeStates(this.masterTarget, this.dead).DefaultState(this.alive.idle).ToggleStatusItem(CREATURES.STATUSITEMS.IDLE.NAME, CREATURES.STATUSITEMS.IDLE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
			this.alive.idle.EventTransition(GameHashes.Wilt, this.alive.wilting, (EvilFlower.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).PlayAnim("idle", KAnim.PlayMode.Loop).Enter(delegate(EvilFlower.StatesInstance smi)
			{
				smi.master.GetComponent<DecorProvider>().SetValues(smi.master.positive_decor_effect);
				smi.master.GetComponent<DecorProvider>().Refresh();
				smi.master.AddTag(GameTags.Decoration);
			});
			this.alive.wilting.PlayAnim("wilt1", KAnim.PlayMode.Loop).EventTransition(GameHashes.WiltRecover, this.alive.idle, null).ToggleTag(GameTags.PreventEmittingDisease)
				.Enter(delegate(EvilFlower.StatesInstance smi)
				{
					smi.master.GetComponent<DecorProvider>().SetValues(smi.master.negative_decor_effect);
					smi.master.GetComponent<DecorProvider>().Refresh();
					smi.master.RemoveTag(GameTags.Decoration);
				});
		}

		// Token: 0x0400696B RID: 26987
		public GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower, object>.State grow;

		// Token: 0x0400696C RID: 26988
		public GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower, object>.State blocked_from_growing;

		// Token: 0x0400696D RID: 26989
		public EvilFlower.States.AliveStates alive;

		// Token: 0x0400696E RID: 26990
		public GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower, object>.State dead;

		// Token: 0x02002090 RID: 8336
		public class AliveStates : GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower, object>.PlantAliveSubState
		{
			// Token: 0x04009120 RID: 37152
			public GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower, object>.State idle;

			// Token: 0x04009121 RID: 37153
			public EvilFlower.States.WiltingState wilting;
		}

		// Token: 0x02002091 RID: 8337
		public class WiltingState : GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower, object>.State
		{
			// Token: 0x04009122 RID: 37154
			public GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower, object>.State wilting_pre;

			// Token: 0x04009123 RID: 37155
			public GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower, object>.State wilting;

			// Token: 0x04009124 RID: 37156
			public GameStateMachine<EvilFlower.States, EvilFlower.StatesInstance, EvilFlower, object>.State wilting_pst;
		}
	}
}
