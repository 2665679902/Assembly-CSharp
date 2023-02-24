using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005E9 RID: 1513
	public class JoinSessionAcceptedCallbackInfo
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x00084EEF File Offset: 0x000830EF
		// (set) Token: 0x06003CE9 RID: 15593 RVA: 0x00084EF7 File Offset: 0x000830F7
		public object ClientData { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06003CEA RID: 15594 RVA: 0x00084F00 File Offset: 0x00083100
		// (set) Token: 0x06003CEB RID: 15595 RVA: 0x00084F08 File Offset: 0x00083108
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x00084F11 File Offset: 0x00083111
		// (set) Token: 0x06003CED RID: 15597 RVA: 0x00084F19 File Offset: 0x00083119
		public ulong UiEventId { get; set; }
	}
}
