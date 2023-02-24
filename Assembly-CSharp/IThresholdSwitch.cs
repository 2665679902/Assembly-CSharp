using System;

// Token: 0x02000652 RID: 1618
public interface IThresholdSwitch
{
	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06002B49 RID: 11081
	// (set) Token: 0x06002B4A RID: 11082
	float Threshold { get; set; }

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06002B4B RID: 11083
	// (set) Token: 0x06002B4C RID: 11084
	bool ActivateAboveThreshold { get; set; }

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06002B4D RID: 11085
	float CurrentValue { get; }

	// Token: 0x170002EF RID: 751
	// (get) Token: 0x06002B4E RID: 11086
	float RangeMin { get; }

	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06002B4F RID: 11087
	float RangeMax { get; }

	// Token: 0x06002B50 RID: 11088
	float GetRangeMinInputField();

	// Token: 0x06002B51 RID: 11089
	float GetRangeMaxInputField();

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x06002B52 RID: 11090
	LocString Title { get; }

	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x06002B53 RID: 11091
	LocString ThresholdValueName { get; }

	// Token: 0x06002B54 RID: 11092
	LocString ThresholdValueUnits();

	// Token: 0x06002B55 RID: 11093
	string Format(float value, bool units);

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x06002B56 RID: 11094
	string AboveToolTip { get; }

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06002B57 RID: 11095
	string BelowToolTip { get; }

	// Token: 0x06002B58 RID: 11096
	float ProcessedSliderValue(float input);

	// Token: 0x06002B59 RID: 11097
	float ProcessedInputValue(float input);

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06002B5A RID: 11098
	ThresholdScreenLayoutType LayoutType { get; }

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06002B5B RID: 11099
	int IncrementScale { get; }

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06002B5C RID: 11100
	NonLinearSlider.Range[] GetRanges { get; }
}
