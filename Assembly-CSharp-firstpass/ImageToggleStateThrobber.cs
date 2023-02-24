using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
[AddComponentMenu("KMonoBehaviour/Plugins/ImageToggleStateThrobber")]
public class ImageToggleStateThrobber : KMonoBehaviour
{
	// Token: 0x0600034D RID: 845 RVA: 0x000118D7 File Offset: 0x0000FAD7
	public void OnEnable()
	{
		this.t = 0f;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x000118E4 File Offset: 0x0000FAE4
	public void OnDisable()
	{
		ImageToggleState[] array = this.targetImageToggleStates;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].ResetColor();
		}
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00011910 File Offset: 0x0000FB10
	public void Update()
	{
		float num = (this.useScaledTime ? Time.deltaTime : Time.unscaledDeltaTime);
		this.t = (this.t + num) % this.period;
		float num2 = Mathf.Cos(this.t / this.period * 2f * 3.1415927f) * 0.5f + 0.5f;
		foreach (ImageToggleState imageToggleState in this.targetImageToggleStates)
		{
			Color color = this.ColorForState(imageToggleState, this.state1);
			Color color2 = this.ColorForState(imageToggleState, this.state2);
			Color color3 = Color.Lerp(color, color2, num2);
			imageToggleState.TargetImage.color = color3;
		}
	}

	// Token: 0x06000350 RID: 848 RVA: 0x000119C2 File Offset: 0x0000FBC2
	private Color ColorForState(ImageToggleState its, ImageToggleState.State state)
	{
		switch (state)
		{
		case ImageToggleState.State.Disabled:
			return its.DisabledColour;
		case ImageToggleState.State.Inactive:
			return its.InactiveColour;
		default:
			return its.ActiveColour;
		case ImageToggleState.State.DisabledActive:
			return its.DisabledActiveColour;
		}
	}

	// Token: 0x040003FE RID: 1022
	public ImageToggleState[] targetImageToggleStates;

	// Token: 0x040003FF RID: 1023
	public ImageToggleState.State state1;

	// Token: 0x04000400 RID: 1024
	public ImageToggleState.State state2;

	// Token: 0x04000401 RID: 1025
	public float period = 2f;

	// Token: 0x04000402 RID: 1026
	public bool useScaledTime;

	// Token: 0x04000403 RID: 1027
	private float t;
}
