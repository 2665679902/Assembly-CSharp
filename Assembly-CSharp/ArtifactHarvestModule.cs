using System;
using UnityEngine;

// Token: 0x02000933 RID: 2355
public class ArtifactHarvestModule : GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>
{
	// Token: 0x060044FA RID: 17658 RVA: 0x00185218 File Offset: 0x00183418
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.grounded;
		this.root.Enter(delegate(ArtifactHarvestModule.StatesInstance smi)
		{
			smi.CheckIfCanHarvest();
		});
		this.grounded.TagTransition(GameTags.RocketNotOnGround, this.not_grounded, false);
		this.not_grounded.DefaultState(this.not_grounded.not_harvesting).EventHandler(GameHashes.ClusterLocationChanged, (ArtifactHarvestModule.StatesInstance smi) => Game.Instance, delegate(ArtifactHarvestModule.StatesInstance smi)
		{
			smi.CheckIfCanHarvest();
		}).EventHandler(GameHashes.OnStorageChange, delegate(ArtifactHarvestModule.StatesInstance smi)
		{
			smi.CheckIfCanHarvest();
		})
			.TagTransition(GameTags.RocketNotOnGround, this.grounded, true);
		this.not_grounded.not_harvesting.PlayAnim("loaded").ParamTransition<bool>(this.canHarvest, this.not_grounded.harvesting, GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.IsTrue);
		this.not_grounded.harvesting.PlayAnim("deploying").Update(delegate(ArtifactHarvestModule.StatesInstance smi, float dt)
		{
			smi.HarvestFromPOI(dt);
		}, UpdateRate.SIM_4000ms, false).ParamTransition<bool>(this.canHarvest, this.not_grounded.not_harvesting, GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.IsFalse);
	}

	// Token: 0x04002E06 RID: 11782
	public StateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.BoolParameter canHarvest;

	// Token: 0x04002E07 RID: 11783
	public GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.State grounded;

	// Token: 0x04002E08 RID: 11784
	public ArtifactHarvestModule.NotGroundedStates not_grounded;

	// Token: 0x02001719 RID: 5913
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200171A RID: 5914
	public class NotGroundedStates : GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.State
	{
		// Token: 0x04006BF9 RID: 27641
		public GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.State not_harvesting;

		// Token: 0x04006BFA RID: 27642
		public GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.State harvesting;
	}

	// Token: 0x0200171B RID: 5915
	public class StatesInstance : GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.GameInstance
	{
		// Token: 0x06008994 RID: 35220 RVA: 0x002F94EA File Offset: 0x002F76EA
		public StatesInstance(IStateMachineTarget master, ArtifactHarvestModule.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008995 RID: 35221 RVA: 0x002F94F4 File Offset: 0x002F76F4
		public void HarvestFromPOI(float dt)
		{
			ClusterGridEntity poiatCurrentLocation = base.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().GetPOIAtCurrentLocation();
			if (poiatCurrentLocation.IsNullOrDestroyed())
			{
				return;
			}
			ArtifactPOIStates.Instance smi = poiatCurrentLocation.GetSMI<ArtifactPOIStates.Instance>();
			if ((poiatCurrentLocation.GetComponent<ArtifactPOIClusterGridEntity>() || poiatCurrentLocation.GetComponent<HarvestablePOIClusterGridEntity>()) && !smi.IsNullOrDestroyed())
			{
				bool flag = false;
				string artifactToHarvest = smi.GetArtifactToHarvest();
				if (artifactToHarvest != null)
				{
					GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(artifactToHarvest), base.transform.position);
					gameObject.SetActive(true);
					this.receptacle.ForceDeposit(gameObject);
					this.storage.Store(gameObject, false, false, true, false);
					smi.HarvestArtifact();
					if (smi.configuration.DestroyOnHarvest())
					{
						flag = true;
					}
					if (flag)
					{
						poiatCurrentLocation.gameObject.DeleteObject();
					}
				}
			}
		}

		// Token: 0x06008996 RID: 35222 RVA: 0x002F95BC File Offset: 0x002F77BC
		public bool CheckIfCanHarvest()
		{
			Clustercraft component = base.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>();
			if (component == null)
			{
				return false;
			}
			ClusterGridEntity poiatCurrentLocation = component.GetPOIAtCurrentLocation();
			if (poiatCurrentLocation != null && (poiatCurrentLocation.GetComponent<ArtifactPOIClusterGridEntity>() || poiatCurrentLocation.GetComponent<HarvestablePOIClusterGridEntity>()))
			{
				ArtifactPOIStates.Instance smi = poiatCurrentLocation.GetSMI<ArtifactPOIStates.Instance>();
				if (smi != null && smi.CanHarvestArtifact() && this.receptacle.Occupant == null)
				{
					base.sm.canHarvest.Set(true, this, false);
					return true;
				}
			}
			base.sm.canHarvest.Set(false, this, false);
			return false;
		}

		// Token: 0x04006BFB RID: 27643
		[MyCmpReq]
		private Storage storage;

		// Token: 0x04006BFC RID: 27644
		[MyCmpReq]
		private SingleEntityReceptacle receptacle;
	}
}
