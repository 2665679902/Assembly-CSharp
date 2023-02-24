using System;
using UnityEngine;

// Token: 0x020007C7 RID: 1991
public class DigTool : DragTool
{
	// Token: 0x060038D9 RID: 14553 RVA: 0x0013B1AF File Offset: 0x001393AF
	public static void DestroyInstance()
	{
		DigTool.Instance = null;
	}

	// Token: 0x060038DA RID: 14554 RVA: 0x0013B1B7 File Offset: 0x001393B7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DigTool.Instance = this;
	}

	// Token: 0x060038DB RID: 14555 RVA: 0x0013B1C5 File Offset: 0x001393C5
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		InterfaceTool.ActiveConfig.DigAction.Uproot(cell);
		InterfaceTool.ActiveConfig.DigAction.Dig(cell, distFromOrigin);
	}

	// Token: 0x060038DC RID: 14556 RVA: 0x0013B1E8 File Offset: 0x001393E8
	public static GameObject PlaceDig(int cell, int animationDelay = 0)
	{
		if (Grid.Solid[cell] && !Grid.Foundation[cell] && Grid.Objects[cell, 7] == null)
		{
			for (int i = 0; i < 44; i++)
			{
				if (Grid.Objects[cell, i] != null && Grid.Objects[cell, i].GetComponent<Constructable>() != null)
				{
					return null;
				}
			}
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(new Tag("DigPlacer")), null, null);
			gameObject.SetActive(true);
			Grid.Objects[cell, 7] = gameObject;
			Vector3 vector = Grid.CellToPosCBC(cell, DigTool.Instance.visualizerLayer);
			float num = -0.15f;
			vector.z += num;
			gameObject.transform.SetPosition(vector);
			gameObject.GetComponentInChildren<EasingAnimations>().PlayAnimation("ScaleUp", Mathf.Max(0f, (float)animationDelay * 0.02f));
			return gameObject;
		}
		if (Grid.Objects[cell, 7] != null)
		{
			return Grid.Objects[cell, 7];
		}
		return null;
	}

	// Token: 0x060038DD RID: 14557 RVA: 0x0013B30C File Offset: 0x0013950C
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x060038DE RID: 14558 RVA: 0x0013B324 File Offset: 0x00139524
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}

	// Token: 0x040025B2 RID: 9650
	public static DigTool Instance;
}
