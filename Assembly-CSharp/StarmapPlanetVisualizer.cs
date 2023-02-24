using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C05 RID: 3077
[AddComponentMenu("KMonoBehaviour/scripts/StarmapPlanetVisualizer")]
public class StarmapPlanetVisualizer : KMonoBehaviour
{
	// Token: 0x04004310 RID: 17168
	public Image image;

	// Token: 0x04004311 RID: 17169
	public LocText label;

	// Token: 0x04004312 RID: 17170
	public MultiToggle button;

	// Token: 0x04004313 RID: 17171
	public RectTransform selection;

	// Token: 0x04004314 RID: 17172
	public GameObject analysisSelection;

	// Token: 0x04004315 RID: 17173
	public Image unknownBG;

	// Token: 0x04004316 RID: 17174
	public GameObject rocketIconContainer;
}
