using System;
using System.Collections.Generic;

// Token: 0x020008AB RID: 2219
public class QuestCriteria_GreaterOrEqual : QuestCriteria
{
	// Token: 0x06003FDE RID: 16350 RVA: 0x001651F4 File Offset: 0x001633F4
	public QuestCriteria_GreaterOrEqual(Tag id, float[] targetValues, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
		: base(id, targetValues, requiredCount, acceptedTags, flags)
	{
	}

	// Token: 0x06003FDF RID: 16351 RVA: 0x00165203 File Offset: 0x00163403
	protected override bool ValueSatisfies_Internal(float current, float target)
	{
		return current >= target;
	}
}
