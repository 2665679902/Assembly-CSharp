using System;
using KSerialization;

// Token: 0x020000E8 RID: 232
[SerializationConfig(MemberSerialization.OptIn)]
public class ResourceGuid : IEquatable<ResourceGuid>, ISaveLoadable
{
	// Token: 0x06000854 RID: 2132 RVA: 0x000218F5 File Offset: 0x0001FAF5
	public ResourceGuid(string id, Resource parent = null)
	{
		if (parent != null)
		{
			this.Guid = parent.Guid.Guid + "." + id;
			return;
		}
		this.Guid = id;
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00021924 File Offset: 0x0001FB24
	public override int GetHashCode()
	{
		return this.Guid.GetHashCode();
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00021934 File Offset: 0x0001FB34
	public override bool Equals(object obj)
	{
		ResourceGuid resourceGuid = (ResourceGuid)obj;
		return obj != null && this.Guid == resourceGuid.Guid;
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0002195E File Offset: 0x0001FB5E
	public bool Equals(ResourceGuid other)
	{
		return this.Guid == other.Guid;
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00021971 File Offset: 0x0001FB71
	public static bool operator ==(ResourceGuid a, ResourceGuid b)
	{
		return a == b || (a != null && b != null && a.Guid == b.Guid);
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00021994 File Offset: 0x0001FB94
	public static bool operator !=(ResourceGuid a, ResourceGuid b)
	{
		return a != b && (a == null || b == null || a.Guid != b.Guid);
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x000219B7 File Offset: 0x0001FBB7
	public override string ToString()
	{
		return this.Guid;
	}

	// Token: 0x0400063C RID: 1596
	[Serialize]
	public string Guid;
}
