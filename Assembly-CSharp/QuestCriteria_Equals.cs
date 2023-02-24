using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008A8 RID: 2216
public class QuestCriteria_Equals : QuestCriteria
{
	// Token: 0x06003FD8 RID: 16344 RVA: 0x001651A7 File Offset: 0x001633A7
	public QuestCriteria_Equals(Tag id, float[] targetValues, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
		: base(id, targetValues, requiredCount, acceptedTags, flags)
	{
	}

	// Token: 0x06003FD9 RID: 16345 RVA: 0x001651B6 File Offset: 0x001633B6
	protected override bool ValueSatisfies_Internal(float current, float target)
	{
		return Mathf.Abs(target - current) <= Mathf.Epsilon;
	}
}
