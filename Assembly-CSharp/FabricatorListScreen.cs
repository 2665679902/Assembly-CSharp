using System;
using System.Collections.Generic;

// Token: 0x02000A99 RID: 2713
public class FabricatorListScreen : KToggleMenu
{
	// Token: 0x06005330 RID: 21296 RVA: 0x001E3180 File Offset: 0x001E1380
	private void Refresh()
	{
		List<KToggleMenu.ToggleInfo> list = new List<KToggleMenu.ToggleInfo>();
		foreach (Fabricator fabricator in Components.Fabricators.Items)
		{
			KSelectable component = fabricator.GetComponent<KSelectable>();
			list.Add(new KToggleMenu.ToggleInfo(component.GetName(), fabricator, global::Action.NumActions));
		}
		base.Setup(list);
	}

	// Token: 0x06005331 RID: 21297 RVA: 0x001E31FC File Offset: 0x001E13FC
	protected override void OnSpawn()
	{
		base.onSelect += this.OnClickFabricator;
	}

	// Token: 0x06005332 RID: 21298 RVA: 0x001E3210 File Offset: 0x001E1410
	protected override void OnActivate()
	{
		base.OnActivate();
		this.Refresh();
	}

	// Token: 0x06005333 RID: 21299 RVA: 0x001E3220 File Offset: 0x001E1420
	private void OnClickFabricator(KToggleMenu.ToggleInfo toggle_info)
	{
		Fabricator fabricator = (Fabricator)toggle_info.userData;
		SelectTool.Instance.Select(fabricator.GetComponent<KSelectable>(), false);
	}
}
