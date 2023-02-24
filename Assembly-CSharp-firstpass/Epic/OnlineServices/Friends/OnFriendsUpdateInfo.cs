using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000810 RID: 2064
	public class OnFriendsUpdateInfo
	{
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060049FF RID: 18943 RVA: 0x00092077 File Offset: 0x00090277
		// (set) Token: 0x06004A00 RID: 18944 RVA: 0x0009207F File Offset: 0x0009027F
		public object ClientData { get; set; }

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06004A01 RID: 18945 RVA: 0x00092088 File Offset: 0x00090288
		// (set) Token: 0x06004A02 RID: 18946 RVA: 0x00092090 File Offset: 0x00090290
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06004A03 RID: 18947 RVA: 0x00092099 File Offset: 0x00090299
		// (set) Token: 0x06004A04 RID: 18948 RVA: 0x000920A1 File Offset: 0x000902A1
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06004A05 RID: 18949 RVA: 0x000920AA File Offset: 0x000902AA
		// (set) Token: 0x06004A06 RID: 18950 RVA: 0x000920B2 File Offset: 0x000902B2
		public FriendsStatus PreviousStatus { get; set; }

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06004A07 RID: 18951 RVA: 0x000920BB File Offset: 0x000902BB
		// (set) Token: 0x06004A08 RID: 18952 RVA: 0x000920C3 File Offset: 0x000902C3
		public FriendsStatus CurrentStatus { get; set; }
	}
}
