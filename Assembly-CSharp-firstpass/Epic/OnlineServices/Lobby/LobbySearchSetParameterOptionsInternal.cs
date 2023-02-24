using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000795 RID: 1941
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetParameterOptionsInternal : IDisposable
	{
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06004763 RID: 18275 RVA: 0x0008FE10 File Offset: 0x0008E010
		// (set) Token: 0x06004764 RID: 18276 RVA: 0x0008FE32 File Offset: 0x0008E032
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

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06004765 RID: 18277 RVA: 0x0008FE44 File Offset: 0x0008E044
		// (set) Token: 0x06004766 RID: 18278 RVA: 0x0008FE66 File Offset: 0x0008E066
		public AttributeDataInternal? Parameter
		{
			get
			{
				AttributeDataInternal? @default = Helper.GetDefault<AttributeDataInternal?>();
				Helper.TryMarshalGet<AttributeDataInternal>(this.m_Parameter, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal>(ref this.m_Parameter, value);
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06004767 RID: 18279 RVA: 0x0008FE78 File Offset: 0x0008E078
		// (set) Token: 0x06004768 RID: 18280 RVA: 0x0008FE9A File Offset: 0x0008E09A
		public ComparisonOp ComparisonOp
		{
			get
			{
				ComparisonOp @default = Helper.GetDefault<ComparisonOp>();
				Helper.TryMarshalGet<ComparisonOp>(this.m_ComparisonOp, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ComparisonOp>(ref this.m_ComparisonOp, value);
			}
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x0008FEA9 File Offset: 0x0008E0A9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Parameter);
		}

		// Token: 0x04001BAE RID: 7086
		private int m_ApiVersion;

		// Token: 0x04001BAF RID: 7087
		private IntPtr m_Parameter;

		// Token: 0x04001BB0 RID: 7088
		private ComparisonOp m_ComparisonOp;
	}
}
