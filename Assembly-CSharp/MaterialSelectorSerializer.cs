using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000B0F RID: 2831
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/MaterialSelectorSerializer")]
public class MaterialSelectorSerializer : KMonoBehaviour
{
	// Token: 0x06005751 RID: 22353 RVA: 0x001FC7A8 File Offset: 0x001FA9A8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.previouslySelectedElementsPerWorld == null)
		{
			this.previouslySelectedElementsPerWorld = new List<Dictionary<Tag, Tag>>[(int)ClusterManager.INVALID_WORLD_IDX];
			if (this.previouslySelectedElements != null)
			{
				foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
				{
					List<Dictionary<Tag, Tag>> list = this.previouslySelectedElements.ConvertAll<Dictionary<Tag, Tag>>((Dictionary<Tag, Tag> input) => new Dictionary<Tag, Tag>(input));
					this.previouslySelectedElementsPerWorld[worldContainer.id] = list;
				}
				this.previouslySelectedElements = null;
			}
		}
	}

	// Token: 0x06005752 RID: 22354 RVA: 0x001FC85C File Offset: 0x001FAA5C
	public void WipeWorldSelectionData(int worldID)
	{
		this.previouslySelectedElementsPerWorld[worldID] = null;
	}

	// Token: 0x06005753 RID: 22355 RVA: 0x001FC868 File Offset: 0x001FAA68
	public void SetSelectedElement(int worldID, int selectorIndex, Tag recipe, Tag element)
	{
		if (this.previouslySelectedElementsPerWorld[worldID] == null)
		{
			this.previouslySelectedElementsPerWorld[worldID] = new List<Dictionary<Tag, Tag>>();
		}
		List<Dictionary<Tag, Tag>> list = this.previouslySelectedElementsPerWorld[worldID];
		while (list.Count <= selectorIndex)
		{
			list.Add(new Dictionary<Tag, Tag>());
		}
		list[selectorIndex][recipe] = element;
	}

	// Token: 0x06005754 RID: 22356 RVA: 0x001FC8BC File Offset: 0x001FAABC
	public Tag GetPreviousElement(int worldID, int selectorIndex, Tag recipe)
	{
		Tag invalid = Tag.Invalid;
		if (this.previouslySelectedElementsPerWorld[worldID] == null)
		{
			return invalid;
		}
		List<Dictionary<Tag, Tag>> list = this.previouslySelectedElementsPerWorld[worldID];
		if (list.Count <= selectorIndex)
		{
			return invalid;
		}
		list[selectorIndex].TryGetValue(recipe, out invalid);
		return invalid;
	}

	// Token: 0x04003B49 RID: 15177
	[Serialize]
	private List<Dictionary<Tag, Tag>> previouslySelectedElements;

	// Token: 0x04003B4A RID: 15178
	[Serialize]
	private List<Dictionary<Tag, Tag>>[] previouslySelectedElementsPerWorld;
}
