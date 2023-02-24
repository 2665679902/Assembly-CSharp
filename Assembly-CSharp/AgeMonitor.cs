using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006BC RID: 1724
public class AgeMonitor : GameStateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>
{
	// Token: 0x06002EF3 RID: 12019 RVA: 0x000F888C File Offset: 0x000F6A8C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.alive;
		this.alive.ToggleAttributeModifier("Aging", (AgeMonitor.Instance smi) => this.aging, null).Transition(this.time_to_die, new StateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>.Transition.ConditionCallback(AgeMonitor.TimeToDie), UpdateRate.SIM_1000ms).Update(new Action<AgeMonitor.Instance, float>(AgeMonitor.UpdateOldStatusItem), UpdateRate.SIM_1000ms, false);
		this.time_to_die.Enter(new StateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>.State.Callback(AgeMonitor.Die));
		this.aging = new AttributeModifier(Db.Get().Amounts.Age.deltaAttribute.Id, 0.0016666667f, CREATURES.MODIFIERS.AGE.NAME, false, false, true);
	}

	// Token: 0x06002EF4 RID: 12020 RVA: 0x000F8938 File Offset: 0x000F6B38
	private static void Die(AgeMonitor.Instance smi)
	{
		smi.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Generic);
	}

	// Token: 0x06002EF5 RID: 12021 RVA: 0x000F8954 File Offset: 0x000F6B54
	private static bool TimeToDie(AgeMonitor.Instance smi)
	{
		return smi.age.value >= smi.age.GetMax();
	}

	// Token: 0x06002EF6 RID: 12022 RVA: 0x000F8974 File Offset: 0x000F6B74
	private static void UpdateOldStatusItem(AgeMonitor.Instance smi, float dt)
	{
		KSelectable component = smi.GetComponent<KSelectable>();
		bool flag = smi.age.value > smi.age.GetMax() * 0.9f;
		smi.oldStatusGuid = component.ToggleStatusItem(Db.Get().CreatureStatusItems.Old, smi.oldStatusGuid, flag, smi);
	}

	// Token: 0x04001C41 RID: 7233
	public GameStateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>.State alive;

	// Token: 0x04001C42 RID: 7234
	public GameStateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>.State time_to_die;

	// Token: 0x04001C43 RID: 7235
	private AttributeModifier aging;

	// Token: 0x02001394 RID: 5012
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06007E5D RID: 32349 RVA: 0x002D8FD9 File Offset: 0x002D71D9
		public override void Configure(GameObject prefab)
		{
			prefab.AddOrGet<Modifiers>().initialAmounts.Add(Db.Get().Amounts.Age.Id);
		}

		// Token: 0x04006122 RID: 24866
		public float maxAgePercentOnSpawn = 0.75f;
	}

	// Token: 0x02001395 RID: 5013
	public new class Instance : GameStateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>.GameInstance
	{
		// Token: 0x06007E5F RID: 32351 RVA: 0x002D9014 File Offset: 0x002D7214
		public Instance(IStateMachineTarget master, AgeMonitor.Def def)
			: base(master, def)
		{
			this.age = Db.Get().Amounts.Age.Lookup(base.gameObject);
			base.Subscribe(1119167081, delegate(object data)
			{
				this.RandomizeAge();
			});
		}

		// Token: 0x06007E60 RID: 32352 RVA: 0x002D9060 File Offset: 0x002D7260
		public void RandomizeAge()
		{
			this.age.value = UnityEngine.Random.value * this.age.GetMax() * base.def.maxAgePercentOnSpawn;
			AmountInstance amountInstance = Db.Get().Amounts.Fertility.Lookup(base.gameObject);
			if (amountInstance != null)
			{
				amountInstance.value = this.age.value / this.age.GetMax() * amountInstance.GetMax() * 1.75f;
				amountInstance.value = Mathf.Min(amountInstance.value, amountInstance.GetMax() * 0.9f);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06007E61 RID: 32353 RVA: 0x002D90FA File Offset: 0x002D72FA
		public float CyclesUntilDeath
		{
			get
			{
				return this.age.GetMax() - this.age.value;
			}
		}

		// Token: 0x04006123 RID: 24867
		public AmountInstance age;

		// Token: 0x04006124 RID: 24868
		public Guid oldStatusGuid;
	}
}
