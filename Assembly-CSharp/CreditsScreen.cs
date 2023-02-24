using System;
using System.Collections.Generic;
using Klei;
using UnityEngine;

// Token: 0x020009FA RID: 2554
public class CreditsScreen : KModalScreen
{
	// Token: 0x06004CBE RID: 19646 RVA: 0x001B0170 File Offset: 0x001AE370
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (TextAsset textAsset in this.creditsFiles)
		{
			this.AddCredits(textAsset);
		}
		this.CloseButton.onClick += this.Close;
	}

	// Token: 0x06004CBF RID: 19647 RVA: 0x001B01BA File Offset: 0x001AE3BA
	public void Close()
	{
		this.Deactivate();
	}

	// Token: 0x06004CC0 RID: 19648 RVA: 0x001B01C4 File Offset: 0x001AE3C4
	private void AddCredits(TextAsset csv)
	{
		string[,] array = CSVReader.SplitCsvGrid(csv.text, csv.name);
		List<string> list = new List<string>();
		for (int i = 1; i < array.GetLength(1); i++)
		{
			string text = string.Format("{0} {1}", array[0, i], array[1, i]);
			if (!(text == " "))
			{
				list.Add(text);
			}
		}
		list.Shuffle<string>();
		string text2 = array[0, 0];
		GameObject gameObject = Util.KInstantiateUI(this.teamHeaderPrefab, this.entryContainer.gameObject, true);
		gameObject.GetComponent<LocText>().text = text2;
		this.teamContainers.Add(text2, gameObject);
		foreach (string text3 in list)
		{
			Util.KInstantiateUI(this.entryPrefab, this.teamContainers[text2], true).GetComponent<LocText>().text = text3;
		}
	}

	// Token: 0x04003292 RID: 12946
	public GameObject entryPrefab;

	// Token: 0x04003293 RID: 12947
	public GameObject teamHeaderPrefab;

	// Token: 0x04003294 RID: 12948
	private Dictionary<string, GameObject> teamContainers = new Dictionary<string, GameObject>();

	// Token: 0x04003295 RID: 12949
	public Transform entryContainer;

	// Token: 0x04003296 RID: 12950
	public KButton CloseButton;

	// Token: 0x04003297 RID: 12951
	public TextAsset[] creditsFiles;
}
