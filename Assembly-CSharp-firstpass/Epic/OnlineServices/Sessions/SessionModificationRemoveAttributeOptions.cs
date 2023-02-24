using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000632 RID: 1586
	public class SessionModificationRemoveAttributeOptions
	{
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06003E72 RID: 15986 RVA: 0x00086217 File Offset: 0x00084417
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06003E73 RID: 15987 RVA: 0x0008621A File Offset: 0x0008441A
		// (set) Token: 0x06003E74 RID: 15988 RVA: 0x00086222 File Offset: 0x00084422
		public string Key { get; set; }
	}
}
