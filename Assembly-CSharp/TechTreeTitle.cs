using System;
using UnityEngine;

// Token: 0x020008E0 RID: 2272
public class TechTreeTitle : Resource
{
	// Token: 0x1700049A RID: 1178
	// (get) Token: 0x0600416D RID: 16749 RVA: 0x0016EADD File Offset: 0x0016CCDD
	public Vector2 center
	{
		get
		{
			return this.node.center;
		}
	}

	// Token: 0x1700049B RID: 1179
	// (get) Token: 0x0600416E RID: 16750 RVA: 0x0016EAEA File Offset: 0x0016CCEA
	public float width
	{
		get
		{
			return this.node.width;
		}
	}

	// Token: 0x1700049C RID: 1180
	// (get) Token: 0x0600416F RID: 16751 RVA: 0x0016EAF7 File Offset: 0x0016CCF7
	public float height
	{
		get
		{
			return this.node.height;
		}
	}

	// Token: 0x06004170 RID: 16752 RVA: 0x0016EB04 File Offset: 0x0016CD04
	public TechTreeTitle(string id, ResourceSet parent, string name, ResourceTreeNode node)
		: base(id, parent, name)
	{
		this.node = node;
	}

	// Token: 0x04002BA7 RID: 11175
	public string desc;

	// Token: 0x04002BA8 RID: 11176
	private ResourceTreeNode node;
}
