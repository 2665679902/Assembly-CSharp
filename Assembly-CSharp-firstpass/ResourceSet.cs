using System;

// Token: 0x020000EA RID: 234
[Serializable]
public abstract class ResourceSet : Resource
{
	// Token: 0x06000861 RID: 2145 RVA: 0x00021ABC File Offset: 0x0001FCBC
	public ResourceSet()
	{
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x00021AC4 File Offset: 0x0001FCC4
	public ResourceSet(string id, ResourceSet parent)
		: base(id, parent, null)
	{
	}

	// Token: 0x06000863 RID: 2147
	public abstract Resource Add(Resource resource);

	// Token: 0x06000864 RID: 2148
	public abstract void Remove(Resource resource);

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000865 RID: 2149
	public abstract int Count { get; }

	// Token: 0x06000866 RID: 2150
	public abstract Resource GetResource(int idx);
}
