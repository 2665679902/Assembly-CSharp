using System;
using System.Collections.Generic;
using System.IO;
using Klei.AI;
using ProcGen;
using UnityEngine;

// Token: 0x0200072E RID: 1838
public class EconomyDetails
{
	// Token: 0x0600325C RID: 12892 RVA: 0x0010D708 File Offset: 0x0010B908
	public EconomyDetails()
	{
		this.massResourceType = new EconomyDetails.Resource.Type("Mass", "kg");
		this.heatResourceType = new EconomyDetails.Resource.Type("Heat Energy", "kdtu");
		this.energyResourceType = new EconomyDetails.Resource.Type("Energy", "joules");
		this.timeResourceType = new EconomyDetails.Resource.Type("Time", "seconds");
		this.attributeResourceType = new EconomyDetails.Resource.Type("Attribute", "units");
		this.caloriesResourceType = new EconomyDetails.Resource.Type("Calories", "kcal");
		this.amountResourceType = new EconomyDetails.Resource.Type("Amount", "units");
		this.buildingTransformationType = new EconomyDetails.Transformation.Type("Building");
		this.foodTransformationType = new EconomyDetails.Transformation.Type("Food");
		this.plantTransformationType = new EconomyDetails.Transformation.Type("Plant");
		this.creatureTransformationType = new EconomyDetails.Transformation.Type("Creature");
		this.dupeTransformationType = new EconomyDetails.Transformation.Type("Duplicant");
		this.referenceTransformationType = new EconomyDetails.Transformation.Type("Reference");
		this.effectTransformationType = new EconomyDetails.Transformation.Type("Effect");
		this.geyserActivePeriodTransformationType = new EconomyDetails.Transformation.Type("GeyserActivePeriod");
		this.geyserLifetimeTransformationType = new EconomyDetails.Transformation.Type("GeyserLifetime");
		this.energyResource = this.CreateResource(TagManager.Create("Energy"), this.energyResourceType);
		this.heatResource = this.CreateResource(TagManager.Create("Heat"), this.heatResourceType);
		this.duplicantTimeResource = this.CreateResource(TagManager.Create("DupeTime"), this.timeResourceType);
		this.caloriesResource = this.CreateResource(new Tag(Db.Get().Amounts.Calories.deltaAttribute.Id), this.caloriesResourceType);
		this.fixedCaloriesResource = this.CreateResource(new Tag(Db.Get().Amounts.Calories.Id), this.caloriesResourceType);
		foreach (Element element in ElementLoader.elements)
		{
			this.CreateResource(element);
		}
		foreach (Tag tag in new List<Tag>
		{
			GameTags.CombustibleLiquid,
			GameTags.CombustibleGas,
			GameTags.CombustibleSolid
		})
		{
			this.CreateResource(tag, this.massResourceType);
		}
		foreach (EdiblesManager.FoodInfo foodInfo in EdiblesManager.GetAllFoodTypes())
		{
			this.CreateResource(foodInfo.Id.ToTag(), this.amountResourceType);
		}
		this.GatherStartingBiomeAmounts();
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			this.CreateTransformation(kprefabID, kprefabID.PrefabTag);
			if (kprefabID.GetComponent<GeyserConfigurator>() != null)
			{
				KPrefabID kprefabID2 = kprefabID;
				Tag prefabTag = kprefabID.PrefabTag;
				this.CreateTransformation(kprefabID2, prefabTag.ToString() + "_ActiveOnly");
			}
		}
		foreach (Effect effect in Db.Get().effects.resources)
		{
			this.CreateTransformation(effect);
		}
		EconomyDetails.Transformation transformation = new EconomyDetails.Transformation(TagManager.Create("Duplicant"), this.dupeTransformationType, 1f, false);
		transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Oxygen), -0.1f));
		transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.CarbonDioxide), 0.1f * Assets.GetPrefab(MinionConfig.ID).GetComponent<OxygenBreather>().O2toCO2conversion));
		transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.duplicantTimeResource, 0.875f));
		transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.caloriesResource, -1.6666667f));
		transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(new Tag(Db.Get().Amounts.Bladder.deltaAttribute.Id), this.amountResourceType), 0.16666667f));
		this.transformations.Add(transformation);
		EconomyDetails.Transformation transformation2 = new EconomyDetails.Transformation(TagManager.Create("Electrolysis"), this.referenceTransformationType, 1f, false);
		transformation2.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Oxygen), 1.7777778f));
		transformation2.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Hydrogen), 0.22222222f));
		transformation2.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Water), -2f));
		this.transformations.Add(transformation2);
		EconomyDetails.Transformation transformation3 = new EconomyDetails.Transformation(TagManager.Create("MethaneCombustion"), this.referenceTransformationType, 1f, false);
		transformation3.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Methane), -1f));
		transformation3.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Oxygen), -4f));
		transformation3.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.CarbonDioxide), 2.75f));
		transformation3.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Water), 2.25f));
		this.transformations.Add(transformation3);
		EconomyDetails.Transformation transformation4 = new EconomyDetails.Transformation(TagManager.Create("CoalCombustion"), this.referenceTransformationType, 1f, false);
		transformation4.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Carbon), -1f));
		transformation4.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Oxygen), -2.6666667f));
		transformation4.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.CarbonDioxide), 3.6666667f));
		this.transformations.Add(transformation4);
	}

	// Token: 0x0600325D RID: 12893 RVA: 0x0010DD5C File Offset: 0x0010BF5C
	private static void WriteProduct(StreamWriter o, string a, string b)
	{
		o.Write(string.Concat(new string[] { "\"=PRODUCT(", a, ", ", b, ")\"" }));
	}

	// Token: 0x0600325E RID: 12894 RVA: 0x0010DD8F File Offset: 0x0010BF8F
	private static void WriteProduct(StreamWriter o, string a, string b, string c)
	{
		o.Write(string.Concat(new string[] { "\"=PRODUCT(", a, ", ", b, ", ", c, ")\"" }));
	}

	// Token: 0x0600325F RID: 12895 RVA: 0x0010DDD0 File Offset: 0x0010BFD0
	public void DumpTransformations(EconomyDetails.Scenario scenario, StreamWriter o)
	{
		List<EconomyDetails.Resource> used_resources = new List<EconomyDetails.Resource>();
		foreach (EconomyDetails.Transformation transformation in this.transformations)
		{
			if (scenario.IncludesTransformation(transformation))
			{
				foreach (EconomyDetails.Transformation.Delta delta in transformation.deltas)
				{
					if (!used_resources.Contains(delta.resource))
					{
						used_resources.Add(delta.resource);
					}
				}
			}
		}
		used_resources.Sort((EconomyDetails.Resource x, EconomyDetails.Resource y) => x.tag.Name.CompareTo(y.tag.Name));
		List<EconomyDetails.Ratio> list = new List<EconomyDetails.Ratio>();
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.Algae), this.GetResource(GameTags.Oxygen), false));
		list.Add(new EconomyDetails.Ratio(this.energyResource, this.GetResource(GameTags.Oxygen), false));
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.Oxygen), this.energyResource, false));
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.Water), this.GetResource(GameTags.Oxygen), false));
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.DirtyWater), this.caloriesResource, false));
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.Water), this.caloriesResource, false));
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.Fertilizer), this.caloriesResource, false));
		list.Add(new EconomyDetails.Ratio(this.energyResource, this.CreateResource(new Tag(Db.Get().Amounts.Stress.deltaAttribute.Id), this.amountResourceType), true));
		list.RemoveAll((EconomyDetails.Ratio x) => !used_resources.Contains(x.input) || !used_resources.Contains(x.output));
		o.Write("Id");
		o.Write(",Count");
		o.Write(",Type");
		o.Write(",Time(s)");
		int num = 4;
		foreach (EconomyDetails.Resource resource in used_resources)
		{
			o.Write(string.Concat(new string[]
			{
				", ",
				resource.tag.Name,
				"(",
				resource.type.unit,
				")"
			}));
			num++;
		}
		o.Write(",MassDelta");
		foreach (EconomyDetails.Ratio ratio in list)
		{
			o.Write(string.Concat(new string[]
			{
				", ",
				ratio.output.tag.Name,
				"(",
				ratio.output.type.unit,
				")/",
				ratio.input.tag.Name,
				"(",
				ratio.input.type.unit,
				")"
			}));
			num++;
		}
		string text = "B";
		o.Write("\n");
		int num2 = 1;
		this.transformations.Sort((EconomyDetails.Transformation x, EconomyDetails.Transformation y) => x.tag.Name.CompareTo(y.tag.Name));
		for (int i = 0; i < this.transformations.Count; i++)
		{
			EconomyDetails.Transformation transformation2 = this.transformations[i];
			if (scenario.IncludesTransformation(transformation2))
			{
				num2++;
			}
		}
		string text2 = "B" + (num2 + 4).ToString();
		int num3 = 1;
		for (int j = 0; j < this.transformations.Count; j++)
		{
			EconomyDetails.Transformation transformation3 = this.transformations[j];
			if (scenario.IncludesTransformation(transformation3))
			{
				if (transformation3.tag == new Tag(EconomyDetails.debugTag))
				{
					int num4 = 0 + 1;
				}
				num3++;
				o.Write("\"" + transformation3.tag.Name + "\"");
				o.Write("," + scenario.GetCount(transformation3.tag).ToString());
				o.Write(",\"" + transformation3.type.id + "\"");
				if (!transformation3.timeInvariant)
				{
					o.Write(",\"" + transformation3.timeInSeconds.ToString("0.00") + "\"");
				}
				else
				{
					o.Write(",\"invariant\"");
				}
				string text3 = text + num3.ToString();
				float num5 = 0f;
				bool flag = false;
				foreach (EconomyDetails.Resource resource2 in used_resources)
				{
					EconomyDetails.Transformation.Delta delta2 = null;
					foreach (EconomyDetails.Transformation.Delta delta3 in transformation3.deltas)
					{
						if (delta3.resource.tag == resource2.tag)
						{
							delta2 = delta3;
							break;
						}
					}
					o.Write(",");
					if (delta2 != null && delta2.amount != 0f)
					{
						if (delta2.resource.type == this.massResourceType)
						{
							flag = true;
							num5 += delta2.amount;
						}
						if (!transformation3.timeInvariant)
						{
							EconomyDetails.WriteProduct(o, text3, (delta2.amount / transformation3.timeInSeconds).ToString("0.00000"), text2);
						}
						else
						{
							EconomyDetails.WriteProduct(o, text3, delta2.amount.ToString("0.00000"));
						}
					}
				}
				o.Write(",");
				if (flag)
				{
					num5 /= transformation3.timeInSeconds;
					EconomyDetails.WriteProduct(o, text3, num5.ToString("0.00000"), text2);
				}
				foreach (EconomyDetails.Ratio ratio2 in list)
				{
					o.Write(", ");
					EconomyDetails.Transformation.Delta delta4 = transformation3.GetDelta(ratio2.input);
					EconomyDetails.Transformation.Delta delta5 = transformation3.GetDelta(ratio2.output);
					if (delta5 != null && delta4 != null && delta4.amount < 0f && (delta5.amount > 0f || ratio2.allowNegativeOutput))
					{
						o.Write(delta5.amount / Mathf.Abs(delta4.amount));
					}
				}
				o.Write("\n");
			}
		}
		int num6 = 4;
		for (int k = 0; k < num; k++)
		{
			if (k >= num6 && k < num6 + used_resources.Count)
			{
				string text4 = ((char)(65 + k % 26)).ToString();
				int num7 = Mathf.FloorToInt((float)k / 26f);
				if (num7 > 0)
				{
					text4 = ((char)(65 + num7 - 1)).ToString() + text4;
				}
				o.Write(string.Concat(new string[]
				{
					"\"=SUM(",
					text4,
					"2: ",
					text4,
					num2.ToString(),
					")\""
				}));
			}
			o.Write(",");
		}
		string text5 = "B" + (num2 + 5).ToString();
		o.Write("\n");
		o.Write("\nTiming:");
		o.Write("\nTimeInSeconds:," + scenario.timeInSeconds.ToString());
		o.Write("\nSecondsPerCycle:," + 600f.ToString());
		o.Write("\nCycles:,=" + text2 + "/" + text5);
	}

	// Token: 0x06003260 RID: 12896 RVA: 0x0010E6FC File Offset: 0x0010C8FC
	public EconomyDetails.Resource CreateResource(Tag tag, EconomyDetails.Resource.Type resource_type)
	{
		foreach (EconomyDetails.Resource resource in this.resources)
		{
			if (resource.tag == tag)
			{
				return resource;
			}
		}
		EconomyDetails.Resource resource2 = new EconomyDetails.Resource(tag, resource_type);
		this.resources.Add(resource2);
		return resource2;
	}

	// Token: 0x06003261 RID: 12897 RVA: 0x0010E774 File Offset: 0x0010C974
	public EconomyDetails.Resource CreateResource(Element element)
	{
		return this.CreateResource(element.tag, this.massResourceType);
	}

	// Token: 0x06003262 RID: 12898 RVA: 0x0010E788 File Offset: 0x0010C988
	public EconomyDetails.Transformation CreateTransformation(Effect effect)
	{
		EconomyDetails.Transformation transformation = new EconomyDetails.Transformation(new Tag(effect.Id), this.effectTransformationType, 1f, false);
		foreach (AttributeModifier attributeModifier in effect.SelfModifiers)
		{
			EconomyDetails.Resource resource = this.CreateResource(new Tag(attributeModifier.AttributeId), this.attributeResourceType);
			transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource, attributeModifier.Value));
		}
		this.transformations.Add(transformation);
		return transformation;
	}

	// Token: 0x06003263 RID: 12899 RVA: 0x0010E828 File Offset: 0x0010CA28
	public EconomyDetails.Transformation GetTransformation(Tag tag)
	{
		foreach (EconomyDetails.Transformation transformation in this.transformations)
		{
			if (transformation.tag == tag)
			{
				return transformation;
			}
		}
		return null;
	}

	// Token: 0x06003264 RID: 12900 RVA: 0x0010E88C File Offset: 0x0010CA8C
	public EconomyDetails.Transformation CreateTransformation(KPrefabID prefab_id, Tag tag)
	{
		if (tag == new Tag(EconomyDetails.debugTag))
		{
			int num = 0 + 1;
		}
		Building component = prefab_id.GetComponent<Building>();
		ElementConverter component2 = prefab_id.GetComponent<ElementConverter>();
		EnergyConsumer component3 = prefab_id.GetComponent<EnergyConsumer>();
		ElementConsumer component4 = prefab_id.GetComponent<ElementConsumer>();
		BuildingElementEmitter component5 = prefab_id.GetComponent<BuildingElementEmitter>();
		Generator component6 = prefab_id.GetComponent<Generator>();
		EnergyGenerator component7 = prefab_id.GetComponent<EnergyGenerator>();
		ManualGenerator component8 = prefab_id.GetComponent<ManualGenerator>();
		ManualDeliveryKG[] components = prefab_id.GetComponents<ManualDeliveryKG>();
		StateMachineController component9 = prefab_id.GetComponent<StateMachineController>();
		Edible component10 = prefab_id.GetComponent<Edible>();
		Crop component11 = prefab_id.GetComponent<Crop>();
		Uprootable component12 = prefab_id.GetComponent<Uprootable>();
		ComplexRecipe complexRecipe = ComplexRecipeManager.Get().recipes.Find((ComplexRecipe r) => r.FirstResult == prefab_id.PrefabTag);
		List<FertilizationMonitor.Def> list = null;
		List<IrrigationMonitor.Def> list2 = null;
		GeyserConfigurator component13 = prefab_id.GetComponent<GeyserConfigurator>();
		Toilet component14 = prefab_id.GetComponent<Toilet>();
		FlushToilet component15 = prefab_id.GetComponent<FlushToilet>();
		RelaxationPoint component16 = prefab_id.GetComponent<RelaxationPoint>();
		CreatureCalorieMonitor.Def def = prefab_id.gameObject.GetDef<CreatureCalorieMonitor.Def>();
		if (component9 != null)
		{
			list = component9.GetDefs<FertilizationMonitor.Def>();
			list2 = component9.GetDefs<IrrigationMonitor.Def>();
		}
		EconomyDetails.Transformation transformation = null;
		float num2 = 1f;
		if (component10 != null)
		{
			transformation = new EconomyDetails.Transformation(tag, this.foodTransformationType, num2, complexRecipe != null);
		}
		else if (component2 != null || component3 != null || component4 != null || component5 != null || component6 != null || component7 != null || component12 != null || component13 != null || component14 != null || component15 != null || component16 != null || def != null)
		{
			if (component12 != null || component11 != null)
			{
				if (component11 != null)
				{
					num2 = component11.cropVal.cropDuration;
				}
				transformation = new EconomyDetails.Transformation(tag, this.plantTransformationType, num2, false);
			}
			else if (def != null)
			{
				transformation = new EconomyDetails.Transformation(tag, this.creatureTransformationType, num2, false);
			}
			else if (component13 != null)
			{
				GeyserConfigurator.GeyserInstanceConfiguration geyserInstanceConfiguration = new GeyserConfigurator.GeyserInstanceConfiguration
				{
					typeId = component13.presetType,
					rateRoll = 0.5f,
					iterationLengthRoll = 0.5f,
					iterationPercentRoll = 0.5f,
					yearLengthRoll = 0.5f,
					yearPercentRoll = 0.5f
				};
				if (tag.Name.Contains("_ActiveOnly"))
				{
					float iterationLength = geyserInstanceConfiguration.GetIterationLength();
					transformation = new EconomyDetails.Transformation(tag, this.geyserActivePeriodTransformationType, iterationLength, false);
				}
				else
				{
					float yearLength = geyserInstanceConfiguration.GetYearLength();
					transformation = new EconomyDetails.Transformation(tag, this.geyserLifetimeTransformationType, yearLength, false);
				}
			}
			else
			{
				if (component14 != null || component15 != null)
				{
					num2 = 600f;
				}
				transformation = new EconomyDetails.Transformation(tag, this.buildingTransformationType, num2, false);
			}
		}
		if (transformation != null)
		{
			if (component2 != null && component2.consumedElements != null)
			{
				foreach (ElementConverter.ConsumedElement consumedElement in component2.consumedElements)
				{
					EconomyDetails.Resource resource = this.CreateResource(consumedElement.Tag, this.massResourceType);
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource, -consumedElement.MassConsumptionRate));
				}
				if (component2.outputElements != null)
				{
					foreach (ElementConverter.OutputElement outputElement in component2.outputElements)
					{
						Element element = ElementLoader.FindElementByHash(outputElement.elementHash);
						EconomyDetails.Resource resource2 = this.CreateResource(element.tag, this.massResourceType);
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource2, outputElement.massGenerationRate));
					}
				}
			}
			if (component4 != null && component7 == null && (component2 == null || prefab_id.GetComponent<AlgaeHabitat>() != null))
			{
				EconomyDetails.Resource resource3 = this.GetResource(ElementLoader.FindElementByHash(component4.elementToConsume).tag);
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource3, -component4.consumptionRate));
			}
			if (component3 != null)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.energyResource, -component3.WattsNeededWhenActive));
			}
			if (component5 != null)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(component5.element), component5.emitRate));
			}
			if (component6 != null)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.energyResource, component6.GetComponent<Building>().Def.GeneratorWattageRating));
			}
			if (component7 != null)
			{
				if (component7.formula.inputs != null)
				{
					foreach (EnergyGenerator.InputItem inputItem in component7.formula.inputs)
					{
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(inputItem.tag), -inputItem.consumptionRate));
					}
				}
				if (component7.formula.outputs != null)
				{
					foreach (EnergyGenerator.OutputItem outputItem in component7.formula.outputs)
					{
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(outputItem.element), outputItem.creationRate));
					}
				}
			}
			if (component)
			{
				BuildingDef def2 = component.Def;
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.heatResource, def2.SelfHeatKilowattsWhenActive + def2.ExhaustKilowattsWhenActive));
			}
			if (component8)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.duplicantTimeResource, -1f));
			}
			if (component10)
			{
				EdiblesManager.FoodInfo foodInfo = component10.FoodInfo;
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.fixedCaloriesResource, foodInfo.CaloriesPerUnit * 0.001f));
				ComplexRecipeManager.Get().recipes.Find((ComplexRecipe a) => a.FirstResult == tag);
			}
			if (component11 != null)
			{
				EconomyDetails.Resource resource4 = this.CreateResource(TagManager.Create(component11.cropVal.cropId), this.amountResourceType);
				float num3 = (float)component11.cropVal.numProduced;
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource4, num3));
				GameObject prefab = Assets.GetPrefab(new Tag(component11.cropVal.cropId));
				if (prefab != null)
				{
					Edible component17 = prefab.GetComponent<Edible>();
					if (component17 != null)
					{
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.caloriesResource, component17.FoodInfo.CaloriesPerUnit * num3 * 0.001f));
					}
				}
			}
			if (complexRecipe != null)
			{
				foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
				{
					this.CreateResource(recipeElement.material, this.amountResourceType);
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(recipeElement.material), -recipeElement.amount));
				}
				foreach (ComplexRecipe.RecipeElement recipeElement2 in complexRecipe.results)
				{
					this.CreateResource(recipeElement2.material, this.amountResourceType);
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(recipeElement2.material), recipeElement2.amount));
				}
			}
			if (components != null)
			{
				for (int j = 0; j < components.Length; j++)
				{
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.duplicantTimeResource, -0.1f * transformation.timeInSeconds));
				}
			}
			if (list != null && list.Count > 0)
			{
				foreach (FertilizationMonitor.Def def3 in list)
				{
					foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in def3.consumedElements)
					{
						EconomyDetails.Resource resource5 = this.CreateResource(consumeInfo.tag, this.massResourceType);
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource5, -consumeInfo.massConsumptionRate * transformation.timeInSeconds));
					}
				}
			}
			if (list2 != null && list2.Count > 0)
			{
				foreach (IrrigationMonitor.Def def4 in list2)
				{
					foreach (PlantElementAbsorber.ConsumeInfo consumeInfo2 in def4.consumedElements)
					{
						EconomyDetails.Resource resource6 = this.CreateResource(consumeInfo2.tag, this.massResourceType);
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource6, -consumeInfo2.massConsumptionRate * transformation.timeInSeconds));
					}
				}
			}
			if (component13 != null)
			{
				GeyserConfigurator.GeyserInstanceConfiguration geyserInstanceConfiguration2 = new GeyserConfigurator.GeyserInstanceConfiguration
				{
					typeId = component13.presetType,
					rateRoll = 0.5f,
					iterationLengthRoll = 0.5f,
					iterationPercentRoll = 0.5f,
					yearLengthRoll = 0.5f,
					yearPercentRoll = 0.5f
				};
				if (tag.Name.Contains("_ActiveOnly"))
				{
					float num4 = geyserInstanceConfiguration2.GetMassPerCycle() / 600f * geyserInstanceConfiguration2.GetIterationLength();
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(geyserInstanceConfiguration2.GetElement().CreateTag(), this.massResourceType), num4));
				}
				else
				{
					float num5 = geyserInstanceConfiguration2.GetMassPerCycle() / 600f * geyserInstanceConfiguration2.GetYearLength() * geyserInstanceConfiguration2.GetYearPercent();
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(geyserInstanceConfiguration2.GetElement().CreateTag(), this.massResourceType), num5));
				}
			}
			if (component14 != null)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(new Tag(Db.Get().Amounts.Bladder.deltaAttribute.Id), this.amountResourceType), -0.16666667f));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(SimHashes.Dirt), -component14.solidWastePerUse.mass));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(component14.solidWastePerUse.elementID), component14.solidWastePerUse.mass));
			}
			if (component15 != null)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(new Tag(Db.Get().Amounts.Bladder.deltaAttribute.Id), this.amountResourceType), -0.16666667f));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(SimHashes.Water), -component15.massConsumedPerUse));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(SimHashes.DirtyWater), component15.massEmittedPerUse));
			}
			if (component16 != null)
			{
				foreach (AttributeModifier attributeModifier in component16.CreateEffect().SelfModifiers)
				{
					EconomyDetails.Resource resource7 = this.CreateResource(new Tag(attributeModifier.AttributeId), this.attributeResourceType);
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource7, attributeModifier.Value));
				}
			}
			if (def != null)
			{
				this.CollectDietTransformations(prefab_id);
			}
			this.transformations.Add(transformation);
		}
		return transformation;
	}

	// Token: 0x06003265 RID: 12901 RVA: 0x0010F474 File Offset: 0x0010D674
	private void CollectDietTransformations(KPrefabID prefab_id)
	{
		Trait trait = Db.Get().traits.Get(prefab_id.GetComponent<Modifiers>().initialTraits[0]);
		CreatureCalorieMonitor.Def def = prefab_id.gameObject.GetDef<CreatureCalorieMonitor.Def>();
		WildnessMonitor.Def def2 = prefab_id.gameObject.GetDef<WildnessMonitor.Def>();
		List<AttributeModifier> list = new List<AttributeModifier>();
		list.AddRange(trait.SelfModifiers);
		list.AddRange(def2.tameEffect.SelfModifiers);
		float num = 0f;
		float num2 = 0f;
		foreach (AttributeModifier attributeModifier in list)
		{
			if (attributeModifier.AttributeId == Db.Get().Amounts.Calories.maxAttribute.Id)
			{
				num = attributeModifier.Value;
			}
			if (attributeModifier.AttributeId == Db.Get().Amounts.Calories.deltaAttribute.Id)
			{
				num2 = attributeModifier.Value;
			}
		}
		foreach (Diet.Info info in def.diet.infos)
		{
			foreach (Tag tag in info.consumedTags)
			{
				float num3 = Mathf.Abs(num / num2);
				float num4 = num / info.caloriesPerKg;
				float num5 = num4 * info.producedConversionRate;
				EconomyDetails.Transformation transformation = new EconomyDetails.Transformation(new Tag(prefab_id.PrefabTag.Name + "Diet" + tag.Name), this.creatureTransformationType, num3, false);
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(tag, this.massResourceType), -num4));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(new Tag(info.producedElement.ToString()), this.massResourceType), num5));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.caloriesResource, num));
				this.transformations.Add(transformation);
			}
		}
	}

	// Token: 0x06003266 RID: 12902 RVA: 0x0010F6BC File Offset: 0x0010D8BC
	private static void CollectDietScenarios(List<EconomyDetails.Scenario> scenarios)
	{
		EconomyDetails.Scenario scenario = new EconomyDetails.Scenario("diets/all", 0f, null);
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			CreatureCalorieMonitor.Def def = kprefabID.gameObject.GetDef<CreatureCalorieMonitor.Def>();
			if (def != null)
			{
				EconomyDetails.Scenario scenario2 = new EconomyDetails.Scenario("diets/" + kprefabID.name, 0f, null);
				Diet.Info[] infos = def.diet.infos;
				for (int i = 0; i < infos.Length; i++)
				{
					foreach (Tag tag in infos[i].consumedTags)
					{
						Tag tag2 = kprefabID.PrefabTag.Name + "Diet" + tag.Name;
						scenario2.AddEntry(new EconomyDetails.Scenario.Entry(tag2, 1f));
						scenario.AddEntry(new EconomyDetails.Scenario.Entry(tag2, 1f));
					}
				}
				scenarios.Add(scenario2);
			}
		}
		scenarios.Add(scenario);
	}

	// Token: 0x06003267 RID: 12903 RVA: 0x0010F80C File Offset: 0x0010DA0C
	public void GatherStartingBiomeAmounts()
	{
		for (int i = 0; i < Grid.CellCount; i++)
		{
			if (global::World.Instance.zoneRenderData.worldZoneTypes[i] == SubWorld.ZoneType.Sandstone)
			{
				Element element = Grid.Element[i];
				float num = 0f;
				this.startingBiomeAmounts.TryGetValue(element, out num);
				this.startingBiomeAmounts[element] = num + Grid.Mass[i];
				this.startingBiomeCellCount++;
			}
		}
	}

	// Token: 0x06003268 RID: 12904 RVA: 0x0010F881 File Offset: 0x0010DA81
	public EconomyDetails.Resource GetResource(SimHashes element)
	{
		return this.GetResource(ElementLoader.FindElementByHash(element).tag);
	}

	// Token: 0x06003269 RID: 12905 RVA: 0x0010F894 File Offset: 0x0010DA94
	public EconomyDetails.Resource GetResource(Tag tag)
	{
		foreach (EconomyDetails.Resource resource in this.resources)
		{
			if (resource.tag == tag)
			{
				return resource;
			}
		}
		DebugUtil.LogErrorArgs(new object[] { "Found a tag without a matching resource!", tag });
		return null;
	}

	// Token: 0x0600326A RID: 12906 RVA: 0x0010F914 File Offset: 0x0010DB14
	private float GetDupeBreathingPerSecond(EconomyDetails details)
	{
		return details.GetTransformation(TagManager.Create("Duplicant")).GetDelta(details.GetResource(GameTags.Oxygen)).amount;
	}

	// Token: 0x0600326B RID: 12907 RVA: 0x0010F93C File Offset: 0x0010DB3C
	private EconomyDetails.BiomeTransformation CreateBiomeTransformationFromTransformation(EconomyDetails details, Tag transformation_tag, Tag input_resource_tag, Tag output_resource_tag)
	{
		EconomyDetails.Resource resource = details.GetResource(input_resource_tag);
		EconomyDetails.Resource resource2 = details.GetResource(output_resource_tag);
		EconomyDetails.Transformation transformation = details.GetTransformation(transformation_tag);
		float num = transformation.GetDelta(resource2).amount / -transformation.GetDelta(resource).amount;
		float num2 = this.GetDupeBreathingPerSecond(details) * 600f;
		return new EconomyDetails.BiomeTransformation((transformation_tag.Name + input_resource_tag.Name + "Cycles").ToTag(), resource, num / -num2);
	}

	// Token: 0x0600326C RID: 12908 RVA: 0x0010F9B4 File Offset: 0x0010DBB4
	private static void DumpEconomyDetails()
	{
		global::Debug.Log("Starting Economy Details Dump...");
		EconomyDetails details = new EconomyDetails();
		List<EconomyDetails.Scenario> list = new List<EconomyDetails.Scenario>();
		EconomyDetails.Scenario scenario = new EconomyDetails.Scenario("default", 1f, (EconomyDetails.Transformation t) => true);
		list.Add(scenario);
		EconomyDetails.Scenario scenario2 = new EconomyDetails.Scenario("all_buildings", 1f, (EconomyDetails.Transformation t) => t.type == details.buildingTransformationType);
		list.Add(scenario2);
		EconomyDetails.Scenario scenario3 = new EconomyDetails.Scenario("all_plants", 1f, (EconomyDetails.Transformation t) => t.type == details.plantTransformationType);
		list.Add(scenario3);
		EconomyDetails.Scenario scenario4 = new EconomyDetails.Scenario("all_creatures", 1f, (EconomyDetails.Transformation t) => t.type == details.creatureTransformationType);
		list.Add(scenario4);
		EconomyDetails.Scenario scenario5 = new EconomyDetails.Scenario("all_stress", 1f, (EconomyDetails.Transformation t) => t.GetDelta(details.GetResource(new Tag(Db.Get().Amounts.Stress.deltaAttribute.Id))) != null);
		list.Add(scenario5);
		EconomyDetails.Scenario scenario6 = new EconomyDetails.Scenario("all_foods", 1f, (EconomyDetails.Transformation t) => t.type == details.foodTransformationType);
		list.Add(scenario6);
		EconomyDetails.Scenario scenario7 = new EconomyDetails.Scenario("geysers/geysers_active_period_only", 1f, (EconomyDetails.Transformation t) => t.type == details.geyserActivePeriodTransformationType);
		list.Add(scenario7);
		EconomyDetails.Scenario scenario8 = new EconomyDetails.Scenario("geysers/geysers_whole_lifetime", 1f, (EconomyDetails.Transformation t) => t.type == details.geyserLifetimeTransformationType);
		list.Add(scenario8);
		EconomyDetails.Scenario scenario9 = new EconomyDetails.Scenario("oxygen/algae_distillery", 0f, null);
		scenario9.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("AlgaeDistillery"), 3f));
		scenario9.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("AlgaeHabitat"), 22f));
		scenario9.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Duplicant"), 9f));
		scenario9.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("WaterPurifier"), 1f));
		list.Add(scenario9);
		EconomyDetails.Scenario scenario10 = new EconomyDetails.Scenario("oxygen/algae_habitat_electrolyzer", 0f, null);
		scenario10.AddEntry(new EconomyDetails.Scenario.Entry("AlgaeHabitat", 1f));
		scenario10.AddEntry(new EconomyDetails.Scenario.Entry("Duplicant", 1f));
		scenario10.AddEntry(new EconomyDetails.Scenario.Entry("Electrolyzer", 1f));
		list.Add(scenario10);
		EconomyDetails.Scenario scenario11 = new EconomyDetails.Scenario("oxygen/electrolyzer", 0f, null);
		scenario11.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Electrolyzer"), 1f));
		scenario11.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("LiquidPump"), 1f));
		scenario11.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Duplicant"), 9f));
		scenario11.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("HydrogenGenerator"), 1f));
		scenario11.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("GasPump"), 1f));
		list.Add(scenario11);
		EconomyDetails.Scenario scenario12 = new EconomyDetails.Scenario("purifiers/methane_generator", 0f, null);
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("MethaneGenerator"), 1f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("FertilizerMaker"), 3f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Electrolyzer"), 1f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("GasPump"), 1f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("LiquidPump"), 2f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("HydrogenGenerator"), 1f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("PrickleFlower"), 0f));
		list.Add(scenario12);
		EconomyDetails.Scenario scenario13 = new EconomyDetails.Scenario("purifiers/water_purifier", 0f, null);
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("WaterPurifier"), 1f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Compost"), 2f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Electrolyzer"), 1f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("LiquidPump"), 2f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("GasPump"), 1f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("HydrogenGenerator"), 1f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("PrickleFlower"), 29f));
		list.Add(scenario13);
		EconomyDetails.Scenario scenario14 = new EconomyDetails.Scenario("energy/petroleum_generator", 0f, null);
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("PetroleumGenerator"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("OilRefinery"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("WaterPurifier"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("LiquidPump"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("GasPump"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("CO2Scrubber"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("MethaneGenerator"), 1f));
		list.Add(scenario14);
		EconomyDetails.Scenario scenario15 = new EconomyDetails.Scenario("energy/coal_generator", 0f, (EconomyDetails.Transformation t) => t.tag.Name.Contains("Hatch"));
		scenario15.AddEntry(new EconomyDetails.Scenario.Entry("Generator", 1f));
		list.Add(scenario15);
		EconomyDetails.Scenario scenario16 = new EconomyDetails.Scenario("waste/outhouse", 0f, null);
		scenario16.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Outhouse"), 1f));
		scenario16.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Compost"), 1f));
		list.Add(scenario16);
		EconomyDetails.Scenario scenario17 = new EconomyDetails.Scenario("stress/massage_table", 0f, null);
		scenario17.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("MassageTable"), 1f));
		scenario17.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("ManualGenerator"), 1f));
		list.Add(scenario17);
		EconomyDetails.Scenario scenario18 = new EconomyDetails.Scenario("waste/flush_toilet", 0f, null);
		scenario18.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("FlushToilet"), 1f));
		scenario18.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("WaterPurifier"), 1f));
		scenario18.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("LiquidPump"), 1f));
		scenario18.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("FertilizerMaker"), 1f));
		list.Add(scenario18);
		EconomyDetails.CollectDietScenarios(list);
		foreach (EconomyDetails.Transformation transformation in details.transformations)
		{
			EconomyDetails.Transformation transformation_iter = transformation;
			EconomyDetails.Scenario scenario19 = new EconomyDetails.Scenario("transformations/" + transformation.tag.Name, 1f, (EconomyDetails.Transformation t) => transformation_iter == t);
			list.Add(scenario19);
		}
		foreach (EconomyDetails.Transformation transformation2 in details.transformations)
		{
			EconomyDetails.Scenario scenario20 = new EconomyDetails.Scenario("transformation_groups/" + transformation2.tag.Name, 0f, null);
			scenario20.AddEntry(new EconomyDetails.Scenario.Entry(transformation2.tag, 1f));
			foreach (EconomyDetails.Transformation transformation3 in details.transformations)
			{
				bool flag = false;
				foreach (EconomyDetails.Transformation.Delta delta in transformation2.deltas)
				{
					if (delta.resource.type != details.energyResourceType)
					{
						foreach (EconomyDetails.Transformation.Delta delta2 in transformation3.deltas)
						{
							if (delta.resource == delta2.resource)
							{
								scenario20.AddEntry(new EconomyDetails.Scenario.Entry(transformation3.tag, 0f));
								flag = true;
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			list.Add(scenario20);
		}
		foreach (EdiblesManager.FoodInfo foodInfo in EdiblesManager.GetAllFoodTypes())
		{
			EconomyDetails.Scenario scenario21 = new EconomyDetails.Scenario("food/" + foodInfo.Id, 0f, null);
			Tag tag2 = TagManager.Create(foodInfo.Id);
			scenario21.AddEntry(new EconomyDetails.Scenario.Entry(tag2, 1f));
			scenario21.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Duplicant"), 1f));
			List<Tag> list2 = new List<Tag>();
			list2.Add(tag2);
			while (list2.Count > 0)
			{
				Tag tag = list2[0];
				list2.RemoveAt(0);
				ComplexRecipe complexRecipe = ComplexRecipeManager.Get().recipes.Find((ComplexRecipe a) => a.FirstResult == tag);
				if (complexRecipe != null)
				{
					foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
					{
						scenario21.AddEntry(new EconomyDetails.Scenario.Entry(recipeElement.material, 1f));
						list2.Add(recipeElement.material);
					}
				}
				foreach (KPrefabID kprefabID in Assets.Prefabs)
				{
					Crop component = kprefabID.GetComponent<Crop>();
					if (component != null && component.cropVal.cropId == tag.Name)
					{
						scenario21.AddEntry(new EconomyDetails.Scenario.Entry(kprefabID.PrefabTag, 1f));
						list2.Add(kprefabID.PrefabTag);
					}
				}
			}
			list.Add(scenario21);
		}
		if (!Directory.Exists("assets/Tuning/Economy"))
		{
			Directory.CreateDirectory("assets/Tuning/Economy");
		}
		foreach (EconomyDetails.Scenario scenario22 in list)
		{
			string text = "assets/Tuning/Economy/" + scenario22.name + ".csv";
			if (!Directory.Exists(System.IO.Path.GetDirectoryName(text)))
			{
				Directory.CreateDirectory(System.IO.Path.GetDirectoryName(text));
			}
			using (StreamWriter streamWriter = new StreamWriter(text))
			{
				details.DumpTransformations(scenario22, streamWriter);
			}
		}
		float dupeBreathingPerSecond = details.GetDupeBreathingPerSecond(details);
		List<EconomyDetails.BiomeTransformation> list3 = new List<EconomyDetails.BiomeTransformation>();
		list3.Add(details.CreateBiomeTransformationFromTransformation(details, "MineralDeoxidizer".ToTag(), GameTags.Algae, GameTags.Oxygen));
		list3.Add(details.CreateBiomeTransformationFromTransformation(details, "AlgaeHabitat".ToTag(), GameTags.Algae, GameTags.Oxygen));
		list3.Add(details.CreateBiomeTransformationFromTransformation(details, "AlgaeHabitat".ToTag(), GameTags.Water, GameTags.Oxygen));
		list3.Add(details.CreateBiomeTransformationFromTransformation(details, "Electrolyzer".ToTag(), GameTags.Water, GameTags.Oxygen));
		list3.Add(new EconomyDetails.BiomeTransformation("StartingOxygenCycles".ToTag(), details.GetResource(GameTags.Oxygen), 1f / -(dupeBreathingPerSecond * 600f)));
		list3.Add(new EconomyDetails.BiomeTransformation("StartingOxyliteCycles".ToTag(), details.CreateResource(GameTags.OxyRock, details.massResourceType), 1f / -(dupeBreathingPerSecond * 600f)));
		string text2 = "assets/Tuning/Economy/biomes/starting_amounts.csv";
		if (!Directory.Exists(System.IO.Path.GetDirectoryName(text2)))
		{
			Directory.CreateDirectory(System.IO.Path.GetDirectoryName(text2));
		}
		using (StreamWriter streamWriter2 = new StreamWriter(text2))
		{
			streamWriter2.Write("Resource,Amount");
			foreach (EconomyDetails.BiomeTransformation biomeTransformation in list3)
			{
				streamWriter2.Write("," + biomeTransformation.tag.ToString());
			}
			streamWriter2.Write("\n");
			streamWriter2.Write("Cells, " + details.startingBiomeCellCount.ToString() + "\n");
			foreach (KeyValuePair<Element, float> keyValuePair in details.startingBiomeAmounts)
			{
				streamWriter2.Write(keyValuePair.Key.id.ToString() + ", " + keyValuePair.Value.ToString());
				foreach (EconomyDetails.BiomeTransformation biomeTransformation2 in list3)
				{
					streamWriter2.Write(",");
					float num = biomeTransformation2.Transform(keyValuePair.Key, keyValuePair.Value);
					if (num > 0f)
					{
						streamWriter2.Write(num);
					}
				}
				streamWriter2.Write("\n");
			}
		}
		global::Debug.Log("Completed economy details dump!!");
	}

	// Token: 0x0600326D RID: 12909 RVA: 0x0011093C File Offset: 0x0010EB3C
	private static void DumpNameMapping()
	{
		string text = "assets/Tuning/Economy/name_mapping.csv";
		if (!Directory.Exists("assets/Tuning/Economy"))
		{
			Directory.CreateDirectory("assets/Tuning/Economy");
		}
		using (StreamWriter streamWriter = new StreamWriter(text))
		{
			streamWriter.Write("Game Name, Prefab Name, Anim Files\n");
			foreach (KPrefabID kprefabID in Assets.Prefabs)
			{
				string text2 = TagManager.StripLinkFormatting(kprefabID.GetProperName());
				Tag tag = kprefabID.PrefabID();
				if (!text2.IsNullOrWhiteSpace() && !tag.Name.Contains("UnderConstruction") && !tag.Name.Contains("Preview"))
				{
					streamWriter.Write(text2);
					TextWriter textWriter = streamWriter;
					string text3 = ",";
					Tag tag2 = tag;
					textWriter.Write(text3 + tag2.ToString());
					KAnimControllerBase component = kprefabID.GetComponent<KAnimControllerBase>();
					if (component != null)
					{
						foreach (KAnimFile kanimFile in component.AnimFiles)
						{
							streamWriter.Write("," + kanimFile.name);
						}
					}
					else
					{
						streamWriter.Write(",");
					}
					streamWriter.Write("\n");
				}
			}
		}
	}

	// Token: 0x04001E98 RID: 7832
	private List<EconomyDetails.Transformation> transformations = new List<EconomyDetails.Transformation>();

	// Token: 0x04001E99 RID: 7833
	private List<EconomyDetails.Resource> resources = new List<EconomyDetails.Resource>();

	// Token: 0x04001E9A RID: 7834
	public Dictionary<Element, float> startingBiomeAmounts = new Dictionary<Element, float>();

	// Token: 0x04001E9B RID: 7835
	public int startingBiomeCellCount;

	// Token: 0x04001E9C RID: 7836
	public EconomyDetails.Resource energyResource;

	// Token: 0x04001E9D RID: 7837
	public EconomyDetails.Resource heatResource;

	// Token: 0x04001E9E RID: 7838
	public EconomyDetails.Resource duplicantTimeResource;

	// Token: 0x04001E9F RID: 7839
	public EconomyDetails.Resource caloriesResource;

	// Token: 0x04001EA0 RID: 7840
	public EconomyDetails.Resource fixedCaloriesResource;

	// Token: 0x04001EA1 RID: 7841
	public EconomyDetails.Resource.Type massResourceType;

	// Token: 0x04001EA2 RID: 7842
	public EconomyDetails.Resource.Type heatResourceType;

	// Token: 0x04001EA3 RID: 7843
	public EconomyDetails.Resource.Type energyResourceType;

	// Token: 0x04001EA4 RID: 7844
	public EconomyDetails.Resource.Type timeResourceType;

	// Token: 0x04001EA5 RID: 7845
	public EconomyDetails.Resource.Type attributeResourceType;

	// Token: 0x04001EA6 RID: 7846
	public EconomyDetails.Resource.Type caloriesResourceType;

	// Token: 0x04001EA7 RID: 7847
	public EconomyDetails.Resource.Type amountResourceType;

	// Token: 0x04001EA8 RID: 7848
	public EconomyDetails.Transformation.Type buildingTransformationType;

	// Token: 0x04001EA9 RID: 7849
	public EconomyDetails.Transformation.Type foodTransformationType;

	// Token: 0x04001EAA RID: 7850
	public EconomyDetails.Transformation.Type plantTransformationType;

	// Token: 0x04001EAB RID: 7851
	public EconomyDetails.Transformation.Type creatureTransformationType;

	// Token: 0x04001EAC RID: 7852
	public EconomyDetails.Transformation.Type dupeTransformationType;

	// Token: 0x04001EAD RID: 7853
	public EconomyDetails.Transformation.Type referenceTransformationType;

	// Token: 0x04001EAE RID: 7854
	public EconomyDetails.Transformation.Type effectTransformationType;

	// Token: 0x04001EAF RID: 7855
	private const string GEYSER_ACTIVE_SUFFIX = "_ActiveOnly";

	// Token: 0x04001EB0 RID: 7856
	public EconomyDetails.Transformation.Type geyserActivePeriodTransformationType;

	// Token: 0x04001EB1 RID: 7857
	public EconomyDetails.Transformation.Type geyserLifetimeTransformationType;

	// Token: 0x04001EB2 RID: 7858
	private static string debugTag = "CO2Scrubber";

	// Token: 0x02001431 RID: 5169
	public class Resource
	{
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06008051 RID: 32849 RVA: 0x002DEFD4 File Offset: 0x002DD1D4
		// (set) Token: 0x06008052 RID: 32850 RVA: 0x002DEFDC File Offset: 0x002DD1DC
		public Tag tag { get; private set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06008053 RID: 32851 RVA: 0x002DEFE5 File Offset: 0x002DD1E5
		// (set) Token: 0x06008054 RID: 32852 RVA: 0x002DEFED File Offset: 0x002DD1ED
		public EconomyDetails.Resource.Type type { get; private set; }

		// Token: 0x06008055 RID: 32853 RVA: 0x002DEFF6 File Offset: 0x002DD1F6
		public Resource(Tag tag, EconomyDetails.Resource.Type type)
		{
			this.tag = tag;
			this.type = type;
		}

		// Token: 0x0200204A RID: 8266
		public class Type
		{
			// Token: 0x17000A01 RID: 2561
			// (get) Token: 0x0600A33A RID: 41786 RVA: 0x0034661B File Offset: 0x0034481B
			// (set) Token: 0x0600A33B RID: 41787 RVA: 0x00346623 File Offset: 0x00344823
			public string id { get; private set; }

			// Token: 0x17000A02 RID: 2562
			// (get) Token: 0x0600A33C RID: 41788 RVA: 0x0034662C File Offset: 0x0034482C
			// (set) Token: 0x0600A33D RID: 41789 RVA: 0x00346634 File Offset: 0x00344834
			public string unit { get; private set; }

			// Token: 0x0600A33E RID: 41790 RVA: 0x0034663D File Offset: 0x0034483D
			public Type(string id, string unit)
			{
				this.id = id;
				this.unit = unit;
			}
		}
	}

	// Token: 0x02001432 RID: 5170
	public class BiomeTransformation
	{
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06008056 RID: 32854 RVA: 0x002DF00C File Offset: 0x002DD20C
		// (set) Token: 0x06008057 RID: 32855 RVA: 0x002DF014 File Offset: 0x002DD214
		public Tag tag { get; private set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06008058 RID: 32856 RVA: 0x002DF01D File Offset: 0x002DD21D
		// (set) Token: 0x06008059 RID: 32857 RVA: 0x002DF025 File Offset: 0x002DD225
		public EconomyDetails.Resource resource { get; private set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600805A RID: 32858 RVA: 0x002DF02E File Offset: 0x002DD22E
		// (set) Token: 0x0600805B RID: 32859 RVA: 0x002DF036 File Offset: 0x002DD236
		public float ratio { get; private set; }

		// Token: 0x0600805C RID: 32860 RVA: 0x002DF03F File Offset: 0x002DD23F
		public BiomeTransformation(Tag tag, EconomyDetails.Resource resource, float ratio)
		{
			this.tag = tag;
			this.resource = resource;
			this.ratio = ratio;
		}

		// Token: 0x0600805D RID: 32861 RVA: 0x002DF05C File Offset: 0x002DD25C
		public float Transform(Element element, float amount)
		{
			if (this.resource.tag == element.tag)
			{
				return this.ratio * amount;
			}
			return 0f;
		}
	}

	// Token: 0x02001433 RID: 5171
	public class Ratio
	{
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600805E RID: 32862 RVA: 0x002DF084 File Offset: 0x002DD284
		// (set) Token: 0x0600805F RID: 32863 RVA: 0x002DF08C File Offset: 0x002DD28C
		public EconomyDetails.Resource input { get; private set; }

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06008060 RID: 32864 RVA: 0x002DF095 File Offset: 0x002DD295
		// (set) Token: 0x06008061 RID: 32865 RVA: 0x002DF09D File Offset: 0x002DD29D
		public EconomyDetails.Resource output { get; private set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06008062 RID: 32866 RVA: 0x002DF0A6 File Offset: 0x002DD2A6
		// (set) Token: 0x06008063 RID: 32867 RVA: 0x002DF0AE File Offset: 0x002DD2AE
		public bool allowNegativeOutput { get; private set; }

		// Token: 0x06008064 RID: 32868 RVA: 0x002DF0B7 File Offset: 0x002DD2B7
		public Ratio(EconomyDetails.Resource input, EconomyDetails.Resource output, bool allow_negative_output)
		{
			this.input = input;
			this.output = output;
			this.allowNegativeOutput = allow_negative_output;
		}
	}

	// Token: 0x02001434 RID: 5172
	public class Scenario
	{
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06008065 RID: 32869 RVA: 0x002DF0D4 File Offset: 0x002DD2D4
		// (set) Token: 0x06008066 RID: 32870 RVA: 0x002DF0DC File Offset: 0x002DD2DC
		public string name { get; private set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06008067 RID: 32871 RVA: 0x002DF0E5 File Offset: 0x002DD2E5
		// (set) Token: 0x06008068 RID: 32872 RVA: 0x002DF0ED File Offset: 0x002DD2ED
		public float defaultCount { get; private set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06008069 RID: 32873 RVA: 0x002DF0F6 File Offset: 0x002DD2F6
		// (set) Token: 0x0600806A RID: 32874 RVA: 0x002DF0FE File Offset: 0x002DD2FE
		public float timeInSeconds { get; set; }

		// Token: 0x0600806B RID: 32875 RVA: 0x002DF107 File Offset: 0x002DD307
		public Scenario(string name, float default_count, Func<EconomyDetails.Transformation, bool> filter)
		{
			this.name = name;
			this.defaultCount = default_count;
			this.filter = filter;
			this.timeInSeconds = 600f;
		}

		// Token: 0x0600806C RID: 32876 RVA: 0x002DF13A File Offset: 0x002DD33A
		public void AddEntry(EconomyDetails.Scenario.Entry entry)
		{
			this.entries.Add(entry);
		}

		// Token: 0x0600806D RID: 32877 RVA: 0x002DF148 File Offset: 0x002DD348
		public float GetCount(Tag tag)
		{
			foreach (EconomyDetails.Scenario.Entry entry in this.entries)
			{
				if (entry.tag == tag)
				{
					return entry.count;
				}
			}
			return this.defaultCount;
		}

		// Token: 0x0600806E RID: 32878 RVA: 0x002DF1B4 File Offset: 0x002DD3B4
		public bool IncludesTransformation(EconomyDetails.Transformation transformation)
		{
			if (this.filter != null && this.filter(transformation))
			{
				return true;
			}
			using (List<EconomyDetails.Scenario.Entry>.Enumerator enumerator = this.entries.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.tag == transformation.tag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040062C3 RID: 25283
		private Func<EconomyDetails.Transformation, bool> filter;

		// Token: 0x040062C4 RID: 25284
		private List<EconomyDetails.Scenario.Entry> entries = new List<EconomyDetails.Scenario.Entry>();

		// Token: 0x0200204B RID: 8267
		public class Entry
		{
			// Token: 0x17000A03 RID: 2563
			// (get) Token: 0x0600A33F RID: 41791 RVA: 0x00346653 File Offset: 0x00344853
			// (set) Token: 0x0600A340 RID: 41792 RVA: 0x0034665B File Offset: 0x0034485B
			public Tag tag { get; private set; }

			// Token: 0x17000A04 RID: 2564
			// (get) Token: 0x0600A341 RID: 41793 RVA: 0x00346664 File Offset: 0x00344864
			// (set) Token: 0x0600A342 RID: 41794 RVA: 0x0034666C File Offset: 0x0034486C
			public float count { get; private set; }

			// Token: 0x0600A343 RID: 41795 RVA: 0x00346675 File Offset: 0x00344875
			public Entry(Tag tag, float count)
			{
				this.tag = tag;
				this.count = count;
			}
		}
	}

	// Token: 0x02001435 RID: 5173
	public class Transformation
	{
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600806F RID: 32879 RVA: 0x002DF230 File Offset: 0x002DD430
		// (set) Token: 0x06008070 RID: 32880 RVA: 0x002DF238 File Offset: 0x002DD438
		public Tag tag { get; private set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06008071 RID: 32881 RVA: 0x002DF241 File Offset: 0x002DD441
		// (set) Token: 0x06008072 RID: 32882 RVA: 0x002DF249 File Offset: 0x002DD449
		public EconomyDetails.Transformation.Type type { get; private set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06008073 RID: 32883 RVA: 0x002DF252 File Offset: 0x002DD452
		// (set) Token: 0x06008074 RID: 32884 RVA: 0x002DF25A File Offset: 0x002DD45A
		public float timeInSeconds { get; private set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06008075 RID: 32885 RVA: 0x002DF263 File Offset: 0x002DD463
		// (set) Token: 0x06008076 RID: 32886 RVA: 0x002DF26B File Offset: 0x002DD46B
		public bool timeInvariant { get; private set; }

		// Token: 0x06008077 RID: 32887 RVA: 0x002DF274 File Offset: 0x002DD474
		public Transformation(Tag tag, EconomyDetails.Transformation.Type type, float time_in_seconds, bool timeInvariant = false)
		{
			this.tag = tag;
			this.type = type;
			this.timeInSeconds = time_in_seconds;
			this.timeInvariant = timeInvariant;
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x002DF2A4 File Offset: 0x002DD4A4
		public void AddDelta(EconomyDetails.Transformation.Delta delta)
		{
			global::Debug.Assert(delta.resource != null);
			this.deltas.Add(delta);
		}

		// Token: 0x06008079 RID: 32889 RVA: 0x002DF2C0 File Offset: 0x002DD4C0
		public EconomyDetails.Transformation.Delta GetDelta(EconomyDetails.Resource resource)
		{
			foreach (EconomyDetails.Transformation.Delta delta in this.deltas)
			{
				if (delta.resource == resource)
				{
					return delta;
				}
			}
			return null;
		}

		// Token: 0x040062C6 RID: 25286
		public List<EconomyDetails.Transformation.Delta> deltas = new List<EconomyDetails.Transformation.Delta>();

		// Token: 0x0200204C RID: 8268
		public class Delta
		{
			// Token: 0x17000A05 RID: 2565
			// (get) Token: 0x0600A344 RID: 41796 RVA: 0x0034668B File Offset: 0x0034488B
			// (set) Token: 0x0600A345 RID: 41797 RVA: 0x00346693 File Offset: 0x00344893
			public EconomyDetails.Resource resource { get; private set; }

			// Token: 0x17000A06 RID: 2566
			// (get) Token: 0x0600A346 RID: 41798 RVA: 0x0034669C File Offset: 0x0034489C
			// (set) Token: 0x0600A347 RID: 41799 RVA: 0x003466A4 File Offset: 0x003448A4
			public float amount { get; set; }

			// Token: 0x0600A348 RID: 41800 RVA: 0x003466AD File Offset: 0x003448AD
			public Delta(EconomyDetails.Resource resource, float amount)
			{
				this.resource = resource;
				this.amount = amount;
			}
		}

		// Token: 0x0200204D RID: 8269
		public class Type
		{
			// Token: 0x17000A07 RID: 2567
			// (get) Token: 0x0600A349 RID: 41801 RVA: 0x003466C3 File Offset: 0x003448C3
			// (set) Token: 0x0600A34A RID: 41802 RVA: 0x003466CB File Offset: 0x003448CB
			public string id { get; private set; }

			// Token: 0x0600A34B RID: 41803 RVA: 0x003466D4 File Offset: 0x003448D4
			public Type(string id)
			{
				this.id = id;
			}
		}
	}
}
