using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200064F RID: 1615
	public class SessionSearchSetParameterOptions
	{
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06003F02 RID: 16130 RVA: 0x00086AEB File Offset: 0x00084CEB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x00086AEE File Offset: 0x00084CEE
		// (set) Token: 0x06003F04 RID: 16132 RVA: 0x00086AF6 File Offset: 0x00084CF6
		public AttributeData Parameter { get; set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x00086AFF File Offset: 0x00084CFF
		// (set) Token: 0x06003F06 RID: 16134 RVA: 0x00086B07 File Offset: 0x00084D07
		public ComparisonOp ComparisonOp { get; set; }
	}
}
