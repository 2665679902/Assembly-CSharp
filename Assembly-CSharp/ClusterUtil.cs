using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000691 RID: 1681
public static class ClusterUtil
{
	// Token: 0x06002DA1 RID: 11681 RVA: 0x000EFF71 File Offset: 0x000EE171
	public static WorldContainer GetMyWorld(this StateMachine.Instance smi)
	{
		return smi.GetComponent<StateMachineController>().GetMyWorld();
	}

	// Token: 0x06002DA2 RID: 11682 RVA: 0x000EFF7E File Offset: 0x000EE17E
	public static WorldContainer GetMyWorld(this KMonoBehaviour component)
	{
		return component.gameObject.GetMyWorld();
	}

	// Token: 0x06002DA3 RID: 11683 RVA: 0x000EFF8C File Offset: 0x000EE18C
	public static WorldContainer GetMyWorld(this GameObject gameObject)
	{
		int num = Grid.PosToCell(gameObject);
		if (Grid.IsValidCell(num) && Grid.WorldIdx[num] != ClusterManager.INVALID_WORLD_IDX)
		{
			return ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[num]);
		}
		return null;
	}

	// Token: 0x06002DA4 RID: 11684 RVA: 0x000EFFC9 File Offset: 0x000EE1C9
	public static int GetMyWorldId(this StateMachine.Instance smi)
	{
		return smi.GetComponent<StateMachineController>().GetMyWorldId();
	}

	// Token: 0x06002DA5 RID: 11685 RVA: 0x000EFFD6 File Offset: 0x000EE1D6
	public static int GetMyWorldId(this KMonoBehaviour component)
	{
		return component.gameObject.GetMyWorldId();
	}

	// Token: 0x06002DA6 RID: 11686 RVA: 0x000EFFE4 File Offset: 0x000EE1E4
	public static int GetMyWorldId(this GameObject gameObject)
	{
		int num = Grid.PosToCell(gameObject);
		if (Grid.IsValidCell(num) && Grid.WorldIdx[num] != ClusterManager.INVALID_WORLD_IDX)
		{
			return (int)Grid.WorldIdx[num];
		}
		return -1;
	}

	// Token: 0x06002DA7 RID: 11687 RVA: 0x000F0017 File Offset: 0x000EE217
	public static int GetMyParentWorldId(this StateMachine.Instance smi)
	{
		return smi.GetComponent<StateMachineController>().GetMyParentWorldId();
	}

	// Token: 0x06002DA8 RID: 11688 RVA: 0x000F0024 File Offset: 0x000EE224
	public static int GetMyParentWorldId(this KMonoBehaviour component)
	{
		return component.gameObject.GetMyParentWorldId();
	}

	// Token: 0x06002DA9 RID: 11689 RVA: 0x000F0034 File Offset: 0x000EE234
	public static int GetMyParentWorldId(this GameObject gameObject)
	{
		WorldContainer myWorld = gameObject.GetMyWorld();
		if (myWorld == null)
		{
			return gameObject.GetMyWorldId();
		}
		return myWorld.ParentWorldId;
	}

	// Token: 0x06002DAA RID: 11690 RVA: 0x000F005E File Offset: 0x000EE25E
	public static AxialI GetMyWorldLocation(this StateMachine.Instance smi)
	{
		return smi.GetComponent<StateMachineController>().GetMyWorldLocation();
	}

	// Token: 0x06002DAB RID: 11691 RVA: 0x000F006B File Offset: 0x000EE26B
	public static AxialI GetMyWorldLocation(this KMonoBehaviour component)
	{
		return component.gameObject.GetMyWorldLocation();
	}

	// Token: 0x06002DAC RID: 11692 RVA: 0x000F0078 File Offset: 0x000EE278
	public static AxialI GetMyWorldLocation(this GameObject gameObject)
	{
		ClusterGridEntity component = gameObject.GetComponent<ClusterGridEntity>();
		if (component != null)
		{
			return component.Location;
		}
		WorldContainer myWorld = gameObject.GetMyWorld();
		DebugUtil.DevAssertArgs(myWorld != null, new object[] { "GetMyWorldLocation called on object with no world", gameObject });
		return myWorld.GetComponent<ClusterGridEntity>().Location;
	}

	// Token: 0x06002DAD RID: 11693 RVA: 0x000F00CC File Offset: 0x000EE2CC
	public static bool IsMyWorld(this GameObject go, GameObject otherGo)
	{
		int num = Grid.PosToCell(go);
		int num2 = Grid.PosToCell(otherGo);
		return Grid.IsValidCell(num) && Grid.IsValidCell(num2) && Grid.WorldIdx[num] == Grid.WorldIdx[num2];
	}

	// Token: 0x06002DAE RID: 11694 RVA: 0x000F010C File Offset: 0x000EE30C
	public static bool IsMyParentWorld(this GameObject go, GameObject otherGo)
	{
		int num = Grid.PosToCell(go);
		int num2 = Grid.PosToCell(otherGo);
		if (Grid.IsValidCell(num) && Grid.IsValidCell(num2))
		{
			if (Grid.WorldIdx[num] == Grid.WorldIdx[num2])
			{
				return true;
			}
			WorldContainer world = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[num]);
			WorldContainer world2 = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[num2]);
			if (world == null)
			{
				DebugUtil.DevLogError(string.Format("{0} at {1} has a valid cell but no world", go, num));
			}
			if (world2 == null)
			{
				DebugUtil.DevLogError(string.Format("{0} at {1} has a valid cell but no world", otherGo, num2));
			}
			if (world != null && world2 != null && world.ParentWorldId == world2.ParentWorldId)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002DAF RID: 11695 RVA: 0x000F01D4 File Offset: 0x000EE3D4
	public static int GetAsteroidWorldIdAtLocation(AxialI location)
	{
		foreach (ClusterGridEntity clusterGridEntity in ClusterGrid.Instance.cellContents[location])
		{
			if (clusterGridEntity.Layer == EntityLayer.Asteroid)
			{
				WorldContainer component = clusterGridEntity.GetComponent<WorldContainer>();
				if (component != null)
				{
					return component.id;
				}
			}
		}
		return -1;
	}

	// Token: 0x06002DB0 RID: 11696 RVA: 0x000F0250 File Offset: 0x000EE450
	public static bool ActiveWorldIsRocketInterior()
	{
		return ClusterManager.Instance.activeWorld.IsModuleInterior;
	}

	// Token: 0x06002DB1 RID: 11697 RVA: 0x000F0261 File Offset: 0x000EE461
	public static bool ActiveWorldHasPrinter()
	{
		return ClusterManager.Instance.activeWorld.IsModuleInterior || Components.Telepads.GetWorldItems(ClusterManager.Instance.activeWorldId, false).Count > 0;
	}

	// Token: 0x06002DB2 RID: 11698 RVA: 0x000F0294 File Offset: 0x000EE494
	public static float GetAmountFromRelatedWorlds(WorldInventory worldInventory, Tag element)
	{
		WorldContainer worldContainer = worldInventory.WorldContainer;
		float num = 0f;
		int parentWorldId = worldContainer.ParentWorldId;
		foreach (WorldContainer worldContainer2 in ClusterManager.Instance.WorldContainers)
		{
			if (worldContainer2.ParentWorldId == parentWorldId)
			{
				num += worldContainer2.worldInventory.GetAmount(element, false);
			}
		}
		return num;
	}

	// Token: 0x06002DB3 RID: 11699 RVA: 0x000F030C File Offset: 0x000EE50C
	public static List<Pickupable> GetPickupablesFromRelatedWorlds(WorldInventory worldInventory, Tag tag)
	{
		List<Pickupable> list = new List<Pickupable>();
		int parentWorldId = worldInventory.GetComponent<WorldContainer>().ParentWorldId;
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			if (worldContainer.ParentWorldId == parentWorldId)
			{
				ICollection<Pickupable> pickupables = worldContainer.worldInventory.GetPickupables(tag, false);
				if (pickupables != null)
				{
					list.AddRange(pickupables);
				}
			}
		}
		return list;
	}

	// Token: 0x06002DB4 RID: 11700 RVA: 0x000F0390 File Offset: 0x000EE590
	public static string DebugGetMyWorldName(this GameObject gameObject)
	{
		WorldContainer myWorld = gameObject.GetMyWorld();
		if (myWorld != null)
		{
			return myWorld.worldName;
		}
		return string.Format("InvalidWorld(pos={0})", gameObject.transform.GetPosition());
	}

	// Token: 0x06002DB5 RID: 11701 RVA: 0x000F03D0 File Offset: 0x000EE5D0
	public static ClusterGridEntity ClosestVisibleAsteroidToLocation(AxialI location)
	{
		foreach (AxialI axialI in AxialUtil.SpiralOut(location, ClusterGrid.Instance.numRings))
		{
			if (ClusterGrid.Instance.IsValidCell(axialI) && ClusterGrid.Instance.IsCellVisible(axialI))
			{
				ClusterGridEntity asteroidAtCell = ClusterGrid.Instance.GetAsteroidAtCell(axialI);
				if (asteroidAtCell != null)
				{
					return asteroidAtCell;
				}
			}
		}
		return null;
	}
}
