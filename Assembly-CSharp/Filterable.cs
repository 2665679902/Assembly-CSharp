using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020005B8 RID: 1464
[AddComponentMenu("KMonoBehaviour/scripts/Filterable")]
public class Filterable : KMonoBehaviour
{
	// Token: 0x14000011 RID: 17
	// (add) Token: 0x0600243D RID: 9277 RVA: 0x000C42E4 File Offset: 0x000C24E4
	// (remove) Token: 0x0600243E RID: 9278 RVA: 0x000C431C File Offset: 0x000C251C
	public event Action<Tag> onFilterChanged;

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x0600243F RID: 9279 RVA: 0x000C4351 File Offset: 0x000C2551
	// (set) Token: 0x06002440 RID: 9280 RVA: 0x000C4359 File Offset: 0x000C2559
	public Tag SelectedTag
	{
		get
		{
			return this.selectedTag;
		}
		set
		{
			this.selectedTag = value;
			this.OnFilterChanged();
		}
	}

	// Token: 0x06002441 RID: 9281 RVA: 0x000C4368 File Offset: 0x000C2568
	public Dictionary<Tag, HashSet<Tag>> GetTagOptions()
	{
		Dictionary<Tag, HashSet<Tag>> dictionary = new Dictionary<Tag, HashSet<Tag>>();
		if (this.filterElementState == Filterable.ElementState.Solid)
		{
			dictionary = DiscoveredResources.Instance.GetDiscoveredResourcesFromTagSet(Filterable.filterableCategories);
		}
		else
		{
			foreach (Element element in ElementLoader.elements)
			{
				if (!element.disabled && ((element.IsGas && this.filterElementState == Filterable.ElementState.Gas) || (element.IsLiquid && this.filterElementState == Filterable.ElementState.Liquid)))
				{
					Tag materialCategoryTag = element.GetMaterialCategoryTag();
					if (!dictionary.ContainsKey(materialCategoryTag))
					{
						dictionary[materialCategoryTag] = new HashSet<Tag>();
					}
					Tag tag = GameTagExtensions.Create(element.id);
					dictionary[materialCategoryTag].Add(tag);
				}
			}
		}
		dictionary.Add(GameTags.Void, new HashSet<Tag> { GameTags.Void });
		return dictionary;
	}

	// Token: 0x06002442 RID: 9282 RVA: 0x000C4458 File Offset: 0x000C2658
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Filterable>(-905833192, Filterable.OnCopySettingsDelegate);
	}

	// Token: 0x06002443 RID: 9283 RVA: 0x000C4474 File Offset: 0x000C2674
	private void OnCopySettings(object data)
	{
		Filterable component = ((GameObject)data).GetComponent<Filterable>();
		if (component != null)
		{
			this.SelectedTag = component.SelectedTag;
		}
	}

	// Token: 0x06002444 RID: 9284 RVA: 0x000C44A2 File Offset: 0x000C26A2
	protected override void OnSpawn()
	{
		this.OnFilterChanged();
	}

	// Token: 0x06002445 RID: 9285 RVA: 0x000C44AC File Offset: 0x000C26AC
	private void OnFilterChanged()
	{
		if (this.onFilterChanged != null)
		{
			this.onFilterChanged(this.selectedTag);
		}
		Operational component = base.GetComponent<Operational>();
		if (component != null)
		{
			component.SetFlag(Filterable.filterSelected, this.selectedTag.IsValid);
		}
	}

	// Token: 0x040014DC RID: 5340
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x040014DD RID: 5341
	[Serialize]
	public Filterable.ElementState filterElementState;

	// Token: 0x040014DE RID: 5342
	[Serialize]
	private Tag selectedTag = GameTags.Void;

	// Token: 0x040014E0 RID: 5344
	private static TagSet filterableCategories = new TagSet(new TagSet[]
	{
		GameTags.CalorieCategories,
		GameTags.UnitCategories,
		GameTags.MaterialCategories,
		GameTags.MaterialBuildingElements
	});

	// Token: 0x040014E1 RID: 5345
	private static readonly Operational.Flag filterSelected = new Operational.Flag("filterSelected", Operational.Flag.Type.Requirement);

	// Token: 0x040014E2 RID: 5346
	private static readonly EventSystem.IntraObjectHandler<Filterable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Filterable>(delegate(Filterable component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x020011F1 RID: 4593
	public enum ElementState
	{
		// Token: 0x04005C7C RID: 23676
		None,
		// Token: 0x04005C7D RID: 23677
		Solid,
		// Token: 0x04005C7E RID: 23678
		Liquid,
		// Token: 0x04005C7F RID: 23679
		Gas
	}
}
