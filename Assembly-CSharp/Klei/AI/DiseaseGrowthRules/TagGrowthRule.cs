using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02000DA6 RID: 3494
	public class TagGrowthRule : GrowthRule
	{
		// Token: 0x06006A66 RID: 27238 RVA: 0x00295079 File Offset: 0x00293279
		public TagGrowthRule(Tag tag)
		{
			this.tag = tag;
		}

		// Token: 0x06006A67 RID: 27239 RVA: 0x00295088 File Offset: 0x00293288
		public override bool Test(Element e)
		{
			return e.HasTag(this.tag);
		}

		// Token: 0x06006A68 RID: 27240 RVA: 0x00295096 File Offset: 0x00293296
		public override string Name()
		{
			return this.tag.ProperName();
		}

		// Token: 0x04004FED RID: 20461
		public Tag tag;
	}
}
