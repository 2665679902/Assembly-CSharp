using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A77 RID: 2679
public class ComicViewer : KScreen
{
	// Token: 0x0600520A RID: 21002 RVA: 0x001DA368 File Offset: 0x001D8568
	public void ShowComic(ComicData comic, bool isVictoryComic)
	{
		for (int i = 0; i < Mathf.Max(comic.images.Length, comic.stringKeys.Length); i++)
		{
			GameObject gameObject = Util.KInstantiateUI(this.panelPrefab, this.contentContainer, true);
			this.activePanels.Add(gameObject);
			gameObject.GetComponentInChildren<Image>().sprite = comic.images[i];
			gameObject.GetComponentInChildren<LocText>().SetText(comic.stringKeys[i]);
		}
		this.closeButton.ClearOnClick();
		if (isVictoryComic)
		{
			this.closeButton.onClick += delegate
			{
				this.Stop();
				this.Show(false);
			};
			return;
		}
		this.closeButton.onClick += delegate
		{
			this.Stop();
		};
	}

	// Token: 0x0600520B RID: 21003 RVA: 0x001DA417 File Offset: 0x001D8617
	public void Stop()
	{
		this.OnStop();
		this.Show(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04003744 RID: 14148
	public GameObject panelPrefab;

	// Token: 0x04003745 RID: 14149
	public GameObject contentContainer;

	// Token: 0x04003746 RID: 14150
	public List<GameObject> activePanels = new List<GameObject>();

	// Token: 0x04003747 RID: 14151
	public KButton closeButton;

	// Token: 0x04003748 RID: 14152
	public System.Action OnStop;
}
