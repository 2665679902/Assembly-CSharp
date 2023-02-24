using System;
using System.Collections.Generic;

// Token: 0x020008F2 RID: 2290
public class RoleSlotUnlock
{
	// Token: 0x170004B2 RID: 1202
	// (get) Token: 0x06004221 RID: 16929 RVA: 0x001742D9 File Offset: 0x001724D9
	// (set) Token: 0x06004222 RID: 16930 RVA: 0x001742E1 File Offset: 0x001724E1
	public string id { get; protected set; }

	// Token: 0x170004B3 RID: 1203
	// (get) Token: 0x06004223 RID: 16931 RVA: 0x001742EA File Offset: 0x001724EA
	// (set) Token: 0x06004224 RID: 16932 RVA: 0x001742F2 File Offset: 0x001724F2
	public string name { get; protected set; }

	// Token: 0x170004B4 RID: 1204
	// (get) Token: 0x06004225 RID: 16933 RVA: 0x001742FB File Offset: 0x001724FB
	// (set) Token: 0x06004226 RID: 16934 RVA: 0x00174303 File Offset: 0x00172503
	public string description { get; protected set; }

	// Token: 0x170004B5 RID: 1205
	// (get) Token: 0x06004227 RID: 16935 RVA: 0x0017430C File Offset: 0x0017250C
	// (set) Token: 0x06004228 RID: 16936 RVA: 0x00174314 File Offset: 0x00172514
	public List<global::Tuple<string, int>> slots { get; protected set; }

	// Token: 0x170004B6 RID: 1206
	// (get) Token: 0x06004229 RID: 16937 RVA: 0x0017431D File Offset: 0x0017251D
	// (set) Token: 0x0600422A RID: 16938 RVA: 0x00174325 File Offset: 0x00172525
	public Func<bool> isSatisfied { get; protected set; }

	// Token: 0x0600422B RID: 16939 RVA: 0x0017432E File Offset: 0x0017252E
	public RoleSlotUnlock(string id, string name, string description, List<global::Tuple<string, int>> slots, Func<bool> isSatisfied)
	{
		this.id = id;
		this.name = name;
		this.description = description;
		this.slots = slots;
		this.isSatisfied = isSatisfied;
	}
}
