﻿using System;
using System.Collections.Generic;
using KSerialization;
using ProcGenGame;
using UnityEngine;

// Token: 0x02000690 RID: 1680
[SerializationConfig(MemberSerialization.OptIn)]
public class ClusterPOIManager : KMonoBehaviour
{
	// Token: 0x06002D92 RID: 11666 RVA: 0x000EF79F File Offset: 0x000ED99F
	private ClusterFogOfWarManager.Instance GetFOWManager()
	{
		if (this.m_fowManager == null)
		{
			this.m_fowManager = SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>();
		}
		return this.m_fowManager;
	}

	// Token: 0x06002D93 RID: 11667 RVA: 0x000EF7BF File Offset: 0x000ED9BF
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			UIScheduler.Instance.ScheduleNextFrame("UpgradeOldSaves", delegate(object o)
			{
				this.UpgradeOldSaves();
			}, null, null);
		}
	}

	// Token: 0x06002D94 RID: 11668 RVA: 0x000EF7EC File Offset: 0x000ED9EC
	public void RegisterTemporalTear(TemporalTear temporalTear)
	{
		this.m_temporalTear.Set(temporalTear);
	}

	// Token: 0x06002D95 RID: 11669 RVA: 0x000EF7FA File Offset: 0x000ED9FA
	public bool HasTemporalTear()
	{
		return this.m_temporalTear.Get() != null;
	}

	// Token: 0x06002D96 RID: 11670 RVA: 0x000EF80D File Offset: 0x000EDA0D
	public TemporalTear GetTemporalTear()
	{
		return this.m_temporalTear.Get();
	}

	// Token: 0x06002D97 RID: 11671 RVA: 0x000EF81C File Offset: 0x000EDA1C
	private void UpgradeOldSaves()
	{
		bool flag = false;
		foreach (KeyValuePair<AxialI, List<ClusterGridEntity>> keyValuePair in ClusterGrid.Instance.cellContents)
		{
			foreach (ClusterGridEntity clusterGridEntity in keyValuePair.Value)
			{
				if (clusterGridEntity.GetComponent<HarvestablePOIClusterGridEntity>() || clusterGridEntity.GetComponent<ArtifactPOIClusterGridEntity>())
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				break;
			}
		}
		if (!flag)
		{
			ClusterManager.Instance.GetClusterPOIManager().SpawnSpacePOIsInLegacySave();
		}
	}

	// Token: 0x06002D98 RID: 11672 RVA: 0x000EF8E4 File Offset: 0x000EDAE4
	public void SpawnSpacePOIsInLegacySave()
	{
		Dictionary<int[], string[]> dictionary = new Dictionary<int[], string[]>();
		dictionary.Add(new int[] { 2, 3 }, new string[] { "HarvestableSpacePOI_SandyOreField" });
		dictionary.Add(new int[] { 5, 7 }, new string[] { "HarvestableSpacePOI_OrganicMassField" });
		dictionary.Add(new int[] { 8, 11 }, new string[] { "HarvestableSpacePOI_GildedAsteroidField", "HarvestableSpacePOI_GlimmeringAsteroidField", "HarvestableSpacePOI_HeliumCloud", "HarvestableSpacePOI_OilyAsteroidField", "HarvestableSpacePOI_FrozenOreField" });
		dictionary.Add(new int[] { 10, 11 }, new string[] { "HarvestableSpacePOI_RadioactiveGasCloud", "HarvestableSpacePOI_RadioactiveAsteroidField" });
		dictionary.Add(new int[] { 5, 7 }, new string[] { "HarvestableSpacePOI_RockyAsteroidField", "HarvestableSpacePOI_InterstellarIceField", "HarvestableSpacePOI_InterstellarOcean", "HarvestableSpacePOI_SandyOreField", "HarvestableSpacePOI_SwampyOreField" });
		dictionary.Add(new int[] { 7, 11 }, new string[] { "HarvestableSpacePOI_MetallicAsteroidField", "HarvestableSpacePOI_SatelliteField", "HarvestableSpacePOI_ChlorineCloud", "HarvestableSpacePOI_OxidizedAsteroidField", "HarvestableSpacePOI_OxygenRichAsteroidField", "HarvestableSpacePOI_GildedAsteroidField", "HarvestableSpacePOI_HeliumCloud", "HarvestableSpacePOI_OilyAsteroidField", "HarvestableSpacePOI_FrozenOreField", "HarvestableSpacePOI_RadioactiveAsteroidField" });
		List<AxialI> list = new List<AxialI>();
		string[] array;
		foreach (KeyValuePair<int[], string[]> keyValuePair in dictionary)
		{
			int[] key = keyValuePair.Key;
			string[] value = keyValuePair.Value;
			int num = Mathf.Min(key[0], ClusterGrid.Instance.numRings - 1);
			int num2 = Mathf.Min(key[1], ClusterGrid.Instance.numRings - 1);
			List<AxialI> rings = AxialUtil.GetRings(AxialI.ZERO, num, num2);
			List<AxialI> list2 = new List<AxialI>();
			foreach (AxialI axialI in rings)
			{
				ClusterGrid instance = ClusterGrid.Instance;
				Dictionary<AxialI, List<ClusterGridEntity>> cellContents = ClusterGrid.Instance.cellContents;
				List<ClusterGridEntity> list3 = ClusterGrid.Instance.cellContents[axialI];
				if (ClusterGrid.Instance.cellContents[axialI].Count == 0 && ClusterGrid.Instance.GetVisibleEntityOfLayerAtAdjacentCell(axialI, EntityLayer.Asteroid) == null)
				{
					list2.Add(axialI);
				}
			}
			array = value;
			for (int i = 0; i < array.Length; i++)
			{
				GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(array[i]), null, null);
				AxialI axialI2 = list2[UnityEngine.Random.Range(0, list2.Count - 1)];
				list2.Remove(axialI2);
				list.Add(axialI2);
				gameObject.GetComponent<ClusterGridEntity>().Location = axialI2;
				gameObject.SetActive(true);
			}
		}
		string[] array2 = new string[] { "ArtifactSpacePOI_GravitasSpaceStation1", "ArtifactSpacePOI_GravitasSpaceStation4", "ArtifactSpacePOI_GravitasSpaceStation5", "ArtifactSpacePOI_GravitasSpaceStation6", "ArtifactSpacePOI_GravitasSpaceStation8", "ArtifactSpacePOI_RussellsTeapot" };
		int num3 = Mathf.Min(2, ClusterGrid.Instance.numRings - 1);
		int num4 = Mathf.Min(11, ClusterGrid.Instance.numRings - 1);
		List<AxialI> rings2 = AxialUtil.GetRings(AxialI.ZERO, num3, num4);
		List<AxialI> list4 = new List<AxialI>();
		foreach (AxialI axialI3 in rings2)
		{
			if (ClusterGrid.Instance.cellContents[axialI3].Count == 0 && ClusterGrid.Instance.GetVisibleEntityOfLayerAtAdjacentCell(axialI3, EntityLayer.Asteroid) == null && !list.Contains(axialI3))
			{
				list4.Add(axialI3);
			}
		}
		array = array2;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject2 = Util.KInstantiate(Assets.GetPrefab(array[i]), null, null);
			AxialI axialI4 = list4[UnityEngine.Random.Range(0, list4.Count - 1)];
			list4.Remove(axialI4);
			HarvestablePOIClusterGridEntity component = gameObject2.GetComponent<HarvestablePOIClusterGridEntity>();
			if (component != null)
			{
				component.Init(axialI4);
			}
			ArtifactPOIClusterGridEntity component2 = gameObject2.GetComponent<ArtifactPOIClusterGridEntity>();
			if (component2 != null)
			{
				component2.Init(axialI4);
			}
			gameObject2.SetActive(true);
		}
	}

	// Token: 0x06002D99 RID: 11673 RVA: 0x000EFD88 File Offset: 0x000EDF88
	public void PopulatePOIsFromWorldGen(Cluster clusterLayout)
	{
		foreach (KeyValuePair<AxialI, string> keyValuePair in clusterLayout.poiPlacements)
		{
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(keyValuePair.Value), null, null);
			gameObject.GetComponent<ClusterGridEntity>().Location = keyValuePair.Key;
			gameObject.SetActive(true);
		}
	}

	// Token: 0x06002D9A RID: 11674 RVA: 0x000EFE04 File Offset: 0x000EE004
	public void RevealTemporalTear()
	{
		if (this.m_temporalTear.Get() == null)
		{
			global::Debug.LogWarning("This cluster has no temporal tear, but has the poi mechanism to reveal it");
			return;
		}
		AxialI location = this.m_temporalTear.Get().Location;
		this.GetFOWManager().RevealLocation(location, 1);
	}

	// Token: 0x06002D9B RID: 11675 RVA: 0x000EFE4D File Offset: 0x000EE04D
	public bool IsTemporalTearRevealed()
	{
		if (this.m_temporalTear.Get() == null)
		{
			global::Debug.LogWarning("This cluster has no temporal tear, but has the poi mechanism to reveal it");
			return false;
		}
		return this.GetFOWManager().IsLocationRevealed(this.m_temporalTear.Get().Location);
	}

	// Token: 0x06002D9C RID: 11676 RVA: 0x000EFE8C File Offset: 0x000EE08C
	public void OpenTemporalTear(int openerWorldId)
	{
		if (this.m_temporalTear.Get() == null)
		{
			global::Debug.LogWarning("This cluster has no temporal tear, but has the poi mechanism to open it");
			return;
		}
		if (!this.m_temporalTear.Get().IsOpen())
		{
			this.m_temporalTear.Get().Open();
			ClusterManager.Instance.GetWorld(openerWorldId).GetSMI<GameplaySeasonManager.Instance>().StartNewSeason(Db.Get().GameplaySeasons.TemporalTearMeteorShowers);
		}
	}

	// Token: 0x06002D9D RID: 11677 RVA: 0x000EFEFD File Offset: 0x000EE0FD
	public bool HasTemporalTearConsumedCraft()
	{
		return !(this.m_temporalTear.Get() == null) && this.m_temporalTear.Get().HasConsumedCraft();
	}

	// Token: 0x06002D9E RID: 11678 RVA: 0x000EFF24 File Offset: 0x000EE124
	public bool IsTemporalTearOpen()
	{
		return !(this.m_temporalTear.Get() == null) && this.m_temporalTear.Get().IsOpen();
	}

	// Token: 0x04001B08 RID: 6920
	[Serialize]
	private List<Ref<ResearchDestination>> m_researchDestinations = new List<Ref<ResearchDestination>>();

	// Token: 0x04001B09 RID: 6921
	[Serialize]
	private Ref<TemporalTear> m_temporalTear = new Ref<TemporalTear>();

	// Token: 0x04001B0A RID: 6922
	private ClusterFogOfWarManager.Instance m_fowManager;
}
