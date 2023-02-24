using System;

// Token: 0x020000B4 RID: 180
public interface IComponentManager
{
	// Token: 0x060006BC RID: 1724
	void Spawn();

	// Token: 0x060006BD RID: 1725
	void RenderEveryTick(float dt);

	// Token: 0x060006BE RID: 1726
	void FixedUpdate(float dt);

	// Token: 0x060006BF RID: 1727
	void Sim200ms(float dt);

	// Token: 0x060006C0 RID: 1728
	void CleanUp();

	// Token: 0x060006C1 RID: 1729
	void Clear();

	// Token: 0x060006C2 RID: 1730
	bool Has(object go);

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060006C3 RID: 1731
	int Count { get; }

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x060006C4 RID: 1732
	string Name { get; }
}
