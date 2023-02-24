using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000BEA RID: 3050
public class TemporalTearSideScreen : SideScreenContent
{
	// Token: 0x170006AD RID: 1709
	// (get) Token: 0x06006016 RID: 24598 RVA: 0x00232700 File Offset: 0x00230900
	private CraftModuleInterface craftModuleInterface
	{
		get
		{
			return this.targetCraft.GetComponent<CraftModuleInterface>();
		}
	}

	// Token: 0x06006017 RID: 24599 RVA: 0x0023270D File Offset: 0x0023090D
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06006018 RID: 24600 RVA: 0x0023271D File Offset: 0x0023091D
	public override float GetSortKey()
	{
		return 21f;
	}

	// Token: 0x06006019 RID: 24601 RVA: 0x00232724 File Offset: 0x00230924
	public override bool IsValidForTarget(GameObject target)
	{
		Clustercraft component = target.GetComponent<Clustercraft>();
		TemporalTear temporalTear = ClusterManager.Instance.GetComponent<ClusterPOIManager>().GetTemporalTear();
		return component != null && temporalTear != null && temporalTear.Location == component.Location;
	}

	// Token: 0x0600601A RID: 24602 RVA: 0x00232770 File Offset: 0x00230970
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetCraft = target.GetComponent<Clustercraft>();
		KButton reference = base.GetComponent<HierarchyReferences>().GetReference<KButton>("button");
		reference.ClearOnClick();
		reference.onClick += delegate
		{
			target.GetComponent<Clustercraft>();
			ClusterManager.Instance.GetComponent<ClusterPOIManager>().GetTemporalTear().ConsumeCraft(this.targetCraft);
		};
		this.RefreshPanel(null);
	}

	// Token: 0x0600601B RID: 24603 RVA: 0x002327DC File Offset: 0x002309DC
	private void RefreshPanel(object data = null)
	{
		TemporalTear temporalTear = ClusterManager.Instance.GetComponent<ClusterPOIManager>().GetTemporalTear();
		HierarchyReferences component = base.GetComponent<HierarchyReferences>();
		bool flag = temporalTear.IsOpen();
		component.GetReference<LocText>("label").SetText(flag ? UI.UISIDESCREENS.TEMPORALTEARSIDESCREEN.BUTTON_OPEN : UI.UISIDESCREENS.TEMPORALTEARSIDESCREEN.BUTTON_CLOSED);
		component.GetReference<KButton>("button").isInteractable = flag;
	}

	// Token: 0x040041CF RID: 16847
	private Clustercraft targetCraft;
}
