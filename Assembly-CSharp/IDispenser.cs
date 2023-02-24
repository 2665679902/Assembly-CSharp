using System;
using System.Collections.Generic;

// Token: 0x02000BAD RID: 2989
public interface IDispenser
{
	// Token: 0x06005E08 RID: 24072
	List<Tag> DispensedItems();

	// Token: 0x06005E09 RID: 24073
	Tag SelectedItem();

	// Token: 0x06005E0A RID: 24074
	void SelectItem(Tag tag);

	// Token: 0x06005E0B RID: 24075
	void OnOrderDispense();

	// Token: 0x06005E0C RID: 24076
	void OnCancelDispense();

	// Token: 0x06005E0D RID: 24077
	bool HasOpenChore();

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x06005E0E RID: 24078
	// (remove) Token: 0x06005E0F RID: 24079
	event System.Action OnStopWorkEvent;
}
