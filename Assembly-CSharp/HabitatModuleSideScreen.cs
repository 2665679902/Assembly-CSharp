using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BB8 RID: 3000
public class HabitatModuleSideScreen : SideScreenContent
{
	// Token: 0x17000692 RID: 1682
	// (get) Token: 0x06005E4D RID: 24141 RVA: 0x002277FB File Offset: 0x002259FB
	private CraftModuleInterface craftModuleInterface
	{
		get
		{
			return this.targetCraft.GetComponent<CraftModuleInterface>();
		}
	}

	// Token: 0x06005E4E RID: 24142 RVA: 0x00227808 File Offset: 0x00225A08
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005E4F RID: 24143 RVA: 0x00227818 File Offset: 0x00225A18
	public override float GetSortKey()
	{
		return 21f;
	}

	// Token: 0x06005E50 RID: 24144 RVA: 0x0022781F File Offset: 0x00225A1F
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Clustercraft>() != null && this.GetPassengerModule(target.GetComponent<Clustercraft>()) != null;
	}

	// Token: 0x06005E51 RID: 24145 RVA: 0x00227844 File Offset: 0x00225A44
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetCraft = target.GetComponent<Clustercraft>();
		PassengerRocketModule passengerModule = this.GetPassengerModule(this.targetCraft);
		this.RefreshModulePanel(passengerModule);
	}

	// Token: 0x06005E52 RID: 24146 RVA: 0x00227878 File Offset: 0x00225A78
	private PassengerRocketModule GetPassengerModule(Clustercraft craft)
	{
		foreach (Ref<RocketModuleCluster> @ref in craft.GetComponent<CraftModuleInterface>().ClusterModules)
		{
			PassengerRocketModule component = @ref.Get().GetComponent<PassengerRocketModule>();
			if (component != null)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x06005E53 RID: 24147 RVA: 0x002278E0 File Offset: 0x00225AE0
	private void RefreshModulePanel(PassengerRocketModule module)
	{
		HierarchyReferences component = base.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("icon").sprite = Def.GetUISprite(module.gameObject, "ui", false).first;
		KButton reference = component.GetReference<KButton>("button");
		reference.ClearOnClick();
		reference.onClick += delegate
		{
			AudioMixer.instance.Start(module.interiorReverbSnapshot);
			ClusterManager.Instance.SetActiveWorld(module.GetComponent<ClustercraftExteriorDoor>().GetTargetWorld().id);
			ManagementMenu.Instance.CloseAll();
		};
		component.GetReference<LocText>("label").SetText(module.gameObject.GetProperName());
	}

	// Token: 0x04004087 RID: 16519
	private Clustercraft targetCraft;

	// Token: 0x04004088 RID: 16520
	public GameObject moduleContentContainer;

	// Token: 0x04004089 RID: 16521
	public GameObject modulePanelPrefab;
}
