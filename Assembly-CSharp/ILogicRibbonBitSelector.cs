using System;

// Token: 0x02000BC2 RID: 3010
public interface ILogicRibbonBitSelector
{
	// Token: 0x06005E9B RID: 24219
	void SetBitSelection(int bit);

	// Token: 0x06005E9C RID: 24220
	int GetBitSelection();

	// Token: 0x06005E9D RID: 24221
	int GetBitDepth();

	// Token: 0x17000694 RID: 1684
	// (get) Token: 0x06005E9E RID: 24222
	string SideScreenTitle { get; }

	// Token: 0x17000695 RID: 1685
	// (get) Token: 0x06005E9F RID: 24223
	string SideScreenDescription { get; }

	// Token: 0x06005EA0 RID: 24224
	bool SideScreenDisplayWriterDescription();

	// Token: 0x06005EA1 RID: 24225
	bool SideScreenDisplayReaderDescription();

	// Token: 0x06005EA2 RID: 24226
	bool IsBitActive(int bit);

	// Token: 0x06005EA3 RID: 24227
	int GetOutputValue();

	// Token: 0x06005EA4 RID: 24228
	int GetInputValue();

	// Token: 0x06005EA5 RID: 24229
	void UpdateVisuals();
}
