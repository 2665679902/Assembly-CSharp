using System;

// Token: 0x02000B8D RID: 2957
public interface IActivationRangeTarget
{
	// Token: 0x17000677 RID: 1655
	// (get) Token: 0x06005CE2 RID: 23778
	// (set) Token: 0x06005CE3 RID: 23779
	float ActivateValue { get; set; }

	// Token: 0x17000678 RID: 1656
	// (get) Token: 0x06005CE4 RID: 23780
	// (set) Token: 0x06005CE5 RID: 23781
	float DeactivateValue { get; set; }

	// Token: 0x17000679 RID: 1657
	// (get) Token: 0x06005CE6 RID: 23782
	float MinValue { get; }

	// Token: 0x1700067A RID: 1658
	// (get) Token: 0x06005CE7 RID: 23783
	float MaxValue { get; }

	// Token: 0x1700067B RID: 1659
	// (get) Token: 0x06005CE8 RID: 23784
	bool UseWholeNumbers { get; }

	// Token: 0x1700067C RID: 1660
	// (get) Token: 0x06005CE9 RID: 23785
	string ActivationRangeTitleText { get; }

	// Token: 0x1700067D RID: 1661
	// (get) Token: 0x06005CEA RID: 23786
	string ActivateSliderLabelText { get; }

	// Token: 0x1700067E RID: 1662
	// (get) Token: 0x06005CEB RID: 23787
	string DeactivateSliderLabelText { get; }

	// Token: 0x1700067F RID: 1663
	// (get) Token: 0x06005CEC RID: 23788
	string ActivateTooltip { get; }

	// Token: 0x17000680 RID: 1664
	// (get) Token: 0x06005CED RID: 23789
	string DeactivateTooltip { get; }
}
