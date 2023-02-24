using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000538 RID: 1336
public static class FXHelpers
{
	// Token: 0x06002002 RID: 8194 RVA: 0x000AEAE4 File Offset: 0x000ACCE4
	public static KBatchedAnimController CreateEffect(string anim_file_name, Vector3 position, Transform parent = null, bool update_looping_sounds_position = false, Grid.SceneLayer layer = Grid.SceneLayer.Front, bool set_inactive = false)
	{
		KBatchedAnimController component = GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.EffectTemplateId), position, layer, null, 0).GetComponent<KBatchedAnimController>();
		component.GetComponent<KPrefabID>().PrefabTag = TagManager.Create(anim_file_name);
		component.name = anim_file_name;
		if (parent != null)
		{
			component.transform.SetParent(parent, false);
		}
		component.transform.SetPosition(position);
		if (update_looping_sounds_position)
		{
			component.FindOrAddComponent<LoopingSounds>().updatePosition = true;
		}
		KAnimFile anim = Assets.GetAnim(anim_file_name);
		if (anim == null)
		{
			global::Debug.LogWarning("Missing effect anim: " + anim_file_name);
		}
		else
		{
			component.AnimFiles = new KAnimFile[] { anim };
		}
		if (!set_inactive)
		{
			component.gameObject.SetActive(true);
		}
		return component;
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x000AEBA4 File Offset: 0x000ACDA4
	public static KBatchedAnimController CreateEffect(string[] anim_file_names, Vector3 position, Transform parent = null, bool update_looping_sounds_position = false, Grid.SceneLayer layer = Grid.SceneLayer.Front, bool set_inactive = false)
	{
		KBatchedAnimController component = GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.EffectTemplateId), position, layer, null, 0).GetComponent<KBatchedAnimController>();
		component.GetComponent<KPrefabID>().PrefabTag = TagManager.Create(anim_file_names[0]);
		component.name = anim_file_names[0];
		if (parent != null)
		{
			component.transform.SetParent(parent, false);
		}
		component.transform.SetPosition(position);
		if (update_looping_sounds_position)
		{
			component.FindOrAddComponent<LoopingSounds>().updatePosition = true;
		}
		component.AnimFiles = (from e in anim_file_names.Select((string name) => new ValueTuple<string, KAnimFile>(name, Assets.GetAnim(name))).Where(delegate([TupleElementNames(new string[] { "name", "anim" })] ValueTuple<string, KAnimFile> e)
			{
				if (e.Item2 == null)
				{
					global::Debug.LogWarning("Missing effect anim: " + e.Item1);
					return false;
				}
				return true;
			})
			select e.Item2).ToArray<KAnimFile>();
		if (!set_inactive)
		{
			component.gameObject.SetActive(true);
		}
		return component;
	}

	// Token: 0x06002004 RID: 8196 RVA: 0x000AECA8 File Offset: 0x000ACEA8
	public static KBatchedAnimController CreateEffectOverride(string[] anim_file_names, Vector3 position, Transform parent = null, bool update_looping_sounds_position = false, Grid.SceneLayer layer = Grid.SceneLayer.Front, bool set_inactive = false)
	{
		KBatchedAnimController component = GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.EffectTemplateOverrideId), position, layer, null, 0).GetComponent<KBatchedAnimController>();
		component.GetComponent<KPrefabID>().PrefabTag = TagManager.Create(anim_file_names[0]);
		component.name = anim_file_names[0];
		if (parent != null)
		{
			component.transform.SetParent(parent, false);
		}
		component.transform.SetPosition(position);
		if (update_looping_sounds_position)
		{
			component.FindOrAddComponent<LoopingSounds>().updatePosition = true;
		}
		component.AnimFiles = (from e in anim_file_names.Select((string name) => new ValueTuple<string, KAnimFile>(name, Assets.GetAnim(name))).Where(delegate([TupleElementNames(new string[] { "name", "anim" })] ValueTuple<string, KAnimFile> e)
			{
				if (e.Item2 == null)
				{
					global::Debug.LogWarning("Missing effect anim: " + e.Item1);
					return false;
				}
				return true;
			})
			select e.Item2).ToArray<KAnimFile>();
		if (!set_inactive)
		{
			component.gameObject.SetActive(true);
		}
		return component;
	}
}
