using System;
using System.Collections.Generic;

// Token: 0x0200094D RID: 2381
public class LaunchPadConditions : KMonoBehaviour, IProcessConditionSet
{
	// Token: 0x0600465F RID: 18015 RVA: 0x0018C8B0 File Offset: 0x0018AAB0
	public List<ProcessCondition> GetConditionSet(ProcessCondition.ProcessConditionType conditionType)
	{
		if (conditionType != ProcessCondition.ProcessConditionType.RocketStorage)
		{
			return null;
		}
		return this.conditions;
	}

	// Token: 0x06004660 RID: 18016 RVA: 0x0018C8BE File Offset: 0x0018AABE
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.conditions = new List<ProcessCondition>();
		this.conditions.Add(new TransferCargoCompleteCondition(base.gameObject));
	}

	// Token: 0x04002E93 RID: 11923
	private List<ProcessCondition> conditions;
}
