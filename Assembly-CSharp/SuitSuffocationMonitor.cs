using System;
using Klei.AI;
using STRINGS;

// Token: 0x020009A2 RID: 2466
public class SuitSuffocationMonitor : GameStateMachine<SuitSuffocationMonitor, SuitSuffocationMonitor.Instance>
{
	// Token: 0x06004937 RID: 18743 RVA: 0x00199F50 File Offset: 0x00198150
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.DefaultState(this.satisfied.normal).ToggleAttributeModifier("Breathing", (SuitSuffocationMonitor.Instance smi) => smi.breathing, null).Transition(this.nooxygen, (SuitSuffocationMonitor.Instance smi) => smi.IsTankEmpty(), UpdateRate.SIM_200ms);
		this.satisfied.normal.Transition(this.satisfied.low, (SuitSuffocationMonitor.Instance smi) => smi.suitTank.NeedsRecharging(), UpdateRate.SIM_200ms);
		this.satisfied.low.DoNothing();
		this.nooxygen.ToggleExpression(Db.Get().Expressions.Suffocate, null).ToggleAttributeModifier("Holding Breath", (SuitSuffocationMonitor.Instance smi) => smi.holdingbreath, null).ToggleTag(GameTags.NoOxygen)
			.DefaultState(this.nooxygen.holdingbreath);
		this.nooxygen.holdingbreath.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Suffocation, Db.Get().DuplicantStatusItems.HoldingBreath, null).Transition(this.nooxygen.suffocating, (SuitSuffocationMonitor.Instance smi) => smi.IsSuffocating(), UpdateRate.SIM_200ms);
		this.nooxygen.suffocating.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Suffocation, Db.Get().DuplicantStatusItems.Suffocating, null).Transition(this.death, (SuitSuffocationMonitor.Instance smi) => smi.HasSuffocated(), UpdateRate.SIM_200ms);
		this.death.Enter("SuffocationDeath", delegate(SuitSuffocationMonitor.Instance smi)
		{
			smi.Kill();
		});
	}

	// Token: 0x04003017 RID: 12311
	public SuitSuffocationMonitor.SatisfiedState satisfied;

	// Token: 0x04003018 RID: 12312
	public SuitSuffocationMonitor.NoOxygenState nooxygen;

	// Token: 0x04003019 RID: 12313
	public GameStateMachine<SuitSuffocationMonitor, SuitSuffocationMonitor.Instance, IStateMachineTarget, object>.State death;

	// Token: 0x020017A4 RID: 6052
	public class NoOxygenState : GameStateMachine<SuitSuffocationMonitor, SuitSuffocationMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006D9D RID: 28061
		public GameStateMachine<SuitSuffocationMonitor, SuitSuffocationMonitor.Instance, IStateMachineTarget, object>.State holdingbreath;

		// Token: 0x04006D9E RID: 28062
		public GameStateMachine<SuitSuffocationMonitor, SuitSuffocationMonitor.Instance, IStateMachineTarget, object>.State suffocating;
	}

	// Token: 0x020017A5 RID: 6053
	public class SatisfiedState : GameStateMachine<SuitSuffocationMonitor, SuitSuffocationMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006D9F RID: 28063
		public GameStateMachine<SuitSuffocationMonitor, SuitSuffocationMonitor.Instance, IStateMachineTarget, object>.State normal;

		// Token: 0x04006DA0 RID: 28064
		public GameStateMachine<SuitSuffocationMonitor, SuitSuffocationMonitor.Instance, IStateMachineTarget, object>.State low;
	}

	// Token: 0x020017A6 RID: 6054
	public new class Instance : GameStateMachine<SuitSuffocationMonitor, SuitSuffocationMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06008B6B RID: 35691 RVA: 0x002FF545 File Offset: 0x002FD745
		// (set) Token: 0x06008B6C RID: 35692 RVA: 0x002FF54D File Offset: 0x002FD74D
		public SuitTank suitTank { get; private set; }

		// Token: 0x06008B6D RID: 35693 RVA: 0x002FF558 File Offset: 0x002FD758
		public Instance(IStateMachineTarget master, SuitTank suit_tank)
			: base(master)
		{
			this.breath = Db.Get().Amounts.Breath.Lookup(master.gameObject);
			Klei.AI.Attribute deltaAttribute = Db.Get().Amounts.Breath.deltaAttribute;
			float num = 0.90909094f;
			this.breathing = new AttributeModifier(deltaAttribute.Id, num, DUPLICANTS.MODIFIERS.BREATHING.NAME, false, false, true);
			this.holdingbreath = new AttributeModifier(deltaAttribute.Id, -num, DUPLICANTS.MODIFIERS.HOLDINGBREATH.NAME, false, false, true);
			this.suitTank = suit_tank;
		}

		// Token: 0x06008B6E RID: 35694 RVA: 0x002FF5ED File Offset: 0x002FD7ED
		public bool IsTankEmpty()
		{
			return this.suitTank.IsEmpty();
		}

		// Token: 0x06008B6F RID: 35695 RVA: 0x002FF5FA File Offset: 0x002FD7FA
		public bool HasSuffocated()
		{
			return this.breath.value <= 0f;
		}

		// Token: 0x06008B70 RID: 35696 RVA: 0x002FF611 File Offset: 0x002FD811
		public bool IsSuffocating()
		{
			return this.breath.value <= 45.454548f;
		}

		// Token: 0x06008B71 RID: 35697 RVA: 0x002FF628 File Offset: 0x002FD828
		public void Kill()
		{
			base.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Suffocation);
		}

		// Token: 0x04006DA1 RID: 28065
		private AmountInstance breath;

		// Token: 0x04006DA2 RID: 28066
		public AttributeModifier breathing;

		// Token: 0x04006DA3 RID: 28067
		public AttributeModifier holdingbreath;

		// Token: 0x04006DA4 RID: 28068
		private OxygenBreather masterOxygenBreather;
	}
}
