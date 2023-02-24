using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000ACA RID: 2762
public class KPopupMenu : KScreen
{
	// Token: 0x060054A5 RID: 21669 RVA: 0x001EB6D8 File Offset: 0x001E98D8
	public void SetOptions(IList<string> options)
	{
		List<KButtonMenu.ButtonInfo> list = new List<KButtonMenu.ButtonInfo>();
		for (int i = 0; i < options.Count; i++)
		{
			int index = i;
			string option = options[i];
			list.Add(new KButtonMenu.ButtonInfo(option, global::Action.NumActions, delegate
			{
				this.SelectOption(option, index);
			}, null, null));
		}
		this.Buttons = list.ToArray();
	}

	// Token: 0x060054A6 RID: 21670 RVA: 0x001EB750 File Offset: 0x001E9950
	public void OnClick()
	{
		if (this.Buttons != null)
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
				return;
			}
			this.buttonMenu.SetButtons(this.Buttons);
			this.buttonMenu.RefreshButtons();
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x060054A7 RID: 21671 RVA: 0x001EB7A7 File Offset: 0x001E99A7
	public void SelectOption(string option, int index)
	{
		if (this.OnSelect != null)
		{
			this.OnSelect(option, index);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x060054A8 RID: 21672 RVA: 0x001EB7CA File Offset: 0x001E99CA
	public IList<KButtonMenu.ButtonInfo> GetButtons()
	{
		return this.Buttons;
	}

	// Token: 0x0400398B RID: 14731
	[SerializeField]
	private KButtonMenu buttonMenu;

	// Token: 0x0400398C RID: 14732
	private KButtonMenu.ButtonInfo[] Buttons;

	// Token: 0x0400398D RID: 14733
	public Action<string, int> OnSelect;
}
