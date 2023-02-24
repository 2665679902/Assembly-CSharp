using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200071D RID: 1821
[AddComponentMenu("KMonoBehaviour/scripts/DietManager")]
public class DietManager : KMonoBehaviour
{
	// Token: 0x060031C8 RID: 12744 RVA: 0x0010A51A File Offset: 0x0010871A
	public static void DestroyInstance()
	{
		DietManager.Instance = null;
	}

	// Token: 0x060031C9 RID: 12745 RVA: 0x0010A522 File Offset: 0x00108722
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.diets = DietManager.CollectDiets(null);
		DietManager.Instance = this;
	}

	// Token: 0x060031CA RID: 12746 RVA: 0x0010A53C File Offset: 0x0010873C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (Tag tag in DiscoveredResources.Instance.GetDiscovered())
		{
			this.Discover(tag);
		}
		foreach (KeyValuePair<Tag, Diet> keyValuePair in this.diets)
		{
			Diet.Info[] infos = keyValuePair.Value.infos;
			for (int i = 0; i < infos.Length; i++)
			{
				foreach (Tag tag2 in infos[i].consumedTags)
				{
					if (Assets.GetPrefab(tag2) == null)
					{
						global::Debug.LogError(string.Format("Could not find prefab {0}, required by diet for {1}", tag2, keyValuePair.Key));
					}
				}
			}
		}
		DiscoveredResources.Instance.OnDiscover += this.OnWorldInventoryDiscover;
	}

	// Token: 0x060031CB RID: 12747 RVA: 0x0010A684 File Offset: 0x00108884
	private void Discover(Tag tag)
	{
		foreach (KeyValuePair<Tag, Diet> keyValuePair in this.diets)
		{
			if (keyValuePair.Value.GetDietInfo(tag) != null)
			{
				DiscoveredResources.Instance.Discover(tag, keyValuePair.Key);
			}
		}
	}

	// Token: 0x060031CC RID: 12748 RVA: 0x0010A6F4 File Offset: 0x001088F4
	private void OnWorldInventoryDiscover(Tag category_tag, Tag tag)
	{
		this.Discover(tag);
	}

	// Token: 0x060031CD RID: 12749 RVA: 0x0010A700 File Offset: 0x00108900
	public static Dictionary<Tag, Diet> CollectDiets(Tag[] target_species)
	{
		Dictionary<Tag, Diet> dictionary = new Dictionary<Tag, Diet>();
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			CreatureCalorieMonitor.Def def = kprefabID.GetDef<CreatureCalorieMonitor.Def>();
			BeehiveCalorieMonitor.Def def2 = kprefabID.GetDef<BeehiveCalorieMonitor.Def>();
			Diet diet = null;
			if (def != null)
			{
				diet = def.diet;
			}
			else if (def2 != null)
			{
				diet = def2.diet;
			}
			if (diet != null && (target_species == null || Array.IndexOf<Tag>(target_species, kprefabID.GetComponent<CreatureBrain>().species) >= 0))
			{
				dictionary[kprefabID.PrefabTag] = diet;
			}
		}
		return dictionary;
	}

	// Token: 0x04001E3F RID: 7743
	private Dictionary<Tag, Diet> diets;

	// Token: 0x04001E40 RID: 7744
	public static DietManager Instance;
}
