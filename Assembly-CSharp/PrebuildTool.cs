using System;
using UnityEngine;

// Token: 0x020007D3 RID: 2003
public class PrebuildTool : InterfaceTool
{
	// Token: 0x06003983 RID: 14723 RVA: 0x0013E495 File Offset: 0x0013C695
	public static void DestroyInstance()
	{
		PrebuildTool.Instance = null;
	}

	// Token: 0x06003984 RID: 14724 RVA: 0x0013E49D File Offset: 0x0013C69D
	protected override void OnPrefabInit()
	{
		PrebuildTool.Instance = this;
	}

	// Token: 0x06003985 RID: 14725 RVA: 0x0013E4A5 File Offset: 0x0013C6A5
	protected override void OnActivateTool()
	{
		this.viewMode = this.def.ViewMode;
		base.OnActivateTool();
	}

	// Token: 0x06003986 RID: 14726 RVA: 0x0013E4BE File Offset: 0x0013C6BE
	public void Activate(BuildingDef def, string errorMessage)
	{
		this.def = def;
		PlayerController.Instance.ActivateTool(this);
		PrebuildToolHoverTextCard component = base.GetComponent<PrebuildToolHoverTextCard>();
		component.errorMessage = errorMessage;
		component.currentDef = def;
	}

	// Token: 0x06003987 RID: 14727 RVA: 0x0013E4E5 File Offset: 0x0013C6E5
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		UISounds.PlaySound(UISounds.Sound.Negative);
		base.OnLeftClickDown(cursor_pos);
	}

	// Token: 0x04002600 RID: 9728
	public static PrebuildTool Instance;

	// Token: 0x04002601 RID: 9729
	private BuildingDef def;
}
