using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using KSerialization;
using UnityEngine;

// Token: 0x0200010C RID: 268
[SerializationConfig(MemberSerialization.OptIn)]
[Serializable]
public class TagSet : ICollection<Tag>, IEnumerable<Tag>, IEnumerable, ICollection
{
	// Token: 0x0600090D RID: 2317 RVA: 0x00024395 File Offset: 0x00022595
	public TagSet()
	{
		this.tags = new List<Tag>();
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x000243B3 File Offset: 0x000225B3
	public TagSet(TagSet other)
	{
		this.tags = new List<Tag>(other.tags);
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x000243D7 File Offset: 0x000225D7
	public TagSet(Tag[] other)
	{
		this.tags = new List<Tag>(other);
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x000243F8 File Offset: 0x000225F8
	public TagSet(IEnumerable<string> others)
	{
		this.tags = new List<Tag>();
		foreach (string text in others)
		{
			this.tags.Add(new Tag(text));
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00024448 File Offset: 0x00022648
	public TagSet(params TagSet[] others)
	{
		this.tags = new List<Tag>();
		for (int i = 0; i < others.Length; i++)
		{
			this.tags.AddRange(others[i]);
		}
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x00024490 File Offset: 0x00022690
	public TagSet(params string[] others)
	{
		this.tags = new List<Tag>();
		for (int i = 0; i < others.Length; i++)
		{
			this.tags.Add(new Tag(others[i]));
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06000913 RID: 2323 RVA: 0x000244DA File Offset: 0x000226DA
	public int Count
	{
		get
		{
			return this.tags.Count;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06000914 RID: 2324 RVA: 0x000244E7 File Offset: 0x000226E7
	public bool IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x000244EA File Offset: 0x000226EA
	public void Add(Tag item)
	{
		if (!this.tags.Contains(item))
		{
			this.tags.Add(item);
		}
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x00024508 File Offset: 0x00022708
	public void Union(TagSet others)
	{
		for (int i = 0; i < others.tags.Count; i++)
		{
			if (!this.tags.Contains(others.tags[i]))
			{
				this.tags.Add(others.tags[i]);
			}
		}
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0002455B File Offset: 0x0002275B
	public void Clear()
	{
		this.tags.Clear();
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00024568 File Offset: 0x00022768
	public bool Contains(Tag item)
	{
		return this.tags.Contains(item);
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00024578 File Offset: 0x00022778
	public bool ContainsAll(TagSet others)
	{
		for (int i = 0; i < others.tags.Count; i++)
		{
			if (!this.tags.Contains(others.tags[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x000245B8 File Offset: 0x000227B8
	public bool ContainsOne(TagSet others)
	{
		for (int i = 0; i < others.tags.Count; i++)
		{
			if (this.tags.Contains(others.tags[i]))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x000245F7 File Offset: 0x000227F7
	public void CopyTo(Tag[] array, int arrayIndex)
	{
		this.tags.CopyTo(array, arrayIndex);
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00024606 File Offset: 0x00022806
	public bool Remove(Tag item)
	{
		return this.tags.Remove(item);
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00024614 File Offset: 0x00022814
	public void Remove(TagSet other)
	{
		for (int i = 0; i < other.tags.Count; i++)
		{
			if (this.tags.Contains(other.tags[i]))
			{
				this.tags.Remove(other.tags[i]);
			}
		}
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00024668 File Offset: 0x00022868
	public IEnumerator<Tag> GetEnumerator()
	{
		return this.tags.GetEnumerator();
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x0002467A File Offset: 0x0002287A
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x170000F3 RID: 243
	public Tag this[int i]
	{
		get
		{
			return this.tags[i];
		}
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x00024690 File Offset: 0x00022890
	public override string ToString()
	{
		if (this.tags.Count > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.tags[0].Name);
			for (int i = 1; i < this.tags.Count; i++)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(this.tags[i].Name);
			}
			return stringBuilder.ToString();
		}
		return "";
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x00024718 File Offset: 0x00022918
	public string GetTagDescription()
	{
		if (this.tags.Count > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(TagDescriptions.GetDescription(this.tags[0].ToString()));
			for (int i = 1; i < this.tags.Count; i++)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(TagDescriptions.GetDescription(this.tags[i].ToString()));
			}
			return stringBuilder.ToString();
		}
		return "";
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06000923 RID: 2339 RVA: 0x000247B3 File Offset: 0x000229B3
	public bool IsSynchronized
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06000924 RID: 2340 RVA: 0x000247BA File Offset: 0x000229BA
	public object SyncRoot
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x000247C1 File Offset: 0x000229C1
	public void CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	// Token: 0x04000681 RID: 1665
	[Serialize]
	[SerializeField]
	private List<Tag> tags = new List<Tag>();
}
