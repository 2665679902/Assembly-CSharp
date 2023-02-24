using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006EB RID: 1771
public class ScaleGrowthMonitor : GameStateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>
{
	// Token: 0x06003023 RID: 12323 RVA: 0x000FE590 File Offset: 0x000FC790
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.growing;
		this.root.Enter(delegate(ScaleGrowthMonitor.Instance smi)
		{
			ScaleGrowthMonitor.UpdateScales(smi, 0f);
		}).Update(new Action<ScaleGrowthMonitor.Instance, float>(ScaleGrowthMonitor.UpdateScales), UpdateRate.SIM_1000ms, false);
		this.growing.DefaultState(this.growing.growing).Transition(this.fullyGrown, new StateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.Transition.ConditionCallback(ScaleGrowthMonitor.AreScalesFullyGrown), UpdateRate.SIM_1000ms);
		this.growing.growing.Transition(this.growing.stunted, GameStateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.Not(new StateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.Transition.ConditionCallback(ScaleGrowthMonitor.IsInCorrectAtmosphere)), UpdateRate.SIM_1000ms).Enter(new StateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.State.Callback(ScaleGrowthMonitor.ApplyModifier)).Exit(new StateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.State.Callback(ScaleGrowthMonitor.RemoveModifier));
		this.growing.stunted.Transition(this.growing.growing, new StateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.Transition.ConditionCallback(ScaleGrowthMonitor.IsInCorrectAtmosphere), UpdateRate.SIM_1000ms).ToggleStatusItem(CREATURES.STATUSITEMS.STUNTED_SCALE_GROWTH.NAME, CREATURES.STATUSITEMS.STUNTED_SCALE_GROWTH.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.fullyGrown.ToggleBehaviour(GameTags.Creatures.ScalesGrown, (ScaleGrowthMonitor.Instance smi) => smi.HasTag(GameTags.Creatures.CanMolt), null).Transition(this.growing, GameStateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.Not(new StateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.Transition.ConditionCallback(ScaleGrowthMonitor.AreScalesFullyGrown)), UpdateRate.SIM_1000ms);
	}

	// Token: 0x06003024 RID: 12324 RVA: 0x000FE71C File Offset: 0x000FC91C
	private static bool IsInCorrectAtmosphere(ScaleGrowthMonitor.Instance smi)
	{
		int num = Grid.PosToCell(smi);
		return Grid.Element[num].id == smi.def.targetAtmosphere;
	}

	// Token: 0x06003025 RID: 12325 RVA: 0x000FE749 File Offset: 0x000FC949
	private static bool AreScalesFullyGrown(ScaleGrowthMonitor.Instance smi)
	{
		return smi.scaleGrowth.value >= smi.scaleGrowth.GetMax();
	}

	// Token: 0x06003026 RID: 12326 RVA: 0x000FE766 File Offset: 0x000FC966
	private static void ApplyModifier(ScaleGrowthMonitor.Instance smi)
	{
		smi.scaleGrowth.deltaAttribute.Add(smi.scaleGrowthModifier);
	}

	// Token: 0x06003027 RID: 12327 RVA: 0x000FE77E File Offset: 0x000FC97E
	private static void RemoveModifier(ScaleGrowthMonitor.Instance smi)
	{
		smi.scaleGrowth.deltaAttribute.Remove(smi.scaleGrowthModifier);
	}

	// Token: 0x06003028 RID: 12328 RVA: 0x000FE798 File Offset: 0x000FC998
	private static void UpdateScales(ScaleGrowthMonitor.Instance smi, float dt)
	{
		int num = (int)((float)smi.def.levelCount * smi.scaleGrowth.value / 100f);
		if (smi.currentScaleLevel != num)
		{
			KBatchedAnimController component = smi.GetComponent<KBatchedAnimController>();
			for (int i = 0; i < ScaleGrowthMonitor.SCALE_SYMBOL_NAMES.Length; i++)
			{
				bool flag = i <= num - 1;
				component.SetSymbolVisiblity(ScaleGrowthMonitor.SCALE_SYMBOL_NAMES[i], flag);
			}
			smi.currentScaleLevel = num;
		}
	}

	// Token: 0x04001D08 RID: 7432
	public ScaleGrowthMonitor.GrowingState growing;

	// Token: 0x04001D09 RID: 7433
	public GameStateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.State fullyGrown;

	// Token: 0x04001D0A RID: 7434
	private AttributeModifier scaleGrowthModifier;

	// Token: 0x04001D0B RID: 7435
	private static HashedString[] SCALE_SYMBOL_NAMES = new HashedString[] { "scale_0", "scale_1", "scale_2", "scale_3", "scale_4" };

	// Token: 0x02001402 RID: 5122
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06007FCB RID: 32715 RVA: 0x002DDB4E File Offset: 0x002DBD4E
		public override void Configure(GameObject prefab)
		{
			prefab.GetComponent<Modifiers>().initialAmounts.Add(Db.Get().Amounts.ScaleGrowth.Id);
		}

		// Token: 0x06007FCC RID: 32716 RVA: 0x002DDB74 File Offset: 0x002DBD74
		public List<Descriptor> GetDescriptors(GameObject obj)
		{
			List<Descriptor> list = new List<Descriptor>();
			if (this.targetAtmosphere == (SimHashes)0)
			{
				list.Add(new Descriptor(UI.BUILDINGEFFECTS.SCALE_GROWTH.Replace("{Item}", this.itemDroppedOnShear.ProperName()).Replace("{Amount}", GameUtil.GetFormattedMass(this.dropMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")).Replace("{Time}", GameUtil.GetFormattedCycles(1f / this.defaultGrowthRate, "F1", false)), UI.BUILDINGEFFECTS.TOOLTIPS.SCALE_GROWTH.Replace("{Item}", this.itemDroppedOnShear.ProperName()).Replace("{Amount}", GameUtil.GetFormattedMass(this.dropMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")).Replace("{Time}", GameUtil.GetFormattedCycles(1f / this.defaultGrowthRate, "F1", false)), Descriptor.DescriptorType.Effect, false));
			}
			else
			{
				list.Add(new Descriptor(UI.BUILDINGEFFECTS.SCALE_GROWTH_ATMO.Replace("{Item}", this.itemDroppedOnShear.ProperName()).Replace("{Amount}", GameUtil.GetFormattedMass(this.dropMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")).Replace("{Time}", GameUtil.GetFormattedCycles(1f / this.defaultGrowthRate, "F1", false))
					.Replace("{Atmosphere}", this.targetAtmosphere.CreateTag().ProperName()), UI.BUILDINGEFFECTS.TOOLTIPS.SCALE_GROWTH_ATMO.Replace("{Item}", this.itemDroppedOnShear.ProperName()).Replace("{Amount}", GameUtil.GetFormattedMass(this.dropMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")).Replace("{Time}", GameUtil.GetFormattedCycles(1f / this.defaultGrowthRate, "F1", false))
					.Replace("{Atmosphere}", this.targetAtmosphere.CreateTag().ProperName()), Descriptor.DescriptorType.Effect, false));
			}
			return list;
		}

		// Token: 0x04006236 RID: 25142
		public int levelCount;

		// Token: 0x04006237 RID: 25143
		public float defaultGrowthRate;

		// Token: 0x04006238 RID: 25144
		public SimHashes targetAtmosphere;

		// Token: 0x04006239 RID: 25145
		public Tag itemDroppedOnShear;

		// Token: 0x0400623A RID: 25146
		public float dropMass;
	}

	// Token: 0x02001403 RID: 5123
	public class GrowingState : GameStateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.State
	{
		// Token: 0x0400623B RID: 25147
		public GameStateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.State growing;

		// Token: 0x0400623C RID: 25148
		public GameStateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.State stunted;
	}

	// Token: 0x02001404 RID: 5124
	public new class Instance : GameStateMachine<ScaleGrowthMonitor, ScaleGrowthMonitor.Instance, IStateMachineTarget, ScaleGrowthMonitor.Def>.GameInstance, IShearable
	{
		// Token: 0x06007FCF RID: 32719 RVA: 0x002DDD58 File Offset: 0x002DBF58
		public Instance(IStateMachineTarget master, ScaleGrowthMonitor.Def def)
			: base(master, def)
		{
			this.scaleGrowth = Db.Get().Amounts.ScaleGrowth.Lookup(base.gameObject);
			this.scaleGrowth.value = this.scaleGrowth.GetMax();
			this.scaleGrowthModifier = new AttributeModifier(this.scaleGrowth.amount.deltaAttribute.Id, def.defaultGrowthRate * 100f, CREATURES.MODIFIERS.SCALE_GROWTH_RATE.NAME, false, false, true);
		}

		// Token: 0x06007FD0 RID: 32720 RVA: 0x002DDDE3 File Offset: 0x002DBFE3
		public bool IsFullyGrown()
		{
			return this.currentScaleLevel == base.def.levelCount;
		}

		// Token: 0x06007FD1 RID: 32721 RVA: 0x002DDDF8 File Offset: 0x002DBFF8
		public void Shear()
		{
			PrimaryElement component = base.smi.GetComponent<PrimaryElement>();
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(base.def.itemDroppedOnShear), null, null);
			gameObject.transform.SetPosition(Grid.CellToPosCCC(Grid.CellLeft(Grid.PosToCell(this)), Grid.SceneLayer.Ore));
			PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
			component2.Temperature = component.Temperature;
			component2.Mass = base.def.dropMass;
			component2.AddDisease(component.DiseaseIdx, component.DiseaseCount, "Shearing");
			gameObject.SetActive(true);
			Vector2 vector = new Vector2(UnityEngine.Random.Range(-1f, 1f) * 1f, UnityEngine.Random.value * 2f + 2f);
			if (GameComps.Fallers.Has(gameObject))
			{
				GameComps.Fallers.Remove(gameObject);
			}
			GameComps.Fallers.Add(gameObject, vector);
			this.scaleGrowth.value = 0f;
			ScaleGrowthMonitor.UpdateScales(this, 0f);
		}

		// Token: 0x0400623D RID: 25149
		public AmountInstance scaleGrowth;

		// Token: 0x0400623E RID: 25150
		public AttributeModifier scaleGrowthModifier;

		// Token: 0x0400623F RID: 25151
		public int currentScaleLevel = -1;
	}
}
