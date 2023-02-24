using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B99 RID: 2969
public class ButtonMenuSideScreen : SideScreenContent
{
	// Token: 0x06005D5B RID: 23899 RVA: 0x00221BC8 File Offset: 0x0021FDC8
	public override bool IsValidForTarget(GameObject target)
	{
		ISidescreenButtonControl sidescreenButtonControl = target.GetComponent<ISidescreenButtonControl>();
		if (sidescreenButtonControl == null)
		{
			sidescreenButtonControl = target.GetSMI<ISidescreenButtonControl>();
		}
		return sidescreenButtonControl != null && sidescreenButtonControl.SidescreenEnabled();
	}

	// Token: 0x06005D5C RID: 23900 RVA: 0x00221BF1 File Offset: 0x0021FDF1
	public override int GetSideScreenSortOrder()
	{
		if (this.targets == null)
		{
			return 20;
		}
		return this.targets[0].ButtonSideScreenSortOrder();
	}

	// Token: 0x06005D5D RID: 23901 RVA: 0x00221C0F File Offset: 0x0021FE0F
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.targets = new_target.GetAllSMI<ISidescreenButtonControl>();
		this.targets.AddRange(new_target.GetComponents<ISidescreenButtonControl>());
		this.Refresh();
	}

	// Token: 0x06005D5E RID: 23902 RVA: 0x00221C48 File Offset: 0x0021FE48
	private void Refresh()
	{
		while (this.liveButtons.Count < this.targets.Count)
		{
			this.liveButtons.Add(Util.KInstantiateUI(this.buttonPrefab, this.buttonContainer.gameObject, true));
		}
		for (int i = 0; i < this.liveButtons.Count; i++)
		{
			if (i >= this.targets.Count)
			{
				this.liveButtons[i].SetActive(false);
			}
			else
			{
				if (!this.liveButtons[i].activeSelf)
				{
					this.liveButtons[i].SetActive(true);
				}
				KButton componentInChildren = this.liveButtons[i].GetComponentInChildren<KButton>();
				ToolTip componentInChildren2 = this.liveButtons[i].GetComponentInChildren<ToolTip>();
				LocText componentInChildren3 = this.liveButtons[i].GetComponentInChildren<LocText>();
				componentInChildren.isInteractable = this.targets[i].SidescreenButtonInteractable();
				componentInChildren.ClearOnClick();
				componentInChildren.onClick += this.targets[i].OnSidescreenButtonPressed;
				componentInChildren.onClick += this.Refresh;
				componentInChildren3.SetText(this.targets[i].SidescreenButtonText);
				componentInChildren2.SetSimpleTooltip(this.targets[i].SidescreenButtonTooltip);
			}
		}
	}

	// Token: 0x04003FD3 RID: 16339
	public const int DefaultButtonMenuSideScreenSortOrder = 20;

	// Token: 0x04003FD4 RID: 16340
	public GameObject buttonPrefab;

	// Token: 0x04003FD5 RID: 16341
	public RectTransform buttonContainer;

	// Token: 0x04003FD6 RID: 16342
	private List<GameObject> liveButtons = new List<GameObject>();

	// Token: 0x04003FD7 RID: 16343
	private List<ISidescreenButtonControl> targets;
}
