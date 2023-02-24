using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020006D8 RID: 1752
	public class Options
	{
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06004294 RID: 17044 RVA: 0x0008A6FB File Offset: 0x000888FB
		public int ApiVersion
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x0008A6FE File Offset: 0x000888FE
		// (set) Token: 0x06004296 RID: 17046 RVA: 0x0008A706 File Offset: 0x00088906
		public IntPtr Reserved { get; set; }

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06004297 RID: 17047 RVA: 0x0008A70F File Offset: 0x0008890F
		// (set) Token: 0x06004298 RID: 17048 RVA: 0x0008A717 File Offset: 0x00088917
		public string ProductId { get; set; }

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06004299 RID: 17049 RVA: 0x0008A720 File Offset: 0x00088920
		// (set) Token: 0x0600429A RID: 17050 RVA: 0x0008A728 File Offset: 0x00088928
		public string SandboxId { get; set; }

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x0008A731 File Offset: 0x00088931
		// (set) Token: 0x0600429C RID: 17052 RVA: 0x0008A739 File Offset: 0x00088939
		public ClientCredentials ClientCredentials { get; set; }

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600429D RID: 17053 RVA: 0x0008A742 File Offset: 0x00088942
		// (set) Token: 0x0600429E RID: 17054 RVA: 0x0008A74A File Offset: 0x0008894A
		public bool IsServer { get; set; }

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x0008A753 File Offset: 0x00088953
		// (set) Token: 0x060042A0 RID: 17056 RVA: 0x0008A75B File Offset: 0x0008895B
		public string EncryptionKey { get; set; }

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x0008A764 File Offset: 0x00088964
		// (set) Token: 0x060042A2 RID: 17058 RVA: 0x0008A76C File Offset: 0x0008896C
		public string OverrideCountryCode { get; set; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060042A3 RID: 17059 RVA: 0x0008A775 File Offset: 0x00088975
		// (set) Token: 0x060042A4 RID: 17060 RVA: 0x0008A77D File Offset: 0x0008897D
		public string OverrideLocaleCode { get; set; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x0008A786 File Offset: 0x00088986
		// (set) Token: 0x060042A6 RID: 17062 RVA: 0x0008A78E File Offset: 0x0008898E
		public string DeploymentId { get; set; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x0008A797 File Offset: 0x00088997
		// (set) Token: 0x060042A8 RID: 17064 RVA: 0x0008A79F File Offset: 0x0008899F
		public PlatformFlags Flags { get; set; }

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x0008A7A8 File Offset: 0x000889A8
		// (set) Token: 0x060042AA RID: 17066 RVA: 0x0008A7B0 File Offset: 0x000889B0
		public string CacheDirectory { get; set; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060042AB RID: 17067 RVA: 0x0008A7B9 File Offset: 0x000889B9
		// (set) Token: 0x060042AC RID: 17068 RVA: 0x0008A7C1 File Offset: 0x000889C1
		public uint TickBudgetInMilliseconds { get; set; }
	}
}
