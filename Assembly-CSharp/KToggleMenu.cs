using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000ACB RID: 2763
public class KToggleMenu : KScreen
{
	// Token: 0x14000025 RID: 37
	// (add) Token: 0x060054AA RID: 21674 RVA: 0x001EB7DC File Offset: 0x001E99DC
	// (remove) Token: 0x060054AB RID: 21675 RVA: 0x001EB814 File Offset: 0x001E9A14
	public event KToggleMenu.OnSelect onSelect;

	// Token: 0x060054AC RID: 21676 RVA: 0x001EB849 File Offset: 0x001E9A49
	public void Setup(IList<KToggleMenu.ToggleInfo> toggleInfo)
	{
		this.toggleInfo = toggleInfo;
		this.RefreshButtons();
	}

	// Token: 0x060054AD RID: 21677 RVA: 0x001EB858 File Offset: 0x001E9A58
	protected void Setup()
	{
		this.RefreshButtons();
	}

	// Token: 0x060054AE RID: 21678 RVA: 0x001EB860 File Offset: 0x001E9A60
	private void RefreshButtons()
	{
		foreach (KToggle ktoggle in this.toggles)
		{
			if (ktoggle != null)
			{
				UnityEngine.Object.Destroy(ktoggle.gameObject);
			}
		}
		this.toggles.Clear();
		if (this.toggleInfo == null)
		{
			return;
		}
		Transform transform = ((this.toggleParent != null) ? this.toggleParent : base.transform);
		for (int i = 0; i < this.toggleInfo.Count; i++)
		{
			int idx = i;
			KToggleMenu.ToggleInfo toggleInfo = this.toggleInfo[i];
			if (toggleInfo == null)
			{
				this.toggles.Add(null);
			}
			else
			{
				KToggle ktoggle2 = UnityEngine.Object.Instantiate<KToggle>(this.prefab, Vector3.zero, Quaternion.identity);
				ktoggle2.gameObject.name = "Toggle:" + toggleInfo.text;
				ktoggle2.transform.SetParent(transform, false);
				ktoggle2.group = this.group;
				ktoggle2.onClick += delegate
				{
					this.OnClick(idx);
				};
				ktoggle2.GetComponentsInChildren<Text>(true)[0].text = toggleInfo.text;
				toggleInfo.toggle = ktoggle2;
				this.toggles.Add(ktoggle2);
			}
		}
	}

	// Token: 0x060054AF RID: 21679 RVA: 0x001EB9D8 File Offset: 0x001E9BD8
	public int GetSelected()
	{
		return KToggleMenu.selected;
	}

	// Token: 0x060054B0 RID: 21680 RVA: 0x001EB9DF File Offset: 0x001E9BDF
	private void OnClick(int i)
	{
		UISounds.PlaySound(UISounds.Sound.ClickObject);
		if (this.onSelect == null)
		{
			return;
		}
		this.onSelect(this.toggleInfo[i]);
	}

	// Token: 0x060054B1 RID: 21681 RVA: 0x001EBA08 File Offset: 0x001E9C08
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.toggles == null)
		{
			return;
		}
		for (int i = 0; i < this.toggleInfo.Count; i++)
		{
			global::Action hotKey = this.toggleInfo[i].hotKey;
			if (hotKey != global::Action.NumActions && e.TryConsume(hotKey))
			{
				this.toggles[i].Click();
				return;
			}
		}
	}

	// Token: 0x0400398E RID: 14734
	[SerializeField]
	private Transform toggleParent;

	// Token: 0x0400398F RID: 14735
	[SerializeField]
	private KToggle prefab;

	// Token: 0x04003990 RID: 14736
	[SerializeField]
	private ToggleGroup group;

	// Token: 0x04003992 RID: 14738
	protected IList<KToggleMenu.ToggleInfo> toggleInfo;

	// Token: 0x04003993 RID: 14739
	protected List<KToggle> toggles = new List<KToggle>();

	// Token: 0x04003994 RID: 14740
	private static int selected = -1;

	// Token: 0x0200194C RID: 6476
	// (Invoke) Token: 0x06008FCA RID: 36810
	public delegate void OnSelect(KToggleMenu.ToggleInfo toggleInfo);

	// Token: 0x0200194D RID: 6477
	public class ToggleInfo
	{
		// Token: 0x06008FCD RID: 36813 RVA: 0x00311002 File Offset: 0x0030F202
		public ToggleInfo(string text, object user_data = null, global::Action hotKey = global::Action.NumActions)
		{
			this.text = text;
			this.userData = user_data;
			this.hotKey = hotKey;
		}

		// Token: 0x040073F8 RID: 29688
		public string text;

		// Token: 0x040073F9 RID: 29689
		public object userData;

		// Token: 0x040073FA RID: 29690
		public KToggle toggle;

		// Token: 0x040073FB RID: 29691
		public global::Action hotKey;
	}
}
