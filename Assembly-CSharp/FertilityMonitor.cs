using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Klei;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006D5 RID: 1749
public class FertilityMonitor : GameStateMachine<FertilityMonitor, FertilityMonitor.Instance, IStateMachineTarget, FertilityMonitor.Def>
{
	// Token: 0x06002F93 RID: 12179 RVA: 0x000FB404 File Offset: 0x000F9604
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.fertile;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.root.DefaultState(this.fertile);
		this.fertile.ToggleBehaviour(GameTags.Creatures.Fertile, (FertilityMonitor.Instance smi) => smi.IsReadyToLayEgg(), null).ToggleEffect((FertilityMonitor.Instance smi) => smi.fertileEffect).Transition(this.infertile, GameStateMachine<FertilityMonitor, FertilityMonitor.Instance, IStateMachineTarget, FertilityMonitor.Def>.Not(new StateMachine<FertilityMonitor, FertilityMonitor.Instance, IStateMachineTarget, FertilityMonitor.Def>.Transition.ConditionCallback(FertilityMonitor.IsFertile)), UpdateRate.SIM_1000ms);
		this.infertile.Transition(this.fertile, new StateMachine<FertilityMonitor, FertilityMonitor.Instance, IStateMachineTarget, FertilityMonitor.Def>.Transition.ConditionCallback(FertilityMonitor.IsFertile), UpdateRate.SIM_1000ms);
	}

	// Token: 0x06002F94 RID: 12180 RVA: 0x000FB4C3 File Offset: 0x000F96C3
	public static bool IsFertile(FertilityMonitor.Instance smi)
	{
		return !smi.HasTag(GameTags.Creatures.Confined) && !smi.HasTag(GameTags.Creatures.Expecting);
	}

	// Token: 0x06002F95 RID: 12181 RVA: 0x000FB4E4 File Offset: 0x000F96E4
	public static Tag EggBreedingRoll(List<FertilityMonitor.BreedingChance> breedingChances, bool excludeOriginalCreature = false)
	{
		float num = UnityEngine.Random.value;
		if (excludeOriginalCreature)
		{
			num *= 1f - breedingChances[0].weight;
		}
		foreach (FertilityMonitor.BreedingChance breedingChance in breedingChances)
		{
			if (excludeOriginalCreature)
			{
				excludeOriginalCreature = false;
			}
			else
			{
				num -= breedingChance.weight;
				if (num <= 0f)
				{
					return breedingChance.egg;
				}
			}
		}
		return Tag.Invalid;
	}

	// Token: 0x04001CA2 RID: 7330
	private GameStateMachine<FertilityMonitor, FertilityMonitor.Instance, IStateMachineTarget, FertilityMonitor.Def>.State fertile;

	// Token: 0x04001CA3 RID: 7331
	private GameStateMachine<FertilityMonitor, FertilityMonitor.Instance, IStateMachineTarget, FertilityMonitor.Def>.State infertile;

	// Token: 0x020013CB RID: 5067
	[Serializable]
	public class BreedingChance
	{
		// Token: 0x040061A3 RID: 24995
		public Tag egg;

		// Token: 0x040061A4 RID: 24996
		public float weight;
	}

	// Token: 0x020013CC RID: 5068
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06007F0E RID: 32526 RVA: 0x002DAE39 File Offset: 0x002D9039
		public override void Configure(GameObject prefab)
		{
			prefab.AddOrGet<Modifiers>().initialAmounts.Add(Db.Get().Amounts.Fertility.Id);
		}

		// Token: 0x040061A5 RID: 24997
		public Tag eggPrefab;

		// Token: 0x040061A6 RID: 24998
		public List<FertilityMonitor.BreedingChance> initialBreedingWeights;

		// Token: 0x040061A7 RID: 24999
		public float baseFertileCycles;
	}

	// Token: 0x020013CD RID: 5069
	public new class Instance : GameStateMachine<FertilityMonitor, FertilityMonitor.Instance, IStateMachineTarget, FertilityMonitor.Def>.GameInstance
	{
		// Token: 0x06007F10 RID: 32528 RVA: 0x002DAE68 File Offset: 0x002D9068
		public Instance(IStateMachineTarget master, FertilityMonitor.Def def)
			: base(master, def)
		{
			this.fertility = Db.Get().Amounts.Fertility.Lookup(base.gameObject);
			if (GenericGameSettings.instance.acceleratedLifecycle)
			{
				this.fertility.deltaAttribute.Add(new AttributeModifier(this.fertility.deltaAttribute.Id, 33.333332f, "Accelerated Lifecycle", false, false, true));
			}
			float num = 100f / (def.baseFertileCycles * 600f);
			this.fertileEffect = new Effect("Fertile", CREATURES.MODIFIERS.BASE_FERTILITY.NAME, CREATURES.MODIFIERS.BASE_FERTILITY.TOOLTIP, 0f, false, false, false, null, -1f, 0f, null, "");
			this.fertileEffect.Add(new AttributeModifier(Db.Get().Amounts.Fertility.deltaAttribute.Id, num, CREATURES.MODIFIERS.BASE_FERTILITY.NAME, false, false, true));
			this.InitializeBreedingChances();
		}

		// Token: 0x06007F11 RID: 32529 RVA: 0x002DAF68 File Offset: 0x002D9168
		[OnDeserialized]
		private void OnDeserialized()
		{
			int num = ((base.def.initialBreedingWeights != null) ? base.def.initialBreedingWeights.Count : 0);
			if (this.breedingChances.Count != num)
			{
				this.InitializeBreedingChances();
			}
		}

		// Token: 0x06007F12 RID: 32530 RVA: 0x002DAFAC File Offset: 0x002D91AC
		private void InitializeBreedingChances()
		{
			this.breedingChances = new List<FertilityMonitor.BreedingChance>();
			if (base.def.initialBreedingWeights != null)
			{
				foreach (FertilityMonitor.BreedingChance breedingChance in base.def.initialBreedingWeights)
				{
					this.breedingChances.Add(new FertilityMonitor.BreedingChance
					{
						egg = breedingChance.egg,
						weight = breedingChance.weight
					});
					foreach (FertilityModifier fertilityModifier in Db.Get().FertilityModifiers.GetForTag(breedingChance.egg))
					{
						fertilityModifier.ApplyFunction(this, breedingChance.egg);
					}
				}
				this.NormalizeBreedingChances();
			}
		}

		// Token: 0x06007F13 RID: 32531 RVA: 0x002DB0A4 File Offset: 0x002D92A4
		public void ShowEgg()
		{
			if (this.egg != null)
			{
				bool flag;
				Vector3 vector = base.GetComponent<KBatchedAnimController>().GetSymbolTransform(FertilityMonitor.Instance.targetEggSymbol, out flag).MultiplyPoint3x4(Vector3.zero);
				if (flag)
				{
					vector.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
					int num = Grid.PosToCell(vector);
					if (Grid.IsValidCell(num) && !Grid.Solid[num])
					{
						this.egg.transform.SetPosition(vector);
					}
				}
				this.egg.SetActive(true);
				Db.Get().Amounts.Wildness.Copy(this.egg, base.gameObject);
				this.egg = null;
			}
		}

		// Token: 0x06007F14 RID: 32532 RVA: 0x002DB154 File Offset: 0x002D9354
		public void LayEgg()
		{
			this.fertility.value = 0f;
			Vector3 position = base.smi.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
			Tag tag = FertilityMonitor.EggBreedingRoll(this.breedingChances, false);
			if (GenericGameSettings.instance.acceleratedLifecycle)
			{
				float num = 0f;
				foreach (FertilityMonitor.BreedingChance breedingChance in this.breedingChances)
				{
					if (breedingChance.weight > num)
					{
						num = breedingChance.weight;
						tag = breedingChance.egg;
					}
				}
			}
			global::Debug.Assert(tag != Tag.Invalid, "Didn't pick an egg to lay. Weights weren't normalized?");
			GameObject prefab = Assets.GetPrefab(tag);
			GameObject gameObject = Util.KInstantiate(prefab, position);
			this.egg = gameObject;
			SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
			string text = "egg01";
			CreatureBrain component2 = Assets.GetPrefab(prefab.GetDef<IncubationMonitor.Def>().spawnedCreature).GetComponent<CreatureBrain>();
			if (!string.IsNullOrEmpty(component2.symbolPrefix))
			{
				text = component2.symbolPrefix + "egg01";
			}
			KAnim.Build.Symbol symbol = this.egg.GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbol(text);
			if (symbol != null)
			{
				component.AddSymbolOverride(FertilityMonitor.Instance.targetEggSymbol, symbol, 0);
			}
			base.Trigger(1193600993, this.egg);
		}

		// Token: 0x06007F15 RID: 32533 RVA: 0x002DB2CC File Offset: 0x002D94CC
		public bool IsReadyToLayEgg()
		{
			return base.smi.fertility.value >= base.smi.fertility.GetMax();
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x002DB2F4 File Offset: 0x002D94F4
		public void AddBreedingChance(Tag type, float addedPercentChance)
		{
			foreach (FertilityMonitor.BreedingChance breedingChance in this.breedingChances)
			{
				if (breedingChance.egg == type)
				{
					float num = Mathf.Min(1f - breedingChance.weight, Mathf.Max(0f - breedingChance.weight, addedPercentChance));
					breedingChance.weight += num;
				}
			}
			this.NormalizeBreedingChances();
			base.master.Trigger(1059811075, this.breedingChances);
		}

		// Token: 0x06007F17 RID: 32535 RVA: 0x002DB39C File Offset: 0x002D959C
		public float GetBreedingChance(Tag type)
		{
			foreach (FertilityMonitor.BreedingChance breedingChance in this.breedingChances)
			{
				if (breedingChance.egg == type)
				{
					return breedingChance.weight;
				}
			}
			return -1f;
		}

		// Token: 0x06007F18 RID: 32536 RVA: 0x002DB408 File Offset: 0x002D9608
		public void NormalizeBreedingChances()
		{
			float num = 0f;
			foreach (FertilityMonitor.BreedingChance breedingChance in this.breedingChances)
			{
				num += breedingChance.weight;
			}
			foreach (FertilityMonitor.BreedingChance breedingChance2 in this.breedingChances)
			{
				breedingChance2.weight /= num;
			}
		}

		// Token: 0x06007F19 RID: 32537 RVA: 0x002DB4AC File Offset: 0x002D96AC
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			if (this.egg != null)
			{
				UnityEngine.Object.Destroy(this.egg);
				this.egg = null;
			}
		}

		// Token: 0x040061A8 RID: 25000
		public AmountInstance fertility;

		// Token: 0x040061A9 RID: 25001
		private GameObject egg;

		// Token: 0x040061AA RID: 25002
		[Serialize]
		public List<FertilityMonitor.BreedingChance> breedingChances;

		// Token: 0x040061AB RID: 25003
		public Effect fertileEffect;

		// Token: 0x040061AC RID: 25004
		private static HashedString targetEggSymbol = "snapto_egg";
	}
}
