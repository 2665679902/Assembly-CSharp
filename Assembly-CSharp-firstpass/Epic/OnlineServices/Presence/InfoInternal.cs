using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000673 RID: 1651
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InfoInternal : IDisposable
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x00087F58 File Offset: 0x00086158
		// (set) Token: 0x06004005 RID: 16389 RVA: 0x00087F7A File Offset: 0x0008617A
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

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x00087F8C File Offset: 0x0008618C
		// (set) Token: 0x06004007 RID: 16391 RVA: 0x00087FAE File Offset: 0x000861AE
		public Status Status
		{
			get
			{
				Status @default = Helper.GetDefault<Status>();
				Helper.TryMarshalGet<Status>(this.m_Status, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<Status>(ref this.m_Status, value);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x00087FC0 File Offset: 0x000861C0
		// (set) Token: 0x06004009 RID: 16393 RVA: 0x00087FE2 File Offset: 0x000861E2
		public EpicAccountId UserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_UserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x00087FF4 File Offset: 0x000861F4
		// (set) Token: 0x0600400B RID: 16395 RVA: 0x00088016 File Offset: 0x00086216
		public string ProductId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ProductId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ProductId, value);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x00088028 File Offset: 0x00086228
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x0008804A File Offset: 0x0008624A
		public string ProductVersion
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ProductVersion, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ProductVersion, value);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x0008805C File Offset: 0x0008625C
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x0008807E File Offset: 0x0008627E
		public string Platform
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Platform, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Platform, value);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x00088090 File Offset: 0x00086290
		// (set) Token: 0x06004011 RID: 16401 RVA: 0x000880B2 File Offset: 0x000862B2
		public string RichText
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_RichText, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_RichText, value);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x000880C4 File Offset: 0x000862C4
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x000880EC File Offset: 0x000862EC
		public DataRecordInternal[] Records
		{
			get
			{
				DataRecordInternal[] @default = Helper.GetDefault<DataRecordInternal[]>();
				Helper.TryMarshalGet<DataRecordInternal>(this.m_Records, out @default, this.m_RecordsCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<DataRecordInternal>(ref this.m_Records, value, out this.m_RecordsCount);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x00088104 File Offset: 0x00086304
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x00088126 File Offset: 0x00086326
		public string ProductName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ProductName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ProductName, value);
			}
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x00088135 File Offset: 0x00086335
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Records);
		}

		// Token: 0x04001870 RID: 6256
		private int m_ApiVersion;

		// Token: 0x04001871 RID: 6257
		private Status m_Status;

		// Token: 0x04001872 RID: 6258
		private IntPtr m_UserId;

		// Token: 0x04001873 RID: 6259
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ProductId;

		// Token: 0x04001874 RID: 6260
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ProductVersion;

		// Token: 0x04001875 RID: 6261
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Platform;

		// Token: 0x04001876 RID: 6262
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_RichText;

		// Token: 0x04001877 RID: 6263
		private int m_RecordsCount;

		// Token: 0x04001878 RID: 6264
		private IntPtr m_Records;

		// Token: 0x04001879 RID: 6265
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ProductName;
	}
}
