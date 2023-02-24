using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007D9 RID: 2009
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardUserScoreByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060048AA RID: 18602 RVA: 0x00090A14 File Offset: 0x0008EC14
		// (set) Token: 0x060048AB RID: 18603 RVA: 0x00090A36 File Offset: 0x0008EC36
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

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060048AC RID: 18604 RVA: 0x00090A48 File Offset: 0x0008EC48
		// (set) Token: 0x060048AD RID: 18605 RVA: 0x00090A6A File Offset: 0x0008EC6A
		public uint LeaderboardUserScoreIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_LeaderboardUserScoreIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_LeaderboardUserScoreIndex, value);
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060048AE RID: 18606 RVA: 0x00090A7C File Offset: 0x0008EC7C
		// (set) Token: 0x060048AF RID: 18607 RVA: 0x00090A9E File Offset: 0x0008EC9E
		public string StatName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_StatName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_StatName, value);
			}
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x00090AAD File Offset: 0x0008ECAD
		public void Dispose()
		{
		}

		// Token: 0x04001C02 RID: 7170
		private int m_ApiVersion;

		// Token: 0x04001C03 RID: 7171
		private uint m_LeaderboardUserScoreIndex;

		// Token: 0x04001C04 RID: 7172
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_StatName;
	}
}
