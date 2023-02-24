using System;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class BeeMakeHiveStates : GameStateMachine<BeeMakeHiveStates, BeeMakeHiveStates.Instance, IStateMachineTarget, BeeMakeHiveStates.Def>
{
	// Token: 0x060002F1 RID: 753 RVA: 0x00017A1C File Offset: 0x00015C1C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.findBuildLocation;
		this.root.DoNothing();
		this.findBuildLocation.Enter(delegate(BeeMakeHiveStates.Instance smi)
		{
			this.FindBuildLocation(smi);
			if (smi.targetBuildCell != Grid.InvalidCell)
			{
				smi.GoTo(this.moveToBuildLocation);
				return;
			}
			smi.GoTo(this.behaviourcomplete);
		});
		this.moveToBuildLocation.MoveTo((BeeMakeHiveStates.Instance smi) => smi.targetBuildCell, this.doBuild, this.behaviourcomplete, false);
		this.doBuild.PlayAnim("hive_grow_pre").EventHandler(GameHashes.AnimQueueComplete, delegate(BeeMakeHiveStates.Instance smi)
		{
			if (smi.gameObject.GetComponent<Bee>().FindHiveInRoom() == null)
			{
				smi.builtHome = true;
				smi.BuildHome();
			}
			smi.GoTo(this.behaviourcomplete);
		});
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToMakeHome, false).Exit(delegate(BeeMakeHiveStates.Instance smi)
		{
			if (smi.builtHome)
			{
				Util.KDestroyGameObject(smi.master.gameObject);
			}
		});
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00017AEC File Offset: 0x00015CEC
	private void FindBuildLocation(BeeMakeHiveStates.Instance smi)
	{
		smi.targetBuildCell = Grid.InvalidCell;
		GameObject prefab = Assets.GetPrefab("BeeHive".ToTag());
		BuildingPlacementQuery buildingPlacementQuery = PathFinderQueries.buildingPlacementQuery.Reset(1, prefab);
		smi.GetComponent<Navigator>().RunQuery(buildingPlacementQuery);
		if (buildingPlacementQuery.result_cells.Count > 0)
		{
			smi.targetBuildCell = buildingPlacementQuery.result_cells[UnityEngine.Random.Range(0, buildingPlacementQuery.result_cells.Count)];
		}
	}

	// Token: 0x040001ED RID: 493
	public GameStateMachine<BeeMakeHiveStates, BeeMakeHiveStates.Instance, IStateMachineTarget, BeeMakeHiveStates.Def>.State findBuildLocation;

	// Token: 0x040001EE RID: 494
	public GameStateMachine<BeeMakeHiveStates, BeeMakeHiveStates.Instance, IStateMachineTarget, BeeMakeHiveStates.Def>.State moveToBuildLocation;

	// Token: 0x040001EF RID: 495
	public GameStateMachine<BeeMakeHiveStates, BeeMakeHiveStates.Instance, IStateMachineTarget, BeeMakeHiveStates.Def>.State doBuild;

	// Token: 0x040001F0 RID: 496
	public GameStateMachine<BeeMakeHiveStates, BeeMakeHiveStates.Instance, IStateMachineTarget, BeeMakeHiveStates.Def>.State behaviourcomplete;

	// Token: 0x02000E10 RID: 3600
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E11 RID: 3601
	public new class Instance : GameStateMachine<BeeMakeHiveStates, BeeMakeHiveStates.Instance, IStateMachineTarget, BeeMakeHiveStates.Def>.GameInstance
	{
		// Token: 0x06006B75 RID: 27509 RVA: 0x0029685E File Offset: 0x00294A5E
		public Instance(Chore<BeeMakeHiveStates.Instance> chore, BeeMakeHiveStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToMakeHome);
		}

		// Token: 0x06006B76 RID: 27510 RVA: 0x00296884 File Offset: 0x00294A84
		public void BuildHome()
		{
			Vector3 vector = Grid.CellToPos(this.targetBuildCell, CellAlignment.Bottom, Grid.SceneLayer.Creatures);
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("BeeHive".ToTag()), vector, Quaternion.identity, null, null, true, 0);
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			component.ElementID = SimHashes.Creature;
			component.Temperature = base.gameObject.GetComponent<PrimaryElement>().Temperature;
			gameObject.SetActive(true);
			gameObject.GetSMI<BeeHive.StatesInstance>().SetUpNewHive();
		}

		// Token: 0x040050CE RID: 20686
		public int targetBuildCell;

		// Token: 0x040050CF RID: 20687
		public bool builtHome;
	}
}
