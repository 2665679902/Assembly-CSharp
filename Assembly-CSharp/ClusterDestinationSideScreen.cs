using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B9E RID: 2974
public class ClusterDestinationSideScreen : SideScreenContent
{
	// Token: 0x1700068E RID: 1678
	// (get) Token: 0x06005D86 RID: 23942 RVA: 0x00222709 File Offset: 0x00220909
	// (set) Token: 0x06005D87 RID: 23943 RVA: 0x00222711 File Offset: 0x00220911
	private ClusterDestinationSelector targetSelector { get; set; }

	// Token: 0x1700068F RID: 1679
	// (get) Token: 0x06005D88 RID: 23944 RVA: 0x0022271A File Offset: 0x0022091A
	// (set) Token: 0x06005D89 RID: 23945 RVA: 0x00222722 File Offset: 0x00220922
	private RocketClusterDestinationSelector targetRocketSelector { get; set; }

	// Token: 0x06005D8A RID: 23946 RVA: 0x0022272C File Offset: 0x0022092C
	protected override void OnSpawn()
	{
		this.changeDestinationButton.onClick += this.OnClickChangeDestination;
		this.clearDestinationButton.onClick += this.OnClickClearDestination;
		this.launchPadDropDown.targetDropDownContainer = GameScreenManager.Instance.ssOverlayCanvas;
		this.launchPadDropDown.CustomizeEmptyRow(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.FIRSTAVAILABLE, null);
		this.repeatButton.onClick += this.OnRepeatClicked;
	}

	// Token: 0x06005D8B RID: 23947 RVA: 0x002227A9 File Offset: 0x002209A9
	public override int GetSideScreenSortOrder()
	{
		return 300;
	}

	// Token: 0x06005D8C RID: 23948 RVA: 0x002227B0 File Offset: 0x002209B0
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.Refresh(null);
			this.m_refreshHandle = this.targetSelector.Subscribe(543433792, delegate(object data)
			{
				this.Refresh(null);
			});
			return;
		}
		if (this.m_refreshHandle != -1)
		{
			this.targetSelector.Unsubscribe(this.m_refreshHandle);
			this.m_refreshHandle = -1;
			this.launchPadDropDown.Close();
		}
	}

	// Token: 0x06005D8D RID: 23949 RVA: 0x00222820 File Offset: 0x00220A20
	public override bool IsValidForTarget(GameObject target)
	{
		ClusterDestinationSelector component = target.GetComponent<ClusterDestinationSelector>();
		return (component != null && component.assignable) || (target.GetComponent<RocketModule>() != null && target.HasTag(GameTags.LaunchButtonRocketModule)) || (target.GetComponent<RocketControlStation>() != null && target.GetComponent<RocketControlStation>().GetMyWorld().GetComponent<Clustercraft>()
			.Status != Clustercraft.CraftStatus.Launching);
	}

	// Token: 0x06005D8E RID: 23950 RVA: 0x00222890 File Offset: 0x00220A90
	public override void SetTarget(GameObject target)
	{
		this.targetSelector = target.GetComponent<ClusterDestinationSelector>();
		if (this.targetSelector == null)
		{
			if (target.GetComponent<RocketModuleCluster>() != null)
			{
				this.targetSelector = target.GetComponent<RocketModuleCluster>().CraftInterface.GetClusterDestinationSelector();
			}
			else if (target.GetComponent<RocketControlStation>() != null)
			{
				this.targetSelector = target.GetMyWorld().GetComponent<Clustercraft>().ModuleInterface.GetClusterDestinationSelector();
			}
		}
		this.targetRocketSelector = this.targetSelector as RocketClusterDestinationSelector;
	}

	// Token: 0x06005D8F RID: 23951 RVA: 0x00222918 File Offset: 0x00220B18
	private void Refresh(object data = null)
	{
		if (!this.targetSelector.IsAtDestination())
		{
			Sprite sprite;
			string text;
			string text2;
			ClusterGrid.Instance.GetLocationDescription(this.targetSelector.GetDestination(), out sprite, out text, out text2);
			this.destinationImage.sprite = sprite;
			this.destinationLabel.text = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.TITLE + ": " + text;
			this.clearDestinationButton.isInteractable = true;
		}
		else
		{
			this.destinationImage.sprite = Assets.GetSprite("hex_unknown");
			this.destinationLabel.text = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.TITLE + ": " + UI.SPACEDESTINATIONS.NONE.NAME;
			this.clearDestinationButton.isInteractable = false;
		}
		if (this.targetRocketSelector != null)
		{
			List<LaunchPad> launchPadsForDestination = LaunchPad.GetLaunchPadsForDestination(this.targetRocketSelector.GetDestination());
			this.launchPadDropDown.gameObject.SetActive(true);
			this.repeatButton.gameObject.SetActive(true);
			this.launchPadDropDown.Initialize(launchPadsForDestination, new Action<IListableOption, object>(this.OnLaunchPadEntryClick), new Func<IListableOption, IListableOption, object, int>(this.PadDropDownSort), new Action<DropDownEntry, object>(this.PadDropDownEntryRefreshAction), true, this.targetRocketSelector);
			if (!this.targetRocketSelector.IsAtDestination() && launchPadsForDestination.Count > 0)
			{
				this.launchPadDropDown.openButton.isInteractable = true;
				LaunchPad destinationPad = this.targetRocketSelector.GetDestinationPad();
				if (destinationPad != null)
				{
					this.launchPadDropDown.selectedLabel.text = destinationPad.GetProperName();
				}
				else
				{
					this.launchPadDropDown.selectedLabel.text = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.FIRSTAVAILABLE;
				}
			}
			else
			{
				this.launchPadDropDown.selectedLabel.text = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.FIRSTAVAILABLE;
				this.launchPadDropDown.openButton.isInteractable = false;
			}
			this.StyleRepeatButton();
		}
		else
		{
			this.launchPadDropDown.gameObject.SetActive(false);
			this.repeatButton.gameObject.SetActive(false);
		}
		this.StyleChangeDestinationButton();
	}

	// Token: 0x06005D90 RID: 23952 RVA: 0x00222B1F File Offset: 0x00220D1F
	private void OnClickChangeDestination()
	{
		if (this.targetSelector.assignable)
		{
			ClusterMapScreen.Instance.ShowInSelectDestinationMode(this.targetSelector);
		}
		this.StyleChangeDestinationButton();
	}

	// Token: 0x06005D91 RID: 23953 RVA: 0x00222B44 File Offset: 0x00220D44
	private void StyleChangeDestinationButton()
	{
	}

	// Token: 0x06005D92 RID: 23954 RVA: 0x00222B46 File Offset: 0x00220D46
	private void OnClickClearDestination()
	{
		this.targetSelector.SetDestination(this.targetSelector.GetMyWorldLocation());
	}

	// Token: 0x06005D93 RID: 23955 RVA: 0x00222B60 File Offset: 0x00220D60
	private void OnLaunchPadEntryClick(IListableOption option, object data)
	{
		LaunchPad launchPad = (LaunchPad)option;
		this.targetRocketSelector.SetDestinationPad(launchPad);
	}

	// Token: 0x06005D94 RID: 23956 RVA: 0x00222B80 File Offset: 0x00220D80
	private void PadDropDownEntryRefreshAction(DropDownEntry entry, object targetData)
	{
		LaunchPad launchPad = (LaunchPad)entry.entryData;
		Clustercraft component = this.targetRocketSelector.GetComponent<Clustercraft>();
		if (!(launchPad != null))
		{
			entry.button.isInteractable = true;
			entry.image.sprite = Assets.GetBuildingDef("LaunchPad").GetUISprite("ui", false);
			entry.tooltip.SetSimpleTooltip(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_FIRST_AVAILABLE);
			return;
		}
		string text;
		if (component.CanLandAtPad(launchPad, out text) == Clustercraft.PadLandingStatus.CanNeverLand)
		{
			entry.button.isInteractable = false;
			entry.image.sprite = Assets.GetSprite("iconWarning");
			entry.tooltip.SetSimpleTooltip(text);
			return;
		}
		entry.button.isInteractable = true;
		entry.image.sprite = launchPad.GetComponent<Building>().Def.GetUISprite("ui", false);
		entry.tooltip.SetSimpleTooltip(string.Format(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_VALID_SITE, launchPad.GetProperName()));
	}

	// Token: 0x06005D95 RID: 23957 RVA: 0x00222C7F File Offset: 0x00220E7F
	private int PadDropDownSort(IListableOption a, IListableOption b, object targetData)
	{
		return 0;
	}

	// Token: 0x06005D96 RID: 23958 RVA: 0x00222C82 File Offset: 0x00220E82
	private void OnRepeatClicked()
	{
		this.targetRocketSelector.Repeat = !this.targetRocketSelector.Repeat;
		this.StyleRepeatButton();
	}

	// Token: 0x06005D97 RID: 23959 RVA: 0x00222CA3 File Offset: 0x00220EA3
	private void StyleRepeatButton()
	{
		this.repeatButton.bgImage.colorStyleSetting = (this.targetRocketSelector.Repeat ? this.repeatOn : this.repeatOff);
		this.repeatButton.bgImage.ApplyColorStyleSetting();
	}

	// Token: 0x04003FE8 RID: 16360
	public Image destinationImage;

	// Token: 0x04003FE9 RID: 16361
	public LocText destinationLabel;

	// Token: 0x04003FEA RID: 16362
	public KButton changeDestinationButton;

	// Token: 0x04003FEB RID: 16363
	public KButton clearDestinationButton;

	// Token: 0x04003FEC RID: 16364
	public DropDown launchPadDropDown;

	// Token: 0x04003FED RID: 16365
	public KButton repeatButton;

	// Token: 0x04003FEE RID: 16366
	public ColorStyleSetting repeatOff;

	// Token: 0x04003FEF RID: 16367
	public ColorStyleSetting repeatOn;

	// Token: 0x04003FF0 RID: 16368
	public ColorStyleSetting defaultButton;

	// Token: 0x04003FF1 RID: 16369
	public ColorStyleSetting highlightButton;

	// Token: 0x04003FF4 RID: 16372
	private int m_refreshHandle = -1;
}
