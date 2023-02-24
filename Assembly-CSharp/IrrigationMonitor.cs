using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006DE RID: 1758
public class IrrigationMonitor : GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>
{
	// Token: 0x06002FDE RID: 12254 RVA: 0x000FCC20 File Offset: 0x000FAE20
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.wild;
		base.serializable = StateMachine.SerializeType.Never;
		this.wild.ParamTransition<GameObject>(this.resourceStorage, this.unfertilizable, (IrrigationMonitor.Instance smi, GameObject p) => p != null);
		this.unfertilizable.Enter(delegate(IrrigationMonitor.Instance smi)
		{
			if (smi.AcceptsLiquid())
			{
				smi.GoTo(this.replanted.irrigated);
			}
		});
		this.replanted.Enter(delegate(IrrigationMonitor.Instance smi)
		{
			ManualDeliveryKG[] components = smi.gameObject.GetComponents<ManualDeliveryKG>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].Pause(false, "replanted");
			}
			smi.UpdateIrrigation(0.033333335f);
		}).Target(this.resourceStorage).EventHandler(GameHashes.OnStorageChange, delegate(IrrigationMonitor.Instance smi)
		{
			smi.UpdateIrrigation(0.2f);
		})
			.Target(this.masterTarget);
		this.replanted.irrigated.DefaultState(this.replanted.irrigated.absorbing).TriggerOnEnter(this.ResourceRecievedEvent, null);
		this.replanted.irrigated.absorbing.DefaultState(this.replanted.irrigated.absorbing.normal).ParamTransition<bool>(this.hasCorrectLiquid, this.replanted.starved, GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.IsFalse).ToggleAttributeModifier("Absorbing", (IrrigationMonitor.Instance smi) => smi.absorptionRate, null)
			.Enter(delegate(IrrigationMonitor.Instance smi)
			{
				smi.UpdateAbsorbing(true);
			})
			.EventHandler(GameHashes.TagsChanged, delegate(IrrigationMonitor.Instance smi)
			{
				smi.UpdateAbsorbing(true);
			})
			.Exit(delegate(IrrigationMonitor.Instance smi)
			{
				smi.UpdateAbsorbing(false);
			});
		this.replanted.irrigated.absorbing.normal.ParamTransition<bool>(this.hasIncorrectLiquid, this.replanted.irrigated.absorbing.wrongLiquid, GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.IsTrue);
		this.replanted.irrigated.absorbing.wrongLiquid.ParamTransition<bool>(this.hasIncorrectLiquid, this.replanted.irrigated.absorbing.normal, GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.IsFalse);
		this.replanted.starved.DefaultState(this.replanted.starved.normal).TriggerOnEnter(this.ResourceDepletedEvent, null).ParamTransition<bool>(this.enoughCorrectLiquidToRecover, this.replanted.irrigated.absorbing, (IrrigationMonitor.Instance smi, bool p) => p && this.hasCorrectLiquid.Get(smi))
			.ParamTransition<bool>(this.hasCorrectLiquid, this.replanted.irrigated.absorbing, (IrrigationMonitor.Instance smi, bool p) => p && this.enoughCorrectLiquidToRecover.Get(smi));
		this.replanted.starved.normal.ParamTransition<bool>(this.hasIncorrectLiquid, this.replanted.starved.wrongLiquid, GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.IsTrue);
		this.replanted.starved.wrongLiquid.ParamTransition<bool>(this.hasIncorrectLiquid, this.replanted.starved.normal, GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.IsFalse);
	}

	// Token: 0x04001CD5 RID: 7381
	public StateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.TargetParameter resourceStorage;

	// Token: 0x04001CD6 RID: 7382
	public StateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.BoolParameter hasCorrectLiquid;

	// Token: 0x04001CD7 RID: 7383
	public StateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.BoolParameter hasIncorrectLiquid;

	// Token: 0x04001CD8 RID: 7384
	public StateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.BoolParameter enoughCorrectLiquidToRecover;

	// Token: 0x04001CD9 RID: 7385
	public GameHashes ResourceRecievedEvent = GameHashes.LiquidResourceRecieved;

	// Token: 0x04001CDA RID: 7386
	public GameHashes ResourceDepletedEvent = GameHashes.LiquidResourceEmpty;

	// Token: 0x04001CDB RID: 7387
	public GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.State wild;

	// Token: 0x04001CDC RID: 7388
	public GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.State unfertilizable;

	// Token: 0x04001CDD RID: 7389
	public IrrigationMonitor.ReplantedStates replanted;

	// Token: 0x020013E4 RID: 5092
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06007F6E RID: 32622 RVA: 0x002DC794 File Offset: 0x002DA994
		public List<Descriptor> GetDescriptors(GameObject obj)
		{
			if (this.consumedElements.Length != 0)
			{
				List<Descriptor> list = new List<Descriptor>();
				float preModifiedAttributeValue = obj.GetComponent<Modifiers>().GetPreModifiedAttributeValue(Db.Get().PlantAttributes.FertilizerUsageMod);
				foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in this.consumedElements)
				{
					float num = consumeInfo.massConsumptionRate * preModifiedAttributeValue;
					list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.IDEAL_FERTILIZER, consumeInfo.tag.ProperName(), GameUtil.GetFormattedMass(-num, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.IDEAL_FERTILIZER, consumeInfo.tag.ProperName(), GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
				}
				return list;
			}
			return null;
		}

		// Token: 0x040061EA RID: 25066
		public Tag wrongIrrigationTestTag;

		// Token: 0x040061EB RID: 25067
		public PlantElementAbsorber.ConsumeInfo[] consumedElements;
	}

	// Token: 0x020013E5 RID: 5093
	public class VariableIrrigationStates : GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.State
	{
		// Token: 0x040061EC RID: 25068
		public GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.State normal;

		// Token: 0x040061ED RID: 25069
		public GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.State wrongLiquid;
	}

	// Token: 0x020013E6 RID: 5094
	public class Irrigated : GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.State
	{
		// Token: 0x040061EE RID: 25070
		public IrrigationMonitor.VariableIrrigationStates absorbing;
	}

	// Token: 0x020013E7 RID: 5095
	public class ReplantedStates : GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.State
	{
		// Token: 0x040061EF RID: 25071
		public IrrigationMonitor.Irrigated irrigated;

		// Token: 0x040061F0 RID: 25072
		public IrrigationMonitor.VariableIrrigationStates starved;
	}

	// Token: 0x020013E8 RID: 5096
	public new class Instance : GameStateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.GameInstance, IWiltCause
	{
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06007F73 RID: 32627 RVA: 0x002DC87C File Offset: 0x002DAA7C
		public float total_fertilizer_available
		{
			get
			{
				return this.total_available_mass;
			}
		}

		// Token: 0x06007F74 RID: 32628 RVA: 0x002DC884 File Offset: 0x002DAA84
		public Instance(IStateMachineTarget master, IrrigationMonitor.Def def)
			: base(master, def)
		{
			this.AddAmounts(base.gameObject);
			this.MakeModifiers();
			master.Subscribe(1309017699, new Action<object>(this.SetStorage));
		}

		// Token: 0x06007F75 RID: 32629 RVA: 0x002DC8C3 File Offset: 0x002DAAC3
		public virtual StatusItem GetStarvedStatusItem()
		{
			return Db.Get().CreatureStatusItems.NeedsIrrigation;
		}

		// Token: 0x06007F76 RID: 32630 RVA: 0x002DC8D4 File Offset: 0x002DAAD4
		public virtual StatusItem GetIncorrectLiquidStatusItem()
		{
			return Db.Get().CreatureStatusItems.WrongIrrigation;
		}

		// Token: 0x06007F77 RID: 32631 RVA: 0x002DC8E5 File Offset: 0x002DAAE5
		public virtual StatusItem GetIncorrectLiquidStatusItemMajor()
		{
			return Db.Get().CreatureStatusItems.WrongIrrigationMajor;
		}

		// Token: 0x06007F78 RID: 32632 RVA: 0x002DC8F8 File Offset: 0x002DAAF8
		protected virtual void AddAmounts(GameObject gameObject)
		{
			Amounts amounts = gameObject.GetAmounts();
			this.irrigation = amounts.Add(new AmountInstance(Db.Get().Amounts.Irrigation, gameObject));
		}

		// Token: 0x06007F79 RID: 32633 RVA: 0x002DC930 File Offset: 0x002DAB30
		protected virtual void MakeModifiers()
		{
			this.consumptionRate = new AttributeModifier(Db.Get().Amounts.Irrigation.deltaAttribute.Id, -0.16666667f, CREATURES.STATS.IRRIGATION.CONSUME_MODIFIER, false, false, true);
			this.absorptionRate = new AttributeModifier(Db.Get().Amounts.Irrigation.deltaAttribute.Id, 1.6666666f, CREATURES.STATS.IRRIGATION.ABSORBING_MODIFIER, false, false, true);
		}

		// Token: 0x06007F7A RID: 32634 RVA: 0x002DC9AC File Offset: 0x002DABAC
		public static void DumpIncorrectFertilizers(Storage storage, GameObject go)
		{
			if (storage == null)
			{
				return;
			}
			if (go == null)
			{
				return;
			}
			IrrigationMonitor.Instance smi = go.GetSMI<IrrigationMonitor.Instance>();
			PlantElementAbsorber.ConsumeInfo[] array = null;
			if (smi != null)
			{
				array = smi.def.consumedElements;
			}
			IrrigationMonitor.Instance.DumpIncorrectFertilizers(storage, array, false);
			FertilizationMonitor.Instance smi2 = go.GetSMI<FertilizationMonitor.Instance>();
			PlantElementAbsorber.ConsumeInfo[] array2 = null;
			if (smi2 != null)
			{
				array2 = smi2.def.consumedElements;
			}
			IrrigationMonitor.Instance.DumpIncorrectFertilizers(storage, array2, true);
		}

		// Token: 0x06007F7B RID: 32635 RVA: 0x002DCA10 File Offset: 0x002DAC10
		private static void DumpIncorrectFertilizers(Storage storage, PlantElementAbsorber.ConsumeInfo[] consumed_infos, bool validate_solids)
		{
			if (storage == null)
			{
				return;
			}
			for (int i = storage.items.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = storage.items[i];
				if (!(gameObject == null))
				{
					PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
					if (!(component == null) && !(gameObject.GetComponent<ElementChunk>() == null))
					{
						if (validate_solids)
						{
							if (!component.Element.IsSolid)
							{
								goto IL_C1;
							}
						}
						else if (!component.Element.IsLiquid)
						{
							goto IL_C1;
						}
						bool flag = false;
						KPrefabID component2 = component.GetComponent<KPrefabID>();
						if (consumed_infos != null)
						{
							foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in consumed_infos)
							{
								if (component2.HasTag(consumeInfo.tag))
								{
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							storage.Drop(gameObject, true);
						}
					}
				}
				IL_C1:;
			}
		}

		// Token: 0x06007F7C RID: 32636 RVA: 0x002DCAEC File Offset: 0x002DACEC
		public void SetStorage(object obj)
		{
			this.storage = (Storage)obj;
			base.sm.resourceStorage.Set(this.storage, base.smi);
			IrrigationMonitor.Instance.DumpIncorrectFertilizers(this.storage, base.smi.gameObject);
			foreach (ManualDeliveryKG manualDeliveryKG in base.smi.gameObject.GetComponents<ManualDeliveryKG>())
			{
				bool flag = false;
				foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in base.def.consumedElements)
				{
					if (manualDeliveryKG.RequestedItemTag == consumeInfo.tag)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					manualDeliveryKG.SetStorage(this.storage);
					manualDeliveryKG.enabled = !this.storage.gameObject.GetComponent<PlantablePlot>().has_liquid_pipe_input;
				}
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06007F7D RID: 32637 RVA: 0x002DCBCC File Offset: 0x002DADCC
		public WiltCondition.Condition[] Conditions
		{
			get
			{
				return new WiltCondition.Condition[] { WiltCondition.Condition.Irrigation };
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06007F7E RID: 32638 RVA: 0x002DCBD8 File Offset: 0x002DADD8
		public string WiltStateString
		{
			get
			{
				string text = "";
				if (base.smi.IsInsideState(base.smi.sm.replanted.irrigated.absorbing.wrongLiquid))
				{
					text = this.GetIncorrectLiquidStatusItem().resolveStringCallback(CREATURES.STATUSITEMS.WRONGIRRIGATION.NAME, this);
				}
				else if (base.smi.IsInsideState(base.smi.sm.replanted.starved))
				{
					text = this.GetStarvedStatusItem().resolveStringCallback(CREATURES.STATUSITEMS.NEEDSIRRIGATION.NAME, this);
				}
				else if (base.smi.IsInsideState(base.smi.sm.replanted.starved.wrongLiquid))
				{
					text = this.GetIncorrectLiquidStatusItemMajor().resolveStringCallback(CREATURES.STATUSITEMS.WRONGIRRIGATIONMAJOR.NAME, this);
				}
				return text;
			}
		}

		// Token: 0x06007F7F RID: 32639 RVA: 0x002DCCBC File Offset: 0x002DAEBC
		public virtual bool AcceptsLiquid()
		{
			PlantablePlot component = base.sm.resourceStorage.Get(this).GetComponent<PlantablePlot>();
			return component != null && component.AcceptsIrrigation;
		}

		// Token: 0x06007F80 RID: 32640 RVA: 0x002DCCF1 File Offset: 0x002DAEF1
		public bool Starved()
		{
			return this.irrigation.value == 0f;
		}

		// Token: 0x06007F81 RID: 32641 RVA: 0x002DCD08 File Offset: 0x002DAF08
		public void UpdateIrrigation(float dt)
		{
			if (base.def.consumedElements == null)
			{
				return;
			}
			Storage storage = base.sm.resourceStorage.Get<Storage>(base.smi);
			bool flag = true;
			bool flag2 = false;
			bool flag3 = true;
			if (storage != null)
			{
				List<GameObject> items = storage.items;
				for (int i = 0; i < base.def.consumedElements.Length; i++)
				{
					float num = 0f;
					PlantElementAbsorber.ConsumeInfo consumeInfo = base.def.consumedElements[i];
					for (int j = 0; j < items.Count; j++)
					{
						GameObject gameObject = items[j];
						if (gameObject.HasTag(consumeInfo.tag))
						{
							num += gameObject.GetComponent<PrimaryElement>().Mass;
						}
						else if (gameObject.HasTag(base.def.wrongIrrigationTestTag))
						{
							flag2 = true;
						}
					}
					this.total_available_mass = num;
					float totalValue = base.gameObject.GetAttributes().Get(Db.Get().PlantAttributes.FertilizerUsageMod).GetTotalValue();
					if (num < consumeInfo.massConsumptionRate * totalValue * dt)
					{
						flag = false;
						break;
					}
					if (num < consumeInfo.massConsumptionRate * totalValue * (dt * 30f))
					{
						flag3 = false;
						break;
					}
				}
			}
			else
			{
				flag = false;
				flag3 = false;
				flag2 = false;
			}
			base.sm.hasCorrectLiquid.Set(flag, base.smi, false);
			base.sm.hasIncorrectLiquid.Set(flag2, base.smi, false);
			base.sm.enoughCorrectLiquidToRecover.Set(flag3 && flag, base.smi, false);
		}

		// Token: 0x06007F82 RID: 32642 RVA: 0x002DCE9C File Offset: 0x002DB09C
		public void UpdateAbsorbing(bool allow)
		{
			bool flag = allow && !base.smi.gameObject.HasTag(GameTags.Wilting);
			if (flag != this.absorberHandle.IsValid())
			{
				if (flag)
				{
					if (base.def.consumedElements == null || base.def.consumedElements.Length == 0)
					{
						return;
					}
					float totalValue = base.gameObject.GetAttributes().Get(Db.Get().PlantAttributes.FertilizerUsageMod).GetTotalValue();
					PlantElementAbsorber.ConsumeInfo[] array = new PlantElementAbsorber.ConsumeInfo[base.def.consumedElements.Length];
					for (int i = 0; i < base.def.consumedElements.Length; i++)
					{
						PlantElementAbsorber.ConsumeInfo consumeInfo = base.def.consumedElements[i];
						consumeInfo.massConsumptionRate *= totalValue;
						array[i] = consumeInfo;
					}
					this.absorberHandle = Game.Instance.plantElementAbsorbers.Add(this.storage, array);
					return;
				}
				else
				{
					this.absorberHandle = Game.Instance.plantElementAbsorbers.Remove(this.absorberHandle);
				}
			}
		}

		// Token: 0x040061F1 RID: 25073
		public AttributeModifier consumptionRate;

		// Token: 0x040061F2 RID: 25074
		public AttributeModifier absorptionRate;

		// Token: 0x040061F3 RID: 25075
		protected AmountInstance irrigation;

		// Token: 0x040061F4 RID: 25076
		private float total_available_mass;

		// Token: 0x040061F5 RID: 25077
		private Storage storage;

		// Token: 0x040061F6 RID: 25078
		private HandleVector<int>.Handle absorberHandle = HandleVector<int>.InvalidHandle;
	}
}
