using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005A4 RID: 1444
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyStatByIndexOptionsInternal : IDisposable
	{
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x000833BC File Offset: 0x000815BC
		// (set) Token: 0x06003B46 RID: 15174 RVA: 0x000833DE File Offset: 0x000815DE
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

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000833F0 File Offset: 0x000815F0
		// (set) Token: 0x06003B48 RID: 15176 RVA: 0x00083412 File Offset: 0x00081612
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x00083424 File Offset: 0x00081624
		// (set) Token: 0x06003B4A RID: 15178 RVA: 0x00083446 File Offset: 0x00081646
		public uint StatIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_StatIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_StatIndex, value);
			}
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x00083455 File Offset: 0x00081655
		public void Dispose()
		{
		}

		// Token: 0x04001698 RID: 5784
		private int m_ApiVersion;

		// Token: 0x04001699 RID: 5785
		private IntPtr m_TargetUserId;

		// Token: 0x0400169A RID: 5786
		private uint m_StatIndex;
	}
}
