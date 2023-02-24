using System;

// Token: 0x02000BE2 RID: 3042
public interface ISliderControl
{
	// Token: 0x170006AB RID: 1707
	// (get) Token: 0x06005FD6 RID: 24534
	string SliderTitleKey { get; }

	// Token: 0x170006AC RID: 1708
	// (get) Token: 0x06005FD7 RID: 24535
	string SliderUnits { get; }

	// Token: 0x06005FD8 RID: 24536
	int SliderDecimalPlaces(int index);

	// Token: 0x06005FD9 RID: 24537
	float GetSliderMin(int index);

	// Token: 0x06005FDA RID: 24538
	float GetSliderMax(int index);

	// Token: 0x06005FDB RID: 24539
	float GetSliderValue(int index);

	// Token: 0x06005FDC RID: 24540
	void SetSliderValue(float percent, int index);

	// Token: 0x06005FDD RID: 24541
	string GetSliderTooltipKey(int index);

	// Token: 0x06005FDE RID: 24542
	string GetSliderTooltip();
}
