using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000BD5 RID: 3029
public class RequestCrewSideScreen : SideScreenContent
{
	// Token: 0x06005F53 RID: 24403 RVA: 0x0022DFFC File Offset: 0x0022C1FC
	protected override void OnSpawn()
	{
		this.changeCrewButton.onClick += this.OnChangeCrewButtonPressed;
		this.crewReleaseButton.onClick += this.CrewRelease;
		this.crewRequestButton.onClick += this.CrewRequest;
		this.toggleMap.Add(this.crewReleaseButton, PassengerRocketModule.RequestCrewState.Release);
		this.toggleMap.Add(this.crewRequestButton, PassengerRocketModule.RequestCrewState.Request);
		this.Refresh();
	}

	// Token: 0x06005F54 RID: 24404 RVA: 0x0022E078 File Offset: 0x0022C278
	public override int GetSideScreenSortOrder()
	{
		return 100;
	}

	// Token: 0x06005F55 RID: 24405 RVA: 0x0022E07C File Offset: 0x0022C27C
	public override bool IsValidForTarget(GameObject target)
	{
		PassengerRocketModule component = target.GetComponent<PassengerRocketModule>();
		RocketControlStation component2 = target.GetComponent<RocketControlStation>();
		if (component != null)
		{
			return component.GetMyWorld() != null;
		}
		if (component2 != null)
		{
			RocketControlStation.StatesInstance smi = component2.GetSMI<RocketControlStation.StatesInstance>();
			return !smi.sm.IsInFlight(smi) && !smi.sm.IsLaunching(smi);
		}
		return false;
	}

	// Token: 0x06005F56 RID: 24406 RVA: 0x0022E0DE File Offset: 0x0022C2DE
	public override void SetTarget(GameObject target)
	{
		if (target.GetComponent<RocketControlStation>() != null)
		{
			this.rocketModule = target.GetMyWorld().GetComponent<Clustercraft>().ModuleInterface.GetPassengerModule();
		}
		else
		{
			this.rocketModule = target.GetComponent<PassengerRocketModule>();
		}
		this.Refresh();
	}

	// Token: 0x06005F57 RID: 24407 RVA: 0x0022E11D File Offset: 0x0022C31D
	private void Refresh()
	{
		this.RefreshRequestButtons();
	}

	// Token: 0x06005F58 RID: 24408 RVA: 0x0022E125 File Offset: 0x0022C325
	private void CrewRelease()
	{
		this.rocketModule.RequestCrewBoard(PassengerRocketModule.RequestCrewState.Release);
		this.RefreshRequestButtons();
	}

	// Token: 0x06005F59 RID: 24409 RVA: 0x0022E139 File Offset: 0x0022C339
	private void CrewRequest()
	{
		this.rocketModule.RequestCrewBoard(PassengerRocketModule.RequestCrewState.Request);
		this.RefreshRequestButtons();
	}

	// Token: 0x06005F5A RID: 24410 RVA: 0x0022E150 File Offset: 0x0022C350
	private void RefreshRequestButtons()
	{
		foreach (KeyValuePair<KToggle, PassengerRocketModule.RequestCrewState> keyValuePair in this.toggleMap)
		{
			this.RefreshRequestButton(keyValuePair.Key);
		}
	}

	// Token: 0x06005F5B RID: 24411 RVA: 0x0022E1AC File Offset: 0x0022C3AC
	private void RefreshRequestButton(KToggle button)
	{
		ImageToggleState[] array;
		if (this.toggleMap[button] == this.rocketModule.PassengersRequested)
		{
			button.isOn = true;
			array = button.GetComponentsInChildren<ImageToggleState>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive();
			}
			button.GetComponent<ImageToggleStateThrobber>().enabled = false;
			return;
		}
		button.isOn = false;
		array = button.GetComponentsInChildren<ImageToggleState>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetInactive();
		}
		button.GetComponent<ImageToggleStateThrobber>().enabled = false;
	}

	// Token: 0x06005F5C RID: 24412 RVA: 0x0022E234 File Offset: 0x0022C434
	private void OnChangeCrewButtonPressed()
	{
		if (this.activeChangeCrewSideScreen == null)
		{
			this.activeChangeCrewSideScreen = (AssignmentGroupControllerSideScreen)DetailsScreen.Instance.SetSecondarySideScreen(this.changeCrewSideScreenPrefab, UI.UISIDESCREENS.ASSIGNMENTGROUPCONTROLLER.TITLE);
			this.activeChangeCrewSideScreen.SetTarget(this.rocketModule.gameObject);
			return;
		}
		DetailsScreen.Instance.ClearSecondarySideScreen();
		this.activeChangeCrewSideScreen = null;
	}

	// Token: 0x06005F5D RID: 24413 RVA: 0x0022E29C File Offset: 0x0022C49C
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show)
		{
			DetailsScreen.Instance.ClearSecondarySideScreen();
			this.activeChangeCrewSideScreen = null;
		}
	}

	// Token: 0x0400414B RID: 16715
	private PassengerRocketModule rocketModule;

	// Token: 0x0400414C RID: 16716
	public KToggle crewReleaseButton;

	// Token: 0x0400414D RID: 16717
	public KToggle crewRequestButton;

	// Token: 0x0400414E RID: 16718
	private Dictionary<KToggle, PassengerRocketModule.RequestCrewState> toggleMap = new Dictionary<KToggle, PassengerRocketModule.RequestCrewState>();

	// Token: 0x0400414F RID: 16719
	public KButton changeCrewButton;

	// Token: 0x04004150 RID: 16720
	public KScreen changeCrewSideScreenPrefab;

	// Token: 0x04004151 RID: 16721
	private AssignmentGroupControllerSideScreen activeChangeCrewSideScreen;
}
