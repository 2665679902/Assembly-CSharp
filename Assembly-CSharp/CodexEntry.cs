using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000A5B RID: 2651
public class CodexEntry
{
	// Token: 0x060050C0 RID: 20672 RVA: 0x001CF1E8 File Offset: 0x001CD3E8
	public CodexEntry()
	{
		this.dlcIds = DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060050C1 RID: 20673 RVA: 0x001CF240 File Offset: 0x001CD440
	public CodexEntry(string category, List<ContentContainer> contentContainers, string name)
	{
		this.category = category;
		this.name = name;
		this.contentContainers = contentContainers;
		if (string.IsNullOrEmpty(this.sortString))
		{
			this.sortString = UI.StripLinkFormatting(name);
		}
		this.dlcIds = DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060050C2 RID: 20674 RVA: 0x001CF2C4 File Offset: 0x001CD4C4
	public CodexEntry(string category, string titleKey, List<ContentContainer> contentContainers)
	{
		this.category = category;
		this.title = titleKey;
		this.contentContainers = contentContainers;
		if (string.IsNullOrEmpty(this.sortString))
		{
			this.sortString = UI.StripLinkFormatting(this.title);
		}
		this.dlcIds = DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x170005E7 RID: 1511
	// (get) Token: 0x060050C3 RID: 20675 RVA: 0x001CF34D File Offset: 0x001CD54D
	// (set) Token: 0x060050C4 RID: 20676 RVA: 0x001CF355 File Offset: 0x001CD555
	public List<ContentContainer> contentContainers
	{
		get
		{
			return this._contentContainers;
		}
		private set
		{
			this._contentContainers = value;
		}
	}

	// Token: 0x060050C5 RID: 20677 RVA: 0x001CF360 File Offset: 0x001CD560
	public static List<string> ContentContainerDebug(List<ContentContainer> _contentContainers)
	{
		List<string> list = new List<string>();
		foreach (ContentContainer contentContainer in _contentContainers)
		{
			if (contentContainer != null)
			{
				string text = string.Concat(new string[]
				{
					"<b>",
					contentContainer.contentLayout.ToString(),
					" container: ",
					((contentContainer.content == null) ? 0 : contentContainer.content.Count).ToString(),
					" items</b>"
				});
				if (contentContainer.content != null)
				{
					text += "\n";
					for (int i = 0; i < contentContainer.content.Count; i++)
					{
						text = string.Concat(new string[]
						{
							text,
							"    • ",
							contentContainer.content[i].ToString(),
							": ",
							CodexEntry.GetContentWidgetDebugString(contentContainer.content[i]),
							"\n"
						});
					}
				}
				list.Add(text);
			}
			else
			{
				list.Add("null container");
			}
		}
		return list;
	}

	// Token: 0x060050C6 RID: 20678 RVA: 0x001CF4B8 File Offset: 0x001CD6B8
	private static string GetContentWidgetDebugString(ICodexWidget widget)
	{
		CodexText codexText = widget as CodexText;
		if (codexText != null)
		{
			return codexText.text;
		}
		CodexLabelWithIcon codexLabelWithIcon = widget as CodexLabelWithIcon;
		if (codexLabelWithIcon != null)
		{
			return codexLabelWithIcon.label.text + " / " + codexLabelWithIcon.icon.spriteName;
		}
		CodexImage codexImage = widget as CodexImage;
		if (codexImage != null)
		{
			return codexImage.spriteName;
		}
		CodexVideo codexVideo = widget as CodexVideo;
		if (codexVideo != null)
		{
			return codexVideo.name;
		}
		CodexIndentedLabelWithIcon codexIndentedLabelWithIcon = widget as CodexIndentedLabelWithIcon;
		if (codexIndentedLabelWithIcon != null)
		{
			return codexIndentedLabelWithIcon.label.text + " / " + codexIndentedLabelWithIcon.icon.spriteName;
		}
		return "";
	}

	// Token: 0x060050C7 RID: 20679 RVA: 0x001CF557 File Offset: 0x001CD757
	public void CreateContentContainerCollection()
	{
		this.contentContainers = new List<ContentContainer>();
	}

	// Token: 0x060050C8 RID: 20680 RVA: 0x001CF564 File Offset: 0x001CD764
	public void InsertContentContainer(int index, ContentContainer container)
	{
		this.contentContainers.Insert(index, container);
	}

	// Token: 0x060050C9 RID: 20681 RVA: 0x001CF573 File Offset: 0x001CD773
	public void RemoveContentContainerAt(int index)
	{
		this.contentContainers.RemoveAt(index);
	}

	// Token: 0x060050CA RID: 20682 RVA: 0x001CF581 File Offset: 0x001CD781
	public void AddContentContainer(ContentContainer container)
	{
		this.contentContainers.Add(container);
	}

	// Token: 0x060050CB RID: 20683 RVA: 0x001CF58F File Offset: 0x001CD78F
	public void AddContentContainerRange(IEnumerable<ContentContainer> containers)
	{
		this.contentContainers.AddRange(containers);
	}

	// Token: 0x060050CC RID: 20684 RVA: 0x001CF59D File Offset: 0x001CD79D
	public void RemoveContentContainer(ContentContainer container)
	{
		this.contentContainers.Remove(container);
	}

	// Token: 0x060050CD RID: 20685 RVA: 0x001CF5AC File Offset: 0x001CD7AC
	public ICodexWidget GetFirstWidget()
	{
		for (int i = 0; i < this.contentContainers.Count; i++)
		{
			if (this.contentContainers[i].content != null)
			{
				for (int j = 0; j < this.contentContainers[i].content.Count; j++)
				{
					if (this.contentContainers[i].content[j] != null)
					{
						return this.contentContainers[i].content[j];
					}
				}
			}
		}
		return null;
	}

	// Token: 0x170005E8 RID: 1512
	// (get) Token: 0x060050CE RID: 20686 RVA: 0x001CF635 File Offset: 0x001CD835
	// (set) Token: 0x060050CF RID: 20687 RVA: 0x001CF640 File Offset: 0x001CD840
	public string[] dlcIds
	{
		get
		{
			return this._dlcIds;
		}
		set
		{
			this._dlcIds = value;
			string text = "";
			for (int i = 0; i < value.Length; i++)
			{
				text += value[i];
				if (i != value.Length - 1)
				{
					text += "\n";
				}
			}
		}
	}

	// Token: 0x060050D0 RID: 20688 RVA: 0x001CF686 File Offset: 0x001CD886
	public string[] GetDlcIds()
	{
		if (this._dlcIds == null)
		{
			this._dlcIds = DlcManager.AVAILABLE_ALL_VERSIONS;
		}
		return this._dlcIds;
	}

	// Token: 0x170005E9 RID: 1513
	// (get) Token: 0x060050D1 RID: 20689 RVA: 0x001CF6A1 File Offset: 0x001CD8A1
	// (set) Token: 0x060050D2 RID: 20690 RVA: 0x001CF6AC File Offset: 0x001CD8AC
	public string[] forbiddenDLCIds
	{
		get
		{
			return this._forbiddenDLCIds;
		}
		set
		{
			this._forbiddenDLCIds = value;
			string text = "";
			for (int i = 0; i < value.Length; i++)
			{
				text += value[i];
				if (i != value.Length - 1)
				{
					text += "\n";
				}
			}
		}
	}

	// Token: 0x060050D3 RID: 20691 RVA: 0x001CF6F2 File Offset: 0x001CD8F2
	public string[] GetForbiddenDLCs()
	{
		if (this._forbiddenDLCIds == null)
		{
			this._forbiddenDLCIds = this.NONE;
		}
		return this._forbiddenDLCIds;
	}

	// Token: 0x170005EA RID: 1514
	// (get) Token: 0x060050D4 RID: 20692 RVA: 0x001CF70E File Offset: 0x001CD90E
	// (set) Token: 0x060050D5 RID: 20693 RVA: 0x001CF716 File Offset: 0x001CD916
	public string id
	{
		get
		{
			return this._id;
		}
		set
		{
			this._id = value;
		}
	}

	// Token: 0x170005EB RID: 1515
	// (get) Token: 0x060050D6 RID: 20694 RVA: 0x001CF71F File Offset: 0x001CD91F
	// (set) Token: 0x060050D7 RID: 20695 RVA: 0x001CF727 File Offset: 0x001CD927
	public string parentId
	{
		get
		{
			return this._parentId;
		}
		set
		{
			this._parentId = value;
		}
	}

	// Token: 0x170005EC RID: 1516
	// (get) Token: 0x060050D8 RID: 20696 RVA: 0x001CF730 File Offset: 0x001CD930
	// (set) Token: 0x060050D9 RID: 20697 RVA: 0x001CF738 File Offset: 0x001CD938
	public string category
	{
		get
		{
			return this._category;
		}
		set
		{
			this._category = value;
		}
	}

	// Token: 0x170005ED RID: 1517
	// (get) Token: 0x060050DA RID: 20698 RVA: 0x001CF741 File Offset: 0x001CD941
	// (set) Token: 0x060050DB RID: 20699 RVA: 0x001CF749 File Offset: 0x001CD949
	public string title
	{
		get
		{
			return this._title;
		}
		set
		{
			this._title = value;
		}
	}

	// Token: 0x170005EE RID: 1518
	// (get) Token: 0x060050DC RID: 20700 RVA: 0x001CF752 File Offset: 0x001CD952
	// (set) Token: 0x060050DD RID: 20701 RVA: 0x001CF75A File Offset: 0x001CD95A
	public string name
	{
		get
		{
			return this._name;
		}
		set
		{
			this._name = value;
		}
	}

	// Token: 0x170005EF RID: 1519
	// (get) Token: 0x060050DE RID: 20702 RVA: 0x001CF763 File Offset: 0x001CD963
	// (set) Token: 0x060050DF RID: 20703 RVA: 0x001CF76B File Offset: 0x001CD96B
	public string subtitle
	{
		get
		{
			return this._subtitle;
		}
		set
		{
			this._subtitle = value;
		}
	}

	// Token: 0x170005F0 RID: 1520
	// (get) Token: 0x060050E0 RID: 20704 RVA: 0x001CF774 File Offset: 0x001CD974
	// (set) Token: 0x060050E1 RID: 20705 RVA: 0x001CF77C File Offset: 0x001CD97C
	public List<SubEntry> subEntries
	{
		get
		{
			return this._subEntries;
		}
		set
		{
			this._subEntries = value;
		}
	}

	// Token: 0x170005F1 RID: 1521
	// (get) Token: 0x060050E2 RID: 20706 RVA: 0x001CF785 File Offset: 0x001CD985
	// (set) Token: 0x060050E3 RID: 20707 RVA: 0x001CF78D File Offset: 0x001CD98D
	public Sprite icon
	{
		get
		{
			return this._icon;
		}
		set
		{
			this._icon = value;
		}
	}

	// Token: 0x170005F2 RID: 1522
	// (get) Token: 0x060050E4 RID: 20708 RVA: 0x001CF796 File Offset: 0x001CD996
	// (set) Token: 0x060050E5 RID: 20709 RVA: 0x001CF79E File Offset: 0x001CD99E
	public Color iconColor
	{
		get
		{
			return this._iconColor;
		}
		set
		{
			this._iconColor = value;
		}
	}

	// Token: 0x170005F3 RID: 1523
	// (get) Token: 0x060050E6 RID: 20710 RVA: 0x001CF7A7 File Offset: 0x001CD9A7
	// (set) Token: 0x060050E7 RID: 20711 RVA: 0x001CF7AF File Offset: 0x001CD9AF
	public string iconPrefabID
	{
		get
		{
			return this._iconPrefabID;
		}
		set
		{
			this._iconPrefabID = value;
		}
	}

	// Token: 0x170005F4 RID: 1524
	// (get) Token: 0x060050E8 RID: 20712 RVA: 0x001CF7B8 File Offset: 0x001CD9B8
	// (set) Token: 0x060050E9 RID: 20713 RVA: 0x001CF7C0 File Offset: 0x001CD9C0
	public string iconLockID
	{
		get
		{
			return this._iconLockID;
		}
		set
		{
			this._iconLockID = value;
		}
	}

	// Token: 0x170005F5 RID: 1525
	// (get) Token: 0x060050EA RID: 20714 RVA: 0x001CF7C9 File Offset: 0x001CD9C9
	// (set) Token: 0x060050EB RID: 20715 RVA: 0x001CF7D1 File Offset: 0x001CD9D1
	public string iconAssetName
	{
		get
		{
			return this._iconAssetName;
		}
		set
		{
			this._iconAssetName = value;
		}
	}

	// Token: 0x170005F6 RID: 1526
	// (get) Token: 0x060050EC RID: 20716 RVA: 0x001CF7DA File Offset: 0x001CD9DA
	// (set) Token: 0x060050ED RID: 20717 RVA: 0x001CF7E2 File Offset: 0x001CD9E2
	public bool disabled
	{
		get
		{
			return this._disabled;
		}
		set
		{
			this._disabled = value;
		}
	}

	// Token: 0x170005F7 RID: 1527
	// (get) Token: 0x060050EE RID: 20718 RVA: 0x001CF7EB File Offset: 0x001CD9EB
	// (set) Token: 0x060050EF RID: 20719 RVA: 0x001CF7F3 File Offset: 0x001CD9F3
	public bool searchOnly
	{
		get
		{
			return this._searchOnly;
		}
		set
		{
			this._searchOnly = value;
		}
	}

	// Token: 0x170005F8 RID: 1528
	// (get) Token: 0x060050F0 RID: 20720 RVA: 0x001CF7FC File Offset: 0x001CD9FC
	// (set) Token: 0x060050F1 RID: 20721 RVA: 0x001CF804 File Offset: 0x001CDA04
	public int customContentLength
	{
		get
		{
			return this._customContentLength;
		}
		set
		{
			this._customContentLength = value;
		}
	}

	// Token: 0x170005F9 RID: 1529
	// (get) Token: 0x060050F2 RID: 20722 RVA: 0x001CF80D File Offset: 0x001CDA0D
	// (set) Token: 0x060050F3 RID: 20723 RVA: 0x001CF815 File Offset: 0x001CDA15
	public string sortString
	{
		get
		{
			return this._sortString;
		}
		set
		{
			this._sortString = value;
		}
	}

	// Token: 0x170005FA RID: 1530
	// (get) Token: 0x060050F4 RID: 20724 RVA: 0x001CF81E File Offset: 0x001CDA1E
	// (set) Token: 0x060050F5 RID: 20725 RVA: 0x001CF826 File Offset: 0x001CDA26
	public bool showBeforeGeneratedCategoryLinks
	{
		get
		{
			return this._showBeforeGeneratedCategoryLinks;
		}
		set
		{
			this._showBeforeGeneratedCategoryLinks = value;
		}
	}

	// Token: 0x04003647 RID: 13895
	public EntryDevLog log = new EntryDevLog();

	// Token: 0x04003648 RID: 13896
	private List<ContentContainer> _contentContainers = new List<ContentContainer>();

	// Token: 0x04003649 RID: 13897
	private string[] _dlcIds;

	// Token: 0x0400364A RID: 13898
	private string[] _forbiddenDLCIds;

	// Token: 0x0400364B RID: 13899
	private string[] NONE = new string[0];

	// Token: 0x0400364C RID: 13900
	private string _id;

	// Token: 0x0400364D RID: 13901
	private string _parentId;

	// Token: 0x0400364E RID: 13902
	private string _category;

	// Token: 0x0400364F RID: 13903
	private string _title;

	// Token: 0x04003650 RID: 13904
	private string _name;

	// Token: 0x04003651 RID: 13905
	private string _subtitle;

	// Token: 0x04003652 RID: 13906
	private List<SubEntry> _subEntries = new List<SubEntry>();

	// Token: 0x04003653 RID: 13907
	private Sprite _icon;

	// Token: 0x04003654 RID: 13908
	private Color _iconColor = Color.white;

	// Token: 0x04003655 RID: 13909
	private string _iconPrefabID;

	// Token: 0x04003656 RID: 13910
	private string _iconLockID;

	// Token: 0x04003657 RID: 13911
	private string _iconAssetName;

	// Token: 0x04003658 RID: 13912
	private bool _disabled;

	// Token: 0x04003659 RID: 13913
	private bool _searchOnly;

	// Token: 0x0400365A RID: 13914
	private int _customContentLength;

	// Token: 0x0400365B RID: 13915
	private string _sortString;

	// Token: 0x0400365C RID: 13916
	private bool _showBeforeGeneratedCategoryLinks;
}
