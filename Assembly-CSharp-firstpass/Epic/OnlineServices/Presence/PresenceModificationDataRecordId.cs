using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000680 RID: 1664
	public class PresenceModificationDataRecordId
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x00088791 File Offset: 0x00086991
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x00088794 File Offset: 0x00086994
		// (set) Token: 0x06004074 RID: 16500 RVA: 0x0008879C File Offset: 0x0008699C
		public string Key { get; set; }
	}
}
