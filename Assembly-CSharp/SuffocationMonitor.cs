using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200078C RID: 1932
public class SuffocationMonitor : GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance>
{
	// Token: 0x06003609 RID: 13833 RVA: 0x0012BB8C File Offset: 0x00129D8C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.Update("CheckOverPressure", delegate(SuffocationMonitor.Instance smi, float dt)
		{
			smi.CheckOverPressure();
		}, UpdateRate.SIM_200ms, false).TagTransition(GameTags.Dead, this.dead, false);
		this.satisfied.DefaultState(this.satisfied.normal).ToggleAttributeModifier("Breathing", (SuffocationMonitor.Instance smi) => smi.breathing, null).EventTransition(GameHashes.ExitedBreathableArea, this.nooxygen, (SuffocationMonitor.Instance smi) => !smi.IsInBreathableArea());
		this.satisfied.normal.Transition(this.satisfied.low, (SuffocationMonitor.Instance smi) => smi.oxygenBreather.IsLowOxygen(), UpdateRate.SIM_200ms);
		this.satisfied.low.Transition(this.satisfied.normal, (SuffocationMonitor.Instance smi) => !smi.oxygenBreather.IsLowOxygen(), UpdateRate.SIM_200ms).Transition(this.nooxygen, (SuffocationMonitor.Instance smi) => !smi.IsInBreathableArea(), UpdateRate.SIM_200ms).ToggleEffect("LowOxygen");
		this.nooxygen.EventTransition(GameHashes.EnteredBreathableArea, this.satisfied, (SuffocationMonitor.Instance smi) => smi.IsInBreathableArea()).TagTransition(GameTags.RecoveringBreath, this.satisfied, false).ToggleExpression(Db.Get().Expressions.Suffocate, null)
			.ToggleAttributeModifier("Holding Breath", (SuffocationMonitor.Instance smi) => smi.holdingbreath, null)
			.ToggleTag(GameTags.NoOxygen)
			.DefaultState(this.nooxygen.holdingbreath);
		this.nooxygen.holdingbreath.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Suffocation, Db.Get().DuplicantStatusItems.HoldingBreath, null).Transition(this.nooxygen.suffocating, (SuffocationMonitor.Instance smi) => smi.IsSuffocating(), UpdateRate.SIM_200ms);
		this.nooxygen.suffocating.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Suffocation, Db.Get().DuplicantStatusItems.Suffocating, null).Transition(this.death, (SuffocationMonitor.Instance smi) => smi.HasSuffocated(), UpdateRate.SIM_200ms);
		this.death.Enter("SuffocationDeath", delegate(SuffocationMonitor.Instance smi)
		{
			smi.Kill();
		});
		this.dead.DoNothing();
	}

	// Token: 0x0400240F RID: 9231
	public SuffocationMonitor.SatisfiedState satisfied;

	// Token: 0x04002410 RID: 9232
	public SuffocationMonitor.NoOxygenState nooxygen;

	// Token: 0x04002411 RID: 9233
	public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, object>.State death;

	// Token: 0x04002412 RID: 9234
	public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, object>.State dead;

	// Token: 0x020014BB RID: 5307
	public class NoOxygenState : GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400648F RID: 25743
		public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, object>.State holdingbreath;

		// Token: 0x04006490 RID: 25744
		public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, object>.State suffocating;
	}

	// Token: 0x020014BC RID: 5308
	public class SatisfiedState : GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006491 RID: 25745
		public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, object>.State normal;

		// Token: 0x04006492 RID: 25746
		public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, object>.State low;
	}

	// Token: 0x020014BD RID: 5309
	public new class Instance : GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060081C0 RID: 33216 RVA: 0x002E3051 File Offset: 0x002E1251
		// (set) Token: 0x060081C1 RID: 33217 RVA: 0x002E3059 File Offset: 0x002E1259
		public OxygenBreather oxygenBreather { get; private set; }

		// Token: 0x060081C2 RID: 33218 RVA: 0x002E3064 File Offset: 0x002E1264
		public Instance(OxygenBreather oxygen_breather)
			: base(oxygen_breather)
		{
			this.breath = Db.Get().Amounts.Breath.Lookup(base.master.gameObject);
			Klei.AI.Attribute deltaAttribute = Db.Get().Amounts.Breath.deltaAttribute;
			float num = 0.90909094f;
			this.breathing = new AttributeModifier(deltaAttribute.Id, num, DUPLICANTS.MODIFIERS.BREATHING.NAME, false, false, true);
			this.holdingbreath = new AttributeModifier(deltaAttribute.Id, -num, DUPLICANTS.MODIFIERS.HOLDINGBREATH.NAME, false, false, true);
			this.oxygenBreather = oxygen_breather;
		}

		// Token: 0x060081C3 RID: 33219 RVA: 0x002E3100 File Offset: 0x002E1300
		public bool IsInBreathableArea()
		{
			return base.master.GetComponent<KPrefabID>().HasTag(GameTags.RecoveringBreath) || base.master.GetComponent<Sensors>().GetSensor<BreathableAreaSensor>().IsBreathable() || this.oxygenBreather.HasTag(GameTags.InTransitTube);
		}

		// Token: 0x060081C4 RID: 33220 RVA: 0x002E314D File Offset: 0x002E134D
		public bool HasSuffocated()
		{
			return this.breath.value <= 0f;
		}

		// Token: 0x060081C5 RID: 33221 RVA: 0x002E3164 File Offset: 0x002E1364
		public bool IsSuffocating()
		{
			return this.breath.deltaAttribute.GetTotalValue() <= 0f && this.breath.value <= 45.454548f;
		}

		// Token: 0x060081C6 RID: 33222 RVA: 0x002E3194 File Offset: 0x002E1394
		public void Kill()
		{
			base.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Suffocation);
		}

		// Token: 0x060081C7 RID: 33223 RVA: 0x002E31B8 File Offset: 0x002E13B8
		public void CheckOverPressure()
		{
			if (this.IsInHighPressure())
			{
				if (!this.wasInHighPressure)
				{
					this.wasInHighPressure = true;
					this.highPressureTime = Time.time;
					return;
				}
				if (Time.time - this.highPressureTime > 3f)
				{
					base.master.GetComponent<Effects>().Add("PoppedEarDrums", true);
					return;
				}
			}
			else
			{
				this.wasInHighPressure = false;
			}
		}

		// Token: 0x060081C8 RID: 33224 RVA: 0x002E321C File Offset: 0x002E141C
		private bool IsInHighPressure()
		{
			int num = Grid.PosToCell(base.gameObject);
			for (int i = 0; i < SuffocationMonitor.Instance.pressureTestOffsets.Length; i++)
			{
				int num2 = Grid.OffsetCell(num, SuffocationMonitor.Instance.pressureTestOffsets[i]);
				if (Grid.IsValidCell(num2) && Grid.Element[num2].IsGas && Grid.Mass[num2] > 4f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04006493 RID: 25747
		private AmountInstance breath;

		// Token: 0x04006494 RID: 25748
		public AttributeModifier breathing;

		// Token: 0x04006495 RID: 25749
		public AttributeModifier holdingbreath;

		// Token: 0x04006497 RID: 25751
		private static CellOffset[] pressureTestOffsets = new CellOffset[]
		{
			new CellOffset(0, 0),
			new CellOffset(0, 1)
		};

		// Token: 0x04006498 RID: 25752
		private const float HIGH_PRESSURE_DELAY = 3f;

		// Token: 0x04006499 RID: 25753
		private bool wasInHighPressure;

		// Token: 0x0400649A RID: 25754
		private float highPressureTime;
	}
}
