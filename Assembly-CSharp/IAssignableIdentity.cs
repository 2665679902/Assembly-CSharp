using System;
using System.Collections.Generic;

// Token: 0x02000485 RID: 1157
public interface IAssignableIdentity
{
	// Token: 0x060019D3 RID: 6611
	string GetProperName();

	// Token: 0x060019D4 RID: 6612
	List<Ownables> GetOwners();

	// Token: 0x060019D5 RID: 6613
	Ownables GetSoleOwner();

	// Token: 0x060019D6 RID: 6614
	bool IsNull();

	// Token: 0x060019D7 RID: 6615
	bool HasOwner(Assignables owner);

	// Token: 0x060019D8 RID: 6616
	int NumOwners();
}
