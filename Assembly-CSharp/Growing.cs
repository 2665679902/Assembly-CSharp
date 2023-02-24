using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006D9 RID: 1753
public class Growing : StateMachineComponent<Growing.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x000FBCB3 File Offset: 0x000F9EB3
	private Crop crop
	{
		get
		{
			if (this._crop == null)
			{
				this._crop = base.GetComponent<Crop>();
			}
			return this._crop;
		}
	}

	// Token: 0x06002FA6 RID: 12198 RVA: 0x000FBCD8 File Offset: 0x000F9ED8
	protected override void OnPrefabInit()
	{
		Amounts amounts = base.gameObject.GetAmounts();
		this.maturity = amounts.Get(Db.Get().Amounts.Maturity);
		this.oldAge = amounts.Add(new AmountInstance(Db.Get().Amounts.OldAge, base.gameObject));
		this.oldAge.maxAttribute.ClearModifiers();
		this.oldAge.maxAttribute.Add(new AttributeModifier(Db.Get().Amounts.OldAge.maxAttribute.Id, this.maxAge, null, false, false, true));
		base.OnPrefabInit();
		base.Subscribe<Growing>(1119167081, Growing.OnNewGameSpawnDelegate);
		base.Subscribe<Growing>(1272413801, Growing.ResetGrowthDelegate);
	}

	// Token: 0x06002FA7 RID: 12199 RVA: 0x000FBDA2 File Offset: 0x000F9FA2
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		base.gameObject.AddTag(GameTags.GrowingPlant);
	}

	// Token: 0x06002FA8 RID: 12200 RVA: 0x000FBDC5 File Offset: 0x000F9FC5
	private void OnNewGameSpawn(object data)
	{
		this.maturity.SetValue(this.maturity.maxAttribute.GetTotalValue() * UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x06002FA9 RID: 12201 RVA: 0x000FBDF4 File Offset: 0x000F9FF4
	public void OverrideMaturityLevel(float percent)
	{
		float num = this.maturity.GetMax() * percent;
		this.maturity.SetValue(num);
	}

	// Token: 0x06002FAA RID: 12202 RVA: 0x000FBE1C File Offset: 0x000FA01C
	public bool ReachedNextHarvest()
	{
		return this.PercentOfCurrentHarvest() >= 1f;
	}

	// Token: 0x06002FAB RID: 12203 RVA: 0x000FBE2E File Offset: 0x000FA02E
	public bool IsGrown()
	{
		return this.maturity.value == this.maturity.GetMax();
	}

	// Token: 0x06002FAC RID: 12204 RVA: 0x000FBE48 File Offset: 0x000FA048
	public bool CanGrow()
	{
		return !this.IsGrown();
	}

	// Token: 0x06002FAD RID: 12205 RVA: 0x000FBE53 File Offset: 0x000FA053
	public bool IsGrowing()
	{
		return this.maturity.GetDelta() > 0f;
	}

	// Token: 0x06002FAE RID: 12206 RVA: 0x000FBE67 File Offset: 0x000FA067
	public void ClampGrowthToHarvest()
	{
		this.maturity.value = this.maturity.GetMax();
	}

	// Token: 0x06002FAF RID: 12207 RVA: 0x000FBE7F File Offset: 0x000FA07F
	public float GetMaxMaturity()
	{
		return this.maturity.GetMax();
	}

	// Token: 0x06002FB0 RID: 12208 RVA: 0x000FBE8C File Offset: 0x000FA08C
	public float PercentOfCurrentHarvest()
	{
		return this.maturity.value / this.maturity.GetMax();
	}

	// Token: 0x06002FB1 RID: 12209 RVA: 0x000FBEA5 File Offset: 0x000FA0A5
	public float TimeUntilNextHarvest()
	{
		return (this.maturity.GetMax() - this.maturity.value) / this.maturity.GetDelta();
	}

	// Token: 0x06002FB2 RID: 12210 RVA: 0x000FBECA File Offset: 0x000FA0CA
	public float DomesticGrowthTime()
	{
		return this.maturity.GetMax() / base.smi.baseGrowingRate.Value;
	}

	// Token: 0x06002FB3 RID: 12211 RVA: 0x000FBEE8 File Offset: 0x000FA0E8
	public float WildGrowthTime()
	{
		return this.maturity.GetMax() / base.smi.wildGrowingRate.Value;
	}

	// Token: 0x06002FB4 RID: 12212 RVA: 0x000FBF06 File Offset: 0x000FA106
	public float PercentGrown()
	{
		return this.maturity.value / this.maturity.GetMax();
	}

	// Token: 0x06002FB5 RID: 12213 RVA: 0x000FBF1F File Offset: 0x000FA11F
	public void ResetGrowth(object data = null)
	{
		this.maturity.value = 0f;
	}

	// Token: 0x06002FB6 RID: 12214 RVA: 0x000FBF31 File Offset: 0x000FA131
	public float PercentOldAge()
	{
		if (!this.shouldGrowOld)
		{
			return 0f;
		}
		return this.oldAge.value / this.oldAge.GetMax();
	}

	// Token: 0x06002FB7 RID: 12215 RVA: 0x000FBF58 File Offset: 0x000FA158
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Klei.AI.Attribute maxAttribute = Db.Get().Amounts.Maturity.maxAttribute;
		list.Add(new Descriptor(go.GetComponent<Modifiers>().GetPreModifiedAttributeDescription(maxAttribute), go.GetComponent<Modifiers>().GetPreModifiedAttributeToolTip(maxAttribute), Descriptor.DescriptorType.Requirement, false));
		return list;
	}

	// Token: 0x06002FB8 RID: 12216 RVA: 0x000FBFA4 File Offset: 0x000FA1A4
	public void ConsumeMass(float mass_to_consume)
	{
		float value = this.maturity.value;
		mass_to_consume = Mathf.Min(mass_to_consume, value);
		this.maturity.value = this.maturity.value - mass_to_consume;
		base.gameObject.Trigger(-1793167409, null);
	}

	// Token: 0x04001CB3 RID: 7347
	public bool shouldGrowOld = true;

	// Token: 0x04001CB4 RID: 7348
	public float maxAge = 2400f;

	// Token: 0x04001CB5 RID: 7349
	private AmountInstance maturity;

	// Token: 0x04001CB6 RID: 7350
	private AmountInstance oldAge;

	// Token: 0x04001CB7 RID: 7351
	[MyCmpGet]
	private WiltCondition wiltCondition;

	// Token: 0x04001CB8 RID: 7352
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04001CB9 RID: 7353
	[MyCmpReq]
	private Modifiers modifiers;

	// Token: 0x04001CBA RID: 7354
	[MyCmpReq]
	private ReceptacleMonitor rm;

	// Token: 0x04001CBB RID: 7355
	private Crop _crop;

	// Token: 0x04001CBC RID: 7356
	private static readonly EventSystem.IntraObjectHandler<Growing> OnNewGameSpawnDelegate = new EventSystem.IntraObjectHandler<Growing>(delegate(Growing component, object data)
	{
		component.OnNewGameSpawn(data);
	});

	// Token: 0x04001CBD RID: 7357
	private static readonly EventSystem.IntraObjectHandler<Growing> ResetGrowthDelegate = new EventSystem.IntraObjectHandler<Growing>(delegate(Growing component, object data)
	{
		component.ResetGrowth(data);
	});

	// Token: 0x020013D7 RID: 5079
	public class StatesInstance : GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.GameInstance
	{
		// Token: 0x06007F41 RID: 32577 RVA: 0x002DBC7C File Offset: 0x002D9E7C
		public StatesInstance(Growing master)
			: base(master)
		{
			this.baseGrowingRate = new AttributeModifier(master.maturity.deltaAttribute.Id, 0.0016666667f, CREATURES.STATS.MATURITY.GROWING, false, false, true);
			this.wildGrowingRate = new AttributeModifier(master.maturity.deltaAttribute.Id, 0.00041666668f, CREATURES.STATS.MATURITY.GROWINGWILD, false, false, true);
			this.getOldRate = new AttributeModifier(master.oldAge.deltaAttribute.Id, master.shouldGrowOld ? 1f : 0f, null, false, false, true);
		}

		// Token: 0x06007F42 RID: 32578 RVA: 0x002DBD1D File Offset: 0x002D9F1D
		public bool IsGrown()
		{
			return base.master.IsGrown();
		}

		// Token: 0x06007F43 RID: 32579 RVA: 0x002DBD2A File Offset: 0x002D9F2A
		public bool ReachedNextHarvest()
		{
			return base.master.ReachedNextHarvest();
		}

		// Token: 0x06007F44 RID: 32580 RVA: 0x002DBD37 File Offset: 0x002D9F37
		public void ClampGrowthToHarvest()
		{
			base.master.ClampGrowthToHarvest();
		}

		// Token: 0x06007F45 RID: 32581 RVA: 0x002DBD44 File Offset: 0x002D9F44
		public bool IsWilting()
		{
			return base.master.wiltCondition != null && base.master.wiltCondition.IsWilting();
		}

		// Token: 0x06007F46 RID: 32582 RVA: 0x002DBD6C File Offset: 0x002D9F6C
		public bool IsSleeping()
		{
			CropSleepingMonitor.Instance smi = base.master.GetSMI<CropSleepingMonitor.Instance>();
			return smi != null && smi.IsSleeping();
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x002DBD90 File Offset: 0x002D9F90
		public bool CanExitStalled()
		{
			return !this.IsWilting() && !this.IsSleeping();
		}

		// Token: 0x040061CB RID: 25035
		public AttributeModifier baseGrowingRate;

		// Token: 0x040061CC RID: 25036
		public AttributeModifier wildGrowingRate;

		// Token: 0x040061CD RID: 25037
		public AttributeModifier getOldRate;
	}

	// Token: 0x020013D8 RID: 5080
	public class States : GameStateMachine<Growing.States, Growing.StatesInstance, Growing>
	{
		// Token: 0x06007F48 RID: 32584 RVA: 0x002DBDA8 File Offset: 0x002D9FA8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.growing;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.growing.EventTransition(GameHashes.Wilt, this.stalled, (Growing.StatesInstance smi) => smi.IsWilting()).EventTransition(GameHashes.CropSleep, this.stalled, (Growing.StatesInstance smi) => smi.IsSleeping()).EventTransition(GameHashes.PlanterStorage, this.growing.planted, (Growing.StatesInstance smi) => smi.master.rm.Replanted)
				.EventTransition(GameHashes.PlanterStorage, this.growing.wild, (Growing.StatesInstance smi) => !smi.master.rm.Replanted)
				.TriggerOnEnter(GameHashes.Grow, null)
				.Update("CheckGrown", delegate(Growing.StatesInstance smi, float dt)
				{
					if (smi.ReachedNextHarvest())
					{
						smi.GoTo(this.grown);
					}
				}, UpdateRate.SIM_4000ms, false)
				.ToggleStatusItem(Db.Get().CreatureStatusItems.Growing, (Growing.StatesInstance smi) => smi.master.GetComponent<Growing>())
				.Enter(delegate(Growing.StatesInstance smi)
				{
					GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State state = (smi.master.rm.Replanted ? this.growing.planted : this.growing.wild);
					smi.GoTo(state);
				});
			this.growing.wild.ToggleAttributeModifier("GrowingWild", (Growing.StatesInstance smi) => smi.wildGrowingRate, null);
			this.growing.planted.ToggleAttributeModifier("Growing", (Growing.StatesInstance smi) => smi.baseGrowingRate, null);
			this.stalled.EventTransition(GameHashes.WiltRecover, this.growing, (Growing.StatesInstance smi) => smi.CanExitStalled()).EventTransition(GameHashes.CropWakeUp, this.growing, (Growing.StatesInstance smi) => smi.CanExitStalled());
			this.grown.DefaultState(this.grown.idle).TriggerOnEnter(GameHashes.Grow, null).Update("CheckNotGrown", delegate(Growing.StatesInstance smi, float dt)
			{
				if (!smi.ReachedNextHarvest())
				{
					smi.GoTo(this.growing);
				}
			}, UpdateRate.SIM_4000ms, false)
				.ToggleAttributeModifier("GettingOld", (Growing.StatesInstance smi) => smi.getOldRate, null)
				.Enter(delegate(Growing.StatesInstance smi)
				{
					smi.ClampGrowthToHarvest();
				})
				.Exit(delegate(Growing.StatesInstance smi)
				{
					smi.master.oldAge.SetValue(0f);
				});
			this.grown.idle.Update("CheckNotGrown", delegate(Growing.StatesInstance smi, float dt)
			{
				if (smi.master.shouldGrowOld && smi.master.oldAge.value >= smi.master.oldAge.GetMax())
				{
					smi.GoTo(this.grown.try_self_harvest);
				}
			}, UpdateRate.SIM_4000ms, false);
			this.grown.try_self_harvest.Enter(delegate(Growing.StatesInstance smi)
			{
				Harvestable component = smi.master.GetComponent<Harvestable>();
				if (component && component.CanBeHarvested)
				{
					bool harvestWhenReady = component.harvestDesignatable.HarvestWhenReady;
					component.ForceCancelHarvest(null);
					component.Harvest();
					if (harvestWhenReady && component != null)
					{
						component.harvestDesignatable.SetHarvestWhenReady(true);
					}
				}
				smi.master.maturity.SetValue(0f);
				smi.master.oldAge.SetValue(0f);
			}).GoTo(this.grown.idle);
		}

		// Token: 0x040061CE RID: 25038
		public Growing.States.GrowingStates growing;

		// Token: 0x040061CF RID: 25039
		public GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State stalled;

		// Token: 0x040061D0 RID: 25040
		public Growing.States.GrownStates grown;

		// Token: 0x0200203D RID: 8253
		public class GrowingStates : GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State
		{
			// Token: 0x04008F98 RID: 36760
			public GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State wild;

			// Token: 0x04008F99 RID: 36761
			public GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State planted;
		}

		// Token: 0x0200203E RID: 8254
		public class GrownStates : GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State
		{
			// Token: 0x04008F9A RID: 36762
			public GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State idle;

			// Token: 0x04008F9B RID: 36763
			public GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State try_self_harvest;
		}
	}
}
