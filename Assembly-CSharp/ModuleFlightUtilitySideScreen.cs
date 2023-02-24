using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BC7 RID: 3015
public class ModuleFlightUtilitySideScreen : SideScreenContent
{
	// Token: 0x1700069D RID: 1693
	// (get) Token: 0x06005EC7 RID: 24263 RVA: 0x0022A250 File Offset: 0x00228450
	private CraftModuleInterface craftModuleInterface
	{
		get
		{
			return this.targetCraft.GetComponent<CraftModuleInterface>();
		}
	}

	// Token: 0x06005EC8 RID: 24264 RVA: 0x0022A25D File Offset: 0x0022845D
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005EC9 RID: 24265 RVA: 0x0022A26D File Offset: 0x0022846D
	public override float GetSortKey()
	{
		return 21f;
	}

	// Token: 0x06005ECA RID: 24266 RVA: 0x0022A274 File Offset: 0x00228474
	public override bool IsValidForTarget(GameObject target)
	{
		if (target.GetComponent<Clustercraft>() != null && this.HasFlightUtilityModule(target.GetComponent<CraftModuleInterface>()))
		{
			return true;
		}
		RocketControlStation component = target.GetComponent<RocketControlStation>();
		return component != null && this.HasFlightUtilityModule(component.GetMyWorld().GetComponent<Clustercraft>().ModuleInterface);
	}

	// Token: 0x06005ECB RID: 24267 RVA: 0x0022A2C8 File Offset: 0x002284C8
	private bool HasFlightUtilityModule(CraftModuleInterface craftModuleInterface)
	{
		using (IEnumerator<Ref<RocketModuleCluster>> enumerator = craftModuleInterface.ClusterModules.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Get().GetSMI<IEmptyableCargo>() != null)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06005ECC RID: 24268 RVA: 0x0022A320 File Offset: 0x00228520
	public override void SetTarget(GameObject target)
	{
		if (target != null)
		{
			foreach (int num in this.refreshHandle)
			{
				target.Unsubscribe(num);
			}
			this.refreshHandle.Clear();
		}
		base.SetTarget(target);
		this.targetCraft = target.GetComponent<Clustercraft>();
		if (this.targetCraft == null && target.GetComponent<RocketControlStation>() != null)
		{
			this.targetCraft = target.GetMyWorld().GetComponent<Clustercraft>();
		}
		this.refreshHandle.Add(this.targetCraft.gameObject.Subscribe(-1298331547, new Action<object>(this.RefreshAll)));
		this.refreshHandle.Add(this.targetCraft.gameObject.Subscribe(1792516731, new Action<object>(this.RefreshAll)));
		this.BuildModules();
	}

	// Token: 0x06005ECD RID: 24269 RVA: 0x0022A428 File Offset: 0x00228628
	private void ClearModules()
	{
		foreach (KeyValuePair<IEmptyableCargo, HierarchyReferences> keyValuePair in this.modulePanels)
		{
			Util.KDestroyGameObject(keyValuePair.Value.gameObject);
		}
		this.modulePanels.Clear();
	}

	// Token: 0x06005ECE RID: 24270 RVA: 0x0022A490 File Offset: 0x00228690
	private void BuildModules()
	{
		this.ClearModules();
		foreach (Ref<RocketModuleCluster> @ref in this.craftModuleInterface.ClusterModules)
		{
			IEmptyableCargo smi = @ref.Get().GetSMI<IEmptyableCargo>();
			if (smi != null)
			{
				HierarchyReferences hierarchyReferences = Util.KInstantiateUI<HierarchyReferences>(this.modulePanelPrefab, this.moduleContentContainer, true);
				this.modulePanels.Add(smi, hierarchyReferences);
				this.RefreshModulePanel(smi);
			}
		}
	}

	// Token: 0x06005ECF RID: 24271 RVA: 0x0022A518 File Offset: 0x00228718
	private void RefreshAll(object data = null)
	{
		this.BuildModules();
	}

	// Token: 0x06005ED0 RID: 24272 RVA: 0x0022A520 File Offset: 0x00228720
	private void RefreshModulePanel(IEmptyableCargo module)
	{
		HierarchyReferences hierarchyReferences = this.modulePanels[module];
		hierarchyReferences.GetReference<Image>("icon").sprite = Def.GetUISprite(module.master.gameObject, "ui", false).first;
		KButton reference = hierarchyReferences.GetReference<KButton>("button");
		reference.isInteractable = module.CanEmptyCargo();
		reference.ClearOnClick();
		reference.onClick += module.EmptyCargo;
		KButton reference2 = hierarchyReferences.GetReference<KButton>("repeatButton");
		if (module.CanAutoDeploy)
		{
			this.StyleRepeatButton(module);
			reference2.ClearOnClick();
			reference2.onClick += delegate
			{
				this.OnRepeatClicked(module);
			};
			reference2.gameObject.SetActive(true);
		}
		else
		{
			reference2.gameObject.SetActive(false);
		}
		DropDown reference3 = hierarchyReferences.GetReference<DropDown>("dropDown");
		reference3.targetDropDownContainer = GameScreenManager.Instance.ssOverlayCanvas;
		reference3.Close();
		CrewPortrait reference4 = hierarchyReferences.GetReference<CrewPortrait>("selectedPortrait");
		WorldContainer component = (module as StateMachine.Instance).GetMaster().GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<WorldContainer>();
		if (component != null && module.ChooseDuplicant)
		{
			int id = component.id;
			reference3.gameObject.SetActive(true);
			reference3.Initialize(Components.LiveMinionIdentities.GetWorldItems(id, false), new Action<IListableOption, object>(this.OnDuplicantEntryClick), null, new Action<DropDownEntry, object>(this.DropDownEntryRefreshAction), true, module);
			reference3.selectedLabel.text = ((module.ChosenDuplicant != null) ? this.GetDuplicantRowName(module.ChosenDuplicant) : UI.UISIDESCREENS.MODULEFLIGHTUTILITYSIDESCREEN.SELECT_DUPLICANT.ToString());
			reference4.gameObject.SetActive(true);
			reference4.SetIdentityObject(module.ChosenDuplicant, false);
			reference3.openButton.isInteractable = !module.ModuleDeployed;
		}
		else
		{
			reference3.gameObject.SetActive(false);
			reference4.gameObject.SetActive(false);
		}
		hierarchyReferences.GetReference<LocText>("label").SetText(module.master.gameObject.GetProperName());
	}

	// Token: 0x06005ED1 RID: 24273 RVA: 0x0022A778 File Offset: 0x00228978
	private string GetDuplicantRowName(MinionIdentity minion)
	{
		MinionResume component = minion.GetComponent<MinionResume>();
		if (component != null && component.HasPerk(Db.Get().SkillPerks.CanUseRocketControlStation))
		{
			return string.Format(UI.UISIDESCREENS.MODULEFLIGHTUTILITYSIDESCREEN.PILOT_FMT, minion.GetProperName());
		}
		return minion.GetProperName();
	}

	// Token: 0x06005ED2 RID: 24274 RVA: 0x0022A7C8 File Offset: 0x002289C8
	private void OnRepeatClicked(IEmptyableCargo module)
	{
		module.AutoDeploy = !module.AutoDeploy;
		this.StyleRepeatButton(module);
	}

	// Token: 0x06005ED3 RID: 24275 RVA: 0x0022A7E0 File Offset: 0x002289E0
	private void OnDuplicantEntryClick(IListableOption option, object data)
	{
		MinionIdentity minionIdentity = (MinionIdentity)option;
		IEmptyableCargo emptyableCargo = (IEmptyableCargo)data;
		emptyableCargo.ChosenDuplicant = minionIdentity;
		HierarchyReferences hierarchyReferences = this.modulePanels[emptyableCargo];
		hierarchyReferences.GetReference<DropDown>("dropDown").selectedLabel.text = ((emptyableCargo.ChosenDuplicant != null) ? this.GetDuplicantRowName(emptyableCargo.ChosenDuplicant) : UI.UISIDESCREENS.MODULEFLIGHTUTILITYSIDESCREEN.SELECT_DUPLICANT.ToString());
		hierarchyReferences.GetReference<CrewPortrait>("selectedPortrait").SetIdentityObject(emptyableCargo.ChosenDuplicant, false);
		this.RefreshAll(null);
	}

	// Token: 0x06005ED4 RID: 24276 RVA: 0x0022A868 File Offset: 0x00228A68
	private void DropDownEntryRefreshAction(DropDownEntry entry, object targetData)
	{
		MinionIdentity minionIdentity = (MinionIdentity)entry.entryData;
		entry.label.text = this.GetDuplicantRowName(minionIdentity);
		entry.portrait.SetIdentityObject(minionIdentity, false);
		bool flag = false;
		foreach (Ref<RocketModuleCluster> @ref in this.targetCraft.ModuleInterface.ClusterModules)
		{
			RocketModuleCluster rocketModuleCluster = @ref.Get();
			if (!(rocketModuleCluster == null))
			{
				IEmptyableCargo smi = rocketModuleCluster.GetSMI<IEmptyableCargo>();
				if (smi != null && !(((IEmptyableCargo)targetData).ChosenDuplicant == minionIdentity))
				{
					flag = flag || smi.ChosenDuplicant == minionIdentity;
				}
			}
		}
		entry.button.isInteractable = !flag;
	}

	// Token: 0x06005ED5 RID: 24277 RVA: 0x0022A938 File Offset: 0x00228B38
	private void StyleRepeatButton(IEmptyableCargo module)
	{
		KButton reference = this.modulePanels[module].GetReference<KButton>("repeatButton");
		reference.bgImage.colorStyleSetting = (module.AutoDeploy ? this.repeatOn : this.repeatOff);
		reference.bgImage.ApplyColorStyleSetting();
	}

	// Token: 0x040040DC RID: 16604
	private Clustercraft targetCraft;

	// Token: 0x040040DD RID: 16605
	public GameObject moduleContentContainer;

	// Token: 0x040040DE RID: 16606
	public GameObject modulePanelPrefab;

	// Token: 0x040040DF RID: 16607
	public ColorStyleSetting repeatOff;

	// Token: 0x040040E0 RID: 16608
	public ColorStyleSetting repeatOn;

	// Token: 0x040040E1 RID: 16609
	private Dictionary<IEmptyableCargo, HierarchyReferences> modulePanels = new Dictionary<IEmptyableCargo, HierarchyReferences>();

	// Token: 0x040040E2 RID: 16610
	private List<int> refreshHandle = new List<int>();
}
