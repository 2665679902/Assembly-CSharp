using System;

// Token: 0x0200063D RID: 1597
public class ScannerModule : GameStateMachine<ScannerModule, ScannerModule.Instance, IStateMachineTarget, ScannerModule.Def>
{
	// Token: 0x06002A43 RID: 10819 RVA: 0x000DF498 File Offset: 0x000DD698
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Enter(delegate(ScannerModule.Instance smi)
		{
			smi.SetFogOfWarAllowed();
		}).EventHandler(GameHashes.RocketLaunched, delegate(ScannerModule.Instance smi)
		{
			smi.Scan();
		}).EventHandler(GameHashes.ClusterLocationChanged, (ScannerModule.Instance smi) => smi.GetComponent<RocketModuleCluster>().CraftInterface, delegate(ScannerModule.Instance smi)
		{
			smi.Scan();
		})
			.EventHandler(GameHashes.RocketModuleChanged, (ScannerModule.Instance smi) => smi.GetComponent<RocketModuleCluster>().CraftInterface, delegate(ScannerModule.Instance smi)
			{
				smi.SetFogOfWarAllowed();
			})
			.Exit(delegate(ScannerModule.Instance smi)
			{
				smi.SetFogOfWarAllowed();
			});
	}

	// Token: 0x020012D5 RID: 4821
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005ED6 RID: 24278
		public int scanRadius = 1;
	}

	// Token: 0x020012D6 RID: 4822
	public new class Instance : GameStateMachine<ScannerModule, ScannerModule.Instance, IStateMachineTarget, ScannerModule.Def>.GameInstance
	{
		// Token: 0x06007BBF RID: 31679 RVA: 0x002CCEA1 File Offset: 0x002CB0A1
		public Instance(IStateMachineTarget master, ScannerModule.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007BC0 RID: 31680 RVA: 0x002CCEAC File Offset: 0x002CB0AC
		public void Scan()
		{
			Clustercraft component = base.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>();
			if (component.Status == Clustercraft.CraftStatus.InFlight)
			{
				ClusterFogOfWarManager.Instance smi = SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>();
				AxialI location = component.Location;
				smi.RevealLocation(location, base.def.scanRadius);
				foreach (ClusterGridEntity clusterGridEntity in ClusterGrid.Instance.GetNotVisibleEntitiesAtAdjacentCell(location))
				{
					smi.RevealLocation(clusterGridEntity.Location, 0);
				}
			}
		}

		// Token: 0x06007BC1 RID: 31681 RVA: 0x002CCF4C File Offset: 0x002CB14C
		public void SetFogOfWarAllowed()
		{
			CraftModuleInterface craftInterface = base.GetComponent<RocketModuleCluster>().CraftInterface;
			if (craftInterface.HasClusterDestinationSelector())
			{
				bool flag = false;
				ClusterDestinationSelector clusterDestinationSelector = craftInterface.GetClusterDestinationSelector();
				bool canNavigateFogOfWar = clusterDestinationSelector.canNavigateFogOfWar;
				foreach (Ref<RocketModuleCluster> @ref in craftInterface.ClusterModules)
				{
					RocketModuleCluster rocketModuleCluster = @ref.Get();
					if (((rocketModuleCluster != null) ? rocketModuleCluster.GetSMI<ScannerModule.Instance>() : null) != null)
					{
						flag = true;
						break;
					}
				}
				clusterDestinationSelector.canNavigateFogOfWar = flag;
				if (canNavigateFogOfWar && !flag)
				{
					ClusterTraveler component = craftInterface.GetComponent<ClusterTraveler>();
					if (component != null)
					{
						component.RevalidatePath(true);
					}
				}
				craftInterface.GetComponent<Clustercraft>().Trigger(-688990705, null);
			}
		}
	}
}
