using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BCA RID: 3018
public class NToggleSideScreen : SideScreenContent
{
	// Token: 0x06005EE4 RID: 24292 RVA: 0x0022ABCA File Offset: 0x00228DCA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06005EE5 RID: 24293 RVA: 0x0022ABD2 File Offset: 0x00228DD2
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<INToggleSideScreenControl>() != null;
	}

	// Token: 0x06005EE6 RID: 24294 RVA: 0x0022ABE0 File Offset: 0x00228DE0
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.target = target.GetComponent<INToggleSideScreenControl>();
		if (this.target == null)
		{
			return;
		}
		this.titleKey = this.target.SidescreenTitleKey;
		base.gameObject.SetActive(true);
		this.Refresh();
	}

	// Token: 0x06005EE7 RID: 24295 RVA: 0x0022AC2C File Offset: 0x00228E2C
	private void Refresh()
	{
		for (int i = 0; i < Mathf.Max(this.target.Options.Count, this.buttonList.Count); i++)
		{
			if (i >= this.target.Options.Count)
			{
				this.buttonList[i].gameObject.SetActive(false);
			}
			else
			{
				if (i >= this.buttonList.Count)
				{
					KToggle ktoggle = Util.KInstantiateUI<KToggle>(this.buttonPrefab.gameObject, this.ContentContainer, false);
					int idx = i;
					ktoggle.onClick += delegate
					{
						this.target.QueueSelectedOption(idx);
						this.Refresh();
					};
					this.buttonList.Add(ktoggle);
				}
				this.buttonList[i].GetComponentInChildren<LocText>().text = this.target.Options[i];
				this.buttonList[i].GetComponentInChildren<ToolTip>().toolTip = this.target.Tooltips[i];
				if (this.target.SelectedOption == i && this.target.QueuedOption == i)
				{
					this.buttonList[i].isOn = true;
					ImageToggleState[] array = this.buttonList[i].GetComponentsInChildren<ImageToggleState>();
					for (int j = 0; j < array.Length; j++)
					{
						array[j].SetActive();
					}
					this.buttonList[i].GetComponent<ImageToggleStateThrobber>().enabled = false;
				}
				else if (this.target.QueuedOption == i)
				{
					this.buttonList[i].isOn = true;
					ImageToggleState[] array = this.buttonList[i].GetComponentsInChildren<ImageToggleState>();
					for (int j = 0; j < array.Length; j++)
					{
						array[j].SetActive();
					}
					this.buttonList[i].GetComponent<ImageToggleStateThrobber>().enabled = true;
				}
				else
				{
					this.buttonList[i].isOn = false;
					foreach (ImageToggleState imageToggleState in this.buttonList[i].GetComponentsInChildren<ImageToggleState>())
					{
						imageToggleState.SetInactive();
						imageToggleState.SetInactive();
					}
					this.buttonList[i].GetComponent<ImageToggleStateThrobber>().enabled = false;
				}
				this.buttonList[i].gameObject.SetActive(true);
			}
		}
		this.description.text = this.target.Description;
		this.description.gameObject.SetActive(!string.IsNullOrEmpty(this.target.Description));
	}

	// Token: 0x040040E9 RID: 16617
	[SerializeField]
	private KToggle buttonPrefab;

	// Token: 0x040040EA RID: 16618
	[SerializeField]
	private LocText description;

	// Token: 0x040040EB RID: 16619
	private INToggleSideScreenControl target;

	// Token: 0x040040EC RID: 16620
	private List<KToggle> buttonList = new List<KToggle>();
}
