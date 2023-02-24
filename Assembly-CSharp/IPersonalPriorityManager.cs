using System;

// Token: 0x02000B04 RID: 2820
public interface IPersonalPriorityManager
{
	// Token: 0x06005689 RID: 22153
	int GetAssociatedSkillLevel(ChoreGroup group);

	// Token: 0x0600568A RID: 22154
	int GetPersonalPriority(ChoreGroup group);

	// Token: 0x0600568B RID: 22155
	void SetPersonalPriority(ChoreGroup group, int value);

	// Token: 0x0600568C RID: 22156
	bool IsChoreGroupDisabled(ChoreGroup group);

	// Token: 0x0600568D RID: 22157
	void ResetPersonalPriorities();
}
