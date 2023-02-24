using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000890 RID: 2192
[SkipSaveFileSerialization]
public class ColdBreather : StateMachineComponent<ColdBreather.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06003EDC RID: 16092 RVA: 0x0015F607 File Offset: 0x0015D807
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.simEmitCBHandle = Game.Instance.massEmitCallbackManager.Add(new Action<Sim.MassEmittedCallback, object>(ColdBreather.OnSimEmittedCallback), this, "ColdBreather");
		base.smi.StartSM();
	}

	// Token: 0x06003EDD RID: 16093 RVA: 0x0015F641 File Offset: 0x0015D841
	protected override void OnPrefabInit()
	{
		this.elementConsumer.EnableConsumption(false);
		base.Subscribe<ColdBreather>(1309017699, ColdBreather.OnReplantedDelegate);
		base.OnPrefabInit();
	}

	// Token: 0x06003EDE RID: 16094 RVA: 0x0015F668 File Offset: 0x0015D868
	private void OnReplanted(object data = null)
	{
		ReceptacleMonitor component = base.GetComponent<ReceptacleMonitor>();
		if (component == null)
		{
			return;
		}
		ElementConsumer component2 = base.GetComponent<ElementConsumer>();
		if (component.Replanted)
		{
			component2.consumptionRate = this.consumptionRate;
		}
		else
		{
			component2.consumptionRate = this.consumptionRate * 0.25f;
		}
		if (this.radiationEmitter != null)
		{
			this.radiationEmitter.emitRads = 480f;
			this.radiationEmitter.Refresh();
		}
	}

	// Token: 0x06003EDF RID: 16095 RVA: 0x0015F6E0 File Offset: 0x0015D8E0
	protected override void OnCleanUp()
	{
		Game.Instance.massEmitCallbackManager.Release(this.simEmitCBHandle, "coldbreather");
		this.simEmitCBHandle.Clear();
		if (this.storage)
		{
			this.storage.DropAll(true, false, default(Vector3), true, null);
		}
		base.OnCleanUp();
	}

	// Token: 0x06003EE0 RID: 16096 RVA: 0x0015F73E File Offset: 0x0015D93E
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06003EE1 RID: 16097 RVA: 0x0015F756 File Offset: 0x0015D956
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.GAMEOBJECTEFFECTS.COLDBREATHER, UI.GAMEOBJECTEFFECTS.TOOLTIPS.COLDBREATHER, Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x06003EE2 RID: 16098 RVA: 0x0015F77E File Offset: 0x0015D97E
	private void SetEmitting(bool emitting)
	{
		if (this.radiationEmitter != null)
		{
			this.radiationEmitter.SetEmitting(emitting);
		}
	}

	// Token: 0x06003EE3 RID: 16099 RVA: 0x0015F79C File Offset: 0x0015D99C
	private void Exhale()
	{
		if (this.lastEmitTag != Tag.Invalid)
		{
			return;
		}
		this.gases.Clear();
		this.storage.Find(GameTags.Gas, this.gases);
		if (this.nextGasEmitIndex >= this.gases.Count)
		{
			this.nextGasEmitIndex = 0;
		}
		while (this.nextGasEmitIndex < this.gases.Count)
		{
			int num = this.nextGasEmitIndex;
			this.nextGasEmitIndex = num + 1;
			int num2 = num;
			PrimaryElement component = this.gases[num2].GetComponent<PrimaryElement>();
			if (component != null && component.Mass > 0f && this.simEmitCBHandle.IsValid())
			{
				float num3 = Mathf.Max(component.Element.lowTemp + 5f, component.Temperature + this.deltaEmitTemperature);
				int num4 = Grid.PosToCell(base.transform.GetPosition() + this.emitOffsetCell);
				ushort idx = component.Element.idx;
				Game.Instance.massEmitCallbackManager.GetItem(this.simEmitCBHandle);
				SimMessages.EmitMass(num4, idx, component.Mass, num3, component.DiseaseIdx, component.DiseaseCount, this.simEmitCBHandle.index);
				this.lastEmitTag = component.Element.tag;
				return;
			}
		}
	}

	// Token: 0x06003EE4 RID: 16100 RVA: 0x0015F8FF File Offset: 0x0015DAFF
	private static void OnSimEmittedCallback(Sim.MassEmittedCallback info, object data)
	{
		((ColdBreather)data).OnSimEmitted(info);
	}

	// Token: 0x06003EE5 RID: 16101 RVA: 0x0015F910 File Offset: 0x0015DB10
	private void OnSimEmitted(Sim.MassEmittedCallback info)
	{
		if (info.suceeded == 1 && this.storage && this.lastEmitTag.IsValid)
		{
			this.storage.ConsumeIgnoringDisease(this.lastEmitTag, info.mass);
		}
		this.lastEmitTag = Tag.Invalid;
	}

	// Token: 0x04002926 RID: 10534
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x04002927 RID: 10535
	[MyCmpReq]
	private KAnimControllerBase animController;

	// Token: 0x04002928 RID: 10536
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04002929 RID: 10537
	[MyCmpReq]
	private ElementConsumer elementConsumer;

	// Token: 0x0400292A RID: 10538
	[MyCmpGet]
	private RadiationEmitter radiationEmitter;

	// Token: 0x0400292B RID: 10539
	[MyCmpReq]
	private ReceptacleMonitor receptacleMonitor;

	// Token: 0x0400292C RID: 10540
	private const float EXHALE_PERIOD = 1f;

	// Token: 0x0400292D RID: 10541
	public float consumptionRate;

	// Token: 0x0400292E RID: 10542
	public float deltaEmitTemperature = -5f;

	// Token: 0x0400292F RID: 10543
	public Vector3 emitOffsetCell = new Vector3(0f, 0f);

	// Token: 0x04002930 RID: 10544
	private List<GameObject> gases = new List<GameObject>();

	// Token: 0x04002931 RID: 10545
	private Tag lastEmitTag;

	// Token: 0x04002932 RID: 10546
	private int nextGasEmitIndex;

	// Token: 0x04002933 RID: 10547
	private HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.Handle simEmitCBHandle = HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.InvalidHandle;

	// Token: 0x04002934 RID: 10548
	private static readonly EventSystem.IntraObjectHandler<ColdBreather> OnReplantedDelegate = new EventSystem.IntraObjectHandler<ColdBreather>(delegate(ColdBreather component, object data)
	{
		component.OnReplanted(data);
	});

	// Token: 0x0200164D RID: 5709
	public class StatesInstance : GameStateMachine<ColdBreather.States, ColdBreather.StatesInstance, ColdBreather, object>.GameInstance
	{
		// Token: 0x06008745 RID: 34629 RVA: 0x002F1544 File Offset: 0x002EF744
		public StatesInstance(ColdBreather master)
			: base(master)
		{
		}
	}

	// Token: 0x0200164E RID: 5710
	public class States : GameStateMachine<ColdBreather.States, ColdBreather.StatesInstance, ColdBreather>
	{
		// Token: 0x06008746 RID: 34630 RVA: 0x002F1550 File Offset: 0x002EF750
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.grow;
			this.statusItemCooling = new StatusItem("cooling", CREATURES.STATUSITEMS.COOLING.NAME, CREATURES.STATUSITEMS.COOLING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022, true, null);
			this.dead.ToggleStatusItem(CREATURES.STATUSITEMS.DEAD.NAME, CREATURES.STATUSITEMS.DEAD.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).Enter(delegate(ColdBreather.StatesInstance smi)
			{
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
			});
			this.blocked_from_growing.ToggleStatusItem(Db.Get().MiscStatusItems.RegionIsBlocked, null).EventTransition(GameHashes.EntombedChanged, this.alive, (ColdBreather.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject)).EventTransition(GameHashes.TooColdWarning, this.alive, (ColdBreather.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.EventTransition(GameHashes.TooHotWarning, this.alive, (ColdBreather.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.TagTransition(GameTags.Uprooted, this.dead, false);
			this.grow.Enter(delegate(ColdBreather.StatesInstance smi)
			{
				if (smi.master.receptacleMonitor.HasReceptacle() && !this.alive.ForceUpdateStatus(smi.master.gameObject))
				{
					smi.GoTo(this.blocked_from_growing);
				}
			}).PlayAnim("grow_seed", KAnim.PlayMode.Once).EventTransition(GameHashes.AnimQueueComplete, this.alive, null);
			this.alive.InitializeStates(this.masterTarget, this.dead).DefaultState(this.alive.mature).Update(delegate(ColdBreather.StatesInstance smi, float dt)
			{
				smi.master.Exhale();
			}, UpdateRate.SIM_200ms, false);
			this.alive.mature.EventTransition(GameHashes.Wilt, this.alive.wilting, (ColdBreather.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).PlayAnim("idle", KAnim.PlayMode.Loop).ToggleMainStatusItem(this.statusItemCooling, null)
				.Enter(delegate(ColdBreather.StatesInstance smi)
				{
					smi.master.elementConsumer.EnableConsumption(true);
					smi.master.SetEmitting(true);
				})
				.Exit(delegate(ColdBreather.StatesInstance smi)
				{
					smi.master.elementConsumer.EnableConsumption(false);
					smi.master.SetEmitting(false);
				});
			this.alive.wilting.PlayAnim("wilt1").EventTransition(GameHashes.WiltRecover, this.alive.mature, (ColdBreather.StatesInstance smi) => !smi.master.wiltCondition.IsWilting()).Enter(delegate(ColdBreather.StatesInstance smi)
			{
				smi.master.SetEmitting(false);
			});
		}

		// Token: 0x0400695C RID: 26972
		public GameStateMachine<ColdBreather.States, ColdBreather.StatesInstance, ColdBreather, object>.State grow;

		// Token: 0x0400695D RID: 26973
		public GameStateMachine<ColdBreather.States, ColdBreather.StatesInstance, ColdBreather, object>.State blocked_from_growing;

		// Token: 0x0400695E RID: 26974
		public ColdBreather.States.AliveStates alive;

		// Token: 0x0400695F RID: 26975
		public GameStateMachine<ColdBreather.States, ColdBreather.StatesInstance, ColdBreather, object>.State dead;

		// Token: 0x04006960 RID: 26976
		private StatusItem statusItemCooling;

		// Token: 0x0200208A RID: 8330
		public class AliveStates : GameStateMachine<ColdBreather.States, ColdBreather.StatesInstance, ColdBreather, object>.PlantAliveSubState
		{
			// Token: 0x040090FC RID: 37116
			public GameStateMachine<ColdBreather.States, ColdBreather.StatesInstance, ColdBreather, object>.State mature;

			// Token: 0x040090FD RID: 37117
			public GameStateMachine<ColdBreather.States, ColdBreather.StatesInstance, ColdBreather, object>.State wilting;
		}
	}
}
