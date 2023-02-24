using System;

// Token: 0x02000BE0 RID: 3040
public interface ICheckboxControl
{
	// Token: 0x170006A8 RID: 1704
	// (get) Token: 0x06005FCA RID: 24522
	string CheckboxTitleKey { get; }

	// Token: 0x170006A9 RID: 1705
	// (get) Token: 0x06005FCB RID: 24523
	string CheckboxLabel { get; }

	// Token: 0x170006AA RID: 1706
	// (get) Token: 0x06005FCC RID: 24524
	string CheckboxTooltip { get; }

	// Token: 0x06005FCD RID: 24525
	bool GetCheckboxValue();

	// Token: 0x06005FCE RID: 24526
	void SetCheckboxValue(bool value);
}
