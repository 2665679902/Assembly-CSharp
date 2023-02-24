using System;
using UnityEngine;

// Token: 0x020007F5 RID: 2037
[AddComponentMenu("KMonoBehaviour/scripts/BuildingLoader")]
public class BuildingLoader : KMonoBehaviour
{
	// Token: 0x06003AB8 RID: 15032 RVA: 0x00145217 File Offset: 0x00143417
	public static void DestroyInstance()
	{
		BuildingLoader.Instance = null;
	}

	// Token: 0x06003AB9 RID: 15033 RVA: 0x0014521F File Offset: 0x0014341F
	protected override void OnPrefabInit()
	{
		BuildingLoader.Instance = this;
		this.previewTemplate = this.CreatePreviewTemplate();
		this.constructionTemplate = this.CreateConstructionTemplate();
		UnityEngine.Object.DontDestroyOnLoad(this.previewTemplate);
	}

	// Token: 0x06003ABA RID: 15034 RVA: 0x0014524A File Offset: 0x0014344A
	private GameObject CreateTemplate()
	{
		GameObject gameObject = new GameObject();
		gameObject.SetActive(false);
		gameObject.AddOrGet<KPrefabID>();
		gameObject.AddOrGet<KSelectable>();
		gameObject.AddOrGet<StateMachineController>();
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.Mass = 1f;
		primaryElement.Temperature = 293f;
		return gameObject;
	}

	// Token: 0x06003ABB RID: 15035 RVA: 0x00145288 File Offset: 0x00143488
	private GameObject CreatePreviewTemplate()
	{
		GameObject gameObject = this.CreateTemplate();
		gameObject.AddComponent<BuildingPreview>();
		return gameObject;
	}

	// Token: 0x06003ABC RID: 15036 RVA: 0x00145297 File Offset: 0x00143497
	private GameObject CreateConstructionTemplate()
	{
		GameObject gameObject = this.CreateTemplate();
		gameObject.AddOrGet<BuildingUnderConstruction>();
		gameObject.AddOrGet<Constructable>();
		gameObject.AddComponent<Storage>().doDiseaseTransfer = false;
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Notifier>();
		gameObject.AddOrGet<SaveLoadRoot>();
		return gameObject;
	}

	// Token: 0x06003ABD RID: 15037 RVA: 0x001452CE File Offset: 0x001434CE
	public GameObject CreateBuilding(BuildingDef def, GameObject go, GameObject parent = null)
	{
		go = UnityEngine.Object.Instantiate<GameObject>(go);
		go.name = def.PrefabID;
		if (parent != null)
		{
			go.transform.parent = parent.transform;
		}
		go.GetComponent<Building>().Def = def;
		return go;
	}

	// Token: 0x06003ABE RID: 15038 RVA: 0x0014530C File Offset: 0x0014350C
	private static bool Add2DComponents(BuildingDef def, GameObject go, string initialAnimState = null, bool no_collider = false, int layer = -1)
	{
		bool flag = def.AnimFiles != null && def.AnimFiles.Length != 0;
		if (layer == -1)
		{
			layer = LayerMask.NameToLayer("Default");
		}
		go.layer = layer;
		KBatchedAnimController[] components = go.GetComponents<KBatchedAnimController>();
		if (components.Length > 1)
		{
			for (int i = 2; i < components.Length; i++)
			{
				UnityEngine.Object.DestroyImmediate(components[i]);
			}
		}
		if (def.BlockTileAtlas == null)
		{
			KBatchedAnimController kbatchedAnimController = BuildingLoader.UpdateComponentRequirement<KBatchedAnimController>(go, flag);
			if (kbatchedAnimController != null)
			{
				kbatchedAnimController.AnimFiles = def.AnimFiles;
				if (def.isKAnimTile)
				{
					kbatchedAnimController.initialAnim = null;
				}
				else
				{
					if (def.isUtility && initialAnimState == null)
					{
						initialAnimState = "idle";
					}
					else if (go.GetComponent<Door>() != null)
					{
						initialAnimState = "closed";
					}
					kbatchedAnimController.initialAnim = ((initialAnimState != null) ? initialAnimState : def.DefaultAnimState);
					kbatchedAnimController.defaultAnim = kbatchedAnimController.initialAnim;
				}
				kbatchedAnimController.SetFGLayer(def.ForegroundLayer);
				kbatchedAnimController.materialType = KAnimBatchGroup.MaterialType.Default;
			}
		}
		KBoxCollider2D kboxCollider2D = BuildingLoader.UpdateComponentRequirement<KBoxCollider2D>(go, flag && !no_collider);
		if (kboxCollider2D != null)
		{
			kboxCollider2D.offset = new Vector3(0f, 0.5f * (float)def.HeightInCells, 0f);
			kboxCollider2D.size = new Vector3((float)def.WidthInCells, (float)def.HeightInCells, 0f);
		}
		if (def.AnimFiles == null)
		{
			global::Debug.LogError(def.Name + " Def missing anim files");
		}
		return flag;
	}

	// Token: 0x06003ABF RID: 15039 RVA: 0x00145490 File Offset: 0x00143690
	private static T UpdateComponentRequirement<T>(GameObject go, bool required) where T : Component
	{
		T t = go.GetComponent(typeof(T)) as T;
		if (!required && t != null)
		{
			UnityEngine.Object.DestroyImmediate(t, true);
			t = default(T);
		}
		else if (required && t == null)
		{
			t = go.AddComponent(typeof(T)) as T;
		}
		return t;
	}

	// Token: 0x06003AC0 RID: 15040 RVA: 0x0014550C File Offset: 0x0014370C
	public static KPrefabID AddID(GameObject go, string str)
	{
		KPrefabID kprefabID = go.GetComponent<KPrefabID>();
		if (kprefabID == null)
		{
			kprefabID = go.AddComponent<KPrefabID>();
		}
		kprefabID.PrefabTag = new Tag(str);
		kprefabID.SaveLoadTag = kprefabID.PrefabTag;
		kprefabID.InitializeTags(true);
		return kprefabID;
	}

	// Token: 0x06003AC1 RID: 15041 RVA: 0x00145550 File Offset: 0x00143750
	public GameObject CreateBuildingUnderConstruction(BuildingDef def)
	{
		GameObject gameObject = this.CreateBuilding(def, this.constructionTemplate, null);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		gameObject.GetComponent<KSelectable>().SetName(def.Name);
		for (int i = 0; i < def.Mass.Length; i++)
		{
			gameObject.GetComponent<PrimaryElement>().MassPerUnit += def.Mass[i];
		}
		KPrefabID kprefabID = BuildingLoader.AddID(gameObject, def.PrefabID + "UnderConstruction");
		kprefabID.AddTag(GameTags.UnderConstruction, false);
		BuildingLoader.UpdateComponentRequirement<BuildingCellVisualizer>(gameObject, def.CheckRequiresBuildingCellVisualizer());
		gameObject.GetComponent<Constructable>().SetWorkTime(def.ConstructionTime);
		if (def.Cancellable)
		{
			gameObject.AddOrGet<Cancellable>();
		}
		gameObject.AddComponent<BuildingFacade>();
		Rotatable rotatable = BuildingLoader.UpdateComponentRequirement<Rotatable>(gameObject, def.PermittedRotations > PermittedRotations.Unrotatable);
		if (rotatable)
		{
			rotatable.permittedRotations = def.PermittedRotations;
		}
		int num = LayerMask.NameToLayer("Construction");
		kprefabID.defaultLayer = num;
		BuildingLoader.Add2DComponents(def, gameObject, "place", false, num);
		BuildingLoader.UpdateComponentRequirement<Vent>(gameObject, false);
		bool flag = def.BuildingComplete.GetComponent<AnimTileable>() != null;
		BuildingLoader.UpdateComponentRequirement<AnimTileable>(gameObject, flag);
		if (def.RequiresPowerInput && def.AddLogicPowerPort)
		{
			GeneratedBuildings.RegisterSingleLogicInputPort(gameObject);
		}
		Assets.AddPrefab(kprefabID);
		gameObject.PreInit();
		GeneratedBuildings.InitializeHighEnergyParticlePorts(gameObject, def);
		GeneratedBuildings.InitializeLogicPorts(gameObject, def);
		return gameObject;
	}

	// Token: 0x06003AC2 RID: 15042 RVA: 0x001456A4 File Offset: 0x001438A4
	public GameObject CreateBuildingComplete(GameObject go, BuildingDef def)
	{
		go.name = def.PrefabID + "Complete";
		go.transform.SetPosition(new Vector3(0f, 0f, Grid.GetLayerZ(def.SceneLayer)));
		go.GetComponent<KSelectable>().SetName(def.Name);
		PrimaryElement component = go.GetComponent<PrimaryElement>();
		component.MassPerUnit = 0f;
		for (int i = 0; i < def.Mass.Length; i++)
		{
			component.MassPerUnit += def.Mass[i];
		}
		component.Temperature = 273.15f;
		BuildingHP buildingHP = go.AddOrGet<BuildingHP>();
		if (def.Invincible)
		{
			buildingHP.invincible = true;
		}
		buildingHP.SetHitPoints(def.HitPoints);
		if (def.Repairable)
		{
			BuildingLoader.UpdateComponentRequirement<Repairable>(go, true);
		}
		int num = LayerMask.NameToLayer("Default");
		go.layer = num;
		go.GetComponent<BuildingComplete>().Def = def;
		if (def.InputConduitType != ConduitType.None || def.OutputConduitType != ConduitType.None)
		{
			go.AddComponent<BuildingConduitEndpoints>();
		}
		if (!BuildingLoader.Add2DComponents(def, go, null, false, -1))
		{
			global::Debug.Log(def.Name + " is not yet a 2d building!");
		}
		go.AddOrGet<BuildingFacade>();
		BuildingLoader.UpdateComponentRequirement<EnergyConsumer>(go, def.RequiresPowerInput);
		Rotatable rotatable = BuildingLoader.UpdateComponentRequirement<Rotatable>(go, def.PermittedRotations > PermittedRotations.Unrotatable);
		if (rotatable)
		{
			rotatable.permittedRotations = def.PermittedRotations;
		}
		if (def.Breakable)
		{
			go.AddComponent<Breakable>();
		}
		ConduitConsumer conduitConsumer = BuildingLoader.UpdateComponentRequirement<ConduitConsumer>(go, def.InputConduitType == ConduitType.Gas || def.InputConduitType == ConduitType.Liquid);
		if (conduitConsumer != null)
		{
			conduitConsumer.SetConduitData(def.InputConduitType);
		}
		bool flag = def.RequiresPowerInput || def.InputConduitType == ConduitType.Gas || def.InputConduitType == ConduitType.Liquid;
		RequireInputs requireInputs = BuildingLoader.UpdateComponentRequirement<RequireInputs>(go, flag);
		if (requireInputs != null)
		{
			requireInputs.SetRequirements(def.RequiresPowerInput, def.InputConduitType == ConduitType.Gas || def.InputConduitType == ConduitType.Liquid);
		}
		BuildingLoader.UpdateComponentRequirement<RequireOutputs>(go, def.OutputConduitType > ConduitType.None);
		BuildingLoader.UpdateComponentRequirement<Operational>(go, !def.isUtility);
		if (def.Floodable)
		{
			go.AddComponent<Floodable>();
		}
		if (def.Disinfectable)
		{
			go.AddOrGet<AutoDisinfectable>();
			go.AddOrGet<Disinfectable>();
		}
		if (def.Overheatable)
		{
			Overheatable overheatable = go.AddComponent<Overheatable>();
			overheatable.baseOverheatTemp = def.OverheatTemperature;
			overheatable.baseFatalTemp = def.FatalHot;
		}
		if (def.Entombable)
		{
			go.AddOrGet<Structure>();
		}
		if (def.RequiresPowerInput && def.AddLogicPowerPort)
		{
			GeneratedBuildings.RegisterSingleLogicInputPort(go);
			go.AddOrGet<LogicOperationalController>();
		}
		BuildingLoader.UpdateComponentRequirement<BuildingCellVisualizer>(go, def.CheckRequiresBuildingCellVisualizer());
		if (def.BaseDecor != 0f)
		{
			DecorProvider decorProvider = BuildingLoader.UpdateComponentRequirement<DecorProvider>(go, true);
			decorProvider.baseDecor = def.BaseDecor;
			decorProvider.baseRadius = def.BaseDecorRadius;
		}
		if (def.AttachmentSlotTag != Tag.Invalid)
		{
			BuildingLoader.UpdateComponentRequirement<AttachableBuilding>(go, true).attachableToTag = def.AttachmentSlotTag;
		}
		KPrefabID kprefabID = BuildingLoader.AddID(go, def.PrefabID);
		kprefabID.defaultLayer = num;
		Assets.AddPrefab(kprefabID);
		go.PreInit();
		GeneratedBuildings.InitializeHighEnergyParticlePorts(go, def);
		GeneratedBuildings.InitializeLogicPorts(go, def);
		return go;
	}

	// Token: 0x06003AC3 RID: 15043 RVA: 0x001459C0 File Offset: 0x00143BC0
	public GameObject CreateBuildingPreview(BuildingDef def)
	{
		GameObject gameObject = this.CreateBuilding(def, this.previewTemplate, null);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		int num = LayerMask.NameToLayer("Place");
		gameObject.transform.SetPosition(new Vector3(0f, 0f, Grid.GetLayerZ(def.SceneLayer)));
		BuildingLoader.Add2DComponents(def, gameObject, "place", true, num);
		KAnimControllerBase component = gameObject.GetComponent<KAnimControllerBase>();
		if (component != null)
		{
			component.fgLayer = Grid.SceneLayer.NoLayer;
		}
		gameObject.AddComponent<BuildingFacade>();
		Rotatable rotatable = BuildingLoader.UpdateComponentRequirement<Rotatable>(gameObject, def.PermittedRotations > PermittedRotations.Unrotatable);
		if (rotatable)
		{
			rotatable.permittedRotations = def.PermittedRotations;
		}
		BuildingLoader.AddID(gameObject, def.PrefabID + "Preview").defaultLayer = num;
		gameObject.GetComponent<KSelectable>().SetName(def.Name);
		BuildingLoader.UpdateComponentRequirement<BuildingCellVisualizer>(gameObject, def.CheckRequiresBuildingCellVisualizer());
		KAnimGraphTileVisualizer component2 = gameObject.GetComponent<KAnimGraphTileVisualizer>();
		if (component2 != null)
		{
			UnityEngine.Object.DestroyImmediate(component2);
		}
		if (def.RequiresPowerInput && def.AddLogicPowerPort)
		{
			GeneratedBuildings.RegisterSingleLogicInputPort(gameObject);
		}
		gameObject.PreInit();
		GeneratedBuildings.InitializeHighEnergyParticlePorts(gameObject, def);
		Assets.AddPrefab(gameObject.GetComponent<KPrefabID>());
		GeneratedBuildings.InitializeLogicPorts(gameObject, def);
		return gameObject;
	}

	// Token: 0x04002686 RID: 9862
	private GameObject previewTemplate;

	// Token: 0x04002687 RID: 9863
	private GameObject constructionTemplate;

	// Token: 0x04002688 RID: 9864
	public static BuildingLoader Instance;
}
