using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000684 RID: 1668
	public class PresenceModificationSetDataOptions
	{
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x000888B3 File Offset: 0x00086AB3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06004085 RID: 16517 RVA: 0x000888B6 File Offset: 0x00086AB6
		// (set) Token: 0x06004086 RID: 16518 RVA: 0x000888BE File Offset: 0x00086ABE
		public DataRecord[] Records { get; set; }
	}
}
