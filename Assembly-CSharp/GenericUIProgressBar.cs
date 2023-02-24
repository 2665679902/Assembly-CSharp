using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AA3 RID: 2723
[AddComponentMenu("KMonoBehaviour/scripts/GenericUIProgressBar")]
public class GenericUIProgressBar : KMonoBehaviour
{
	// Token: 0x06005379 RID: 21369 RVA: 0x001E4759 File Offset: 0x001E2959
	public void SetMaxValue(float max)
	{
		this.maxValue = max;
	}

	// Token: 0x0600537A RID: 21370 RVA: 0x001E4764 File Offset: 0x001E2964
	public void SetFillPercentage(float value)
	{
		this.fill.fillAmount = value;
		this.label.text = Util.FormatWholeNumber(Mathf.Min(this.maxValue, this.maxValue * value)) + "/" + this.maxValue.ToString();
	}

	// Token: 0x0400388E RID: 14478
	public Image fill;

	// Token: 0x0400388F RID: 14479
	public LocText label;

	// Token: 0x04003890 RID: 14480
	private float maxValue;
}
