using System;

// Token: 0x0200012B RID: 299
[Serializable]
public class BaseLocation
{
	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00027413 File Offset: 0x00025613
	// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0002741B File Offset: 0x0002561B
	public int left { get; set; }

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00027424 File Offset: 0x00025624
	// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0002742C File Offset: 0x0002562C
	public int right { get; set; }

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00027435 File Offset: 0x00025635
	// (set) Token: 0x06000A4A RID: 2634 RVA: 0x0002743D File Offset: 0x0002563D
	public int top { get; set; }

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00027446 File Offset: 0x00025646
	// (set) Token: 0x06000A4C RID: 2636 RVA: 0x0002744E File Offset: 0x0002564E
	public int bottom { get; set; }
}
