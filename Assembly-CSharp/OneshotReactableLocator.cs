using System;
using UnityEngine;

// Token: 0x02000262 RID: 610
public class OneshotReactableLocator : IEntityConfig
{
	// Token: 0x06000C1A RID: 3098 RVA: 0x00043FE4 File Offset: 0x000421E4
	public static EmoteReactable CreateOneshotReactable(GameObject source, float lifetime, string id, ChoreType chore_type, int range_width = 15, int range_height = 15, float min_reactor_time = 20f)
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(OneshotReactableLocator.ID), source.transform.GetPosition());
		EmoteReactable emoteReactable = new EmoteReactable(gameObject, id, chore_type, range_width, range_height, 100000f, min_reactor_time, float.PositiveInfinity, 0f);
		emoteReactable.AddPrecondition(OneshotReactableLocator.ReactorIsNotSource(source));
		OneshotReactableHost component = gameObject.GetComponent<OneshotReactableHost>();
		component.lifetime = lifetime;
		component.SetReactable(emoteReactable);
		gameObject.SetActive(true);
		return emoteReactable;
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x0004405A File Offset: 0x0004225A
	private static Reactable.ReactablePrecondition ReactorIsNotSource(GameObject source)
	{
		return (GameObject reactor, Navigator.ActiveTransition transition) => reactor != source;
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00044073 File Offset: 0x00042273
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x0004407A File Offset: 0x0004227A
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(OneshotReactableLocator.ID, OneshotReactableLocator.ID, false);
		gameObject.AddTag(GameTags.NotConversationTopic);
		gameObject.AddOrGet<OneshotReactableHost>();
		return gameObject;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0004409E File Offset: 0x0004229E
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x000440A0 File Offset: 0x000422A0
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000710 RID: 1808
	public static readonly string ID = "OneshotReactableLocator";
}
