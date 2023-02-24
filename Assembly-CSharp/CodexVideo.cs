using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A72 RID: 2674
public class CodexVideo : CodexWidget<CodexVideo>
{
	// Token: 0x17000614 RID: 1556
	// (get) Token: 0x060051B9 RID: 20921 RVA: 0x001D87DE File Offset: 0x001D69DE
	// (set) Token: 0x060051BA RID: 20922 RVA: 0x001D87E6 File Offset: 0x001D69E6
	public string name { get; set; }

	// Token: 0x17000615 RID: 1557
	// (get) Token: 0x060051BC RID: 20924 RVA: 0x001D87F8 File Offset: 0x001D69F8
	// (set) Token: 0x060051BB RID: 20923 RVA: 0x001D87EF File Offset: 0x001D69EF
	public string videoName
	{
		get
		{
			return "--> " + (this.name ?? "NULL");
		}
		set
		{
			this.name = value;
		}
	}

	// Token: 0x17000616 RID: 1558
	// (get) Token: 0x060051BD RID: 20925 RVA: 0x001D8813 File Offset: 0x001D6A13
	// (set) Token: 0x060051BE RID: 20926 RVA: 0x001D881B File Offset: 0x001D6A1B
	public string overlayName { get; set; }

	// Token: 0x17000617 RID: 1559
	// (get) Token: 0x060051BF RID: 20927 RVA: 0x001D8824 File Offset: 0x001D6A24
	// (set) Token: 0x060051C0 RID: 20928 RVA: 0x001D882C File Offset: 0x001D6A2C
	public List<string> overlayTexts { get; set; }

	// Token: 0x060051C1 RID: 20929 RVA: 0x001D8835 File Offset: 0x001D6A35
	public void ConfigureVideo(VideoWidget videoWidget, string clipName, string overlayName = null, List<string> overlayTexts = null)
	{
		videoWidget.SetClip(Assets.GetVideo(clipName), overlayName, overlayTexts);
	}

	// Token: 0x060051C2 RID: 20930 RVA: 0x001D8846 File Offset: 0x001D6A46
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		this.ConfigureVideo(contentGameObject.GetComponent<VideoWidget>(), this.name, this.overlayName, this.overlayTexts);
		base.ConfigurePreferredLayout(contentGameObject);
	}
}
