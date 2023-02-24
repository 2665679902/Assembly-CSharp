using System;

namespace Database
{
	// Token: 0x02000C90 RID: 3216
	public class Dreams : ResourceSet<Dream>
	{
		// Token: 0x06006579 RID: 25977 RVA: 0x00269445 File Offset: 0x00267645
		public Dreams(ResourceSet parent)
			: base("Dreams", parent)
		{
			this.CommonDream = new Dream("CommonDream", this, "dream_tear_swirly_kanim", new string[] { "dreamIcon_journal" });
		}

		// Token: 0x0400486D RID: 18541
		public Dream CommonDream;
	}
}
