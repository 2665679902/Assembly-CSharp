using System;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000465 RID: 1125
public class CreatureCalorieMonitor : GameStateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>
{
	// Token: 0x060018F5 RID: 6389 RVA: 0x0008545C File Offset: 0x0008365C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.normal;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.root.EventHandler(GameHashes.CaloriesConsumed, delegate(CreatureCalorieMonitor.Instance smi, object data)
		{
			smi.OnCaloriesConsumed(data);
		}).ToggleBehaviour(GameTags.Creatures.Poop, new StateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.Transition.ConditionCallback(CreatureCalorieMonitor.ReadyToPoop), delegate(CreatureCalorieMonitor.Instance smi)
		{
			smi.Poop();
		}).Update(new Action<CreatureCalorieMonitor.Instance, float>(CreatureCalorieMonitor.UpdateMetabolismCalorieModifier), UpdateRate.SIM_200ms, false);
		this.normal.Transition(this.hungry, (CreatureCalorieMonitor.Instance smi) => smi.IsHungry(), UpdateRate.SIM_1000ms);
		this.hungry.DefaultState(this.hungry.hungry).ToggleTag(GameTags.Creatures.Hungry).EventTransition(GameHashes.CaloriesConsumed, this.normal, (CreatureCalorieMonitor.Instance smi) => !smi.IsHungry());
		this.hungry.hungry.Transition(this.normal, (CreatureCalorieMonitor.Instance smi) => !smi.IsHungry(), UpdateRate.SIM_1000ms).Transition(this.hungry.outofcalories, (CreatureCalorieMonitor.Instance smi) => smi.IsOutOfCalories(), UpdateRate.SIM_1000ms).ToggleStatusItem(Db.Get().CreatureStatusItems.Hungry, null);
		this.hungry.outofcalories.DefaultState(this.hungry.outofcalories.wild).Transition(this.hungry.hungry, (CreatureCalorieMonitor.Instance smi) => !smi.IsOutOfCalories(), UpdateRate.SIM_1000ms);
		this.hungry.outofcalories.wild.TagTransition(GameTags.Creatures.Wild, this.hungry.outofcalories.tame, true).ToggleStatusItem(Db.Get().CreatureStatusItems.Hungry, null);
		this.hungry.outofcalories.tame.Enter("StarvationStartTime", new StateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.State.Callback(CreatureCalorieMonitor.StarvationStartTime)).Exit("ClearStarvationTime", delegate(CreatureCalorieMonitor.Instance smi)
		{
			this.starvationStartTime.Set(0f, smi, false);
		}).Transition(this.hungry.outofcalories.starvedtodeath, (CreatureCalorieMonitor.Instance smi) => smi.GetDeathTimeRemaining() <= 0f, UpdateRate.SIM_1000ms)
			.TagTransition(GameTags.Creatures.Wild, this.hungry.outofcalories.wild, false)
			.ToggleStatusItem(STRINGS.CREATURES.STATUSITEMS.STARVING.NAME, STRINGS.CREATURES.STATUSITEMS.STARVING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.BadMinor, false, default(HashedString), 129022, (string str, CreatureCalorieMonitor.Instance smi) => str.Replace("{TimeUntilDeath}", GameUtil.GetFormattedCycles(smi.GetDeathTimeRemaining(), "F1", false)), null, null)
			.ToggleNotification((CreatureCalorieMonitor.Instance smi) => new Notification(STRINGS.CREATURES.STATUSITEMS.STARVING.NOTIFICATION_NAME, NotificationType.BadMinor, (List<Notification> notifications, object data) => STRINGS.CREATURES.STATUSITEMS.STARVING.NOTIFICATION_TOOLTIP + notifications.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false))
			.ToggleEffect((CreatureCalorieMonitor.Instance smi) => this.outOfCaloriesTame);
		this.hungry.outofcalories.starvedtodeath.Enter(delegate(CreatureCalorieMonitor.Instance smi)
		{
			smi.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Starvation);
		});
		this.outOfCaloriesTame = new Effect("OutOfCaloriesTame", STRINGS.CREATURES.MODIFIERS.OUT_OF_CALORIES.NAME, STRINGS.CREATURES.MODIFIERS.OUT_OF_CALORIES.TOOLTIP, 0f, false, false, false, null, -1f, 0f, null, "");
		this.outOfCaloriesTame.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -10f, STRINGS.CREATURES.MODIFIERS.OUT_OF_CALORIES.NAME, false, false, true));
	}

	// Token: 0x060018F6 RID: 6390 RVA: 0x0008583A File Offset: 0x00083A3A
	private static bool ReadyToPoop(CreatureCalorieMonitor.Instance smi)
	{
		return smi.stomach.IsReadyToPoop() && Time.time - smi.lastMealOrPoopTime >= smi.def.minimumTimeBeforePooping;
	}

	// Token: 0x060018F7 RID: 6391 RVA: 0x00085867 File Offset: 0x00083A67
	private static void UpdateMetabolismCalorieModifier(CreatureCalorieMonitor.Instance smi, float dt)
	{
		smi.deltaCalorieMetabolismModifier.SetValue(1f - smi.metabolism.GetTotalValue() / 100f);
	}

	// Token: 0x060018F8 RID: 6392 RVA: 0x0008588B File Offset: 0x00083A8B
	private static void StarvationStartTime(CreatureCalorieMonitor.Instance smi)
	{
		if (smi.sm.starvationStartTime.Get(smi) == 0f)
		{
			smi.sm.starvationStartTime.Set(GameClock.Instance.GetTime(), smi, false);
		}
	}

	// Token: 0x04000DF7 RID: 3575
	public GameStateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.State normal;

	// Token: 0x04000DF8 RID: 3576
	private CreatureCalorieMonitor.HungryStates hungry;

	// Token: 0x04000DF9 RID: 3577
	private Effect outOfCaloriesTame;

	// Token: 0x04000DFA RID: 3578
	public StateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.FloatParameter starvationStartTime;

	// Token: 0x02001092 RID: 4242
	public struct CaloriesConsumedEvent
	{
		// Token: 0x04005808 RID: 22536
		public Tag tag;

		// Token: 0x04005809 RID: 22537
		public float calories;
	}

	// Token: 0x02001093 RID: 4243
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06007392 RID: 29586 RVA: 0x002B08BB File Offset: 0x002AEABB
		public override void Configure(GameObject prefab)
		{
			prefab.GetComponent<Modifiers>().initialAmounts.Add(Db.Get().Amounts.Calories.Id);
		}

		// Token: 0x06007393 RID: 29587 RVA: 0x002B08E4 File Offset: 0x002AEAE4
		public List<Descriptor> GetDescriptors(GameObject obj)
		{
			List<Descriptor> list = new List<Descriptor>();
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.DIET_HEADER, UI.BUILDINGEFFECTS.TOOLTIPS.DIET_HEADER, Descriptor.DescriptorType.Effect, false));
			float dailyPlantGrowthConsumption = 1f;
			if (this.diet.consumedTags.Count > 0)
			{
				float calorie_loss_per_second = 0f;
				foreach (AttributeModifier attributeModifier in Db.Get().traits.Get(obj.GetComponent<Modifiers>().initialTraits[0]).SelfModifiers)
				{
					if (attributeModifier.AttributeId == Db.Get().Amounts.Calories.deltaAttribute.Id)
					{
						calorie_loss_per_second = attributeModifier.Value;
					}
				}
				string text = string.Join(", ", this.diet.consumedTags.Select((KeyValuePair<Tag, float> t) => t.Key.ProperName()).ToArray<string>());
				string text2;
				if (this.diet.eatsPlantsDirectly)
				{
					text2 = string.Join("\n", this.diet.consumedTags.Select(delegate(KeyValuePair<Tag, float> t)
					{
						dailyPlantGrowthConsumption = -calorie_loss_per_second / t.Value;
						GameObject prefab = Assets.GetPrefab(t.Key.ToString());
						Crop crop = prefab.GetComponent<Crop>();
						float num = CROPS.CROP_TYPES.Find((Crop.CropVal m) => m.cropId == crop.cropId).cropDuration / 600f;
						float num2 = 1f / num;
						return UI.BUILDINGEFFECTS.DIET_CONSUMED_ITEM.text.Replace("{Food}", t.Key.ProperName()).Replace("{Amount}", GameUtil.GetFormattedPlantGrowth(-calorie_loss_per_second / t.Value * num2 * 100f, GameUtil.TimeSlice.PerCycle));
					}).ToArray<string>());
				}
				else
				{
					text2 = string.Join("\n", this.diet.consumedTags.Select((KeyValuePair<Tag, float> t) => UI.BUILDINGEFFECTS.DIET_CONSUMED_ITEM.text.Replace("{Food}", t.Key.ProperName()).Replace("{Amount}", GameUtil.GetFormattedMass(-calorie_loss_per_second / t.Value, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"))).ToArray<string>());
				}
				list.Add(new Descriptor(UI.BUILDINGEFFECTS.DIET_CONSUMED.text.Replace("{Foodlist}", text), UI.BUILDINGEFFECTS.TOOLTIPS.DIET_CONSUMED.text.Replace("{Foodlist}", text2), Descriptor.DescriptorType.Effect, false));
			}
			if (this.diet.producedTags.Count > 0)
			{
				string text3 = string.Join(", ", this.diet.producedTags.Select((KeyValuePair<Tag, float> t) => t.Key.ProperName()).ToArray<string>());
				string text4;
				if (this.diet.eatsPlantsDirectly)
				{
					text4 = string.Join("\n", this.diet.producedTags.Select((KeyValuePair<Tag, float> t) => UI.BUILDINGEFFECTS.DIET_PRODUCED_ITEM_FROM_PLANT.text.Replace("{Item}", t.Key.ProperName()).Replace("{Amount}", GameUtil.GetFormattedMass(t.Value * dailyPlantGrowthConsumption, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"))).ToArray<string>());
				}
				else
				{
					text4 = string.Join("\n", this.diet.producedTags.Select((KeyValuePair<Tag, float> t) => UI.BUILDINGEFFECTS.DIET_PRODUCED_ITEM.text.Replace("{Item}", t.Key.ProperName()).Replace("{Percent}", GameUtil.GetFormattedPercent(t.Value * 100f, GameUtil.TimeSlice.None))).ToArray<string>());
				}
				list.Add(new Descriptor(UI.BUILDINGEFFECTS.DIET_PRODUCED.text.Replace("{Items}", text3), UI.BUILDINGEFFECTS.TOOLTIPS.DIET_PRODUCED.text.Replace("{Items}", text4), Descriptor.DescriptorType.Effect, false));
			}
			return list;
		}

		// Token: 0x0400580A RID: 22538
		public Diet diet;

		// Token: 0x0400580B RID: 22539
		public float minPoopSizeInCalories = 100f;

		// Token: 0x0400580C RID: 22540
		public float minimumTimeBeforePooping = 10f;

		// Token: 0x0400580D RID: 22541
		public float deathTimer = 6000f;

		// Token: 0x0400580E RID: 22542
		public bool storePoop;
	}

	// Token: 0x02001094 RID: 4244
	public class HungryStates : GameStateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.State
	{
		// Token: 0x0400580F RID: 22543
		public GameStateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.State hungry;

		// Token: 0x04005810 RID: 22544
		public CreatureCalorieMonitor.HungryStates.OutOfCaloriesState outofcalories;

		// Token: 0x02001F6D RID: 8045
		public class OutOfCaloriesState : GameStateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.State
		{
			// Token: 0x04008BB6 RID: 35766
			public GameStateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.State wild;

			// Token: 0x04008BB7 RID: 35767
			public GameStateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.State tame;

			// Token: 0x04008BB8 RID: 35768
			public GameStateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.State starvedtodeath;
		}
	}

	// Token: 0x02001095 RID: 4245
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Stomach
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06007396 RID: 29590 RVA: 0x002B0C19 File Offset: 0x002AEE19
		// (set) Token: 0x06007397 RID: 29591 RVA: 0x002B0C21 File Offset: 0x002AEE21
		public Diet diet { get; private set; }

		// Token: 0x06007398 RID: 29592 RVA: 0x002B0C2A File Offset: 0x002AEE2A
		public Stomach(Diet diet, GameObject owner, float min_poop_size_in_calories, bool storePoop)
		{
			this.diet = diet;
			this.owner = owner;
			this.minPoopSizeInCalories = min_poop_size_in_calories;
			this.storePoop = storePoop;
		}

		// Token: 0x06007399 RID: 29593 RVA: 0x002B0C5C File Offset: 0x002AEE5C
		public void Poop()
		{
			float num = 0f;
			Tag tag = Tag.Invalid;
			byte b = byte.MaxValue;
			int num2 = 0;
			bool flag = false;
			for (int i = 0; i < this.caloriesConsumed.Count; i++)
			{
				CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry caloriesConsumedEntry = this.caloriesConsumed[i];
				if (caloriesConsumedEntry.calories > 0f)
				{
					Diet.Info dietInfo = this.diet.GetDietInfo(caloriesConsumedEntry.tag);
					if (dietInfo != null && (!(tag != Tag.Invalid) || !(tag != dietInfo.producedElement)))
					{
						num += dietInfo.ConvertConsumptionMassToProducedMass(dietInfo.ConvertCaloriesToConsumptionMass(caloriesConsumedEntry.calories));
						tag = dietInfo.producedElement;
						b = dietInfo.diseaseIdx;
						num2 = (int)(dietInfo.diseasePerKgProduced * num);
						caloriesConsumedEntry.calories = 0f;
						this.caloriesConsumed[i] = caloriesConsumedEntry;
						flag = flag || dietInfo.produceSolidTile;
					}
				}
			}
			if (num <= 0f || tag == Tag.Invalid)
			{
				return;
			}
			Element element = ElementLoader.GetElement(tag);
			global::Debug.Assert(element != null, "TODO: implement non-element tag spawning");
			int num3 = Grid.PosToCell(this.owner.transform.GetPosition());
			float temperature = this.owner.GetComponent<PrimaryElement>().Temperature;
			DebugUtil.DevAssert(!this.storePoop || !flag, "Stomach cannot both store poop & create a solid tile.", null);
			if (this.storePoop)
			{
				Storage component = this.owner.GetComponent<Storage>();
				if (element.IsLiquid)
				{
					component.AddLiquid(element.id, num, temperature, b, num2, false, true);
				}
				else if (element.IsGas)
				{
					component.AddGasChunk(element.id, num, temperature, b, num2, false, true);
				}
				else
				{
					component.AddOre(element.id, num, temperature, b, num2, false, true);
				}
			}
			else if (element.IsLiquid)
			{
				FallingWater.instance.AddParticle(num3, element.idx, num, temperature, b, num2, true, false, false, false);
			}
			else if (element.IsGas)
			{
				SimMessages.AddRemoveSubstance(num3, element.idx, CellEventLogger.Instance.ElementConsumerSimUpdate, num, temperature, b, num2, true, -1);
			}
			else if (flag)
			{
				int num4 = this.owner.GetComponent<Facing>().GetFrontCell();
				if (!Grid.IsValidCell(num4))
				{
					global::Debug.LogWarningFormat("{0} attemping to Poop {1} on invalid cell {2} from cell {3}", new object[] { this.owner, element.name, num4, num3 });
					num4 = num3;
				}
				SimMessages.AddRemoveSubstance(num4, element.idx, CellEventLogger.Instance.ElementConsumerSimUpdate, num, temperature, b, num2, true, -1);
			}
			else
			{
				element.substance.SpawnResource(Grid.CellToPosCCC(num3, Grid.SceneLayer.Ore), num, temperature, b, num2, false, false, false);
			}
			KPrefabID component2 = this.owner.GetComponent<KPrefabID>();
			if (!Game.Instance.savedInfo.creaturePoopAmount.ContainsKey(component2.PrefabTag))
			{
				Game.Instance.savedInfo.creaturePoopAmount.Add(component2.PrefabTag, 0f);
			}
			Dictionary<Tag, float> creaturePoopAmount = Game.Instance.savedInfo.creaturePoopAmount;
			Tag prefabTag = component2.PrefabTag;
			creaturePoopAmount[prefabTag] += num;
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, element.name, this.owner.transform, 1.5f, false);
		}

		// Token: 0x0600739A RID: 29594 RVA: 0x002B0FCC File Offset: 0x002AF1CC
		public List<CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry> GetCalorieEntries()
		{
			return this.caloriesConsumed;
		}

		// Token: 0x0600739B RID: 29595 RVA: 0x002B0FD4 File Offset: 0x002AF1D4
		public float GetTotalConsumedCalories()
		{
			float num = 0f;
			foreach (CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry caloriesConsumedEntry in this.caloriesConsumed)
			{
				if (caloriesConsumedEntry.calories > 0f)
				{
					Diet.Info dietInfo = this.diet.GetDietInfo(caloriesConsumedEntry.tag);
					if (dietInfo != null && !(dietInfo.producedElement == Tag.Invalid))
					{
						num += caloriesConsumedEntry.calories;
					}
				}
			}
			return num;
		}

		// Token: 0x0600739C RID: 29596 RVA: 0x002B1064 File Offset: 0x002AF264
		public float GetFullness()
		{
			return this.GetTotalConsumedCalories() / this.minPoopSizeInCalories;
		}

		// Token: 0x0600739D RID: 29597 RVA: 0x002B1074 File Offset: 0x002AF274
		public bool IsReadyToPoop()
		{
			float totalConsumedCalories = this.GetTotalConsumedCalories();
			return totalConsumedCalories > 0f && totalConsumedCalories >= this.minPoopSizeInCalories;
		}

		// Token: 0x0600739E RID: 29598 RVA: 0x002B10A0 File Offset: 0x002AF2A0
		public void Consume(Tag tag, float calories)
		{
			for (int i = 0; i < this.caloriesConsumed.Count; i++)
			{
				CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry caloriesConsumedEntry = this.caloriesConsumed[i];
				if (caloriesConsumedEntry.tag == tag)
				{
					caloriesConsumedEntry.calories += calories;
					this.caloriesConsumed[i] = caloriesConsumedEntry;
					return;
				}
			}
			CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry caloriesConsumedEntry2 = default(CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry);
			caloriesConsumedEntry2.tag = tag;
			caloriesConsumedEntry2.calories = calories;
			this.caloriesConsumed.Add(caloriesConsumedEntry2);
		}

		// Token: 0x0600739F RID: 29599 RVA: 0x002B111C File Offset: 0x002AF31C
		public Tag GetNextPoopEntry()
		{
			for (int i = 0; i < this.caloriesConsumed.Count; i++)
			{
				CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry caloriesConsumedEntry = this.caloriesConsumed[i];
				if (caloriesConsumedEntry.calories > 0f)
				{
					Diet.Info dietInfo = this.diet.GetDietInfo(caloriesConsumedEntry.tag);
					if (dietInfo != null && !(dietInfo.producedElement == Tag.Invalid))
					{
						return dietInfo.producedElement;
					}
				}
			}
			return Tag.Invalid;
		}

		// Token: 0x04005811 RID: 22545
		[Serialize]
		private List<CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry> caloriesConsumed = new List<CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry>();

		// Token: 0x04005812 RID: 22546
		private float minPoopSizeInCalories;

		// Token: 0x04005814 RID: 22548
		private GameObject owner;

		// Token: 0x04005815 RID: 22549
		private bool storePoop;

		// Token: 0x02001F6E RID: 8046
		[Serializable]
		public struct CaloriesConsumedEntry
		{
			// Token: 0x04008BB9 RID: 35769
			public Tag tag;

			// Token: 0x04008BBA RID: 35770
			public float calories;
		}
	}

	// Token: 0x02001096 RID: 4246
	public new class Instance : GameStateMachine<CreatureCalorieMonitor, CreatureCalorieMonitor.Instance, IStateMachineTarget, CreatureCalorieMonitor.Def>.GameInstance
	{
		// Token: 0x060073A0 RID: 29600 RVA: 0x002B118C File Offset: 0x002AF38C
		public Instance(IStateMachineTarget master, CreatureCalorieMonitor.Def def)
			: base(master, def)
		{
			this.calories = Db.Get().Amounts.Calories.Lookup(base.gameObject);
			this.calories.value = this.calories.GetMax() * 0.9f;
			this.stomach = new CreatureCalorieMonitor.Stomach(def.diet, master.gameObject, def.minPoopSizeInCalories, def.storePoop);
			this.metabolism = base.gameObject.GetAttributes().Add(Db.Get().CritterAttributes.Metabolism);
			this.deltaCalorieMetabolismModifier = new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, 1f, DUPLICANTS.MODIFIERS.METABOLISM_CALORIE_MODIFIER.NAME, true, false, false);
			this.calories.deltaAttribute.Add(this.deltaCalorieMetabolismModifier);
		}

		// Token: 0x060073A1 RID: 29601 RVA: 0x002B1274 File Offset: 0x002AF474
		public void OnCaloriesConsumed(object data)
		{
			CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = (CreatureCalorieMonitor.CaloriesConsumedEvent)data;
			this.calories.value += caloriesConsumedEvent.calories;
			this.stomach.Consume(caloriesConsumedEvent.tag, caloriesConsumedEvent.calories);
			this.lastMealOrPoopTime = Time.time;
		}

		// Token: 0x060073A2 RID: 29602 RVA: 0x002B12C2 File Offset: 0x002AF4C2
		public float GetDeathTimeRemaining()
		{
			return base.smi.def.deathTimer - (GameClock.Instance.GetTime() - base.sm.starvationStartTime.Get(base.smi));
		}

		// Token: 0x060073A3 RID: 29603 RVA: 0x002B12F6 File Offset: 0x002AF4F6
		public void Poop()
		{
			this.lastMealOrPoopTime = Time.time;
			this.stomach.Poop();
		}

		// Token: 0x060073A4 RID: 29604 RVA: 0x002B130E File Offset: 0x002AF50E
		public float GetCalories0to1()
		{
			return this.calories.value / this.calories.GetMax();
		}

		// Token: 0x060073A5 RID: 29605 RVA: 0x002B1327 File Offset: 0x002AF527
		public bool IsHungry()
		{
			return this.GetCalories0to1() < 0.9f;
		}

		// Token: 0x060073A6 RID: 29606 RVA: 0x002B1336 File Offset: 0x002AF536
		public bool IsOutOfCalories()
		{
			return this.GetCalories0to1() <= 0f;
		}

		// Token: 0x04005816 RID: 22550
		public const float HUNGRY_RATIO = 0.9f;

		// Token: 0x04005817 RID: 22551
		public AmountInstance calories;

		// Token: 0x04005818 RID: 22552
		[Serialize]
		public CreatureCalorieMonitor.Stomach stomach;

		// Token: 0x04005819 RID: 22553
		public float lastMealOrPoopTime;

		// Token: 0x0400581A RID: 22554
		public AttributeInstance metabolism;

		// Token: 0x0400581B RID: 22555
		public AttributeModifier deltaCalorieMetabolismModifier;
	}
}
