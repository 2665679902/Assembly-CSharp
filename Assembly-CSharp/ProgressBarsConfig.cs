using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B63 RID: 2915
public class ProgressBarsConfig : ScriptableObject
{
	// Token: 0x06005AF7 RID: 23287 RVA: 0x002106C7 File Offset: 0x0020E8C7
	public static void DestroyInstance()
	{
		ProgressBarsConfig.instance = null;
	}

	// Token: 0x17000671 RID: 1649
	// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x002106CF File Offset: 0x0020E8CF
	public static ProgressBarsConfig Instance
	{
		get
		{
			if (ProgressBarsConfig.instance == null)
			{
				ProgressBarsConfig.instance = Resources.Load<ProgressBarsConfig>("ProgressBarsConfig");
				ProgressBarsConfig.instance.Initialize();
			}
			return ProgressBarsConfig.instance;
		}
	}

	// Token: 0x06005AF9 RID: 23289 RVA: 0x002106FC File Offset: 0x0020E8FC
	public void Initialize()
	{
		foreach (ProgressBarsConfig.BarData barData in this.barColorDataList)
		{
			this.barColorMap.Add(barData.barName, barData);
		}
	}

	// Token: 0x06005AFA RID: 23290 RVA: 0x0021075C File Offset: 0x0020E95C
	public string GetBarDescription(string barName)
	{
		string text = "";
		if (this.IsBarNameValid(barName))
		{
			text = Strings.Get(this.barColorMap[barName].barDescriptionKey);
		}
		return text;
	}

	// Token: 0x06005AFB RID: 23291 RVA: 0x00210798 File Offset: 0x0020E998
	public Color GetBarColor(string barName)
	{
		Color color = Color.clear;
		if (this.IsBarNameValid(barName))
		{
			color = this.barColorMap[barName].barColor;
		}
		return color;
	}

	// Token: 0x06005AFC RID: 23292 RVA: 0x002107C7 File Offset: 0x0020E9C7
	public bool IsBarNameValid(string barName)
	{
		if (string.IsNullOrEmpty(barName))
		{
			global::Debug.LogError("The barName provided was null or empty. Don't do that.");
			return false;
		}
		if (!this.barColorMap.ContainsKey(barName))
		{
			global::Debug.LogError(string.Format("No BarData found for the entry [ {0} ]", barName));
			return false;
		}
		return true;
	}

	// Token: 0x04003DA0 RID: 15776
	public GameObject progressBarPrefab;

	// Token: 0x04003DA1 RID: 15777
	public GameObject progressBarUIPrefab;

	// Token: 0x04003DA2 RID: 15778
	public GameObject healthBarPrefab;

	// Token: 0x04003DA3 RID: 15779
	public List<ProgressBarsConfig.BarData> barColorDataList = new List<ProgressBarsConfig.BarData>();

	// Token: 0x04003DA4 RID: 15780
	public Dictionary<string, ProgressBarsConfig.BarData> barColorMap = new Dictionary<string, ProgressBarsConfig.BarData>();

	// Token: 0x04003DA5 RID: 15781
	private static ProgressBarsConfig instance;

	// Token: 0x02001A09 RID: 6665
	[Serializable]
	public struct BarData
	{
		// Token: 0x0400764C RID: 30284
		public string barName;

		// Token: 0x0400764D RID: 30285
		public Color barColor;

		// Token: 0x0400764E RID: 30286
		public string barDescriptionKey;
	}
}
