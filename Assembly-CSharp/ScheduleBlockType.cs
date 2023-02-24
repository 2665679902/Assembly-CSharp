using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200050F RID: 1295
[DebuggerDisplay("{Id}")]
public class ScheduleBlockType : Resource
{
	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06001F14 RID: 7956 RVA: 0x000A5A6A File Offset: 0x000A3C6A
	// (set) Token: 0x06001F15 RID: 7957 RVA: 0x000A5A72 File Offset: 0x000A3C72
	public Color color { get; private set; }

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06001F16 RID: 7958 RVA: 0x000A5A7B File Offset: 0x000A3C7B
	// (set) Token: 0x06001F17 RID: 7959 RVA: 0x000A5A83 File Offset: 0x000A3C83
	public string description { get; private set; }

	// Token: 0x06001F18 RID: 7960 RVA: 0x000A5A8C File Offset: 0x000A3C8C
	public ScheduleBlockType(string id, ResourceSet parent, string name, string description, Color color)
		: base(id, parent, name)
	{
		this.color = color;
		this.description = description;
	}
}
