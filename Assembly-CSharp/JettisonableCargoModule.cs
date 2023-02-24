using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200094A RID: 2378
public class JettisonableCargoModule : GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>
{
	// Token: 0x06004635 RID: 17973 RVA: 0x0018B70C File Offset: 0x0018990C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.grounded;
		this.root.Enter(delegate(JettisonableCargoModule.StatesInstance smi)
		{
			smi.CheckIfLoaded();
		}).EventHandler(GameHashes.OnStorageChange, delegate(JettisonableCargoModule.StatesInstance smi)
		{
			smi.CheckIfLoaded();
		});
		this.grounded.DefaultState(this.grounded.loaded).TagTransition(GameTags.RocketNotOnGround, this.not_grounded, false);
		this.grounded.loaded.PlayAnim("loaded").ParamTransition<bool>(this.hasCargo, this.grounded.empty, GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.IsFalse);
		this.grounded.empty.PlayAnim("deployed").ParamTransition<bool>(this.hasCargo, this.grounded.loaded, GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.IsTrue);
		this.not_grounded.DefaultState(this.not_grounded.loaded).TagTransition(GameTags.RocketNotOnGround, this.grounded, true);
		this.not_grounded.loaded.PlayAnim("loaded").ParamTransition<bool>(this.hasCargo, this.not_grounded.empty, GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.IsFalse).OnSignal(this.emptyCargo, this.not_grounded.emptying);
		this.not_grounded.emptying.PlayAnim("deploying").Update(delegate(JettisonableCargoModule.StatesInstance smi, float dt)
		{
			if (smi.CheckReadyForFinalDeploy())
			{
				smi.FinalDeploy();
				smi.GoTo(smi.sm.not_grounded.empty);
			}
		}, UpdateRate.SIM_200ms, false).EventTransition(GameHashes.ClusterLocationChanged, (JettisonableCargoModule.StatesInstance smi) => Game.Instance, this.not_grounded, null)
			.Exit(delegate(JettisonableCargoModule.StatesInstance smi)
			{
				smi.CancelPendingDeploy();
			});
		this.not_grounded.empty.PlayAnim("deployed").ParamTransition<bool>(this.hasCargo, this.not_grounded.loaded, GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.IsTrue);
	}

	// Token: 0x04002E7E RID: 11902
	public StateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.BoolParameter hasCargo;

	// Token: 0x04002E7F RID: 11903
	public StateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.Signal emptyCargo;

	// Token: 0x04002E80 RID: 11904
	public JettisonableCargoModule.GroundedStates grounded;

	// Token: 0x04002E81 RID: 11905
	public JettisonableCargoModule.NotGroundedStates not_grounded;

	// Token: 0x0200173D RID: 5949
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006C76 RID: 27766
		public DefComponent<Storage> landerContainer;

		// Token: 0x04006C77 RID: 27767
		public Tag landerPrefabID;

		// Token: 0x04006C78 RID: 27768
		public Vector3 cargoDropOffset;

		// Token: 0x04006C79 RID: 27769
		public string clusterMapFXPrefabID;
	}

	// Token: 0x0200173E RID: 5950
	public class GroundedStates : GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.State
	{
		// Token: 0x04006C7A RID: 27770
		public GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.State loaded;

		// Token: 0x04006C7B RID: 27771
		public GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.State empty;
	}

	// Token: 0x0200173F RID: 5951
	public class NotGroundedStates : GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.State
	{
		// Token: 0x04006C7C RID: 27772
		public GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.State loaded;

		// Token: 0x04006C7D RID: 27773
		public GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.State emptying;

		// Token: 0x04006C7E RID: 27774
		public GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.State empty;
	}

	// Token: 0x02001740 RID: 5952
	public class StatesInstance : GameStateMachine<JettisonableCargoModule, JettisonableCargoModule.StatesInstance, IStateMachineTarget, JettisonableCargoModule.Def>.GameInstance, IEmptyableCargo
	{
		// Token: 0x06008A21 RID: 35361 RVA: 0x002FA7EF File Offset: 0x002F89EF
		public StatesInstance(IStateMachineTarget master, JettisonableCargoModule.Def def)
			: base(master, def)
		{
			this.landerContainer = def.landerContainer.Get(this);
		}

		// Token: 0x06008A22 RID: 35362 RVA: 0x002FA80C File Offset: 0x002F8A0C
		private void ChooseLanderLocation()
		{
			ClusterGridEntity stableOrbitAsteroid = base.master.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().GetStableOrbitAsteroid();
			if (stableOrbitAsteroid != null)
			{
				WorldContainer component = stableOrbitAsteroid.GetComponent<WorldContainer>();
				Placeable component2 = this.landerContainer.FindFirst(base.def.landerPrefabID).GetComponent<Placeable>();
				component2.restrictWorldId = component.id;
				component.LookAtSurface();
				ClusterManager.Instance.SetActiveWorld(component.id);
				ManagementMenu.Instance.CloseAll();
				PlaceTool.Instance.Activate(component2, new Action<Placeable, int>(this.OnLanderPlaced));
			}
		}

		// Token: 0x06008A23 RID: 35363 RVA: 0x002FA8A4 File Offset: 0x002F8AA4
		private void OnLanderPlaced(Placeable lander, int cell)
		{
			this.landerPlaced = true;
			this.landerPlacementCell = cell;
			if (lander.GetComponent<MinionStorage>() != null)
			{
				this.OpenMoveChoreForChosenDuplicant();
			}
			ManagementMenu.Instance.ToggleClusterMap();
			base.sm.emptyCargo.Trigger(base.smi);
			ClusterMapScreen.Instance.SelectEntity(base.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<ClusterGridEntity>(), true);
		}

		// Token: 0x06008A24 RID: 35364 RVA: 0x002FA910 File Offset: 0x002F8B10
		private void OpenMoveChoreForChosenDuplicant()
		{
			RocketModuleCluster component = base.master.GetComponent<RocketModuleCluster>();
			Clustercraft craft = component.CraftInterface.GetComponent<Clustercraft>();
			MinionStorage storage = this.landerContainer.FindFirst(base.def.landerPrefabID).GetComponent<MinionStorage>();
			this.EnableTeleport(true);
			this.ChosenDuplicant.GetSMI<RocketPassengerMonitor.Instance>().SetModuleDeployChore(this.landerPlacementCell, delegate(Chore obj)
			{
				Game.Instance.assignmentManager.RemoveFromWorld(this.ChosenDuplicant.assignableProxy.Get(), craft.ModuleInterface.GetInteriorWorld().id);
				craft.ModuleInterface.GetPassengerModule().RemoveRocketPassenger(this.ChosenDuplicant);
				storage.SerializeMinion(this.ChosenDuplicant.gameObject);
				this.EnableTeleport(false);
			});
		}

		// Token: 0x06008A25 RID: 35365 RVA: 0x002FA994 File Offset: 0x002F8B94
		private void EnableTeleport(bool enable)
		{
			ClustercraftExteriorDoor component = base.master.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().ModuleInterface.GetPassengerModule().GetComponent<ClustercraftExteriorDoor>();
			ClustercraftInteriorDoor interiorDoor = component.GetInteriorDoor();
			AccessControl component2 = component.GetInteriorDoor().GetComponent<AccessControl>();
			NavTeleporter component3 = base.GetComponent<NavTeleporter>();
			if (enable)
			{
				component3.SetOverrideCell(this.landerPlacementCell);
				interiorDoor.GetComponent<NavTeleporter>().SetTarget(component3);
				component3.SetTarget(interiorDoor.GetComponent<NavTeleporter>());
				using (List<MinionIdentity>.Enumerator enumerator = Components.MinionIdentities.GetWorldItems(interiorDoor.GetMyWorldId(), false).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MinionIdentity minionIdentity = enumerator.Current;
						component2.SetPermission(minionIdentity.assignableProxy.Get(), (minionIdentity == this.ChosenDuplicant) ? AccessControl.Permission.Both : AccessControl.Permission.Neither);
					}
					return;
				}
			}
			component3.SetOverrideCell(-1);
			interiorDoor.GetComponent<NavTeleporter>().SetTarget(null);
			component3.SetTarget(null);
			component2.SetPermission(this.ChosenDuplicant.assignableProxy.Get(), AccessControl.Permission.Neither);
		}

		// Token: 0x06008A26 RID: 35366 RVA: 0x002FAAAC File Offset: 0x002F8CAC
		public void FinalDeploy()
		{
			this.landerPlaced = false;
			Placeable component = this.landerContainer.FindFirst(base.def.landerPrefabID).GetComponent<Placeable>();
			this.landerContainer.FindFirst(base.def.landerPrefabID);
			this.landerContainer.Drop(component.gameObject, true);
			TreeFilterable component2 = base.GetComponent<TreeFilterable>();
			TreeFilterable component3 = component.GetComponent<TreeFilterable>();
			if (component3 != null)
			{
				component3.UpdateFilters(component2.AcceptedTags);
			}
			Storage component4 = component.GetComponent<Storage>();
			if (component4 != null)
			{
				Storage[] components = base.gameObject.GetComponents<Storage>();
				for (int i = 0; i < components.Length; i++)
				{
					components[i].Transfer(component4, false, true);
				}
			}
			Vector3 vector = Grid.CellToPosCBC(this.landerPlacementCell, Grid.SceneLayer.Building);
			component.transform.SetPosition(vector);
			component.gameObject.SetActive(true);
			base.master.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().gameObject.Trigger(1792516731, component);
			component.Trigger(1792516731, base.gameObject);
			GameObject gameObject = Assets.TryGetPrefab(base.smi.def.clusterMapFXPrefabID);
			if (gameObject != null)
			{
				this.clusterMapFX = GameUtil.KInstantiate(gameObject, Grid.SceneLayer.Background, null, 0);
				this.clusterMapFX.SetActive(true);
				this.clusterMapFX.GetComponent<ClusterFXEntity>().Init(component.GetMyWorldLocation(), Vector3.zero);
				component.Subscribe(1969584890, delegate(object data)
				{
					if (!this.clusterMapFX.IsNullOrDestroyed())
					{
						Util.KDestroyGameObject(this.clusterMapFX);
					}
				});
				component.Subscribe(1591811118, delegate(object data)
				{
					if (!this.clusterMapFX.IsNullOrDestroyed())
					{
						Util.KDestroyGameObject(this.clusterMapFX);
					}
				});
			}
		}

		// Token: 0x06008A27 RID: 35367 RVA: 0x002FAC54 File Offset: 0x002F8E54
		public bool CheckReadyForFinalDeploy()
		{
			MinionStorage component = this.landerContainer.FindFirst(base.def.landerPrefabID).GetComponent<MinionStorage>();
			return !(component != null) || component.GetStoredMinionInfo().Count > 0;
		}

		// Token: 0x06008A28 RID: 35368 RVA: 0x002FAC96 File Offset: 0x002F8E96
		public void CancelPendingDeploy()
		{
			this.landerPlaced = false;
			if (this.ChosenDuplicant != null && this.CheckIfLoaded())
			{
				this.ChosenDuplicant.GetSMI<RocketPassengerMonitor.Instance>().CancelModuleDeployChore();
			}
		}

		// Token: 0x06008A29 RID: 35369 RVA: 0x002FACC8 File Offset: 0x002F8EC8
		public bool CheckIfLoaded()
		{
			bool flag = false;
			using (List<GameObject>.Enumerator enumerator = this.landerContainer.items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.PrefabID() == base.def.landerPrefabID)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag != base.sm.hasCargo.Get(this))
			{
				base.sm.hasCargo.Set(flag, this, false);
			}
			return flag;
		}

		// Token: 0x06008A2A RID: 35370 RVA: 0x002FAD60 File Offset: 0x002F8F60
		public bool IsValidDropLocation()
		{
			return base.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().GetStableOrbitAsteroid() != null;
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06008A2B RID: 35371 RVA: 0x002FAD7D File Offset: 0x002F8F7D
		// (set) Token: 0x06008A2C RID: 35372 RVA: 0x002FAD85 File Offset: 0x002F8F85
		public bool AutoDeploy { get; set; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06008A2D RID: 35373 RVA: 0x002FAD8E File Offset: 0x002F8F8E
		public bool CanAutoDeploy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008A2E RID: 35374 RVA: 0x002FAD91 File Offset: 0x002F8F91
		public void EmptyCargo()
		{
			this.ChooseLanderLocation();
		}

		// Token: 0x06008A2F RID: 35375 RVA: 0x002FAD9C File Offset: 0x002F8F9C
		public bool CanEmptyCargo()
		{
			return base.sm.hasCargo.Get(base.smi) && this.IsValidDropLocation() && (!this.ChooseDuplicant || this.ChosenDuplicant != null) && !this.landerPlaced;
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06008A30 RID: 35376 RVA: 0x002FADEC File Offset: 0x002F8FEC
		public bool ChooseDuplicant
		{
			get
			{
				GameObject gameObject = this.landerContainer.FindFirst(base.def.landerPrefabID);
				return !(gameObject == null) && gameObject.GetComponent<MinionStorage>() != null;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06008A31 RID: 35377 RVA: 0x002FAE27 File Offset: 0x002F9027
		public bool ModuleDeployed
		{
			get
			{
				return this.landerPlaced;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06008A32 RID: 35378 RVA: 0x002FAE2F File Offset: 0x002F902F
		// (set) Token: 0x06008A33 RID: 35379 RVA: 0x002FAE37 File Offset: 0x002F9037
		public MinionIdentity ChosenDuplicant
		{
			get
			{
				return this.chosenDuplicant;
			}
			set
			{
				this.chosenDuplicant = value;
			}
		}

		// Token: 0x04006C7F RID: 27775
		private Storage landerContainer;

		// Token: 0x04006C80 RID: 27776
		private bool landerPlaced;

		// Token: 0x04006C81 RID: 27777
		private MinionIdentity chosenDuplicant;

		// Token: 0x04006C82 RID: 27778
		private int landerPlacementCell;

		// Token: 0x04006C83 RID: 27779
		public GameObject clusterMapFX;
	}
}
