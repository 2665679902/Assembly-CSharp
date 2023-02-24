using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005B8 RID: 1464
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StatInternal : IDisposable
	{
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06003BCF RID: 15311 RVA: 0x00083B58 File Offset: 0x00081D58
		// (set) Token: 0x06003BD0 RID: 15312 RVA: 0x00083B7A File Offset: 0x00081D7A
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

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x00083B8C File Offset: 0x00081D8C
		// (set) Token: 0x06003BD2 RID: 15314 RVA: 0x00083BAE File Offset: 0x00081DAE
		public string Name
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Name, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Name, value);
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x00083BC0 File Offset: 0x00081DC0
		// (set) Token: 0x06003BD4 RID: 15316 RVA: 0x00083BE2 File Offset: 0x00081DE2
		public DateTimeOffset? StartTime
		{
			get
			{
				DateTimeOffset? @default = Helper.GetDefault<DateTimeOffset?>();
				Helper.TryMarshalGet(this.m_StartTime, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_StartTime, value);
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x00083BF4 File Offset: 0x00081DF4
		// (set) Token: 0x06003BD6 RID: 15318 RVA: 0x00083C16 File Offset: 0x00081E16
		public DateTimeOffset? EndTime
		{
			get
			{
				DateTimeOffset? @default = Helper.GetDefault<DateTimeOffset?>();
				Helper.TryMarshalGet(this.m_EndTime, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_EndTime, value);
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x00083C28 File Offset: 0x00081E28
		// (set) Token: 0x06003BD8 RID: 15320 RVA: 0x00083C4A File Offset: 0x00081E4A
		public int Value
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_Value, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_Value, value);
			}
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x00083C59 File Offset: 0x00081E59
		public void Dispose()
		{
		}

		// Token: 0x040016D0 RID: 5840
		private int m_ApiVersion;

		// Token: 0x040016D1 RID: 5841
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Name;

		// Token: 0x040016D2 RID: 5842
		private long m_StartTime;

		// Token: 0x040016D3 RID: 5843
		private long m_EndTime;

		// Token: 0x040016D4 RID: 5844
		private int m_Value;
	}
}
