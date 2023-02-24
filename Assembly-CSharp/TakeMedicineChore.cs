using System;
using STRINGS;

// Token: 0x0200039A RID: 922
public class TakeMedicineChore : Chore<TakeMedicineChore.StatesInstance>
{
	// Token: 0x0600129E RID: 4766 RVA: 0x00063AC4 File Offset: 0x00061CC4
	public TakeMedicineChore(MedicinalPillWorkable master)
		: base(Db.Get().ChoreTypes.TakeMedicine, master, null, false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		this.medicine = master;
		this.pickupable = this.medicine.GetComponent<Pickupable>();
		base.smi = new TakeMedicineChore.StatesInstance(this);
		base.AddPrecondition(ChorePreconditions.instance.CanPickup, this.pickupable);
		base.AddPrecondition(TakeMedicineChore.CanCure, this);
		base.AddPrecondition(TakeMedicineChore.IsConsumptionPermitted, this);
		base.AddPrecondition(ChorePreconditions.instance.IsNotARobot, this);
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x00063B58 File Offset: 0x00061D58
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.source.Set(this.pickupable.gameObject, base.smi, false);
		base.smi.sm.requestedpillcount.Set(1f, base.smi, false);
		base.smi.sm.eater.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
		new TakeMedicineChore(this.medicine);
	}

	// Token: 0x04000A08 RID: 2568
	private Pickupable pickupable;

	// Token: 0x04000A09 RID: 2569
	private MedicinalPillWorkable medicine;

	// Token: 0x04000A0A RID: 2570
	public static readonly Chore.Precondition CanCure = new Chore.Precondition
	{
		id = "CanCure",
		description = DUPLICANTS.CHORES.PRECONDITIONS.CAN_CURE,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return ((TakeMedicineChore)data).medicine.CanBeTakenBy(context.consumerState.gameObject);
		}
	};

	// Token: 0x04000A0B RID: 2571
	public static readonly Chore.Precondition IsConsumptionPermitted = new Chore.Precondition
	{
		id = "IsConsumptionPermitted",
		description = DUPLICANTS.CHORES.PRECONDITIONS.IS_CONSUMPTION_PERMITTED,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			TakeMedicineChore takeMedicineChore = (TakeMedicineChore)data;
			ConsumableConsumer consumableConsumer = context.consumerState.consumableConsumer;
			return consumableConsumer == null || consumableConsumer.IsPermitted(takeMedicineChore.medicine.PrefabID().Name);
		}
	};

	// Token: 0x02000F9C RID: 3996
	public class StatesInstance : GameStateMachine<TakeMedicineChore.States, TakeMedicineChore.StatesInstance, TakeMedicineChore, object>.GameInstance
	{
		// Token: 0x06007009 RID: 28681 RVA: 0x002A55D1 File Offset: 0x002A37D1
		public StatesInstance(TakeMedicineChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02000F9D RID: 3997
	public class States : GameStateMachine<TakeMedicineChore.States, TakeMedicineChore.StatesInstance, TakeMedicineChore>
	{
		// Token: 0x0600700A RID: 28682 RVA: 0x002A55DC File Offset: 0x002A37DC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.fetch;
			base.Target(this.eater);
			this.fetch.InitializeStates(this.eater, this.source, this.chunk, this.requestedpillcount, this.actualpillcount, this.takemedicine, null);
			this.takemedicine.ToggleAnims("anim_eat_floor_kanim", 0f, "").ToggleTag(GameTags.TakingMedicine).ToggleWork("TakeMedicine", delegate(TakeMedicineChore.StatesInstance smi)
			{
				MedicinalPillWorkable medicinalPillWorkable = this.chunk.Get<MedicinalPillWorkable>(smi);
				this.eater.Get<Worker>(smi).StartWork(new Worker.StartWorkInfo(medicinalPillWorkable));
			}, (TakeMedicineChore.StatesInstance smi) => this.chunk.Get<MedicinalPill>(smi) != null, null, null);
		}

		// Token: 0x040054FE RID: 21758
		public StateMachine<TakeMedicineChore.States, TakeMedicineChore.StatesInstance, TakeMedicineChore, object>.TargetParameter eater;

		// Token: 0x040054FF RID: 21759
		public StateMachine<TakeMedicineChore.States, TakeMedicineChore.StatesInstance, TakeMedicineChore, object>.TargetParameter source;

		// Token: 0x04005500 RID: 21760
		public StateMachine<TakeMedicineChore.States, TakeMedicineChore.StatesInstance, TakeMedicineChore, object>.TargetParameter chunk;

		// Token: 0x04005501 RID: 21761
		public StateMachine<TakeMedicineChore.States, TakeMedicineChore.StatesInstance, TakeMedicineChore, object>.FloatParameter requestedpillcount;

		// Token: 0x04005502 RID: 21762
		public StateMachine<TakeMedicineChore.States, TakeMedicineChore.StatesInstance, TakeMedicineChore, object>.FloatParameter actualpillcount;

		// Token: 0x04005503 RID: 21763
		public GameStateMachine<TakeMedicineChore.States, TakeMedicineChore.StatesInstance, TakeMedicineChore, object>.FetchSubState fetch;

		// Token: 0x04005504 RID: 21764
		public GameStateMachine<TakeMedicineChore.States, TakeMedicineChore.StatesInstance, TakeMedicineChore, object>.State takemedicine;
	}
}
