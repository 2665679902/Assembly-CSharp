using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BB4 RID: 2996
public class FlatTagFilterSideScreen : SideScreenContent
{
	// Token: 0x06005E32 RID: 24114 RVA: 0x00226A71 File Offset: 0x00224C71
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<FlatTagFilterable>() != null;
	}

	// Token: 0x06005E33 RID: 24115 RVA: 0x00226A7F File Offset: 0x00224C7F
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.tagFilterable = target.GetComponent<FlatTagFilterable>();
		this.Build();
	}

	// Token: 0x06005E34 RID: 24116 RVA: 0x00226A9C File Offset: 0x00224C9C
	private void Build()
	{
		this.headerLabel.SetText(this.tagFilterable.GetHeaderText());
		foreach (KeyValuePair<Tag, GameObject> keyValuePair in this.rows)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.rows.Clear();
		foreach (Tag tag in this.tagFilterable.tagOptions)
		{
			GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.listContainer, false);
			gameObject.gameObject.name = tag.ProperName();
			this.rows.Add(tag, gameObject);
		}
		this.Refresh();
	}

	// Token: 0x06005E35 RID: 24117 RVA: 0x00226B90 File Offset: 0x00224D90
	private void Refresh()
	{
		using (Dictionary<Tag, GameObject>.Enumerator enumerator = this.rows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<Tag, GameObject> kvp = enumerator.Current;
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(kvp.Key.ProperNameStripLink());
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").sprite = Def.GetUISprite(kvp.Key, "ui", false).first;
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").color = Def.GetUISprite(kvp.Key, "ui", false).second;
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").onClick = delegate
				{
					this.tagFilterable.ToggleTag(kvp.Key);
					this.Refresh();
				};
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").ChangeState(this.tagFilterable.selectedTags.Contains(kvp.Key) ? 1 : 0);
				kvp.Value.SetActive(DiscoveredResources.Instance.IsDiscovered(kvp.Key));
			}
		}
	}

	// Token: 0x0400406F RID: 16495
	private FlatTagFilterable tagFilterable;

	// Token: 0x04004070 RID: 16496
	[SerializeField]
	private GameObject rowPrefab;

	// Token: 0x04004071 RID: 16497
	[SerializeField]
	private GameObject listContainer;

	// Token: 0x04004072 RID: 16498
	[SerializeField]
	private LocText headerLabel;

	// Token: 0x04004073 RID: 16499
	private Dictionary<Tag, GameObject> rows = new Dictionary<Tag, GameObject>();
}
