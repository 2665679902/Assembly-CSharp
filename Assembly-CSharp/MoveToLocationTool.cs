using System;
using UnityEngine;

// Token: 0x020007D1 RID: 2001
public class MoveToLocationTool : InterfaceTool
{
	// Token: 0x0600396B RID: 14699 RVA: 0x0013E04A File Offset: 0x0013C24A
	public static void DestroyInstance()
	{
		MoveToLocationTool.Instance = null;
	}

	// Token: 0x0600396C RID: 14700 RVA: 0x0013E052 File Offset: 0x0013C252
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MoveToLocationTool.Instance = this;
		this.visualizer = Util.KInstantiate(this.visualizer, null, null);
	}

	// Token: 0x0600396D RID: 14701 RVA: 0x0013E073 File Offset: 0x0013C273
	public void Activate(Navigator navigator)
	{
		this.targetNavigator = navigator;
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x0600396E RID: 14702 RVA: 0x0013E087 File Offset: 0x0013C287
	public bool CanMoveTo(int target_cell)
	{
		return this.targetNavigator.CanReach(target_cell);
	}

	// Token: 0x0600396F RID: 14703 RVA: 0x0013E095 File Offset: 0x0013C295
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		this.visualizer.gameObject.SetActive(true);
	}

	// Token: 0x06003970 RID: 14704 RVA: 0x0013E0B0 File Offset: 0x0013C2B0
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		if (this.targetNavigator != null && new_tool == SelectTool.Instance)
		{
			SelectTool.Instance.SelectNextFrame(this.targetNavigator.GetComponent<KSelectable>(), true);
		}
		this.visualizer.gameObject.SetActive(false);
	}

	// Token: 0x06003971 RID: 14705 RVA: 0x0013E108 File Offset: 0x0013C308
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		base.OnLeftClickDown(cursor_pos);
		if (this.targetNavigator != null)
		{
			int mouseCell = DebugHandler.GetMouseCell();
			MoveToLocationMonitor.Instance smi = this.targetNavigator.GetSMI<MoveToLocationMonitor.Instance>();
			if (this.CanMoveTo(mouseCell) && smi != null)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click", false));
				smi.MoveToLocation(mouseCell);
				SelectTool.Instance.Activate();
				return;
			}
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
		}
	}

	// Token: 0x06003972 RID: 14706 RVA: 0x0013E17C File Offset: 0x0013C37C
	private void RefreshColor()
	{
		Color white = new Color(0.91f, 0.21f, 0.2f);
		if (this.CanMoveTo(DebugHandler.GetMouseCell()))
		{
			white = Color.white;
		}
		this.SetColor(this.visualizer, white);
	}

	// Token: 0x06003973 RID: 14707 RVA: 0x0013E1BF File Offset: 0x0013C3BF
	public override void OnMouseMove(Vector3 cursor_pos)
	{
		base.OnMouseMove(cursor_pos);
		this.RefreshColor();
	}

	// Token: 0x06003974 RID: 14708 RVA: 0x0013E1CE File Offset: 0x0013C3CE
	private void SetColor(GameObject root, Color c)
	{
		root.GetComponentInChildren<MeshRenderer>().material.color = c;
	}

	// Token: 0x040025F8 RID: 9720
	public static MoveToLocationTool Instance;

	// Token: 0x040025F9 RID: 9721
	private Navigator targetNavigator;
}
