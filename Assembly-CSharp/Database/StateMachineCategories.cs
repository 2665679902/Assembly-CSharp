using System;

namespace Database
{
	// Token: 0x02000CB5 RID: 3253
	public class StateMachineCategories : ResourceSet<StateMachine.Category>
	{
		// Token: 0x060065FC RID: 26108 RVA: 0x0027225C File Offset: 0x0027045C
		public StateMachineCategories()
		{
			this.Ai = base.Add(new StateMachine.Category("Ai"));
			this.Monitor = base.Add(new StateMachine.Category("Monitor"));
			this.Chore = base.Add(new StateMachine.Category("Chore"));
			this.Misc = base.Add(new StateMachine.Category("Misc"));
		}

		// Token: 0x04004A4D RID: 19021
		public StateMachine.Category Ai;

		// Token: 0x04004A4E RID: 19022
		public StateMachine.Category Monitor;

		// Token: 0x04004A4F RID: 19023
		public StateMachine.Category Chore;

		// Token: 0x04004A50 RID: 19024
		public StateMachine.Category Misc;
	}
}
