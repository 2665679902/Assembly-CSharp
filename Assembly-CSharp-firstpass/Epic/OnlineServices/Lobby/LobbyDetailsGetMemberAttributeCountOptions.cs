using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000763 RID: 1891
	public class LobbyDetailsGetMemberAttributeCountOptions
	{
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x0008E21B File Offset: 0x0008C41B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x0008E21E File Offset: 0x0008C41E
		// (set) Token: 0x06004603 RID: 17923 RVA: 0x0008E226 File Offset: 0x0008C426
		public ProductUserId TargetUserId { get; set; }
	}
}
