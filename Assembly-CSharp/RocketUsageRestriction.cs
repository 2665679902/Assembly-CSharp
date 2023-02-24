using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020008EF RID: 2287
public class RocketUsageRestriction : GameStateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>
{
	// Token: 0x060041D1 RID: 16849 RVA: 0x00172730 File Offset: 0x00170930
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.Enter(delegate(RocketUsageRestriction.StatesInstance smi)
		{
			if (DlcManager.FeatureClusterSpaceEnabled() && smi.master.gameObject.GetMyWorld().IsModuleInterior)
			{
				smi.Subscribe(493375141, new Action<object>(smi.OnRefreshUserMenu));
				smi.GoToRestrictionState();
				return;
			}
			smi.StopSM("Not inside rocket or no cluster space");
		});
		this.restriction.Enter(new StateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.State.Callback(this.AquireRocketControlStation)).Enter(delegate(RocketUsageRestriction.StatesInstance smi)
		{
			Components.RocketControlStations.OnAdd += new Action<RocketControlStation>(smi.ControlStationBuilt);
		}).Exit(delegate(RocketUsageRestriction.StatesInstance smi)
		{
			Components.RocketControlStations.OnAdd -= new Action<RocketControlStation>(smi.ControlStationBuilt);
		});
		this.restriction.uncontrolled.ToggleStatusItem(Db.Get().BuildingStatusItems.NoRocketRestriction, null).Enter(delegate(RocketUsageRestriction.StatesInstance smi)
		{
			this.RestrictUsage(smi, false);
		});
		this.restriction.controlled.DefaultState(this.restriction.controlled.nostation);
		this.restriction.controlled.nostation.Enter(new StateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.State.Callback(this.OnRocketRestrictionChanged)).ParamTransition<GameObject>(this.rocketControlStation, this.restriction.controlled.controlled, GameStateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.IsNotNull);
		this.restriction.controlled.controlled.OnTargetLost(this.rocketControlStation, this.restriction.controlled.nostation).Enter(new StateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.State.Callback(this.OnRocketRestrictionChanged)).Target(this.rocketControlStation)
			.EventHandler(GameHashes.RocketRestrictionChanged, new StateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.State.Callback(this.OnRocketRestrictionChanged))
			.Target(this.masterTarget);
	}

	// Token: 0x060041D2 RID: 16850 RVA: 0x001728D5 File Offset: 0x00170AD5
	private void OnRocketRestrictionChanged(RocketUsageRestriction.StatesInstance smi)
	{
		this.RestrictUsage(smi, smi.BuildingRestrictionsActive());
	}

	// Token: 0x060041D3 RID: 16851 RVA: 0x001728E4 File Offset: 0x00170AE4
	private void RestrictUsage(RocketUsageRestriction.StatesInstance smi, bool restrict)
	{
		smi.master.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.RocketRestrictionInactive, !restrict && smi.isControlled, null);
		if (smi.isRestrictionApplied == restrict)
		{
			return;
		}
		smi.isRestrictionApplied = restrict;
		smi.operational.SetFlag(RocketUsageRestriction.rocketUsageAllowed, !smi.def.restrictOperational || !restrict);
		smi.master.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.RocketRestrictionActive, restrict, null);
		Storage[] components = smi.master.gameObject.GetComponents<Storage>();
		if (components != null && components.Length != 0)
		{
			for (int i = 0; i < components.Length; i++)
			{
				if (restrict)
				{
					smi.previousStorageAllowItemRemovalStates = new bool[components.Length];
					smi.previousStorageAllowItemRemovalStates[i] = components[i].allowItemRemoval;
					components[i].allowItemRemoval = false;
				}
				else if (smi.previousStorageAllowItemRemovalStates != null && i < smi.previousStorageAllowItemRemovalStates.Length)
				{
					components[i].allowItemRemoval = smi.previousStorageAllowItemRemovalStates[i];
				}
				foreach (GameObject gameObject in components[i].items)
				{
					gameObject.Trigger(-778359855, components[i]);
				}
			}
		}
		Ownable component = smi.master.GetComponent<Ownable>();
		if (restrict && component != null && component.IsAssigned())
		{
			component.Unassign();
		}
	}

	// Token: 0x060041D4 RID: 16852 RVA: 0x00172A6C File Offset: 0x00170C6C
	private void AquireRocketControlStation(RocketUsageRestriction.StatesInstance smi)
	{
		if (!this.rocketControlStation.IsNull(smi))
		{
			return;
		}
		foreach (object obj in Components.RocketControlStations)
		{
			RocketControlStation rocketControlStation = (RocketControlStation)obj;
			if (rocketControlStation.GetMyWorldId() == smi.GetMyWorldId())
			{
				this.rocketControlStation.Set(rocketControlStation, smi);
			}
		}
	}

	// Token: 0x04002BE3 RID: 11235
	public static readonly Operational.Flag rocketUsageAllowed = new Operational.Flag("rocketUsageAllowed", Operational.Flag.Type.Requirement);

	// Token: 0x04002BE4 RID: 11236
	private StateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.TargetParameter rocketControlStation;

	// Token: 0x04002BE5 RID: 11237
	public RocketUsageRestriction.RestrictionStates restriction;

	// Token: 0x020016B5 RID: 5813
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06008848 RID: 34888 RVA: 0x002F50EF File Offset: 0x002F32EF
		public override void Configure(GameObject prefab)
		{
			RocketControlStation.CONTROLLED_BUILDINGS.Add(prefab.PrefabID());
		}

		// Token: 0x04006AA7 RID: 27303
		public bool initialControlledStateWhenBuilt = true;

		// Token: 0x04006AA8 RID: 27304
		public bool restrictOperational = true;
	}

	// Token: 0x020016B6 RID: 5814
	public class ControlledStates : GameStateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.State
	{
		// Token: 0x04006AA9 RID: 27305
		public GameStateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.State nostation;

		// Token: 0x04006AAA RID: 27306
		public GameStateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.State controlled;
	}

	// Token: 0x020016B7 RID: 5815
	public class RestrictionStates : GameStateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.State
	{
		// Token: 0x04006AAB RID: 27307
		public GameStateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.State uncontrolled;

		// Token: 0x04006AAC RID: 27308
		public RocketUsageRestriction.ControlledStates controlled;
	}

	// Token: 0x020016B8 RID: 5816
	public class StatesInstance : GameStateMachine<RocketUsageRestriction, RocketUsageRestriction.StatesInstance, IStateMachineTarget, RocketUsageRestriction.Def>.GameInstance
	{
		// Token: 0x0600884C RID: 34892 RVA: 0x002F5127 File Offset: 0x002F3327
		public StatesInstance(IStateMachineTarget master, RocketUsageRestriction.Def def)
			: base(master, def)
		{
			this.isControlled = def.initialControlledStateWhenBuilt;
		}

		// Token: 0x0600884D RID: 34893 RVA: 0x002F5144 File Offset: 0x002F3344
		public void OnRefreshUserMenu(object data)
		{
			KIconButtonMenu.ButtonInfo buttonInfo;
			if (this.isControlled)
			{
				buttonInfo = new KIconButtonMenu.ButtonInfo("action_rocket_restriction_uncontrolled", UI.USERMENUACTIONS.ROCKETUSAGERESTRICTION.NAME_UNCONTROLLED, new System.Action(this.OnChange), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.ROCKETUSAGERESTRICTION.TOOLTIP_UNCONTROLLED, true);
			}
			else
			{
				buttonInfo = new KIconButtonMenu.ButtonInfo("action_rocket_restriction_controlled", UI.USERMENUACTIONS.ROCKETUSAGERESTRICTION.NAME_CONTROLLED, new System.Action(this.OnChange), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.ROCKETUSAGERESTRICTION.TOOLTIP_CONTROLLED, true);
			}
			Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 11f);
		}

		// Token: 0x0600884E RID: 34894 RVA: 0x002F51E0 File Offset: 0x002F33E0
		public void ControlStationBuilt(object o)
		{
			base.sm.AquireRocketControlStation(base.smi);
		}

		// Token: 0x0600884F RID: 34895 RVA: 0x002F51F3 File Offset: 0x002F33F3
		private void OnChange()
		{
			this.isControlled = !this.isControlled;
			this.GoToRestrictionState();
		}

		// Token: 0x06008850 RID: 34896 RVA: 0x002F520C File Offset: 0x002F340C
		public void GoToRestrictionState()
		{
			if (base.smi.isControlled)
			{
				base.smi.GoTo(base.sm.restriction.controlled);
				return;
			}
			base.smi.GoTo(base.sm.restriction.uncontrolled);
		}

		// Token: 0x06008851 RID: 34897 RVA: 0x002F525D File Offset: 0x002F345D
		public bool BuildingRestrictionsActive()
		{
			return this.isControlled && !base.sm.rocketControlStation.IsNull(base.smi) && base.sm.rocketControlStation.Get<RocketControlStation>(base.smi).BuildingRestrictionsActive;
		}

		// Token: 0x04006AAD RID: 27309
		[MyCmpGet]
		public Operational operational;

		// Token: 0x04006AAE RID: 27310
		public bool[] previousStorageAllowItemRemovalStates;

		// Token: 0x04006AAF RID: 27311
		[Serialize]
		public bool isControlled = true;

		// Token: 0x04006AB0 RID: 27312
		public bool isRestrictionApplied;
	}
}
