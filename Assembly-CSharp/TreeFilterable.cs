using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x02000665 RID: 1637
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/TreeFilterable")]
public class TreeFilterable : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000E724D File Offset: 0x000E544D
	public HashSet<Tag> AcceptedTags
	{
		get
		{
			return this.acceptedTagSet;
		}
	}

	// Token: 0x06002C0B RID: 11275 RVA: 0x000E7258 File Offset: 0x000E5458
	[OnDeserialized]
	[Obsolete]
	private void OnDeserialized()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 20))
		{
			this.filterByStorageCategoriesOnSpawn = false;
		}
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 29))
		{
			this.acceptedTagSet.UnionWith(this.acceptedTags);
			this.acceptedTags = null;
		}
	}

	// Token: 0x06002C0C RID: 11276 RVA: 0x000E72B4 File Offset: 0x000E54B4
	private void OnDiscover(Tag category_tag, Tag tag)
	{
		if (this.storage.storageFilters.Contains(category_tag))
		{
			bool flag = false;
			if (DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(category_tag).Count <= 1)
			{
				foreach (Tag tag2 in this.storage.storageFilters)
				{
					if (!(tag2 == category_tag) && DiscoveredResources.Instance.IsDiscovered(tag2))
					{
						flag = true;
						foreach (Tag tag3 in DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(tag2))
						{
							if (!this.acceptedTagSet.Contains(tag3))
							{
								return;
							}
						}
					}
				}
				if (!flag)
				{
					return;
				}
			}
			foreach (Tag tag4 in DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(category_tag))
			{
				if (!(tag4 == tag) && !this.acceptedTagSet.Contains(tag4))
				{
					return;
				}
			}
			this.AddTagToFilter(tag);
		}
	}

	// Token: 0x06002C0D RID: 11277 RVA: 0x000E7408 File Offset: 0x000E5608
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<TreeFilterable>(-905833192, TreeFilterable.OnCopySettingsDelegate);
	}

	// Token: 0x06002C0E RID: 11278 RVA: 0x000E7424 File Offset: 0x000E5624
	protected override void OnSpawn()
	{
		DiscoveredResources.Instance.OnDiscover += this.OnDiscover;
		if (this.autoSelectStoredOnLoad && this.storage != null)
		{
			HashSet<Tag> hashSet = new HashSet<Tag>(this.acceptedTagSet);
			hashSet.UnionWith(this.storage.GetAllIDsInStorage());
			this.UpdateFilters(hashSet);
		}
		if (this.OnFilterChanged != null)
		{
			this.OnFilterChanged(this.acceptedTagSet);
		}
		this.RefreshTint();
		if (this.filterByStorageCategoriesOnSpawn)
		{
			this.RemoveIncorrectAcceptedTags();
		}
	}

	// Token: 0x06002C0F RID: 11279 RVA: 0x000E74B0 File Offset: 0x000E56B0
	private void RemoveIncorrectAcceptedTags()
	{
		List<Tag> list = new List<Tag>();
		foreach (Tag tag in this.acceptedTagSet)
		{
			bool flag = false;
			foreach (Tag tag2 in this.storage.storageFilters)
			{
				if (DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(tag2).Contains(tag))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.Add(tag);
			}
		}
		foreach (Tag tag3 in list)
		{
			this.RemoveTagFromFilter(tag3);
		}
	}

	// Token: 0x06002C10 RID: 11280 RVA: 0x000E75A8 File Offset: 0x000E57A8
	protected override void OnCleanUp()
	{
		DiscoveredResources.Instance.OnDiscover -= this.OnDiscover;
		base.OnCleanUp();
	}

	// Token: 0x06002C11 RID: 11281 RVA: 0x000E75C8 File Offset: 0x000E57C8
	private void OnCopySettings(object data)
	{
		TreeFilterable component = ((GameObject)data).GetComponent<TreeFilterable>();
		if (component != null)
		{
			this.UpdateFilters(component.GetTags());
		}
	}

	// Token: 0x06002C12 RID: 11282 RVA: 0x000E75F6 File Offset: 0x000E57F6
	public HashSet<Tag> GetTags()
	{
		return this.acceptedTagSet;
	}

	// Token: 0x06002C13 RID: 11283 RVA: 0x000E75FE File Offset: 0x000E57FE
	public bool ContainsTag(Tag t)
	{
		return this.acceptedTagSet.Contains(t);
	}

	// Token: 0x06002C14 RID: 11284 RVA: 0x000E760C File Offset: 0x000E580C
	public void AddTagToFilter(Tag t)
	{
		if (this.ContainsTag(t))
		{
			return;
		}
		this.UpdateFilters(new HashSet<Tag>(this.acceptedTagSet) { t });
	}

	// Token: 0x06002C15 RID: 11285 RVA: 0x000E7640 File Offset: 0x000E5840
	public void RemoveTagFromFilter(Tag t)
	{
		if (!this.ContainsTag(t))
		{
			return;
		}
		HashSet<Tag> hashSet = new HashSet<Tag>(this.acceptedTagSet);
		hashSet.Remove(t);
		this.UpdateFilters(hashSet);
	}

	// Token: 0x06002C16 RID: 11286 RVA: 0x000E7674 File Offset: 0x000E5874
	public void UpdateFilters(HashSet<Tag> filters)
	{
		this.acceptedTagSet.Clear();
		this.acceptedTagSet.UnionWith(filters);
		if (this.OnFilterChanged != null)
		{
			this.OnFilterChanged(this.acceptedTagSet);
		}
		this.RefreshTint();
		if (!this.dropIncorrectOnFilterChange || this.storage == null || this.storage.items == null)
		{
			return;
		}
		for (int i = this.storage.items.Count - 1; i >= 0; i--)
		{
			GameObject gameObject = this.storage.items[i];
			if (!(gameObject == null))
			{
				KPrefabID component = gameObject.GetComponent<KPrefabID>();
				if (!this.acceptedTagSet.Contains(component.PrefabTag))
				{
					this.storage.Drop(gameObject, true);
				}
			}
		}
	}

	// Token: 0x06002C17 RID: 11287 RVA: 0x000E773C File Offset: 0x000E593C
	public string GetTagsAsStatus(int maxDisplays = 6)
	{
		string text = "Tags:\n";
		List<Tag> list = new List<Tag>(this.storage.storageFilters);
		list.Intersect(this.acceptedTagSet);
		for (int i = 0; i < Mathf.Min(list.Count, maxDisplays); i++)
		{
			text += list[i].ProperName();
			if (i < Mathf.Min(list.Count, maxDisplays) - 1)
			{
				text += "\n";
			}
			if (i == maxDisplays - 1 && list.Count > maxDisplays)
			{
				text += "\n...";
				break;
			}
		}
		if (base.tag.Length == 0)
		{
			text = "No tags selected";
		}
		return text;
	}

	// Token: 0x06002C18 RID: 11288 RVA: 0x000E77E8 File Offset: 0x000E59E8
	private void RefreshTint()
	{
		bool flag = this.acceptedTagSet != null && this.acceptedTagSet.Count != 0;
		base.GetComponent<KBatchedAnimController>().TintColour = (flag ? this.filterTint : this.noFilterTint);
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoStorageFilterSet, !flag, this);
	}

	// Token: 0x04001A13 RID: 6675
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04001A14 RID: 6676
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001A15 RID: 6677
	public static readonly Color32 FILTER_TINT = Color.white;

	// Token: 0x04001A16 RID: 6678
	public static readonly Color32 NO_FILTER_TINT = new Color(0.5019608f, 0.5019608f, 0.5019608f, 1f);

	// Token: 0x04001A17 RID: 6679
	public Color32 filterTint = TreeFilterable.FILTER_TINT;

	// Token: 0x04001A18 RID: 6680
	public Color32 noFilterTint = TreeFilterable.NO_FILTER_TINT;

	// Token: 0x04001A19 RID: 6681
	[SerializeField]
	public bool dropIncorrectOnFilterChange = true;

	// Token: 0x04001A1A RID: 6682
	[SerializeField]
	public bool autoSelectStoredOnLoad = true;

	// Token: 0x04001A1B RID: 6683
	public bool showUserMenu = true;

	// Token: 0x04001A1C RID: 6684
	public TreeFilterable.UISideScreenHeight uiHeight = TreeFilterable.UISideScreenHeight.Tall;

	// Token: 0x04001A1D RID: 6685
	public bool filterByStorageCategoriesOnSpawn = true;

	// Token: 0x04001A1E RID: 6686
	[SerializeField]
	[Serialize]
	[Obsolete("Deprecated, use acceptedTagSet")]
	private List<Tag> acceptedTags = new List<Tag>();

	// Token: 0x04001A1F RID: 6687
	[SerializeField]
	[Serialize]
	private HashSet<Tag> acceptedTagSet = new HashSet<Tag>();

	// Token: 0x04001A20 RID: 6688
	public Action<HashSet<Tag>> OnFilterChanged;

	// Token: 0x04001A21 RID: 6689
	private static readonly EventSystem.IntraObjectHandler<TreeFilterable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<TreeFilterable>(delegate(TreeFilterable component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x02001326 RID: 4902
	public enum UISideScreenHeight
	{
		// Token: 0x04005FAD RID: 24493
		Short,
		// Token: 0x04005FAE RID: 24494
		Tall
	}
}
