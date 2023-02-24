using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AED RID: 2797
[AddComponentMenu("KMonoBehaviour/scripts/LogicRibbonDisplayUI")]
public class LogicRibbonDisplayUI : KMonoBehaviour
{
	// Token: 0x060055BD RID: 21949 RVA: 0x001EFDD8 File Offset: 0x001EDFD8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.colourOn = GlobalAssets.Instance.colorSet.logicOn;
		this.colourOff = GlobalAssets.Instance.colorSet.logicOff;
		this.colourOn.a = (this.colourOff.a = byte.MaxValue);
		this.wire1.raycastTarget = false;
		this.wire2.raycastTarget = false;
		this.wire3.raycastTarget = false;
		this.wire4.raycastTarget = false;
	}

	// Token: 0x060055BE RID: 21950 RVA: 0x001EFE64 File Offset: 0x001EE064
	public void SetContent(LogicCircuitNetwork network)
	{
		Color32 color = this.colourDisconnected;
		List<Color32> list = new List<Color32>();
		for (int i = 0; i < this.bitDepth; i++)
		{
			list.Add((network == null) ? color : (network.IsBitActive(i) ? this.colourOn : this.colourOff));
		}
		if (this.wire1.color != list[0])
		{
			this.wire1.color = list[0];
		}
		if (this.wire2.color != list[1])
		{
			this.wire2.color = list[1];
		}
		if (this.wire3.color != list[2])
		{
			this.wire3.color = list[2];
		}
		if (this.wire4.color != list[3])
		{
			this.wire4.color = list[3];
		}
	}

	// Token: 0x04003A46 RID: 14918
	[SerializeField]
	private Image wire1;

	// Token: 0x04003A47 RID: 14919
	[SerializeField]
	private Image wire2;

	// Token: 0x04003A48 RID: 14920
	[SerializeField]
	private Image wire3;

	// Token: 0x04003A49 RID: 14921
	[SerializeField]
	private Image wire4;

	// Token: 0x04003A4A RID: 14922
	[SerializeField]
	private LogicModeUI uiAsset;

	// Token: 0x04003A4B RID: 14923
	private Color32 colourOn;

	// Token: 0x04003A4C RID: 14924
	private Color32 colourOff;

	// Token: 0x04003A4D RID: 14925
	private Color32 colourDisconnected = new Color(255f, 255f, 255f, 255f);

	// Token: 0x04003A4E RID: 14926
	private int bitDepth = 4;
}
