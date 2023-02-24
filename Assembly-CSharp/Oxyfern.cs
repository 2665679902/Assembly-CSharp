using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000896 RID: 2198
public class Oxyfern : StateMachineComponent<Oxyfern.StatesInstance>
{
	// Token: 0x06003EFD RID: 16125 RVA: 0x0015FED3 File Offset: 0x0015E0D3
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06003EFE RID: 16126 RVA: 0x0015FEEB File Offset: 0x0015E0EB
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003EFF RID: 16127 RVA: 0x0015FEFE File Offset: 0x0015E0FE
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (Tutorial.Instance.oxygenGenerators.Contains(base.gameObject))
		{
			Tutorial.Instance.oxygenGenerators.Remove(base.gameObject);
		}
	}

	// Token: 0x06003F00 RID: 16128 RVA: 0x0015FF33 File Offset: 0x0015E133
	protected override void OnPrefabInit()
	{
		base.Subscribe<Oxyfern>(1309017699, Oxyfern.OnReplantedDelegate);
		base.OnPrefabInit();
	}

	// Token: 0x06003F01 RID: 16129 RVA: 0x0015FF4C File Offset: 0x0015E14C
	private void OnReplanted(object data = null)
	{
		this.SetConsumptionRate();
		if (this.receptacleMonitor.Replanted)
		{
			Tutorial.Instance.oxygenGenerators.Add(base.gameObject);
		}
	}

	// Token: 0x06003F02 RID: 16130 RVA: 0x0015FF76 File Offset: 0x0015E176
	public void SetConsumptionRate()
	{
		if (this.receptacleMonitor.Replanted)
		{
			this.elementConsumer.consumptionRate = 0.00062500004f;
			return;
		}
		this.elementConsumer.consumptionRate = 0.00015625001f;
	}

	// Token: 0x0400295C RID: 10588
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x0400295D RID: 10589
	[MyCmpReq]
	private ElementConsumer elementConsumer;

	// Token: 0x0400295E RID: 10590
	[MyCmpReq]
	private ElementConverter elementConverter;

	// Token: 0x0400295F RID: 10591
	[MyCmpReq]
	private ReceptacleMonitor receptacleMonitor;

	// Token: 0x04002960 RID: 10592
	private static readonly EventSystem.IntraObjectHandler<Oxyfern> OnReplantedDelegate = new EventSystem.IntraObjectHandler<Oxyfern>(delegate(Oxyfern component, object data)
	{
		component.OnReplanted(data);
	});

	// Token: 0x0200165A RID: 5722
	public class StatesInstance : GameStateMachine<Oxyfern.States, Oxyfern.StatesInstance, Oxyfern, object>.GameInstance
	{
		// Token: 0x06008770 RID: 34672 RVA: 0x002F2A15 File Offset: 0x002F0C15
		public StatesInstance(Oxyfern master)
			: base(master)
		{
		}
	}

	// Token: 0x0200165B RID: 5723
	public class States : GameStateMachine<Oxyfern.States, Oxyfern.StatesInstance, Oxyfern>
	{
		// Token: 0x06008771 RID: 34673 RVA: 0x002F2A20 File Offset: 0x002F0C20
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.grow;
			this.dead.ToggleStatusItem(CREATURES.STATUSITEMS.DEAD.NAME, CREATURES.STATUSITEMS.DEAD.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).Enter(delegate(Oxyfern.StatesInstance smi)
			{
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
			});
			this.blocked_from_growing.ToggleStatusItem(Db.Get().MiscStatusItems.RegionIsBlocked, null).EventTransition(GameHashes.EntombedChanged, this.alive, (Oxyfern.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject)).EventTransition(GameHashes.TooColdWarning, this.alive, (Oxyfern.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.EventTransition(GameHashes.TooHotWarning, this.alive, (Oxyfern.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.TagTransition(GameTags.Uprooted, this.dead, false);
			this.grow.Enter(delegate(Oxyfern.StatesInstance smi)
			{
				if (smi.master.receptacleMonitor.HasReceptacle() && !this.alive.ForceUpdateStatus(smi.master.gameObject))
				{
					smi.GoTo(this.blocked_from_growing);
				}
			}).PlayAnim("grow_pst", KAnim.PlayMode.Once).EventTransition(GameHashes.AnimQueueComplete, this.alive, null);
			this.alive.InitializeStates(this.masterTarget, this.dead).DefaultState(this.alive.mature);
			this.alive.mature.EventTransition(GameHashes.Wilt, this.alive.wilting, (Oxyfern.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).PlayAnim("idle_full", KAnim.PlayMode.Loop).Enter(delegate(Oxyfern.StatesInstance smi)
			{
				smi.master.elementConsumer.EnableConsumption(true);
			})
				.Exit(delegate(Oxyfern.StatesInstance smi)
				{
					smi.master.elementConsumer.EnableConsumption(false);
				});
			this.alive.wilting.PlayAnim("wilt3").EventTransition(GameHashes.WiltRecover, this.alive.mature, (Oxyfern.StatesInstance smi) => !smi.master.wiltCondition.IsWilting());
		}

		// Token: 0x04006977 RID: 26999
		public GameStateMachine<Oxyfern.States, Oxyfern.StatesInstance, Oxyfern, object>.State grow;

		// Token: 0x04006978 RID: 27000
		public GameStateMachine<Oxyfern.States, Oxyfern.StatesInstance, Oxyfern, object>.State blocked_from_growing;

		// Token: 0x04006979 RID: 27001
		public Oxyfern.States.AliveStates alive;

		// Token: 0x0400697A RID: 27002
		public GameStateMachine<Oxyfern.States, Oxyfern.StatesInstance, Oxyfern, object>.State dead;

		// Token: 0x02002099 RID: 8345
		public class AliveStates : GameStateMachine<Oxyfern.States, Oxyfern.StatesInstance, Oxyfern, object>.PlantAliveSubState
		{
			// Token: 0x04009146 RID: 37190
			public GameStateMachine<Oxyfern.States, Oxyfern.StatesInstance, Oxyfern, object>.State mature;

			// Token: 0x04009147 RID: 37191
			public GameStateMachine<Oxyfern.States, Oxyfern.StatesInstance, Oxyfern, object>.State wilting;
		}
	}
}
