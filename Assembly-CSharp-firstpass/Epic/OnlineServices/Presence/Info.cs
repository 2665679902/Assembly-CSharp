using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000672 RID: 1650
	public class Info
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06003FF2 RID: 16370 RVA: 0x00087EC3 File Offset: 0x000860C3
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x00087EC6 File Offset: 0x000860C6
		// (set) Token: 0x06003FF4 RID: 16372 RVA: 0x00087ECE File Offset: 0x000860CE
		public Status Status { get; set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06003FF5 RID: 16373 RVA: 0x00087ED7 File Offset: 0x000860D7
		// (set) Token: 0x06003FF6 RID: 16374 RVA: 0x00087EDF File Offset: 0x000860DF
		public EpicAccountId UserId { get; set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x00087EE8 File Offset: 0x000860E8
		// (set) Token: 0x06003FF8 RID: 16376 RVA: 0x00087EF0 File Offset: 0x000860F0
		public string ProductId { get; set; }

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06003FF9 RID: 16377 RVA: 0x00087EF9 File Offset: 0x000860F9
		// (set) Token: 0x06003FFA RID: 16378 RVA: 0x00087F01 File Offset: 0x00086101
		public string ProductVersion { get; set; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x00087F0A File Offset: 0x0008610A
		// (set) Token: 0x06003FFC RID: 16380 RVA: 0x00087F12 File Offset: 0x00086112
		public string Platform { get; set; }

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06003FFD RID: 16381 RVA: 0x00087F1B File Offset: 0x0008611B
		// (set) Token: 0x06003FFE RID: 16382 RVA: 0x00087F23 File Offset: 0x00086123
		public string RichText { get; set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06003FFF RID: 16383 RVA: 0x00087F2C File Offset: 0x0008612C
		// (set) Token: 0x06004000 RID: 16384 RVA: 0x00087F34 File Offset: 0x00086134
		public DataRecord[] Records { get; set; }

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06004001 RID: 16385 RVA: 0x00087F3D File Offset: 0x0008613D
		// (set) Token: 0x06004002 RID: 16386 RVA: 0x00087F45 File Offset: 0x00086145
		public string ProductName { get; set; }
	}
}
