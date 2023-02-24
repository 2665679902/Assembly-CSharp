using System;

// Token: 0x020006AC RID: 1708
internal class TaskDivision<Task, SharedData> where Task : DivisibleTask<SharedData>, new()
{
	// Token: 0x06002E5B RID: 11867 RVA: 0x000F46AC File Offset: 0x000F28AC
	public TaskDivision(int taskCount)
	{
		this.tasks = new Task[taskCount];
		for (int num = 0; num != this.tasks.Length; num++)
		{
			this.tasks[num] = new Task();
		}
	}

	// Token: 0x06002E5C RID: 11868 RVA: 0x000F46EF File Offset: 0x000F28EF
	public TaskDivision()
		: this(CPUBudget.coreCount)
	{
	}

	// Token: 0x06002E5D RID: 11869 RVA: 0x000F46FC File Offset: 0x000F28FC
	public void Initialize(int count)
	{
		int num = count / this.tasks.Length;
		for (int num2 = 0; num2 != this.tasks.Length; num2++)
		{
			this.tasks[num2].start = num2 * num;
			this.tasks[num2].end = this.tasks[num2].start + num;
		}
		DebugUtil.Assert(this.tasks[this.tasks.Length - 1].end + count % this.tasks.Length == count);
		this.tasks[this.tasks.Length - 1].end = count;
	}

	// Token: 0x06002E5E RID: 11870 RVA: 0x000F47C0 File Offset: 0x000F29C0
	public void Run(SharedData sharedData)
	{
		Task[] array = this.tasks;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Run(sharedData);
		}
	}

	// Token: 0x04001C00 RID: 7168
	public Task[] tasks;
}
