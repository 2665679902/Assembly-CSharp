using System;
using UnityEngine;

// Token: 0x020008DF RID: 2271
public class TechItem : Resource
{
	// Token: 0x06004169 RID: 16745 RVA: 0x0016EA7B File Offset: 0x0016CC7B
	public TechItem(string id, ResourceSet parent, string name, string description, Func<string, bool, Sprite> getUISprite, string parentTechId, string[] dlcIds)
		: base(id, parent, name)
	{
		this.description = description;
		this.getUISprite = getUISprite;
		this.parentTechId = parentTechId;
		this.dlcIds = dlcIds;
	}

	// Token: 0x17000499 RID: 1177
	// (get) Token: 0x0600416A RID: 16746 RVA: 0x0016EAA6 File Offset: 0x0016CCA6
	public Tech ParentTech
	{
		get
		{
			return Db.Get().Techs.Get(this.parentTechId);
		}
	}

	// Token: 0x0600416B RID: 16747 RVA: 0x0016EABD File Offset: 0x0016CCBD
	public Sprite UISprite()
	{
		return this.getUISprite("ui", false);
	}

	// Token: 0x0600416C RID: 16748 RVA: 0x0016EAD0 File Offset: 0x0016CCD0
	public bool IsComplete()
	{
		return this.ParentTech.IsComplete();
	}

	// Token: 0x04002BA3 RID: 11171
	public string description;

	// Token: 0x04002BA4 RID: 11172
	public Func<string, bool, Sprite> getUISprite;

	// Token: 0x04002BA5 RID: 11173
	public string parentTechId;

	// Token: 0x04002BA6 RID: 11174
	public string[] dlcIds;
}
