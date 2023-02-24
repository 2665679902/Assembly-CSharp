using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000BBA RID: 3002
public class HighEnergyParticleDirectionSideScreen : SideScreenContent
{
	// Token: 0x06005E5E RID: 24158 RVA: 0x00227BC3 File Offset: 0x00225DC3
	public override string GetTitle()
	{
		return UI.UISIDESCREENS.HIGHENERGYPARTICLEDIRECTIONSIDESCREEN.TITLE;
	}

	// Token: 0x06005E5F RID: 24159 RVA: 0x00227BD0 File Offset: 0x00225DD0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		for (int i = 0; i < this.Buttons.Count; i++)
		{
			KButton button = this.Buttons[i];
			button.onClick += delegate
			{
				int num = this.Buttons.IndexOf(button);
				if (this.activeButton != null)
				{
					this.activeButton.isInteractable = true;
				}
				button.isInteractable = false;
				this.activeButton = button;
				if (this.target != null)
				{
					this.target.Direction = EightDirectionUtil.AngleToDirection(num * 45);
					Game.Instance.ForceOverlayUpdate(true);
					this.Refresh();
				}
			};
		}
	}

	// Token: 0x06005E60 RID: 24160 RVA: 0x00227C2F File Offset: 0x00225E2F
	public override int GetSideScreenSortOrder()
	{
		return 10;
	}

	// Token: 0x06005E61 RID: 24161 RVA: 0x00227C34 File Offset: 0x00225E34
	public override bool IsValidForTarget(GameObject target)
	{
		HighEnergyParticleRedirector component = target.GetComponent<HighEnergyParticleRedirector>();
		bool flag = component != null;
		if (flag)
		{
			flag = flag && component.directionControllable;
		}
		bool flag2 = target.GetComponent<HighEnergyParticleSpawner>() != null || target.GetComponent<ManualHighEnergyParticleSpawner>() != null;
		return (flag || flag2) && target.GetComponent<IHighEnergyParticleDirection>() != null;
	}

	// Token: 0x06005E62 RID: 24162 RVA: 0x00227C8E File Offset: 0x00225E8E
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<IHighEnergyParticleDirection>();
		if (this.target == null)
		{
			global::Debug.LogError("The gameObject received does not contain IHighEnergyParticleDirection component");
			return;
		}
		this.Refresh();
	}

	// Token: 0x06005E63 RID: 24163 RVA: 0x00227CCC File Offset: 0x00225ECC
	private void Refresh()
	{
		int directionIndex = EightDirectionUtil.GetDirectionIndex(this.target.Direction);
		if (directionIndex >= 0 && directionIndex < this.Buttons.Count)
		{
			this.Buttons[directionIndex].SignalClick(KKeyCode.Mouse0);
		}
		else
		{
			if (this.activeButton)
			{
				this.activeButton.isInteractable = true;
			}
			this.activeButton = null;
		}
		this.directionLabel.SetText(string.Format(UI.UISIDESCREENS.HIGHENERGYPARTICLEDIRECTIONSIDESCREEN.SELECTED_DIRECTION, this.directionStrings[directionIndex]));
	}

	// Token: 0x0400408D RID: 16525
	private IHighEnergyParticleDirection target;

	// Token: 0x0400408E RID: 16526
	public List<KButton> Buttons;

	// Token: 0x0400408F RID: 16527
	private KButton activeButton;

	// Token: 0x04004090 RID: 16528
	public LocText directionLabel;

	// Token: 0x04004091 RID: 16529
	private string[] directionStrings = new string[]
	{
		UI.UISIDESCREENS.HIGHENERGYPARTICLEDIRECTIONSIDESCREEN.DIRECTION_N,
		UI.UISIDESCREENS.HIGHENERGYPARTICLEDIRECTIONSIDESCREEN.DIRECTION_NW,
		UI.UISIDESCREENS.HIGHENERGYPARTICLEDIRECTIONSIDESCREEN.DIRECTION_W,
		UI.UISIDESCREENS.HIGHENERGYPARTICLEDIRECTIONSIDESCREEN.DIRECTION_SW,
		UI.UISIDESCREENS.HIGHENERGYPARTICLEDIRECTIONSIDESCREEN.DIRECTION_S,
		UI.UISIDESCREENS.HIGHENERGYPARTICLEDIRECTIONSIDESCREEN.DIRECTION_SE,
		UI.UISIDESCREENS.HIGHENERGYPARTICLEDIRECTIONSIDESCREEN.DIRECTION_E,
		UI.UISIDESCREENS.HIGHENERGYPARTICLEDIRECTIONSIDESCREEN.DIRECTION_NE
	};
}
