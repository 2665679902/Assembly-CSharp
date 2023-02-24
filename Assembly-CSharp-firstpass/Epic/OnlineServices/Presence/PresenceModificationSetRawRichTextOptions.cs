using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000688 RID: 1672
	public class PresenceModificationSetRawRichTextOptions
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06004096 RID: 16534 RVA: 0x000889D3 File Offset: 0x00086BD3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06004097 RID: 16535 RVA: 0x000889D6 File Offset: 0x00086BD6
		// (set) Token: 0x06004098 RID: 16536 RVA: 0x000889DE File Offset: 0x00086BDE
		public string RichText { get; set; }
	}
}
