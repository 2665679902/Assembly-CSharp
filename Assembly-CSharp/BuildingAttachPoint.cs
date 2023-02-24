using System;
using UnityEngine;

// Token: 0x02000562 RID: 1378
[AddComponentMenu("KMonoBehaviour/scripts/BuildingAttachPoint")]
public class BuildingAttachPoint : KMonoBehaviour
{
	// Token: 0x0600213E RID: 8510 RVA: 0x000B51BE File Offset: 0x000B33BE
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.BuildingAttachPoints.Add(this);
		this.TryAttachEmptyHardpoints();
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x000B51D7 File Offset: 0x000B33D7
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06002140 RID: 8512 RVA: 0x000B51E0 File Offset: 0x000B33E0
	private void TryAttachEmptyHardpoints()
	{
		for (int i = 0; i < this.points.Length; i++)
		{
			if (!(this.points[i].attachedBuilding != null))
			{
				bool flag = false;
				int num = 0;
				while (num < Components.AttachableBuildings.Count && !flag)
				{
					if (Components.AttachableBuildings[num].attachableToTag == this.points[i].attachableType && Grid.OffsetCell(Grid.PosToCell(base.gameObject), this.points[i].position) == Grid.PosToCell(Components.AttachableBuildings[num]))
					{
						this.points[i].attachedBuilding = Components.AttachableBuildings[num];
						flag = true;
					}
					num++;
				}
			}
		}
	}

	// Token: 0x06002141 RID: 8513 RVA: 0x000B52B8 File Offset: 0x000B34B8
	public bool AcceptsAttachment(Tag type, int cell)
	{
		int num = Grid.PosToCell(base.gameObject);
		for (int i = 0; i < this.points.Length; i++)
		{
			if (Grid.OffsetCell(num, this.points[i].position) == cell && this.points[i].attachableType == type)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002142 RID: 8514 RVA: 0x000B531A File Offset: 0x000B351A
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.BuildingAttachPoints.Remove(this);
	}

	// Token: 0x04001317 RID: 4887
	public BuildingAttachPoint.HardPoint[] points = new BuildingAttachPoint.HardPoint[0];

	// Token: 0x0200118A RID: 4490
	[Serializable]
	public struct HardPoint
	{
		// Token: 0x060076EB RID: 30443 RVA: 0x002B9885 File Offset: 0x002B7A85
		public HardPoint(CellOffset position, Tag attachableType, AttachableBuilding attachedBuilding)
		{
			this.position = position;
			this.attachableType = attachableType;
			this.attachedBuilding = attachedBuilding;
		}

		// Token: 0x04005B25 RID: 23333
		public CellOffset position;

		// Token: 0x04005B26 RID: 23334
		public Tag attachableType;

		// Token: 0x04005B27 RID: 23335
		public AttachableBuilding attachedBuilding;
	}
}
