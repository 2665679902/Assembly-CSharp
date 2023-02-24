using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000940 RID: 2368
public class ClustercraftExteriorDoor : KMonoBehaviour
{
	// Token: 0x060045B1 RID: 17841 RVA: 0x0018895C File Offset: 0x00186B5C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.targetWorldId < 0)
		{
			GameObject gameObject = base.GetComponent<RocketModuleCluster>().CraftInterface.gameObject;
			WorldContainer worldContainer = ClusterManager.Instance.CreateRocketInteriorWorld(gameObject, this.interiorTemplateName, delegate
			{
				this.PairWithInteriorDoor();
			});
			if (worldContainer != null)
			{
				this.targetWorldId = worldContainer.id;
			}
		}
		else
		{
			this.PairWithInteriorDoor();
		}
		base.Subscribe<ClustercraftExteriorDoor>(-1277991738, ClustercraftExteriorDoor.OnLaunchDelegate);
		base.Subscribe<ClustercraftExteriorDoor>(-887025858, ClustercraftExteriorDoor.OnLandDelegate);
	}

	// Token: 0x060045B2 RID: 17842 RVA: 0x001889E6 File Offset: 0x00186BE6
	protected override void OnCleanUp()
	{
		ClusterManager.Instance.DestoryRocketInteriorWorld(this.targetWorldId, this);
		base.OnCleanUp();
	}

	// Token: 0x060045B3 RID: 17843 RVA: 0x00188A00 File Offset: 0x00186C00
	private void PairWithInteriorDoor()
	{
		foreach (object obj in Components.ClusterCraftInteriorDoors)
		{
			ClustercraftInteriorDoor clustercraftInteriorDoor = (ClustercraftInteriorDoor)obj;
			if (clustercraftInteriorDoor.GetMyWorldId() == this.targetWorldId)
			{
				this.SetTarget(clustercraftInteriorDoor);
				break;
			}
		}
		if (this.targetDoor == null)
		{
			global::Debug.LogWarning("No ClusterCraftInteriorDoor found on world");
		}
		WorldContainer targetWorld = this.GetTargetWorld();
		int myWorldId = this.GetMyWorldId();
		if (targetWorld != null && myWorldId != -1)
		{
			targetWorld.SetParentIdx(myWorldId);
		}
		if (base.gameObject.GetComponent<KSelectable>().IsSelected)
		{
			RocketModuleSideScreen.instance.UpdateButtonStates();
		}
		base.Trigger(-1118736034, null);
		targetWorld.gameObject.Trigger(-1118736034, null);
	}

	// Token: 0x060045B4 RID: 17844 RVA: 0x00188AE0 File Offset: 0x00186CE0
	public void SetTarget(ClustercraftInteriorDoor target)
	{
		this.targetDoor = target;
		target.GetComponent<AssignmentGroupController>().SetGroupID(base.GetComponent<AssignmentGroupController>().AssignmentGroupID);
		base.GetComponent<NavTeleporter>().TwoWayTarget(target.GetComponent<NavTeleporter>());
	}

	// Token: 0x060045B5 RID: 17845 RVA: 0x00188B10 File Offset: 0x00186D10
	public bool HasTargetWorld()
	{
		return this.targetDoor != null;
	}

	// Token: 0x060045B6 RID: 17846 RVA: 0x00188B1E File Offset: 0x00186D1E
	public WorldContainer GetTargetWorld()
	{
		global::Debug.Assert(this.targetDoor != null, "Clustercraft Exterior Door has no targetDoor");
		return this.targetDoor.GetMyWorld();
	}

	// Token: 0x060045B7 RID: 17847 RVA: 0x00188B44 File Offset: 0x00186D44
	public void FerryMinion(GameObject minion)
	{
		Vector3 vector = Vector3.left * 3f;
		minion.transform.SetPosition(Grid.CellToPos(Grid.PosToCell(this.targetDoor.transform.position + vector), CellAlignment.Bottom, Grid.SceneLayer.Move));
		ClusterManager.Instance.MigrateMinion(minion.GetComponent<MinionIdentity>(), this.targetDoor.GetMyWorldId());
	}

	// Token: 0x060045B8 RID: 17848 RVA: 0x00188BAC File Offset: 0x00186DAC
	private void OnLaunch(object data)
	{
		base.GetComponent<NavTeleporter>().EnableTwoWayTarget(false);
		WorldContainer targetWorld = this.GetTargetWorld();
		if (targetWorld != null)
		{
			targetWorld.SetParentIdx(targetWorld.id);
		}
	}

	// Token: 0x060045B9 RID: 17849 RVA: 0x00188BE4 File Offset: 0x00186DE4
	private void OnLand(object data)
	{
		base.GetComponent<NavTeleporter>().EnableTwoWayTarget(true);
		WorldContainer targetWorld = this.GetTargetWorld();
		if (targetWorld != null)
		{
			int myWorldId = this.GetMyWorldId();
			targetWorld.SetParentIdx(myWorldId);
		}
	}

	// Token: 0x060045BA RID: 17850 RVA: 0x00188C1B File Offset: 0x00186E1B
	public int TargetCell()
	{
		return this.targetDoor.GetComponent<NavTeleporter>().GetCell();
	}

	// Token: 0x060045BB RID: 17851 RVA: 0x00188C2D File Offset: 0x00186E2D
	public ClustercraftInteriorDoor GetInteriorDoor()
	{
		return this.targetDoor;
	}

	// Token: 0x04002E56 RID: 11862
	public string interiorTemplateName;

	// Token: 0x04002E57 RID: 11863
	private ClustercraftInteriorDoor targetDoor;

	// Token: 0x04002E58 RID: 11864
	[Serialize]
	private int targetWorldId = -1;

	// Token: 0x04002E59 RID: 11865
	private static readonly EventSystem.IntraObjectHandler<ClustercraftExteriorDoor> OnLaunchDelegate = new EventSystem.IntraObjectHandler<ClustercraftExteriorDoor>(delegate(ClustercraftExteriorDoor component, object data)
	{
		component.OnLaunch(data);
	});

	// Token: 0x04002E5A RID: 11866
	private static readonly EventSystem.IntraObjectHandler<ClustercraftExteriorDoor> OnLandDelegate = new EventSystem.IntraObjectHandler<ClustercraftExteriorDoor>(delegate(ClustercraftExteriorDoor component, object data)
	{
		component.OnLand(data);
	});
}
