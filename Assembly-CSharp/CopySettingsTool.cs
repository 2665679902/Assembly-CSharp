using System;
using UnityEngine;

// Token: 0x020007C4 RID: 1988
public class CopySettingsTool : DragTool
{
	// Token: 0x060038BB RID: 14523 RVA: 0x0013A83C File Offset: 0x00138A3C
	public static void DestroyInstance()
	{
		CopySettingsTool.Instance = null;
	}

	// Token: 0x060038BC RID: 14524 RVA: 0x0013A844 File Offset: 0x00138A44
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		CopySettingsTool.Instance = this;
	}

	// Token: 0x060038BD RID: 14525 RVA: 0x0013A852 File Offset: 0x00138A52
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060038BE RID: 14526 RVA: 0x0013A85F File Offset: 0x00138A5F
	public void SetSourceObject(GameObject sourceGameObject)
	{
		this.sourceGameObject = sourceGameObject;
	}

	// Token: 0x060038BF RID: 14527 RVA: 0x0013A868 File Offset: 0x00138A68
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		if (this.sourceGameObject == null)
		{
			return;
		}
		if (Grid.IsValidCell(cell))
		{
			CopyBuildingSettings.ApplyCopy(cell, this.sourceGameObject);
		}
	}

	// Token: 0x060038C0 RID: 14528 RVA: 0x0013A88E File Offset: 0x00138A8E
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
	}

	// Token: 0x060038C1 RID: 14529 RVA: 0x0013A896 File Offset: 0x00138A96
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		this.sourceGameObject = null;
	}

	// Token: 0x040025AC RID: 9644
	public static CopySettingsTool Instance;

	// Token: 0x040025AD RID: 9645
	public GameObject Placer;

	// Token: 0x040025AE RID: 9646
	private GameObject sourceGameObject;
}
