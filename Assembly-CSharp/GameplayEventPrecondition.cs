using System;

// Token: 0x020003B4 RID: 948
public class GameplayEventPrecondition
{
	// Token: 0x04000AA4 RID: 2724
	public string description;

	// Token: 0x04000AA5 RID: 2725
	public GameplayEventPrecondition.PreconditionFn condition;

	// Token: 0x04000AA6 RID: 2726
	public bool required;

	// Token: 0x04000AA7 RID: 2727
	public int priorityModifier;

	// Token: 0x02000FC8 RID: 4040
	// (Invoke) Token: 0x06007078 RID: 28792
	public delegate bool PreconditionFn();
}
