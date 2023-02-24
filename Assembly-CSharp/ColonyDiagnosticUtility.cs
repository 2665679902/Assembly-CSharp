using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200045C RID: 1116
public class ColonyDiagnosticUtility : KMonoBehaviour, ISim1000ms
{
	// Token: 0x06001894 RID: 6292 RVA: 0x00082676 File Offset: 0x00080876
	public static void DestroyInstance()
	{
		ColonyDiagnosticUtility.Instance = null;
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x00082680 File Offset: 0x00080880
	public ColonyDiagnostic.DiagnosticResult.Opinion GetWorldDiagnosticResult(int worldID)
	{
		ColonyDiagnostic.DiagnosticResult.Opinion opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Good;
		foreach (ColonyDiagnostic colonyDiagnostic in this.worldDiagnostics[worldID])
		{
			if (ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[worldID][colonyDiagnostic.id] != ColonyDiagnosticUtility.DisplaySetting.Never && !ColonyDiagnosticUtility.Instance.IsDiagnosticTutorialDisabled(colonyDiagnostic.id))
			{
				ColonyDiagnosticUtility.DisplaySetting displaySetting = this.diagnosticDisplaySettings[worldID][colonyDiagnostic.id];
				if (displaySetting > ColonyDiagnosticUtility.DisplaySetting.AlertOnly)
				{
					if (displaySetting != ColonyDiagnosticUtility.DisplaySetting.Never)
					{
					}
				}
				else
				{
					opinion = (ColonyDiagnostic.DiagnosticResult.Opinion)Math.Min((int)opinion, (int)colonyDiagnostic.LatestResult.opinion);
				}
			}
		}
		return opinion;
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x0008273C File Offset: 0x0008093C
	public string GetWorldDiagnosticResultStatus(int worldID)
	{
		ColonyDiagnostic colonyDiagnostic = null;
		foreach (ColonyDiagnostic colonyDiagnostic2 in this.worldDiagnostics[worldID])
		{
			if (ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[worldID][colonyDiagnostic2.id] != ColonyDiagnosticUtility.DisplaySetting.Never && !ColonyDiagnosticUtility.Instance.IsDiagnosticTutorialDisabled(colonyDiagnostic2.id))
			{
				ColonyDiagnosticUtility.DisplaySetting displaySetting = this.diagnosticDisplaySettings[worldID][colonyDiagnostic2.id];
				if (displaySetting > ColonyDiagnosticUtility.DisplaySetting.AlertOnly)
				{
					if (displaySetting != ColonyDiagnosticUtility.DisplaySetting.Never)
					{
					}
				}
				else if (colonyDiagnostic == null || colonyDiagnostic2.LatestResult.opinion < colonyDiagnostic.LatestResult.opinion)
				{
					colonyDiagnostic = colonyDiagnostic2;
				}
			}
		}
		if (colonyDiagnostic == null || colonyDiagnostic.LatestResult.opinion == ColonyDiagnostic.DiagnosticResult.Opinion.Normal)
		{
			return "";
		}
		return colonyDiagnostic.name;
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x0008281C File Offset: 0x00080A1C
	public string GetWorldDiagnosticResultTooltip(int worldID)
	{
		string text = "";
		foreach (ColonyDiagnostic colonyDiagnostic in this.worldDiagnostics[worldID])
		{
			if (ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[worldID][colonyDiagnostic.id] != ColonyDiagnosticUtility.DisplaySetting.Never && !ColonyDiagnosticUtility.Instance.IsDiagnosticTutorialDisabled(colonyDiagnostic.id))
			{
				ColonyDiagnosticUtility.DisplaySetting displaySetting = this.diagnosticDisplaySettings[worldID][colonyDiagnostic.id];
				if (displaySetting > ColonyDiagnosticUtility.DisplaySetting.AlertOnly)
				{
					if (displaySetting != ColonyDiagnosticUtility.DisplaySetting.Never)
					{
					}
				}
				else if (colonyDiagnostic.LatestResult.opinion < ColonyDiagnostic.DiagnosticResult.Opinion.Normal)
				{
					text = text + "\n" + colonyDiagnostic.LatestResult.Message;
				}
			}
		}
		return text;
	}

	// Token: 0x06001898 RID: 6296 RVA: 0x000828F8 File Offset: 0x00080AF8
	public bool IsDiagnosticTutorialDisabled(string id)
	{
		return ColonyDiagnosticUtility.Instance.diagnosticTutorialStatus.ContainsKey(id) && GameClock.Instance.GetTime() < ColonyDiagnosticUtility.Instance.diagnosticTutorialStatus[id];
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x0008292B File Offset: 0x00080B2B
	public void ClearDiagnosticTutorialSetting(string id)
	{
		if (ColonyDiagnosticUtility.Instance.diagnosticTutorialStatus.ContainsKey(id))
		{
			ColonyDiagnosticUtility.Instance.diagnosticTutorialStatus[id] = -1f;
		}
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x00082954 File Offset: 0x00080B54
	public bool IsCriteriaEnabled(int worldID, string diagnosticID, string criteriaID)
	{
		Dictionary<string, List<string>> dictionary = this.diagnosticCriteriaDisabled[worldID];
		return dictionary.ContainsKey(diagnosticID) && !dictionary[diagnosticID].Contains(criteriaID);
	}

	// Token: 0x0600189B RID: 6299 RVA: 0x0008298C File Offset: 0x00080B8C
	public void SetCriteriaEnabled(int worldID, string diagnosticID, string criteriaID, bool enabled)
	{
		Dictionary<string, List<string>> dictionary = this.diagnosticCriteriaDisabled[worldID];
		global::Debug.Assert(dictionary.ContainsKey(diagnosticID), string.Format("Trying to set criteria on World {0} lacks diagnostic {1} that criteria {2} relates to", worldID, diagnosticID, criteriaID));
		List<string> list = dictionary[diagnosticID];
		if (enabled && list.Contains(criteriaID))
		{
			list.Remove(criteriaID);
		}
		if (!enabled && !list.Contains(criteriaID))
		{
			list.Add(criteriaID);
		}
	}

	// Token: 0x0600189C RID: 6300 RVA: 0x000829F3 File Offset: 0x00080BF3
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ColonyDiagnosticUtility.Instance = this;
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x00082A04 File Offset: 0x00080C04
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (int num in ClusterManager.Instance.GetWorldIDsSorted())
		{
			this.AddWorld(num);
		}
		ClusterManager.Instance.Subscribe(-1280433810, new Action<object>(this.Refresh));
		ClusterManager.Instance.Subscribe(-1078710002, new Action<object>(this.RemoveWorld));
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x00082A9C File Offset: 0x00080C9C
	private void Refresh(object data)
	{
		int num = (int)data;
		this.AddWorld(num);
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x00082AB8 File Offset: 0x00080CB8
	private void RemoveWorld(object data)
	{
		int num = (int)data;
		if (this.diagnosticDisplaySettings.Remove(num))
		{
			List<ColonyDiagnostic> list;
			if (this.worldDiagnostics.TryGetValue(num, out list))
			{
				foreach (ColonyDiagnostic colonyDiagnostic in list)
				{
					colonyDiagnostic.OnCleanUp();
				}
			}
			this.worldDiagnostics.Remove(num);
		}
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x00082B38 File Offset: 0x00080D38
	public ColonyDiagnostic GetDiagnostic(string id, int worldID)
	{
		return this.worldDiagnostics[worldID].Find((ColonyDiagnostic match) => match.id == id);
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x00082B6F File Offset: 0x00080D6F
	public T GetDiagnostic<T>(int worldID) where T : ColonyDiagnostic
	{
		return (T)((object)this.worldDiagnostics[worldID].Find((ColonyDiagnostic match) => match is T));
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x00082BA8 File Offset: 0x00080DA8
	public string GetDiagnosticName(string id)
	{
		foreach (KeyValuePair<int, List<ColonyDiagnostic>> keyValuePair in this.worldDiagnostics)
		{
			foreach (ColonyDiagnostic colonyDiagnostic in keyValuePair.Value)
			{
				if (colonyDiagnostic.id == id)
				{
					return colonyDiagnostic.name;
				}
			}
		}
		global::Debug.LogWarning("Cannot locate name of diagnostic " + id + " because no worlds have a diagnostic with that id ");
		return "";
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x00082C68 File Offset: 0x00080E68
	public ChoreGroupDiagnostic GetChoreGroupDiagnostic(int worldID, ChoreGroup choreGroup)
	{
		return (ChoreGroupDiagnostic)this.worldDiagnostics[worldID].Find((ColonyDiagnostic match) => match is ChoreGroupDiagnostic && ((ChoreGroupDiagnostic)match).choreGroup == choreGroup);
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x00082CA4 File Offset: 0x00080EA4
	public WorkTimeDiagnostic GetWorkTimeDiagnostic(int worldID, ChoreGroup choreGroup)
	{
		return (WorkTimeDiagnostic)this.worldDiagnostics[worldID].Find((ColonyDiagnostic match) => match is WorkTimeDiagnostic && ((WorkTimeDiagnostic)match).choreGroup == choreGroup);
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x00082CE0 File Offset: 0x00080EE0
	private void TryAddDiagnosticToWorldCollection(ref List<ColonyDiagnostic> newWorldDiagnostics, ColonyDiagnostic newDiagnostic)
	{
		if (!DlcManager.IsDlcListValidForCurrentContent(newDiagnostic.GetDlcIds()))
		{
			return;
		}
		newWorldDiagnostics.Add(newDiagnostic);
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x00082CF8 File Offset: 0x00080EF8
	public void AddWorld(int worldID)
	{
		bool flag = false;
		if (!this.diagnosticDisplaySettings.ContainsKey(worldID))
		{
			this.diagnosticDisplaySettings.Add(worldID, new Dictionary<string, ColonyDiagnosticUtility.DisplaySetting>());
			flag = true;
		}
		if (!this.diagnosticCriteriaDisabled.ContainsKey(worldID))
		{
			this.diagnosticCriteriaDisabled.Add(worldID, new Dictionary<string, List<string>>());
		}
		List<ColonyDiagnostic> list = new List<ColonyDiagnostic>();
		this.TryAddDiagnosticToWorldCollection(ref list, new BreathabilityDiagnostic(worldID));
		this.TryAddDiagnosticToWorldCollection(ref list, new FoodDiagnostic(worldID));
		this.TryAddDiagnosticToWorldCollection(ref list, new StressDiagnostic(worldID));
		this.TryAddDiagnosticToWorldCollection(ref list, new RadiationDiagnostic(worldID));
		this.TryAddDiagnosticToWorldCollection(ref list, new ReactorDiagnostic(worldID));
		if (ClusterManager.Instance.GetWorld(worldID).IsModuleInterior)
		{
			this.TryAddDiagnosticToWorldCollection(ref list, new FloatingRocketDiagnostic(worldID));
			this.TryAddDiagnosticToWorldCollection(ref list, new RocketFuelDiagnostic(worldID));
			this.TryAddDiagnosticToWorldCollection(ref list, new RocketOxidizerDiagnostic(worldID));
		}
		else
		{
			this.TryAddDiagnosticToWorldCollection(ref list, new BedDiagnostic(worldID));
			this.TryAddDiagnosticToWorldCollection(ref list, new ToiletDiagnostic(worldID));
			this.TryAddDiagnosticToWorldCollection(ref list, new PowerUseDiagnostic(worldID));
			this.TryAddDiagnosticToWorldCollection(ref list, new BatteryDiagnostic(worldID));
			this.TryAddDiagnosticToWorldCollection(ref list, new IdleDiagnostic(worldID));
			this.TryAddDiagnosticToWorldCollection(ref list, new TrappedDuplicantDiagnostic(worldID));
			this.TryAddDiagnosticToWorldCollection(ref list, new FarmDiagnostic(worldID));
			this.TryAddDiagnosticToWorldCollection(ref list, new EntombedDiagnostic(worldID));
			this.TryAddDiagnosticToWorldCollection(ref list, new RocketsInOrbitDiagnostic(worldID));
		}
		this.worldDiagnostics.Add(worldID, list);
		foreach (ColonyDiagnostic colonyDiagnostic in list)
		{
			if (!this.diagnosticDisplaySettings[worldID].ContainsKey(colonyDiagnostic.id))
			{
				this.diagnosticDisplaySettings[worldID].Add(colonyDiagnostic.id, ColonyDiagnosticUtility.DisplaySetting.AlertOnly);
			}
			if (!this.diagnosticCriteriaDisabled[worldID].ContainsKey(colonyDiagnostic.id))
			{
				this.diagnosticCriteriaDisabled[worldID].Add(colonyDiagnostic.id, new List<string>());
			}
		}
		if (flag)
		{
			this.diagnosticDisplaySettings[worldID][typeof(BreathabilityDiagnostic).Name] = ColonyDiagnosticUtility.DisplaySetting.Always;
			this.diagnosticDisplaySettings[worldID][typeof(FoodDiagnostic).Name] = ColonyDiagnosticUtility.DisplaySetting.Always;
			this.diagnosticDisplaySettings[worldID][typeof(StressDiagnostic).Name] = ColonyDiagnosticUtility.DisplaySetting.Always;
			this.diagnosticDisplaySettings[worldID][typeof(IdleDiagnostic).Name] = ColonyDiagnosticUtility.DisplaySetting.Never;
			if (ClusterManager.Instance.GetWorld(worldID).IsModuleInterior)
			{
				this.diagnosticDisplaySettings[worldID][typeof(FloatingRocketDiagnostic).Name] = ColonyDiagnosticUtility.DisplaySetting.Always;
				this.diagnosticDisplaySettings[worldID][typeof(RocketFuelDiagnostic).Name] = ColonyDiagnosticUtility.DisplaySetting.Always;
				this.diagnosticDisplaySettings[worldID][typeof(RocketOxidizerDiagnostic).Name] = ColonyDiagnosticUtility.DisplaySetting.Always;
			}
		}
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x00082FFC File Offset: 0x000811FC
	public void Sim1000ms(float dt)
	{
		if (ColonyDiagnosticUtility.IgnoreFirstUpdate)
		{
			ColonyDiagnosticUtility.IgnoreFirstUpdate = false;
		}
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x0008300C File Offset: 0x0008120C
	public static bool PastNewBuildingGracePeriod(Transform building)
	{
		BuildingComplete component = building.GetComponent<BuildingComplete>();
		return !(component != null) || GameClock.Instance.GetTime() - component.creationTime >= 600f;
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x00083044 File Offset: 0x00081244
	public static bool IgnoreRocketsWithNoCrewRequested(int worldID, out ColonyDiagnostic.DiagnosticResult result)
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(worldID);
		result = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.NO_MINIONS, null);
		if (world.IsModuleInterior)
		{
			for (int i = 0; i < Components.Clustercrafts.Count; i++)
			{
				WorldContainer interiorWorld = Components.Clustercrafts[i].ModuleInterface.GetInteriorWorld();
				if (!(interiorWorld == null) && interiorWorld.id == worldID)
				{
					PassengerRocketModule passengerModule = Components.Clustercrafts[i].ModuleInterface.GetPassengerModule();
					if (passengerModule != null && !passengerModule.ShouldCrewGetIn())
					{
						result = default(ColonyDiagnostic.DiagnosticResult);
						result.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
						result.Message = UI.COLONY_DIAGNOSTICS.NO_MINIONS_REQUESTED;
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x04000DBC RID: 3516
	public static ColonyDiagnosticUtility Instance;

	// Token: 0x04000DBD RID: 3517
	private Dictionary<int, List<ColonyDiagnostic>> worldDiagnostics = new Dictionary<int, List<ColonyDiagnostic>>();

	// Token: 0x04000DBE RID: 3518
	[Serialize]
	public Dictionary<int, Dictionary<string, ColonyDiagnosticUtility.DisplaySetting>> diagnosticDisplaySettings = new Dictionary<int, Dictionary<string, ColonyDiagnosticUtility.DisplaySetting>>();

	// Token: 0x04000DBF RID: 3519
	[Serialize]
	public Dictionary<int, Dictionary<string, List<string>>> diagnosticCriteriaDisabled = new Dictionary<int, Dictionary<string, List<string>>>();

	// Token: 0x04000DC0 RID: 3520
	[Serialize]
	private Dictionary<string, float> diagnosticTutorialStatus = new Dictionary<string, float>
	{
		{
			typeof(ToiletDiagnostic).Name,
			450f
		},
		{
			typeof(BedDiagnostic).Name,
			900f
		},
		{
			typeof(BreathabilityDiagnostic).Name,
			1800f
		},
		{
			typeof(FoodDiagnostic).Name,
			3000f
		},
		{
			typeof(FarmDiagnostic).Name,
			6000f
		},
		{
			typeof(StressDiagnostic).Name,
			9000f
		},
		{
			typeof(PowerUseDiagnostic).Name,
			12000f
		},
		{
			typeof(BatteryDiagnostic).Name,
			12000f
		}
	};

	// Token: 0x04000DC1 RID: 3521
	public static bool IgnoreFirstUpdate = true;

	// Token: 0x04000DC2 RID: 3522
	public static ColonyDiagnostic.DiagnosticResult NoDataResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.NO_DATA, null);

	// Token: 0x02001077 RID: 4215
	public enum DisplaySetting
	{
		// Token: 0x040057BC RID: 22460
		Always,
		// Token: 0x040057BD RID: 22461
		AlertOnly,
		// Token: 0x040057BE RID: 22462
		Never,
		// Token: 0x040057BF RID: 22463
		LENGTH
	}
}
