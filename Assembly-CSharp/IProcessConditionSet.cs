using System;
using System.Collections.Generic;

// Token: 0x02000BA5 RID: 2981
public interface IProcessConditionSet
{
	// Token: 0x06005DD5 RID: 24021
	List<ProcessCondition> GetConditionSet(ProcessCondition.ProcessConditionType conditionType);
}
