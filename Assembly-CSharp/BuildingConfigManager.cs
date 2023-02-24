using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x02000580 RID: 1408
[AddComponentMenu("KMonoBehaviour/scripts/BuildingConfigManager")]
public class BuildingConfigManager : KMonoBehaviour
{
	// Token: 0x0600223D RID: 8765 RVA: 0x000B9AF4 File Offset: 0x000B7CF4
	protected override void OnPrefabInit()
	{
		BuildingConfigManager.Instance = this;
		this.baseTemplate = new GameObject("BuildingTemplate");
		this.baseTemplate.SetActive(false);
		this.baseTemplate.AddComponent<KPrefabID>();
		this.baseTemplate.AddComponent<KSelectable>();
		this.baseTemplate.AddComponent<Modifiers>();
		this.baseTemplate.AddComponent<PrimaryElement>();
		this.baseTemplate.AddComponent<BuildingComplete>();
		this.baseTemplate.AddComponent<StateMachineController>();
		this.baseTemplate.AddComponent<Deconstructable>();
		this.baseTemplate.AddComponent<SaveLoadRoot>();
		this.baseTemplate.AddComponent<OccupyArea>();
		this.baseTemplate.AddComponent<DecorProvider>();
		this.baseTemplate.AddComponent<Operational>();
		this.baseTemplate.AddComponent<BuildingEnabledButton>();
		this.baseTemplate.AddComponent<Prioritizable>();
		this.baseTemplate.AddComponent<BuildingHP>();
		this.baseTemplate.AddComponent<LoopingSounds>();
		this.baseTemplate.AddComponent<InvalidPortReporter>();
		this.defaultBuildingCompleteKComponents.Add(typeof(RequiresFoundation));
	}

	// Token: 0x0600223E RID: 8766 RVA: 0x000B9BF9 File Offset: 0x000B7DF9
	public static string GetUnderConstructionName(string name)
	{
		return name + "UnderConstruction";
	}

	// Token: 0x0600223F RID: 8767 RVA: 0x000B9C08 File Offset: 0x000B7E08
	public void RegisterBuilding(IBuildingConfig config)
	{
		if (!DlcManager.IsDlcListValidForCurrentContent(config.GetDlcIds()))
		{
			return;
		}
		BuildingDef buildingDef = config.CreateBuildingDef();
		buildingDef.RequiredDlcIds = config.GetDlcIds();
		this.configTable[config] = buildingDef;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.baseTemplate);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		gameObject.GetComponent<KPrefabID>().PrefabTag = buildingDef.Tag;
		gameObject.name = buildingDef.PrefabID + "Template";
		gameObject.GetComponent<Building>().Def = buildingDef;
		gameObject.GetComponent<OccupyArea>().OccupiedCellsOffsets = buildingDef.PlacementOffsets;
		if (buildingDef.Deprecated)
		{
			gameObject.GetComponent<KPrefabID>().AddTag(GameTags.DeprecatedContent, false);
		}
		config.ConfigureBuildingTemplate(gameObject, buildingDef.Tag);
		buildingDef.BuildingComplete = BuildingLoader.Instance.CreateBuildingComplete(gameObject, buildingDef);
		bool flag = true;
		for (int i = 0; i < this.NonBuildableBuildings.Length; i++)
		{
			if (buildingDef.PrefabID == this.NonBuildableBuildings[i])
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			buildingDef.BuildingUnderConstruction = BuildingLoader.Instance.CreateBuildingUnderConstruction(buildingDef);
			buildingDef.BuildingUnderConstruction.name = BuildingConfigManager.GetUnderConstructionName(buildingDef.BuildingUnderConstruction.name);
			buildingDef.BuildingPreview = BuildingLoader.Instance.CreateBuildingPreview(buildingDef);
			GameObject buildingPreview = buildingDef.BuildingPreview;
			buildingPreview.name += "Preview";
		}
		buildingDef.PostProcess();
		config.DoPostConfigureComplete(buildingDef.BuildingComplete);
		if (flag)
		{
			config.DoPostConfigurePreview(buildingDef, buildingDef.BuildingPreview);
			config.DoPostConfigureUnderConstruction(buildingDef.BuildingUnderConstruction);
		}
		Assets.AddBuildingDef(buildingDef);
	}

	// Token: 0x06002240 RID: 8768 RVA: 0x000B9D90 File Offset: 0x000B7F90
	public void ConfigurePost()
	{
		foreach (KeyValuePair<IBuildingConfig, BuildingDef> keyValuePair in this.configTable)
		{
			keyValuePair.Key.ConfigurePost(keyValuePair.Value);
		}
	}

	// Token: 0x06002241 RID: 8769 RVA: 0x000B9DF0 File Offset: 0x000B7FF0
	public void IgnoreDefaultKComponent(Type type_to_ignore, Tag building_tag)
	{
		HashSet<Tag> hashSet;
		if (!this.ignoredDefaultKComponents.TryGetValue(type_to_ignore, out hashSet))
		{
			hashSet = new HashSet<Tag>();
			this.ignoredDefaultKComponents[type_to_ignore] = hashSet;
		}
		hashSet.Add(building_tag);
	}

	// Token: 0x06002242 RID: 8770 RVA: 0x000B9E28 File Offset: 0x000B8028
	private bool IsIgnoredDefaultKComponent(Tag building_tag, Type type)
	{
		bool flag = false;
		HashSet<Tag> hashSet;
		if (this.ignoredDefaultKComponents.TryGetValue(type, out hashSet) && hashSet.Contains(building_tag))
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x06002243 RID: 8771 RVA: 0x000B9E54 File Offset: 0x000B8054
	public void AddBuildingCompleteKComponents(GameObject go, Tag prefab_tag)
	{
		foreach (Type type in this.defaultBuildingCompleteKComponents)
		{
			if (!this.IsIgnoredDefaultKComponent(prefab_tag, type))
			{
				GameComps.GetKComponentManager(type).Add(go);
			}
		}
		HashSet<Type> hashSet;
		if (this.buildingCompleteKComponents.TryGetValue(prefab_tag, out hashSet))
		{
			foreach (Type type2 in hashSet)
			{
				GameComps.GetKComponentManager(type2).Add(go);
			}
		}
	}

	// Token: 0x06002244 RID: 8772 RVA: 0x000B9F08 File Offset: 0x000B8108
	public void DestroyBuildingCompleteKComponents(GameObject go, Tag prefab_tag)
	{
		foreach (Type type in this.defaultBuildingCompleteKComponents)
		{
			if (!this.IsIgnoredDefaultKComponent(prefab_tag, type))
			{
				GameComps.GetKComponentManager(type).Remove(go);
			}
		}
		HashSet<Type> hashSet;
		if (this.buildingCompleteKComponents.TryGetValue(prefab_tag, out hashSet))
		{
			foreach (Type type2 in hashSet)
			{
				GameComps.GetKComponentManager(type2).Remove(go);
			}
		}
	}

	// Token: 0x06002245 RID: 8773 RVA: 0x000B9FBC File Offset: 0x000B81BC
	public void AddDefaultBuildingCompleteKComponent(Type kcomponent_type)
	{
		this.defaultKComponents.Add(kcomponent_type);
	}

	// Token: 0x06002246 RID: 8774 RVA: 0x000B9FCC File Offset: 0x000B81CC
	public void AddBuildingCompleteKComponent(Tag prefab_tag, Type kcomponent_type)
	{
		HashSet<Type> hashSet;
		if (!this.buildingCompleteKComponents.TryGetValue(prefab_tag, out hashSet))
		{
			hashSet = new HashSet<Type>();
			this.buildingCompleteKComponents[prefab_tag] = hashSet;
		}
		hashSet.Add(kcomponent_type);
	}

	// Token: 0x040013C5 RID: 5061
	public static BuildingConfigManager Instance;

	// Token: 0x040013C6 RID: 5062
	private GameObject baseTemplate;

	// Token: 0x040013C7 RID: 5063
	private Dictionary<IBuildingConfig, BuildingDef> configTable = new Dictionary<IBuildingConfig, BuildingDef>();

	// Token: 0x040013C8 RID: 5064
	private string[] NonBuildableBuildings = new string[] { "Headquarters" };

	// Token: 0x040013C9 RID: 5065
	private HashSet<Type> defaultKComponents = new HashSet<Type>();

	// Token: 0x040013CA RID: 5066
	private HashSet<Type> defaultBuildingCompleteKComponents = new HashSet<Type>();

	// Token: 0x040013CB RID: 5067
	private Dictionary<Type, HashSet<Tag>> ignoredDefaultKComponents = new Dictionary<Type, HashSet<Tag>>();

	// Token: 0x040013CC RID: 5068
	private Dictionary<Tag, HashSet<Type>> buildingCompleteKComponents = new Dictionary<Tag, HashSet<Type>>();
}
