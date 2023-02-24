using System;

// Token: 0x020006AE RID: 1710
public interface IConduitFlow
{
	// Token: 0x06002E5F RID: 11871
	void AddConduitUpdater(Action<float> callback, ConduitFlowPriority priority = ConduitFlowPriority.Default);

	// Token: 0x06002E60 RID: 11872
	void RemoveConduitUpdater(Action<float> callback);

	// Token: 0x06002E61 RID: 11873
	bool IsConduitEmpty(int cell);
}
