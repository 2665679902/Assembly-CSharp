using System;

namespace Klei.AI
{
	// Token: 0x02000D92 RID: 3474
	public class FertilityModifier : Resource
	{
		// Token: 0x060069CB RID: 27083 RVA: 0x00292778 File Offset: 0x00290978
		public FertilityModifier(string id, Tag targetTag, string name, string description, Func<string, string> tooltipCB, FertilityModifier.FertilityModFn applyFunction)
			: base(id, name)
		{
			this.Description = description;
			this.TargetTag = targetTag;
			this.TooltipCB = tooltipCB;
			this.ApplyFunction = applyFunction;
		}

		// Token: 0x060069CC RID: 27084 RVA: 0x002927A1 File Offset: 0x002909A1
		public string GetTooltip()
		{
			if (this.TooltipCB != null)
			{
				return this.TooltipCB(this.Description);
			}
			return this.Description;
		}

		// Token: 0x04004F9C RID: 20380
		public string Description;

		// Token: 0x04004F9D RID: 20381
		public Tag TargetTag;

		// Token: 0x04004F9E RID: 20382
		public Func<string, string> TooltipCB;

		// Token: 0x04004F9F RID: 20383
		public FertilityModifier.FertilityModFn ApplyFunction;

		// Token: 0x02001E6E RID: 7790
		// (Invoke) Token: 0x06009BB9 RID: 39865
		public delegate void FertilityModFn(FertilityMonitor.Instance inst, Tag eggTag);
	}
}
