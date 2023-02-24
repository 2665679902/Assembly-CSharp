using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006F9 RID: 1785
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryNATTypeCompleteInfoInternal : ICallbackInfo
	{
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x0008B674 File Offset: 0x00089874
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06004395 RID: 17301 RVA: 0x0008B698 File Offset: 0x00089898
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06004396 RID: 17302 RVA: 0x0008B6BA File Offset: 0x000898BA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06004397 RID: 17303 RVA: 0x0008B6C4 File Offset: 0x000898C4
		public NATType NATType
		{
			get
			{
				NATType @default = Helper.GetDefault<NATType>();
				Helper.TryMarshalGet<NATType>(this.m_NATType, out @default);
				return @default;
			}
		}

		// Token: 0x040019E8 RID: 6632
		private Result m_ResultCode;

		// Token: 0x040019E9 RID: 6633
		private IntPtr m_ClientData;

		// Token: 0x040019EA RID: 6634
		private NATType m_NATType;
	}
}
