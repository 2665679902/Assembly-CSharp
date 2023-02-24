using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x0200082E RID: 2094
public class GasLiquidExposureMonitor : GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>
{
	// Token: 0x06003C8E RID: 15502 RVA: 0x00151B50 File Offset: 0x0014FD50
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.normal;
		this.root.Update(new Action<GasLiquidExposureMonitor.Instance, float>(this.UpdateExposure), UpdateRate.SIM_33ms, false);
		this.normal.ParamTransition<bool>(this.isIrritated, this.irritated, (GasLiquidExposureMonitor.Instance smi, bool p) => this.isIrritated.Get(smi));
		this.irritated.ParamTransition<bool>(this.isIrritated, this.normal, (GasLiquidExposureMonitor.Instance smi, bool p) => !this.isIrritated.Get(smi)).ToggleStatusItem(Db.Get().DuplicantStatusItems.GasLiquidIrritation, (GasLiquidExposureMonitor.Instance smi) => smi).DefaultState(this.irritated.irritated);
		this.irritated.irritated.Transition(this.irritated.rubbingEyes, new StateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.Transition.ConditionCallback(GasLiquidExposureMonitor.CanReact), UpdateRate.SIM_200ms);
		this.irritated.rubbingEyes.Exit(delegate(GasLiquidExposureMonitor.Instance smi)
		{
			smi.lastReactTime = GameClock.Instance.GetTime();
		}).ToggleReactable((GasLiquidExposureMonitor.Instance smi) => smi.GetReactable()).OnSignal(this.reactFinished, this.irritated.irritated);
	}

	// Token: 0x06003C8F RID: 15503 RVA: 0x00151C9D File Offset: 0x0014FE9D
	private static bool CanReact(GasLiquidExposureMonitor.Instance smi)
	{
		return GameClock.Instance.GetTime() > smi.lastReactTime + 60f;
	}

	// Token: 0x06003C90 RID: 15504 RVA: 0x00151CB8 File Offset: 0x0014FEB8
	private static void InitializeCustomRates()
	{
		if (GasLiquidExposureMonitor.customExposureRates != null)
		{
			return;
		}
		GasLiquidExposureMonitor.minorIrritationEffect = Db.Get().effects.Get("MinorIrritation");
		GasLiquidExposureMonitor.majorIrritationEffect = Db.Get().effects.Get("MajorIrritation");
		GasLiquidExposureMonitor.customExposureRates = new Dictionary<SimHashes, float>();
		float num = -1f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Water] = num;
		float num2 = -0.25f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.CarbonDioxide] = num2;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Oxygen] = num2;
		float num3 = 0f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.ContaminatedOxygen] = num3;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.DirtyWater] = num3;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.ViscoGel] = num3;
		float num4 = 0.5f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Hydrogen] = num4;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.SaltWater] = num4;
		float num5 = 1f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.ChlorineGas] = num5;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.EthanolGas] = num5;
		float num6 = 3f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Chlorine] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.SourGas] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Brine] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Ethanol] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.SuperCoolant] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.CrudeOil] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Naphtha] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Petroleum] = num6;
	}

	// Token: 0x06003C91 RID: 15505 RVA: 0x00151E5C File Offset: 0x0015005C
	public float GetCurrentExposure(GasLiquidExposureMonitor.Instance smi)
	{
		float num;
		if (GasLiquidExposureMonitor.customExposureRates.TryGetValue(smi.CurrentlyExposedToElement().id, out num))
		{
			return num;
		}
		return 0f;
	}

	// Token: 0x06003C92 RID: 15506 RVA: 0x00151E8C File Offset: 0x0015008C
	private void UpdateExposure(GasLiquidExposureMonitor.Instance smi, float dt)
	{
		GasLiquidExposureMonitor.InitializeCustomRates();
		float num = 0f;
		smi.isInAirtightEnvironment = false;
		smi.isImmuneToIrritability = false;
		int num2 = Grid.CellAbove(Grid.PosToCell(smi.gameObject));
		if (Grid.IsValidCell(num2))
		{
			Element element = Grid.Element[num2];
			float num3;
			if (!GasLiquidExposureMonitor.customExposureRates.TryGetValue(element.id, out num3))
			{
				if (Grid.Temperature[num2] >= -13657.5f && Grid.Temperature[num2] <= 27315f)
				{
					num3 = 1f;
				}
				else
				{
					num3 = 2f;
				}
			}
			if (smi.effects.HasImmunityTo(GasLiquidExposureMonitor.minorIrritationEffect) || smi.effects.HasImmunityTo(GasLiquidExposureMonitor.majorIrritationEffect))
			{
				smi.isImmuneToIrritability = true;
				num = GasLiquidExposureMonitor.customExposureRates[SimHashes.Oxygen];
			}
			if ((smi.master.gameObject.HasTag(GameTags.HasSuitTank) && smi.gameObject.GetComponent<SuitEquipper>().IsWearingAirtightSuit()) || smi.master.gameObject.HasTag(GameTags.InTransitTube))
			{
				smi.isInAirtightEnvironment = true;
				num = GasLiquidExposureMonitor.customExposureRates[SimHashes.Oxygen];
			}
			if (!smi.isInAirtightEnvironment && !smi.isImmuneToIrritability)
			{
				if (element.IsGas)
				{
					num = num3 * Grid.Mass[num2] / 1f;
				}
				else if (element.IsLiquid)
				{
					num = num3 * Grid.Mass[num2] / 1000f;
				}
			}
		}
		smi.exposureRate = num;
		smi.exposure += smi.exposureRate * dt;
		smi.exposure = MathUtil.Clamp(0f, 30f, smi.exposure);
		this.ApplyEffects(smi);
	}

	// Token: 0x06003C93 RID: 15507 RVA: 0x0015203C File Offset: 0x0015023C
	private void ApplyEffects(GasLiquidExposureMonitor.Instance smi)
	{
		if (smi.IsMinorIrritation())
		{
			if (smi.effects.Add(GasLiquidExposureMonitor.minorIrritationEffect, true) != null)
			{
				this.isIrritated.Set(true, smi, false);
				return;
			}
		}
		else if (smi.IsMajorIrritation())
		{
			if (smi.effects.Add(GasLiquidExposureMonitor.majorIrritationEffect, true) != null)
			{
				this.isIrritated.Set(true, smi, false);
				return;
			}
		}
		else
		{
			smi.effects.Remove(GasLiquidExposureMonitor.minorIrritationEffect);
			smi.effects.Remove(GasLiquidExposureMonitor.majorIrritationEffect);
			this.isIrritated.Set(false, smi, false);
		}
	}

	// Token: 0x06003C94 RID: 15508 RVA: 0x001520CE File Offset: 0x001502CE
	public Effect GetAppliedEffect(GasLiquidExposureMonitor.Instance smi)
	{
		if (smi.IsMinorIrritation())
		{
			return GasLiquidExposureMonitor.minorIrritationEffect;
		}
		if (smi.IsMajorIrritation())
		{
			return GasLiquidExposureMonitor.majorIrritationEffect;
		}
		return null;
	}

	// Token: 0x04002779 RID: 10105
	public const float MIN_REACT_INTERVAL = 60f;

	// Token: 0x0400277A RID: 10106
	private static Dictionary<SimHashes, float> customExposureRates;

	// Token: 0x0400277B RID: 10107
	private static Effect minorIrritationEffect;

	// Token: 0x0400277C RID: 10108
	private static Effect majorIrritationEffect;

	// Token: 0x0400277D RID: 10109
	public StateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.BoolParameter isIrritated;

	// Token: 0x0400277E RID: 10110
	public StateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.Signal reactFinished;

	// Token: 0x0400277F RID: 10111
	public GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.State normal;

	// Token: 0x04002780 RID: 10112
	public GasLiquidExposureMonitor.IrritatedStates irritated;

	// Token: 0x020015AD RID: 5549
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020015AE RID: 5550
	public class TUNING
	{
		// Token: 0x04006780 RID: 26496
		public const float MINOR_IRRITATION_THRESHOLD = 8f;

		// Token: 0x04006781 RID: 26497
		public const float MAJOR_IRRITATION_THRESHOLD = 15f;

		// Token: 0x04006782 RID: 26498
		public const float MAX_EXPOSURE = 30f;

		// Token: 0x04006783 RID: 26499
		public const float GAS_UNITS = 1f;

		// Token: 0x04006784 RID: 26500
		public const float LIQUID_UNITS = 1000f;

		// Token: 0x04006785 RID: 26501
		public const float REDUCE_EXPOSURE_RATE_FAST = -1f;

		// Token: 0x04006786 RID: 26502
		public const float REDUCE_EXPOSURE_RATE_SLOW = -0.25f;

		// Token: 0x04006787 RID: 26503
		public const float NO_CHANGE = 0f;

		// Token: 0x04006788 RID: 26504
		public const float SLOW_EXPOSURE_RATE = 0.5f;

		// Token: 0x04006789 RID: 26505
		public const float NORMAL_EXPOSURE_RATE = 1f;

		// Token: 0x0400678A RID: 26506
		public const float QUICK_EXPOSURE_RATE = 3f;

		// Token: 0x0400678B RID: 26507
		public const float DEFAULT_MIN_TEMPERATURE = -13657.5f;

		// Token: 0x0400678C RID: 26508
		public const float DEFAULT_MAX_TEMPERATURE = 27315f;

		// Token: 0x0400678D RID: 26509
		public const float DEFAULT_LOW_RATE = 1f;

		// Token: 0x0400678E RID: 26510
		public const float DEFAULT_HIGH_RATE = 2f;
	}

	// Token: 0x020015AF RID: 5551
	public class IrritatedStates : GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.State
	{
		// Token: 0x0400678F RID: 26511
		public GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.State irritated;

		// Token: 0x04006790 RID: 26512
		public GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.State rubbingEyes;
	}

	// Token: 0x020015B0 RID: 5552
	public new class Instance : GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.GameInstance
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060084DB RID: 34011 RVA: 0x002EADA7 File Offset: 0x002E8FA7
		public float minorIrritationThreshold
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x060084DC RID: 34012 RVA: 0x002EADAE File Offset: 0x002E8FAE
		public Instance(IStateMachineTarget master, GasLiquidExposureMonitor.Def def)
			: base(master, def)
		{
			this.effects = master.GetComponent<Effects>();
		}

		// Token: 0x060084DD RID: 34013 RVA: 0x002EADC4 File Offset: 0x002E8FC4
		public Reactable GetReactable()
		{
			Emote iritatedEyes = Db.Get().Emotes.Minion.IritatedEyes;
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, "IrritatedEyes", Db.Get().ChoreTypes.Cough, 0f, 0f, float.PositiveInfinity, 0f);
			selfEmoteReactable.SetEmote(iritatedEyes);
			selfEmoteReactable.preventChoreInterruption = true;
			selfEmoteReactable.RegisterEmoteStepCallbacks("irritated_eyes", null, delegate(GameObject go)
			{
				base.sm.reactFinished.Trigger(this);
			});
			return selfEmoteReactable;
		}

		// Token: 0x060084DE RID: 34014 RVA: 0x002EAE50 File Offset: 0x002E9050
		public bool IsMinorIrritation()
		{
			return this.exposure >= 8f && this.exposure < 15f;
		}

		// Token: 0x060084DF RID: 34015 RVA: 0x002EAE6E File Offset: 0x002E906E
		public bool IsMajorIrritation()
		{
			return this.exposure >= 15f;
		}

		// Token: 0x060084E0 RID: 34016 RVA: 0x002EAE80 File Offset: 0x002E9080
		public Element CurrentlyExposedToElement()
		{
			if (this.isInAirtightEnvironment)
			{
				return ElementLoader.GetElement(SimHashes.Oxygen.CreateTag());
			}
			int num = Grid.CellAbove(Grid.PosToCell(base.smi.gameObject));
			return Grid.Element[num];
		}

		// Token: 0x060084E1 RID: 34017 RVA: 0x002EAEC2 File Offset: 0x002E90C2
		public void ResetExposure()
		{
			this.exposure = 0f;
		}

		// Token: 0x04006791 RID: 26513
		[Serialize]
		public float exposure;

		// Token: 0x04006792 RID: 26514
		[Serialize]
		public float lastReactTime;

		// Token: 0x04006793 RID: 26515
		[Serialize]
		public float exposureRate;

		// Token: 0x04006794 RID: 26516
		public Effects effects;

		// Token: 0x04006795 RID: 26517
		public bool isInAirtightEnvironment;

		// Token: 0x04006796 RID: 26518
		public bool isImmuneToIrritability;
	}
}
