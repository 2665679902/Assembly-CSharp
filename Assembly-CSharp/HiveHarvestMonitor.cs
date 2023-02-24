using System;
using STRINGS;

// Token: 0x020000C7 RID: 199
public class HiveHarvestMonitor : GameStateMachine<HiveHarvestMonitor, HiveHarvestMonitor.Instance, IStateMachineTarget, HiveHarvestMonitor.Def>
{
	// Token: 0x06000373 RID: 883 RVA: 0x0001AF84 File Offset: 0x00019184
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.do_not_harvest;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.EventHandler(GameHashes.RefreshUserMenu, delegate(HiveHarvestMonitor.Instance smi)
		{
			smi.OnRefreshUserMenu();
		});
		this.do_not_harvest.ParamTransition<bool>(this.shouldHarvest, this.harvest, (HiveHarvestMonitor.Instance smi, bool bShouldHarvest) => bShouldHarvest);
		this.harvest.ParamTransition<bool>(this.shouldHarvest, this.do_not_harvest, (HiveHarvestMonitor.Instance smi, bool bShouldHarvest) => !bShouldHarvest).DefaultState(this.harvest.not_ready);
		this.harvest.not_ready.EventTransition(GameHashes.OnStorageChange, this.harvest.ready, (HiveHarvestMonitor.Instance smi) => smi.storage.GetMassAvailable(smi.def.producedOre) >= smi.def.harvestThreshold);
		this.harvest.ready.ToggleChore((HiveHarvestMonitor.Instance smi) => smi.CreateHarvestChore(), this.harvest.not_ready).EventTransition(GameHashes.OnStorageChange, this.harvest.not_ready, (HiveHarvestMonitor.Instance smi) => smi.storage.GetMassAvailable(smi.def.producedOre) < smi.def.harvestThreshold);
	}

	// Token: 0x0400023E RID: 574
	public StateMachine<HiveHarvestMonitor, HiveHarvestMonitor.Instance, IStateMachineTarget, HiveHarvestMonitor.Def>.BoolParameter shouldHarvest;

	// Token: 0x0400023F RID: 575
	public GameStateMachine<HiveHarvestMonitor, HiveHarvestMonitor.Instance, IStateMachineTarget, HiveHarvestMonitor.Def>.State do_not_harvest;

	// Token: 0x04000240 RID: 576
	public HiveHarvestMonitor.HarvestStates harvest;

	// Token: 0x02000E70 RID: 3696
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005172 RID: 20850
		public Tag producedOre;

		// Token: 0x04005173 RID: 20851
		public float harvestThreshold;
	}

	// Token: 0x02000E71 RID: 3697
	public class HarvestStates : GameStateMachine<HiveHarvestMonitor, HiveHarvestMonitor.Instance, IStateMachineTarget, HiveHarvestMonitor.Def>.State
	{
		// Token: 0x04005174 RID: 20852
		public GameStateMachine<HiveHarvestMonitor, HiveHarvestMonitor.Instance, IStateMachineTarget, HiveHarvestMonitor.Def>.State not_ready;

		// Token: 0x04005175 RID: 20853
		public GameStateMachine<HiveHarvestMonitor, HiveHarvestMonitor.Instance, IStateMachineTarget, HiveHarvestMonitor.Def>.State ready;
	}

	// Token: 0x02000E72 RID: 3698
	public new class Instance : GameStateMachine<HiveHarvestMonitor, HiveHarvestMonitor.Instance, IStateMachineTarget, HiveHarvestMonitor.Def>.GameInstance
	{
		// Token: 0x06006C3D RID: 27709 RVA: 0x00297A4E File Offset: 0x00295C4E
		public Instance(IStateMachineTarget master, HiveHarvestMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06006C3E RID: 27710 RVA: 0x00297A58 File Offset: 0x00295C58
		public void OnRefreshUserMenu()
		{
			if (base.sm.shouldHarvest.Get(this))
			{
				Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_building_disabled", UI.USERMENUACTIONS.CANCELEMPTYBEEHIVE.NAME, delegate
				{
					base.sm.shouldHarvest.Set(false, this, false);
				}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCELEMPTYBEEHIVE.TOOLTIP, true), 1f);
				return;
			}
			Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_empty_contents", UI.USERMENUACTIONS.EMPTYBEEHIVE.NAME, delegate
			{
				base.sm.shouldHarvest.Set(true, this, false);
			}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.EMPTYBEEHIVE.TOOLTIP, true), 1f);
		}

		// Token: 0x06006C3F RID: 27711 RVA: 0x00297B14 File Offset: 0x00295D14
		public Chore CreateHarvestChore()
		{
			return new WorkChore<HiveWorkableEmpty>(Db.Get().ChoreTypes.Ranch, base.master.GetComponent<HiveWorkableEmpty>(), null, true, new Action<Chore>(base.smi.OnEmptyComplete), null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x06006C40 RID: 27712 RVA: 0x00297B61 File Offset: 0x00295D61
		public void OnEmptyComplete(Chore chore)
		{
			base.smi.storage.Drop(base.smi.def.producedOre);
		}

		// Token: 0x04005176 RID: 20854
		[MyCmpReq]
		public Storage storage;
	}
}
