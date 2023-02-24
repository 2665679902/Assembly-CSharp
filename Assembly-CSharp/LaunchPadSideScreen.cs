using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000BBF RID: 3007
public class LaunchPadSideScreen : SideScreenContent
{
	// Token: 0x06005E79 RID: 24185 RVA: 0x002285E4 File Offset: 0x002267E4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.startNewRocketbutton.onClick += this.ClickStartNewRocket;
		this.devAutoRocketButton.onClick += this.ClickAutoRocket;
	}

	// Token: 0x06005E7A RID: 24186 RVA: 0x0022861A File Offset: 0x0022681A
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show)
		{
			DetailsScreen.Instance.ClearSecondarySideScreen();
		}
	}

	// Token: 0x06005E7B RID: 24187 RVA: 0x00228630 File Offset: 0x00226830
	public override int GetSideScreenSortOrder()
	{
		return 100;
	}

	// Token: 0x06005E7C RID: 24188 RVA: 0x00228634 File Offset: 0x00226834
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LaunchPad>() != null;
	}

	// Token: 0x06005E7D RID: 24189 RVA: 0x00228644 File Offset: 0x00226844
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		if (this.refreshEventHandle != -1)
		{
			this.selectedPad.Unsubscribe(this.refreshEventHandle);
		}
		this.selectedPad = new_target.GetComponent<LaunchPad>();
		if (this.selectedPad == null)
		{
			global::Debug.LogError("The gameObject received does not contain a LaunchPad component");
			return;
		}
		this.refreshEventHandle = this.selectedPad.Subscribe(-887025858, new Action<object>(this.RefreshWaitingToLandList));
		this.RefreshRocketButton();
		this.RefreshWaitingToLandList(null);
	}

	// Token: 0x06005E7E RID: 24190 RVA: 0x002286D4 File Offset: 0x002268D4
	private void RefreshWaitingToLandList(object data = null)
	{
		for (int i = this.waitingToLandRows.Count - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.waitingToLandRows[i]);
		}
		this.waitingToLandRows.Clear();
		this.nothingWaitingRow.SetActive(true);
		AxialI myWorldLocation = this.selectedPad.GetMyWorldLocation();
		foreach (ClusterGridEntity clusterGridEntity in ClusterGrid.Instance.GetEntitiesInRange(myWorldLocation, 1))
		{
			Clustercraft craft = clusterGridEntity as Clustercraft;
			if (!(craft == null) && craft.Status == Clustercraft.CraftStatus.InFlight && (!craft.IsFlightInProgress() || !(craft.Destination != myWorldLocation)))
			{
				GameObject gameObject = Util.KInstantiateUI(this.landableRocketRowPrefab, this.landableRowContainer, true);
				gameObject.GetComponentInChildren<LocText>().text = craft.Name;
				this.waitingToLandRows.Add(gameObject);
				KButton componentInChildren = gameObject.GetComponentInChildren<KButton>();
				componentInChildren.GetComponentInChildren<LocText>().SetText((craft.ModuleInterface.GetClusterDestinationSelector().GetDestinationPad() == this.selectedPad) ? UI.UISIDESCREENS.LAUNCHPADSIDESCREEN.CANCEL_LAND_BUTTON : UI.UISIDESCREENS.LAUNCHPADSIDESCREEN.LAND_BUTTON);
				string text;
				componentInChildren.isInteractable = craft.CanLandAtPad(this.selectedPad, out text) != Clustercraft.PadLandingStatus.CanNeverLand;
				if (!componentInChildren.isInteractable)
				{
					componentInChildren.GetComponent<ToolTip>().SetSimpleTooltip(text);
				}
				else
				{
					componentInChildren.GetComponent<ToolTip>().ClearMultiStringTooltip();
				}
				componentInChildren.onClick += delegate
				{
					if (craft.ModuleInterface.GetClusterDestinationSelector().GetDestinationPad() == this.selectedPad)
					{
						craft.GetComponent<ClusterDestinationSelector>().SetDestination(craft.Location);
					}
					else
					{
						craft.LandAtPad(this.selectedPad);
					}
					this.RefreshWaitingToLandList(null);
				};
				this.nothingWaitingRow.SetActive(false);
			}
		}
	}

	// Token: 0x06005E7F RID: 24191 RVA: 0x002288D4 File Offset: 0x00226AD4
	private void ClickStartNewRocket()
	{
		((SelectModuleSideScreen)DetailsScreen.Instance.SetSecondarySideScreen(this.changeModuleSideScreen, UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.CHANGEMODULEPANEL)).SetLaunchPad(this.selectedPad);
	}

	// Token: 0x06005E80 RID: 24192 RVA: 0x00228900 File Offset: 0x00226B00
	private void RefreshRocketButton()
	{
		bool isOperational = this.selectedPad.GetComponent<Operational>().IsOperational;
		this.startNewRocketbutton.isInteractable = this.selectedPad.LandedRocket == null && isOperational;
		if (!isOperational)
		{
			this.startNewRocketbutton.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_PAD_DISABLED);
		}
		else
		{
			this.startNewRocketbutton.GetComponent<ToolTip>().ClearMultiStringTooltip();
		}
		this.devAutoRocketButton.isInteractable = this.selectedPad.LandedRocket == null;
		this.devAutoRocketButton.gameObject.SetActive(DebugHandler.InstantBuildMode);
	}

	// Token: 0x06005E81 RID: 24193 RVA: 0x0022899C File Offset: 0x00226B9C
	private void ClickAutoRocket()
	{
		AutoRocketUtility.StartAutoRocket(this.selectedPad);
	}

	// Token: 0x040040A0 RID: 16544
	public GameObject content;

	// Token: 0x040040A1 RID: 16545
	private LaunchPad selectedPad;

	// Token: 0x040040A2 RID: 16546
	public LocText DescriptionText;

	// Token: 0x040040A3 RID: 16547
	public GameObject landableRocketRowPrefab;

	// Token: 0x040040A4 RID: 16548
	public GameObject newRocketPanel;

	// Token: 0x040040A5 RID: 16549
	public KButton startNewRocketbutton;

	// Token: 0x040040A6 RID: 16550
	public KButton devAutoRocketButton;

	// Token: 0x040040A7 RID: 16551
	public GameObject landableRowContainer;

	// Token: 0x040040A8 RID: 16552
	public GameObject nothingWaitingRow;

	// Token: 0x040040A9 RID: 16553
	public KScreen changeModuleSideScreen;

	// Token: 0x040040AA RID: 16554
	private int refreshEventHandle = -1;

	// Token: 0x040040AB RID: 16555
	public List<GameObject> waitingToLandRows = new List<GameObject>();
}
