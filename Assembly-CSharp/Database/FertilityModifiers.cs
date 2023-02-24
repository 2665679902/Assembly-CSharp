using System;
using System.Collections.Generic;
using Klei.AI;

namespace Database
{
	// Token: 0x02000C70 RID: 3184
	public class FertilityModifiers : ResourceSet<FertilityModifier>
	{
		// Token: 0x0600650A RID: 25866 RVA: 0x0025B62C File Offset: 0x0025982C
		public List<FertilityModifier> GetForTag(Tag searchTag)
		{
			List<FertilityModifier> list = new List<FertilityModifier>();
			foreach (FertilityModifier fertilityModifier in this.resources)
			{
				if (fertilityModifier.TargetTag == searchTag)
				{
					list.Add(fertilityModifier);
				}
			}
			return list;
		}
	}
}
