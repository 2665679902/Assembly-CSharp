using System;
using System.Collections.Generic;
using Database;
using TUNING;
using UnityEngine;

// Token: 0x0200054B RID: 1355
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/ArtifactFinder")]
public class ArtifactFinder : KMonoBehaviour
{
	// Token: 0x0600204F RID: 8271 RVA: 0x000B098C File Offset: 0x000AEB8C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<ArtifactFinder>(-887025858, ArtifactFinder.OnLandDelegate);
	}

	// Token: 0x06002050 RID: 8272 RVA: 0x000B09A8 File Offset: 0x000AEBA8
	public ArtifactTier GetArtifactDropTier(StoredMinionIdentity minionID, SpaceDestination destination)
	{
		ArtifactDropRate artifactDropTable = destination.GetDestinationType().artifactDropTable;
		bool flag = minionID.traitIDs.Contains("Archaeologist");
		if (artifactDropTable != null)
		{
			float num = artifactDropTable.totalWeight;
			if (flag)
			{
				num -= artifactDropTable.GetTierWeight(DECOR.SPACEARTIFACT.TIER_NONE);
			}
			float num2 = UnityEngine.Random.value * num;
			foreach (global::Tuple<ArtifactTier, float> tuple in artifactDropTable.rates)
			{
				if (!flag || (flag && tuple.first != DECOR.SPACEARTIFACT.TIER_NONE))
				{
					num2 -= tuple.second;
				}
				if (num2 <= 0f)
				{
					return tuple.first;
				}
			}
		}
		return DECOR.SPACEARTIFACT.TIER0;
	}

	// Token: 0x06002051 RID: 8273 RVA: 0x000B0A74 File Offset: 0x000AEC74
	public List<string> GetArtifactsOfTier(ArtifactTier tier)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<ArtifactType, List<string>> keyValuePair in ArtifactConfig.artifactItems)
		{
			foreach (string text in keyValuePair.Value)
			{
				if (Assets.GetPrefab(text.ToTag()).GetComponent<SpaceArtifact>().GetArtifactTier() == tier)
				{
					list.Add(text);
				}
			}
		}
		return list;
	}

	// Token: 0x06002052 RID: 8274 RVA: 0x000B0B24 File Offset: 0x000AED24
	public string SearchForArtifact(StoredMinionIdentity minionID, SpaceDestination destination)
	{
		ArtifactTier artifactDropTier = this.GetArtifactDropTier(minionID, destination);
		if (artifactDropTier == DECOR.SPACEARTIFACT.TIER_NONE)
		{
			return null;
		}
		List<string> artifactsOfTier = this.GetArtifactsOfTier(artifactDropTier);
		return artifactsOfTier[UnityEngine.Random.Range(0, artifactsOfTier.Count)];
	}

	// Token: 0x06002053 RID: 8275 RVA: 0x000B0B60 File Offset: 0x000AED60
	public void OnLand(object data)
	{
		SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(SpacecraftManager.instance.GetSpacecraftID(base.GetComponent<RocketModule>().conditionManager.GetComponent<ILaunchableRocket>()));
		foreach (MinionStorage.Info info in this.minionStorage.GetStoredMinionInfo())
		{
			StoredMinionIdentity storedMinionIdentity = info.serializedMinion.Get<StoredMinionIdentity>();
			string text = this.SearchForArtifact(storedMinionIdentity, spacecraftDestination);
			if (text != null)
			{
				GameUtil.KInstantiate(Assets.GetPrefab(text.ToTag()), base.gameObject.transform.GetPosition(), Grid.SceneLayer.Ore, null, 0).SetActive(true);
			}
		}
	}

	// Token: 0x04001297 RID: 4759
	public const string ID = "ArtifactFinder";

	// Token: 0x04001298 RID: 4760
	[MyCmpReq]
	private MinionStorage minionStorage;

	// Token: 0x04001299 RID: 4761
	private static readonly EventSystem.IntraObjectHandler<ArtifactFinder> OnLandDelegate = new EventSystem.IntraObjectHandler<ArtifactFinder>(delegate(ArtifactFinder component, object data)
	{
		component.OnLand(data);
	});
}
