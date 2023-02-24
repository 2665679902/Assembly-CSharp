using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200093A RID: 2362
	public class OnQueryDefinitionsCompleteCallbackInfo
	{
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060051F8 RID: 20984 RVA: 0x00099F7E File Offset: 0x0009817E
		// (set) Token: 0x060051F9 RID: 20985 RVA: 0x00099F86 File Offset: 0x00098186
		public Result ResultCode { get; set; }

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060051FA RID: 20986 RVA: 0x00099F8F File Offset: 0x0009818F
		// (set) Token: 0x060051FB RID: 20987 RVA: 0x00099F97 File Offset: 0x00098197
		public object ClientData { get; set; }
	}
}
