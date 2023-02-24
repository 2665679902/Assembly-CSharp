using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006CF RID: 1743
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WriteFileDataCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600424A RID: 16970 RVA: 0x0008A25C File Offset: 0x0008845C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x0008A27E File Offset: 0x0008847E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x0008A288 File Offset: 0x00088488
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x0008A2AC File Offset: 0x000884AC
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600424E RID: 16974 RVA: 0x0008A2D0 File Offset: 0x000884D0
		public uint DataBufferLengthBytes
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_DataBufferLengthBytes, out @default);
				return @default;
			}
		}

		// Token: 0x0400195D RID: 6493
		private IntPtr m_ClientData;

		// Token: 0x0400195E RID: 6494
		private IntPtr m_LocalUserId;

		// Token: 0x0400195F RID: 6495
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;

		// Token: 0x04001960 RID: 6496
		private uint m_DataBufferLengthBytes;
	}
}
