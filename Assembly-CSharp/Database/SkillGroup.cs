using System;
using System.Collections.Generic;
using Klei.AI;

namespace Database
{
	// Token: 0x02000CFF RID: 3327
	public class SkillGroup : Resource, IListableOption
	{
		// Token: 0x06006723 RID: 26403 RVA: 0x0027B206 File Offset: 0x00279406
		string IListableOption.GetProperName()
		{
			return Strings.Get("STRINGS.DUPLICANTS.SKILLGROUPS." + this.Id.ToUpper() + ".NAME");
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x0027B22C File Offset: 0x0027942C
		public SkillGroup(string id, string choreGroupID, string name, string icon, string archetype_icon)
			: base(id, name)
		{
			this.choreGroupID = choreGroupID;
			this.choreGroupIcon = icon;
			this.archetypeIcon = archetype_icon;
		}

		// Token: 0x04004B84 RID: 19332
		public string choreGroupID;

		// Token: 0x04004B85 RID: 19333
		public List<Klei.AI.Attribute> relevantAttributes;

		// Token: 0x04004B86 RID: 19334
		public List<string> requiredChoreGroups;

		// Token: 0x04004B87 RID: 19335
		public string choreGroupIcon;

		// Token: 0x04004B88 RID: 19336
		public string archetypeIcon;
	}
}
