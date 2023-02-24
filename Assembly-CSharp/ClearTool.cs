using System;
using UnityEngine;

// Token: 0x020007C2 RID: 1986
public class ClearTool : DragTool
{
	// Token: 0x060038A8 RID: 14504 RVA: 0x0013A4BD File Offset: 0x001386BD
	public static void DestroyInstance()
	{
		ClearTool.Instance = null;
	}

	// Token: 0x060038A9 RID: 14505 RVA: 0x0013A4C5 File Offset: 0x001386C5
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ClearTool.Instance = this;
		this.interceptNumberKeysForPriority = true;
	}

	// Token: 0x060038AA RID: 14506 RVA: 0x0013A4DA File Offset: 0x001386DA
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060038AB RID: 14507 RVA: 0x0013A4E8 File Offset: 0x001386E8
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		GameObject gameObject = Grid.Objects[cell, 3];
		if (gameObject == null)
		{
			return;
		}
		ObjectLayerListItem objectLayerListItem = gameObject.GetComponent<Pickupable>().objectLayerListItem;
		while (objectLayerListItem != null)
		{
			GameObject gameObject2 = objectLayerListItem.gameObject;
			objectLayerListItem = objectLayerListItem.nextItem;
			if (!(gameObject2 == null) && !(gameObject2.GetComponent<MinionIdentity>() != null) && gameObject2.GetComponent<Clearable>().isClearable)
			{
				gameObject2.GetComponent<Clearable>().MarkForClear(false, false);
				Prioritizable component = gameObject2.GetComponent<Prioritizable>();
				if (component != null)
				{
					component.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
				}
			}
		}
	}

	// Token: 0x060038AC RID: 14508 RVA: 0x0013A581 File Offset: 0x00138781
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x060038AD RID: 14509 RVA: 0x0013A599 File Offset: 0x00138799
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}

	// Token: 0x040025A6 RID: 9638
	public static ClearTool Instance;
}
