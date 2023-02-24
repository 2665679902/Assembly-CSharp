using System;
using System.Collections.Generic;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x0200068D RID: 1677
public class ClusterFogOfWarManager : GameStateMachine<ClusterFogOfWarManager, ClusterFogOfWarManager.Instance, IStateMachineTarget, ClusterFogOfWarManager.Def>
{
	// Token: 0x06002D66 RID: 11622 RVA: 0x000EE4AC File Offset: 0x000EC6AC
	public override void InitializeStates(out StateMachine.BaseState defaultState)
	{
		defaultState = this.root;
		this.root.Enter(delegate(ClusterFogOfWarManager.Instance smi)
		{
			smi.Initialize();
		}).EventHandler(GameHashes.DiscoveredWorldsChanged, (ClusterFogOfWarManager.Instance smi) => Game.Instance, delegate(ClusterFogOfWarManager.Instance smi)
		{
			smi.UpdateRevealedCellsFromDiscoveredWorlds();
		});
	}

	// Token: 0x04001AF8 RID: 6904
	public const int AUTOMATIC_PEEK_RADIUS = 2;

	// Token: 0x02001357 RID: 4951
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001358 RID: 4952
	public new class Instance : GameStateMachine<ClusterFogOfWarManager, ClusterFogOfWarManager.Instance, IStateMachineTarget, ClusterFogOfWarManager.Def>.GameInstance
	{
		// Token: 0x06007D84 RID: 32132 RVA: 0x002D4401 File Offset: 0x002D2601
		public Instance(IStateMachineTarget master, ClusterFogOfWarManager.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007D85 RID: 32133 RVA: 0x002D4416 File Offset: 0x002D2616
		public void Initialize()
		{
			this.UpdateRevealedCellsFromDiscoveredWorlds();
			this.EnsureRevealedTilesHavePeek();
		}

		// Token: 0x06007D86 RID: 32134 RVA: 0x002D4424 File Offset: 0x002D2624
		public ClusterRevealLevel GetCellRevealLevel(AxialI location)
		{
			if (this.GetRevealCompleteFraction(location) >= 1f)
			{
				return ClusterRevealLevel.Visible;
			}
			if (this.GetRevealCompleteFraction(location) > 0f)
			{
				return ClusterRevealLevel.Peeked;
			}
			return ClusterRevealLevel.Hidden;
		}

		// Token: 0x06007D87 RID: 32135 RVA: 0x002D4447 File Offset: 0x002D2647
		public void DEBUG_REVEAL_ENTIRE_MAP()
		{
			this.RevealLocation(AxialI.ZERO, 100);
		}

		// Token: 0x06007D88 RID: 32136 RVA: 0x002D4456 File Offset: 0x002D2656
		public bool IsLocationRevealed(AxialI location)
		{
			return this.GetRevealCompleteFraction(location) >= 1f;
		}

		// Token: 0x06007D89 RID: 32137 RVA: 0x002D446C File Offset: 0x002D266C
		private void EnsureRevealedTilesHavePeek()
		{
			foreach (KeyValuePair<AxialI, List<ClusterGridEntity>> keyValuePair in ClusterGrid.Instance.cellContents)
			{
				if (this.IsLocationRevealed(keyValuePair.Key))
				{
					this.PeekLocation(keyValuePair.Key, 2);
				}
			}
		}

		// Token: 0x06007D8A RID: 32138 RVA: 0x002D44DC File Offset: 0x002D26DC
		public void PeekLocation(AxialI location, int radius)
		{
			foreach (AxialI axialI in AxialUtil.GetAllPointsWithinRadius(location, radius))
			{
				if (this.m_revealPointsByCell.ContainsKey(axialI))
				{
					this.m_revealPointsByCell[axialI] = Mathf.Max(this.m_revealPointsByCell[axialI], 0.01f);
				}
				else
				{
					this.m_revealPointsByCell[axialI] = 0.01f;
				}
			}
		}

		// Token: 0x06007D8B RID: 32139 RVA: 0x002D456C File Offset: 0x002D276C
		public void RevealLocation(AxialI location, int radius = 0)
		{
			if (ClusterGrid.Instance.GetHiddenEntitiesOfLayerAtCell(location, EntityLayer.Asteroid).Count > 0 || ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(location, EntityLayer.Asteroid) != null)
			{
				radius = Mathf.Max(radius, 1);
			}
			bool flag = false;
			foreach (AxialI axialI in AxialUtil.GetAllPointsWithinRadius(location, radius))
			{
				flag |= this.RevealCellIfValid(axialI);
			}
			if (flag)
			{
				Game.Instance.Trigger(-1991583975, location);
			}
		}

		// Token: 0x06007D8C RID: 32140 RVA: 0x002D4610 File Offset: 0x002D2810
		public void EarnRevealPointsForLocation(AxialI location, float points)
		{
			global::Debug.Assert(ClusterGrid.Instance.IsValidCell(location), string.Format("EarnRevealPointsForLocation called with invalid location: {0}", location));
			if (this.IsLocationRevealed(location))
			{
				return;
			}
			if (this.m_revealPointsByCell.ContainsKey(location))
			{
				Dictionary<AxialI, float> revealPointsByCell = this.m_revealPointsByCell;
				revealPointsByCell[location] += points;
			}
			else
			{
				this.m_revealPointsByCell[location] = points;
				Game.Instance.Trigger(-1554423969, location);
			}
			if (this.IsLocationRevealed(location))
			{
				this.RevealLocation(location, 0);
				this.PeekLocation(location, 2);
				Game.Instance.Trigger(-1991583975, location);
			}
		}

		// Token: 0x06007D8D RID: 32141 RVA: 0x002D46C0 File Offset: 0x002D28C0
		public float GetRevealCompleteFraction(AxialI location)
		{
			if (!ClusterGrid.Instance.IsValidCell(location))
			{
				global::Debug.LogError(string.Format("GetRevealCompleteFraction called with invalid location: {0}, {1}", location.r, location.q));
			}
			if (DebugHandler.RevealFogOfWar)
			{
				return 1f;
			}
			float num;
			if (this.m_revealPointsByCell.TryGetValue(location, out num))
			{
				return Mathf.Min(num / ROCKETRY.CLUSTER_FOW.POINTS_TO_REVEAL, 1f);
			}
			return 0f;
		}

		// Token: 0x06007D8E RID: 32142 RVA: 0x002D4733 File Offset: 0x002D2933
		private bool RevealCellIfValid(AxialI cell)
		{
			if (!ClusterGrid.Instance.IsValidCell(cell))
			{
				return false;
			}
			if (this.IsLocationRevealed(cell))
			{
				return false;
			}
			this.m_revealPointsByCell[cell] = ROCKETRY.CLUSTER_FOW.POINTS_TO_REVEAL;
			this.PeekLocation(cell, 2);
			return true;
		}

		// Token: 0x06007D8F RID: 32143 RVA: 0x002D476C File Offset: 0x002D296C
		public bool GetUnrevealedLocationWithinRadius(AxialI center, int radius, out AxialI result)
		{
			for (int i = 0; i <= radius; i++)
			{
				foreach (AxialI axialI in AxialUtil.GetRing(center, i))
				{
					if (ClusterGrid.Instance.IsValidCell(axialI) && !this.IsLocationRevealed(axialI))
					{
						result = axialI;
						return true;
					}
				}
			}
			result = AxialI.ZERO;
			return false;
		}

		// Token: 0x06007D90 RID: 32144 RVA: 0x002D47F4 File Offset: 0x002D29F4
		public void UpdateRevealedCellsFromDiscoveredWorlds()
		{
			int num = (DlcManager.IsExpansion1Active() ? 0 : 2);
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				if (worldContainer.IsDiscovered && !DebugHandler.RevealFogOfWar)
				{
					this.RevealLocation(worldContainer.GetComponent<ClusterGridEntity>().Location, num);
				}
			}
		}

		// Token: 0x04006044 RID: 24644
		[Serialize]
		private Dictionary<AxialI, float> m_revealPointsByCell = new Dictionary<AxialI, float>();
	}
}
