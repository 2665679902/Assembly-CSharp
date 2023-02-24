using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AEC RID: 2796
[AddComponentMenu("KMonoBehaviour/scripts/LogicRibbonDisplayUI")]
public class LogicControlInputUI : KMonoBehaviour
{
	// Token: 0x060055BA RID: 21946 RVA: 0x001EFCFC File Offset: 0x001EDEFC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.colourOn = GlobalAssets.Instance.colorSet.logicOn;
		this.colourOff = GlobalAssets.Instance.colorSet.logicOff;
		this.colourOn.a = (this.colourOff.a = byte.MaxValue);
		this.colourDisconnected = GlobalAssets.Instance.colorSet.logicDisconnected;
		this.icon.raycastTarget = false;
		this.border.raycastTarget = false;
	}

	// Token: 0x060055BB RID: 21947 RVA: 0x001EFD84 File Offset: 0x001EDF84
	public void SetContent(LogicCircuitNetwork network)
	{
		Color32 color = ((network == null) ? GlobalAssets.Instance.colorSet.logicDisconnected : (network.IsBitActive(0) ? this.colourOn : this.colourOff));
		this.icon.color = color;
	}

	// Token: 0x04003A40 RID: 14912
	[SerializeField]
	private Image icon;

	// Token: 0x04003A41 RID: 14913
	[SerializeField]
	private Image border;

	// Token: 0x04003A42 RID: 14914
	[SerializeField]
	private LogicModeUI uiAsset;

	// Token: 0x04003A43 RID: 14915
	private Color32 colourOn;

	// Token: 0x04003A44 RID: 14916
	private Color32 colourOff;

	// Token: 0x04003A45 RID: 14917
	private Color32 colourDisconnected;
}
