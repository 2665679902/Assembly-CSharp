using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200071E RID: 1822
[SerializationConfig(MemberSerialization.OptIn)]
public class DiscoveredResources : KMonoBehaviour, ISaveLoadable, ISim4000ms
{
	// Token: 0x060031CF RID: 12751 RVA: 0x0010A7B0 File Offset: 0x001089B0
	public static void DestroyInstance()
	{
		DiscoveredResources.Instance = null;
	}

	// Token: 0x14000018 RID: 24
	// (add) Token: 0x060031D0 RID: 12752 RVA: 0x0010A7B8 File Offset: 0x001089B8
	// (remove) Token: 0x060031D1 RID: 12753 RVA: 0x0010A7F0 File Offset: 0x001089F0
	public event Action<Tag, Tag> OnDiscover;

	// Token: 0x060031D2 RID: 12754 RVA: 0x0010A828 File Offset: 0x00108A28
	public void Discover(Tag tag, Tag categoryTag)
	{
		bool flag = this.Discovered.Add(tag);
		this.DiscoverCategory(categoryTag, tag);
		if (flag)
		{
			if (this.OnDiscover != null)
			{
				this.OnDiscover(categoryTag, tag);
			}
			if (!this.newDiscoveries.ContainsKey(tag))
			{
				this.newDiscoveries.Add(tag, (float)GameClock.Instance.GetCycle() + GameClock.Instance.GetCurrentCycleAsPercentage());
			}
		}
	}

	// Token: 0x060031D3 RID: 12755 RVA: 0x0010A890 File Offset: 0x00108A90
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DiscoveredResources.Instance = this;
	}

	// Token: 0x060031D4 RID: 12756 RVA: 0x0010A89E File Offset: 0x00108A9E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.FilterDisabledContent();
	}

	// Token: 0x060031D5 RID: 12757 RVA: 0x0010A8AC File Offset: 0x00108AAC
	private void FilterDisabledContent()
	{
		HashSet<Tag> hashSet = new HashSet<Tag>();
		foreach (Tag tag in this.Discovered)
		{
			Element element = ElementLoader.GetElement(tag);
			if (element != null && element.disabled)
			{
				hashSet.Add(tag);
			}
			else
			{
				GameObject gameObject = Assets.TryGetPrefab(tag);
				if (gameObject != null && gameObject.HasTag(GameTags.DeprecatedContent))
				{
					hashSet.Add(tag);
				}
				else if (gameObject == null)
				{
					hashSet.Add(tag);
				}
			}
		}
		foreach (Tag tag2 in hashSet)
		{
			this.Discovered.Remove(tag2);
		}
		foreach (KeyValuePair<Tag, HashSet<Tag>> keyValuePair in this.DiscoveredCategories)
		{
			foreach (Tag tag3 in hashSet)
			{
				if (keyValuePair.Value.Contains(tag3))
				{
					keyValuePair.Value.Remove(tag3);
				}
			}
		}
	}

	// Token: 0x060031D6 RID: 12758 RVA: 0x0010AA30 File Offset: 0x00108C30
	public bool CheckAllDiscoveredAreNew()
	{
		foreach (Tag tag in this.Discovered)
		{
			if (!this.newDiscoveries.ContainsKey(tag))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060031D7 RID: 12759 RVA: 0x0010AA94 File Offset: 0x00108C94
	private void DiscoverCategory(Tag category_tag, Tag item_tag)
	{
		HashSet<Tag> hashSet;
		if (!this.DiscoveredCategories.TryGetValue(category_tag, out hashSet))
		{
			hashSet = new HashSet<Tag>();
			this.DiscoveredCategories[category_tag] = hashSet;
		}
		hashSet.Add(item_tag);
	}

	// Token: 0x060031D8 RID: 12760 RVA: 0x0010AACC File Offset: 0x00108CCC
	public HashSet<Tag> GetDiscovered()
	{
		return this.Discovered;
	}

	// Token: 0x060031D9 RID: 12761 RVA: 0x0010AAD4 File Offset: 0x00108CD4
	public bool IsDiscovered(Tag tag)
	{
		return this.Discovered.Contains(tag) || this.DiscoveredCategories.ContainsKey(tag);
	}

	// Token: 0x060031DA RID: 12762 RVA: 0x0010AAF4 File Offset: 0x00108CF4
	public bool AnyDiscovered(ICollection<Tag> tags)
	{
		foreach (Tag tag in tags)
		{
			if (this.IsDiscovered(tag))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060031DB RID: 12763 RVA: 0x0010AB48 File Offset: 0x00108D48
	public bool TryGetDiscoveredResourcesFromTag(Tag tag, out HashSet<Tag> resources)
	{
		return this.DiscoveredCategories.TryGetValue(tag, out resources);
	}

	// Token: 0x060031DC RID: 12764 RVA: 0x0010AB58 File Offset: 0x00108D58
	public HashSet<Tag> GetDiscoveredResourcesFromTag(Tag tag)
	{
		HashSet<Tag> hashSet;
		if (this.DiscoveredCategories.TryGetValue(tag, out hashSet))
		{
			return hashSet;
		}
		return new HashSet<Tag>();
	}

	// Token: 0x060031DD RID: 12765 RVA: 0x0010AB7C File Offset: 0x00108D7C
	public Dictionary<Tag, HashSet<Tag>> GetDiscoveredResourcesFromTagSet(TagSet tagSet)
	{
		Dictionary<Tag, HashSet<Tag>> dictionary = new Dictionary<Tag, HashSet<Tag>>();
		foreach (Tag tag in tagSet)
		{
			HashSet<Tag> hashSet;
			if (this.DiscoveredCategories.TryGetValue(tag, out hashSet))
			{
				dictionary[tag] = hashSet;
			}
		}
		return dictionary;
	}

	// Token: 0x060031DE RID: 12766 RVA: 0x0010ABDC File Offset: 0x00108DDC
	public static Tag GetCategoryForTags(HashSet<Tag> tags)
	{
		Tag tag = Tag.Invalid;
		foreach (Tag tag2 in tags)
		{
			if (GameTags.AllCategories.Contains(tag2) || GameTags.IgnoredMaterialCategories.Contains(tag2))
			{
				tag = tag2;
				break;
			}
		}
		return tag;
	}

	// Token: 0x060031DF RID: 12767 RVA: 0x0010AC48 File Offset: 0x00108E48
	public static Tag GetCategoryForEntity(KPrefabID entity)
	{
		ElementChunk component = entity.GetComponent<ElementChunk>();
		if (component != null)
		{
			return component.GetComponent<PrimaryElement>().Element.materialCategory;
		}
		return DiscoveredResources.GetCategoryForTags(entity.Tags);
	}

	// Token: 0x060031E0 RID: 12768 RVA: 0x0010AC84 File Offset: 0x00108E84
	public void Sim4000ms(float dt)
	{
		float num = GameClock.Instance.GetTimeInCycles() + GameClock.Instance.GetCurrentCycleAsPercentage();
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, float> keyValuePair in this.newDiscoveries)
		{
			if (num - keyValuePair.Value > 3f)
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (Tag tag in list)
		{
			this.newDiscoveries.Remove(tag);
		}
	}

	// Token: 0x04001E41 RID: 7745
	public static DiscoveredResources Instance;

	// Token: 0x04001E42 RID: 7746
	[Serialize]
	private HashSet<Tag> Discovered = new HashSet<Tag>();

	// Token: 0x04001E43 RID: 7747
	[Serialize]
	private Dictionary<Tag, HashSet<Tag>> DiscoveredCategories = new Dictionary<Tag, HashSet<Tag>>();

	// Token: 0x04001E45 RID: 7749
	[Serialize]
	public Dictionary<Tag, float> newDiscoveries = new Dictionary<Tag, float>();
}
