using System;

namespace Klei.AI
{
	// Token: 0x02000D9F RID: 3487
	public class TraitGroup : ModifierGroup<Trait>
	{
		// Token: 0x06006A2E RID: 27182 RVA: 0x00293CC9 File Offset: 0x00291EC9
		public TraitGroup(string id, string name, bool is_spawn_trait)
			: base(id, name)
		{
			this.IsSpawnTrait = is_spawn_trait;
		}

		// Token: 0x04004FC0 RID: 20416
		public bool IsSpawnTrait;
	}
}
