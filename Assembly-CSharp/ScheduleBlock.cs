using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000909 RID: 2313
[Serializable]
public class ScheduleBlock
{
	// Token: 0x170004C9 RID: 1225
	// (get) Token: 0x06004362 RID: 17250 RVA: 0x0017CF0C File Offset: 0x0017B10C
	// (set) Token: 0x06004361 RID: 17249 RVA: 0x0017CF03 File Offset: 0x0017B103
	public string GroupId
	{
		get
		{
			if (this._groupId == null)
			{
				this._groupId = Db.Get().ScheduleGroups.FindGroupForScheduleTypes(this.allowed_types).Id;
			}
			return this._groupId;
		}
		set
		{
			this._groupId = value;
		}
	}

	// Token: 0x06004363 RID: 17251 RVA: 0x0017CF3C File Offset: 0x0017B13C
	public ScheduleBlock(string name, List<ScheduleBlockType> allowed_types, string groupId)
	{
		this.name = name;
		this.allowed_types = allowed_types;
		this._groupId = groupId;
	}

	// Token: 0x06004364 RID: 17252 RVA: 0x0017CF5C File Offset: 0x0017B15C
	public bool IsAllowed(ScheduleBlockType type)
	{
		if (this.allowed_types != null)
		{
			foreach (ScheduleBlockType scheduleBlockType in this.allowed_types)
			{
				if (type.IdHash == scheduleBlockType.IdHash)
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x04002CF1 RID: 11505
	[Serialize]
	public string name;

	// Token: 0x04002CF2 RID: 11506
	[Serialize]
	public List<ScheduleBlockType> allowed_types;

	// Token: 0x04002CF3 RID: 11507
	[Serialize]
	private string _groupId;
}
