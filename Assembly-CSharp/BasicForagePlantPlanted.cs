using System;
using UnityEngine;

// Token: 0x0200088E RID: 2190
public class BasicForagePlantPlanted : StateMachineComponent<BasicForagePlantPlanted.StatesInstance>
{
	// Token: 0x06003EC2 RID: 16066 RVA: 0x0015EE15 File Offset: 0x0015D015
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003EC3 RID: 16067 RVA: 0x0015EE28 File Offset: 0x0015D028
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x04002915 RID: 10517
	[MyCmpReq]
	private Harvestable harvestable;

	// Token: 0x04002916 RID: 10518
	[MyCmpReq]
	private SeedProducer seedProducer;

	// Token: 0x04002917 RID: 10519
	[MyCmpReq]
	private KBatchedAnimController animController;

	// Token: 0x02001649 RID: 5705
	public class StatesInstance : GameStateMachine<BasicForagePlantPlanted.States, BasicForagePlantPlanted.StatesInstance, BasicForagePlantPlanted, object>.GameInstance
	{
		// Token: 0x06008736 RID: 34614 RVA: 0x002F1309 File Offset: 0x002EF509
		public StatesInstance(BasicForagePlantPlanted smi)
			: base(smi)
		{
		}
	}

	// Token: 0x0200164A RID: 5706
	public class States : GameStateMachine<BasicForagePlantPlanted.States, BasicForagePlantPlanted.StatesInstance, BasicForagePlantPlanted>
	{
		// Token: 0x06008737 RID: 34615 RVA: 0x002F1314 File Offset: 0x002EF514
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.seed_grow;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.seed_grow.PlayAnim("idle", KAnim.PlayMode.Once).EventTransition(GameHashes.AnimQueueComplete, this.alive.idle, null);
			this.alive.InitializeStates(this.masterTarget, this.dead);
			this.alive.idle.PlayAnim("idle").EventTransition(GameHashes.Harvest, this.alive.harvest, null).Enter(delegate(BasicForagePlantPlanted.StatesInstance smi)
			{
				smi.master.harvestable.SetCanBeHarvested(true);
			});
			this.alive.harvest.Enter(delegate(BasicForagePlantPlanted.StatesInstance smi)
			{
				smi.master.seedProducer.DropSeed(null);
			}).GoTo(this.dead);
			this.dead.Enter(delegate(BasicForagePlantPlanted.StatesInstance smi)
			{
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				smi.master.Trigger(1623392196, null);
				smi.master.animController.StopAndClear();
				UnityEngine.Object.Destroy(smi.master.animController);
				smi.master.DestroySelf(null);
			});
		}

		// Token: 0x04006954 RID: 26964
		public GameStateMachine<BasicForagePlantPlanted.States, BasicForagePlantPlanted.StatesInstance, BasicForagePlantPlanted, object>.State seed_grow;

		// Token: 0x04006955 RID: 26965
		public BasicForagePlantPlanted.States.AliveStates alive;

		// Token: 0x04006956 RID: 26966
		public GameStateMachine<BasicForagePlantPlanted.States, BasicForagePlantPlanted.StatesInstance, BasicForagePlantPlanted, object>.State dead;

		// Token: 0x02002088 RID: 8328
		public class AliveStates : GameStateMachine<BasicForagePlantPlanted.States, BasicForagePlantPlanted.StatesInstance, BasicForagePlantPlanted, object>.PlantAliveSubState
		{
			// Token: 0x040090F6 RID: 37110
			public GameStateMachine<BasicForagePlantPlanted.States, BasicForagePlantPlanted.StatesInstance, BasicForagePlantPlanted, object>.State idle;

			// Token: 0x040090F7 RID: 37111
			public GameStateMachine<BasicForagePlantPlanted.States, BasicForagePlantPlanted.StatesInstance, BasicForagePlantPlanted, object>.State harvest;
		}
	}
}
