using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000898 RID: 2200
public class PrickleGrass : StateMachineComponent<PrickleGrass.StatesInstance>
{
	// Token: 0x06003F17 RID: 16151 RVA: 0x001604AB File Offset: 0x0015E6AB
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<PrickleGrass>(1309017699, PrickleGrass.SetReplantedTrueDelegate);
	}

	// Token: 0x06003F18 RID: 16152 RVA: 0x001604C4 File Offset: 0x0015E6C4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003F19 RID: 16153 RVA: 0x001604D7 File Offset: 0x0015E6D7
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x04002964 RID: 10596
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x04002965 RID: 10597
	[MyCmpReq]
	private EntombVulnerable entombVulnerable;

	// Token: 0x04002966 RID: 10598
	public bool replanted;

	// Token: 0x04002967 RID: 10599
	public EffectorValues positive_decor_effect = new EffectorValues
	{
		amount = 1,
		radius = 5
	};

	// Token: 0x04002968 RID: 10600
	public EffectorValues negative_decor_effect = new EffectorValues
	{
		amount = -1,
		radius = 5
	};

	// Token: 0x04002969 RID: 10601
	private static readonly EventSystem.IntraObjectHandler<PrickleGrass> SetReplantedTrueDelegate = new EventSystem.IntraObjectHandler<PrickleGrass>(delegate(PrickleGrass component, object data)
	{
		component.replanted = true;
	});

	// Token: 0x02001662 RID: 5730
	public class StatesInstance : GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass, object>.GameInstance
	{
		// Token: 0x06008790 RID: 34704 RVA: 0x002F3067 File Offset: 0x002F1267
		public StatesInstance(PrickleGrass smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001663 RID: 5731
	public class States : GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass>
	{
		// Token: 0x06008791 RID: 34705 RVA: 0x002F3070 File Offset: 0x002F1270
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.grow;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.dead.ToggleStatusItem(CREATURES.STATUSITEMS.DEAD.NAME, CREATURES.STATUSITEMS.DEAD.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).ToggleTag(GameTags.PreventEmittingDisease).Enter(delegate(PrickleGrass.StatesInstance smi)
			{
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
			});
			this.blocked_from_growing.ToggleStatusItem(Db.Get().MiscStatusItems.RegionIsBlocked, null).EventTransition(GameHashes.EntombedChanged, this.alive, (PrickleGrass.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject)).EventTransition(GameHashes.TooColdWarning, this.alive, (PrickleGrass.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.EventTransition(GameHashes.TooHotWarning, this.alive, (PrickleGrass.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.EventTransition(GameHashes.AreaElementSafeChanged, this.alive, (PrickleGrass.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.TagTransition(GameTags.Uprooted, this.dead, false);
			this.grow.Enter(delegate(PrickleGrass.StatesInstance smi)
			{
				if (smi.master.replanted && !this.alive.ForceUpdateStatus(smi.master.gameObject))
				{
					smi.GoTo(this.blocked_from_growing);
				}
			}).PlayAnim("grow_seed", KAnim.PlayMode.Once).EventTransition(GameHashes.AnimQueueComplete, this.alive, null);
			this.alive.InitializeStates(this.masterTarget, this.dead).DefaultState(this.alive.idle).ToggleStatusItem(CREATURES.STATUSITEMS.IDLE.NAME, CREATURES.STATUSITEMS.IDLE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
			this.alive.idle.EventTransition(GameHashes.Wilt, this.alive.wilting, (PrickleGrass.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).PlayAnim("idle", KAnim.PlayMode.Loop).Enter(delegate(PrickleGrass.StatesInstance smi)
			{
				smi.master.GetComponent<DecorProvider>().SetValues(smi.master.positive_decor_effect);
				smi.master.GetComponent<DecorProvider>().Refresh();
				smi.master.AddTag(GameTags.Decoration);
			});
			this.alive.wilting.PlayAnim("wilt1", KAnim.PlayMode.Loop).EventTransition(GameHashes.WiltRecover, this.alive.idle, null).ToggleTag(GameTags.PreventEmittingDisease)
				.Enter(delegate(PrickleGrass.StatesInstance smi)
				{
					smi.master.GetComponent<DecorProvider>().SetValues(smi.master.negative_decor_effect);
					smi.master.GetComponent<DecorProvider>().Refresh();
					smi.master.RemoveTag(GameTags.Decoration);
				});
		}

		// Token: 0x04006987 RID: 27015
		public GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass, object>.State grow;

		// Token: 0x04006988 RID: 27016
		public GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass, object>.State blocked_from_growing;

		// Token: 0x04006989 RID: 27017
		public PrickleGrass.States.AliveStates alive;

		// Token: 0x0400698A RID: 27018
		public GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass, object>.State dead;

		// Token: 0x0200209B RID: 8347
		public class AliveStates : GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass, object>.PlantAliveSubState
		{
			// Token: 0x0400914E RID: 37198
			public GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass, object>.State idle;

			// Token: 0x0400914F RID: 37199
			public PrickleGrass.States.WiltingState wilting;
		}

		// Token: 0x0200209C RID: 8348
		public class WiltingState : GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass, object>.State
		{
			// Token: 0x04009150 RID: 37200
			public GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass, object>.State wilting_pre;

			// Token: 0x04009151 RID: 37201
			public GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass, object>.State wilting;

			// Token: 0x04009152 RID: 37202
			public GameStateMachine<PrickleGrass.States, PrickleGrass.StatesInstance, PrickleGrass, object>.State wilting_pst;
		}
	}
}
