using System;

// Token: 0x02000B97 RID: 2967
public interface ISidescreenButtonControl
{
	// Token: 0x17000682 RID: 1666
	// (get) Token: 0x06005D52 RID: 23890
	string SidescreenButtonText { get; }

	// Token: 0x17000683 RID: 1667
	// (get) Token: 0x06005D53 RID: 23891
	string SidescreenButtonTooltip { get; }

	// Token: 0x06005D54 RID: 23892
	void SetButtonTextOverride(ButtonMenuTextOverride textOverride);

	// Token: 0x06005D55 RID: 23893
	bool SidescreenEnabled();

	// Token: 0x06005D56 RID: 23894
	bool SidescreenButtonInteractable();

	// Token: 0x06005D57 RID: 23895
	void OnSidescreenButtonPressed();

	// Token: 0x06005D58 RID: 23896
	int ButtonSideScreenSortOrder();
}
