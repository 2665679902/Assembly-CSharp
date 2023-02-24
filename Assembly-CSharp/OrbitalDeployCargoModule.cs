using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000954 RID: 2388
public class OrbitalDeployCargoModule : GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>
{
	// Token: 0x06004684 RID: 18052 RVA: 0x0018CEA0 File Offset: 0x0018B0A0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.grounded;
		this.root.Enter(delegate(OrbitalDeployCargoModule.StatesInstance smi)
		{
			smi.CheckIfLoaded();
		}).EventHandler(GameHashes.OnStorageChange, delegate(OrbitalDeployCargoModule.StatesInstance smi)
		{
			smi.CheckIfLoaded();
		}).EventHandler(GameHashes.ClusterDestinationReached, delegate(OrbitalDeployCargoModule.StatesInstance smi)
		{
			if (smi.AutoDeploy && smi.IsValidDropLocation())
			{
				smi.DeployCargoPods();
			}
		});
		this.grounded.DefaultState(this.grounded.loaded).TagTransition(GameTags.RocketNotOnGround, this.not_grounded, false);
		this.grounded.loading.PlayAnim((OrbitalDeployCargoModule.StatesInstance smi) => smi.GetLoadingAnimName(), KAnim.PlayMode.Once).ParamTransition<bool>(this.hasCargo, this.grounded.empty, GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.IsFalse).OnAnimQueueComplete(this.grounded.loaded);
		this.grounded.loaded.ParamTransition<bool>(this.hasCargo, this.grounded.empty, GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.IsFalse).EventTransition(GameHashes.OnStorageChange, this.grounded.loading, (OrbitalDeployCargoModule.StatesInstance smi) => smi.NeedsVisualUpdate());
		this.grounded.empty.Enter(delegate(OrbitalDeployCargoModule.StatesInstance smi)
		{
			this.numVisualCapsules.Set(0, smi, false);
		}).PlayAnim("deployed").ParamTransition<bool>(this.hasCargo, this.grounded.loaded, GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.IsTrue);
		this.not_grounded.DefaultState(this.not_grounded.loaded).TagTransition(GameTags.RocketNotOnGround, this.grounded, true);
		this.not_grounded.loaded.PlayAnim("loaded").ParamTransition<bool>(this.hasCargo, this.not_grounded.empty, GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.IsFalse).OnSignal(this.emptyCargo, this.not_grounded.emptying);
		this.not_grounded.emptying.PlayAnim("deploying").GoTo(this.not_grounded.empty);
		this.not_grounded.empty.PlayAnim("deployed").ParamTransition<bool>(this.hasCargo, this.not_grounded.loaded, GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.IsTrue);
	}

	// Token: 0x04002EA3 RID: 11939
	public StateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.BoolParameter hasCargo;

	// Token: 0x04002EA4 RID: 11940
	public StateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.Signal emptyCargo;

	// Token: 0x04002EA5 RID: 11941
	public OrbitalDeployCargoModule.GroundedStates grounded;

	// Token: 0x04002EA6 RID: 11942
	public OrbitalDeployCargoModule.NotGroundedStates not_grounded;

	// Token: 0x04002EA7 RID: 11943
	public StateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.IntParameter numVisualCapsules;

	// Token: 0x0200174B RID: 5963
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006CAA RID: 27818
		public float numCapsules;
	}

	// Token: 0x0200174C RID: 5964
	public class GroundedStates : GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.State
	{
		// Token: 0x04006CAB RID: 27819
		public GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.State loading;

		// Token: 0x04006CAC RID: 27820
		public GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.State loaded;

		// Token: 0x04006CAD RID: 27821
		public GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.State empty;
	}

	// Token: 0x0200174D RID: 5965
	public class NotGroundedStates : GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.State
	{
		// Token: 0x04006CAE RID: 27822
		public GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.State loaded;

		// Token: 0x04006CAF RID: 27823
		public GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.State emptying;

		// Token: 0x04006CB0 RID: 27824
		public GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.State empty;
	}

	// Token: 0x0200174E RID: 5966
	public class StatesInstance : GameStateMachine<OrbitalDeployCargoModule, OrbitalDeployCargoModule.StatesInstance, IStateMachineTarget, OrbitalDeployCargoModule.Def>.GameInstance, IEmptyableCargo
	{
		// Token: 0x06008A6A RID: 35434 RVA: 0x002FCB48 File Offset: 0x002FAD48
		public StatesInstance(IStateMachineTarget master, OrbitalDeployCargoModule.Def def)
			: base(master, def)
		{
			this.storage = base.GetComponent<Storage>();
			base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketStorage, new LoadingCompleteCondition(this.storage));
			base.gameObject.Subscribe(-1683615038, new Action<object>(this.SetupMeter));
		}

		// Token: 0x06008A6B RID: 35435 RVA: 0x002FCB9E File Offset: 0x002FAD9E
		private void SetupMeter(object obj)
		{
			KBatchedAnimTracker componentInChildren = base.gameObject.GetComponentInChildren<KBatchedAnimTracker>();
			componentInChildren.forceAlwaysAlive = true;
			componentInChildren.matchParentOffset = true;
		}

		// Token: 0x06008A6C RID: 35436 RVA: 0x002FCBB8 File Offset: 0x002FADB8
		protected override void OnCleanUp()
		{
			base.gameObject.Unsubscribe(-1683615038, new Action<object>(this.SetupMeter));
			base.OnCleanUp();
		}

		// Token: 0x06008A6D RID: 35437 RVA: 0x002FCBDC File Offset: 0x002FADDC
		public bool NeedsVisualUpdate()
		{
			int num = base.sm.numVisualCapsules.Get(this);
			int num2 = Mathf.FloorToInt(this.storage.MassStored() / 200f);
			if (num < num2)
			{
				base.sm.numVisualCapsules.Delta(1, this);
				return true;
			}
			return false;
		}

		// Token: 0x06008A6E RID: 35438 RVA: 0x002FCC2C File Offset: 0x002FAE2C
		public string GetLoadingAnimName()
		{
			int num = base.sm.numVisualCapsules.Get(this);
			int num2 = Mathf.RoundToInt(this.storage.capacityKg / 200f);
			if (num == num2)
			{
				return "loading6_full";
			}
			if (num == num2 - 1)
			{
				return "loading5";
			}
			if (num == num2 - 2)
			{
				return "loading4";
			}
			if (num == num2 - 3 || num > 2)
			{
				return "loading3_repeat";
			}
			if (num == 2)
			{
				return "loading2";
			}
			if (num == 1)
			{
				return "loading1";
			}
			return "deployed";
		}

		// Token: 0x06008A6F RID: 35439 RVA: 0x002FCCB0 File Offset: 0x002FAEB0
		public void DeployCargoPods()
		{
			Clustercraft component = base.master.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>();
			ClusterGridEntity orbitAsteroid = component.GetOrbitAsteroid();
			if (orbitAsteroid != null)
			{
				WorldContainer component2 = orbitAsteroid.GetComponent<WorldContainer>();
				int id = component2.id;
				Vector3 vector = new Vector3(component2.minimumBounds.x + 1f, component2.maximumBounds.y, Grid.GetLayerZ(Grid.SceneLayer.Front));
				while (this.storage.MassStored() > 0f)
				{
					GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("RailGunPayload"), vector);
					gameObject.GetComponent<Pickupable>().deleteOffGrid = false;
					float num = 0f;
					while (num < 200f && this.storage.MassStored() > 0f)
					{
						num += this.storage.Transfer(gameObject.GetComponent<Storage>(), GameTags.Stored, 200f - num, false, true);
					}
					gameObject.SetActive(true);
					gameObject.GetSMI<RailGunPayload.StatesInstance>().Travel(component.Location, component2.GetMyWorldLocation());
				}
			}
			this.CheckIfLoaded();
		}

		// Token: 0x06008A70 RID: 35440 RVA: 0x002FCDD0 File Offset: 0x002FAFD0
		public bool CheckIfLoaded()
		{
			bool flag = this.storage.MassStored() > 0f;
			if (flag != base.sm.hasCargo.Get(this))
			{
				base.sm.hasCargo.Set(flag, this, false);
			}
			return flag;
		}

		// Token: 0x06008A71 RID: 35441 RVA: 0x002FCE19 File Offset: 0x002FB019
		public bool IsValidDropLocation()
		{
			return base.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().GetOrbitAsteroid() != null;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06008A72 RID: 35442 RVA: 0x002FCE36 File Offset: 0x002FB036
		// (set) Token: 0x06008A73 RID: 35443 RVA: 0x002FCE3E File Offset: 0x002FB03E
		public bool AutoDeploy
		{
			get
			{
				return this.autoDeploy;
			}
			set
			{
				this.autoDeploy = value;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06008A74 RID: 35444 RVA: 0x002FCE47 File Offset: 0x002FB047
		public bool CanAutoDeploy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008A75 RID: 35445 RVA: 0x002FCE4A File Offset: 0x002FB04A
		public void EmptyCargo()
		{
			this.DeployCargoPods();
		}

		// Token: 0x06008A76 RID: 35446 RVA: 0x002FCE52 File Offset: 0x002FB052
		public bool CanEmptyCargo()
		{
			return base.sm.hasCargo.Get(base.smi) && this.IsValidDropLocation();
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06008A77 RID: 35447 RVA: 0x002FCE74 File Offset: 0x002FB074
		public bool ChooseDuplicant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06008A78 RID: 35448 RVA: 0x002FCE77 File Offset: 0x002FB077
		// (set) Token: 0x06008A79 RID: 35449 RVA: 0x002FCE7A File Offset: 0x002FB07A
		public MinionIdentity ChosenDuplicant
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06008A7A RID: 35450 RVA: 0x002FCE7C File Offset: 0x002FB07C
		public bool ModuleDeployed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04006CB1 RID: 27825
		private Storage storage;

		// Token: 0x04006CB2 RID: 27826
		[Serialize]
		private bool autoDeploy;
	}
}
