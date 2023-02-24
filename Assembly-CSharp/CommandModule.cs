using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000942 RID: 2370
[SerializationConfig(MemberSerialization.OptIn)]
public class CommandModule : StateMachineComponent<CommandModule.StatesInstance>
{
	// Token: 0x060045C9 RID: 17865 RVA: 0x00188D3C File Offset: 0x00186F3C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.rocketStats = new RocketStats(this);
		this.conditions = base.GetComponent<RocketCommandConditions>();
	}

	// Token: 0x060045CA RID: 17866 RVA: 0x00188D5C File Offset: 0x00186F5C
	public void ReleaseAstronaut(bool fill_bladder)
	{
		if (this.releasingAstronaut)
		{
			return;
		}
		this.releasingAstronaut = true;
		MinionStorage component = base.GetComponent<MinionStorage>();
		List<MinionStorage.Info> storedMinionInfo = component.GetStoredMinionInfo();
		for (int i = storedMinionInfo.Count - 1; i >= 0; i--)
		{
			MinionStorage.Info info = storedMinionInfo[i];
			GameObject gameObject = component.DeserializeMinion(info.id, Grid.CellToPos(Grid.PosToCell(base.smi.master.transform.GetPosition())));
			if (!(gameObject == null))
			{
				if (Grid.FakeFloor[Grid.OffsetCell(Grid.PosToCell(base.smi.master.gameObject), 0, -1)])
				{
					gameObject.GetComponent<Navigator>().SetCurrentNavType(NavType.Floor);
				}
				if (fill_bladder)
				{
					AmountInstance amountInstance = Db.Get().Amounts.Bladder.Lookup(gameObject);
					if (amountInstance != null)
					{
						amountInstance.value = amountInstance.GetMax();
					}
				}
			}
		}
		this.releasingAstronaut = false;
	}

	// Token: 0x060045CB RID: 17867 RVA: 0x00188E48 File Offset: 0x00187048
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.storage = base.GetComponent<Storage>();
		this.assignable = base.GetComponent<Assignable>();
		this.assignable.AddAssignPrecondition(new Func<MinionAssignablesProxy, bool>(this.CanAssignTo));
		base.smi.StartSM();
		int num = Grid.PosToCell(base.gameObject);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("CommandModule.gantryChanged", base.gameObject, num, GameScenePartitioner.Instance.validNavCellChangedLayer, new Action<object>(this.OnGantryChanged));
		this.OnGantryChanged(null);
	}

	// Token: 0x060045CC RID: 17868 RVA: 0x00188EDC File Offset: 0x001870DC
	private bool CanAssignTo(MinionAssignablesProxy worker)
	{
		if (worker.target is MinionIdentity)
		{
			return (worker.target as KMonoBehaviour).GetComponent<MinionResume>().HasPerk(Db.Get().SkillPerks.CanUseRockets);
		}
		return worker.target is StoredMinionIdentity && (worker.target as StoredMinionIdentity).HasPerk(Db.Get().SkillPerks.CanUseRockets);
	}

	// Token: 0x060045CD RID: 17869 RVA: 0x00188F4C File Offset: 0x0018714C
	private static bool HasValidGantry(GameObject go)
	{
		int num = Grid.OffsetCell(Grid.PosToCell(go), 0, -1);
		return Grid.IsValidCell(num) && Grid.FakeFloor[num];
	}

	// Token: 0x060045CE RID: 17870 RVA: 0x00188F7C File Offset: 0x0018717C
	private void OnGantryChanged(object data)
	{
		if (base.gameObject != null)
		{
			KSelectable component = base.GetComponent<KSelectable>();
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.HasGantry, false);
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.MissingGantry, false);
			if (CommandModule.HasValidGantry(base.smi.master.gameObject))
			{
				component.AddStatusItem(Db.Get().BuildingStatusItems.HasGantry, null);
			}
			else
			{
				component.AddStatusItem(Db.Get().BuildingStatusItems.MissingGantry, null);
			}
			base.smi.sm.gantryChanged.Trigger(base.smi);
		}
	}

	// Token: 0x060045CF RID: 17871 RVA: 0x00189034 File Offset: 0x00187234
	private Chore CreateWorkChore()
	{
		WorkChore<CommandModuleWorkable> workChore = new WorkChore<CommandModuleWorkable>(Db.Get().ChoreTypes.Astronaut, this, null, true, null, null, null, false, null, false, true, Assets.GetAnim("anim_hat_kanim"), false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, true);
		workChore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanUseRockets);
		workChore.AddPrecondition(ChorePreconditions.instance.IsAssignedtoMe, this.assignable);
		return workChore;
	}

	// Token: 0x060045D0 RID: 17872 RVA: 0x001890AA File Offset: 0x001872AA
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		this.partitionerEntry.Clear();
		this.ReleaseAstronaut(false);
		base.smi.StopSM("cleanup");
	}

	// Token: 0x04002E5B RID: 11867
	public Storage storage;

	// Token: 0x04002E5C RID: 11868
	public RocketStats rocketStats;

	// Token: 0x04002E5D RID: 11869
	public RocketCommandConditions conditions;

	// Token: 0x04002E5E RID: 11870
	private bool releasingAstronaut;

	// Token: 0x04002E5F RID: 11871
	private const Sim.Cell.Properties floorCellProperties = (Sim.Cell.Properties)39;

	// Token: 0x04002E60 RID: 11872
	public Assignable assignable;

	// Token: 0x04002E61 RID: 11873
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x02001733 RID: 5939
	public class StatesInstance : GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.GameInstance
	{
		// Token: 0x060089F3 RID: 35315 RVA: 0x002F9FB8 File Offset: 0x002F81B8
		public StatesInstance(CommandModule master)
			: base(master)
		{
		}

		// Token: 0x060089F4 RID: 35316 RVA: 0x002F9FC4 File Offset: 0x002F81C4
		public void SetSuspended(bool suspended)
		{
			Storage component = base.GetComponent<Storage>();
			if (component != null)
			{
				component.allowItemRemoval = !suspended;
			}
			ManualDeliveryKG component2 = base.GetComponent<ManualDeliveryKG>();
			if (component2 != null)
			{
				component2.Pause(suspended, "Rocket is suspended");
			}
		}

		// Token: 0x060089F5 RID: 35317 RVA: 0x002FA008 File Offset: 0x002F8208
		public bool CheckStoredMinionIsAssignee()
		{
			foreach (MinionStorage.Info info in base.GetComponent<MinionStorage>().GetStoredMinionInfo())
			{
				if (info.serializedMinion != null)
				{
					KPrefabID kprefabID = info.serializedMinion.Get();
					if (!(kprefabID == null))
					{
						StoredMinionIdentity component = kprefabID.GetComponent<StoredMinionIdentity>();
						if (base.GetComponent<Assignable>().assignee == component.assignableProxy.Get())
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	// Token: 0x02001734 RID: 5940
	public class States : GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule>
	{
		// Token: 0x060089F6 RID: 35318 RVA: 0x002FA0A0 File Offset: 0x002F82A0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.grounded;
			this.grounded.PlayAnim("grounded", KAnim.PlayMode.Loop).DefaultState(this.grounded.awaitingAstronaut).TagTransition(GameTags.RocketNotOnGround, this.spaceborne, false);
			this.grounded.refreshChore.GoTo(this.grounded.awaitingAstronaut);
			this.grounded.awaitingAstronaut.Enter(delegate(CommandModule.StatesInstance smi)
			{
				if (smi.CheckStoredMinionIsAssignee())
				{
					smi.GoTo(this.grounded.hasAstronaut);
				}
				Game.Instance.userMenu.Refresh(smi.gameObject);
			}).EventHandler(GameHashes.AssigneeChanged, delegate(CommandModule.StatesInstance smi)
			{
				if (smi.CheckStoredMinionIsAssignee())
				{
					smi.GoTo(this.grounded.hasAstronaut);
				}
				else
				{
					smi.GoTo(this.grounded.refreshChore);
				}
				Game.Instance.userMenu.Refresh(smi.gameObject);
			}).ToggleChore((CommandModule.StatesInstance smi) => smi.master.CreateWorkChore(), this.grounded.hasAstronaut);
			this.grounded.hasAstronaut.EventHandler(GameHashes.AssigneeChanged, delegate(CommandModule.StatesInstance smi)
			{
				if (!smi.CheckStoredMinionIsAssignee())
				{
					smi.GoTo(this.grounded.waitingToRelease);
				}
			});
			this.grounded.waitingToRelease.ToggleStatusItem(Db.Get().BuildingStatusItems.DisembarkingDuplicant, null).OnSignal(this.gantryChanged, this.grounded.awaitingAstronaut, delegate(CommandModule.StatesInstance smi)
			{
				if (CommandModule.HasValidGantry(smi.gameObject))
				{
					smi.master.ReleaseAstronaut(this.accumulatedPee.Get(smi));
					this.accumulatedPee.Set(false, smi, false);
					Game.Instance.userMenu.Refresh(smi.gameObject);
					return true;
				}
				return false;
			});
			this.spaceborne.DefaultState(this.spaceborne.launch);
			this.spaceborne.launch.Enter(delegate(CommandModule.StatesInstance smi)
			{
				smi.SetSuspended(true);
			}).GoTo(this.spaceborne.idle);
			this.spaceborne.idle.TagTransition(GameTags.RocketNotOnGround, this.spaceborne.land, true);
			this.spaceborne.land.Enter(delegate(CommandModule.StatesInstance smi)
			{
				smi.SetSuspended(false);
				Game.Instance.userMenu.Refresh(smi.gameObject);
				this.accumulatedPee.Set(true, smi, false);
			}).GoTo(this.grounded.waitingToRelease);
		}

		// Token: 0x04006C55 RID: 27733
		public StateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.Signal gantryChanged;

		// Token: 0x04006C56 RID: 27734
		public StateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.BoolParameter accumulatedPee;

		// Token: 0x04006C57 RID: 27735
		public CommandModule.States.GroundedStates grounded;

		// Token: 0x04006C58 RID: 27736
		public CommandModule.States.SpaceborneStates spaceborne;

		// Token: 0x020020B8 RID: 8376
		public class GroundedStates : GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.State
		{
			// Token: 0x040091BE RID: 37310
			public GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.State refreshChore;

			// Token: 0x040091BF RID: 37311
			public GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.State awaitingAstronaut;

			// Token: 0x040091C0 RID: 37312
			public GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.State hasAstronaut;

			// Token: 0x040091C1 RID: 37313
			public GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.State waitingToRelease;
		}

		// Token: 0x020020B9 RID: 8377
		public class SpaceborneStates : GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.State
		{
			// Token: 0x040091C2 RID: 37314
			public GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.State launch;

			// Token: 0x040091C3 RID: 37315
			public GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.State idle;

			// Token: 0x040091C4 RID: 37316
			public GameStateMachine<CommandModule.States, CommandModule.StatesInstance, CommandModule, object>.State land;
		}
	}
}
