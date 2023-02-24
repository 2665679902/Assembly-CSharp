using System;

// Token: 0x020007E4 RID: 2020
public class UtilityBuildTool : BaseUtilityBuildTool
{
	// Token: 0x06003A3A RID: 14906 RVA: 0x00142B87 File Offset: 0x00140D87
	public static void DestroyInstance()
	{
		UtilityBuildTool.Instance = null;
	}

	// Token: 0x06003A3B RID: 14907 RVA: 0x00142B8F File Offset: 0x00140D8F
	protected override void OnPrefabInit()
	{
		UtilityBuildTool.Instance = this;
		base.OnPrefabInit();
		this.populateHitsList = true;
		this.canChangeDragAxis = false;
	}

	// Token: 0x06003A3C RID: 14908 RVA: 0x00142BAC File Offset: 0x00140DAC
	protected override void ApplyPathToConduitSystem()
	{
		if (this.path.Count < 2)
		{
			return;
		}
		for (int i = 1; i < this.path.Count; i++)
		{
			if (this.path[i - 1].valid && this.path[i].valid)
			{
				int cell = this.path[i - 1].cell;
				int cell2 = this.path[i].cell;
				UtilityConnections utilityConnections = UtilityConnectionsExtensions.DirectionFromToCell(cell, cell2);
				if (utilityConnections != (UtilityConnections)0)
				{
					UtilityConnections utilityConnections2 = utilityConnections.InverseDirection();
					string text;
					if (this.conduitMgr.CanAddConnection(utilityConnections, cell, false, out text) && this.conduitMgr.CanAddConnection(utilityConnections2, cell2, false, out text))
					{
						this.conduitMgr.AddConnection(utilityConnections, cell, false);
						this.conduitMgr.AddConnection(utilityConnections2, cell2, false);
					}
					else if (i == this.path.Count - 1 && this.lastPathHead != i)
					{
						PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Building, text, null, Grid.CellToPosCCC(cell2, (Grid.SceneLayer)0), 1.5f, false, false);
					}
				}
			}
		}
		this.lastPathHead = this.path.Count - 1;
	}

	// Token: 0x0400263B RID: 9787
	public static UtilityBuildTool Instance;

	// Token: 0x0400263C RID: 9788
	private int lastPathHead = -1;
}
