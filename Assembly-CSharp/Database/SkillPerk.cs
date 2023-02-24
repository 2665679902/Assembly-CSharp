using System;

namespace Database
{
	// Token: 0x02000CFB RID: 3323
	public class SkillPerk : Resource
	{
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06006714 RID: 26388 RVA: 0x0027A321 File Offset: 0x00278521
		// (set) Token: 0x06006715 RID: 26389 RVA: 0x0027A329 File Offset: 0x00278529
		public Action<MinionResume> OnApply { get; protected set; }

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06006716 RID: 26390 RVA: 0x0027A332 File Offset: 0x00278532
		// (set) Token: 0x06006717 RID: 26391 RVA: 0x0027A33A File Offset: 0x0027853A
		public Action<MinionResume> OnRemove { get; protected set; }

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06006718 RID: 26392 RVA: 0x0027A343 File Offset: 0x00278543
		// (set) Token: 0x06006719 RID: 26393 RVA: 0x0027A34B File Offset: 0x0027854B
		public Action<MinionResume> OnMinionsChanged { get; protected set; }

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600671A RID: 26394 RVA: 0x0027A354 File Offset: 0x00278554
		// (set) Token: 0x0600671B RID: 26395 RVA: 0x0027A35C File Offset: 0x0027855C
		public bool affectAll { get; protected set; }

		// Token: 0x0600671C RID: 26396 RVA: 0x0027A365 File Offset: 0x00278565
		public SkillPerk(string id_str, string description, Action<MinionResume> OnApply, Action<MinionResume> OnRemove, Action<MinionResume> OnMinionsChanged, bool affectAll = false)
			: base(id_str, description)
		{
			this.OnApply = OnApply;
			this.OnRemove = OnRemove;
			this.OnMinionsChanged = OnMinionsChanged;
			this.affectAll = affectAll;
		}
	}
}
