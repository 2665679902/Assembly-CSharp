using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Database;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000963 RID: 2403
[SerializationConfig(MemberSerialization.OptIn)]
public class SpaceDestination
{
	// Token: 0x06004704 RID: 18180 RVA: 0x0018FD84 File Offset: 0x0018DF84
	private static global::Tuple<SimHashes, MathUtil.MinMax> GetRareElement(SimHashes id)
	{
		foreach (global::Tuple<SimHashes, MathUtil.MinMax> tuple in SpaceDestination.RARE_ELEMENTS)
		{
			if (tuple.first == id)
			{
				return tuple;
			}
		}
		return null;
	}

	// Token: 0x1700054B RID: 1355
	// (get) Token: 0x06004705 RID: 18181 RVA: 0x0018FDE0 File Offset: 0x0018DFE0
	public int OneBasedDistance
	{
		get
		{
			return this.distance + 1;
		}
	}

	// Token: 0x1700054C RID: 1356
	// (get) Token: 0x06004706 RID: 18182 RVA: 0x0018FDEA File Offset: 0x0018DFEA
	public float CurrentMass
	{
		get
		{
			return (float)this.GetDestinationType().minimumMass + this.availableMass;
		}
	}

	// Token: 0x1700054D RID: 1357
	// (get) Token: 0x06004707 RID: 18183 RVA: 0x0018FDFF File Offset: 0x0018DFFF
	public float AvailableMass
	{
		get
		{
			return this.availableMass;
		}
	}

	// Token: 0x06004708 RID: 18184 RVA: 0x0018FE08 File Offset: 0x0018E008
	public SpaceDestination(int id, string type, int distance)
	{
		this.id = id;
		this.type = type;
		this.distance = distance;
		SpaceDestinationType destinationType = this.GetDestinationType();
		this.availableMass = (float)(destinationType.maxiumMass - destinationType.minimumMass);
		this.GenerateSurfaceElements();
		this.GenerateResearchOpportunities();
	}

	// Token: 0x06004709 RID: 18185 RVA: 0x0018FE84 File Offset: 0x0018E084
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 9))
		{
			SpaceDestinationType destinationType = this.GetDestinationType();
			this.availableMass = (float)(destinationType.maxiumMass - destinationType.minimumMass);
		}
	}

	// Token: 0x0600470A RID: 18186 RVA: 0x0018FEC3 File Offset: 0x0018E0C3
	public SpaceDestinationType GetDestinationType()
	{
		return Db.Get().SpaceDestinationTypes.Get(this.type);
	}

	// Token: 0x0600470B RID: 18187 RVA: 0x0018FEDC File Offset: 0x0018E0DC
	public SpaceDestination.ResearchOpportunity TryCompleteResearchOpportunity()
	{
		foreach (SpaceDestination.ResearchOpportunity researchOpportunity in this.researchOpportunities)
		{
			if (researchOpportunity.TryComplete(this))
			{
				return researchOpportunity;
			}
		}
		return null;
	}

	// Token: 0x0600470C RID: 18188 RVA: 0x0018FF38 File Offset: 0x0018E138
	public void GenerateSurfaceElements()
	{
		foreach (KeyValuePair<SimHashes, MathUtil.MinMax> keyValuePair in this.GetDestinationType().elementTable)
		{
			this.recoverableElements.Add(keyValuePair.Key, UnityEngine.Random.value);
		}
	}

	// Token: 0x0600470D RID: 18189 RVA: 0x0018FFA0 File Offset: 0x0018E1A0
	public SpacecraftManager.DestinationAnalysisState AnalysisState()
	{
		return SpacecraftManager.instance.GetDestinationAnalysisState(this);
	}

	// Token: 0x0600470E RID: 18190 RVA: 0x0018FFB0 File Offset: 0x0018E1B0
	public void GenerateResearchOpportunities()
	{
		this.researchOpportunities.Add(new SpaceDestination.ResearchOpportunity(UI.STARMAP.DESTINATIONSTUDY.UPPERATMO, ROCKETRY.DESTINATION_RESEARCH.BASIC));
		this.researchOpportunities.Add(new SpaceDestination.ResearchOpportunity(UI.STARMAP.DESTINATIONSTUDY.LOWERATMO, ROCKETRY.DESTINATION_RESEARCH.BASIC));
		this.researchOpportunities.Add(new SpaceDestination.ResearchOpportunity(UI.STARMAP.DESTINATIONSTUDY.MAGNETICFIELD, ROCKETRY.DESTINATION_RESEARCH.BASIC));
		this.researchOpportunities.Add(new SpaceDestination.ResearchOpportunity(UI.STARMAP.DESTINATIONSTUDY.SURFACE, ROCKETRY.DESTINATION_RESEARCH.BASIC));
		this.researchOpportunities.Add(new SpaceDestination.ResearchOpportunity(UI.STARMAP.DESTINATIONSTUDY.SUBSURFACE, ROCKETRY.DESTINATION_RESEARCH.BASIC));
		float num = 0f;
		foreach (global::Tuple<float, int> tuple in SpaceDestination.RARE_ELEMENT_CHANCES)
		{
			num += tuple.first;
		}
		float num2 = UnityEngine.Random.value * num;
		int num3 = 0;
		foreach (global::Tuple<float, int> tuple2 in SpaceDestination.RARE_ELEMENT_CHANCES)
		{
			num2 -= tuple2.first;
			if (num2 <= 0f)
			{
				num3 = tuple2.second;
			}
		}
		for (int i = 0; i < num3; i++)
		{
			this.researchOpportunities[UnityEngine.Random.Range(0, this.researchOpportunities.Count)].discoveredRareResource = SpaceDestination.RARE_ELEMENTS[UnityEngine.Random.Range(0, SpaceDestination.RARE_ELEMENTS.Count)].first;
		}
		if (UnityEngine.Random.value < 0.33f)
		{
			int num4 = UnityEngine.Random.Range(0, this.researchOpportunities.Count);
			this.researchOpportunities[num4].discoveredRareItem = SpaceDestination.RARE_ITEMS[UnityEngine.Random.Range(0, SpaceDestination.RARE_ITEMS.Count)].first;
		}
	}

	// Token: 0x0600470F RID: 18191 RVA: 0x001901A8 File Offset: 0x0018E3A8
	public float GetResourceValue(SimHashes resource, float roll)
	{
		if (this.GetDestinationType().elementTable.ContainsKey(resource))
		{
			return this.GetDestinationType().elementTable[resource].Lerp(roll);
		}
		if (SpaceDestinationTypes.extendedElementTable.ContainsKey(resource))
		{
			return SpaceDestinationTypes.extendedElementTable[resource].Lerp(roll);
		}
		return 0f;
	}

	// Token: 0x06004710 RID: 18192 RVA: 0x0019020C File Offset: 0x0018E40C
	public Dictionary<SimHashes, float> GetMissionResourceResult(float totalCargoSpace, float reservedMass, bool solids = true, bool liquids = true, bool gasses = true)
	{
		Dictionary<SimHashes, float> dictionary = new Dictionary<SimHashes, float>();
		float num = 0f;
		foreach (KeyValuePair<SimHashes, float> keyValuePair in this.recoverableElements)
		{
			if ((ElementLoader.FindElementByHash(keyValuePair.Key).IsSolid && solids) || (ElementLoader.FindElementByHash(keyValuePair.Key).IsLiquid && liquids) || (ElementLoader.FindElementByHash(keyValuePair.Key).IsGas && gasses))
			{
				num += this.GetResourceValue(keyValuePair.Key, keyValuePair.Value);
			}
		}
		float num2 = Mathf.Min(this.CurrentMass + reservedMass - (float)this.GetDestinationType().minimumMass, totalCargoSpace);
		foreach (KeyValuePair<SimHashes, float> keyValuePair2 in this.recoverableElements)
		{
			if ((ElementLoader.FindElementByHash(keyValuePair2.Key).IsSolid && solids) || (ElementLoader.FindElementByHash(keyValuePair2.Key).IsLiquid && liquids) || (ElementLoader.FindElementByHash(keyValuePair2.Key).IsGas && gasses))
			{
				float num3 = num2 * (this.GetResourceValue(keyValuePair2.Key, keyValuePair2.Value) / num);
				dictionary.Add(keyValuePair2.Key, num3);
			}
		}
		return dictionary;
	}

	// Token: 0x06004711 RID: 18193 RVA: 0x00190380 File Offset: 0x0018E580
	public Dictionary<Tag, int> GetRecoverableEntities()
	{
		Dictionary<Tag, int> dictionary = new Dictionary<Tag, int>();
		Dictionary<string, int> recoverableEntities = this.GetDestinationType().recoverableEntities;
		if (recoverableEntities != null)
		{
			foreach (KeyValuePair<string, int> keyValuePair in recoverableEntities)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
		return dictionary;
	}

	// Token: 0x06004712 RID: 18194 RVA: 0x001903F8 File Offset: 0x0018E5F8
	public Dictionary<Tag, int> GetMissionEntityResult()
	{
		return this.GetRecoverableEntities();
	}

	// Token: 0x06004713 RID: 18195 RVA: 0x00190400 File Offset: 0x0018E600
	public float ReserveResources(CargoBay bay)
	{
		float num = 0f;
		if (bay != null)
		{
			Storage component = bay.GetComponent<Storage>();
			foreach (KeyValuePair<SimHashes, float> keyValuePair in this.recoverableElements)
			{
				if (this.HasElementType(bay.storageType))
				{
					num += component.capacityKg;
					this.availableMass = Mathf.Max(0f, this.availableMass - component.capacityKg);
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x06004714 RID: 18196 RVA: 0x0019049C File Offset: 0x0018E69C
	public bool HasElementType(CargoBay.CargoType type)
	{
		foreach (KeyValuePair<SimHashes, float> keyValuePair in this.recoverableElements)
		{
			if ((ElementLoader.FindElementByHash(keyValuePair.Key).IsSolid && type == CargoBay.CargoType.Solids) || (ElementLoader.FindElementByHash(keyValuePair.Key).IsLiquid && type == CargoBay.CargoType.Liquids) || (ElementLoader.FindElementByHash(keyValuePair.Key).IsGas && type == CargoBay.CargoType.Gasses))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004715 RID: 18197 RVA: 0x00190534 File Offset: 0x0018E734
	public void Replenish(float dt)
	{
		SpaceDestinationType destinationType = this.GetDestinationType();
		if (this.CurrentMass < (float)destinationType.maxiumMass)
		{
			this.availableMass += destinationType.replishmentPerSim1000ms;
		}
	}

	// Token: 0x06004716 RID: 18198 RVA: 0x0019056C File Offset: 0x0018E76C
	public float GetAvailableResourcesPercentage(CargoBay.CargoType cargoType)
	{
		float num = 0f;
		float totalMass = this.GetTotalMass();
		foreach (KeyValuePair<SimHashes, float> keyValuePair in this.recoverableElements)
		{
			if ((ElementLoader.FindElementByHash(keyValuePair.Key).IsSolid && cargoType == CargoBay.CargoType.Solids) || (ElementLoader.FindElementByHash(keyValuePair.Key).IsLiquid && cargoType == CargoBay.CargoType.Liquids) || (ElementLoader.FindElementByHash(keyValuePair.Key).IsGas && cargoType == CargoBay.CargoType.Gasses))
			{
				num += this.GetResourceValue(keyValuePair.Key, keyValuePair.Value) / totalMass;
			}
		}
		return num;
	}

	// Token: 0x06004717 RID: 18199 RVA: 0x00190624 File Offset: 0x0018E824
	public float GetTotalMass()
	{
		float num = 0f;
		foreach (KeyValuePair<SimHashes, float> keyValuePair in this.recoverableElements)
		{
			num += this.GetResourceValue(keyValuePair.Key, keyValuePair.Value);
		}
		return num;
	}

	// Token: 0x04002F0D RID: 12045
	private const int MASS_TO_RECOVER_AMOUNT = 1000;

	// Token: 0x04002F0E RID: 12046
	private static List<global::Tuple<float, int>> RARE_ELEMENT_CHANCES = new List<global::Tuple<float, int>>
	{
		new global::Tuple<float, int>(1f, 0),
		new global::Tuple<float, int>(0.33f, 1),
		new global::Tuple<float, int>(0.03f, 2)
	};

	// Token: 0x04002F0F RID: 12047
	private static readonly List<global::Tuple<SimHashes, MathUtil.MinMax>> RARE_ELEMENTS = new List<global::Tuple<SimHashes, MathUtil.MinMax>>
	{
		new global::Tuple<SimHashes, MathUtil.MinMax>(SimHashes.Katairite, new MathUtil.MinMax(1f, 10f)),
		new global::Tuple<SimHashes, MathUtil.MinMax>(SimHashes.Niobium, new MathUtil.MinMax(1f, 10f)),
		new global::Tuple<SimHashes, MathUtil.MinMax>(SimHashes.Fullerene, new MathUtil.MinMax(1f, 10f)),
		new global::Tuple<SimHashes, MathUtil.MinMax>(SimHashes.Isoresin, new MathUtil.MinMax(1f, 10f))
	};

	// Token: 0x04002F10 RID: 12048
	private const float RARE_ITEM_CHANCE = 0.33f;

	// Token: 0x04002F11 RID: 12049
	private static readonly List<global::Tuple<string, MathUtil.MinMax>> RARE_ITEMS = new List<global::Tuple<string, MathUtil.MinMax>>
	{
		new global::Tuple<string, MathUtil.MinMax>("GeneShufflerRecharge", new MathUtil.MinMax(1f, 2f))
	};

	// Token: 0x04002F12 RID: 12050
	[Serialize]
	public int id;

	// Token: 0x04002F13 RID: 12051
	[Serialize]
	public string type;

	// Token: 0x04002F14 RID: 12052
	public bool startAnalyzed;

	// Token: 0x04002F15 RID: 12053
	[Serialize]
	public int distance;

	// Token: 0x04002F16 RID: 12054
	[Serialize]
	public float activePeriod = 20f;

	// Token: 0x04002F17 RID: 12055
	[Serialize]
	public float inactivePeriod = 10f;

	// Token: 0x04002F18 RID: 12056
	[Serialize]
	public float startingOrbitPercentage;

	// Token: 0x04002F19 RID: 12057
	[Serialize]
	public Dictionary<SimHashes, float> recoverableElements = new Dictionary<SimHashes, float>();

	// Token: 0x04002F1A RID: 12058
	[Serialize]
	public List<SpaceDestination.ResearchOpportunity> researchOpportunities = new List<SpaceDestination.ResearchOpportunity>();

	// Token: 0x04002F1B RID: 12059
	[Serialize]
	private float availableMass;

	// Token: 0x02001765 RID: 5989
	[SerializationConfig(MemberSerialization.OptIn)]
	public class ResearchOpportunity
	{
		// Token: 0x06008AD7 RID: 35543 RVA: 0x002FE327 File Offset: 0x002FC527
		[OnDeserialized]
		private void OnDeserialized()
		{
			if (this.discoveredRareResource == (SimHashes)0)
			{
				this.discoveredRareResource = SimHashes.Void;
			}
			if (this.dataValue > 50)
			{
				this.dataValue = 50;
			}
		}

		// Token: 0x06008AD8 RID: 35544 RVA: 0x002FE34E File Offset: 0x002FC54E
		public ResearchOpportunity(string description, int pointValue)
		{
			this.description = description;
			this.dataValue = pointValue;
		}

		// Token: 0x06008AD9 RID: 35545 RVA: 0x002FE370 File Offset: 0x002FC570
		public bool TryComplete(SpaceDestination destination)
		{
			if (!this.completed)
			{
				this.completed = true;
				if (this.discoveredRareResource != SimHashes.Void && !destination.recoverableElements.ContainsKey(this.discoveredRareResource))
				{
					destination.recoverableElements.Add(this.discoveredRareResource, UnityEngine.Random.value);
				}
				return true;
			}
			return false;
		}

		// Token: 0x04006CEC RID: 27884
		[Serialize]
		public string description;

		// Token: 0x04006CED RID: 27885
		[Serialize]
		public int dataValue;

		// Token: 0x04006CEE RID: 27886
		[Serialize]
		public bool completed;

		// Token: 0x04006CEF RID: 27887
		[Serialize]
		public SimHashes discoveredRareResource = SimHashes.Void;

		// Token: 0x04006CF0 RID: 27888
		[Serialize]
		public string discoveredRareItem;
	}
}
