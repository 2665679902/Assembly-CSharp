using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x020005BB RID: 1467
public class FlatTagFilterable : KMonoBehaviour
{
	// Token: 0x06002466 RID: 9318 RVA: 0x000C4D77 File Offset: 0x000C2F77
	protected override void OnSpawn()
	{
		base.OnSpawn();
		TreeFilterable component = base.GetComponent<TreeFilterable>();
		component.filterByStorageCategoriesOnSpawn = false;
		component.UpdateFilters(new HashSet<Tag>(this.selectedTags));
	}

	// Token: 0x06002467 RID: 9319 RVA: 0x000C4D9C File Offset: 0x000C2F9C
	public void SelectTag(Tag tag, bool state)
	{
		Debug.Assert(this.tagOptions.Contains(tag), "The tag " + tag.Name + " is not valid for this filterable - it must be added to tagOptions");
		if (state)
		{
			if (!this.selectedTags.Contains(tag))
			{
				this.selectedTags.Add(tag);
			}
		}
		else if (this.selectedTags.Contains(tag))
		{
			this.selectedTags.Remove(tag);
		}
		base.GetComponent<TreeFilterable>().UpdateFilters(new HashSet<Tag>(this.selectedTags));
	}

	// Token: 0x06002468 RID: 9320 RVA: 0x000C4E20 File Offset: 0x000C3020
	public void ToggleTag(Tag tag)
	{
		this.SelectTag(tag, !this.selectedTags.Contains(tag));
	}

	// Token: 0x06002469 RID: 9321 RVA: 0x000C4E38 File Offset: 0x000C3038
	public string GetHeaderText()
	{
		return this.headerText;
	}

	// Token: 0x040014F2 RID: 5362
	[Serialize]
	public List<Tag> selectedTags = new List<Tag>();

	// Token: 0x040014F3 RID: 5363
	public List<Tag> tagOptions = new List<Tag>();

	// Token: 0x040014F4 RID: 5364
	public string headerText;
}
