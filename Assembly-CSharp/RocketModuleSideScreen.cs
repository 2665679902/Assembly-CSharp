using System;
using System.Collections;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BD7 RID: 3031
public class RocketModuleSideScreen : SideScreenContent
{
	// Token: 0x06005F67 RID: 24423 RVA: 0x0022E798 File Offset: 0x0022C998
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		RocketModuleSideScreen.instance = this;
	}

	// Token: 0x06005F68 RID: 24424 RVA: 0x0022E7A6 File Offset: 0x0022C9A6
	protected override void OnForcedCleanUp()
	{
		RocketModuleSideScreen.instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06005F69 RID: 24425 RVA: 0x0022E7B4 File Offset: 0x0022C9B4
	public override int GetSideScreenSortOrder()
	{
		return 500;
	}

	// Token: 0x06005F6A RID: 24426 RVA: 0x0022E7BC File Offset: 0x0022C9BC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.addNewModuleButton.onClick += delegate
		{
			Vector2 vector = Vector2.zero;
			if (SelectModuleSideScreen.Instance != null)
			{
				vector = SelectModuleSideScreen.Instance.mainContents.GetComponent<KScrollRect>().content.rectTransform().anchoredPosition;
			}
			this.ClickAddNew(vector.y, null);
		};
		this.removeModuleButton.onClick += this.ClickRemove;
		this.moveModuleUpButton.onClick += this.ClickSwapUp;
		this.moveModuleDownButton.onClick += this.ClickSwapDown;
		this.changeModuleButton.onClick += delegate
		{
			Vector2 vector2 = Vector2.zero;
			if (SelectModuleSideScreen.Instance != null)
			{
				vector2 = SelectModuleSideScreen.Instance.mainContents.GetComponent<KScrollRect>().content.rectTransform().anchoredPosition;
			}
			this.ClickChangeModule(vector2.y);
		};
		this.viewInteriorButton.onClick += this.ClickViewInterior;
		this.moduleNameLabel.textStyleSetting = this.nameSetting;
		this.moduleDescriptionLabel.textStyleSetting = this.descriptionSetting;
		this.moduleNameLabel.ApplySettings();
		this.moduleDescriptionLabel.ApplySettings();
	}

	// Token: 0x06005F6B RID: 24427 RVA: 0x0022E891 File Offset: 0x0022CA91
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		DetailsScreen.Instance.ClearSecondarySideScreen();
	}

	// Token: 0x06005F6C RID: 24428 RVA: 0x0022E8A3 File Offset: 0x0022CAA3
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06005F6D RID: 24429 RVA: 0x0022E8AB File Offset: 0x0022CAAB
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<ReorderableBuilding>() != null;
	}

	// Token: 0x06005F6E RID: 24430 RVA: 0x0022E8BC File Offset: 0x0022CABC
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.reorderable = new_target.GetComponent<ReorderableBuilding>();
		this.moduleIcon.sprite = Def.GetUISprite(this.reorderable.gameObject, "ui", false).first;
		this.moduleNameLabel.SetText(this.reorderable.GetProperName());
		this.moduleDescriptionLabel.SetText(this.reorderable.GetComponent<Building>().Desc);
		this.UpdateButtonStates();
	}

	// Token: 0x06005F6F RID: 24431 RVA: 0x0022E948 File Offset: 0x0022CB48
	public void UpdateButtonStates()
	{
		this.changeModuleButton.isInteractable = this.reorderable.CanChangeModule();
		this.changeModuleButton.GetComponent<ToolTip>().SetSimpleTooltip(this.changeModuleButton.isInteractable ? UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONCHANGEMODULE.DESC.text : UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONCHANGEMODULE.INVALID.text);
		this.addNewModuleButton.isInteractable = true;
		this.addNewModuleButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.ADDMODULE.DESC.text);
		this.removeModuleButton.isInteractable = this.reorderable.CanRemoveModule();
		this.removeModuleButton.GetComponent<ToolTip>().SetSimpleTooltip(this.removeModuleButton.isInteractable ? UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONREMOVEMODULE.DESC.text : UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONREMOVEMODULE.INVALID.text);
		this.moveModuleDownButton.isInteractable = this.reorderable.CanSwapDown(true);
		this.moveModuleDownButton.GetComponent<ToolTip>().SetSimpleTooltip(this.moveModuleDownButton.isInteractable ? UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONSWAPMODULEDOWN.DESC.text : UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONSWAPMODULEDOWN.INVALID.text);
		this.moveModuleUpButton.isInteractable = this.reorderable.CanSwapUp(true);
		this.moveModuleUpButton.GetComponent<ToolTip>().SetSimpleTooltip(this.moveModuleUpButton.isInteractable ? UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONSWAPMODULEUP.DESC.text : UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONSWAPMODULEUP.INVALID.text);
		ClustercraftExteriorDoor component = this.reorderable.GetComponent<ClustercraftExteriorDoor>();
		if (!(component != null) || !component.HasTargetWorld())
		{
			this.viewInteriorButton.isInteractable = false;
			this.viewInteriorButton.GetComponentInChildren<LocText>().SetText(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWINTERIOR.LABEL);
			this.viewInteriorButton.GetComponent<ToolTip>().SetSimpleTooltip(this.viewInteriorButton.isInteractable ? UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWINTERIOR.DESC.text : UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWINTERIOR.INVALID.text);
			return;
		}
		if (ClusterManager.Instance.activeWorld == component.GetTargetWorld())
		{
			this.changeModuleButton.isInteractable = false;
			this.addNewModuleButton.isInteractable = false;
			this.removeModuleButton.isInteractable = false;
			this.moveModuleDownButton.isInteractable = false;
			this.moveModuleUpButton.isInteractable = false;
			this.viewInteriorButton.isInteractable = component.GetMyWorldId() != (int)ClusterManager.INVALID_WORLD_IDX;
			this.viewInteriorButton.GetComponentInChildren<LocText>().SetText(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWEXTERIOR.LABEL);
			this.viewInteriorButton.GetComponent<ToolTip>().SetSimpleTooltip(this.viewInteriorButton.isInteractable ? UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWEXTERIOR.DESC.text : UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWEXTERIOR.INVALID.text);
			return;
		}
		this.viewInteriorButton.isInteractable = this.reorderable.GetComponent<PassengerRocketModule>() != null;
		this.viewInteriorButton.GetComponentInChildren<LocText>().SetText(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWINTERIOR.LABEL);
		this.viewInteriorButton.GetComponent<ToolTip>().SetSimpleTooltip(this.viewInteriorButton.isInteractable ? UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWINTERIOR.DESC.text : UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.BUTTONVIEWINTERIOR.INVALID.text);
	}

	// Token: 0x06005F70 RID: 24432 RVA: 0x0022EC48 File Offset: 0x0022CE48
	public void ClickAddNew(float scrollViewPosition, BuildingDef autoSelectDef = null)
	{
		SelectModuleSideScreen selectModuleSideScreen = (SelectModuleSideScreen)DetailsScreen.Instance.SetSecondarySideScreen(this.changeModuleSideScreen, UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.CHANGEMODULEPANEL);
		selectModuleSideScreen.addingNewModule = true;
		selectModuleSideScreen.SetTarget(this.reorderable.gameObject);
		if (autoSelectDef != null)
		{
			selectModuleSideScreen.SelectModule(autoSelectDef);
		}
		this.ScrollToTargetPoint(scrollViewPosition);
	}

	// Token: 0x06005F71 RID: 24433 RVA: 0x0022ECA4 File Offset: 0x0022CEA4
	private void ScrollToTargetPoint(float scrollViewPosition)
	{
		if (SelectModuleSideScreen.Instance != null)
		{
			SelectModuleSideScreen.Instance.mainContents.GetComponent<KScrollRect>().content.anchoredPosition = new Vector2(0f, scrollViewPosition);
			if (base.gameObject.activeInHierarchy)
			{
				base.StartCoroutine(this.DelayedScrollToTargetPoint(scrollViewPosition));
			}
		}
	}

	// Token: 0x06005F72 RID: 24434 RVA: 0x0022ECFD File Offset: 0x0022CEFD
	private IEnumerator DelayedScrollToTargetPoint(float scrollViewPosition)
	{
		if (SelectModuleSideScreen.Instance != null)
		{
			yield return SequenceUtil.WaitForEndOfFrame;
			SelectModuleSideScreen.Instance.mainContents.GetComponent<KScrollRect>().content.anchoredPosition = new Vector2(0f, scrollViewPosition);
		}
		yield break;
	}

	// Token: 0x06005F73 RID: 24435 RVA: 0x0022ED0C File Offset: 0x0022CF0C
	private void ClickRemove()
	{
		this.reorderable.Trigger(-790448070, null);
		this.UpdateButtonStates();
	}

	// Token: 0x06005F74 RID: 24436 RVA: 0x0022ED25 File Offset: 0x0022CF25
	private void ClickSwapUp()
	{
		this.reorderable.SwapWithAbove(true);
		this.UpdateButtonStates();
	}

	// Token: 0x06005F75 RID: 24437 RVA: 0x0022ED39 File Offset: 0x0022CF39
	private void ClickSwapDown()
	{
		this.reorderable.SwapWithBelow(true);
		this.UpdateButtonStates();
	}

	// Token: 0x06005F76 RID: 24438 RVA: 0x0022ED4D File Offset: 0x0022CF4D
	private void ClickChangeModule(float scrollViewPosition)
	{
		SelectModuleSideScreen selectModuleSideScreen = (SelectModuleSideScreen)DetailsScreen.Instance.SetSecondarySideScreen(this.changeModuleSideScreen, UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.CHANGEMODULEPANEL);
		selectModuleSideScreen.addingNewModule = false;
		selectModuleSideScreen.SetTarget(this.reorderable.gameObject);
		this.ScrollToTargetPoint(scrollViewPosition);
	}

	// Token: 0x06005F77 RID: 24439 RVA: 0x0022ED8C File Offset: 0x0022CF8C
	private void ClickViewInterior()
	{
		ClustercraftExteriorDoor component = this.reorderable.GetComponent<ClustercraftExteriorDoor>();
		PassengerRocketModule component2 = this.reorderable.GetComponent<PassengerRocketModule>();
		WorldContainer targetWorld = component.GetTargetWorld();
		WorldContainer myWorld = component.GetMyWorld();
		if (ClusterManager.Instance.activeWorld == targetWorld)
		{
			if (myWorld.id != (int)ClusterManager.INVALID_WORLD_IDX)
			{
				AudioMixer.instance.Stop(component2.interiorReverbSnapshot, STOP_MODE.ALLOWFADEOUT);
				ClusterManager.Instance.SetActiveWorld(myWorld.id);
			}
		}
		else
		{
			AudioMixer.instance.Start(component2.interiorReverbSnapshot);
			ClusterManager.Instance.SetActiveWorld(targetWorld.id);
		}
		DetailsScreen.Instance.ClearSecondarySideScreen();
		this.UpdateButtonStates();
	}

	// Token: 0x04004158 RID: 16728
	public static RocketModuleSideScreen instance;

	// Token: 0x04004159 RID: 16729
	private ReorderableBuilding reorderable;

	// Token: 0x0400415A RID: 16730
	public KScreen changeModuleSideScreen;

	// Token: 0x0400415B RID: 16731
	public Image moduleIcon;

	// Token: 0x0400415C RID: 16732
	[Header("Buttons")]
	public KButton addNewModuleButton;

	// Token: 0x0400415D RID: 16733
	public KButton removeModuleButton;

	// Token: 0x0400415E RID: 16734
	public KButton changeModuleButton;

	// Token: 0x0400415F RID: 16735
	public KButton moveModuleUpButton;

	// Token: 0x04004160 RID: 16736
	public KButton moveModuleDownButton;

	// Token: 0x04004161 RID: 16737
	public KButton viewInteriorButton;

	// Token: 0x04004162 RID: 16738
	[Header("Labels")]
	public LocText moduleNameLabel;

	// Token: 0x04004163 RID: 16739
	public LocText moduleDescriptionLabel;

	// Token: 0x04004164 RID: 16740
	public TextStyleSetting nameSetting;

	// Token: 0x04004165 RID: 16741
	public TextStyleSetting descriptionSetting;
}
