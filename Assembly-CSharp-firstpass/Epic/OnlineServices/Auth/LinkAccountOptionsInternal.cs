using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008F1 RID: 2289
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountOptionsInternal : IDisposable
	{
		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004FF5 RID: 20469 RVA: 0x00098040 File Offset: 0x00096240
		// (set) Token: 0x06004FF6 RID: 20470 RVA: 0x00098062 File Offset: 0x00096262
		public int ApiVersion
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ApiVersion, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ApiVersion, value);
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06004FF7 RID: 20471 RVA: 0x00098074 File Offset: 0x00096274
		// (set) Token: 0x06004FF8 RID: 20472 RVA: 0x00098096 File Offset: 0x00096296
		public LinkAccountFlags LinkAccountFlags
		{
			get
			{
				LinkAccountFlags @default = Helper.GetDefault<LinkAccountFlags>();
				Helper.TryMarshalGet<LinkAccountFlags>(this.m_LinkAccountFlags, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<LinkAccountFlags>(ref this.m_LinkAccountFlags, value);
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06004FF9 RID: 20473 RVA: 0x000980A8 File Offset: 0x000962A8
		// (set) Token: 0x06004FFA RID: 20474 RVA: 0x000980CA File Offset: 0x000962CA
		public ContinuanceToken ContinuanceToken
		{
			get
			{
				ContinuanceToken @default = Helper.GetDefault<ContinuanceToken>();
				Helper.TryMarshalGet<ContinuanceToken>(this.m_ContinuanceToken, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ContinuanceToken, value);
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06004FFB RID: 20475 RVA: 0x000980DC File Offset: 0x000962DC
		// (set) Token: 0x06004FFC RID: 20476 RVA: 0x000980FE File Offset: 0x000962FE
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x0009810D File Offset: 0x0009630D
		public void Dispose()
		{
		}

		// Token: 0x04001F0F RID: 7951
		private int m_ApiVersion;

		// Token: 0x04001F10 RID: 7952
		private LinkAccountFlags m_LinkAccountFlags;

		// Token: 0x04001F11 RID: 7953
		private IntPtr m_ContinuanceToken;

		// Token: 0x04001F12 RID: 7954
		private IntPtr m_LocalUserId;
	}
}
