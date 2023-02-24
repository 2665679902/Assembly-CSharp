using System;
using UnityEngine;

// Token: 0x020007C6 RID: 1990
public class DeconstructTool : FilteredDragTool
{
	// Token: 0x060038CF RID: 14543 RVA: 0x0013B0C9 File Offset: 0x001392C9
	public static void DestroyInstance()
	{
		DeconstructTool.Instance = null;
	}

	// Token: 0x060038D0 RID: 14544 RVA: 0x0013B0D1 File Offset: 0x001392D1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DeconstructTool.Instance = this;
	}

	// Token: 0x060038D1 RID: 14545 RVA: 0x0013B0DF File Offset: 0x001392DF
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060038D2 RID: 14546 RVA: 0x0013B0EC File Offset: 0x001392EC
	protected override string GetConfirmSound()
	{
		return "Tile_Confirm_NegativeTool";
	}

	// Token: 0x060038D3 RID: 14547 RVA: 0x0013B0F3 File Offset: 0x001392F3
	protected override string GetDragSound()
	{
		return "Tile_Drag_NegativeTool";
	}

	// Token: 0x060038D4 RID: 14548 RVA: 0x0013B0FA File Offset: 0x001392FA
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		this.DeconstructCell(cell);
	}

	// Token: 0x060038D5 RID: 14549 RVA: 0x0013B104 File Offset: 0x00139304
	public void DeconstructCell(int cell)
	{
		for (int i = 0; i < 44; i++)
		{
			GameObject gameObject = Grid.Objects[cell, i];
			if (gameObject != null)
			{
				string filterLayerFromGameObject = this.GetFilterLayerFromGameObject(gameObject);
				if (base.IsActiveLayer(filterLayerFromGameObject))
				{
					gameObject.Trigger(-790448070, null);
					Prioritizable component = gameObject.GetComponent<Prioritizable>();
					if (component != null)
					{
						component.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
					}
				}
			}
		}
	}

	// Token: 0x060038D6 RID: 14550 RVA: 0x0013B176 File Offset: 0x00139376
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x060038D7 RID: 14551 RVA: 0x0013B18E File Offset: 0x0013938E
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}

	// Token: 0x040025B1 RID: 9649
	public static DeconstructTool Instance;
}
