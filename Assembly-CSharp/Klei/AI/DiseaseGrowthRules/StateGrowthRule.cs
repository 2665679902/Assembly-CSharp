using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02000DA4 RID: 3492
	public class StateGrowthRule : GrowthRule
	{
		// Token: 0x06006A60 RID: 27232 RVA: 0x0029501E File Offset: 0x0029321E
		public StateGrowthRule(Element.State state)
		{
			this.state = state;
		}

		// Token: 0x06006A61 RID: 27233 RVA: 0x0029502D File Offset: 0x0029322D
		public override bool Test(Element e)
		{
			return e.IsState(this.state);
		}

		// Token: 0x06006A62 RID: 27234 RVA: 0x0029503B File Offset: 0x0029323B
		public override string Name()
		{
			return Element.GetStateString(this.state);
		}

		// Token: 0x04004FEB RID: 20459
		public Element.State state;
	}
}
