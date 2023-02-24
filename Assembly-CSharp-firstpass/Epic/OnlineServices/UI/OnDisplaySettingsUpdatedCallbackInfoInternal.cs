using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200056A RID: 1386
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnDisplaySettingsUpdatedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x00081DA8 File Offset: 0x0007FFA8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060039D7 RID: 14807 RVA: 0x00081DCA File Offset: 0x0007FFCA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x00081DD4 File Offset: 0x0007FFD4
		public bool IsVisible
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_IsVisible, out @default);
				return @default;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060039D9 RID: 14809 RVA: 0x00081DF8 File Offset: 0x0007FFF8
		public bool IsExclusiveInput
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_IsExclusiveInput, out @default);
				return @default;
			}
		}

		// Token: 0x0400160D RID: 5645
		private IntPtr m_ClientData;

		// Token: 0x0400160E RID: 5646
		private int m_IsVisible;

		// Token: 0x0400160F RID: 5647
		private int m_IsExclusiveInput;
	}
}
