using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Klei;
using ProcGenGame;
using STRINGS;
using UnityEngine;

// Token: 0x020007F6 RID: 2038
public class ElementLoader
{
	// Token: 0x06003AC5 RID: 15045 RVA: 0x00145AF8 File Offset: 0x00143CF8
	public static List<ElementLoader.ElementEntry> CollectElementsFromYAML()
	{
		List<ElementLoader.ElementEntry> list = new List<ElementLoader.ElementEntry>();
		ListPool<FileHandle, ElementLoader>.PooledList pooledList = ListPool<FileHandle, ElementLoader>.Allocate();
		FileSystem.GetFiles(FileSystem.Normalize(ElementLoader.path), "*.yaml", pooledList);
		ListPool<YamlIO.Error, ElementLoader>.PooledList errors = ListPool<YamlIO.Error, ElementLoader>.Allocate();
		YamlIO.ErrorHandler <>9__0;
		foreach (FileHandle fileHandle in pooledList)
		{
			string full_path = fileHandle.full_path;
			YamlIO.ErrorHandler errorHandler;
			if ((errorHandler = <>9__0) == null)
			{
				errorHandler = (<>9__0 = delegate(YamlIO.Error error, bool force_log_as_warning)
				{
					errors.Add(error);
				});
			}
			ElementLoader.ElementEntryCollection elementEntryCollection = YamlIO.LoadFile<ElementLoader.ElementEntryCollection>(full_path, errorHandler, null);
			if (elementEntryCollection != null)
			{
				list.AddRange(elementEntryCollection.elements);
			}
		}
		pooledList.Recycle();
		if (Global.Instance != null && Global.Instance.modManager != null)
		{
			Global.Instance.modManager.HandleErrors(errors);
		}
		errors.Recycle();
		return list;
	}

	// Token: 0x06003AC6 RID: 15046 RVA: 0x00145BF0 File Offset: 0x00143DF0
	public static void Load(ref Hashtable substanceList, Dictionary<string, SubstanceTable> substanceTablesByDlc)
	{
		ElementLoader.elements = new List<Element>();
		ElementLoader.elementTable = new Dictionary<int, Element>();
		ElementLoader.elementTagTable = new Dictionary<Tag, Element>();
		foreach (ElementLoader.ElementEntry elementEntry in ElementLoader.CollectElementsFromYAML())
		{
			int num = Hash.SDBMLower(elementEntry.elementId);
			if (!ElementLoader.elementTable.ContainsKey(num) && substanceTablesByDlc.ContainsKey(elementEntry.dlcId))
			{
				Element element = new Element();
				element.id = (SimHashes)num;
				element.name = Strings.Get(elementEntry.localizationID);
				element.nameUpperCase = element.name.ToUpper();
				element.description = Strings.Get(elementEntry.description);
				element.tag = TagManager.Create(elementEntry.elementId, element.name);
				ElementLoader.CopyEntryToElement(elementEntry, element);
				ElementLoader.elements.Add(element);
				ElementLoader.elementTable[num] = element;
				ElementLoader.elementTagTable[element.tag] = element;
				if (!ElementLoader.ManifestSubstanceForElement(element, ref substanceList, substanceTablesByDlc[elementEntry.dlcId]))
				{
					global::Debug.LogWarning("Missing substance for element: " + element.id.ToString());
				}
			}
		}
		ElementLoader.FinaliseElementsTable(ref substanceList);
		WorldGen.SetupDefaultElements();
	}

	// Token: 0x06003AC7 RID: 15047 RVA: 0x00145D68 File Offset: 0x00143F68
	private static void CopyEntryToElement(ElementLoader.ElementEntry entry, Element elem)
	{
		Hash.SDBMLower(entry.elementId);
		elem.tag = TagManager.Create(entry.elementId.ToString());
		elem.specificHeatCapacity = entry.specificHeatCapacity;
		elem.thermalConductivity = entry.thermalConductivity;
		elem.molarMass = entry.molarMass;
		elem.strength = entry.strength;
		elem.disabled = entry.isDisabled;
		elem.dlcId = entry.dlcId;
		elem.flow = entry.flow;
		elem.maxMass = entry.maxMass;
		elem.maxCompression = entry.liquidCompression;
		elem.viscosity = entry.speed;
		elem.minHorizontalFlow = entry.minHorizontalFlow;
		elem.minVerticalFlow = entry.minVerticalFlow;
		elem.solidSurfaceAreaMultiplier = entry.solidSurfaceAreaMultiplier;
		elem.liquidSurfaceAreaMultiplier = entry.liquidSurfaceAreaMultiplier;
		elem.gasSurfaceAreaMultiplier = entry.gasSurfaceAreaMultiplier;
		elem.state = entry.state;
		elem.hardness = entry.hardness;
		elem.lowTemp = entry.lowTemp;
		elem.lowTempTransitionTarget = (SimHashes)Hash.SDBMLower(entry.lowTempTransitionTarget);
		elem.highTemp = entry.highTemp;
		elem.highTempTransitionTarget = (SimHashes)Hash.SDBMLower(entry.highTempTransitionTarget);
		elem.highTempTransitionOreID = (SimHashes)Hash.SDBMLower(entry.highTempTransitionOreId);
		elem.highTempTransitionOreMassConversion = entry.highTempTransitionOreMassConversion;
		elem.lowTempTransitionOreID = (SimHashes)Hash.SDBMLower(entry.lowTempTransitionOreId);
		elem.lowTempTransitionOreMassConversion = entry.lowTempTransitionOreMassConversion;
		elem.sublimateId = (SimHashes)Hash.SDBMLower(entry.sublimateId);
		elem.convertId = (SimHashes)Hash.SDBMLower(entry.convertId);
		elem.sublimateFX = (SpawnFXHashes)Hash.SDBMLower(entry.sublimateFx);
		elem.sublimateRate = entry.sublimateRate;
		elem.sublimateEfficiency = entry.sublimateEfficiency;
		elem.sublimateProbability = entry.sublimateProbability;
		elem.offGasPercentage = entry.offGasPercentage;
		elem.lightAbsorptionFactor = entry.lightAbsorptionFactor;
		elem.radiationAbsorptionFactor = entry.radiationAbsorptionFactor;
		elem.radiationPer1000Mass = entry.radiationPer1000Mass;
		elem.toxicity = entry.toxicity;
		elem.elementComposition = entry.composition;
		Tag tag = TagManager.Create(entry.state.ToString());
		elem.materialCategory = ElementLoader.CreateMaterialCategoryTag(elem.id, tag, entry.materialCategory);
		elem.oreTags = ElementLoader.CreateOreTags(elem.materialCategory, tag, entry.tags);
		elem.buildMenuSort = entry.buildMenuSort;
		Sim.PhysicsData physicsData = default(Sim.PhysicsData);
		physicsData.temperature = entry.defaultTemperature;
		physicsData.mass = entry.defaultMass;
		physicsData.pressure = entry.defaultPressure;
		switch (entry.state)
		{
		case Element.State.Gas:
			GameTags.GasElements.Add(elem.tag);
			physicsData.mass = 1f;
			elem.maxMass = 1.8f;
			break;
		case Element.State.Liquid:
			GameTags.LiquidElements.Add(elem.tag);
			break;
		case Element.State.Solid:
			GameTags.SolidElements.Add(elem.tag);
			break;
		}
		elem.defaultValues = physicsData;
	}

	// Token: 0x06003AC8 RID: 15048 RVA: 0x0014606C File Offset: 0x0014426C
	private static bool ManifestSubstanceForElement(Element elem, ref Hashtable substanceList, SubstanceTable substanceTable)
	{
		elem.substance = null;
		if (substanceList.ContainsKey(elem.id))
		{
			elem.substance = substanceList[elem.id] as Substance;
			return false;
		}
		if (substanceTable != null)
		{
			elem.substance = substanceTable.GetSubstance(elem.id);
		}
		if (elem.substance == null)
		{
			elem.substance = new Substance();
			substanceTable.GetList().Add(elem.substance);
		}
		elem.substance.elementID = elem.id;
		elem.substance.renderedByWorld = elem.IsSolid;
		elem.substance.idx = substanceList.Count;
		if (elem.substance.uiColour == ElementLoader.noColour)
		{
			int count = ElementLoader.elements.Count;
			int idx = elem.substance.idx;
			elem.substance.uiColour = Color.HSVToRGB((float)idx / (float)count, 1f, 1f);
		}
		string text = UI.StripLinkFormatting(elem.name);
		elem.substance.name = text;
		if (Array.IndexOf<SimHashes>((SimHashes[])Enum.GetValues(typeof(SimHashes)), elem.id) >= 0)
		{
			elem.substance.nameTag = GameTagExtensions.Create(elem.id);
		}
		else
		{
			elem.substance.nameTag = ((text != null) ? TagManager.Create(text) : Tag.Invalid);
		}
		elem.substance.audioConfig = ElementsAudio.Instance.GetConfigForElement(elem.id);
		substanceList.Add(elem.id, elem.substance);
		return true;
	}

	// Token: 0x06003AC9 RID: 15049 RVA: 0x00146220 File Offset: 0x00144420
	public static Element FindElementByName(string name)
	{
		Element element;
		try
		{
			element = ElementLoader.FindElementByHash((SimHashes)Enum.Parse(typeof(SimHashes), name));
		}
		catch
		{
			element = ElementLoader.FindElementByHash((SimHashes)Hash.SDBMLower(name));
		}
		return element;
	}

	// Token: 0x06003ACA RID: 15050 RVA: 0x0014626C File Offset: 0x0014446C
	public static Element FindElementByHash(SimHashes hash)
	{
		Element element = null;
		ElementLoader.elementTable.TryGetValue((int)hash, out element);
		return element;
	}

	// Token: 0x06003ACB RID: 15051 RVA: 0x0014628C File Offset: 0x0014448C
	public static ushort GetElementIndex(SimHashes hash)
	{
		Element element = null;
		ElementLoader.elementTable.TryGetValue((int)hash, out element);
		if (element != null)
		{
			return element.idx;
		}
		return ushort.MaxValue;
	}

	// Token: 0x06003ACC RID: 15052 RVA: 0x001462B8 File Offset: 0x001444B8
	public static Element GetElement(Tag tag)
	{
		Element element;
		ElementLoader.elementTagTable.TryGetValue(tag, out element);
		return element;
	}

	// Token: 0x06003ACD RID: 15053 RVA: 0x001462D4 File Offset: 0x001444D4
	public static SimHashes GetElementID(Tag tag)
	{
		Element element;
		ElementLoader.elementTagTable.TryGetValue(tag, out element);
		if (element != null)
		{
			return element.id;
		}
		return SimHashes.Vacuum;
	}

	// Token: 0x06003ACE RID: 15054 RVA: 0x00146300 File Offset: 0x00144500
	private static SimHashes GetID(int column, int row, string[,] grid, SimHashes defaultValue = SimHashes.Vacuum)
	{
		if (column >= grid.GetLength(0) || row > grid.GetLength(1))
		{
			global::Debug.LogError(string.Format("Could not find element at loc [{0},{1}] grid is only [{2},{3}]", new object[]
			{
				column,
				row,
				grid.GetLength(0),
				grid.GetLength(1)
			}));
			return defaultValue;
		}
		string text = grid[column, row];
		if (text == null || text == "")
		{
			return defaultValue;
		}
		object obj = null;
		try
		{
			obj = Enum.Parse(typeof(SimHashes), text);
		}
		catch (Exception ex)
		{
			global::Debug.LogError(string.Format("Could not find element {0}: {1}", text, ex.ToString()));
			return defaultValue;
		}
		return (SimHashes)obj;
	}

	// Token: 0x06003ACF RID: 15055 RVA: 0x001463CC File Offset: 0x001445CC
	private static SpawnFXHashes GetSpawnFX(int column, int row, string[,] grid)
	{
		if (column >= grid.GetLength(0) || row > grid.GetLength(1))
		{
			global::Debug.LogError(string.Format("Could not find SpawnFXHashes at loc [{0},{1}] grid is only [{2},{3}]", new object[]
			{
				column,
				row,
				grid.GetLength(0),
				grid.GetLength(1)
			}));
			return SpawnFXHashes.None;
		}
		string text = grid[column, row];
		if (text == null || text == "")
		{
			return SpawnFXHashes.None;
		}
		object obj = null;
		try
		{
			obj = Enum.Parse(typeof(SpawnFXHashes), text);
		}
		catch (Exception ex)
		{
			global::Debug.LogError(string.Format("Could not find FX {0}: {1}", text, ex.ToString()));
			return SpawnFXHashes.None;
		}
		return (SpawnFXHashes)obj;
	}

	// Token: 0x06003AD0 RID: 15056 RVA: 0x00146498 File Offset: 0x00144698
	private static Tag CreateMaterialCategoryTag(SimHashes element_id, Tag phaseTag, string materialCategoryField)
	{
		if (!string.IsNullOrEmpty(materialCategoryField))
		{
			Tag tag = TagManager.Create(materialCategoryField);
			if (!GameTags.MaterialCategories.Contains(tag) && !GameTags.IgnoredMaterialCategories.Contains(tag))
			{
				global::Debug.LogWarningFormat("Element {0} has category {1}, but that isn't in GameTags.MaterialCategores!", new object[] { element_id, materialCategoryField });
			}
			return tag;
		}
		return phaseTag;
	}

	// Token: 0x06003AD1 RID: 15057 RVA: 0x001464F0 File Offset: 0x001446F0
	private static Tag[] CreateOreTags(Tag materialCategory, Tag phaseTag, string[] ore_tags_split)
	{
		List<Tag> list = new List<Tag>();
		if (ore_tags_split != null)
		{
			foreach (string text in ore_tags_split)
			{
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(TagManager.Create(text));
				}
			}
		}
		list.Add(phaseTag);
		if (materialCategory.IsValid && !list.Contains(materialCategory))
		{
			list.Add(materialCategory);
		}
		return list.ToArray();
	}

	// Token: 0x06003AD2 RID: 15058 RVA: 0x00146554 File Offset: 0x00144754
	private static void FinaliseElementsTable(ref Hashtable substanceList)
	{
		foreach (Element element in ElementLoader.elements)
		{
			if (element != null)
			{
				if (element.substance == null)
				{
					global::Debug.LogWarning("Skipping finalise for missing element: " + element.id.ToString());
				}
				else
				{
					global::Debug.Assert(element.substance.nameTag.IsValid);
					if (element.thermalConductivity == 0f)
					{
						element.state |= Element.State.TemperatureInsulated;
					}
					if (element.strength == 0f)
					{
						element.state |= Element.State.Unbreakable;
					}
					if (element.IsSolid)
					{
						Element element2 = ElementLoader.FindElementByHash(element.highTempTransitionTarget);
						if (element2 != null)
						{
							element.highTempTransition = element2;
						}
					}
					else if (element.IsLiquid)
					{
						Element element3 = ElementLoader.FindElementByHash(element.highTempTransitionTarget);
						if (element3 != null)
						{
							element.highTempTransition = element3;
						}
						Element element4 = ElementLoader.FindElementByHash(element.lowTempTransitionTarget);
						if (element4 != null)
						{
							element.lowTempTransition = element4;
						}
					}
					else if (element.IsGas)
					{
						Element element5 = ElementLoader.FindElementByHash(element.lowTempTransitionTarget);
						if (element5 != null)
						{
							element.lowTempTransition = element5;
						}
					}
				}
			}
		}
		ElementLoader.elements = (from e in ElementLoader.elements
			orderby (int)(e.state & Element.State.Solid) descending, e.id
			select e).ToList<Element>();
		for (int i = 0; i < ElementLoader.elements.Count; i++)
		{
			if (ElementLoader.elements[i].substance != null)
			{
				ElementLoader.elements[i].substance.idx = i;
			}
			ElementLoader.elements[i].idx = (ushort)i;
		}
	}

	// Token: 0x06003AD3 RID: 15059 RVA: 0x0014675C File Offset: 0x0014495C
	private static void ValidateElements()
	{
		global::Debug.Log("------ Start Validating Elements ------");
		foreach (Element element in ElementLoader.elements)
		{
			string text = string.Format("{0} ({1})", element.tag.ProperNameStripLink(), element.state);
			if (element.IsLiquid && element.sublimateId != (SimHashes)0)
			{
				global::Debug.Assert(element.sublimateRate == 0f, text + ": Liquids don't use sublimateRate, use offGasPercentage instead.");
				global::Debug.Assert(element.offGasPercentage > 0f, text + ": Missing offGasPercentage");
			}
			if (element.IsSolid && element.sublimateId != (SimHashes)0)
			{
				global::Debug.Assert(element.offGasPercentage == 0f, text + ": Solids don't use offGasPercentage, use sublimateRate instead.");
				global::Debug.Assert(element.sublimateRate > 0f, text + ": Missing sublimationRate");
				global::Debug.Assert(element.sublimateRate * element.sublimateEfficiency > 0.001f, text + ": Sublimation rate and efficiency will result in gas that will be obliterated because its less than 1g. Increase these values and use sublimateProbability if you want a low amount of sublimation");
			}
			if (element.highTempTransition != null && element.highTempTransition.lowTempTransition == element)
			{
				global::Debug.Assert(element.highTemp >= element.highTempTransition.lowTemp, text + ": highTemp is higher than transition element's (" + element.highTempTransition.tag.ProperNameStripLink() + ") lowTemp");
			}
			global::Debug.Assert(element.defaultValues.mass <= element.maxMass, text + ": Default mass should be less than max mass");
			if (false)
			{
				if (element.IsSolid && element.highTempTransition != null && element.highTempTransition.IsLiquid && element.defaultValues.mass > element.highTempTransition.maxMass)
				{
					global::Debug.LogWarning(string.Format("{0} defaultMass {1} > {2}: maxMass {3}", new object[]
					{
						text,
						element.defaultValues.mass,
						element.highTempTransition.tag.ProperNameStripLink(),
						element.highTempTransition.maxMass
					}));
				}
				if (element.defaultValues.mass < element.maxMass && element.IsLiquid)
				{
					global::Debug.LogWarning(string.Format("{0} has defaultMass: {1} and maxMass {2}", element.tag.ProperNameStripLink(), element.defaultValues.mass, element.maxMass));
				}
			}
		}
		global::Debug.Log("------ End Validating Elements ------");
	}

	// Token: 0x04002689 RID: 9865
	public static List<Element> elements;

	// Token: 0x0400268A RID: 9866
	public static Dictionary<int, Element> elementTable;

	// Token: 0x0400268B RID: 9867
	public static Dictionary<Tag, Element> elementTagTable;

	// Token: 0x0400268C RID: 9868
	private static string path = Application.streamingAssetsPath + "/elements/";

	// Token: 0x0400268D RID: 9869
	private static readonly Color noColour = new Color(0f, 0f, 0f, 0f);

	// Token: 0x02001549 RID: 5449
	public class ElementEntryCollection
	{
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600832B RID: 33579 RVA: 0x002E78E0 File Offset: 0x002E5AE0
		// (set) Token: 0x0600832C RID: 33580 RVA: 0x002E78E8 File Offset: 0x002E5AE8
		public ElementLoader.ElementEntry[] elements { get; set; }
	}

	// Token: 0x0200154A RID: 5450
	public class ElementComposition
	{
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600832F RID: 33583 RVA: 0x002E7901 File Offset: 0x002E5B01
		// (set) Token: 0x06008330 RID: 33584 RVA: 0x002E7909 File Offset: 0x002E5B09
		public string elementID { get; set; }

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06008331 RID: 33585 RVA: 0x002E7912 File Offset: 0x002E5B12
		// (set) Token: 0x06008332 RID: 33586 RVA: 0x002E791A File Offset: 0x002E5B1A
		public float percentage { get; set; }
	}

	// Token: 0x0200154B RID: 5451
	public class ElementEntry
	{
		// Token: 0x06008333 RID: 33587 RVA: 0x002E7923 File Offset: 0x002E5B23
		public ElementEntry()
		{
			this.lowTemp = 0f;
			this.highTemp = 10000f;
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06008334 RID: 33588 RVA: 0x002E7941 File Offset: 0x002E5B41
		// (set) Token: 0x06008335 RID: 33589 RVA: 0x002E7949 File Offset: 0x002E5B49
		public string elementId { get; set; }

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06008336 RID: 33590 RVA: 0x002E7952 File Offset: 0x002E5B52
		// (set) Token: 0x06008337 RID: 33591 RVA: 0x002E795A File Offset: 0x002E5B5A
		public float specificHeatCapacity { get; set; }

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06008338 RID: 33592 RVA: 0x002E7963 File Offset: 0x002E5B63
		// (set) Token: 0x06008339 RID: 33593 RVA: 0x002E796B File Offset: 0x002E5B6B
		public float thermalConductivity { get; set; }

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600833A RID: 33594 RVA: 0x002E7974 File Offset: 0x002E5B74
		// (set) Token: 0x0600833B RID: 33595 RVA: 0x002E797C File Offset: 0x002E5B7C
		public float solidSurfaceAreaMultiplier { get; set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x0600833C RID: 33596 RVA: 0x002E7985 File Offset: 0x002E5B85
		// (set) Token: 0x0600833D RID: 33597 RVA: 0x002E798D File Offset: 0x002E5B8D
		public float liquidSurfaceAreaMultiplier { get; set; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600833E RID: 33598 RVA: 0x002E7996 File Offset: 0x002E5B96
		// (set) Token: 0x0600833F RID: 33599 RVA: 0x002E799E File Offset: 0x002E5B9E
		public float gasSurfaceAreaMultiplier { get; set; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06008340 RID: 33600 RVA: 0x002E79A7 File Offset: 0x002E5BA7
		// (set) Token: 0x06008341 RID: 33601 RVA: 0x002E79AF File Offset: 0x002E5BAF
		public float defaultMass { get; set; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06008342 RID: 33602 RVA: 0x002E79B8 File Offset: 0x002E5BB8
		// (set) Token: 0x06008343 RID: 33603 RVA: 0x002E79C0 File Offset: 0x002E5BC0
		public float defaultTemperature { get; set; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06008344 RID: 33604 RVA: 0x002E79C9 File Offset: 0x002E5BC9
		// (set) Token: 0x06008345 RID: 33605 RVA: 0x002E79D1 File Offset: 0x002E5BD1
		public float defaultPressure { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06008346 RID: 33606 RVA: 0x002E79DA File Offset: 0x002E5BDA
		// (set) Token: 0x06008347 RID: 33607 RVA: 0x002E79E2 File Offset: 0x002E5BE2
		public float molarMass { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06008348 RID: 33608 RVA: 0x002E79EB File Offset: 0x002E5BEB
		// (set) Token: 0x06008349 RID: 33609 RVA: 0x002E79F3 File Offset: 0x002E5BF3
		public float lightAbsorptionFactor { get; set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600834A RID: 33610 RVA: 0x002E79FC File Offset: 0x002E5BFC
		// (set) Token: 0x0600834B RID: 33611 RVA: 0x002E7A04 File Offset: 0x002E5C04
		public float radiationAbsorptionFactor { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600834C RID: 33612 RVA: 0x002E7A0D File Offset: 0x002E5C0D
		// (set) Token: 0x0600834D RID: 33613 RVA: 0x002E7A15 File Offset: 0x002E5C15
		public float radiationPer1000Mass { get; set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600834E RID: 33614 RVA: 0x002E7A1E File Offset: 0x002E5C1E
		// (set) Token: 0x0600834F RID: 33615 RVA: 0x002E7A26 File Offset: 0x002E5C26
		public string lowTempTransitionTarget { get; set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06008350 RID: 33616 RVA: 0x002E7A2F File Offset: 0x002E5C2F
		// (set) Token: 0x06008351 RID: 33617 RVA: 0x002E7A37 File Offset: 0x002E5C37
		public float lowTemp { get; set; }

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06008352 RID: 33618 RVA: 0x002E7A40 File Offset: 0x002E5C40
		// (set) Token: 0x06008353 RID: 33619 RVA: 0x002E7A48 File Offset: 0x002E5C48
		public string highTempTransitionTarget { get; set; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06008354 RID: 33620 RVA: 0x002E7A51 File Offset: 0x002E5C51
		// (set) Token: 0x06008355 RID: 33621 RVA: 0x002E7A59 File Offset: 0x002E5C59
		public float highTemp { get; set; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06008356 RID: 33622 RVA: 0x002E7A62 File Offset: 0x002E5C62
		// (set) Token: 0x06008357 RID: 33623 RVA: 0x002E7A6A File Offset: 0x002E5C6A
		public string lowTempTransitionOreId { get; set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06008358 RID: 33624 RVA: 0x002E7A73 File Offset: 0x002E5C73
		// (set) Token: 0x06008359 RID: 33625 RVA: 0x002E7A7B File Offset: 0x002E5C7B
		public float lowTempTransitionOreMassConversion { get; set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600835A RID: 33626 RVA: 0x002E7A84 File Offset: 0x002E5C84
		// (set) Token: 0x0600835B RID: 33627 RVA: 0x002E7A8C File Offset: 0x002E5C8C
		public string highTempTransitionOreId { get; set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600835C RID: 33628 RVA: 0x002E7A95 File Offset: 0x002E5C95
		// (set) Token: 0x0600835D RID: 33629 RVA: 0x002E7A9D File Offset: 0x002E5C9D
		public float highTempTransitionOreMassConversion { get; set; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600835E RID: 33630 RVA: 0x002E7AA6 File Offset: 0x002E5CA6
		// (set) Token: 0x0600835F RID: 33631 RVA: 0x002E7AAE File Offset: 0x002E5CAE
		public string sublimateId { get; set; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06008360 RID: 33632 RVA: 0x002E7AB7 File Offset: 0x002E5CB7
		// (set) Token: 0x06008361 RID: 33633 RVA: 0x002E7ABF File Offset: 0x002E5CBF
		public string sublimateFx { get; set; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06008362 RID: 33634 RVA: 0x002E7AC8 File Offset: 0x002E5CC8
		// (set) Token: 0x06008363 RID: 33635 RVA: 0x002E7AD0 File Offset: 0x002E5CD0
		public float sublimateRate { get; set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06008364 RID: 33636 RVA: 0x002E7AD9 File Offset: 0x002E5CD9
		// (set) Token: 0x06008365 RID: 33637 RVA: 0x002E7AE1 File Offset: 0x002E5CE1
		public float sublimateEfficiency { get; set; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06008366 RID: 33638 RVA: 0x002E7AEA File Offset: 0x002E5CEA
		// (set) Token: 0x06008367 RID: 33639 RVA: 0x002E7AF2 File Offset: 0x002E5CF2
		public float sublimateProbability { get; set; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06008368 RID: 33640 RVA: 0x002E7AFB File Offset: 0x002E5CFB
		// (set) Token: 0x06008369 RID: 33641 RVA: 0x002E7B03 File Offset: 0x002E5D03
		public float offGasPercentage { get; set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600836A RID: 33642 RVA: 0x002E7B0C File Offset: 0x002E5D0C
		// (set) Token: 0x0600836B RID: 33643 RVA: 0x002E7B14 File Offset: 0x002E5D14
		public string materialCategory { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600836C RID: 33644 RVA: 0x002E7B1D File Offset: 0x002E5D1D
		// (set) Token: 0x0600836D RID: 33645 RVA: 0x002E7B25 File Offset: 0x002E5D25
		public string[] tags { get; set; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600836E RID: 33646 RVA: 0x002E7B2E File Offset: 0x002E5D2E
		// (set) Token: 0x0600836F RID: 33647 RVA: 0x002E7B36 File Offset: 0x002E5D36
		public bool isDisabled { get; set; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06008370 RID: 33648 RVA: 0x002E7B3F File Offset: 0x002E5D3F
		// (set) Token: 0x06008371 RID: 33649 RVA: 0x002E7B47 File Offset: 0x002E5D47
		public float strength { get; set; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06008372 RID: 33650 RVA: 0x002E7B50 File Offset: 0x002E5D50
		// (set) Token: 0x06008373 RID: 33651 RVA: 0x002E7B58 File Offset: 0x002E5D58
		public float maxMass { get; set; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06008374 RID: 33652 RVA: 0x002E7B61 File Offset: 0x002E5D61
		// (set) Token: 0x06008375 RID: 33653 RVA: 0x002E7B69 File Offset: 0x002E5D69
		public byte hardness { get; set; }

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06008376 RID: 33654 RVA: 0x002E7B72 File Offset: 0x002E5D72
		// (set) Token: 0x06008377 RID: 33655 RVA: 0x002E7B7A File Offset: 0x002E5D7A
		public float toxicity { get; set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06008378 RID: 33656 RVA: 0x002E7B83 File Offset: 0x002E5D83
		// (set) Token: 0x06008379 RID: 33657 RVA: 0x002E7B8B File Offset: 0x002E5D8B
		public float liquidCompression { get; set; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x0600837A RID: 33658 RVA: 0x002E7B94 File Offset: 0x002E5D94
		// (set) Token: 0x0600837B RID: 33659 RVA: 0x002E7B9C File Offset: 0x002E5D9C
		public float speed { get; set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600837C RID: 33660 RVA: 0x002E7BA5 File Offset: 0x002E5DA5
		// (set) Token: 0x0600837D RID: 33661 RVA: 0x002E7BAD File Offset: 0x002E5DAD
		public float minHorizontalFlow { get; set; }

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600837E RID: 33662 RVA: 0x002E7BB6 File Offset: 0x002E5DB6
		// (set) Token: 0x0600837F RID: 33663 RVA: 0x002E7BBE File Offset: 0x002E5DBE
		public float minVerticalFlow { get; set; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06008380 RID: 33664 RVA: 0x002E7BC7 File Offset: 0x002E5DC7
		// (set) Token: 0x06008381 RID: 33665 RVA: 0x002E7BCF File Offset: 0x002E5DCF
		public string convertId { get; set; }

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06008382 RID: 33666 RVA: 0x002E7BD8 File Offset: 0x002E5DD8
		// (set) Token: 0x06008383 RID: 33667 RVA: 0x002E7BE0 File Offset: 0x002E5DE0
		public float flow { get; set; }

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06008384 RID: 33668 RVA: 0x002E7BE9 File Offset: 0x002E5DE9
		// (set) Token: 0x06008385 RID: 33669 RVA: 0x002E7BF1 File Offset: 0x002E5DF1
		public int buildMenuSort { get; set; }

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06008386 RID: 33670 RVA: 0x002E7BFA File Offset: 0x002E5DFA
		// (set) Token: 0x06008387 RID: 33671 RVA: 0x002E7C02 File Offset: 0x002E5E02
		public Element.State state { get; set; }

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06008388 RID: 33672 RVA: 0x002E7C0B File Offset: 0x002E5E0B
		// (set) Token: 0x06008389 RID: 33673 RVA: 0x002E7C13 File Offset: 0x002E5E13
		public string localizationID { get; set; }

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600838A RID: 33674 RVA: 0x002E7C1C File Offset: 0x002E5E1C
		// (set) Token: 0x0600838B RID: 33675 RVA: 0x002E7C24 File Offset: 0x002E5E24
		public string dlcId { get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600838C RID: 33676 RVA: 0x002E7C2D File Offset: 0x002E5E2D
		// (set) Token: 0x0600838D RID: 33677 RVA: 0x002E7C35 File Offset: 0x002E5E35
		public ElementLoader.ElementComposition[] composition { get; set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600838E RID: 33678 RVA: 0x002E7C3E File Offset: 0x002E5E3E
		// (set) Token: 0x0600838F RID: 33679 RVA: 0x002E7C69 File Offset: 0x002E5E69
		public string description
		{
			get
			{
				return this.description_backing ?? ("STRINGS.ELEMENTS." + this.elementId.ToString().ToUpper() + ".DESC");
			}
			set
			{
				this.description_backing = value;
			}
		}

		// Token: 0x04006652 RID: 26194
		private string description_backing;
	}
}
