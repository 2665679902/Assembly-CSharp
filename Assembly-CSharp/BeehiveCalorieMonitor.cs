using System;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000460 RID: 1120
public class BeehiveCalorieMonitor : GameStateMachine<BeehiveCalorieMonitor, BeehiveCalorieMonitor.Instance, IStateMachineTarget, BeehiveCalorieMonitor.Def>
{
	// Token: 0x060018E5 RID: 6373 RVA: 0x00084E1C File Offset: 0x0008301C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.normal;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.root.EventHandler(GameHashes.CaloriesConsumed, delegate(BeehiveCalorieMonitor.Instance smi, object data)
		{
			smi.OnCaloriesConsumed(data);
		}).ToggleBehaviour(GameTags.Creatures.Poop, new StateMachine<BeehiveCalorieMonitor, BeehiveCalorieMonitor.Instance, IStateMachineTarget, BeehiveCalorieMonitor.Def>.Transition.ConditionCallback(BeehiveCalorieMonitor.ReadyToPoop), delegate(BeehiveCalorieMonitor.Instance smi)
		{
			smi.Poop();
		}).Update(new Action<BeehiveCalorieMonitor.Instance, float>(BeehiveCalorieMonitor.UpdateMetabolismCalorieModifier), UpdateRate.SIM_200ms, false);
		this.normal.Transition(this.hungry, (BeehiveCalorieMonitor.Instance smi) => smi.IsHungry(), UpdateRate.SIM_1000ms);
		this.hungry.ToggleTag(GameTags.Creatures.Hungry).EventTransition(GameHashes.CaloriesConsumed, this.normal, (BeehiveCalorieMonitor.Instance smi) => !smi.IsHungry()).ToggleStatusItem(Db.Get().CreatureStatusItems.HiveHungry, null)
			.Transition(this.normal, (BeehiveCalorieMonitor.Instance smi) => !smi.IsHungry(), UpdateRate.SIM_1000ms);
	}

	// Token: 0x060018E6 RID: 6374 RVA: 0x00084F62 File Offset: 0x00083162
	private static bool ReadyToPoop(BeehiveCalorieMonitor.Instance smi)
	{
		return smi.stomach.IsReadyToPoop() && Time.time - smi.lastMealOrPoopTime >= smi.def.minimumTimeBeforePooping;
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x00084F8F File Offset: 0x0008318F
	private static void UpdateMetabolismCalorieModifier(BeehiveCalorieMonitor.Instance smi, float dt)
	{
		smi.deltaCalorieMetabolismModifier.SetValue(1f - smi.metabolism.GetTotalValue() / 100f);
	}

	// Token: 0x04000DEB RID: 3563
	public GameStateMachine<BeehiveCalorieMonitor, BeehiveCalorieMonitor.Instance, IStateMachineTarget, BeehiveCalorieMonitor.Def>.State normal;

	// Token: 0x04000DEC RID: 3564
	public GameStateMachine<BeehiveCalorieMonitor, BeehiveCalorieMonitor.Instance, IStateMachineTarget, BeehiveCalorieMonitor.Def>.State hungry;

	// Token: 0x02001083 RID: 4227
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06007344 RID: 29508 RVA: 0x002AFB90 File Offset: 0x002ADD90
		public override void Configure(GameObject prefab)
		{
			prefab.GetComponent<Modifiers>().initialAmounts.Add(Db.Get().Amounts.Calories.Id);
		}

		// Token: 0x06007345 RID: 29509 RVA: 0x002AFBB8 File Offset: 0x002ADDB8
		public List<Descriptor> GetDescriptors(GameObject obj)
		{
			List<Descriptor> list = new List<Descriptor>();
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.DIET_HEADER, UI.BUILDINGEFFECTS.TOOLTIPS.DIET_HEADER, Descriptor.DescriptorType.Effect, false));
			float calorie_loss_per_second = 0f;
			foreach (AttributeModifier attributeModifier in Db.Get().traits.Get(obj.GetComponent<Modifiers>().initialTraits[0]).SelfModifiers)
			{
				if (attributeModifier.AttributeId == Db.Get().Amounts.Calories.deltaAttribute.Id)
				{
					calorie_loss_per_second = attributeModifier.Value;
				}
			}
			string text = string.Join(", ", this.diet.consumedTags.Select((KeyValuePair<Tag, float> t) => t.Key.ProperName()).ToArray<string>());
			string text2 = string.Join("\n", this.diet.consumedTags.Select((KeyValuePair<Tag, float> t) => UI.BUILDINGEFFECTS.DIET_CONSUMED_ITEM.text.Replace("{Food}", t.Key.ProperName()).Replace("{Amount}", GameUtil.GetFormattedMass(-calorie_loss_per_second / t.Value, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"))).ToArray<string>());
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.DIET_CONSUMED.text.Replace("{Foodlist}", text), UI.BUILDINGEFFECTS.TOOLTIPS.DIET_CONSUMED.text.Replace("{Foodlist}", text2), Descriptor.DescriptorType.Effect, false));
			string text3 = string.Join(", ", this.diet.producedTags.Select((KeyValuePair<Tag, float> t) => t.Key.ProperName()).ToArray<string>());
			string text4 = string.Join("\n", this.diet.producedTags.Select((KeyValuePair<Tag, float> t) => UI.BUILDINGEFFECTS.DIET_PRODUCED_ITEM.text.Replace("{Item}", t.Key.ProperName()).Replace("{Percent}", GameUtil.GetFormattedPercent(t.Value * 100f, GameUtil.TimeSlice.None))).ToArray<string>());
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.DIET_PRODUCED.text.Replace("{Items}", text3), UI.BUILDINGEFFECTS.TOOLTIPS.DIET_PRODUCED.text.Replace("{Items}", text4), Descriptor.DescriptorType.Effect, false));
			return list;
		}

		// Token: 0x040057D1 RID: 22481
		public Diet diet;

		// Token: 0x040057D2 RID: 22482
		public float minPoopSizeInCalories = 100f;

		// Token: 0x040057D3 RID: 22483
		public float minimumTimeBeforePooping = 10f;

		// Token: 0x040057D4 RID: 22484
		public bool storePoop = true;
	}

	// Token: 0x02001084 RID: 4228
	public new class Instance : GameStateMachine<BeehiveCalorieMonitor, BeehiveCalorieMonitor.Instance, IStateMachineTarget, BeehiveCalorieMonitor.Def>.GameInstance
	{
		// Token: 0x06007347 RID: 29511 RVA: 0x002AFE18 File Offset: 0x002AE018
		public Instance(IStateMachineTarget master, BeehiveCalorieMonitor.Def def)
			: base(master, def)
		{
			this.calories = Db.Get().Amounts.Calories.Lookup(base.gameObject);
			this.calories.value = this.calories.GetMax() * 0.9f;
			this.stomach = new CreatureCalorieMonitor.Stomach(def.diet, master.gameObject, def.minPoopSizeInCalories, def.storePoop);
			this.metabolism = base.gameObject.GetAttributes().Add(Db.Get().CritterAttributes.Metabolism);
			this.deltaCalorieMetabolismModifier = new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, 1f, DUPLICANTS.MODIFIERS.METABOLISM_CALORIE_MODIFIER.NAME, true, false, false);
			this.calories.deltaAttribute.Add(this.deltaCalorieMetabolismModifier);
		}

		// Token: 0x06007348 RID: 29512 RVA: 0x002AFF00 File Offset: 0x002AE100
		public void OnCaloriesConsumed(object data)
		{
			CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = (CreatureCalorieMonitor.CaloriesConsumedEvent)data;
			this.calories.value += caloriesConsumedEvent.calories;
			this.stomach.Consume(caloriesConsumedEvent.tag, caloriesConsumedEvent.calories);
			this.lastMealOrPoopTime = Time.time;
		}

		// Token: 0x06007349 RID: 29513 RVA: 0x002AFF4E File Offset: 0x002AE14E
		public void Poop()
		{
			this.lastMealOrPoopTime = Time.time;
			this.stomach.Poop();
		}

		// Token: 0x0600734A RID: 29514 RVA: 0x002AFF66 File Offset: 0x002AE166
		public float GetCalories0to1()
		{
			return this.calories.value / this.calories.GetMax();
		}

		// Token: 0x0600734B RID: 29515 RVA: 0x002AFF7F File Offset: 0x002AE17F
		public bool IsHungry()
		{
			return this.GetCalories0to1() < 0.9f;
		}

		// Token: 0x040057D5 RID: 22485
		public const float HUNGRY_RATIO = 0.9f;

		// Token: 0x040057D6 RID: 22486
		public AmountInstance calories;

		// Token: 0x040057D7 RID: 22487
		[Serialize]
		public CreatureCalorieMonitor.Stomach stomach;

		// Token: 0x040057D8 RID: 22488
		public float lastMealOrPoopTime;

		// Token: 0x040057D9 RID: 22489
		public AttributeInstance metabolism;

		// Token: 0x040057DA RID: 22490
		public AttributeModifier deltaCalorieMetabolismModifier;
	}
}
