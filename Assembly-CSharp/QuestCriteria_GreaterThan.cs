using System;
using System.Collections.Generic;

// Token: 0x020008A9 RID: 2217
public class QuestCriteria_GreaterThan : QuestCriteria
{
	// Token: 0x06003FDA RID: 16346 RVA: 0x001651CA File Offset: 0x001633CA
	public QuestCriteria_GreaterThan(Tag id, float[] targetValues, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
		: base(id, targetValues, requiredCount, acceptedTags, flags)
	{
	}

	// Token: 0x06003FDB RID: 16347 RVA: 0x001651D9 File Offset: 0x001633D9
	protected override bool ValueSatisfies_Internal(float current, float target)
	{
		return current > target;
	}
}
