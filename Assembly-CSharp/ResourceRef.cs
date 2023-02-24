using System;
using System.Runtime.Serialization;
using KSerialization;

// Token: 0x020008E5 RID: 2277
[SerializationConfig(MemberSerialization.OptIn)]
public class ResourceRef<ResourceType> : ISaveLoadable where ResourceType : Resource
{
	// Token: 0x06004192 RID: 16786 RVA: 0x0016F748 File Offset: 0x0016D948
	public ResourceRef(ResourceType resource)
	{
		this.Set(resource);
	}

	// Token: 0x06004193 RID: 16787 RVA: 0x0016F757 File Offset: 0x0016D957
	public ResourceRef()
	{
	}

	// Token: 0x1700049F RID: 1183
	// (get) Token: 0x06004194 RID: 16788 RVA: 0x0016F75F File Offset: 0x0016D95F
	public ResourceGuid Guid
	{
		get
		{
			return this.guid;
		}
	}

	// Token: 0x06004195 RID: 16789 RVA: 0x0016F767 File Offset: 0x0016D967
	public ResourceType Get()
	{
		return this.resource;
	}

	// Token: 0x06004196 RID: 16790 RVA: 0x0016F76F File Offset: 0x0016D96F
	public void Set(ResourceType resource)
	{
		this.guid = null;
		this.resource = resource;
	}

	// Token: 0x06004197 RID: 16791 RVA: 0x0016F77F File Offset: 0x0016D97F
	[OnSerializing]
	private void OnSerializing()
	{
		if (this.resource == null)
		{
			this.guid = null;
			return;
		}
		this.guid = this.resource.Guid;
	}

	// Token: 0x06004198 RID: 16792 RVA: 0x0016F7AC File Offset: 0x0016D9AC
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.guid != null)
		{
			this.resource = Db.Get().GetResource<ResourceType>(this.guid);
			if (this.resource != null)
			{
				this.guid = null;
			}
		}
	}

	// Token: 0x04002BB9 RID: 11193
	[Serialize]
	private ResourceGuid guid;

	// Token: 0x04002BBA RID: 11194
	private ResourceType resource;
}
