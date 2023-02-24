using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200063A RID: 1594
	public class SessionModificationSetJoinInProgressAllowedOptions
	{
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06003E96 RID: 16022 RVA: 0x00086427 File Offset: 0x00084627
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06003E97 RID: 16023 RVA: 0x0008642A File Offset: 0x0008462A
		// (set) Token: 0x06003E98 RID: 16024 RVA: 0x00086432 File Offset: 0x00084632
		public bool AllowJoinInProgress { get; set; }
	}
}
