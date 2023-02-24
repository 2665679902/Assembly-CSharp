using System;
using System.Collections.Generic;

// Token: 0x020009A4 RID: 2468
public class TagCollection : IReadonlyTags
{
	// Token: 0x0600493C RID: 18748 RVA: 0x0019A171 File Offset: 0x00198371
	public TagCollection()
	{
	}

	// Token: 0x0600493D RID: 18749 RVA: 0x0019A184 File Offset: 0x00198384
	public TagCollection(int[] initialTags)
	{
		for (int i = 0; i < initialTags.Length; i++)
		{
			this.tags.Add(initialTags[i]);
		}
	}

	// Token: 0x0600493E RID: 18750 RVA: 0x0019A1C0 File Offset: 0x001983C0
	public TagCollection(string[] initialTags)
	{
		for (int i = 0; i < initialTags.Length; i++)
		{
			this.tags.Add(Hash.SDBMLower(initialTags[i]));
		}
	}

	// Token: 0x0600493F RID: 18751 RVA: 0x0019A200 File Offset: 0x00198400
	public TagCollection(TagCollection initialTags)
	{
		if (initialTags != null && initialTags.tags != null)
		{
			this.tags.UnionWith(initialTags.tags);
		}
	}

	// Token: 0x06004940 RID: 18752 RVA: 0x0019A230 File Offset: 0x00198430
	public TagCollection Append(TagCollection others)
	{
		foreach (int num in others.tags)
		{
			this.tags.Add(num);
		}
		return this;
	}

	// Token: 0x06004941 RID: 18753 RVA: 0x0019A28C File Offset: 0x0019848C
	public void AddTag(string tag)
	{
		this.tags.Add(Hash.SDBMLower(tag));
	}

	// Token: 0x06004942 RID: 18754 RVA: 0x0019A2A0 File Offset: 0x001984A0
	public void AddTag(int tag)
	{
		this.tags.Add(tag);
	}

	// Token: 0x06004943 RID: 18755 RVA: 0x0019A2AF File Offset: 0x001984AF
	public void RemoveTag(string tag)
	{
		this.tags.Remove(Hash.SDBMLower(tag));
	}

	// Token: 0x06004944 RID: 18756 RVA: 0x0019A2C3 File Offset: 0x001984C3
	public void RemoveTag(int tag)
	{
		this.tags.Remove(tag);
	}

	// Token: 0x06004945 RID: 18757 RVA: 0x0019A2D2 File Offset: 0x001984D2
	public bool HasTag(string tag)
	{
		return this.tags.Contains(Hash.SDBMLower(tag));
	}

	// Token: 0x06004946 RID: 18758 RVA: 0x0019A2E5 File Offset: 0x001984E5
	public bool HasTag(int tag)
	{
		return this.tags.Contains(tag);
	}

	// Token: 0x06004947 RID: 18759 RVA: 0x0019A2F4 File Offset: 0x001984F4
	public bool HasTags(int[] searchTags)
	{
		for (int i = 0; i < searchTags.Length; i++)
		{
			if (!this.tags.Contains(searchTags[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0400301A RID: 12314
	private HashSet<int> tags = new HashSet<int>();
}
