using System;

// Token: 0x020007E5 RID: 2021
public class WireBuildTool : BaseUtilityBuildTool
{
	// Token: 0x06003A3E RID: 14910 RVA: 0x00142CF6 File Offset: 0x00140EF6
	public static void DestroyInstance()
	{
		WireBuildTool.Instance = null;
	}

	// Token: 0x06003A3F RID: 14911 RVA: 0x00142CFE File Offset: 0x00140EFE
	protected override void OnPrefabInit()
	{
		WireBuildTool.Instance = this;
		base.OnPrefabInit();
		this.viewMode = OverlayModes.Power.ID;
	}

	// Token: 0x06003A40 RID: 14912 RVA: 0x00142D18 File Offset: 0x00140F18
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
				UtilityConnections utilityConnections = UtilityConnectionsExtensions.DirectionFromToCell(cell, this.path[i].cell);
				if (utilityConnections != (UtilityConnections)0)
				{
					UtilityConnections utilityConnections2 = utilityConnections.InverseDirection();
					this.conduitMgr.AddConnection(utilityConnections, cell, false);
					this.conduitMgr.AddConnection(utilityConnections2, cell2, false);
				}
			}
		}
	}

	// Token: 0x0400263D RID: 9789
	public static WireBuildTool Instance;
}
