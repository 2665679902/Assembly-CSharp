using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000558 RID: 1368
[AddComponentMenu("KMonoBehaviour/scripts/AttachableBuilding")]
public class AttachableBuilding : KMonoBehaviour
{
	// Token: 0x060020EB RID: 8427 RVA: 0x000B34F8 File Offset: 0x000B16F8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.RegisterWithAttachPoint(true);
		Components.AttachableBuildings.Add(this);
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this))
		{
			AttachableBuilding component = gameObject.GetComponent<AttachableBuilding>();
			if (component != null && component.onAttachmentNetworkChanged != null)
			{
				component.onAttachmentNetworkChanged(this);
			}
		}
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x000B3580 File Offset: 0x000B1780
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060020ED RID: 8429 RVA: 0x000B3588 File Offset: 0x000B1788
	public void RegisterWithAttachPoint(bool register)
	{
		BuildingDef buildingDef = null;
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		BuildingUnderConstruction component2 = base.GetComponent<BuildingUnderConstruction>();
		if (component != null)
		{
			buildingDef = component.Def;
		}
		else if (component2 != null)
		{
			buildingDef = component2.Def;
		}
		int num = Grid.OffsetCell(Grid.PosToCell(base.gameObject), buildingDef.attachablePosition);
		bool flag = false;
		int num2 = 0;
		while (!flag && num2 < Components.BuildingAttachPoints.Count)
		{
			for (int i = 0; i < Components.BuildingAttachPoints[num2].points.Length; i++)
			{
				if (num == Grid.OffsetCell(Grid.PosToCell(Components.BuildingAttachPoints[num2]), Components.BuildingAttachPoints[num2].points[i].position))
				{
					if (register)
					{
						Components.BuildingAttachPoints[num2].points[i].attachedBuilding = this;
					}
					else if (Components.BuildingAttachPoints[num2].points[i].attachedBuilding == this)
					{
						Components.BuildingAttachPoints[num2].points[i].attachedBuilding = null;
					}
					flag = true;
					break;
				}
			}
			num2++;
		}
	}

	// Token: 0x060020EE RID: 8430 RVA: 0x000B36D0 File Offset: 0x000B18D0
	public static void GetAttachedBelow(AttachableBuilding searchStart, ref List<GameObject> buildings)
	{
		AttachableBuilding attachableBuilding = searchStart;
		while (attachableBuilding != null)
		{
			BuildingAttachPoint attachedTo = attachableBuilding.GetAttachedTo();
			attachableBuilding = null;
			if (attachedTo != null)
			{
				buildings.Add(attachedTo.gameObject);
				attachableBuilding = attachedTo.GetComponent<AttachableBuilding>();
			}
		}
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x000B3710 File Offset: 0x000B1910
	public static int CountAttachedBelow(AttachableBuilding searchStart)
	{
		int num = 0;
		AttachableBuilding attachableBuilding = searchStart;
		while (attachableBuilding != null)
		{
			BuildingAttachPoint attachedTo = attachableBuilding.GetAttachedTo();
			attachableBuilding = null;
			if (attachedTo != null)
			{
				num++;
				attachableBuilding = attachedTo.GetComponent<AttachableBuilding>();
			}
		}
		return num;
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x000B374C File Offset: 0x000B194C
	public static void GetAttachedAbove(AttachableBuilding searchStart, ref List<GameObject> buildings)
	{
		BuildingAttachPoint buildingAttachPoint = searchStart.GetComponent<BuildingAttachPoint>();
		while (buildingAttachPoint != null)
		{
			bool flag = false;
			foreach (BuildingAttachPoint.HardPoint hardPoint in buildingAttachPoint.points)
			{
				if (flag)
				{
					break;
				}
				if (hardPoint.attachedBuilding != null)
				{
					foreach (object obj in Components.AttachableBuildings)
					{
						AttachableBuilding attachableBuilding = (AttachableBuilding)obj;
						if (attachableBuilding == hardPoint.attachedBuilding)
						{
							buildings.Add(attachableBuilding.gameObject);
							buildingAttachPoint = attachableBuilding.GetComponent<BuildingAttachPoint>();
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				buildingAttachPoint = null;
			}
		}
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x000B3828 File Offset: 0x000B1A28
	public static void NotifyBuildingsNetworkChanged(List<GameObject> buildings, AttachableBuilding attachable = null)
	{
		foreach (GameObject gameObject in buildings)
		{
			AttachableBuilding component = gameObject.GetComponent<AttachableBuilding>();
			if (component != null && component.onAttachmentNetworkChanged != null)
			{
				component.onAttachmentNetworkChanged(attachable);
			}
		}
	}

	// Token: 0x060020F2 RID: 8434 RVA: 0x000B3894 File Offset: 0x000B1A94
	public static List<GameObject> GetAttachedNetwork(AttachableBuilding searchStart)
	{
		List<GameObject> list = new List<GameObject>();
		list.Add(searchStart.gameObject);
		AttachableBuilding.GetAttachedAbove(searchStart, ref list);
		AttachableBuilding.GetAttachedBelow(searchStart, ref list);
		return list;
	}

	// Token: 0x060020F3 RID: 8435 RVA: 0x000B38C4 File Offset: 0x000B1AC4
	public BuildingAttachPoint GetAttachedTo()
	{
		for (int i = 0; i < Components.BuildingAttachPoints.Count; i++)
		{
			for (int j = 0; j < Components.BuildingAttachPoints[i].points.Length; j++)
			{
				if (Components.BuildingAttachPoints[i].points[j].attachedBuilding == this && (Components.BuildingAttachPoints[i].points[j].attachedBuilding.GetComponent<Deconstructable>() == null || !Components.BuildingAttachPoints[i].points[j].attachedBuilding.GetComponent<Deconstructable>().HasBeenDestroyed))
				{
					return Components.BuildingAttachPoints[i];
				}
			}
		}
		return null;
	}

	// Token: 0x060020F4 RID: 8436 RVA: 0x000B398E File Offset: 0x000B1B8E
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		AttachableBuilding.NotifyBuildingsNetworkChanged(AttachableBuilding.GetAttachedNetwork(this), this);
		this.RegisterWithAttachPoint(false);
		Components.AttachableBuildings.Remove(this);
	}

	// Token: 0x040012F1 RID: 4849
	public Tag attachableToTag;

	// Token: 0x040012F2 RID: 4850
	public Action<object> onAttachmentNetworkChanged;
}
