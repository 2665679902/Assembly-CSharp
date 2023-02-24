using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005D6 RID: 1494
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateSessionModificationOptionsInternal : IDisposable
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06003C82 RID: 15490 RVA: 0x00084868 File Offset: 0x00082A68
		// (set) Token: 0x06003C83 RID: 15491 RVA: 0x0008488A File Offset: 0x00082A8A
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

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x0008489C File Offset: 0x00082A9C
		// (set) Token: 0x06003C85 RID: 15493 RVA: 0x000848BE File Offset: 0x00082ABE
		public string SessionName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_SessionName, value);
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06003C86 RID: 15494 RVA: 0x000848D0 File Offset: 0x00082AD0
		// (set) Token: 0x06003C87 RID: 15495 RVA: 0x000848F2 File Offset: 0x00082AF2
		public string BucketId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_BucketId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_BucketId, value);
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06003C88 RID: 15496 RVA: 0x00084904 File Offset: 0x00082B04
		// (set) Token: 0x06003C89 RID: 15497 RVA: 0x00084926 File Offset: 0x00082B26
		public uint MaxPlayers
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_MaxPlayers, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_MaxPlayers, value);
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06003C8A RID: 15498 RVA: 0x00084938 File Offset: 0x00082B38
		// (set) Token: 0x06003C8B RID: 15499 RVA: 0x0008495A File Offset: 0x00082B5A
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06003C8C RID: 15500 RVA: 0x0008496C File Offset: 0x00082B6C
		// (set) Token: 0x06003C8D RID: 15501 RVA: 0x0008498E File Offset: 0x00082B8E
		public bool PresenceEnabled
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_PresenceEnabled, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_PresenceEnabled, value);
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06003C8E RID: 15502 RVA: 0x000849A0 File Offset: 0x00082BA0
		// (set) Token: 0x06003C8F RID: 15503 RVA: 0x000849C2 File Offset: 0x00082BC2
		public string SessionId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_SessionId, value);
			}
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x000849D1 File Offset: 0x00082BD1
		public void Dispose()
		{
		}

		// Token: 0x04001712 RID: 5906
		private int m_ApiVersion;

		// Token: 0x04001713 RID: 5907
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;

		// Token: 0x04001714 RID: 5908
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_BucketId;

		// Token: 0x04001715 RID: 5909
		private uint m_MaxPlayers;

		// Token: 0x04001716 RID: 5910
		private IntPtr m_LocalUserId;

		// Token: 0x04001717 RID: 5911
		private int m_PresenceEnabled;

		// Token: 0x04001718 RID: 5912
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionId;
	}
}
