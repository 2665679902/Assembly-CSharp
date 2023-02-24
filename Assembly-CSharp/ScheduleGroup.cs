using System;
using System.Collections.Generic;
using System.Diagnostics;
using STRINGS;

// Token: 0x02000510 RID: 1296
[DebuggerDisplay("{Id}")]
public class ScheduleGroup : Resource
{
	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06001F19 RID: 7961 RVA: 0x000A5AA7 File Offset: 0x000A3CA7
	// (set) Token: 0x06001F1A RID: 7962 RVA: 0x000A5AAF File Offset: 0x000A3CAF
	public int defaultSegments { get; private set; }

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x06001F1B RID: 7963 RVA: 0x000A5AB8 File Offset: 0x000A3CB8
	// (set) Token: 0x06001F1C RID: 7964 RVA: 0x000A5AC0 File Offset: 0x000A3CC0
	public string description { get; private set; }

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x06001F1D RID: 7965 RVA: 0x000A5AC9 File Offset: 0x000A3CC9
	// (set) Token: 0x06001F1E RID: 7966 RVA: 0x000A5AD1 File Offset: 0x000A3CD1
	public string notificationTooltip { get; private set; }

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06001F1F RID: 7967 RVA: 0x000A5ADA File Offset: 0x000A3CDA
	// (set) Token: 0x06001F20 RID: 7968 RVA: 0x000A5AE2 File Offset: 0x000A3CE2
	public List<ScheduleBlockType> allowedTypes { get; private set; }

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06001F21 RID: 7969 RVA: 0x000A5AEB File Offset: 0x000A3CEB
	// (set) Token: 0x06001F22 RID: 7970 RVA: 0x000A5AF3 File Offset: 0x000A3CF3
	public bool alarm { get; private set; }

	// Token: 0x06001F23 RID: 7971 RVA: 0x000A5AFC File Offset: 0x000A3CFC
	public ScheduleGroup(string id, ResourceSet parent, int defaultSegments, string name, string description, string notificationTooltip, List<ScheduleBlockType> allowedTypes, bool alarm = false)
		: base(id, parent, name)
	{
		this.defaultSegments = defaultSegments;
		this.description = description;
		this.notificationTooltip = notificationTooltip;
		this.allowedTypes = allowedTypes;
		this.alarm = alarm;
	}

	// Token: 0x06001F24 RID: 7972 RVA: 0x000A5B2F File Offset: 0x000A3D2F
	public bool Allowed(ScheduleBlockType type)
	{
		return this.allowedTypes.Contains(type);
	}

	// Token: 0x06001F25 RID: 7973 RVA: 0x000A5B3D File Offset: 0x000A3D3D
	public string GetTooltip()
	{
		return string.Format(UI.SCHEDULEGROUPS.TOOLTIP_FORMAT, this.Name, this.description);
	}
}
