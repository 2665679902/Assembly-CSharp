using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000C83 RID: 3203
	public class FacadeInfo
	{
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600653F RID: 25919 RVA: 0x0025FBF8 File Offset: 0x0025DDF8
		// (set) Token: 0x06006540 RID: 25920 RVA: 0x0025FC00 File Offset: 0x0025DE00
		public string prefabID { get; set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06006541 RID: 25921 RVA: 0x0025FC09 File Offset: 0x0025DE09
		// (set) Token: 0x06006542 RID: 25922 RVA: 0x0025FC11 File Offset: 0x0025DE11
		public string id { get; set; }

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06006543 RID: 25923 RVA: 0x0025FC1A File Offset: 0x0025DE1A
		// (set) Token: 0x06006544 RID: 25924 RVA: 0x0025FC22 File Offset: 0x0025DE22
		public string name { get; set; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06006545 RID: 25925 RVA: 0x0025FC2B File Offset: 0x0025DE2B
		// (set) Token: 0x06006546 RID: 25926 RVA: 0x0025FC33 File Offset: 0x0025DE33
		public string description { get; set; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06006547 RID: 25927 RVA: 0x0025FC3C File Offset: 0x0025DE3C
		// (set) Token: 0x06006548 RID: 25928 RVA: 0x0025FC44 File Offset: 0x0025DE44
		public string animFile { get; set; }

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06006549 RID: 25929 RVA: 0x0025FC4D File Offset: 0x0025DE4D
		// (set) Token: 0x0600654A RID: 25930 RVA: 0x0025FC55 File Offset: 0x0025DE55
		public List<FacadeInfo.workable> workables { get; set; }

		// Token: 0x02001B15 RID: 6933
		public class workable
		{
			// Token: 0x170009E4 RID: 2532
			// (get) Token: 0x060094B9 RID: 38073 RVA: 0x0031D179 File Offset: 0x0031B379
			// (set) Token: 0x060094BA RID: 38074 RVA: 0x0031D181 File Offset: 0x0031B381
			public string workableName { get; set; }

			// Token: 0x170009E5 RID: 2533
			// (get) Token: 0x060094BB RID: 38075 RVA: 0x0031D18A File Offset: 0x0031B38A
			// (set) Token: 0x060094BC RID: 38076 RVA: 0x0031D192 File Offset: 0x0031B392
			public string workableAnim { get; set; }
		}
	}
}
