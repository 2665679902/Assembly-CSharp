using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000687 RID: 1671
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetJoinInfoOptionsInternal : IDisposable
	{
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x0008896C File Offset: 0x00086B6C
		// (set) Token: 0x06004092 RID: 16530 RVA: 0x0008898E File Offset: 0x00086B8E
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

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06004093 RID: 16531 RVA: 0x000889A0 File Offset: 0x00086BA0
		// (set) Token: 0x06004094 RID: 16532 RVA: 0x000889C2 File Offset: 0x00086BC2
		public string JoinInfo
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_JoinInfo, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_JoinInfo, value);
			}
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x000889D1 File Offset: 0x00086BD1
		public void Dispose()
		{
		}

		// Token: 0x040018AF RID: 6319
		private int m_ApiVersion;

		// Token: 0x040018B0 RID: 6320
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_JoinInfo;
	}
}
