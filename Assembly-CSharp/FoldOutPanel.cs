using System;
using UnityEngine;

// Token: 0x02000A9D RID: 2717
public class FoldOutPanel : KMonoBehaviour
{
	// Token: 0x06005344 RID: 21316 RVA: 0x001E372C File Offset: 0x001E192C
	protected override void OnSpawn()
	{
		MultiToggle componentInChildren = base.GetComponentInChildren<MultiToggle>();
		componentInChildren.onClick = (System.Action)Delegate.Combine(componentInChildren.onClick, new System.Action(this.OnClick));
		this.ToggleOpen(this.startOpen);
	}

	// Token: 0x06005345 RID: 21317 RVA: 0x001E3761 File Offset: 0x001E1961
	private void OnClick()
	{
		this.ToggleOpen(!this.panelOpen);
	}

	// Token: 0x06005346 RID: 21318 RVA: 0x001E3772 File Offset: 0x001E1972
	private void ToggleOpen(bool open)
	{
		this.panelOpen = open;
		this.container.SetActive(this.panelOpen);
		base.GetComponentInChildren<MultiToggle>().ChangeState(this.panelOpen ? 1 : 0);
	}

	// Token: 0x04003871 RID: 14449
	private bool panelOpen = true;

	// Token: 0x04003872 RID: 14450
	public GameObject container;

	// Token: 0x04003873 RID: 14451
	public bool startOpen = true;
}
