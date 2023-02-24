using System;

// Token: 0x02000907 RID: 2311
public class ScenePartitionerLayer
{
	// Token: 0x0600435A RID: 17242 RVA: 0x0017CE2E File Offset: 0x0017B02E
	public ScenePartitionerLayer(HashedString name, int layer)
	{
		this.name = name;
		this.layer = layer;
	}

	// Token: 0x04002CEE RID: 11502
	public HashedString name;

	// Token: 0x04002CEF RID: 11503
	public int layer;

	// Token: 0x04002CF0 RID: 11504
	public Action<int, object> OnEvent;
}
