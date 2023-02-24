using System;
using System.Diagnostics;

// Token: 0x020000E7 RID: 231
[DebuggerDisplay("{IdHash}")]
public class Resource
{
	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x0600084E RID: 2126 RVA: 0x0002183F File Offset: 0x0001FA3F
	// (set) Token: 0x0600084F RID: 2127 RVA: 0x00021847 File Offset: 0x0001FA47
	public ResourceGuid Guid { get; private set; }

	// Token: 0x06000850 RID: 2128 RVA: 0x00021850 File Offset: 0x0001FA50
	public Resource()
	{
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00021858 File Offset: 0x0001FA58
	public Resource(string id, ResourceSet parent = null, string name = null)
	{
		global::Debug.Assert(id != null);
		this.Id = id;
		this.IdHash = new HashedString(this.Id);
		this.Guid = new ResourceGuid(id, parent);
		if (parent != null)
		{
			parent.Add(this);
		}
		if (name != null)
		{
			this.Name = name;
			return;
		}
		this.Name = id;
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x000218B6 File Offset: 0x0001FAB6
	public Resource(string id, string name)
	{
		global::Debug.Assert(id != null);
		this.Guid = new ResourceGuid(id, null);
		this.Id = id;
		this.IdHash = new HashedString(this.Id);
		this.Name = name;
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x000218F3 File Offset: 0x0001FAF3
	public virtual void Initialize()
	{
	}

	// Token: 0x04000638 RID: 1592
	public string Name;

	// Token: 0x04000639 RID: 1593
	public string Id;

	// Token: 0x0400063A RID: 1594
	public HashedString IdHash;

	// Token: 0x0400063B RID: 1595
	public bool Disabled;
}
