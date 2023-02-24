using System;
using UnityEngine;

// Token: 0x020004BB RID: 1211
[AddComponentMenu("KMonoBehaviour/scripts/PumpingStationGuide")]
public class PumpingStationGuide : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x06001BF2 RID: 7154 RVA: 0x0009472C File Offset: 0x0009292C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.parentController = this.parent.GetComponent<KBatchedAnimController>();
		this.guideController = base.GetComponent<KBatchedAnimController>();
		this.RefreshTint();
		this.RefreshDepthAvailable();
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x0009475D File Offset: 0x0009295D
	private void RefreshTint()
	{
		this.guideController.TintColour = this.parentController.TintColour;
	}

	// Token: 0x06001BF4 RID: 7156 RVA: 0x00094778 File Offset: 0x00092978
	private void RefreshDepthAvailable()
	{
		int depthAvailable = PumpingStationGuide.GetDepthAvailable(Grid.PosToCell(this), this.parent);
		if (depthAvailable != this.previousDepthAvailable)
		{
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			if (depthAvailable == 0)
			{
				component.enabled = false;
			}
			else
			{
				component.enabled = true;
				component.Play(new HashedString("place_pipe" + depthAvailable.ToString()), KAnim.PlayMode.Once, 1f, 0f);
			}
			if (this.occupyTiles)
			{
				PumpingStationGuide.OccupyArea(this.parent, depthAvailable);
			}
			this.previousDepthAvailable = depthAvailable;
		}
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x000947FC File Offset: 0x000929FC
	public void RenderEveryTick(float dt)
	{
		this.RefreshTint();
		this.RefreshDepthAvailable();
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x0009480C File Offset: 0x00092A0C
	public static void OccupyArea(GameObject go, int depth_available)
	{
		int num = Grid.PosToCell(go.transform.GetPosition());
		for (int i = 1; i <= depth_available; i++)
		{
			int num2 = Grid.OffsetCell(num, 0, -i);
			int num3 = Grid.OffsetCell(num, 1, -i);
			Grid.ObjectLayers[1][num2] = go;
			Grid.ObjectLayers[1][num3] = go;
		}
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x00094868 File Offset: 0x00092A68
	public static int GetDepthAvailable(int root_cell, GameObject pump)
	{
		int num = 4;
		int num2 = 0;
		for (int i = 1; i <= num; i++)
		{
			int num3 = Grid.OffsetCell(root_cell, 0, -i);
			int num4 = Grid.OffsetCell(root_cell, 1, -i);
			if (!Grid.IsValidCell(num3) || Grid.Solid[num3] || !Grid.IsValidCell(num4) || Grid.Solid[num4] || (Grid.ObjectLayers[1].ContainsKey(num3) && !(Grid.ObjectLayers[1][num3] == null) && !(Grid.ObjectLayers[1][num3] == pump)) || (Grid.ObjectLayers[1].ContainsKey(num4) && !(Grid.ObjectLayers[1][num4] == null) && !(Grid.ObjectLayers[1][num4] == pump)))
			{
				break;
			}
			num2 = i;
		}
		return num2;
	}

	// Token: 0x04000FA0 RID: 4000
	private int previousDepthAvailable = -1;

	// Token: 0x04000FA1 RID: 4001
	public GameObject parent;

	// Token: 0x04000FA2 RID: 4002
	public bool occupyTiles;

	// Token: 0x04000FA3 RID: 4003
	private KBatchedAnimController parentController;

	// Token: 0x04000FA4 RID: 4004
	private KBatchedAnimController guideController;
}
