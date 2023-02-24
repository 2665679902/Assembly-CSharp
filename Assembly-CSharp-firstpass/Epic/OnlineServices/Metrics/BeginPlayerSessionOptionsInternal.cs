using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x0200070E RID: 1806
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BeginPlayerSessionOptionsInternal : IDisposable
	{
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06004437 RID: 17463 RVA: 0x0008C280 File Offset: 0x0008A480
		// (set) Token: 0x06004438 RID: 17464 RVA: 0x0008C2A2 File Offset: 0x0008A4A2
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

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06004439 RID: 17465 RVA: 0x0008C2B4 File Offset: 0x0008A4B4
		// (set) Token: 0x0600443A RID: 17466 RVA: 0x0008C2D6 File Offset: 0x0008A4D6
		public BeginPlayerSessionOptionsAccountIdInternal AccountId
		{
			get
			{
				BeginPlayerSessionOptionsAccountIdInternal @default = Helper.GetDefault<BeginPlayerSessionOptionsAccountIdInternal>();
				Helper.TryMarshalGet<BeginPlayerSessionOptionsAccountIdInternal>(this.m_AccountId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<BeginPlayerSessionOptionsAccountIdInternal>(ref this.m_AccountId, value);
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600443B RID: 17467 RVA: 0x0008C2E8 File Offset: 0x0008A4E8
		// (set) Token: 0x0600443C RID: 17468 RVA: 0x0008C30A File Offset: 0x0008A50A
		public string DisplayName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DisplayName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x0008C31C File Offset: 0x0008A51C
		// (set) Token: 0x0600443E RID: 17470 RVA: 0x0008C33E File Offset: 0x0008A53E
		public UserControllerType ControllerType
		{
			get
			{
				UserControllerType @default = Helper.GetDefault<UserControllerType>();
				Helper.TryMarshalGet<UserControllerType>(this.m_ControllerType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<UserControllerType>(ref this.m_ControllerType, value);
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600443F RID: 17471 RVA: 0x0008C350 File Offset: 0x0008A550
		// (set) Token: 0x06004440 RID: 17472 RVA: 0x0008C372 File Offset: 0x0008A572
		public string ServerIp
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ServerIp, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ServerIp, value);
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06004441 RID: 17473 RVA: 0x0008C384 File Offset: 0x0008A584
		// (set) Token: 0x06004442 RID: 17474 RVA: 0x0008C3A6 File Offset: 0x0008A5A6
		public string GameSessionId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_GameSessionId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_GameSessionId, value);
			}
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x0008C3B5 File Offset: 0x0008A5B5
		public void Dispose()
		{
			Helper.TryMarshalDispose<BeginPlayerSessionOptionsAccountIdInternal>(ref this.m_AccountId);
		}

		// Token: 0x04001A36 RID: 6710
		private int m_ApiVersion;

		// Token: 0x04001A37 RID: 6711
		private BeginPlayerSessionOptionsAccountIdInternal m_AccountId;

		// Token: 0x04001A38 RID: 6712
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DisplayName;

		// Token: 0x04001A39 RID: 6713
		private UserControllerType m_ControllerType;

		// Token: 0x04001A3A RID: 6714
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ServerIp;

		// Token: 0x04001A3B RID: 6715
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_GameSessionId;
	}
}
