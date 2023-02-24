using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008F3 RID: 2291
public class Room : IAssignableIdentity
{
	// Token: 0x170004B7 RID: 1207
	// (get) Token: 0x0600422C RID: 16940 RVA: 0x0017435B File Offset: 0x0017255B
	public List<KPrefabID> buildings
	{
		get
		{
			return this.cavity.buildings;
		}
	}

	// Token: 0x170004B8 RID: 1208
	// (get) Token: 0x0600422D RID: 16941 RVA: 0x00174368 File Offset: 0x00172568
	public List<KPrefabID> plants
	{
		get
		{
			return this.cavity.plants;
		}
	}

	// Token: 0x0600422E RID: 16942 RVA: 0x00174375 File Offset: 0x00172575
	public string GetProperName()
	{
		return this.roomType.Name;
	}

	// Token: 0x0600422F RID: 16943 RVA: 0x00174384 File Offset: 0x00172584
	public List<Ownables> GetOwners()
	{
		this.current_owners.Clear();
		foreach (KPrefabID kprefabID in this.GetPrimaryEntities())
		{
			if (kprefabID != null)
			{
				Ownable component = kprefabID.GetComponent<Ownable>();
				if (component != null && component.assignee != null && component.assignee != this)
				{
					foreach (Ownables ownables in component.assignee.GetOwners())
					{
						if (!this.current_owners.Contains(ownables))
						{
							this.current_owners.Add(ownables);
						}
					}
				}
			}
		}
		return this.current_owners;
	}

	// Token: 0x06004230 RID: 16944 RVA: 0x00174470 File Offset: 0x00172670
	public List<GameObject> GetBuildingsOnFloor()
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.buildings.Count; i++)
		{
			if (!Grid.Solid[Grid.PosToCell(this.buildings[i])] && Grid.Solid[Grid.CellBelow(Grid.PosToCell(this.buildings[i]))])
			{
				list.Add(this.buildings[i].gameObject);
			}
		}
		return list;
	}

	// Token: 0x06004231 RID: 16945 RVA: 0x001744F0 File Offset: 0x001726F0
	public Ownables GetSoleOwner()
	{
		List<Ownables> owners = this.GetOwners();
		if (owners.Count <= 0)
		{
			return null;
		}
		return owners[0];
	}

	// Token: 0x06004232 RID: 16946 RVA: 0x00174518 File Offset: 0x00172718
	public bool HasOwner(Assignables owner)
	{
		return this.GetOwners().Find((Ownables x) => x == owner) != null;
	}

	// Token: 0x06004233 RID: 16947 RVA: 0x0017454F File Offset: 0x0017274F
	public int NumOwners()
	{
		return this.GetOwners().Count;
	}

	// Token: 0x06004234 RID: 16948 RVA: 0x0017455C File Offset: 0x0017275C
	public List<KPrefabID> GetPrimaryEntities()
	{
		this.primary_buildings.Clear();
		RoomType roomType = this.roomType;
		if (roomType.primary_constraint != null)
		{
			foreach (KPrefabID kprefabID in this.buildings)
			{
				if (kprefabID != null && roomType.primary_constraint.building_criteria(kprefabID))
				{
					this.primary_buildings.Add(kprefabID);
				}
			}
			foreach (KPrefabID kprefabID2 in this.plants)
			{
				if (kprefabID2 != null && roomType.primary_constraint.building_criteria(kprefabID2))
				{
					this.primary_buildings.Add(kprefabID2);
				}
			}
		}
		return this.primary_buildings;
	}

	// Token: 0x06004235 RID: 16949 RVA: 0x00174658 File Offset: 0x00172858
	public void RetriggerBuildings()
	{
		foreach (KPrefabID kprefabID in this.buildings)
		{
			if (!(kprefabID == null))
			{
				kprefabID.Trigger(144050788, this);
			}
		}
		foreach (KPrefabID kprefabID2 in this.plants)
		{
			if (!(kprefabID2 == null))
			{
				kprefabID2.Trigger(144050788, this);
			}
		}
	}

	// Token: 0x06004236 RID: 16950 RVA: 0x0017470C File Offset: 0x0017290C
	public bool IsNull()
	{
		return false;
	}

	// Token: 0x06004237 RID: 16951 RVA: 0x0017470F File Offset: 0x0017290F
	public void CleanUp()
	{
		Game.Instance.assignmentManager.RemoveFromAllGroups(this);
	}

	// Token: 0x04002C04 RID: 11268
	public CavityInfo cavity;

	// Token: 0x04002C05 RID: 11269
	public RoomType roomType;

	// Token: 0x04002C06 RID: 11270
	private List<KPrefabID> primary_buildings = new List<KPrefabID>();

	// Token: 0x04002C07 RID: 11271
	private List<Ownables> current_owners = new List<Ownables>();
}
