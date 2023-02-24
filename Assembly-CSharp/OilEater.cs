using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000895 RID: 2197
public class OilEater : StateMachineComponent<OilEater.StatesInstance>
{
	// Token: 0x06003EFA RID: 16122 RVA: 0x0015FE05 File Offset: 0x0015E005
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003EFB RID: 16123 RVA: 0x0015FE18 File Offset: 0x0015E018
	public void Exhaust(float dt)
	{
		if (base.smi.master.wiltCondition.IsWilting())
		{
			return;
		}
		this.emittedMass += dt * this.emitRate;
		if (this.emittedMass >= this.minEmitMass)
		{
			int num = Grid.PosToCell(base.transform.GetPosition() + this.emitOffset);
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			SimMessages.AddRemoveSubstance(num, SimHashes.CarbonDioxide, CellEventLogger.Instance.ElementEmitted, this.emittedMass, component.Temperature, byte.MaxValue, 0, true, -1);
			this.emittedMass = 0f;
		}
	}

	// Token: 0x04002953 RID: 10579
	private const SimHashes srcElement = SimHashes.CrudeOil;

	// Token: 0x04002954 RID: 10580
	private const SimHashes emitElement = SimHashes.CarbonDioxide;

	// Token: 0x04002955 RID: 10581
	public float emitRate = 1f;

	// Token: 0x04002956 RID: 10582
	public float minEmitMass;

	// Token: 0x04002957 RID: 10583
	public Vector3 emitOffset = Vector3.zero;

	// Token: 0x04002958 RID: 10584
	[Serialize]
	private float emittedMass;

	// Token: 0x04002959 RID: 10585
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x0400295A RID: 10586
	[MyCmpReq]
	private Storage storage;

	// Token: 0x0400295B RID: 10587
	[MyCmpReq]
	private ReceptacleMonitor receptacleMonitor;

	// Token: 0x02001658 RID: 5720
	public class StatesInstance : GameStateMachine<OilEater.States, OilEater.StatesInstance, OilEater, object>.GameInstance
	{
		// Token: 0x06008769 RID: 34665 RVA: 0x002F2765 File Offset: 0x002F0965
		public StatesInstance(OilEater master)
			: base(master)
		{
		}
	}

	// Token: 0x02001659 RID: 5721
	public class States : GameStateMachine<OilEater.States, OilEater.StatesInstance, OilEater>
	{
		// Token: 0x0600876A RID: 34666 RVA: 0x002F2770 File Offset: 0x002F0970
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.grow;
			this.dead.ToggleStatusItem(CREATURES.STATUSITEMS.DEAD.NAME, CREATURES.STATUSITEMS.DEAD.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).Enter(delegate(OilEater.StatesInstance smi)
			{
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, delegate(object data)
				{
					GameObject gameObject = (GameObject)data;
					CreatureHelpers.DeselectCreature(gameObject);
					Util.KDestroyGameObject(gameObject);
				}, smi.master.gameObject);
			});
			this.blocked_from_growing.ToggleStatusItem(Db.Get().MiscStatusItems.RegionIsBlocked, null).EventTransition(GameHashes.EntombedChanged, this.alive, (OilEater.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject)).EventTransition(GameHashes.TooColdWarning, this.alive, (OilEater.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.EventTransition(GameHashes.TooHotWarning, this.alive, (OilEater.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.TagTransition(GameTags.Uprooted, this.dead, false);
			this.grow.Enter(delegate(OilEater.StatesInstance smi)
			{
				if (smi.master.receptacleMonitor.HasReceptacle() && !this.alive.ForceUpdateStatus(smi.master.gameObject))
				{
					smi.GoTo(this.blocked_from_growing);
				}
			}).PlayAnim("grow_seed", KAnim.PlayMode.Once).EventTransition(GameHashes.AnimQueueComplete, this.alive, null);
			this.alive.InitializeStates(this.masterTarget, this.dead).DefaultState(this.alive.mature).Update("Alive", delegate(OilEater.StatesInstance smi, float dt)
			{
				smi.master.Exhaust(dt);
			}, UpdateRate.SIM_200ms, false);
			this.alive.mature.EventTransition(GameHashes.Wilt, this.alive.wilting, (OilEater.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).PlayAnim("idle", KAnim.PlayMode.Loop);
			this.alive.wilting.PlayAnim("wilt1").EventTransition(GameHashes.WiltRecover, this.alive.mature, (OilEater.StatesInstance smi) => !smi.master.wiltCondition.IsWilting());
		}

		// Token: 0x04006973 RID: 26995
		public GameStateMachine<OilEater.States, OilEater.StatesInstance, OilEater, object>.State grow;

		// Token: 0x04006974 RID: 26996
		public GameStateMachine<OilEater.States, OilEater.StatesInstance, OilEater, object>.State blocked_from_growing;

		// Token: 0x04006975 RID: 26997
		public OilEater.States.AliveStates alive;

		// Token: 0x04006976 RID: 26998
		public GameStateMachine<OilEater.States, OilEater.StatesInstance, OilEater, object>.State dead;

		// Token: 0x02002097 RID: 8343
		public class AliveStates : GameStateMachine<OilEater.States, OilEater.StatesInstance, OilEater, object>.PlantAliveSubState
		{
			// Token: 0x0400913E RID: 37182
			public GameStateMachine<OilEater.States, OilEater.StatesInstance, OilEater, object>.State mature;

			// Token: 0x0400913F RID: 37183
			public GameStateMachine<OilEater.States, OilEater.StatesInstance, OilEater, object>.State wilting;
		}
	}
}
