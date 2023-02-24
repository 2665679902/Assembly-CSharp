using System;
using System.Collections.Generic;
using Database;
using UnityEngine;

// Token: 0x020008DD RID: 2269
public class Tech : Resource
{
	// Token: 0x17000494 RID: 1172
	// (get) Token: 0x06004155 RID: 16725 RVA: 0x0016E5BC File Offset: 0x0016C7BC
	public bool FoundNode
	{
		get
		{
			return this.node != null;
		}
	}

	// Token: 0x17000495 RID: 1173
	// (get) Token: 0x06004156 RID: 16726 RVA: 0x0016E5C7 File Offset: 0x0016C7C7
	public Vector2 center
	{
		get
		{
			return this.node.center;
		}
	}

	// Token: 0x17000496 RID: 1174
	// (get) Token: 0x06004157 RID: 16727 RVA: 0x0016E5D4 File Offset: 0x0016C7D4
	public float width
	{
		get
		{
			return this.node.width;
		}
	}

	// Token: 0x17000497 RID: 1175
	// (get) Token: 0x06004158 RID: 16728 RVA: 0x0016E5E1 File Offset: 0x0016C7E1
	public float height
	{
		get
		{
			return this.node.height;
		}
	}

	// Token: 0x17000498 RID: 1176
	// (get) Token: 0x06004159 RID: 16729 RVA: 0x0016E5EE File Offset: 0x0016C7EE
	public List<ResourceTreeNode.Edge> edges
	{
		get
		{
			return this.node.edges;
		}
	}

	// Token: 0x0600415A RID: 16730 RVA: 0x0016E5FC File Offset: 0x0016C7FC
	public Tech(string id, List<string> unlockedItemIDs, Techs techs, Dictionary<string, float> overrideDefaultCosts = null)
		: base(id, techs, Strings.Get("STRINGS.RESEARCH.TECHS." + id.ToUpper() + ".NAME"))
	{
		this.desc = Strings.Get("STRINGS.RESEARCH.TECHS." + id.ToUpper() + ".DESC");
		this.unlockedItemIDs = unlockedItemIDs;
		if (overrideDefaultCosts != null && DlcManager.IsExpansion1Active())
		{
			foreach (KeyValuePair<string, float> keyValuePair in overrideDefaultCosts)
			{
				this.costsByResearchTypeID.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	// Token: 0x0600415B RID: 16731 RVA: 0x0016E6F4 File Offset: 0x0016C8F4
	public void AddUnlockedItemIDs(params string[] ids)
	{
		foreach (string text in ids)
		{
			this.unlockedItemIDs.Add(text);
		}
	}

	// Token: 0x0600415C RID: 16732 RVA: 0x0016E724 File Offset: 0x0016C924
	public void RemoveUnlockedItemIDs(params string[] ids)
	{
		foreach (string text in ids)
		{
			if (!this.unlockedItemIDs.Remove(text))
			{
				DebugUtil.DevLogError("Tech item '" + text + "' does not exist to remove");
			}
		}
	}

	// Token: 0x0600415D RID: 16733 RVA: 0x0016E768 File Offset: 0x0016C968
	public bool RequiresResearchType(string type)
	{
		return this.costsByResearchTypeID.ContainsKey(type);
	}

	// Token: 0x0600415E RID: 16734 RVA: 0x0016E776 File Offset: 0x0016C976
	public void SetNode(ResourceTreeNode node, string categoryID)
	{
		this.node = node;
		this.category = categoryID;
	}

	// Token: 0x0600415F RID: 16735 RVA: 0x0016E788 File Offset: 0x0016C988
	public bool CanAfford(ResearchPointInventory pointInventory)
	{
		foreach (KeyValuePair<string, float> keyValuePair in this.costsByResearchTypeID)
		{
			if (pointInventory.PointsByTypeID[keyValuePair.Key] < keyValuePair.Value)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06004160 RID: 16736 RVA: 0x0016E7F8 File Offset: 0x0016C9F8
	public string CostString(ResearchTypes types)
	{
		string text = "";
		foreach (KeyValuePair<string, float> keyValuePair in this.costsByResearchTypeID)
		{
			text += string.Format("{0}:{1}", types.GetResearchType(keyValuePair.Key).name.ToString(), keyValuePair.Value.ToString());
			text += "\n";
		}
		return text;
	}

	// Token: 0x06004161 RID: 16737 RVA: 0x0016E890 File Offset: 0x0016CA90
	public bool IsComplete()
	{
		if (Research.Instance != null)
		{
			TechInstance techInstance = Research.Instance.Get(this);
			return techInstance != null && techInstance.IsComplete();
		}
		return false;
	}

	// Token: 0x06004162 RID: 16738 RVA: 0x0016E8C4 File Offset: 0x0016CAC4
	public bool ArePrerequisitesComplete()
	{
		using (List<Tech>.Enumerator enumerator = this.requiredTech.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsComplete())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x04002B96 RID: 11158
	public List<Tech> requiredTech = new List<Tech>();

	// Token: 0x04002B97 RID: 11159
	public List<Tech> unlockedTech = new List<Tech>();

	// Token: 0x04002B98 RID: 11160
	public List<TechItem> unlockedItems = new List<TechItem>();

	// Token: 0x04002B99 RID: 11161
	public List<string> unlockedItemIDs = new List<string>();

	// Token: 0x04002B9A RID: 11162
	public int tier;

	// Token: 0x04002B9B RID: 11163
	public Dictionary<string, float> costsByResearchTypeID = new Dictionary<string, float>();

	// Token: 0x04002B9C RID: 11164
	public string desc;

	// Token: 0x04002B9D RID: 11165
	public string category;

	// Token: 0x04002B9E RID: 11166
	public Tag[] tags;

	// Token: 0x04002B9F RID: 11167
	private ResourceTreeNode node;
}
