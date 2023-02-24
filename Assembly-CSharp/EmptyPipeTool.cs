using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007CB RID: 1995
public class EmptyPipeTool : FilteredDragTool
{
	// Token: 0x06003918 RID: 14616 RVA: 0x0013C668 File Offset: 0x0013A868
	public static void DestroyInstance()
	{
		EmptyPipeTool.Instance = null;
	}

	// Token: 0x06003919 RID: 14617 RVA: 0x0013C670 File Offset: 0x0013A870
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		EmptyPipeTool.Instance = this;
	}

	// Token: 0x0600391A RID: 14618 RVA: 0x0013C680 File Offset: 0x0013A880
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		for (int i = 0; i < 44; i++)
		{
			if (base.IsActiveLayer((ObjectLayer)i))
			{
				GameObject gameObject = Grid.Objects[cell, i];
				if (!(gameObject == null))
				{
					IEmptyConduitWorkable component = gameObject.GetComponent<IEmptyConduitWorkable>();
					if (!component.IsNullOrDestroyed())
					{
						if (DebugHandler.InstantBuildMode)
						{
							component.EmptyContents();
						}
						else
						{
							component.MarkForEmptying();
							Prioritizable component2 = gameObject.GetComponent<Prioritizable>();
							if (component2 != null)
							{
								component2.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600391B RID: 14619 RVA: 0x0013C702 File Offset: 0x0013A902
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x0600391C RID: 14620 RVA: 0x0013C71A File Offset: 0x0013A91A
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}

	// Token: 0x0600391D RID: 14621 RVA: 0x0013C733 File Offset: 0x0013A933
	protected override void GetDefaultFilters(Dictionary<string, ToolParameterMenu.ToggleState> filters)
	{
		filters.Add(ToolParameterMenu.FILTERLAYERS.ALL, ToolParameterMenu.ToggleState.On);
		filters.Add(ToolParameterMenu.FILTERLAYERS.LIQUIDCONDUIT, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.GASCONDUIT, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.SOLIDCONDUIT, ToolParameterMenu.ToggleState.Off);
	}

	// Token: 0x040025CB RID: 9675
	public static EmptyPipeTool Instance;
}
