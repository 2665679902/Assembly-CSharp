using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006A5 RID: 1701
	public class FileTransferProgressCallbackInfo
	{
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x000893D3 File Offset: 0x000875D3
		// (set) Token: 0x06004143 RID: 16707 RVA: 0x000893DB File Offset: 0x000875DB
		public object ClientData { get; set; }

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x000893E4 File Offset: 0x000875E4
		// (set) Token: 0x06004145 RID: 16709 RVA: 0x000893EC File Offset: 0x000875EC
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x000893F5 File Offset: 0x000875F5
		// (set) Token: 0x06004147 RID: 16711 RVA: 0x000893FD File Offset: 0x000875FD
		public string Filename { get; set; }

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x00089406 File Offset: 0x00087606
		// (set) Token: 0x06004149 RID: 16713 RVA: 0x0008940E File Offset: 0x0008760E
		public uint BytesTransferred { get; set; }

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x0600414A RID: 16714 RVA: 0x00089417 File Offset: 0x00087617
		// (set) Token: 0x0600414B RID: 16715 RVA: 0x0008941F File Offset: 0x0008761F
		public uint TotalFileSizeBytes { get; set; }
	}
}
