using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000A59 RID: 2649
public class SubEntry
{
	// Token: 0x0600509B RID: 20635 RVA: 0x001CEFBC File Offset: 0x001CD1BC
	public SubEntry()
	{
	}

	// Token: 0x0600509C RID: 20636 RVA: 0x001CEFD0 File Offset: 0x001CD1D0
	public SubEntry(string id, string parentEntryID, List<ContentContainer> contentContainers, string name)
	{
		this.id = id;
		this.parentEntryID = parentEntryID;
		this.name = name;
		this.contentContainers = contentContainers;
		if (!string.IsNullOrEmpty(this.lockID))
		{
			foreach (ContentContainer contentContainer in contentContainers)
			{
				contentContainer.lockID = this.lockID;
			}
		}
		if (string.IsNullOrEmpty(this.sortString))
		{
			if (!string.IsNullOrEmpty(this.title))
			{
				this.sortString = UI.StripLinkFormatting(this.title);
				return;
			}
			this.sortString = UI.StripLinkFormatting(name);
		}
	}

	// Token: 0x170005D7 RID: 1495
	// (get) Token: 0x0600509D RID: 20637 RVA: 0x001CF098 File Offset: 0x001CD298
	// (set) Token: 0x0600509E RID: 20638 RVA: 0x001CF0A0 File Offset: 0x001CD2A0
	public List<ContentContainer> contentContainers { get; set; }

	// Token: 0x170005D8 RID: 1496
	// (get) Token: 0x0600509F RID: 20639 RVA: 0x001CF0A9 File Offset: 0x001CD2A9
	// (set) Token: 0x060050A0 RID: 20640 RVA: 0x001CF0B1 File Offset: 0x001CD2B1
	public string parentEntryID { get; set; }

	// Token: 0x170005D9 RID: 1497
	// (get) Token: 0x060050A1 RID: 20641 RVA: 0x001CF0BA File Offset: 0x001CD2BA
	// (set) Token: 0x060050A2 RID: 20642 RVA: 0x001CF0C2 File Offset: 0x001CD2C2
	public string id { get; set; }

	// Token: 0x170005DA RID: 1498
	// (get) Token: 0x060050A3 RID: 20643 RVA: 0x001CF0CB File Offset: 0x001CD2CB
	// (set) Token: 0x060050A4 RID: 20644 RVA: 0x001CF0D3 File Offset: 0x001CD2D3
	public string name { get; set; }

	// Token: 0x170005DB RID: 1499
	// (get) Token: 0x060050A5 RID: 20645 RVA: 0x001CF0DC File Offset: 0x001CD2DC
	// (set) Token: 0x060050A6 RID: 20646 RVA: 0x001CF0E4 File Offset: 0x001CD2E4
	public string title { get; set; }

	// Token: 0x170005DC RID: 1500
	// (get) Token: 0x060050A7 RID: 20647 RVA: 0x001CF0ED File Offset: 0x001CD2ED
	// (set) Token: 0x060050A8 RID: 20648 RVA: 0x001CF0F5 File Offset: 0x001CD2F5
	public string subtitle { get; set; }

	// Token: 0x170005DD RID: 1501
	// (get) Token: 0x060050A9 RID: 20649 RVA: 0x001CF0FE File Offset: 0x001CD2FE
	// (set) Token: 0x060050AA RID: 20650 RVA: 0x001CF106 File Offset: 0x001CD306
	public Sprite icon { get; set; }

	// Token: 0x170005DE RID: 1502
	// (get) Token: 0x060050AB RID: 20651 RVA: 0x001CF10F File Offset: 0x001CD30F
	// (set) Token: 0x060050AC RID: 20652 RVA: 0x001CF117 File Offset: 0x001CD317
	public int layoutPriority { get; set; }

	// Token: 0x170005DF RID: 1503
	// (get) Token: 0x060050AD RID: 20653 RVA: 0x001CF120 File Offset: 0x001CD320
	// (set) Token: 0x060050AE RID: 20654 RVA: 0x001CF128 File Offset: 0x001CD328
	public bool disabled { get; set; }

	// Token: 0x170005E0 RID: 1504
	// (get) Token: 0x060050AF RID: 20655 RVA: 0x001CF131 File Offset: 0x001CD331
	// (set) Token: 0x060050B0 RID: 20656 RVA: 0x001CF139 File Offset: 0x001CD339
	public string lockID { get; set; }

	// Token: 0x170005E1 RID: 1505
	// (get) Token: 0x060050B1 RID: 20657 RVA: 0x001CF142 File Offset: 0x001CD342
	// (set) Token: 0x060050B2 RID: 20658 RVA: 0x001CF14A File Offset: 0x001CD34A
	public string[] dlcIds { get; set; }

	// Token: 0x170005E2 RID: 1506
	// (get) Token: 0x060050B3 RID: 20659 RVA: 0x001CF153 File Offset: 0x001CD353
	// (set) Token: 0x060050B4 RID: 20660 RVA: 0x001CF15B File Offset: 0x001CD35B
	public string[] forbiddenDLCIds { get; set; }

	// Token: 0x060050B5 RID: 20661 RVA: 0x001CF164 File Offset: 0x001CD364
	public string[] GetDlcIds()
	{
		return this.dlcIds;
	}

	// Token: 0x060050B6 RID: 20662 RVA: 0x001CF16C File Offset: 0x001CD36C
	public string[] GetForbiddenDlCIds()
	{
		return this.forbiddenDLCIds;
	}

	// Token: 0x170005E3 RID: 1507
	// (get) Token: 0x060050B7 RID: 20663 RVA: 0x001CF174 File Offset: 0x001CD374
	// (set) Token: 0x060050B8 RID: 20664 RVA: 0x001CF17C File Offset: 0x001CD37C
	public string sortString { get; set; }

	// Token: 0x170005E4 RID: 1508
	// (get) Token: 0x060050B9 RID: 20665 RVA: 0x001CF185 File Offset: 0x001CD385
	// (set) Token: 0x060050BA RID: 20666 RVA: 0x001CF18D File Offset: 0x001CD38D
	public bool showBeforeGeneratedCategoryLinks { get; set; }

	// Token: 0x04003635 RID: 13877
	public ContentContainer lockedContentContainer;

	// Token: 0x0400363C RID: 13884
	public Color iconColor = Color.white;
}
