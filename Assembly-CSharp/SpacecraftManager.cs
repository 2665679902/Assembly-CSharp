using System;
using System.Collections.Generic;
using Database;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000965 RID: 2405
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/SpacecraftManager")]
public class SpacecraftManager : KMonoBehaviour, ISim1000ms
{
	// Token: 0x0600472D RID: 18221 RVA: 0x00190C20 File Offset: 0x0018EE20
	public static void DestroyInstance()
	{
		SpacecraftManager.instance = null;
	}

	// Token: 0x0600472E RID: 18222 RVA: 0x00190C28 File Offset: 0x0018EE28
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SpacecraftManager.instance = this;
		if (this.savedSpacecraftDestinations == null)
		{
			this.savedSpacecraftDestinations = new Dictionary<int, int>();
		}
	}

	// Token: 0x0600472F RID: 18223 RVA: 0x00190C4C File Offset: 0x0018EE4C
	private void GenerateFixedDestinations()
	{
		SpaceDestinationTypes spaceDestinationTypes = Db.Get().SpaceDestinationTypes;
		if (this.destinations != null)
		{
			return;
		}
		this.destinations = new List<SpaceDestination>
		{
			new SpaceDestination(0, spaceDestinationTypes.CarbonaceousAsteroid.Id, 0),
			new SpaceDestination(1, spaceDestinationTypes.CarbonaceousAsteroid.Id, 0),
			new SpaceDestination(2, spaceDestinationTypes.MetallicAsteroid.Id, 1),
			new SpaceDestination(3, spaceDestinationTypes.RockyAsteroid.Id, 2),
			new SpaceDestination(4, spaceDestinationTypes.IcyDwarf.Id, 3),
			new SpaceDestination(5, spaceDestinationTypes.OrganicDwarf.Id, 4)
		};
	}

	// Token: 0x06004730 RID: 18224 RVA: 0x00190D08 File Offset: 0x0018EF08
	private void GenerateRandomDestinations()
	{
		KRandom krandom = new KRandom(SaveLoader.Instance.clusterDetailSave.globalWorldSeed);
		SpaceDestinationTypes spaceDestinationTypes = Db.Get().SpaceDestinationTypes;
		List<List<string>> list = new List<List<string>>
		{
			new List<string>(),
			new List<string> { spaceDestinationTypes.OilyAsteroid.Id },
			new List<string> { spaceDestinationTypes.Satellite.Id },
			new List<string>
			{
				spaceDestinationTypes.Satellite.Id,
				spaceDestinationTypes.RockyAsteroid.Id,
				spaceDestinationTypes.CarbonaceousAsteroid.Id,
				spaceDestinationTypes.ForestPlanet.Id
			},
			new List<string>
			{
				spaceDestinationTypes.MetallicAsteroid.Id,
				spaceDestinationTypes.RockyAsteroid.Id,
				spaceDestinationTypes.CarbonaceousAsteroid.Id,
				spaceDestinationTypes.SaltDwarf.Id
			},
			new List<string>
			{
				spaceDestinationTypes.MetallicAsteroid.Id,
				spaceDestinationTypes.RockyAsteroid.Id,
				spaceDestinationTypes.CarbonaceousAsteroid.Id,
				spaceDestinationTypes.IcyDwarf.Id,
				spaceDestinationTypes.OrganicDwarf.Id
			},
			new List<string>
			{
				spaceDestinationTypes.IcyDwarf.Id,
				spaceDestinationTypes.OrganicDwarf.Id,
				spaceDestinationTypes.DustyMoon.Id,
				spaceDestinationTypes.ChlorinePlanet.Id,
				spaceDestinationTypes.RedDwarf.Id
			},
			new List<string>
			{
				spaceDestinationTypes.DustyMoon.Id,
				spaceDestinationTypes.TerraPlanet.Id,
				spaceDestinationTypes.VolcanoPlanet.Id
			},
			new List<string>
			{
				spaceDestinationTypes.TerraPlanet.Id,
				spaceDestinationTypes.GasGiant.Id,
				spaceDestinationTypes.IceGiant.Id,
				spaceDestinationTypes.RustPlanet.Id
			},
			new List<string>
			{
				spaceDestinationTypes.GasGiant.Id,
				spaceDestinationTypes.IceGiant.Id,
				spaceDestinationTypes.HydrogenGiant.Id
			},
			new List<string>
			{
				spaceDestinationTypes.RustPlanet.Id,
				spaceDestinationTypes.VolcanoPlanet.Id,
				spaceDestinationTypes.RockyAsteroid.Id,
				spaceDestinationTypes.TerraPlanet.Id,
				spaceDestinationTypes.MetallicAsteroid.Id
			},
			new List<string>
			{
				spaceDestinationTypes.ShinyPlanet.Id,
				spaceDestinationTypes.MetallicAsteroid.Id,
				spaceDestinationTypes.RockyAsteroid.Id
			},
			new List<string>
			{
				spaceDestinationTypes.GoldAsteroid.Id,
				spaceDestinationTypes.OrganicDwarf.Id,
				spaceDestinationTypes.ForestPlanet.Id,
				spaceDestinationTypes.ChlorinePlanet.Id
			},
			new List<string>
			{
				spaceDestinationTypes.IcyDwarf.Id,
				spaceDestinationTypes.MetallicAsteroid.Id,
				spaceDestinationTypes.DustyMoon.Id,
				spaceDestinationTypes.VolcanoPlanet.Id,
				spaceDestinationTypes.IceGiant.Id
			},
			new List<string>
			{
				spaceDestinationTypes.ShinyPlanet.Id,
				spaceDestinationTypes.RedDwarf.Id,
				spaceDestinationTypes.RockyAsteroid.Id,
				spaceDestinationTypes.GasGiant.Id
			},
			new List<string>
			{
				spaceDestinationTypes.HydrogenGiant.Id,
				spaceDestinationTypes.ForestPlanet.Id,
				spaceDestinationTypes.OilyAsteroid.Id
			},
			new List<string>
			{
				spaceDestinationTypes.GoldAsteroid.Id,
				spaceDestinationTypes.SaltDwarf.Id,
				spaceDestinationTypes.TerraPlanet.Id,
				spaceDestinationTypes.VolcanoPlanet.Id
			}
		};
		List<int> list2 = new List<int>();
		int num = 3;
		int num2 = 15;
		int num3 = 25;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Count != 0)
			{
				for (int j = 0; j < num; j++)
				{
					list2.Add(i);
				}
			}
		}
		int num4 = krandom.Next(num2, num3);
		for (int k = 0; k < num4; k++)
		{
			int num5 = krandom.Next(0, list2.Count - 1);
			int num6 = list2[num5];
			list2.RemoveAt(num5);
			List<string> list3 = list[num6];
			string text = list3[krandom.Next(0, list3.Count)];
			SpaceDestination spaceDestination = new SpaceDestination(this.destinations.Count, text, num6);
			this.destinations.Add(spaceDestination);
		}
		this.destinations.Add(new SpaceDestination(this.destinations.Count, Db.Get().SpaceDestinationTypes.Earth.Id, 4));
		this.destinations.Add(new SpaceDestination(this.destinations.Count, Db.Get().SpaceDestinationTypes.Wormhole.Id, list.Count));
	}

	// Token: 0x06004731 RID: 18225 RVA: 0x0019130C File Offset: 0x0018F50C
	private void RestoreDestinations()
	{
		if (this.destinationsGenerated)
		{
			return;
		}
		this.GenerateFixedDestinations();
		this.GenerateRandomDestinations();
		this.destinations.Sort((SpaceDestination a, SpaceDestination b) => a.distance.CompareTo(b.distance));
		List<float> list = new List<float>();
		for (int i = 0; i < 10; i++)
		{
			list.Add((float)i / 10f);
		}
		for (int j = 0; j < 20; j++)
		{
			list.Shuffle<float>();
			int num = 0;
			foreach (SpaceDestination spaceDestination in this.destinations)
			{
				if (spaceDestination.distance == j)
				{
					num++;
					spaceDestination.startingOrbitPercentage = list[num];
				}
			}
		}
		this.destinationsGenerated = true;
	}

	// Token: 0x06004732 RID: 18226 RVA: 0x001913F4 File Offset: 0x0018F5F4
	public SpaceDestination GetSpacecraftDestination(LaunchConditionManager lcm)
	{
		Spacecraft spacecraftFromLaunchConditionManager = this.GetSpacecraftFromLaunchConditionManager(lcm);
		return this.GetSpacecraftDestination(spacecraftFromLaunchConditionManager.id);
	}

	// Token: 0x06004733 RID: 18227 RVA: 0x00191415 File Offset: 0x0018F615
	public SpaceDestination GetSpacecraftDestination(int spacecraftID)
	{
		this.CleanSavedSpacecraftDestinations();
		if (this.savedSpacecraftDestinations.ContainsKey(spacecraftID))
		{
			return this.GetDestination(this.savedSpacecraftDestinations[spacecraftID]);
		}
		return null;
	}

	// Token: 0x06004734 RID: 18228 RVA: 0x00191440 File Offset: 0x0018F640
	public List<int> GetSpacecraftsForDestination(SpaceDestination destination)
	{
		this.CleanSavedSpacecraftDestinations();
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, int> keyValuePair in this.savedSpacecraftDestinations)
		{
			if (keyValuePair.Value == destination.id)
			{
				list.Add(keyValuePair.Key);
			}
		}
		return list;
	}

	// Token: 0x06004735 RID: 18229 RVA: 0x001914B8 File Offset: 0x0018F6B8
	private void CleanSavedSpacecraftDestinations()
	{
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, int> keyValuePair in this.savedSpacecraftDestinations)
		{
			bool flag = false;
			using (List<Spacecraft>.Enumerator enumerator2 = this.spacecraft.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.id == keyValuePair.Key)
					{
						flag = true;
						break;
					}
				}
			}
			bool flag2 = false;
			using (List<SpaceDestination>.Enumerator enumerator3 = this.destinations.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					if (enumerator3.Current.id == keyValuePair.Value)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (!flag || !flag2)
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (int num in list)
		{
			this.savedSpacecraftDestinations.Remove(num);
		}
	}

	// Token: 0x06004736 RID: 18230 RVA: 0x0019160C File Offset: 0x0018F80C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Game.Instance.spacecraftManager = this;
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			global::Debug.Assert(this.spacecraft == null || this.spacecraft.Count == 0);
			return;
		}
		this.RestoreDestinations();
	}

	// Token: 0x06004737 RID: 18231 RVA: 0x0019164C File Offset: 0x0018F84C
	public void SetSpacecraftDestination(LaunchConditionManager lcm, SpaceDestination destination)
	{
		Spacecraft spacecraftFromLaunchConditionManager = this.GetSpacecraftFromLaunchConditionManager(lcm);
		this.savedSpacecraftDestinations[spacecraftFromLaunchConditionManager.id] = destination.id;
	}

	// Token: 0x06004738 RID: 18232 RVA: 0x00191678 File Offset: 0x0018F878
	public int GetSpacecraftID(ILaunchableRocket rocket)
	{
		foreach (Spacecraft spacecraft in this.spacecraft)
		{
			if (spacecraft.launchConditions.gameObject == rocket.LaunchableGameObject)
			{
				return spacecraft.id;
			}
		}
		return -1;
	}

	// Token: 0x06004739 RID: 18233 RVA: 0x001916E8 File Offset: 0x0018F8E8
	public SpaceDestination GetDestination(int destinationID)
	{
		foreach (SpaceDestination spaceDestination in this.destinations)
		{
			if (spaceDestination.id == destinationID)
			{
				return spaceDestination;
			}
		}
		global::Debug.LogErrorFormat("No space destination with ID {0}", new object[] { destinationID });
		return null;
	}

	// Token: 0x0600473A RID: 18234 RVA: 0x00191760 File Offset: 0x0018F960
	public void RegisterSpacecraft(Spacecraft craft)
	{
		if (this.spacecraft.Contains(craft))
		{
			return;
		}
		if (craft.HasInvalidID())
		{
			craft.SetID(this.nextSpacecraftID);
			this.nextSpacecraftID++;
		}
		this.spacecraft.Add(craft);
	}

	// Token: 0x0600473B RID: 18235 RVA: 0x001917A0 File Offset: 0x0018F9A0
	public void UnregisterSpacecraft(LaunchConditionManager conditionManager)
	{
		Spacecraft spacecraftFromLaunchConditionManager = this.GetSpacecraftFromLaunchConditionManager(conditionManager);
		spacecraftFromLaunchConditionManager.SetState(Spacecraft.MissionState.Destroyed);
		this.spacecraft.Remove(spacecraftFromLaunchConditionManager);
	}

	// Token: 0x0600473C RID: 18236 RVA: 0x001917C9 File Offset: 0x0018F9C9
	public List<Spacecraft> GetSpacecraft()
	{
		return this.spacecraft;
	}

	// Token: 0x0600473D RID: 18237 RVA: 0x001917D4 File Offset: 0x0018F9D4
	public Spacecraft GetSpacecraftFromLaunchConditionManager(LaunchConditionManager lcm)
	{
		foreach (Spacecraft spacecraft in this.spacecraft)
		{
			if (spacecraft.launchConditions == lcm)
			{
				return spacecraft;
			}
		}
		return null;
	}

	// Token: 0x0600473E RID: 18238 RVA: 0x00191838 File Offset: 0x0018FA38
	public void Sim1000ms(float dt)
	{
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			return;
		}
		foreach (Spacecraft spacecraft in this.spacecraft)
		{
			spacecraft.ProgressMission(dt);
		}
		foreach (SpaceDestination spaceDestination in this.destinations)
		{
			spaceDestination.Replenish(dt);
		}
	}

	// Token: 0x0600473F RID: 18239 RVA: 0x001918D4 File Offset: 0x0018FAD4
	public void PushReadyToLandNotification(Spacecraft spacecraft)
	{
		Notification notification = new Notification(BUILDING.STATUSITEMS.SPACECRAFTREADYTOLAND.NOTIFICATION, NotificationType.Good, delegate(List<Notification> notificationList, object data)
		{
			string text = BUILDING.STATUSITEMS.SPACECRAFTREADYTOLAND.NOTIFICATION_TOOLTIP;
			foreach (Notification notification2 in notificationList)
			{
				text = text + "\n" + (string)notification2.tooltipData;
			}
			return text;
		}, "• " + spacecraft.rocketName, true, 0f, null, null, null, true, false, false);
		spacecraft.launchConditions.gameObject.AddOrGet<Notifier>().Add(notification, "");
	}

	// Token: 0x06004740 RID: 18240 RVA: 0x00191948 File Offset: 0x0018FB48
	private void SpawnMissionResults(Dictionary<SimHashes, float> results)
	{
		foreach (KeyValuePair<SimHashes, float> keyValuePair in results)
		{
			ElementLoader.FindElementByHash(keyValuePair.Key).substance.SpawnResource(PlayerController.GetCursorPos(KInputManager.GetMousePos()), keyValuePair.Value, 300f, 0, 0, false, false, false);
		}
	}

	// Token: 0x06004741 RID: 18241 RVA: 0x001919C4 File Offset: 0x0018FBC4
	public float GetDestinationAnalysisScore(SpaceDestination destination)
	{
		return this.GetDestinationAnalysisScore(destination.id);
	}

	// Token: 0x06004742 RID: 18242 RVA: 0x001919D2 File Offset: 0x0018FBD2
	public float GetDestinationAnalysisScore(int destinationID)
	{
		if (this.destinationAnalysisScores.ContainsKey(destinationID))
		{
			return this.destinationAnalysisScores[destinationID];
		}
		return 0f;
	}

	// Token: 0x06004743 RID: 18243 RVA: 0x001919F4 File Offset: 0x0018FBF4
	public void EarnDestinationAnalysisPoints(int destinationID, float points)
	{
		if (!this.destinationAnalysisScores.ContainsKey(destinationID))
		{
			this.destinationAnalysisScores.Add(destinationID, 0f);
		}
		SpaceDestination destination = this.GetDestination(destinationID);
		SpacecraftManager.DestinationAnalysisState destinationAnalysisState = this.GetDestinationAnalysisState(destination);
		Dictionary<int, float> dictionary = this.destinationAnalysisScores;
		dictionary[destinationID] += points;
		SpacecraftManager.DestinationAnalysisState destinationAnalysisState2 = this.GetDestinationAnalysisState(destination);
		if (destinationAnalysisState != destinationAnalysisState2)
		{
			int starmapAnalysisDestinationID = SpacecraftManager.instance.GetStarmapAnalysisDestinationID();
			if (starmapAnalysisDestinationID == destinationID)
			{
				if (destinationAnalysisState2 == SpacecraftManager.DestinationAnalysisState.Complete)
				{
					if (SpacecraftManager.instance.GetDestination(starmapAnalysisDestinationID).type == Db.Get().SpaceDestinationTypes.Earth.Id)
					{
						Game.Instance.unlocks.Unlock("earth", true);
					}
					if (SpacecraftManager.instance.GetDestination(starmapAnalysisDestinationID).type == Db.Get().SpaceDestinationTypes.Wormhole.Id)
					{
						Game.Instance.unlocks.Unlock("wormhole", true);
					}
					SpacecraftManager.instance.SetStarmapAnalysisDestinationID(-1);
				}
				base.Trigger(532901469, null);
			}
		}
	}

	// Token: 0x06004744 RID: 18244 RVA: 0x00191B0C File Offset: 0x0018FD0C
	public SpacecraftManager.DestinationAnalysisState GetDestinationAnalysisState(SpaceDestination destination)
	{
		if (destination.startAnalyzed)
		{
			return SpacecraftManager.DestinationAnalysisState.Complete;
		}
		float destinationAnalysisScore = this.GetDestinationAnalysisScore(destination);
		if (destinationAnalysisScore >= (float)ROCKETRY.DESTINATION_ANALYSIS.COMPLETE)
		{
			return SpacecraftManager.DestinationAnalysisState.Complete;
		}
		if (destinationAnalysisScore >= (float)ROCKETRY.DESTINATION_ANALYSIS.DISCOVERED)
		{
			return SpacecraftManager.DestinationAnalysisState.Discovered;
		}
		return SpacecraftManager.DestinationAnalysisState.Hidden;
	}

	// Token: 0x06004745 RID: 18245 RVA: 0x00191B44 File Offset: 0x0018FD44
	public bool AreAllDestinationsAnalyzed()
	{
		foreach (SpaceDestination spaceDestination in this.destinations)
		{
			if (this.GetDestinationAnalysisState(spaceDestination) != SpacecraftManager.DestinationAnalysisState.Complete)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06004746 RID: 18246 RVA: 0x00191BA4 File Offset: 0x0018FDA4
	public void SetStarmapAnalysisDestinationID(int id)
	{
		this.analyzeDestinationID = id;
		base.Trigger(532901469, id);
	}

	// Token: 0x06004747 RID: 18247 RVA: 0x00191BBE File Offset: 0x0018FDBE
	public int GetStarmapAnalysisDestinationID()
	{
		return this.analyzeDestinationID;
	}

	// Token: 0x06004748 RID: 18248 RVA: 0x00191BC6 File Offset: 0x0018FDC6
	public bool HasAnalysisTarget()
	{
		return this.analyzeDestinationID != -1;
	}

	// Token: 0x04002F23 RID: 12067
	public static SpacecraftManager instance;

	// Token: 0x04002F24 RID: 12068
	[Serialize]
	private List<Spacecraft> spacecraft = new List<Spacecraft>();

	// Token: 0x04002F25 RID: 12069
	[Serialize]
	private int nextSpacecraftID;

	// Token: 0x04002F26 RID: 12070
	public const int INVALID_DESTINATION_ID = -1;

	// Token: 0x04002F27 RID: 12071
	[Serialize]
	private int analyzeDestinationID = -1;

	// Token: 0x04002F28 RID: 12072
	[Serialize]
	public bool hasVisitedWormHole;

	// Token: 0x04002F29 RID: 12073
	[Serialize]
	public List<SpaceDestination> destinations;

	// Token: 0x04002F2A RID: 12074
	[Serialize]
	public Dictionary<int, int> savedSpacecraftDestinations;

	// Token: 0x04002F2B RID: 12075
	[Serialize]
	public bool destinationsGenerated;

	// Token: 0x04002F2C RID: 12076
	[Serialize]
	public Dictionary<int, float> destinationAnalysisScores = new Dictionary<int, float>();

	// Token: 0x02001767 RID: 5991
	public enum DestinationAnalysisState
	{
		// Token: 0x04006CF9 RID: 27897
		Hidden,
		// Token: 0x04006CFA RID: 27898
		Discovered,
		// Token: 0x04006CFB RID: 27899
		Complete
	}
}
