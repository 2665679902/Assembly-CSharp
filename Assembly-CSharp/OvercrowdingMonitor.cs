using System;
using Klei.AI;
using STRINGS;

// Token: 0x020006E2 RID: 1762
public class OvercrowdingMonitor : GameStateMachine<OvercrowdingMonitor, OvercrowdingMonitor.Instance, IStateMachineTarget, OvercrowdingMonitor.Def>
{
	// Token: 0x06002FEA RID: 12266 RVA: 0x000FD158 File Offset: 0x000FB358
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Update(new Action<OvercrowdingMonitor.Instance, float>(OvercrowdingMonitor.UpdateState), UpdateRate.SIM_1000ms, true);
		OvercrowdingMonitor.futureOvercrowdedEffect = new Effect("FutureOvercrowded", CREATURES.MODIFIERS.FUTURE_OVERCROWDED.NAME, CREATURES.MODIFIERS.FUTURE_OVERCROWDED.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		OvercrowdingMonitor.futureOvercrowdedEffect.Add(new AttributeModifier(Db.Get().Amounts.Fertility.deltaAttribute.Id, -1f, CREATURES.MODIFIERS.FUTURE_OVERCROWDED.NAME, true, false, true));
		OvercrowdingMonitor.overcrowdedEffect = new Effect("Overcrowded", CREATURES.MODIFIERS.OVERCROWDED.NAME, CREATURES.MODIFIERS.OVERCROWDED.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		OvercrowdingMonitor.overcrowdedEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -5f, CREATURES.MODIFIERS.OVERCROWDED.NAME, false, false, true));
		OvercrowdingMonitor.fishOvercrowdedEffect = new Effect("Overcrowded", CREATURES.MODIFIERS.OVERCROWDED.NAME, CREATURES.MODIFIERS.OVERCROWDED.FISHTOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		OvercrowdingMonitor.fishOvercrowdedEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -5f, CREATURES.MODIFIERS.OVERCROWDED.NAME, false, false, true));
		OvercrowdingMonitor.stuckEffect = new Effect("Confined", CREATURES.MODIFIERS.CONFINED.NAME, CREATURES.MODIFIERS.CONFINED.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		OvercrowdingMonitor.stuckEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -10f, CREATURES.MODIFIERS.CONFINED.NAME, false, false, true));
	}

	// Token: 0x06002FEB RID: 12267 RVA: 0x000FD350 File Offset: 0x000FB550
	private static bool IsConfined(OvercrowdingMonitor.Instance smi)
	{
		return !smi.HasTag(GameTags.Creatures.Burrowed) && !smi.HasTag(GameTags.Creatures.Digger) && (smi.cavity == null || smi.cavity.numCells < smi.def.spaceRequiredPerCreature);
	}

	// Token: 0x06002FEC RID: 12268 RVA: 0x000FD3A0 File Offset: 0x000FB5A0
	private static bool IsFutureOvercrowded(OvercrowdingMonitor.Instance smi)
	{
		if (smi.def.spaceRequiredPerCreature == 0)
		{
			return false;
		}
		if (smi.cavity != null)
		{
			int num = smi.cavity.creatures.Count + smi.cavity.eggs.Count;
			return num != 0 && smi.cavity.eggs.Count != 0 && smi.cavity.numCells / num < smi.def.spaceRequiredPerCreature;
		}
		return false;
	}

	// Token: 0x06002FED RID: 12269 RVA: 0x000FD41C File Offset: 0x000FB61C
	private static bool IsOvercrowded(OvercrowdingMonitor.Instance smi)
	{
		if (smi.def.spaceRequiredPerCreature == 0)
		{
			return false;
		}
		FishOvercrowdingMonitor.Instance smi2 = smi.GetSMI<FishOvercrowdingMonitor.Instance>();
		if (smi2 == null)
		{
			return smi.cavity != null && smi.cavity.creatures.Count > 1 && smi.cavity.numCells / smi.cavity.creatures.Count < smi.def.spaceRequiredPerCreature;
		}
		int fishCount = smi2.fishCount;
		if (fishCount > 0)
		{
			return smi2.cellCount / fishCount < smi.def.spaceRequiredPerCreature;
		}
		return !Grid.IsLiquid(Grid.PosToCell(smi));
	}

	// Token: 0x06002FEE RID: 12270 RVA: 0x000FD4B8 File Offset: 0x000FB6B8
	private static void UpdateState(OvercrowdingMonitor.Instance smi, float dt)
	{
		OvercrowdingMonitor.UpdateCavity(smi, dt);
		bool flag = OvercrowdingMonitor.IsConfined(smi);
		bool flag2 = OvercrowdingMonitor.IsOvercrowded(smi);
		bool flag3 = !smi.isBaby && OvercrowdingMonitor.IsFutureOvercrowded(smi);
		KPrefabID component = smi.gameObject.GetComponent<KPrefabID>();
		Effect effect = (smi.isFish ? OvercrowdingMonitor.fishOvercrowdedEffect : OvercrowdingMonitor.overcrowdedEffect);
		component.SetTag(GameTags.Creatures.Confined, flag);
		component.SetTag(GameTags.Creatures.Overcrowded, flag2);
		component.SetTag(GameTags.Creatures.Expecting, flag3);
		OvercrowdingMonitor.SetEffect(smi, OvercrowdingMonitor.stuckEffect, flag);
		OvercrowdingMonitor.SetEffect(smi, effect, !flag && flag2);
		OvercrowdingMonitor.SetEffect(smi, OvercrowdingMonitor.futureOvercrowdedEffect, !flag && flag3);
	}

	// Token: 0x06002FEF RID: 12271 RVA: 0x000FD55C File Offset: 0x000FB75C
	private static void SetEffect(OvercrowdingMonitor.Instance smi, Effect effect, bool set)
	{
		Effects component = smi.GetComponent<Effects>();
		if (set)
		{
			component.Add(effect, false);
			return;
		}
		component.Remove(effect);
	}

	// Token: 0x06002FF0 RID: 12272 RVA: 0x000FD584 File Offset: 0x000FB784
	private static void UpdateCavity(OvercrowdingMonitor.Instance smi, float dt)
	{
		CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(smi));
		if (cavityForCell != smi.cavity)
		{
			KPrefabID component = smi.GetComponent<KPrefabID>();
			if (smi.cavity != null)
			{
				if (smi.HasTag(GameTags.Egg))
				{
					smi.cavity.RemoveFromCavity(component, smi.cavity.eggs);
				}
				else
				{
					smi.cavity.RemoveFromCavity(component, smi.cavity.creatures);
				}
				Game.Instance.roomProber.UpdateRoom(cavityForCell);
			}
			smi.cavity = cavityForCell;
			if (smi.cavity != null)
			{
				if (smi.HasTag(GameTags.Egg))
				{
					smi.cavity.eggs.Add(component);
				}
				else
				{
					smi.cavity.creatures.Add(component);
				}
				Game.Instance.roomProber.UpdateRoom(smi.cavity);
			}
		}
	}

	// Token: 0x04001CE2 RID: 7394
	public const float OVERCROWDED_FERTILITY_DEBUFF = -1f;

	// Token: 0x04001CE3 RID: 7395
	public static Effect futureOvercrowdedEffect;

	// Token: 0x04001CE4 RID: 7396
	public static Effect overcrowdedEffect;

	// Token: 0x04001CE5 RID: 7397
	public static Effect fishOvercrowdedEffect;

	// Token: 0x04001CE6 RID: 7398
	public static Effect stuckEffect;

	// Token: 0x020013EF RID: 5103
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400620D RID: 25101
		public int spaceRequiredPerCreature;
	}

	// Token: 0x020013F0 RID: 5104
	public new class Instance : GameStateMachine<OvercrowdingMonitor, OvercrowdingMonitor.Instance, IStateMachineTarget, OvercrowdingMonitor.Def>.GameInstance
	{
		// Token: 0x06007FA4 RID: 32676 RVA: 0x002DD300 File Offset: 0x002DB500
		public Instance(IStateMachineTarget master, OvercrowdingMonitor.Def def)
			: base(master, def)
		{
			BabyMonitor.Def def2 = master.gameObject.GetDef<BabyMonitor.Def>();
			this.isBaby = def2 != null;
			FishOvercrowdingMonitor.Def def3 = master.gameObject.GetDef<FishOvercrowdingMonitor.Def>();
			this.isFish = def3 != null;
			OvercrowdingMonitor.UpdateState(this, 0f);
		}

		// Token: 0x06007FA5 RID: 32677 RVA: 0x002DD34C File Offset: 0x002DB54C
		protected override void OnCleanUp()
		{
			if (this.cavity == null)
			{
				return;
			}
			KPrefabID component = base.master.GetComponent<KPrefabID>();
			if (base.HasTag(GameTags.Egg))
			{
				this.cavity.RemoveFromCavity(component, this.cavity.eggs);
				return;
			}
			this.cavity.RemoveFromCavity(component, this.cavity.creatures);
		}

		// Token: 0x06007FA6 RID: 32678 RVA: 0x002DD3AA File Offset: 0x002DB5AA
		public void RoomRefreshUpdateCavity()
		{
			OvercrowdingMonitor.UpdateState(this, 0f);
		}

		// Token: 0x0400620E RID: 25102
		public CavityInfo cavity;

		// Token: 0x0400620F RID: 25103
		public bool isBaby;

		// Token: 0x04006210 RID: 25104
		public bool isFish;
	}
}
