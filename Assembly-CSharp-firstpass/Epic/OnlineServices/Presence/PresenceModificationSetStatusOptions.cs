using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200068A RID: 1674
	public class PresenceModificationSetStatusOptions
	{
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x0600409F RID: 16543 RVA: 0x00088A57 File Offset: 0x00086C57
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060040A0 RID: 16544 RVA: 0x00088A5A File Offset: 0x00086C5A
		// (set) Token: 0x060040A1 RID: 16545 RVA: 0x00088A62 File Offset: 0x00086C62
		public Status Status { get; set; }
	}
}
