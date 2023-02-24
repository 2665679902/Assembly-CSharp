using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B9F RID: 2975
public class ClusterGridWorldSideScreen : SideScreenContent
{
	// Token: 0x06005D9A RID: 23962 RVA: 0x00222CF8 File Offset: 0x00220EF8
	protected override void OnSpawn()
	{
		this.viewButton.onClick += this.OnClickView;
	}

	// Token: 0x06005D9B RID: 23963 RVA: 0x00222D11 File Offset: 0x00220F11
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<AsteroidGridEntity>() != null;
	}

	// Token: 0x06005D9C RID: 23964 RVA: 0x00222D20 File Offset: 0x00220F20
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetEntity = target.GetComponent<AsteroidGridEntity>();
		this.icon.sprite = Def.GetUISprite(this.targetEntity, "ui", false).first;
		WorldContainer component = this.targetEntity.GetComponent<WorldContainer>();
		bool flag = component != null && component.IsDiscovered;
		this.viewButton.isInteractable = flag;
		if (!flag)
		{
			this.viewButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.CLUSTERWORLDSIDESCREEN.VIEW_WORLD_DISABLE_TOOLTIP);
			return;
		}
		this.viewButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.CLUSTERWORLDSIDESCREEN.VIEW_WORLD_TOOLTIP);
	}

	// Token: 0x06005D9D RID: 23965 RVA: 0x00222DC4 File Offset: 0x00220FC4
	private void OnClickView()
	{
		WorldContainer component = this.targetEntity.GetComponent<WorldContainer>();
		if (!component.IsDupeVisited)
		{
			component.LookAtSurface();
		}
		ClusterManager.Instance.SetActiveWorld(component.id);
		ManagementMenu.Instance.CloseAll();
	}

	// Token: 0x04003FF5 RID: 16373
	public Image icon;

	// Token: 0x04003FF6 RID: 16374
	public KButton viewButton;

	// Token: 0x04003FF7 RID: 16375
	private AsteroidGridEntity targetEntity;
}
