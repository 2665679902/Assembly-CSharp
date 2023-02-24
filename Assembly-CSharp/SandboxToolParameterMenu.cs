using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000B76 RID: 2934
public class SandboxToolParameterMenu : KScreen
{
	// Token: 0x06005C20 RID: 23584 RVA: 0x0021A2D5 File Offset: 0x002184D5
	public static void DestroyInstance()
	{
		SandboxToolParameterMenu.instance = null;
	}

	// Token: 0x06005C21 RID: 23585 RVA: 0x0021A2DD File Offset: 0x002184DD
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x06005C22 RID: 23586 RVA: 0x0021A2E4 File Offset: 0x002184E4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.ConfigureSettings();
		this.activateOnSpawn = true;
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005C23 RID: 23587 RVA: 0x0021A300 File Offset: 0x00218500
	private void ConfigureSettings()
	{
		this.massSlider.clampValueLow = 0.001f;
		this.massSlider.clampValueHigh = 10000f;
		this.temperatureAdditiveSlider.clampValueLow = -9999f;
		this.temperatureAdditiveSlider.clampValueHigh = 9999f;
		this.temperatureSlider.clampValueLow = -458f;
		this.temperatureSlider.clampValueHigh = 9999f;
		this.brushRadiusSlider.clampValueLow = 1f;
		this.brushRadiusSlider.clampValueHigh = 50f;
		this.diseaseCountSlider.clampValueHigh = 1000000f;
		this.diseaseCountSlider.slideMaxValue = 1000000f;
		this.settings = new SandboxSettings();
		SandboxSettings sandboxSettings = this.settings;
		sandboxSettings.OnChangeElement = (Action<bool>)Delegate.Combine(sandboxSettings.OnChangeElement, new Action<bool>(delegate(bool forceElementDefaults)
		{
			int num = this.settings.GetIntSetting("SandboxTools.SelectedElement");
			if (num >= ElementLoader.elements.Count)
			{
				num = 0;
			}
			Element element = ElementLoader.elements[num];
			this.elementSelector.button.GetComponentInChildren<LocText>().text = element.name + " (" + element.GetStateString() + ")";
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(element, "ui", false);
			this.elementSelector.button.GetComponentsInChildren<Image>()[1].sprite = uisprite.first;
			this.elementSelector.button.GetComponentsInChildren<Image>()[1].color = uisprite.second;
			this.SetAbsoluteTemperatureSliderRange(element);
			this.massSlider.SetRange(0.1f, Mathf.Min(element.maxMass * 2f, this.massSlider.clampValueHigh), false);
			if (forceElementDefaults)
			{
				this.temperatureSlider.SetValue(GameUtil.GetConvertedTemperature(element.defaultValues.temperature, true), true);
				this.massSlider.SetValue(element.defaultValues.mass, true);
			}
		}));
		SandboxSettings sandboxSettings2 = this.settings;
		sandboxSettings2.OnChangeMass = (System.Action)Delegate.Combine(sandboxSettings2.OnChangeMass, new System.Action(delegate
		{
			this.massSlider.SetValue(this.settings.GetFloatSetting("SandboxTools.Mass"), false);
		}));
		SandboxSettings sandboxSettings3 = this.settings;
		sandboxSettings3.OnChangeDisease = (System.Action)Delegate.Combine(sandboxSettings3.OnChangeDisease, new System.Action(delegate
		{
			Disease disease = Db.Get().Diseases.TryGet(SandboxToolParameterMenu.instance.settings.GetStringSetting("SandboxTools.SelectedDisease"));
			if (disease == null)
			{
				disease = Db.Get().Diseases.Get("FoodPoisoning");
			}
			this.diseaseSelector.button.GetComponentInChildren<LocText>().text = disease.Name;
			this.diseaseSelector.button.GetComponentsInChildren<Image>()[1].sprite = Assets.GetSprite("germ");
			this.diseaseCountSlider.SetRange(0f, 1000000f, false);
		}));
		SandboxSettings sandboxSettings4 = this.settings;
		sandboxSettings4.OnChangeDiseaseCount = (System.Action)Delegate.Combine(sandboxSettings4.OnChangeDiseaseCount, new System.Action(delegate
		{
			this.diseaseCountSlider.SetValue((float)this.settings.GetIntSetting("SandboxTools.DiseaseCount"), false);
		}));
		SandboxSettings sandboxSettings5 = this.settings;
		sandboxSettings5.OnChangeEntity = (System.Action)Delegate.Combine(sandboxSettings5.OnChangeEntity, new System.Action(delegate
		{
			string stringSetting = SandboxToolParameterMenu.instance.settings.GetStringSetting("SandboxTools.SelectedEntity");
			GameObject gameObject = Assets.TryGetPrefab(stringSetting);
			if (gameObject == null)
			{
				this.settings.ForceDefaultStringSetting("SandboxTools.SelectedEntity");
				return;
			}
			this.entitySelector.button.GetComponentInChildren<LocText>().text = gameObject.GetProperName();
			global::Tuple<Sprite, Color> tuple;
			if (stringSetting == MinionConfig.ID)
			{
				tuple = new global::Tuple<Sprite, Color>(Assets.GetSprite("ui_duplicant_portrait_placeholder"), Color.white);
			}
			else
			{
				tuple = Def.GetUISprite(stringSetting, "ui", false);
			}
			if (tuple != null)
			{
				this.entitySelector.button.GetComponentsInChildren<Image>()[1].sprite = tuple.first;
				this.entitySelector.button.GetComponentsInChildren<Image>()[1].color = tuple.second;
			}
		}));
		SandboxSettings sandboxSettings6 = this.settings;
		sandboxSettings6.OnChangeBrushSize = (System.Action)Delegate.Combine(sandboxSettings6.OnChangeBrushSize, new System.Action(delegate
		{
			if (PlayerController.Instance.ActiveTool is BrushTool)
			{
				(PlayerController.Instance.ActiveTool as BrushTool).SetBrushSize(this.settings.GetIntSetting("SandboxTools.BrushSize"));
			}
		}));
		SandboxSettings sandboxSettings7 = this.settings;
		sandboxSettings7.OnChangeNoiseScale = (System.Action)Delegate.Combine(sandboxSettings7.OnChangeNoiseScale, new System.Action(delegate
		{
			if (PlayerController.Instance.ActiveTool is SandboxSprinkleTool)
			{
				(PlayerController.Instance.ActiveTool as SandboxSprinkleTool).SetBrushSize(this.settings.GetIntSetting("SandboxTools.BrushSize"));
			}
		}));
		SandboxSettings sandboxSettings8 = this.settings;
		sandboxSettings8.OnChangeNoiseDensity = (System.Action)Delegate.Combine(sandboxSettings8.OnChangeNoiseDensity, new System.Action(delegate
		{
			if (PlayerController.Instance.ActiveTool is SandboxSprinkleTool)
			{
				(PlayerController.Instance.ActiveTool as SandboxSprinkleTool).SetBrushSize(this.settings.GetIntSetting("SandboxTools.BrushSize"));
			}
		}));
		SandboxSettings sandboxSettings9 = this.settings;
		sandboxSettings9.OnChangeTemperature = (System.Action)Delegate.Combine(sandboxSettings9.OnChangeTemperature, new System.Action(delegate
		{
			this.temperatureSlider.SetValue(GameUtil.GetConvertedTemperature(this.settings.GetFloatSetting("SandbosTools.Temperature"), false), false);
		}));
		SandboxSettings sandboxSettings10 = this.settings;
		sandboxSettings10.OnChangeAdditiveTemperature = (System.Action)Delegate.Combine(sandboxSettings10.OnChangeAdditiveTemperature, new System.Action(delegate
		{
			this.temperatureAdditiveSlider.SetValue(GameUtil.GetConvertedTemperature(this.settings.GetFloatSetting("SandbosTools.TemperatureAdditive"), true), false);
		}));
		Game.Instance.Subscribe(999382396, new Action<object>(this.OnTemperatureUnitChanged));
		SandboxSettings sandboxSettings11 = this.settings;
		sandboxSettings11.OnChangeAdditiveStress = (System.Action)Delegate.Combine(sandboxSettings11.OnChangeAdditiveStress, new System.Action(delegate
		{
			this.stressAdditiveSlider.SetValue(this.settings.GetFloatSetting("SandbosTools.StressAdditive"), false);
		}));
		SandboxSettings sandboxSettings12 = this.settings;
		sandboxSettings12.OnChangeMoraleAdjustment = (System.Action)Delegate.Combine(sandboxSettings12.OnChangeMoraleAdjustment, new System.Action(delegate
		{
			this.moraleSlider.SetValue((float)this.settings.GetIntSetting("SandbosTools.MoraleAdjustment"), false);
		}));
	}

	// Token: 0x06005C24 RID: 23588 RVA: 0x0021A5A8 File Offset: 0x002187A8
	public void DisableParameters()
	{
		this.elementSelector.row.SetActive(false);
		this.entitySelector.row.SetActive(false);
		this.brushRadiusSlider.row.SetActive(false);
		this.noiseScaleSlider.row.SetActive(false);
		this.noiseDensitySlider.row.SetActive(false);
		this.massSlider.row.SetActive(false);
		this.temperatureAdditiveSlider.row.SetActive(false);
		this.temperatureSlider.row.SetActive(false);
		this.diseaseCountSlider.row.SetActive(false);
		this.diseaseSelector.row.SetActive(false);
		this.stressAdditiveSlider.row.SetActive(false);
		this.moraleSlider.row.SetActive(false);
	}

	// Token: 0x06005C25 RID: 23589 RVA: 0x0021A684 File Offset: 0x00218884
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ConfigureElementSelector();
		this.ConfigureDiseaseSelector();
		this.ConfigureEntitySelector();
		this.SpawnSelector(this.entitySelector);
		this.SpawnSelector(this.elementSelector);
		this.SpawnSlider(this.brushRadiusSlider);
		this.SpawnSlider(this.noiseScaleSlider);
		this.SpawnSlider(this.noiseDensitySlider);
		this.SpawnSlider(this.massSlider);
		this.SpawnSlider(this.temperatureSlider);
		this.SpawnSlider(this.temperatureAdditiveSlider);
		this.SpawnSlider(this.stressAdditiveSlider);
		this.SpawnSelector(this.diseaseSelector);
		this.SpawnSlider(this.diseaseCountSlider);
		this.SpawnSlider(this.moraleSlider);
		if (SandboxToolParameterMenu.instance == null)
		{
			SandboxToolParameterMenu.instance = this;
			base.gameObject.SetActive(false);
			this.settings.RestorePrefs();
		}
	}

	// Token: 0x06005C26 RID: 23590 RVA: 0x0021A770 File Offset: 0x00218970
	private void ConfigureElementSelector()
	{
		Func<object, bool> func = (object element) => (element as Element).IsSolid;
		Func<object, bool> func2 = (object element) => (element as Element).IsLiquid;
		Func<object, bool> func3 = (object element) => (element as Element).IsGas;
		List<Element> commonElements = new List<Element>();
		Func<object, bool> func4 = (object element) => commonElements.Contains(element as Element);
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Oxygen));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Water));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Vacuum));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Dirt));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.SandStone));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Cuprite));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Steel));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Algae));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.CrudeOil));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.CarbonDioxide));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Sand));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.SlimeMold));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Granite));
		List<Element> list = new List<Element>();
		foreach (Element element2 in ElementLoader.elements)
		{
			if (!element2.disabled)
			{
				list.Add(element2);
			}
		}
		list.Sort((Element a, Element b) => a.name.CompareTo(b.name));
		object[] array = list.ToArray();
		this.elementSelector = new SandboxToolParameterMenu.SelectorValue(array, delegate(object element)
		{
			this.settings.SetIntSetting("SandboxTools.SelectedElement", (int)((Element)element).idx);
		}, (object element) => (element as Element).name + " (" + (element as Element).GetStateString() + ")", (string filterString, object option) => ((option as Element).name.ToUpper() + (option as Element).GetStateString().ToUpper()).Contains(filterString.ToUpper()), (object element) => Def.GetUISprite(element as Element, "ui", false), UI.SANDBOXTOOLS.SETTINGS.ELEMENT.NAME, new SandboxToolParameterMenu.SelectorValue.SearchFilter[]
		{
			new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.COMMON, func4, null, null),
			new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.SOLID, func, null, Def.GetUISprite(ElementLoader.FindElementByHash(SimHashes.SandStone), "ui", false)),
			new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.LIQUID, func2, null, Def.GetUISprite(ElementLoader.FindElementByHash(SimHashes.Water), "ui", false)),
			new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.GAS, func3, null, Def.GetUISprite(ElementLoader.FindElementByHash(SimHashes.Oxygen), "ui", false))
		});
	}

	// Token: 0x06005C27 RID: 23591 RVA: 0x0021AAC0 File Offset: 0x00218CC0
	private void ConfigureEntitySelector()
	{
		List<SandboxToolParameterMenu.SelectorValue.SearchFilter> list = new List<SandboxToolParameterMenu.SelectorValue.SearchFilter>();
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.FOOD, delegate(object entity)
		{
			string idString = (entity as KPrefabID).PrefabID().ToString();
			return !(entity as KPrefabID).HasTag(GameTags.Egg) && EdiblesManager.GetAllFoodTypes().Find((EdiblesManager.FoodInfo match) => match.Id == idString) != null;
		}, null, Def.GetUISprite(Assets.GetPrefab("MushBar"), "ui", false));
		list.Add(searchFilter);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter2 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.COMETS, (object entity) => (entity as KPrefabID).HasTag(GameTags.Comet), null, Def.GetUISprite(Assets.GetPrefab(CopperCometConfig.ID), "ui", false));
		list.Add(searchFilter2);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter3 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.SPECIAL, (object entity) => (entity as KPrefabID).PrefabID().Name == MinionConfig.ID, null, new global::Tuple<Sprite, Color>(Assets.GetSprite("ui_duplicant_portrait_placeholder"), Color.white));
		list.Add(searchFilter3);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter4 = null;
		searchFilter4 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.CREATURE, (object entity) => false, null, Def.GetUISprite(Assets.GetPrefab("Hatch"), "ui", false));
		list.Add(searchFilter4);
		List<Tag> list2 = new List<Tag>();
		foreach (GameObject gameObject in Assets.GetPrefabsWithTag(GameTags.CreatureBrain))
		{
			CreatureBrain brain = gameObject.GetComponent<CreatureBrain>();
			if (!list2.Contains(brain.species))
			{
				global::Tuple<Sprite, Color> tuple = new global::Tuple<Sprite, Color>(CodexCache.entries[brain.species.ToString().ToUpper()].icon, CodexCache.entries[brain.species.ToString().ToUpper()].iconColor);
				list2.Add(brain.species);
				SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter5 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(Strings.Get("STRINGS.CREATURES.FAMILY_PLURAL." + brain.species.ToString().ToUpper()), delegate(object entity)
				{
					CreatureBrain component = Assets.GetPrefab((entity as KPrefabID).PrefabID()).GetComponent<CreatureBrain>();
					return (entity as KPrefabID).HasTag(GameTags.CreatureBrain) && component.species == brain.species;
				}, searchFilter4, tuple);
				list.Add(searchFilter5);
			}
		}
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter6 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.CREATURE_EGG, (object entity) => (entity as KPrefabID).HasTag(GameTags.Egg), searchFilter4, Def.GetUISprite(Assets.GetPrefab("HatchEgg"), "ui", false));
		list.Add(searchFilter6);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter7 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.EQUIPMENT, delegate(object entity)
		{
			if ((entity as KPrefabID).gameObject == null)
			{
				return false;
			}
			GameObject gameObject2 = (entity as KPrefabID).gameObject;
			return gameObject2 != null && gameObject2.GetComponent<Equippable>() != null;
		}, null, Def.GetUISprite(Assets.GetPrefab("Funky_Vest"), "ui", false));
		list.Add(searchFilter7);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter8 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.PLANTS, delegate(object entity)
		{
			if ((entity as KPrefabID).gameObject == null)
			{
				return false;
			}
			GameObject gameObject3 = (entity as KPrefabID).gameObject;
			return gameObject3 != null && (gameObject3.GetComponent<Harvestable>() != null || gameObject3.GetComponent<WiltCondition>() != null);
		}, null, Def.GetUISprite(Assets.GetPrefab("PrickleFlower"), "ui", false));
		list.Add(searchFilter8);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter9 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.SEEDS, delegate(object entity)
		{
			if ((entity as KPrefabID).gameObject == null)
			{
				return false;
			}
			GameObject gameObject4 = (entity as KPrefabID).gameObject;
			return gameObject4 != null && gameObject4.GetComponent<PlantableSeed>() != null;
		}, searchFilter8, Def.GetUISprite(Assets.GetPrefab("PrickleFlowerSeed"), "ui", false));
		list.Add(searchFilter9);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter10 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.INDUSTRIAL_PRODUCTS, delegate(object entity)
		{
			if ((entity as KPrefabID).gameObject == null)
			{
				return false;
			}
			GameObject gameObject5 = (entity as KPrefabID).gameObject;
			return gameObject5 != null && (gameObject5.HasTag(GameTags.IndustrialIngredient) || gameObject5.HasTag(GameTags.IndustrialProduct) || gameObject5.HasTag(GameTags.Medicine) || gameObject5.HasTag(GameTags.MedicalSupplies));
		}, null, Def.GetUISprite(Assets.GetPrefab("BasicCure"), "ui", false));
		list.Add(searchFilter10);
		List<KPrefabID> list3 = new List<KPrefabID>();
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			using (List<SandboxToolParameterMenu.SelectorValue.SearchFilter>.Enumerator enumerator3 = list.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					if (enumerator3.Current.condition(kprefabID))
					{
						list3.Add(kprefabID);
						break;
					}
				}
			}
		}
		object[] array = list3.ToArray();
		this.entitySelector = new SandboxToolParameterMenu.SelectorValue(array, delegate(object entity)
		{
			this.settings.SetStringSetting("SandboxTools.SelectedEntity", (entity as KPrefabID).PrefabID().Name);
		}, (object entity) => (entity as KPrefabID).GetProperName(), null, delegate(object entity)
		{
			GameObject prefab = Assets.GetPrefab((entity as KPrefabID).PrefabTag);
			if (prefab != null)
			{
				if (prefab.PrefabID() == MinionConfig.ID)
				{
					return new global::Tuple<Sprite, Color>(Assets.GetSprite("ui_duplicant_portrait_placeholder"), Color.white);
				}
				KBatchedAnimController component2 = prefab.GetComponent<KBatchedAnimController>();
				if (component2 != null && component2.AnimFiles.Length != 0 && component2.AnimFiles[0] != null)
				{
					return Def.GetUISprite(prefab, "ui", false);
				}
			}
			return null;
		}, UI.SANDBOXTOOLS.SETTINGS.SPAWN_ENTITY.NAME, list.ToArray());
	}

	// Token: 0x06005C28 RID: 23592 RVA: 0x0021B028 File Offset: 0x00219228
	private void ConfigureDiseaseSelector()
	{
		object[] array = Db.Get().Diseases.resources.ToArray();
		this.diseaseSelector = new SandboxToolParameterMenu.SelectorValue(array, delegate(object disease)
		{
			this.settings.SetStringSetting("SandboxTools.SelectedDisease", ((Disease)disease).Id);
		}, (object disease) => (disease as Disease).Name, null, (object disease) => new global::Tuple<Sprite, Color>(Assets.GetSprite("germ"), GlobalAssets.Instance.colorSet.GetColorByName((disease as Disease).overlayColourName)), UI.SANDBOXTOOLS.SETTINGS.DISEASE.NAME, null);
	}

	// Token: 0x06005C29 RID: 23593 RVA: 0x0021B0AC File Offset: 0x002192AC
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (PlayerController.Instance.ActiveTool != null && SandboxToolParameterMenu.instance != null)
		{
			this.RefreshDisplay();
		}
	}

	// Token: 0x06005C2A RID: 23594 RVA: 0x0021B0DC File Offset: 0x002192DC
	public void RefreshDisplay()
	{
		this.brushRadiusSlider.row.SetActive(PlayerController.Instance.ActiveTool is BrushTool);
		if (PlayerController.Instance.ActiveTool is BrushTool)
		{
			this.brushRadiusSlider.SetValue((float)this.settings.GetIntSetting("SandboxTools.BrushSize"), true);
		}
		this.massSlider.SetValue(this.settings.GetFloatSetting("SandboxTools.Mass"), true);
		this.stressAdditiveSlider.SetValue(this.settings.GetFloatSetting("SandbosTools.StressAdditive"), true);
		this.RefreshTemperatureUnitDisplays();
		this.temperatureSlider.SetValue(GameUtil.GetConvertedTemperature(this.settings.GetFloatSetting("SandbosTools.Temperature"), true), true);
		this.temperatureAdditiveSlider.SetValue(GameUtil.GetConvertedTemperature(this.settings.GetFloatSetting("SandbosTools.TemperatureAdditive"), true), true);
		this.diseaseCountSlider.SetValue((float)this.settings.GetIntSetting("SandboxTools.DiseaseCount"), true);
		this.moraleSlider.SetValue((float)this.settings.GetIntSetting("SandbosTools.MoraleAdjustment"), true);
	}

	// Token: 0x06005C2B RID: 23595 RVA: 0x0021B1F8 File Offset: 0x002193F8
	private void OnTemperatureUnitChanged(object unit)
	{
		int num = this.settings.GetIntSetting("SandboxTools.SelectedElement");
		if (num >= ElementLoader.elements.Count)
		{
			num = 0;
		}
		Element element = ElementLoader.elements[num];
		this.SetAbsoluteTemperatureSliderRange(element);
		this.temperatureAdditiveSlider.SetValue(5f, true);
	}

	// Token: 0x06005C2C RID: 23596 RVA: 0x0021B24C File Offset: 0x0021944C
	private void SetAbsoluteTemperatureSliderRange(Element element)
	{
		float num = Mathf.Max(element.lowTemp - 10f, 1f);
		float num2;
		if (element.IsGas)
		{
			num2 = Mathf.Min(new float[]
			{
				9999f,
				element.highTemp + 10f,
				element.defaultValues.temperature + 100f
			});
		}
		else
		{
			num2 = Mathf.Min(9999f, element.highTemp + 10f);
		}
		num = GameUtil.GetConvertedTemperature(num, true);
		num2 = GameUtil.GetConvertedTemperature(num2, true);
		this.temperatureSlider.SetRange(num, num2, false);
	}

	// Token: 0x06005C2D RID: 23597 RVA: 0x0021B2E8 File Offset: 0x002194E8
	private void RefreshTemperatureUnitDisplays()
	{
		this.temperatureSlider.unitString = GameUtil.GetTemperatureUnitSuffix();
		this.temperatureSlider.row.GetComponent<HierarchyReferences>().GetReference<LocText>("UnitLabel").text = this.temperatureSlider.unitString;
		this.temperatureAdditiveSlider.unitString = GameUtil.GetTemperatureUnitSuffix();
		this.temperatureAdditiveSlider.row.GetComponent<HierarchyReferences>().GetReference<LocText>("UnitLabel").text = this.temperatureSlider.unitString;
	}

	// Token: 0x06005C2E RID: 23598 RVA: 0x0021B36C File Offset: 0x0021956C
	private GameObject SpawnSelector(SandboxToolParameterMenu.SelectorValue selector)
	{
		GameObject gameObject = Util.KInstantiateUI(this.selectorPropertyPrefab, base.gameObject, true);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		GameObject panel = component.GetReference("ScrollPanel").gameObject;
		GameObject gameObject2 = component.GetReference("Content").gameObject;
		InputField filterInputField = component.GetReference<InputField>("Filter");
		component.GetReference<LocText>("Label").SetText(selector.labelText);
		Game.Instance.Subscribe(1174281782, delegate(object data)
		{
			if (panel.activeSelf)
			{
				panel.SetActive(false);
			}
		});
		KButton reference = component.GetReference<KButton>("Button");
		reference.onClick += delegate
		{
			panel.SetActive(!panel.activeSelf);
			if (panel.activeSelf)
			{
				panel.GetComponent<KScrollRect>().verticalNormalizedPosition = 1f;
				filterInputField.ActivateInputField();
				filterInputField.onValueChanged.Invoke(filterInputField.text);
			}
		};
		GameObject gameObject3 = component.GetReference("optionPrefab").gameObject;
		selector.row = gameObject;
		selector.optionButtons = new List<KeyValuePair<object, GameObject>>();
		GameObject clearFilterButton = Util.KInstantiateUI(gameObject3, gameObject2, false);
		clearFilterButton.GetComponentInChildren<LocText>().text = UI.SANDBOXTOOLS.FILTERS.BACK;
		clearFilterButton.GetComponentsInChildren<Image>()[1].enabled = false;
		clearFilterButton.GetComponent<KButton>().onClick += delegate
		{
			selector.currentFilter = null;
			selector.optionButtons.ForEach(delegate(KeyValuePair<object, GameObject> test)
			{
				if (test.Key is SandboxToolParameterMenu.SelectorValue.SearchFilter)
				{
					test.Value.SetActive((test.Key as SandboxToolParameterMenu.SelectorValue.SearchFilter).parentFilter == null);
					return;
				}
				test.Value.SetActive(false);
			});
			clearFilterButton.SetActive(false);
			panel.GetComponent<KScrollRect>().verticalNormalizedPosition = 1f;
			filterInputField.text = "";
			filterInputField.onValueChanged.Invoke(filterInputField.text);
		};
		if (selector.filters != null)
		{
			SandboxToolParameterMenu.SelectorValue.SearchFilter[] filters = selector.filters;
			for (int i = 0; i < filters.Length; i++)
			{
				SandboxToolParameterMenu.SelectorValue.SearchFilter filter = filters[i];
				GameObject gameObject4 = Util.KInstantiateUI(gameObject3, gameObject2, false);
				gameObject4.SetActive(filter.parentFilter == null);
				gameObject4.GetComponentInChildren<LocText>().text = filter.Name;
				if (filter.icon != null)
				{
					gameObject4.GetComponentsInChildren<Image>()[1].sprite = filter.icon.first;
					gameObject4.GetComponentsInChildren<Image>()[1].color = filter.icon.second;
				}
				Action<KeyValuePair<object, GameObject>> <>9__6;
				gameObject4.GetComponent<KButton>().onClick += delegate
				{
					selector.currentFilter = filter;
					clearFilterButton.SetActive(true);
					List<KeyValuePair<object, GameObject>> optionButtons = selector.optionButtons;
					Action<KeyValuePair<object, GameObject>> action;
					if ((action = <>9__6) == null)
					{
						action = (<>9__6 = delegate(KeyValuePair<object, GameObject> test)
						{
							if (!(test.Key is SandboxToolParameterMenu.SelectorValue.SearchFilter))
							{
								test.Value.SetActive(selector.runCurrentFilter(test.Key));
								return;
							}
							if ((test.Key as SandboxToolParameterMenu.SelectorValue.SearchFilter).parentFilter == null)
							{
								test.Value.SetActive(false);
								return;
							}
							test.Value.SetActive((test.Key as SandboxToolParameterMenu.SelectorValue.SearchFilter).parentFilter == filter);
						});
					}
					optionButtons.ForEach(action);
					panel.GetComponent<KScrollRect>().verticalNormalizedPosition = 1f;
				};
				selector.optionButtons.Add(new KeyValuePair<object, GameObject>(filter, gameObject4));
			}
		}
		object[] options = selector.options;
		for (int i = 0; i < options.Length; i++)
		{
			object option = options[i];
			GameObject gameObject5 = Util.KInstantiateUI(gameObject3, gameObject2, true);
			gameObject5.GetComponentInChildren<LocText>().text = selector.getOptionName(option);
			gameObject5.GetComponent<KButton>().onClick += delegate
			{
				selector.onValueChanged(option);
				panel.SetActive(false);
			};
			global::Tuple<Sprite, Color> tuple = selector.getOptionSprite(option);
			gameObject5.GetComponentsInChildren<Image>()[1].sprite = tuple.first;
			gameObject5.GetComponentsInChildren<Image>()[1].color = tuple.second;
			selector.optionButtons.Add(new KeyValuePair<object, GameObject>(option, gameObject5));
			if (option is SandboxToolParameterMenu.SelectorValue.SearchFilter)
			{
				gameObject5.SetActive((option as SandboxToolParameterMenu.SelectorValue.SearchFilter).parentFilter == null);
			}
			else
			{
				gameObject5.SetActive(false);
			}
		}
		selector.button = reference;
		filterInputField.onValueChanged.AddListener(delegate(string filterString)
		{
			if (!clearFilterButton.activeSelf && !string.IsNullOrEmpty(filterString))
			{
				clearFilterButton.SetActive(true);
			}
			new List<KeyValuePair<object, GameObject>>();
			bool flag = selector.optionButtons.Find((KeyValuePair<object, GameObject> match) => match.Key is SandboxToolParameterMenu.SelectorValue.SearchFilter).Key != null;
			if (string.IsNullOrEmpty(filterString))
			{
				if (!flag)
				{
					selector.optionButtons.ForEach(delegate(KeyValuePair<object, GameObject> test)
					{
						test.Value.SetActive(true);
					});
				}
				else
				{
					selector.optionButtons.ForEach(delegate(KeyValuePair<object, GameObject> test)
					{
						if (test.Key is SandboxToolParameterMenu.SelectorValue.SearchFilter && ((SandboxToolParameterMenu.SelectorValue.SearchFilter)test.Key).parentFilter == null)
						{
							test.Value.SetActive(true);
							return;
						}
						test.Value.SetActive(false);
					});
				}
			}
			else
			{
				selector.optionButtons.ForEach(delegate(KeyValuePair<object, GameObject> test)
				{
					if (test.Key is SandboxToolParameterMenu.SelectorValue.SearchFilter)
					{
						test.Value.SetActive(((SandboxToolParameterMenu.SelectorValue.SearchFilter)test.Key).Name.ToUpper().Contains(filterString.ToUpper()));
						return;
					}
					test.Value.SetActive(selector.getOptionName(test.Key).ToUpper().Contains(filterString.ToUpper()));
				});
			}
			if (selector.filterOptionFunction != null)
			{
				object[] options2 = selector.options;
				for (int j = 0; j < options2.Length; j++)
				{
					object option = options2[j];
					foreach (KeyValuePair<object, GameObject> keyValuePair in selector.optionButtons.FindAll((KeyValuePair<object, GameObject> match) => match.Key == option))
					{
						if (string.IsNullOrEmpty(filterString))
						{
							keyValuePair.Value.SetActive(false);
						}
						else
						{
							keyValuePair.Value.SetActive(selector.filterOptionFunction(filterString, option));
						}
					}
				}
			}
			panel.GetComponent<KScrollRect>().verticalNormalizedPosition = 1f;
		});
		this.inputFields.Add(filterInputField.gameObject);
		panel.SetActive(false);
		return gameObject;
	}

	// Token: 0x06005C2F RID: 23599 RVA: 0x0021B744 File Offset: 0x00219944
	private GameObject SpawnSlider(SandboxToolParameterMenu.SliderValue value)
	{
		GameObject gameObject = Util.KInstantiateUI(this.sliderPropertyPrefab, base.gameObject, true);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("BottomIcon").sprite = Assets.GetSprite(value.bottomSprite);
		component.GetReference<Image>("TopIcon").sprite = Assets.GetSprite(value.topSprite);
		component.GetReference<LocText>("Label").SetText(value.labelText);
		KSlider slider = component.GetReference<KSlider>("Slider");
		KNumberInputField inputField = component.GetReference<KNumberInputField>("InputField");
		gameObject.GetComponent<ToolTip>().SetSimpleTooltip(value.tooltip);
		slider.minValue = value.slideMinValue;
		slider.maxValue = value.slideMaxValue;
		inputField.minValue = value.clampValueLow;
		inputField.maxValue = value.clampValueHigh;
		this.inputFields.Add(inputField.gameObject);
		value.slider = slider;
		inputField.decimalPlaces = value.roundToDecimalPlaces;
		value.inputField = inputField;
		value.row = gameObject;
		slider.onReleaseHandle += delegate
		{
			float num = Mathf.Round(slider.value * Mathf.Pow(10f, (float)value.roundToDecimalPlaces)) / Mathf.Pow(10f, (float)value.roundToDecimalPlaces);
			slider.value = num;
			inputField.currentValue = Mathf.Round(slider.value * Mathf.Pow(10f, (float)value.roundToDecimalPlaces)) / Mathf.Pow(10f, (float)value.roundToDecimalPlaces);
			inputField.SetDisplayValue(inputField.currentValue.ToString());
			if (value.onValueChanged != null)
			{
				value.onValueChanged(slider.value);
			}
		};
		slider.onDrag += delegate
		{
			float num2 = Mathf.Round(slider.value * Mathf.Pow(10f, (float)value.roundToDecimalPlaces)) / Mathf.Pow(10f, (float)value.roundToDecimalPlaces);
			slider.value = num2;
			inputField.currentValue = num2;
			inputField.SetDisplayValue(inputField.currentValue.ToString());
			if (value.onValueChanged != null)
			{
				value.onValueChanged(slider.value);
			}
		};
		slider.onMove += delegate
		{
			float num3 = Mathf.Round(slider.value * Mathf.Pow(10f, (float)value.roundToDecimalPlaces)) / Mathf.Pow(10f, (float)value.roundToDecimalPlaces);
			slider.value = num3;
			inputField.currentValue = num3;
			inputField.SetDisplayValue(inputField.currentValue.ToString());
			if (value.onValueChanged != null)
			{
				value.onValueChanged(slider.value);
			}
		};
		inputField.onEndEdit += delegate
		{
			float num4 = inputField.currentValue;
			num4 = Mathf.Round(num4 * Mathf.Pow(10f, (float)value.roundToDecimalPlaces)) / Mathf.Pow(10f, (float)value.roundToDecimalPlaces);
			inputField.SetDisplayValue(num4.ToString());
			slider.value = num4;
			if (value.onValueChanged != null)
			{
				value.onValueChanged(num4);
			}
		};
		component.GetReference<LocText>("UnitLabel").text = value.unitString;
		return gameObject;
	}

	// Token: 0x06005C30 RID: 23600 RVA: 0x0021B93F File Offset: 0x00219B3F
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.CheckBlockedInput())
		{
			if (!e.Consumed)
			{
				e.Consumed = true;
				return;
			}
		}
		else
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06005C31 RID: 23601 RVA: 0x0021B960 File Offset: 0x00219B60
	private bool CheckBlockedInput()
	{
		bool flag = false;
		if (UnityEngine.EventSystems.EventSystem.current != null)
		{
			GameObject currentSelectedGameObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
			if (currentSelectedGameObject != null)
			{
				foreach (GameObject gameObject in this.inputFields)
				{
					if (currentSelectedGameObject == gameObject.gameObject)
					{
						flag = true;
						break;
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x04003EF3 RID: 16115
	public static SandboxToolParameterMenu instance;

	// Token: 0x04003EF4 RID: 16116
	public SandboxSettings settings;

	// Token: 0x04003EF5 RID: 16117
	[SerializeField]
	private GameObject sliderPropertyPrefab;

	// Token: 0x04003EF6 RID: 16118
	[SerializeField]
	private GameObject selectorPropertyPrefab;

	// Token: 0x04003EF7 RID: 16119
	private List<GameObject> inputFields = new List<GameObject>();

	// Token: 0x04003EF8 RID: 16120
	private Dictionary<Tag, List<KPrefabID>> items;

	// Token: 0x04003EF9 RID: 16121
	public SandboxToolParameterMenu.SelectorValue elementSelector;

	// Token: 0x04003EFA RID: 16122
	public SandboxToolParameterMenu.SliderValue brushRadiusSlider = new SandboxToolParameterMenu.SliderValue(1f, 10f, "dash", "circle_hard", "", UI.SANDBOXTOOLS.SETTINGS.BRUSH_SIZE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.BRUSH_SIZE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetIntSetting("SandboxTools.BrushSize", Mathf.Clamp(Mathf.RoundToInt(value), 1, 50));
	}, 0);

	// Token: 0x04003EFB RID: 16123
	public SandboxToolParameterMenu.SliderValue noiseScaleSlider = new SandboxToolParameterMenu.SliderValue(0f, 1f, "little", "lots", "", UI.SANDBOXTOOLS.SETTINGS.BRUSH_NOISE_SCALE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.BRUSH_NOISE_SCALE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandboxTools.NoiseScale", value);
	}, 2);

	// Token: 0x04003EFC RID: 16124
	public SandboxToolParameterMenu.SliderValue noiseDensitySlider = new SandboxToolParameterMenu.SliderValue(1f, 20f, "little", "lots", "", UI.SANDBOXTOOLS.SETTINGS.BRUSH_NOISE_SCALE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.BRUSH_NOISE_DENSITY.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandboxTools.NoiseDensity", value);
	}, 2);

	// Token: 0x04003EFD RID: 16125
	public SandboxToolParameterMenu.SliderValue massSlider = new SandboxToolParameterMenu.SliderValue(0.1f, 1000f, "action_pacify", "status_item_plant_solid", UI.UNITSUFFIXES.MASS.KILOGRAM, UI.SANDBOXTOOLS.SETTINGS.MASS.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.MASS.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandboxTools.Mass", Mathf.Clamp(value, 0.001f, 9999f));
	}, 2);

	// Token: 0x04003EFE RID: 16126
	public SandboxToolParameterMenu.SliderValue temperatureSlider = new SandboxToolParameterMenu.SliderValue(150f, 500f, "cold", "hot", GameUtil.GetTemperatureUnitSuffix(), UI.SANDBOXTOOLS.SETTINGS.TEMPERATURE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.TEMPERATURE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandbosTools.Temperature", Mathf.Clamp(GameUtil.GetTemperatureConvertedToKelvin(value), 1f, 9999f));
	}, 0);

	// Token: 0x04003EFF RID: 16127
	public SandboxToolParameterMenu.SliderValue temperatureAdditiveSlider = new SandboxToolParameterMenu.SliderValue(-15f, 15f, "cold", "hot", GameUtil.GetTemperatureUnitSuffix(), UI.SANDBOXTOOLS.SETTINGS.TEMPERATURE_ADDITIVE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.TEMPERATURE_ADDITIVE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandbosTools.TemperatureAdditive", GameUtil.GetTemperatureConvertedToKelvin(value));
	}, 0);

	// Token: 0x04003F00 RID: 16128
	public SandboxToolParameterMenu.SliderValue stressAdditiveSlider = new SandboxToolParameterMenu.SliderValue(-10f, 10f, "little", "lots", UI.UNITSUFFIXES.PERCENT, UI.SANDBOXTOOLS.SETTINGS.STRESS_ADDITIVE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.STRESS_ADDITIVE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandbosTools.StressAdditive", value);
	}, 0);

	// Token: 0x04003F01 RID: 16129
	public SandboxToolParameterMenu.SliderValue moraleSlider = new SandboxToolParameterMenu.SliderValue(-25f, 25f, "little", "lots", UI.UNITSUFFIXES.UNITS, UI.SANDBOXTOOLS.SETTINGS.MORALE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.MORALE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetIntSetting("SandbosTools.MoraleAdjustment", Mathf.RoundToInt(value));
	}, 0);

	// Token: 0x04003F02 RID: 16130
	public SandboxToolParameterMenu.SelectorValue diseaseSelector;

	// Token: 0x04003F03 RID: 16131
	public SandboxToolParameterMenu.SliderValue diseaseCountSlider = new SandboxToolParameterMenu.SliderValue(0f, 10000f, "status_item_barren", "germ", UI.UNITSUFFIXES.DISEASE.UNITS, UI.SANDBOXTOOLS.SETTINGS.DISEASE_COUNT.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.DISEASE_COUNT.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetIntSetting("SandboxTools.DiseaseCount", Mathf.RoundToInt(value));
	}, 0);

	// Token: 0x04003F04 RID: 16132
	public SandboxToolParameterMenu.SelectorValue entitySelector;

	// Token: 0x02001A32 RID: 6706
	public class SelectorValue
	{
		// Token: 0x0600925D RID: 37469 RVA: 0x0031712C File Offset: 0x0031532C
		public SelectorValue(object[] options, Action<object> onValueChanged, Func<object, string> getOptionName, Func<string, object, bool> filterOptionFunction, Func<object, global::Tuple<Sprite, Color>> getOptionSprite, string labelText, SandboxToolParameterMenu.SelectorValue.SearchFilter[] filters = null)
		{
			this.options = options;
			this.onValueChanged = onValueChanged;
			this.getOptionName = getOptionName;
			this.filterOptionFunction = filterOptionFunction;
			this.getOptionSprite = getOptionSprite;
			this.filters = filters;
			this.labelText = labelText;
		}

		// Token: 0x0600925E RID: 37470 RVA: 0x0031717F File Offset: 0x0031537F
		public bool runCurrentFilter(object obj)
		{
			return this.currentFilter == null || this.currentFilter.condition(obj);
		}

		// Token: 0x040076B9 RID: 30393
		public GameObject row;

		// Token: 0x040076BA RID: 30394
		public List<KeyValuePair<object, GameObject>> optionButtons;

		// Token: 0x040076BB RID: 30395
		public KButton button;

		// Token: 0x040076BC RID: 30396
		public object[] options;

		// Token: 0x040076BD RID: 30397
		public Action<object> onValueChanged;

		// Token: 0x040076BE RID: 30398
		public Func<object, string> getOptionName;

		// Token: 0x040076BF RID: 30399
		public Func<string, object, bool> filterOptionFunction;

		// Token: 0x040076C0 RID: 30400
		public Func<object, global::Tuple<Sprite, Color>> getOptionSprite;

		// Token: 0x040076C1 RID: 30401
		public SandboxToolParameterMenu.SelectorValue.SearchFilter[] filters;

		// Token: 0x040076C2 RID: 30402
		public List<SandboxToolParameterMenu.SelectorValue.SearchFilter> activeFilters = new List<SandboxToolParameterMenu.SelectorValue.SearchFilter>();

		// Token: 0x040076C3 RID: 30403
		public SandboxToolParameterMenu.SelectorValue.SearchFilter currentFilter;

		// Token: 0x040076C4 RID: 30404
		public string labelText;

		// Token: 0x02002107 RID: 8455
		public class SearchFilter
		{
			// Token: 0x0600A5DC RID: 42460 RVA: 0x0034B04A File Offset: 0x0034924A
			public SearchFilter(string Name, Func<object, bool> condition, SandboxToolParameterMenu.SelectorValue.SearchFilter parentFilter = null, global::Tuple<Sprite, Color> icon = null)
			{
				this.Name = Name;
				this.condition = condition;
				this.parentFilter = parentFilter;
				this.icon = icon;
			}

			// Token: 0x040092E2 RID: 37602
			public string Name;

			// Token: 0x040092E3 RID: 37603
			public Func<object, bool> condition;

			// Token: 0x040092E4 RID: 37604
			public SandboxToolParameterMenu.SelectorValue.SearchFilter parentFilter;

			// Token: 0x040092E5 RID: 37605
			public global::Tuple<Sprite, Color> icon;
		}
	}

	// Token: 0x02001A33 RID: 6707
	public class SliderValue
	{
		// Token: 0x0600925F RID: 37471 RVA: 0x003171A4 File Offset: 0x003153A4
		public SliderValue(float slideMinValue, float slideMaxValue, string bottomSprite, string topSprite, string unitString, string tooltip, string labelText, Action<float> onValueChanged, int decimalPlaces = 0)
		{
			this.slideMinValue = slideMinValue;
			this.slideMaxValue = slideMaxValue;
			this.bottomSprite = bottomSprite;
			this.topSprite = topSprite;
			this.unitString = unitString;
			this.onValueChanged = onValueChanged;
			this.tooltip = tooltip;
			this.roundToDecimalPlaces = decimalPlaces;
			this.labelText = labelText;
			this.clampValueLow = slideMinValue;
			this.clampValueHigh = slideMaxValue;
		}

		// Token: 0x06009260 RID: 37472 RVA: 0x0031720C File Offset: 0x0031540C
		public void SetRange(float min, float max, bool resetCurrentValue = true)
		{
			this.slideMinValue = min;
			this.slideMaxValue = max;
			this.slider.minValue = this.slideMinValue;
			this.slider.maxValue = this.slideMaxValue;
			this.inputField.currentValue = this.slideMinValue + (this.slideMaxValue - this.slideMinValue) / 2f;
			this.inputField.SetDisplayValue(this.inputField.currentValue.ToString());
			if (resetCurrentValue)
			{
				this.slider.value = this.slideMinValue + (this.slideMaxValue - this.slideMinValue) / 2f;
				this.onValueChanged(this.slideMinValue + (this.slideMaxValue - this.slideMinValue) / 2f);
			}
		}

		// Token: 0x06009261 RID: 37473 RVA: 0x003172D8 File Offset: 0x003154D8
		public void SetValue(float value, bool runOnValueChanged = true)
		{
			value = Mathf.Clamp(value, this.clampValueLow, this.clampValueHigh);
			this.slider.value = value;
			this.inputField.currentValue = value;
			if (runOnValueChanged)
			{
				this.onValueChanged(value);
			}
			this.RefreshDisplay();
		}

		// Token: 0x06009262 RID: 37474 RVA: 0x00317328 File Offset: 0x00315528
		public void RefreshDisplay()
		{
			this.inputField.SetDisplayValue(((this.roundToDecimalPlaces == 0) ? ((float)Mathf.RoundToInt(this.inputField.currentValue)) : this.inputField.currentValue).ToString());
		}

		// Token: 0x040076C5 RID: 30405
		public GameObject row;

		// Token: 0x040076C6 RID: 30406
		public string bottomSprite;

		// Token: 0x040076C7 RID: 30407
		public string topSprite;

		// Token: 0x040076C8 RID: 30408
		public float slideMinValue;

		// Token: 0x040076C9 RID: 30409
		public float slideMaxValue;

		// Token: 0x040076CA RID: 30410
		public float clampValueLow;

		// Token: 0x040076CB RID: 30411
		public float clampValueHigh;

		// Token: 0x040076CC RID: 30412
		public string unitString;

		// Token: 0x040076CD RID: 30413
		public Action<float> onValueChanged;

		// Token: 0x040076CE RID: 30414
		public string tooltip;

		// Token: 0x040076CF RID: 30415
		public int roundToDecimalPlaces;

		// Token: 0x040076D0 RID: 30416
		public string labelText;

		// Token: 0x040076D1 RID: 30417
		public KSlider slider;

		// Token: 0x040076D2 RID: 30418
		public KNumberInputField inputField;
	}
}
