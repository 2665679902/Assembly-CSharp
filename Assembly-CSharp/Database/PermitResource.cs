using System;

namespace Database
{
	// Token: 0x02000CA4 RID: 3236
	public abstract class PermitResource : Resource
	{
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060065BE RID: 26046 RVA: 0x0026E5A6 File Offset: 0x0026C7A6
		public string PermitId
		{
			get
			{
				return this.Id;
			}
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x0026E5AE File Offset: 0x0026C7AE
		public PermitResource(string id, string Name, string Desc, PermitCategory permitCategory, PermitRarity rarity)
			: base(id, Name)
		{
			DebugUtil.DevAssert(Name != null, "Name must be provided.", null);
			DebugUtil.DevAssert(Desc != null, "Description must be provided.", null);
			this.Description = Desc;
			this.Category = permitCategory;
			this.Rarity = rarity;
		}

		// Token: 0x060065C0 RID: 26048
		public abstract PermitPresentationInfo GetPermitPresentationInfo();

		// Token: 0x060065C1 RID: 26049 RVA: 0x0026E5ED File Offset: 0x0026C7ED
		public bool IsOwnable()
		{
			return this.Rarity != PermitRarity.Universal;
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x0026E5FB File Offset: 0x0026C7FB
		public bool IsUnlocked()
		{
			return !this.IsOwnable() || PermitItems.IsPermitUnlocked(this);
		}

		// Token: 0x040049BB RID: 18875
		public string Description;

		// Token: 0x040049BC RID: 18876
		public PermitCategory Category;

		// Token: 0x040049BD RID: 18877
		public PermitRarity Rarity;
	}
}
