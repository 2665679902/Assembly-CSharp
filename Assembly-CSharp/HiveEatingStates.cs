using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class HiveEatingStates : GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>
{
	// Token: 0x06000366 RID: 870 RVA: 0x0001AC08 File Offset: 0x00018E08
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.eating;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.eating.ToggleStatusItem(CREATURES.STATUSITEMS.HIVE_DIGESTING.NAME, CREATURES.STATUSITEMS.HIVE_DIGESTING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).DefaultState(this.eating.pre).Enter(delegate(HiveEatingStates.Instance smi)
		{
			smi.TurnOn();
		})
			.Exit(delegate(HiveEatingStates.Instance smi)
			{
				smi.TurnOff();
			});
		this.eating.pre.PlayAnim("eating_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.eating.loop);
		this.eating.loop.PlayAnim("eating_loop", KAnim.PlayMode.Loop).Update(delegate(HiveEatingStates.Instance smi, float dt)
		{
			smi.EatOreFromStorage(smi, dt);
		}, UpdateRate.SIM_4000ms, false).EventTransition(GameHashes.OnStorageChange, this.eating.pst, (HiveEatingStates.Instance smi) => !smi.storage.FindFirst(smi.def.consumedOre));
		this.eating.pst.PlayAnim("eating_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToEat, false);
	}

	// Token: 0x0400023A RID: 570
	public HiveEatingStates.EatingStates eating;

	// Token: 0x0400023B RID: 571
	public GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State behaviourcomplete;

	// Token: 0x02000E62 RID: 3682
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06006C21 RID: 27681 RVA: 0x00297739 File Offset: 0x00295939
		public Def(Tag consumedOre)
		{
			this.consumedOre = consumedOre;
		}

		// Token: 0x0400515E RID: 20830
		public Tag consumedOre;
	}

	// Token: 0x02000E63 RID: 3683
	public class EatingStates : GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State
	{
		// Token: 0x0400515F RID: 20831
		public GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State pre;

		// Token: 0x04005160 RID: 20832
		public GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State loop;

		// Token: 0x04005161 RID: 20833
		public GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State pst;
	}

	// Token: 0x02000E64 RID: 3684
	public new class Instance : GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.GameInstance
	{
		// Token: 0x06006C23 RID: 27683 RVA: 0x00297750 File Offset: 0x00295950
		public Instance(Chore<HiveEatingStates.Instance> chore, HiveEatingStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToEat);
		}

		// Token: 0x06006C24 RID: 27684 RVA: 0x00297774 File Offset: 0x00295974
		public void TurnOn()
		{
			this.emitter.emitRads = 600f * this.emitter.emitRate;
			this.emitter.Refresh();
		}

		// Token: 0x06006C25 RID: 27685 RVA: 0x0029779D File Offset: 0x0029599D
		public void TurnOff()
		{
			this.emitter.emitRads = 0f;
			this.emitter.Refresh();
		}

		// Token: 0x06006C26 RID: 27686 RVA: 0x002977BC File Offset: 0x002959BC
		public void EatOreFromStorage(HiveEatingStates.Instance smi, float dt)
		{
			GameObject gameObject = smi.storage.FindFirst(smi.def.consumedOre);
			if (!gameObject)
			{
				return;
			}
			float num = 0.25f;
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component == null)
			{
				return;
			}
			PrimaryElement component2 = component.GetComponent<PrimaryElement>();
			if (component2 == null)
			{
				return;
			}
			Diet.Info dietInfo = smi.gameObject.AddOrGetDef<BeehiveCalorieMonitor.Def>().diet.GetDietInfo(component.PrefabTag);
			if (dietInfo == null)
			{
				return;
			}
			AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(smi.gameObject);
			float num2 = amountInstance.GetMax() - amountInstance.value;
			float num3 = dietInfo.ConvertCaloriesToConsumptionMass(num2);
			float num4 = num * dt;
			if (num3 < num4)
			{
				num4 = num3;
			}
			num4 = Mathf.Min(num4, component2.Mass);
			component2.Mass -= num4;
			Pickupable component3 = component2.GetComponent<Pickupable>();
			if (component3.storage != null)
			{
				component3.storage.Trigger(-1452790913, smi.gameObject);
				component3.storage.Trigger(-1697596308, smi.gameObject);
			}
			float num5 = dietInfo.ConvertConsumptionMassToCalories(num4);
			CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = new CreatureCalorieMonitor.CaloriesConsumedEvent
			{
				tag = component.PrefabTag,
				calories = num5
			};
			smi.gameObject.Trigger(-2038961714, caloriesConsumedEvent);
		}

		// Token: 0x04005162 RID: 20834
		[MyCmpReq]
		public Storage storage;

		// Token: 0x04005163 RID: 20835
		[MyCmpReq]
		private RadiationEmitter emitter;
	}
}
