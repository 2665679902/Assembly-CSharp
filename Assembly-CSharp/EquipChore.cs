using System;

// Token: 0x02000381 RID: 897
public class EquipChore : Chore<EquipChore.StatesInstance>
{
	// Token: 0x06001242 RID: 4674 RVA: 0x00061890 File Offset: 0x0005FA90
	public EquipChore(IStateMachineTarget equippable)
		: base(Db.Get().ChoreTypes.Equip, equippable, null, false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new EquipChore.StatesInstance(this);
		base.smi.sm.equippable_source.Set(equippable.gameObject, base.smi, false);
		base.smi.sm.requested_units.Set(1f, base.smi, false);
		this.showAvailabilityInHoverText = false;
		Prioritizable.AddRef(equippable.gameObject);
		Game.Instance.Trigger(1980521255, equippable.gameObject);
		base.AddPrecondition(ChorePreconditions.instance.IsAssignedtoMe, equippable.GetComponent<Assignable>());
		base.AddPrecondition(ChorePreconditions.instance.CanPickup, equippable.GetComponent<Pickupable>());
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x00061964 File Offset: 0x0005FB64
	public override void Begin(Chore.Precondition.Context context)
	{
		if (context.consumerState.consumer == null)
		{
			Debug.LogError("EquipChore null context.consumer");
			return;
		}
		if (base.smi == null)
		{
			Debug.LogError("EquipChore null smi");
			return;
		}
		if (base.smi.sm == null)
		{
			Debug.LogError("EquipChore null smi.sm");
			return;
		}
		if (base.smi.sm.equippable_source == null)
		{
			Debug.LogError("EquipChore null smi.sm.equippable_source");
			return;
		}
		base.smi.sm.equipper.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x02000F61 RID: 3937
	public class StatesInstance : GameStateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore, object>.GameInstance
	{
		// Token: 0x06006F52 RID: 28498 RVA: 0x002A07E9 File Offset: 0x0029E9E9
		public StatesInstance(EquipChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02000F62 RID: 3938
	public class States : GameStateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore>
	{
		// Token: 0x06006F53 RID: 28499 RVA: 0x002A07F4 File Offset: 0x0029E9F4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.fetch;
			base.Target(this.equipper);
			this.root.DoNothing();
			this.fetch.InitializeStates(this.equipper, this.equippable_source, this.equippable_result, this.requested_units, this.actual_units, this.equip, null);
			this.equip.ToggleWork<EquippableWorkable>(this.equippable_result, null, null, null);
		}

		// Token: 0x04005441 RID: 21569
		public StateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore, object>.TargetParameter equipper;

		// Token: 0x04005442 RID: 21570
		public StateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore, object>.TargetParameter equippable_source;

		// Token: 0x04005443 RID: 21571
		public StateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore, object>.TargetParameter equippable_result;

		// Token: 0x04005444 RID: 21572
		public StateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore, object>.FloatParameter requested_units;

		// Token: 0x04005445 RID: 21573
		public StateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore, object>.FloatParameter actual_units;

		// Token: 0x04005446 RID: 21574
		public GameStateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore, object>.FetchSubState fetch;

		// Token: 0x04005447 RID: 21575
		public EquipChore.States.Equip equip;

		// Token: 0x02001EAE RID: 7854
		public class Equip : GameStateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore, object>.State
		{
			// Token: 0x0400896E RID: 35182
			public GameStateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore, object>.State pre;

			// Token: 0x0400896F RID: 35183
			public GameStateMachine<EquipChore.States, EquipChore.StatesInstance, EquipChore, object>.State pst;
		}
	}
}
