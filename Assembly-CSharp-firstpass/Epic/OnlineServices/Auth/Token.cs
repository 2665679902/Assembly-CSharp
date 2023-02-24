using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200090B RID: 2315
	public class Token
	{
		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06005086 RID: 20614 RVA: 0x0009869F File Offset: 0x0009689F
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06005087 RID: 20615 RVA: 0x000986A2 File Offset: 0x000968A2
		// (set) Token: 0x06005088 RID: 20616 RVA: 0x000986AA File Offset: 0x000968AA
		public string App { get; set; }

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06005089 RID: 20617 RVA: 0x000986B3 File Offset: 0x000968B3
		// (set) Token: 0x0600508A RID: 20618 RVA: 0x000986BB File Offset: 0x000968BB
		public string ClientId { get; set; }

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x0600508B RID: 20619 RVA: 0x000986C4 File Offset: 0x000968C4
		// (set) Token: 0x0600508C RID: 20620 RVA: 0x000986CC File Offset: 0x000968CC
		public EpicAccountId AccountId { get; set; }

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x0600508D RID: 20621 RVA: 0x000986D5 File Offset: 0x000968D5
		// (set) Token: 0x0600508E RID: 20622 RVA: 0x000986DD File Offset: 0x000968DD
		public string AccessToken { get; set; }

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x0600508F RID: 20623 RVA: 0x000986E6 File Offset: 0x000968E6
		// (set) Token: 0x06005090 RID: 20624 RVA: 0x000986EE File Offset: 0x000968EE
		public double ExpiresIn { get; set; }

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06005091 RID: 20625 RVA: 0x000986F7 File Offset: 0x000968F7
		// (set) Token: 0x06005092 RID: 20626 RVA: 0x000986FF File Offset: 0x000968FF
		public string ExpiresAt { get; set; }

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06005093 RID: 20627 RVA: 0x00098708 File Offset: 0x00096908
		// (set) Token: 0x06005094 RID: 20628 RVA: 0x00098710 File Offset: 0x00096910
		public AuthTokenType AuthType { get; set; }

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06005095 RID: 20629 RVA: 0x00098719 File Offset: 0x00096919
		// (set) Token: 0x06005096 RID: 20630 RVA: 0x00098721 File Offset: 0x00096921
		public string RefreshToken { get; set; }

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06005097 RID: 20631 RVA: 0x0009872A File Offset: 0x0009692A
		// (set) Token: 0x06005098 RID: 20632 RVA: 0x00098732 File Offset: 0x00096932
		public double RefreshExpiresIn { get; set; }

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06005099 RID: 20633 RVA: 0x0009873B File Offset: 0x0009693B
		// (set) Token: 0x0600509A RID: 20634 RVA: 0x00098743 File Offset: 0x00096943
		public string RefreshExpiresAt { get; set; }
	}
}
