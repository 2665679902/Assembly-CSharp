using System;
using System.Collections.Generic;

// Token: 0x020008AC RID: 2220
public class QuestCriteria_LessOrEqual : QuestCriteria
{
	// Token: 0x06003FE0 RID: 16352 RVA: 0x0016520C File Offset: 0x0016340C
	public QuestCriteria_LessOrEqual(Tag id, float[] targetValues, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
		: base(id, targetValues, requiredCount, acceptedTags, flags)
	{
	}

	// Token: 0x06003FE1 RID: 16353 RVA: 0x0016521B File Offset: 0x0016341B
	protected override bool ValueSatisfies_Internal(float current, float target)
	{
		return current <= target;
	}
}
