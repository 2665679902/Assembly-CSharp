using System;

namespace ProcGen
{
	// Token: 0x020004E3 RID: 1251
	[Serializable]
	public class MobSettings : IMerge<MobSettings>
	{
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060035E3 RID: 13795 RVA: 0x0007644F File Offset: 0x0007464F
		// (set) Token: 0x060035E4 RID: 13796 RVA: 0x00076457 File Offset: 0x00074657
		public ComposableDictionary<string, Mob> MobLookupTable { get; private set; }

		// Token: 0x060035E5 RID: 13797 RVA: 0x00076460 File Offset: 0x00074660
		public MobSettings()
		{
			this.MobLookupTable = new ComposableDictionary<string, Mob>();
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x00076473 File Offset: 0x00074673
		public bool HasMob(string id)
		{
			return this.MobLookupTable.ContainsKey(id);
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x00076484 File Offset: 0x00074684
		public Mob GetMob(string id)
		{
			Mob mob = null;
			this.MobLookupTable.TryGetValue(id, out mob);
			return mob;
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000764A4 File Offset: 0x000746A4
		public TagSet GetMobTags()
		{
			if (this.mobkeys == null)
			{
				this.mobkeys = new TagSet();
				foreach (string text in this.MobLookupTable.Keys)
				{
					this.mobkeys.Add(new Tag(text));
				}
			}
			return this.mobkeys;
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x0007651C File Offset: 0x0007471C
		public MobSettings Merge(MobSettings other)
		{
			this.MobLookupTable.Merge(other.MobLookupTable);
			this.mobkeys = null;
			return this;
		}

		// Token: 0x040012F8 RID: 4856
		public static float AmbientMobDensity = 1f;

		// Token: 0x040012FA RID: 4858
		private TagSet mobkeys;
	}
}
