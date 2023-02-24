using System;
using System.Collections.Generic;
using System.Diagnostics;
using Klei.AI;

// Token: 0x0200050A RID: 1290
[DebuggerDisplay("{IdHash}")]
public class ChoreGroup : Resource
{
	// Token: 0x17000160 RID: 352
	// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x000A5444 File Offset: 0x000A3644
	public int DefaultPersonalPriority
	{
		get
		{
			return this.defaultPersonalPriority;
		}
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x000A544C File Offset: 0x000A364C
	public ChoreGroup(string id, string name, Klei.AI.Attribute attribute, string sprite, int default_personal_priority, bool user_prioritizable = true)
		: base(id, name)
	{
		this.attribute = attribute;
		this.description = Strings.Get("STRINGS.DUPLICANTS.CHOREGROUPS." + id.ToUpper() + ".DESC").String;
		this.sprite = sprite;
		this.defaultPersonalPriority = default_personal_priority;
		this.userPrioritizable = user_prioritizable;
	}

	// Token: 0x0400117A RID: 4474
	public List<ChoreType> choreTypes = new List<ChoreType>();

	// Token: 0x0400117B RID: 4475
	public Klei.AI.Attribute attribute;

	// Token: 0x0400117C RID: 4476
	public string description;

	// Token: 0x0400117D RID: 4477
	public string sprite;

	// Token: 0x0400117E RID: 4478
	private int defaultPersonalPriority;

	// Token: 0x0400117F RID: 4479
	public bool userPrioritizable;
}
