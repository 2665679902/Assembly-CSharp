using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007B3 RID: 1971
public interface IStorage
{
	// Token: 0x060037C4 RID: 14276
	bool ShouldShowInUI();

	// Token: 0x17000416 RID: 1046
	// (get) Token: 0x060037C5 RID: 14277
	// (set) Token: 0x060037C6 RID: 14278
	bool allowUIItemRemoval { get; set; }

	// Token: 0x060037C7 RID: 14279
	GameObject Drop(GameObject go, bool do_disease_transfer = true);

	// Token: 0x060037C8 RID: 14280
	List<GameObject> GetItems();

	// Token: 0x060037C9 RID: 14281
	bool IsFull();

	// Token: 0x060037CA RID: 14282
	bool IsEmpty();

	// Token: 0x060037CB RID: 14283
	float Capacity();

	// Token: 0x060037CC RID: 14284
	float RemainingCapacity();

	// Token: 0x060037CD RID: 14285
	float GetAmountAvailable(Tag tag);

	// Token: 0x060037CE RID: 14286
	void ConsumeIgnoringDisease(Tag tag, float amount);
}
