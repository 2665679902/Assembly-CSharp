using System;

// Token: 0x020006F8 RID: 1784
public class Death : Resource
{
	// Token: 0x060030C2 RID: 12482 RVA: 0x001023EE File Offset: 0x001005EE
	public Death(string id, ResourceSet parent, string name, string description, string pre_anim, string loop_anim)
		: base(id, parent, name)
	{
		this.preAnim = pre_anim;
		this.loopAnim = loop_anim;
		this.description = description;
	}

	// Token: 0x04001D66 RID: 7526
	public string preAnim;

	// Token: 0x04001D67 RID: 7527
	public string loopAnim;

	// Token: 0x04001D68 RID: 7528
	public string sound;

	// Token: 0x04001D69 RID: 7529
	public string description;
}
