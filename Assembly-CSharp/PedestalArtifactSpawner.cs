using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000888 RID: 2184
public class PedestalArtifactSpawner : KMonoBehaviour
{
	// Token: 0x06003EA6 RID: 16038 RVA: 0x0015E3C4 File Offset: 0x0015C5C4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (GameObject gameObject in this.storage.items)
		{
			if (ArtifactSelector.Instance.GetArtifactType(gameObject.name) == ArtifactType.Terrestrial)
			{
				gameObject.GetComponent<KPrefabID>().AddTag(GameTags.TerrestrialArtifact, true);
			}
		}
		if (this.artifactSpawned)
		{
			return;
		}
		GameObject gameObject2 = Util.KInstantiate(Assets.GetPrefab(ArtifactSelector.Instance.GetUniqueArtifactID(ArtifactType.Terrestrial)), base.transform.position);
		gameObject2.SetActive(true);
		gameObject2.GetComponent<KPrefabID>().AddTag(GameTags.TerrestrialArtifact, true);
		this.storage.Store(gameObject2, false, false, true, false);
		this.receptacle.ForceDeposit(gameObject2);
		this.artifactSpawned = true;
	}

	// Token: 0x040028FC RID: 10492
	[MyCmpReq]
	private Storage storage;

	// Token: 0x040028FD RID: 10493
	[MyCmpReq]
	private SingleEntityReceptacle receptacle;

	// Token: 0x040028FE RID: 10494
	[Serialize]
	private bool artifactSpawned;
}
