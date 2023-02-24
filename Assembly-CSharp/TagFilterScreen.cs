using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A1F RID: 2591
public class TagFilterScreen : SideScreenContent
{
	// Token: 0x06004E41 RID: 20033 RVA: 0x001BBC13 File Offset: 0x001B9E13
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<TreeFilterable>() != null;
	}

	// Token: 0x06004E42 RID: 20034 RVA: 0x001BBC24 File Offset: 0x001B9E24
	public override void SetTarget(GameObject target)
	{
		if (target == null)
		{
			global::Debug.LogError("The target object provided was null");
			return;
		}
		this.targetFilterable = target.GetComponent<TreeFilterable>();
		if (this.targetFilterable == null)
		{
			global::Debug.LogError("The target provided does not have a Tree Filterable component");
			return;
		}
		if (!this.targetFilterable.showUserMenu)
		{
			return;
		}
		this.Filter(this.targetFilterable.AcceptedTags);
		base.Activate();
	}

	// Token: 0x06004E43 RID: 20035 RVA: 0x001BBC90 File Offset: 0x001B9E90
	protected override void OnActivate()
	{
		this.rootItem = this.BuildDisplay(this.rootTag);
		this.treeControl.SetUserItemRoot(this.rootItem);
		this.treeControl.root.opened = true;
		this.Filter(this.treeControl.root, this.acceptedTags, false);
	}

	// Token: 0x06004E44 RID: 20036 RVA: 0x001BBCEC File Offset: 0x001B9EEC
	public static List<Tag> GetAllTags()
	{
		List<Tag> list = new List<Tag>();
		foreach (TagFilterScreen.TagEntry tagEntry in TagFilterScreen.defaultRootTag.children)
		{
			if (tagEntry.tag.IsValid)
			{
				list.Add(tagEntry.tag);
			}
		}
		return list;
	}

	// Token: 0x06004E45 RID: 20037 RVA: 0x001BBD38 File Offset: 0x001B9F38
	private KTreeControl.UserItem BuildDisplay(TagFilterScreen.TagEntry root)
	{
		KTreeControl.UserItem userItem = null;
		if (root.name != null && root.name != "")
		{
			userItem = new KTreeControl.UserItem
			{
				text = root.name,
				userData = root.tag
			};
			List<KTreeControl.UserItem> list = new List<KTreeControl.UserItem>();
			if (root.children != null)
			{
				foreach (TagFilterScreen.TagEntry tagEntry in root.children)
				{
					list.Add(this.BuildDisplay(tagEntry));
				}
			}
			userItem.children = list;
		}
		return userItem;
	}

	// Token: 0x06004E46 RID: 20038 RVA: 0x001BBDC4 File Offset: 0x001B9FC4
	private static KTreeControl.UserItem CreateTree(string tree_name, Tag tree_tag, IList<Element> items)
	{
		KTreeControl.UserItem userItem = new KTreeControl.UserItem
		{
			text = tree_name,
			userData = tree_tag,
			children = new List<KTreeControl.UserItem>()
		};
		foreach (Element element in items)
		{
			KTreeControl.UserItem userItem2 = new KTreeControl.UserItem
			{
				text = element.name,
				userData = GameTagExtensions.Create(element.id)
			};
			userItem.children.Add(userItem2);
		}
		return userItem;
	}

	// Token: 0x06004E47 RID: 20039 RVA: 0x001BBE60 File Offset: 0x001BA060
	public void SetRootTag(TagFilterScreen.TagEntry root_tag)
	{
		this.rootTag = root_tag;
	}

	// Token: 0x06004E48 RID: 20040 RVA: 0x001BBE69 File Offset: 0x001BA069
	public void Filter(HashSet<Tag> acceptedTags)
	{
		this.acceptedTags = acceptedTags;
	}

	// Token: 0x06004E49 RID: 20041 RVA: 0x001BBE74 File Offset: 0x001BA074
	private void Filter(KTreeItem root, HashSet<Tag> acceptedTags, bool parentEnabled)
	{
		root.checkboxChecked = parentEnabled || (root.userData != null && acceptedTags.Contains((Tag)root.userData));
		foreach (KTreeItem ktreeItem in root.children)
		{
			this.Filter(ktreeItem, acceptedTags, root.checkboxChecked);
		}
		if (!root.checkboxChecked && root.children.Count > 0)
		{
			bool flag = true;
			using (IEnumerator<KTreeItem> enumerator = root.children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.checkboxChecked)
					{
						flag = false;
						break;
					}
				}
			}
			root.checkboxChecked = flag;
		}
	}

	// Token: 0x040033B8 RID: 13240
	[SerializeField]
	private KTreeControl treeControl;

	// Token: 0x040033B9 RID: 13241
	private KTreeControl.UserItem rootItem;

	// Token: 0x040033BA RID: 13242
	private TagFilterScreen.TagEntry rootTag = TagFilterScreen.defaultRootTag;

	// Token: 0x040033BB RID: 13243
	private HashSet<Tag> acceptedTags = new HashSet<Tag>();

	// Token: 0x040033BC RID: 13244
	private TreeFilterable targetFilterable;

	// Token: 0x040033BD RID: 13245
	public static TagFilterScreen.TagEntry defaultRootTag = new TagFilterScreen.TagEntry
	{
		name = "All",
		tag = default(Tag),
		children = new TagFilterScreen.TagEntry[0]
	};

	// Token: 0x02001853 RID: 6227
	public class TagEntry
	{
		// Token: 0x0400701D RID: 28701
		public string name;

		// Token: 0x0400701E RID: 28702
		public Tag tag;

		// Token: 0x0400701F RID: 28703
		public TagFilterScreen.TagEntry[] children;
	}
}
