using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020006F3 RID: 1779
public class WildnessMonitor : GameStateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>
{
	// Token: 0x0600306A RID: 12394 RVA: 0x000FF868 File Offset: 0x000FDA68
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.tame;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.wild.Enter(new StateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State.Callback(WildnessMonitor.RefreshAmounts)).Enter(new StateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State.Callback(WildnessMonitor.HideDomesticationSymbol)).Transition(this.tame, (WildnessMonitor.Instance smi) => !WildnessMonitor.IsWild(smi), UpdateRate.SIM_1000ms)
			.ToggleEffect((WildnessMonitor.Instance smi) => smi.def.wildEffect)
			.ToggleTag(GameTags.Creatures.Wild);
		this.tame.Enter(new StateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State.Callback(WildnessMonitor.RefreshAmounts)).Enter(new StateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State.Callback(WildnessMonitor.ShowDomesticationSymbol)).Transition(this.wild, new StateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.Transition.ConditionCallback(WildnessMonitor.IsWild), UpdateRate.SIM_1000ms)
			.ToggleEffect((WildnessMonitor.Instance smi) => smi.def.tameEffect)
			.Enter(delegate(WildnessMonitor.Instance smi)
			{
				SaveGame.Instance.GetComponent<ColonyAchievementTracker>().LogCritterTamed(smi.PrefabID());
			});
	}

	// Token: 0x0600306B RID: 12395 RVA: 0x000FF990 File Offset: 0x000FDB90
	private static void HideDomesticationSymbol(WildnessMonitor.Instance smi)
	{
		foreach (KAnimHashedString kanimHashedString in WildnessMonitor.DOMESTICATION_SYMBOLS)
		{
			smi.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(kanimHashedString, false);
		}
	}

	// Token: 0x0600306C RID: 12396 RVA: 0x000FF9C8 File Offset: 0x000FDBC8
	private static void ShowDomesticationSymbol(WildnessMonitor.Instance smi)
	{
		foreach (KAnimHashedString kanimHashedString in WildnessMonitor.DOMESTICATION_SYMBOLS)
		{
			smi.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(kanimHashedString, true);
		}
	}

	// Token: 0x0600306D RID: 12397 RVA: 0x000FF9FE File Offset: 0x000FDBFE
	private static bool IsWild(WildnessMonitor.Instance smi)
	{
		return smi.wildness.value > 0f;
	}

	// Token: 0x0600306E RID: 12398 RVA: 0x000FFA14 File Offset: 0x000FDC14
	private static void RefreshAmounts(WildnessMonitor.Instance smi)
	{
		bool flag = WildnessMonitor.IsWild(smi);
		smi.wildness.hide = !flag;
		AttributeInstance attributeInstance = Db.Get().CritterAttributes.Happiness.Lookup(smi.gameObject);
		if (attributeInstance != null)
		{
			attributeInstance.hide = flag;
		}
		AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(smi.gameObject);
		if (amountInstance != null)
		{
			amountInstance.hide = flag;
		}
		AmountInstance amountInstance2 = Db.Get().Amounts.Temperature.Lookup(smi.gameObject);
		if (amountInstance2 != null)
		{
			amountInstance2.hide = flag;
		}
		AmountInstance amountInstance3 = Db.Get().Amounts.Fertility.Lookup(smi.gameObject);
		if (amountInstance3 != null)
		{
			amountInstance3.hide = flag;
		}
	}

	// Token: 0x04001D30 RID: 7472
	public GameStateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State wild;

	// Token: 0x04001D31 RID: 7473
	public GameStateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State tame;

	// Token: 0x04001D32 RID: 7474
	private static readonly KAnimHashedString[] DOMESTICATION_SYMBOLS = new KAnimHashedString[] { "tag", "snapto_tag" };

	// Token: 0x02001411 RID: 5137
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06007FEF RID: 32751 RVA: 0x002DE35F File Offset: 0x002DC55F
		public override void Configure(GameObject prefab)
		{
			prefab.GetComponent<Modifiers>().initialAmounts.Add(Db.Get().Amounts.Wildness.Id);
		}

		// Token: 0x04006263 RID: 25187
		public Effect wildEffect;

		// Token: 0x04006264 RID: 25188
		public Effect tameEffect;
	}

	// Token: 0x02001412 RID: 5138
	public new class Instance : GameStateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.GameInstance
	{
		// Token: 0x06007FF1 RID: 32753 RVA: 0x002DE38D File Offset: 0x002DC58D
		public Instance(IStateMachineTarget master, WildnessMonitor.Def def)
			: base(master, def)
		{
			this.wildness = Db.Get().Amounts.Wildness.Lookup(base.gameObject);
			this.wildness.value = this.wildness.GetMax();
		}

		// Token: 0x04006265 RID: 25189
		public AmountInstance wildness;
	}
}
