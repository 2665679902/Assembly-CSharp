using System;
using UnityEngine;

// Token: 0x020007C9 RID: 1993
public class DisinfectTool : DragTool
{
	// Token: 0x060038F3 RID: 14579 RVA: 0x0013B91F File Offset: 0x00139B1F
	public static void DestroyInstance()
	{
		DisinfectTool.Instance = null;
	}

	// Token: 0x060038F4 RID: 14580 RVA: 0x0013B927 File Offset: 0x00139B27
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DisinfectTool.Instance = this;
		this.interceptNumberKeysForPriority = true;
		this.viewMode = OverlayModes.Disease.ID;
	}

	// Token: 0x060038F5 RID: 14581 RVA: 0x0013B947 File Offset: 0x00139B47
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060038F6 RID: 14582 RVA: 0x0013B954 File Offset: 0x00139B54
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		for (int i = 0; i < 44; i++)
		{
			GameObject gameObject = Grid.Objects[cell, i];
			if (gameObject != null)
			{
				Disinfectable component = gameObject.GetComponent<Disinfectable>();
				if (component != null && component.GetComponent<PrimaryElement>().DiseaseCount > 0)
				{
					component.MarkForDisinfect(false);
				}
			}
		}
	}

	// Token: 0x040025BA RID: 9658
	public static DisinfectTool Instance;
}
