using System;
using Klei.AI;
using Klei.CustomSettings;

// Token: 0x0200084A RID: 2122
public class StressMonitor : GameStateMachine<StressMonitor, StressMonitor.Instance>
{
	// Token: 0x06003D22 RID: 15650 RVA: 0x00155ADC File Offset: 0x00153CDC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		default_state = this.satisfied;
		this.root.Update("StressMonitor", delegate(StressMonitor.Instance smi, float dt)
		{
			smi.ReportStress(dt);
		}, UpdateRate.SIM_200ms, false);
		this.satisfied.TriggerOnEnter(GameHashes.NotStressed, null).Transition(this.stressed.tier1, (StressMonitor.Instance smi) => smi.stress.value >= 60f, UpdateRate.SIM_200ms).ToggleExpression(Db.Get().Expressions.Neutral, null);
		this.stressed.ToggleStatusItem(Db.Get().DuplicantStatusItems.Stressed, null).Transition(this.satisfied, (StressMonitor.Instance smi) => smi.stress.value < 60f, UpdateRate.SIM_200ms).ToggleReactable((StressMonitor.Instance smi) => smi.CreateConcernReactable())
			.TriggerOnEnter(GameHashes.Stressed, null);
		this.stressed.tier1.Transition(this.stressed.tier2, (StressMonitor.Instance smi) => smi.HasHadEnough(), UpdateRate.SIM_200ms);
		this.stressed.tier2.TriggerOnEnter(GameHashes.StressedHadEnough, null).Transition(this.stressed.tier1, (StressMonitor.Instance smi) => !smi.HasHadEnough(), UpdateRate.SIM_200ms);
	}

	// Token: 0x04002802 RID: 10242
	public GameStateMachine<StressMonitor, StressMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x04002803 RID: 10243
	public StressMonitor.Stressed stressed;

	// Token: 0x04002804 RID: 10244
	private const float StressThreshold_One = 60f;

	// Token: 0x04002805 RID: 10245
	private const float StressThreshold_Two = 100f;

	// Token: 0x020015EF RID: 5615
	public class Stressed : GameStateMachine<StressMonitor, StressMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400685B RID: 26715
		public GameStateMachine<StressMonitor, StressMonitor.Instance, IStateMachineTarget, object>.State tier1;

		// Token: 0x0400685C RID: 26716
		public GameStateMachine<StressMonitor, StressMonitor.Instance, IStateMachineTarget, object>.State tier2;
	}

	// Token: 0x020015F0 RID: 5616
	public new class Instance : GameStateMachine<StressMonitor, StressMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060085F4 RID: 34292 RVA: 0x002ED7C0 File Offset: 0x002EB9C0
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.stress = Db.Get().Amounts.Stress.Lookup(base.gameObject);
			SettingConfig settingConfig = CustomGameSettings.Instance.QualitySettings[CustomGameSettingConfigs.StressBreaks.id];
			SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.StressBreaks);
			this.allowStressBreak = settingConfig.IsDefaultLevel(currentQualitySetting.id);
		}

		// Token: 0x060085F5 RID: 34293 RVA: 0x002ED837 File Offset: 0x002EBA37
		public bool IsStressed()
		{
			return base.IsInsideState(base.sm.stressed);
		}

		// Token: 0x060085F6 RID: 34294 RVA: 0x002ED84A File Offset: 0x002EBA4A
		public bool HasHadEnough()
		{
			return this.allowStressBreak && this.stress.value >= 100f;
		}

		// Token: 0x060085F7 RID: 34295 RVA: 0x002ED86C File Offset: 0x002EBA6C
		public void ReportStress(float dt)
		{
			for (int num = 0; num != this.stress.deltaAttribute.Modifiers.Count; num++)
			{
				AttributeModifier attributeModifier = this.stress.deltaAttribute.Modifiers[num];
				DebugUtil.DevAssert(!attributeModifier.IsMultiplier, "Reporting stress for multipliers not supported yet.", null);
				ReportManager.Instance.ReportValue(ReportManager.ReportType.StressDelta, attributeModifier.Value * dt, attributeModifier.GetDescription(), base.gameObject.GetProperName());
			}
		}

		// Token: 0x060085F8 RID: 34296 RVA: 0x002ED8E8 File Offset: 0x002EBAE8
		public Reactable CreateConcernReactable()
		{
			return new EmoteReactable(base.master.gameObject, "StressConcern", Db.Get().ChoreTypes.Emote, 15, 8, 0f, 30f, float.PositiveInfinity, 0f).SetEmote(Db.Get().Emotes.Minion.Concern);
		}

		// Token: 0x0400685D RID: 26717
		public AmountInstance stress;

		// Token: 0x0400685E RID: 26718
		private bool allowStressBreak = true;
	}
}
