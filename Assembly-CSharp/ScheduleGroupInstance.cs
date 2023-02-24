using System;
using KSerialization;

// Token: 0x02000511 RID: 1297
[SerializationConfig(MemberSerialization.OptIn)]
public class ScheduleGroupInstance
{
	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06001F26 RID: 7974 RVA: 0x000A5B5A File Offset: 0x000A3D5A
	// (set) Token: 0x06001F27 RID: 7975 RVA: 0x000A5B71 File Offset: 0x000A3D71
	public ScheduleGroup scheduleGroup
	{
		get
		{
			return Db.Get().ScheduleGroups.Get(this.scheduleGroupID);
		}
		set
		{
			this.scheduleGroupID = value.Id;
		}
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x000A5B7F File Offset: 0x000A3D7F
	public ScheduleGroupInstance(ScheduleGroup scheduleGroup)
	{
		this.scheduleGroup = scheduleGroup;
		this.segments = scheduleGroup.defaultSegments;
	}

	// Token: 0x040011AC RID: 4524
	[Serialize]
	private string scheduleGroupID;

	// Token: 0x040011AD RID: 4525
	[Serialize]
	public int segments;
}
