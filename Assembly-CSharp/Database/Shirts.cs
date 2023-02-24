using System;

namespace Database
{
	// Token: 0x02000CAF RID: 3247
	public class Shirts : ResourceSet<Shirt>
	{
		// Token: 0x060065E0 RID: 26080 RVA: 0x00270D28 File Offset: 0x0026EF28
		public Shirts()
		{
			this.Hot00 = base.Add(new Shirt("body_shirt_hot01"));
			this.Hot01 = base.Add(new Shirt("body_shirt_hot02"));
			this.Decor00 = base.Add(new Shirt("body_shirt_decor01"));
			this.Cold00 = base.Add(new Shirt("body_shirt_cold01"));
			this.Cold01 = base.Add(new Shirt("body_shirt_cold02"));
		}

		// Token: 0x04004A0F RID: 18959
		public Shirt Hot00;

		// Token: 0x04004A10 RID: 18960
		public Shirt Hot01;

		// Token: 0x04004A11 RID: 18961
		public Shirt Decor00;

		// Token: 0x04004A12 RID: 18962
		public Shirt Cold00;

		// Token: 0x04004A13 RID: 18963
		public Shirt Cold01;
	}
}
