using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BB9 RID: 3001
public class HarvestModuleSideScreen : SideScreenContent, ISimEveryTick
{
	// Token: 0x17000693 RID: 1683
	// (get) Token: 0x06005E55 RID: 24149 RVA: 0x00227974 File Offset: 0x00225B74
	private CraftModuleInterface craftModuleInterface
	{
		get
		{
			return this.targetCraft.GetComponent<CraftModuleInterface>();
		}
	}

	// Token: 0x06005E56 RID: 24150 RVA: 0x00227981 File Offset: 0x00225B81
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005E57 RID: 24151 RVA: 0x00227991 File Offset: 0x00225B91
	public override float GetSortKey()
	{
		return 21f;
	}

	// Token: 0x06005E58 RID: 24152 RVA: 0x00227998 File Offset: 0x00225B98
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Clustercraft>() != null && this.GetResourceHarvestModule(target.GetComponent<Clustercraft>()) != null;
	}

	// Token: 0x06005E59 RID: 24153 RVA: 0x002279BC File Offset: 0x00225BBC
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetCraft = target.GetComponent<Clustercraft>();
		ResourceHarvestModule.StatesInstance resourceHarvestModule = this.GetResourceHarvestModule(this.targetCraft);
		this.RefreshModulePanel(resourceHarvestModule);
	}

	// Token: 0x06005E5A RID: 24154 RVA: 0x002279F0 File Offset: 0x00225BF0
	private ResourceHarvestModule.StatesInstance GetResourceHarvestModule(Clustercraft craft)
	{
		foreach (Ref<RocketModuleCluster> @ref in craft.GetComponent<CraftModuleInterface>().ClusterModules)
		{
			GameObject gameObject = @ref.Get().gameObject;
			if (gameObject.GetDef<ResourceHarvestModule.Def>() != null)
			{
				return gameObject.GetSMI<ResourceHarvestModule.StatesInstance>();
			}
		}
		return null;
	}

	// Token: 0x06005E5B RID: 24155 RVA: 0x00227A5C File Offset: 0x00225C5C
	private void RefreshModulePanel(StateMachine.Instance module)
	{
		HierarchyReferences component = base.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("icon").sprite = Def.GetUISprite(module.gameObject, "ui", false).first;
		component.GetReference<LocText>("label").SetText(module.gameObject.GetProperName());
	}

	// Token: 0x06005E5C RID: 24156 RVA: 0x00227AB0 File Offset: 0x00225CB0
	public void SimEveryTick(float dt)
	{
		if (this.targetCraft.IsNullOrDestroyed())
		{
			return;
		}
		HierarchyReferences component = base.GetComponent<HierarchyReferences>();
		ResourceHarvestModule.StatesInstance resourceHarvestModule = this.GetResourceHarvestModule(this.targetCraft);
		if (resourceHarvestModule == null)
		{
			return;
		}
		GenericUIProgressBar reference = component.GetReference<GenericUIProgressBar>("progressBar");
		float num = 4f;
		float num2 = resourceHarvestModule.timeinstate % num;
		if (resourceHarvestModule.sm.canHarvest.Get(resourceHarvestModule))
		{
			reference.SetFillPercentage(num2 / num);
			reference.label.SetText(UI.UISIDESCREENS.HARVESTMODULESIDESCREEN.MINING_IN_PROGRESS);
		}
		else
		{
			reference.SetFillPercentage(0f);
			reference.label.SetText(UI.UISIDESCREENS.HARVESTMODULESIDESCREEN.MINING_STOPPED);
		}
		GenericUIProgressBar reference2 = component.GetReference<GenericUIProgressBar>("diamondProgressBar");
		Storage component2 = resourceHarvestModule.GetComponent<Storage>();
		float num3 = component2.MassStored() / component2.Capacity();
		reference2.SetFillPercentage(num3);
		reference2.label.SetText(ElementLoader.GetElement(SimHashes.Diamond.CreateTag()).name + ": " + GameUtil.GetFormattedMass(component2.MassStored(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x0400408A RID: 16522
	private Clustercraft targetCraft;

	// Token: 0x0400408B RID: 16523
	public GameObject moduleContentContainer;

	// Token: 0x0400408C RID: 16524
	public GameObject modulePanelPrefab;
}
