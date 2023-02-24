using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x0200050B RID: 1291
[DebuggerDisplay("{IdHash}")]
public class ChoreType : Resource
{
	// Token: 0x17000161 RID: 353
	// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x000A54B0 File Offset: 0x000A36B0
	// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x000A54B8 File Offset: 0x000A36B8
	public Urge urge { get; private set; }

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x000A54C1 File Offset: 0x000A36C1
	// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x000A54C9 File Offset: 0x000A36C9
	public ChoreGroup[] groups { get; private set; }

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x000A54D2 File Offset: 0x000A36D2
	// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x000A54DA File Offset: 0x000A36DA
	public int priority { get; private set; }

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000A54E3 File Offset: 0x000A36E3
	// (set) Token: 0x06001EEA RID: 7914 RVA: 0x000A54EB File Offset: 0x000A36EB
	public int interruptPriority { get; set; }

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x06001EEB RID: 7915 RVA: 0x000A54F4 File Offset: 0x000A36F4
	// (set) Token: 0x06001EEC RID: 7916 RVA: 0x000A54FC File Offset: 0x000A36FC
	public int explicitPriority { get; private set; }

	// Token: 0x06001EED RID: 7917 RVA: 0x000A5505 File Offset: 0x000A3705
	private string ResolveStringCallback(string str, object data)
	{
		return ((Chore)data).ResolveString(str);
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x000A5514 File Offset: 0x000A3714
	public ChoreType(string id, ResourceSet parent, string[] chore_groups, string urge, string name, string status_message, string tooltip, IEnumerable<Tag> interrupt_exclusion, int implicit_priority, int explicit_priority)
		: base(id, parent, name)
	{
		this.statusItem = new StatusItem(id, status_message, tooltip, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022, true, null);
		this.statusItem.resolveStringCallback = new Func<string, object, string>(this.ResolveStringCallback);
		this.tags.Add(TagManager.Create(id));
		this.interruptExclusion = new HashSet<Tag>(interrupt_exclusion);
		Db.Get().DuplicantStatusItems.Add(this.statusItem);
		List<ChoreGroup> list = new List<ChoreGroup>();
		for (int i = 0; i < chore_groups.Length; i++)
		{
			ChoreGroup choreGroup = Db.Get().ChoreGroups.TryGet(chore_groups[i]);
			if (choreGroup != null)
			{
				if (!choreGroup.choreTypes.Contains(this))
				{
					choreGroup.choreTypes.Add(this);
				}
				list.Add(choreGroup);
			}
		}
		this.groups = list.ToArray();
		if (!string.IsNullOrEmpty(urge))
		{
			this.urge = Db.Get().Urges.Get(urge);
		}
		this.priority = implicit_priority;
		this.explicitPriority = explicit_priority;
	}

	// Token: 0x04001180 RID: 4480
	public StatusItem statusItem;

	// Token: 0x04001185 RID: 4485
	public HashSet<Tag> tags = new HashSet<Tag>();

	// Token: 0x04001186 RID: 4486
	public HashSet<Tag> interruptExclusion;

	// Token: 0x04001188 RID: 4488
	public string reportName;
}
