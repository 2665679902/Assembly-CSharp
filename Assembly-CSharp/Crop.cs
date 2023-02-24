using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006C9 RID: 1737
[AddComponentMenu("KMonoBehaviour/scripts/Crop")]
public class Crop : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x17000351 RID: 849
	// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000F9A89 File Offset: 0x000F7C89
	public string cropId
	{
		get
		{
			return this.cropVal.cropId;
		}
	}

	// Token: 0x17000352 RID: 850
	// (get) Token: 0x06002F3D RID: 12093 RVA: 0x000F9A96 File Offset: 0x000F7C96
	// (set) Token: 0x06002F3E RID: 12094 RVA: 0x000F9A9E File Offset: 0x000F7C9E
	public Storage PlanterStorage
	{
		get
		{
			return this.planterStorage;
		}
		set
		{
			this.planterStorage = value;
		}
	}

	// Token: 0x06002F3F RID: 12095 RVA: 0x000F9AA7 File Offset: 0x000F7CA7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.Crops.Add(this);
		this.yield = this.GetAttributes().Add(Db.Get().PlantAttributes.YieldAmount);
	}

	// Token: 0x06002F40 RID: 12096 RVA: 0x000F9ADA File Offset: 0x000F7CDA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Crop>(1272413801, Crop.OnHarvestDelegate);
		base.Subscribe<Crop>(-1736624145, Crop.OnSeedDroppedDelegate);
	}

	// Token: 0x06002F41 RID: 12097 RVA: 0x000F9B04 File Offset: 0x000F7D04
	public void Configure(Crop.CropVal cropval)
	{
		this.cropVal = cropval;
	}

	// Token: 0x06002F42 RID: 12098 RVA: 0x000F9B0D File Offset: 0x000F7D0D
	public bool CanGrow()
	{
		return this.cropVal.renewable;
	}

	// Token: 0x06002F43 RID: 12099 RVA: 0x000F9B1C File Offset: 0x000F7D1C
	public void SpawnConfiguredFruit(object callbackParam)
	{
		if (this == null)
		{
			return;
		}
		Crop.CropVal cropVal = this.cropVal;
		if (!string.IsNullOrEmpty(cropVal.cropId))
		{
			this.SpawnSomeFruit(cropVal.cropId, this.yield.GetTotalValue());
			base.Trigger(-1072826864, this);
		}
	}

	// Token: 0x06002F44 RID: 12100 RVA: 0x000F9B70 File Offset: 0x000F7D70
	public void SpawnSomeFruit(Tag cropID, float amount)
	{
		GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(cropID), base.transform.GetPosition() + new Vector3(0f, 0.75f, 0f), Grid.SceneLayer.Ore, null, 0);
		if (gameObject != null)
		{
			MutantPlant component = base.GetComponent<MutantPlant>();
			MutantPlant component2 = gameObject.GetComponent<MutantPlant>();
			if (component != null && component.IsOriginal && component2 != null && base.GetComponent<SeedProducer>().RollForMutation())
			{
				component2.Mutate();
			}
			gameObject.SetActive(true);
			PrimaryElement component3 = gameObject.GetComponent<PrimaryElement>();
			component3.Units = amount;
			component3.Temperature = base.gameObject.GetComponent<PrimaryElement>().Temperature;
			base.Trigger(35625290, gameObject);
			Edible component4 = gameObject.GetComponent<Edible>();
			if (component4)
			{
				ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, component4.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.HARVESTED, "{0}", component4.GetProperName()), UI.ENDOFDAYREPORT.NOTES.HARVESTED_CONTEXT);
				return;
			}
		}
		else
		{
			DebugUtil.LogErrorArgs(base.gameObject, new object[] { "tried to spawn an invalid crop prefab:", cropID });
		}
	}

	// Token: 0x06002F45 RID: 12101 RVA: 0x000F9C94 File Offset: 0x000F7E94
	protected override void OnCleanUp()
	{
		Components.Crops.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002F46 RID: 12102 RVA: 0x000F9CA7 File Offset: 0x000F7EA7
	private void OnHarvest(object obj)
	{
	}

	// Token: 0x06002F47 RID: 12103 RVA: 0x000F9CA9 File Offset: 0x000F7EA9
	public void OnSeedDropped(object data)
	{
	}

	// Token: 0x06002F48 RID: 12104 RVA: 0x000F9CAB File Offset: 0x000F7EAB
	public List<Descriptor> RequirementDescriptors(GameObject go)
	{
		return new List<Descriptor>();
	}

	// Token: 0x06002F49 RID: 12105 RVA: 0x000F9CB4 File Offset: 0x000F7EB4
	public List<Descriptor> InformationDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Tag tag = new Tag(this.cropVal.cropId);
		GameObject prefab = Assets.GetPrefab(tag);
		Edible component = prefab.GetComponent<Edible>();
		Klei.AI.Attribute yieldAmount = Db.Get().PlantAttributes.YieldAmount;
		float preModifiedAttributeValue = go.GetComponent<Modifiers>().GetPreModifiedAttributeValue(yieldAmount);
		if (component != null)
		{
			DebugUtil.Assert(GameTags.DisplayAsCalories.Contains(tag), "Trying to display crop info for an edible fruit which isn't displayed as calories!", tag.ToString());
			float caloriesPerUnit = component.FoodInfo.CaloriesPerUnit;
			float num = caloriesPerUnit * preModifiedAttributeValue;
			string text = GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true);
			Descriptor descriptor = new Descriptor(string.Format(UI.UISIDESCREENS.PLANTERSIDESCREEN.YIELD, prefab.GetProperName(), text), string.Format(UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.YIELD, "", GameUtil.GetFormattedCalories(caloriesPerUnit, GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true)), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor);
		}
		else
		{
			string text;
			if (GameTags.DisplayAsUnits.Contains(tag))
			{
				text = GameUtil.GetFormattedUnits((float)this.cropVal.numProduced, GameUtil.TimeSlice.None, false, "");
			}
			else
			{
				text = GameUtil.GetFormattedMass((float)this.cropVal.numProduced, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
			}
			Descriptor descriptor2 = new Descriptor(string.Format(UI.UISIDESCREENS.PLANTERSIDESCREEN.YIELD_NONFOOD, prefab.GetProperName(), text), string.Format(UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.YIELD_NONFOOD, text), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x06002F4A RID: 12106 RVA: 0x000F9E24 File Offset: 0x000F8024
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in this.RequirementDescriptors(go))
		{
			list.Add(descriptor);
		}
		foreach (Descriptor descriptor2 in this.InformationDescriptors(go))
		{
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x04001C66 RID: 7270
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04001C67 RID: 7271
	public Crop.CropVal cropVal;

	// Token: 0x04001C68 RID: 7272
	private AttributeInstance yield;

	// Token: 0x04001C69 RID: 7273
	public string domesticatedDesc = "";

	// Token: 0x04001C6A RID: 7274
	private Storage planterStorage;

	// Token: 0x04001C6B RID: 7275
	private static readonly EventSystem.IntraObjectHandler<Crop> OnHarvestDelegate = new EventSystem.IntraObjectHandler<Crop>(delegate(Crop component, object data)
	{
		component.OnHarvest(data);
	});

	// Token: 0x04001C6C RID: 7276
	private static readonly EventSystem.IntraObjectHandler<Crop> OnSeedDroppedDelegate = new EventSystem.IntraObjectHandler<Crop>(delegate(Crop component, object data)
	{
		component.OnSeedDropped(data);
	});

	// Token: 0x020013AF RID: 5039
	[Serializable]
	public struct CropVal
	{
		// Token: 0x06007EA3 RID: 32419 RVA: 0x002D9881 File Offset: 0x002D7A81
		public CropVal(string crop_id, float crop_duration, int num_produced = 1, bool renewable = true)
		{
			this.cropId = crop_id;
			this.cropDuration = crop_duration;
			this.numProduced = num_produced;
			this.renewable = renewable;
		}

		// Token: 0x04006154 RID: 24916
		public string cropId;

		// Token: 0x04006155 RID: 24917
		public float cropDuration;

		// Token: 0x04006156 RID: 24918
		public int numProduced;

		// Token: 0x04006157 RID: 24919
		public bool renewable;
	}
}
