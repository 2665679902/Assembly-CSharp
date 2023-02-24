using System;
using System.Collections.Generic;

// Token: 0x020008AA RID: 2218
public class QuestCriteria_LessThan : QuestCriteria
{
	// Token: 0x06003FDC RID: 16348 RVA: 0x001651DF File Offset: 0x001633DF
	public QuestCriteria_LessThan(Tag id, float[] targetValues, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
		: base(id, targetValues, requiredCount, acceptedTags, flags)
	{
	}

	// Token: 0x06003FDD RID: 16349 RVA: 0x001651EE File Offset: 0x001633EE
	protected override bool ValueSatisfies_Internal(float current, float target)
	{
		return current < target;
	}
}
