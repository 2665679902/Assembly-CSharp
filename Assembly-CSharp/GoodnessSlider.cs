using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AA4 RID: 2724
[AddComponentMenu("KMonoBehaviour/scripts/GoodnessSlider")]
public class GoodnessSlider : KMonoBehaviour
{
	// Token: 0x0600537C RID: 21372 RVA: 0x001E47BD File Offset: 0x001E29BD
	protected override void OnSpawn()
	{
		base.Spawn();
		this.UpdateValues();
	}

	// Token: 0x0600537D RID: 21373 RVA: 0x001E47CC File Offset: 0x001E29CC
	public void UpdateValues()
	{
		this.text.color = (this.fill.color = this.gradient.Evaluate(this.slider.value));
		for (int i = 0; i < this.gradient.colorKeys.Length; i++)
		{
			if (this.gradient.colorKeys[i].time < this.slider.value)
			{
				this.text.text = this.names[i];
			}
			if (i == this.gradient.colorKeys.Length - 1 && this.gradient.colorKeys[i - 1].time < this.slider.value)
			{
				this.text.text = this.names[i];
			}
		}
	}

	// Token: 0x04003891 RID: 14481
	public Image icon;

	// Token: 0x04003892 RID: 14482
	public Text text;

	// Token: 0x04003893 RID: 14483
	public Slider slider;

	// Token: 0x04003894 RID: 14484
	public Image fill;

	// Token: 0x04003895 RID: 14485
	public Gradient gradient;

	// Token: 0x04003896 RID: 14486
	public string[] names;
}
