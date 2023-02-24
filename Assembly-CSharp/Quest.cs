using System;

// Token: 0x020008A5 RID: 2213
public class Quest : Resource
{
	// Token: 0x06003FB0 RID: 16304 RVA: 0x00164498 File Offset: 0x00162698
	public Quest(string id, QuestCriteria[] criteria)
		: base(id, id)
	{
		Debug.Assert(criteria.Length != 0);
		this.Criteria = criteria;
		string text = "STRINGS.CODEX.QUESTS." + id.ToUpperInvariant();
		StringEntry stringEntry;
		if (Strings.TryGet(text + ".NAME", out stringEntry))
		{
			this.Title = stringEntry.String;
		}
		if (Strings.TryGet(text + ".COMPLETE", out stringEntry))
		{
			this.CompletionText = stringEntry.String;
		}
		for (int i = 0; i < this.Criteria.Length; i++)
		{
			this.Criteria[i].PopulateStrings("STRINGS.CODEX.QUESTS.");
		}
	}

	// Token: 0x040029CF RID: 10703
	public const string STRINGS_PREFIX = "STRINGS.CODEX.QUESTS.";

	// Token: 0x040029D0 RID: 10704
	public readonly QuestCriteria[] Criteria;

	// Token: 0x040029D1 RID: 10705
	public readonly string Title;

	// Token: 0x040029D2 RID: 10706
	public readonly string CompletionText;

	// Token: 0x0200167B RID: 5755
	public struct ItemData
	{
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060087D7 RID: 34775 RVA: 0x002F3FD6 File Offset: 0x002F21D6
		// (set) Token: 0x060087D8 RID: 34776 RVA: 0x002F3FE0 File Offset: 0x002F21E0
		public int ValueHandle
		{
			get
			{
				return this.valueHandle - 1;
			}
			set
			{
				this.valueHandle = value + 1;
			}
		}

		// Token: 0x040069ED RID: 27117
		public int LocalCellId;

		// Token: 0x040069EE RID: 27118
		public float CurrentValue;

		// Token: 0x040069EF RID: 27119
		public Tag SatisfyingItem;

		// Token: 0x040069F0 RID: 27120
		public Tag QualifyingTag;

		// Token: 0x040069F1 RID: 27121
		public HashedString CriteriaId;

		// Token: 0x040069F2 RID: 27122
		private int valueHandle;
	}

	// Token: 0x0200167C RID: 5756
	public enum State
	{
		// Token: 0x040069F4 RID: 27124
		NotStarted,
		// Token: 0x040069F5 RID: 27125
		InProgress,
		// Token: 0x040069F6 RID: 27126
		Completed
	}
}
