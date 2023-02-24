using System;

// Token: 0x02000769 RID: 1897
public class Expectation
{
	// Token: 0x170003CB RID: 971
	// (get) Token: 0x06003415 RID: 13333 RVA: 0x001184ED File Offset: 0x001166ED
	// (set) Token: 0x06003416 RID: 13334 RVA: 0x001184F5 File Offset: 0x001166F5
	public string id { get; protected set; }

	// Token: 0x170003CC RID: 972
	// (get) Token: 0x06003417 RID: 13335 RVA: 0x001184FE File Offset: 0x001166FE
	// (set) Token: 0x06003418 RID: 13336 RVA: 0x00118506 File Offset: 0x00116706
	public string name { get; protected set; }

	// Token: 0x170003CD RID: 973
	// (get) Token: 0x06003419 RID: 13337 RVA: 0x0011850F File Offset: 0x0011670F
	// (set) Token: 0x0600341A RID: 13338 RVA: 0x00118517 File Offset: 0x00116717
	public string description { get; protected set; }

	// Token: 0x170003CE RID: 974
	// (get) Token: 0x0600341B RID: 13339 RVA: 0x00118520 File Offset: 0x00116720
	// (set) Token: 0x0600341C RID: 13340 RVA: 0x00118528 File Offset: 0x00116728
	public Action<MinionResume> OnApply { get; protected set; }

	// Token: 0x170003CF RID: 975
	// (get) Token: 0x0600341D RID: 13341 RVA: 0x00118531 File Offset: 0x00116731
	// (set) Token: 0x0600341E RID: 13342 RVA: 0x00118539 File Offset: 0x00116739
	public Action<MinionResume> OnRemove { get; protected set; }

	// Token: 0x0600341F RID: 13343 RVA: 0x00118542 File Offset: 0x00116742
	public Expectation(string id, string name, string description, Action<MinionResume> OnApply, Action<MinionResume> OnRemove)
	{
		this.id = id;
		this.name = name;
		this.description = description;
		this.OnApply = OnApply;
		this.OnRemove = OnRemove;
	}
}
