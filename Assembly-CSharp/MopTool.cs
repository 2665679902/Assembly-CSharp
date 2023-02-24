using System;
using STRINGS;
using UnityEngine;

// Token: 0x020007D0 RID: 2000
public class MopTool : DragTool
{
	// Token: 0x06003963 RID: 14691 RVA: 0x0013DE5B File Offset: 0x0013C05B
	public static void DestroyInstance()
	{
		MopTool.Instance = null;
	}

	// Token: 0x06003964 RID: 14692 RVA: 0x0013DE63 File Offset: 0x0013C063
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.Placer = Assets.GetPrefab(new Tag("MopPlacer"));
		this.interceptNumberKeysForPriority = true;
		MopTool.Instance = this;
	}

	// Token: 0x06003965 RID: 14693 RVA: 0x0013DE8D File Offset: 0x0013C08D
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x06003966 RID: 14694 RVA: 0x0013DE9C File Offset: 0x0013C09C
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		if (Grid.IsValidCell(cell))
		{
			if (DebugHandler.InstantBuildMode)
			{
				if (Grid.IsValidCell(cell))
				{
					Moppable.MopCell(cell, 1000000f, null);
					return;
				}
			}
			else
			{
				GameObject gameObject = Grid.Objects[cell, 8];
				if (!Grid.Solid[cell] && gameObject == null && Grid.Element[cell].IsLiquid)
				{
					bool flag = Grid.Solid[Grid.CellBelow(cell)];
					bool flag2 = Grid.Mass[cell] <= MopTool.maxMopAmt;
					if (flag && flag2)
					{
						gameObject = Util.KInstantiate(this.Placer, null, null);
						Grid.Objects[cell, 8] = gameObject;
						Vector3 vector = Grid.CellToPosCBC(cell, this.visualizerLayer);
						float num = -0.15f;
						vector.z += num;
						gameObject.transform.SetPosition(vector);
						gameObject.SetActive(true);
					}
					else
					{
						string text = UI.TOOLS.MOP.TOO_MUCH_LIQUID;
						if (!flag)
						{
							text = UI.TOOLS.MOP.NOT_ON_FLOOR;
						}
						PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, text, null, Grid.CellToPosCBC(cell, this.visualizerLayer), 1.5f, false, false);
					}
				}
				if (gameObject != null)
				{
					Prioritizable component = gameObject.GetComponent<Prioritizable>();
					if (component != null)
					{
						component.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
					}
				}
			}
		}
	}

	// Token: 0x06003967 RID: 14695 RVA: 0x0013E005 File Offset: 0x0013C205
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x06003968 RID: 14696 RVA: 0x0013E01D File Offset: 0x0013C21D
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}

	// Token: 0x040025F4 RID: 9716
	private GameObject Placer;

	// Token: 0x040025F5 RID: 9717
	public static MopTool Instance;

	// Token: 0x040025F6 RID: 9718
	private SimHashes Element;

	// Token: 0x040025F7 RID: 9719
	public static float maxMopAmt = 150f;
}
