using System;

namespace ProcGen.Noise
{
	// Token: 0x020004F1 RID: 1265
	public class SampleSettings : NoiseBase
	{
		// Token: 0x06003698 RID: 13976 RVA: 0x00077C00 File Offset: 0x00075E00
		public override Type GetObjectType()
		{
			return typeof(SampleSettings);
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x00077C0C File Offset: 0x00075E0C
		// (set) Token: 0x0600369A RID: 13978 RVA: 0x00077C14 File Offset: 0x00075E14
		public float zoom { get; set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600369B RID: 13979 RVA: 0x00077C1D File Offset: 0x00075E1D
		// (set) Token: 0x0600369C RID: 13980 RVA: 0x00077C25 File Offset: 0x00075E25
		public bool normalise { get; set; }

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x00077C2E File Offset: 0x00075E2E
		// (set) Token: 0x0600369E RID: 13982 RVA: 0x00077C36 File Offset: 0x00075E36
		public bool seamless { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x00077C3F File Offset: 0x00075E3F
		// (set) Token: 0x060036A0 RID: 13984 RVA: 0x00077C47 File Offset: 0x00075E47
		public Vector2f lowerBound { get; set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x00077C50 File Offset: 0x00075E50
		// (set) Token: 0x060036A2 RID: 13986 RVA: 0x00077C58 File Offset: 0x00075E58
		public Vector2f upperBound { get; set; }

		// Token: 0x060036A3 RID: 13987 RVA: 0x00077C61 File Offset: 0x00075E61
		public SampleSettings()
		{
			this.zoom = 0.1f;
			this.lowerBound = new Vector2f(2, 2);
			this.upperBound = new Vector2f(4, 4);
			this.seamless = false;
			this.normalise = false;
		}
	}
}
