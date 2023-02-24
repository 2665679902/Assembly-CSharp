using System;

// Token: 0x02000BCD RID: 3021
public interface IPlayerControlledToggle
{
	// Token: 0x06005F04 RID: 24324
	void ToggledByPlayer();

	// Token: 0x06005F05 RID: 24325
	bool ToggledOn();

	// Token: 0x06005F06 RID: 24326
	KSelectable GetSelectable();

	// Token: 0x170006A5 RID: 1701
	// (get) Token: 0x06005F07 RID: 24327
	string SideScreenTitleKey { get; }

	// Token: 0x170006A6 RID: 1702
	// (get) Token: 0x06005F08 RID: 24328
	// (set) Token: 0x06005F09 RID: 24329
	bool ToggleRequested { get; set; }
}
