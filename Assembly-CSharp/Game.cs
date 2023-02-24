﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using FMOD.Studio;
using Klei;
using Klei.AI;
using Klei.CustomSettings;
using KSerialization;
using ProcGenGame;
using STRINGS;
using TUNING;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

// Token: 0x0200077E RID: 1918
[AddComponentMenu("KMonoBehaviour/scripts/Game")]
public class Game : KMonoBehaviour
{
	// Token: 0x060034C9 RID: 13513 RVA: 0x0011CB8C File Offset: 0x0011AD8C
	public static bool IsQuitting()
	{
		return Game.quitting;
	}

	// Token: 0x170003E1 RID: 993
	// (get) Token: 0x060034CA RID: 13514 RVA: 0x0011CB93 File Offset: 0x0011AD93
	// (set) Token: 0x060034CB RID: 13515 RVA: 0x0011CB9B File Offset: 0x0011AD9B
	public KInputHandler inputHandler { get; set; }

	// Token: 0x170003E2 RID: 994
	// (get) Token: 0x060034CC RID: 13516 RVA: 0x0011CBA4 File Offset: 0x0011ADA4
	// (set) Token: 0x060034CD RID: 13517 RVA: 0x0011CBAB File Offset: 0x0011ADAB
	public static Game Instance { get; private set; }

	// Token: 0x170003E3 RID: 995
	// (get) Token: 0x060034CE RID: 13518 RVA: 0x0011CBB3 File Offset: 0x0011ADB3
	public static Camera MainCamera
	{
		get
		{
			if (Game.m_CachedCamera == null)
			{
				Game.m_CachedCamera = Camera.main;
			}
			return Game.m_CachedCamera;
		}
	}

	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x060034CF RID: 13519 RVA: 0x0011CBD1 File Offset: 0x0011ADD1
	// (set) Token: 0x060034D0 RID: 13520 RVA: 0x0011CBF4 File Offset: 0x0011ADF4
	public bool SaveToCloudActive
	{
		get
		{
			return CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.SaveToCloud).id == "Enabled";
		}
		set
		{
			string text = (value ? "Enabled" : "Disabled");
			CustomGameSettings.Instance.SetQualitySetting(CustomGameSettingConfigs.SaveToCloud, text);
		}
	}

	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x060034D1 RID: 13521 RVA: 0x0011CC21 File Offset: 0x0011AE21
	// (set) Token: 0x060034D2 RID: 13522 RVA: 0x0011CC44 File Offset: 0x0011AE44
	public bool FastWorkersModeActive
	{
		get
		{
			return CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.FastWorkersMode).id == "Enabled";
		}
		set
		{
			string text = (value ? "Enabled" : "Disabled");
			CustomGameSettings.Instance.SetQualitySetting(CustomGameSettingConfigs.FastWorkersMode, text);
		}
	}

	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x060034D3 RID: 13523 RVA: 0x0011CC71 File Offset: 0x0011AE71
	// (set) Token: 0x060034D4 RID: 13524 RVA: 0x0011CC7C File Offset: 0x0011AE7C
	public bool SandboxModeActive
	{
		get
		{
			return this.sandboxModeActive;
		}
		set
		{
			this.sandboxModeActive = value;
			base.Trigger(-1948169901, null);
			if (PlanScreen.Instance != null)
			{
				PlanScreen.Instance.Refresh();
			}
			if (BuildMenu.Instance != null)
			{
				BuildMenu.Instance.Refresh();
			}
		}
	}

	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x060034D5 RID: 13525 RVA: 0x0011CCCA File Offset: 0x0011AECA
	public bool DebugOnlyBuildingsAllowed
	{
		get
		{
			return DebugHandler.enabled && (this.SandboxModeActive || DebugHandler.InstantBuildMode);
		}
	}

	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x060034D6 RID: 13526 RVA: 0x0011CCE4 File Offset: 0x0011AEE4
	// (set) Token: 0x060034D7 RID: 13527 RVA: 0x0011CCEC File Offset: 0x0011AEEC
	public StatusItemRenderer statusItemRenderer { get; private set; }

	// Token: 0x170003E9 RID: 1001
	// (get) Token: 0x060034D8 RID: 13528 RVA: 0x0011CCF5 File Offset: 0x0011AEF5
	// (set) Token: 0x060034D9 RID: 13529 RVA: 0x0011CCFD File Offset: 0x0011AEFD
	public PrioritizableRenderer prioritizableRenderer { get; private set; }

	// Token: 0x060034DA RID: 13530 RVA: 0x0011CD08 File Offset: 0x0011AF08
	protected override void OnPrefabInit()
	{
		DebugUtil.LogArgs(new object[]
		{
			Time.realtimeSinceStartup,
			"Level Loaded....",
			SceneManager.GetActiveScene().name
		});
		Components.BuildingCellVisualizers.OnAdd += this.OnAddBuildingCellVisualizer;
		Components.BuildingCellVisualizers.OnRemove += this.OnRemoveBuildingCellVisualizer;
		Singleton<KBatchedAnimUpdater>.CreateInstance();
		Singleton<CellChangeMonitor>.CreateInstance();
		this.userMenu = new UserMenu();
		SimTemperatureTransfer.ClearInstanceMap();
		StructureTemperatureComponents.ClearInstanceMap();
		ElementConsumer.ClearInstanceMap();
		App.OnPreLoadScene = (System.Action)Delegate.Combine(App.OnPreLoadScene, new System.Action(this.StopBE));
		Game.Instance = this;
		this.statusItemRenderer = new StatusItemRenderer();
		this.prioritizableRenderer = new PrioritizableRenderer();
		this.LoadEventHashes();
		this.savedInfo.InitializeEmptyVariables();
		this.gasFlowPos = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.GasConduits) - 0.4f);
		this.liquidFlowPos = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.LiquidConduits) - 0.4f);
		this.solidFlowPos = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.SolidConduitContents) - 0.4f);
		Shader.WarmupAllShaders();
		Db.Get();
		Game.quitting = false;
		Game.PickupableLayer = LayerMask.NameToLayer("Pickupable");
		Game.BlockSelectionLayerMask = LayerMask.GetMask(new string[] { "BlockSelection" });
		this.world = World.Instance;
		KPrefabID.NextUniqueID = KPlayerPrefs.GetInt(Game.NextUniqueIDKey, 0);
		this.circuitManager = new CircuitManager();
		this.energySim = new EnergySim();
		this.gasConduitSystem = new UtilityNetworkManager<FlowUtilityNetwork, Vent>(Grid.WidthInCells, Grid.HeightInCells, 13);
		this.liquidConduitSystem = new UtilityNetworkManager<FlowUtilityNetwork, Vent>(Grid.WidthInCells, Grid.HeightInCells, 17);
		this.electricalConduitSystem = new UtilityNetworkManager<ElectricalUtilityNetwork, Wire>(Grid.WidthInCells, Grid.HeightInCells, 27);
		this.logicCircuitSystem = new UtilityNetworkManager<LogicCircuitNetwork, LogicWire>(Grid.WidthInCells, Grid.HeightInCells, 32);
		this.logicCircuitManager = new LogicCircuitManager(this.logicCircuitSystem);
		this.travelTubeSystem = new UtilityNetworkTubesManager(Grid.WidthInCells, Grid.HeightInCells, 35);
		this.solidConduitSystem = new UtilityNetworkManager<FlowUtilityNetwork, SolidConduit>(Grid.WidthInCells, Grid.HeightInCells, 21);
		this.conduitTemperatureManager = new ConduitTemperatureManager();
		this.conduitDiseaseManager = new ConduitDiseaseManager(this.conduitTemperatureManager);
		this.gasConduitFlow = new ConduitFlow(ConduitType.Gas, Grid.CellCount, this.gasConduitSystem, 1f, 0.25f);
		this.liquidConduitFlow = new ConduitFlow(ConduitType.Liquid, Grid.CellCount, this.liquidConduitSystem, 10f, 0.75f);
		this.solidConduitFlow = new SolidConduitFlow(Grid.CellCount, this.solidConduitSystem, 0.75f);
		this.gasFlowVisualizer = new ConduitFlowVisualizer(this.gasConduitFlow, this.gasConduitVisInfo, GlobalResources.Instance().ConduitOverlaySoundGas, Lighting.Instance.Settings.GasConduit);
		this.liquidFlowVisualizer = new ConduitFlowVisualizer(this.liquidConduitFlow, this.liquidConduitVisInfo, GlobalResources.Instance().ConduitOverlaySoundLiquid, Lighting.Instance.Settings.LiquidConduit);
		this.solidFlowVisualizer = new SolidConduitFlowVisualizer(this.solidConduitFlow, this.solidConduitVisInfo, GlobalResources.Instance().ConduitOverlaySoundSolid, Lighting.Instance.Settings.SolidConduit);
		this.accumulators = new Accumulators();
		this.plantElementAbsorbers = new PlantElementAbsorbers();
		this.activeFX = new ushort[Grid.CellCount];
		this.UnsafePrefabInit();
		Shader.SetGlobalVector("_MetalParameters", new Vector4(0f, 0f, 0f, 0f));
		Shader.SetGlobalVector("_WaterParameters", new Vector4(0f, 0f, 0f, 0f));
		this.InitializeFXSpawners();
		PathFinder.Initialize();
		new GameNavGrids(Pathfinding.Instance);
		this.screenMgr = global::Util.KInstantiate(this.screenManagerPrefab, null, null).GetComponent<GameScreenManager>();
		this.roomProber = new RoomProber();
		this.fetchManager = base.gameObject.AddComponent<FetchManager>();
		this.ediblesManager = base.gameObject.AddComponent<EdiblesManager>();
		Singleton<CellChangeMonitor>.Instance.SetGridSize(Grid.WidthInCells, Grid.HeightInCells);
		this.unlocks = base.GetComponent<Unlocks>();
		this.changelistsPlayedOn = new List<uint>();
		this.changelistsPlayedOn.Add(544519U);
		this.dateGenerated = System.DateTime.UtcNow.ToString("U", CultureInfo.InvariantCulture);
	}

	// Token: 0x060034DB RID: 13531 RVA: 0x0011D177 File Offset: 0x0011B377
	public void SetGameStarted()
	{
		this.gameStarted = true;
	}

	// Token: 0x060034DC RID: 13532 RVA: 0x0011D180 File Offset: 0x0011B380
	public bool GameStarted()
	{
		return this.gameStarted;
	}

	// Token: 0x060034DD RID: 13533 RVA: 0x0011D188 File Offset: 0x0011B388
	private void UnsafePrefabInit()
	{
		this.StepTheSim(0f);
	}

	// Token: 0x060034DE RID: 13534 RVA: 0x0011D196 File Offset: 0x0011B396
	protected override void OnLoadLevel()
	{
		base.Unsubscribe<Game>(1798162660, Game.MarkStatusItemRendererDirtyDelegate, false);
		base.Unsubscribe<Game>(1983128072, Game.ActiveWorldChangedDelegate, false);
		base.OnLoadLevel();
	}

	// Token: 0x060034DF RID: 13535 RVA: 0x0011D1C0 File Offset: 0x0011B3C0
	private void MarkStatusItemRendererDirty(object data)
	{
		this.statusItemRenderer.MarkAllDirty();
	}

	// Token: 0x060034E0 RID: 13536 RVA: 0x0011D1D0 File Offset: 0x0011B3D0
	protected override void OnForcedCleanUp()
	{
		if (this.prioritizableRenderer != null)
		{
			this.prioritizableRenderer.Cleanup();
			this.prioritizableRenderer = null;
		}
		if (this.statusItemRenderer != null)
		{
			this.statusItemRenderer.Destroy();
			this.statusItemRenderer = null;
		}
		if (this.conduitTemperatureManager != null)
		{
			this.conduitTemperatureManager.Shutdown();
		}
		this.gasFlowVisualizer.FreeResources();
		this.liquidFlowVisualizer.FreeResources();
		this.solidFlowVisualizer.FreeResources();
		LightGridManager.Shutdown();
		RadiationGridManager.Shutdown();
		App.OnPreLoadScene = (System.Action)Delegate.Remove(App.OnPreLoadScene, new System.Action(this.StopBE));
		base.OnForcedCleanUp();
	}

	// Token: 0x060034E1 RID: 13537 RVA: 0x0011D278 File Offset: 0x0011B478
	protected override void OnSpawn()
	{
		global::Debug.Log("-- GAME --");
		PropertyTextures.FogOfWarScale = 0f;
		if (CameraController.Instance != null)
		{
			CameraController.Instance.EnableFreeCamera(false);
		}
		this.LocalPlayer = this.SpawnPlayer();
		WaterCubes.Instance.Init();
		SpeedControlScreen.Instance.Pause(false, false);
		LightGridManager.Initialise();
		RadiationGridManager.Initialise();
		this.RefreshRadiationLoop();
		this.UnsafeOnSpawn();
		Time.timeScale = 0f;
		if (this.tempIntroScreenPrefab != null)
		{
			global::Util.KInstantiate(this.tempIntroScreenPrefab, null, null);
		}
		if (SaveLoader.Instance.ClusterLayout != null)
		{
			foreach (WorldGen worldGen in SaveLoader.Instance.ClusterLayout.worlds)
			{
				this.Reset(worldGen.data.gameSpawnData, worldGen.WorldOffset);
			}
			NewBaseScreen.SetInitialCamera();
		}
		TagManager.FillMissingProperNames();
		CameraController.Instance.OrthographicSize = 20f;
		if (SaveLoader.Instance.loadedFromSave)
		{
			this.baseAlreadyCreated = true;
			base.Trigger(-1992507039, null);
			base.Trigger(-838649377, null);
		}
		UnityEngine.Object[] array = Resources.FindObjectsOfTypeAll(typeof(MeshRenderer));
		for (int i = 0; i < array.Length; i++)
		{
			((MeshRenderer)array[i]).reflectionProbeUsage = ReflectionProbeUsage.Off;
		}
		base.Subscribe<Game>(1798162660, Game.MarkStatusItemRendererDirtyDelegate);
		base.Subscribe<Game>(1983128072, Game.ActiveWorldChangedDelegate);
		this.solidConduitFlow.Initialize();
		SimAndRenderScheduler.instance.Add(this.roomProber, false);
		SimAndRenderScheduler.instance.Add(KComponentSpawn.instance, false);
		SimAndRenderScheduler.instance.RegisterBatchUpdate<ISim200ms, AmountInstance>(new UpdateBucketWithUpdater<ISim200ms>.BatchUpdateDelegate(AmountInstance.BatchUpdate));
		SimAndRenderScheduler.instance.RegisterBatchUpdate<ISim1000ms, SolidTransferArm>(new UpdateBucketWithUpdater<ISim1000ms>.BatchUpdateDelegate(SolidTransferArm.BatchUpdate));
		if (!SaveLoader.Instance.loadedFromSave)
		{
			SettingConfig settingConfig = CustomGameSettings.Instance.QualitySettings[CustomGameSettingConfigs.SandboxMode.id];
			SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.SandboxMode);
			SaveGame.Instance.sandboxEnabled = !settingConfig.IsDefaultLevel(currentQualitySetting.id);
		}
		this.mingleCellTracker = base.gameObject.AddComponent<MingleCellTracker>();
		if (Global.Instance != null)
		{
			Global.Instance.GetComponent<PerformanceMonitor>().Reset();
			Global.Instance.modManager.NotifyDialog(UI.FRONTEND.MOD_DIALOGS.SAVE_GAME_MODS_DIFFER.TITLE, UI.FRONTEND.MOD_DIALOGS.SAVE_GAME_MODS_DIFFER.MESSAGE, Global.Instance.globalCanvas);
		}
	}

	// Token: 0x060034E2 RID: 13538 RVA: 0x0011D514 File Offset: 0x0011B714
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		SimAndRenderScheduler.instance.Remove(KComponentSpawn.instance);
		SimAndRenderScheduler.instance.RegisterBatchUpdate<ISim200ms, AmountInstance>(null);
		SimAndRenderScheduler.instance.RegisterBatchUpdate<ISim1000ms, SolidTransferArm>(null);
		this.DestroyInstances();
	}

	// Token: 0x060034E3 RID: 13539 RVA: 0x0011D547 File Offset: 0x0011B747
	private new void OnDestroy()
	{
		base.OnDestroy();
		this.DestroyInstances();
	}

	// Token: 0x060034E4 RID: 13540 RVA: 0x0011D555 File Offset: 0x0011B755
	private void UnsafeOnSpawn()
	{
		this.world.UpdateCellInfo(this.gameSolidInfo, this.callbackInfo, 0, null, 0, null);
	}

	// Token: 0x060034E5 RID: 13541 RVA: 0x0011D574 File Offset: 0x0011B774
	private void RefreshRadiationLoop()
	{
		GameScheduler.Instance.Schedule("UpdateRadiation", 1f, delegate(object obj)
		{
			RadiationGridManager.Refresh();
			this.RefreshRadiationLoop();
		}, null, null);
	}

	// Token: 0x060034E6 RID: 13542 RVA: 0x0011D599 File Offset: 0x0011B799
	public void SetMusicEnabled(bool enabled)
	{
		if (enabled)
		{
			MusicManager.instance.PlaySong("Music_FrontEnd", false);
			return;
		}
		MusicManager.instance.StopSong("Music_FrontEnd", true, STOP_MODE.ALLOWFADEOUT);
	}

	// Token: 0x060034E7 RID: 13543 RVA: 0x0011D5C0 File Offset: 0x0011B7C0
	private Player SpawnPlayer()
	{
		Player component = global::Util.KInstantiate(this.playerPrefab, base.gameObject, null).GetComponent<Player>();
		component.ScreenManager = this.screenMgr;
		component.ScreenManager.StartScreen(ScreenPrefabs.Instance.HudScreen.gameObject, null, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay);
		component.ScreenManager.StartScreen(ScreenPrefabs.Instance.HoverTextScreen.gameObject, null, GameScreenManager.UIRenderTarget.HoverTextScreen);
		component.ScreenManager.StartScreen(ScreenPrefabs.Instance.ToolTipScreen.gameObject, null, GameScreenManager.UIRenderTarget.HoverTextScreen);
		this.cameraController = global::Util.KInstantiate(this.cameraControllerPrefab, null, null).GetComponent<CameraController>();
		component.CameraController = this.cameraController;
		if (KInputManager.currentController != null)
		{
			KInputHandler.Add(KInputManager.currentController, this.cameraController, 1);
		}
		else
		{
			KInputHandler.Add(Global.GetInputManager().GetDefaultController(), this.cameraController, 1);
		}
		Global.GetInputManager().usedMenus.Add(this.cameraController);
		this.playerController = component.GetComponent<PlayerController>();
		if (KInputManager.currentController != null)
		{
			KInputHandler.Add(KInputManager.currentController, this.playerController, 20);
		}
		else
		{
			KInputHandler.Add(Global.GetInputManager().GetDefaultController(), this.playerController, 20);
		}
		Global.GetInputManager().usedMenus.Add(this.playerController);
		return component;
	}

	// Token: 0x060034E8 RID: 13544 RVA: 0x0011D705 File Offset: 0x0011B905
	public void SetDupePassableSolid(int cell, bool passable, bool solid)
	{
		Grid.DupePassable[cell] = passable;
		this.gameSolidInfo.Add(new SolidInfo(cell, solid));
	}

	// Token: 0x060034E9 RID: 13545 RVA: 0x0011D728 File Offset: 0x0011B928
	private unsafe Sim.GameDataUpdate* StepTheSim(float dt)
	{
		Sim.GameDataUpdate* ptr;
		using (new KProfiler.Region("StepTheSim", null))
		{
			IntPtr intPtr = IntPtr.Zero;
			using (new KProfiler.Region("WaitingForSim", null))
			{
				if (Grid.Visible == null || Grid.Visible.Length == 0)
				{
					global::Debug.LogError("Invalid Grid.Visible, what have you done?!");
					return null;
				}
				intPtr = Sim.HandleMessage(SimMessageHashes.PrepareGameData, Grid.Visible.Length, Grid.Visible);
			}
			if (intPtr == IntPtr.Zero)
			{
				ptr = null;
			}
			else
			{
				Sim.GameDataUpdate* ptr2 = (Sim.GameDataUpdate*)(void*)intPtr;
				Grid.elementIdx = ptr2->elementIdx;
				Grid.temperature = ptr2->temperature;
				Grid.mass = ptr2->mass;
				Grid.radiation = ptr2->radiation;
				Grid.properties = ptr2->properties;
				Grid.strengthInfo = ptr2->strengthInfo;
				Grid.insulation = ptr2->insulation;
				Grid.diseaseIdx = ptr2->diseaseIdx;
				Grid.diseaseCount = ptr2->diseaseCount;
				Grid.AccumulatedFlowValues = ptr2->accumulatedFlow;
				Grid.exposedToSunlight = (byte*)(void*)ptr2->propertyTextureExposedToSunlight;
				PropertyTextures.externalFlowTex = ptr2->propertyTextureFlow;
				PropertyTextures.externalLiquidTex = ptr2->propertyTextureLiquid;
				PropertyTextures.externalExposedToSunlight = ptr2->propertyTextureExposedToSunlight;
				List<Element> elements = ElementLoader.elements;
				this.simData.emittedMassEntries = ptr2->emittedMassEntries;
				this.simData.elementChunks = ptr2->elementChunkInfos;
				this.simData.buildingTemperatures = ptr2->buildingTemperatures;
				this.simData.diseaseEmittedInfos = ptr2->diseaseEmittedInfos;
				this.simData.diseaseConsumedInfos = ptr2->diseaseConsumedInfos;
				for (int i = 0; i < ptr2->numSubstanceChangeInfo; i++)
				{
					Sim.SubstanceChangeInfo substanceChangeInfo = ptr2->substanceChangeInfo[i];
					Element element = elements[(int)substanceChangeInfo.newElemIdx];
					Grid.Element[substanceChangeInfo.cellIdx] = element;
				}
				for (int j = 0; j < ptr2->numSolidInfo; j++)
				{
					Sim.SolidInfo solidInfo = ptr2->solidInfo[j];
					if (!this.solidChangedFilter.Contains(solidInfo.cellIdx))
					{
						this.solidInfo.Add(new SolidInfo(solidInfo.cellIdx, solidInfo.isSolid != 0));
						bool flag = solidInfo.isSolid != 0;
						Grid.SetSolid(solidInfo.cellIdx, flag, CellEventLogger.Instance.SimMessagesSolid);
					}
				}
				for (int k = 0; k < ptr2->numCallbackInfo; k++)
				{
					Sim.CallbackInfo callbackInfo = ptr2->callbackInfo[k];
					HandleVector<Game.CallbackInfo>.Handle handle = new HandleVector<Game.CallbackInfo>.Handle
					{
						index = callbackInfo.callbackIdx
					};
					if (!this.IsManuallyReleasedHandle(handle))
					{
						this.callbackInfo.Add(new Klei.CallbackInfo(handle));
					}
				}
				int numSpawnFallingLiquidInfo = ptr2->numSpawnFallingLiquidInfo;
				for (int l = 0; l < numSpawnFallingLiquidInfo; l++)
				{
					Sim.SpawnFallingLiquidInfo spawnFallingLiquidInfo = ptr2->spawnFallingLiquidInfo[l];
					FallingWater.instance.AddParticle(spawnFallingLiquidInfo.cellIdx, spawnFallingLiquidInfo.elemIdx, spawnFallingLiquidInfo.mass, spawnFallingLiquidInfo.temperature, spawnFallingLiquidInfo.diseaseIdx, spawnFallingLiquidInfo.diseaseCount, false, false, false, false);
				}
				int numDigInfo = ptr2->numDigInfo;
				WorldDamage component = this.world.GetComponent<WorldDamage>();
				for (int m = 0; m < numDigInfo; m++)
				{
					Sim.SpawnOreInfo spawnOreInfo = ptr2->digInfo[m];
					if (spawnOreInfo.temperature <= 0f && spawnOreInfo.mass > 0f)
					{
						global::Debug.LogError("Sim is telling us to spawn a zero temperature object. This shouldn't be possible because I have asserts in the dll about this....");
					}
					component.OnDigComplete(spawnOreInfo.cellIdx, spawnOreInfo.mass, spawnOreInfo.temperature, spawnOreInfo.elemIdx, spawnOreInfo.diseaseIdx, spawnOreInfo.diseaseCount);
				}
				int numSpawnOreInfo = ptr2->numSpawnOreInfo;
				for (int n = 0; n < numSpawnOreInfo; n++)
				{
					Sim.SpawnOreInfo spawnOreInfo2 = ptr2->spawnOreInfo[n];
					Vector3 vector = Grid.CellToPosCCC(spawnOreInfo2.cellIdx, Grid.SceneLayer.Ore);
					Element element2 = ElementLoader.elements[(int)spawnOreInfo2.elemIdx];
					if (spawnOreInfo2.temperature <= 0f && spawnOreInfo2.mass > 0f)
					{
						global::Debug.LogError("Sim is telling us to spawn a zero temperature object. This shouldn't be possible because I have asserts in the dll about this....");
					}
					element2.substance.SpawnResource(vector, spawnOreInfo2.mass, spawnOreInfo2.temperature, spawnOreInfo2.diseaseIdx, spawnOreInfo2.diseaseCount, false, false, false);
				}
				int numSpawnFXInfo = ptr2->numSpawnFXInfo;
				for (int num = 0; num < numSpawnFXInfo; num++)
				{
					Sim.SpawnFXInfo spawnFXInfo = ptr2->spawnFXInfo[num];
					this.SpawnFX((SpawnFXHashes)spawnFXInfo.fxHash, spawnFXInfo.cellIdx, spawnFXInfo.rotation);
				}
				UnstableGroundManager component2 = this.world.GetComponent<UnstableGroundManager>();
				int numUnstableCellInfo = ptr2->numUnstableCellInfo;
				for (int num2 = 0; num2 < numUnstableCellInfo; num2++)
				{
					Sim.UnstableCellInfo unstableCellInfo = ptr2->unstableCellInfo[num2];
					if (unstableCellInfo.fallingInfo == 0)
					{
						component2.Spawn(unstableCellInfo.cellIdx, ElementLoader.elements[(int)unstableCellInfo.elemIdx], unstableCellInfo.mass, unstableCellInfo.temperature, unstableCellInfo.diseaseIdx, unstableCellInfo.diseaseCount);
					}
				}
				int numWorldDamageInfo = ptr2->numWorldDamageInfo;
				for (int num3 = 0; num3 < numWorldDamageInfo; num3++)
				{
					Sim.WorldDamageInfo worldDamageInfo = ptr2->worldDamageInfo[num3];
					WorldDamage.Instance.ApplyDamage(worldDamageInfo);
				}
				for (int num4 = 0; num4 < ptr2->numRemovedMassEntries; num4++)
				{
					ElementConsumer.AddMass(ptr2->removedMassEntries[num4]);
				}
				int numMassConsumedCallbacks = ptr2->numMassConsumedCallbacks;
				HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle2 = default(HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle);
				for (int num5 = 0; num5 < numMassConsumedCallbacks; num5++)
				{
					Sim.MassConsumedCallback massConsumedCallback = ptr2->massConsumedCallbacks[num5];
					handle2.index = massConsumedCallback.callbackIdx;
					Game.ComplexCallbackInfo<Sim.MassConsumedCallback> complexCallbackInfo = this.massConsumedCallbackManager.Release(handle2, "massConsumedCB");
					if (complexCallbackInfo.cb != null)
					{
						complexCallbackInfo.cb(massConsumedCallback, complexCallbackInfo.callbackData);
					}
				}
				int numMassEmittedCallbacks = ptr2->numMassEmittedCallbacks;
				HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.Handle handle3 = default(HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.Handle);
				for (int num6 = 0; num6 < numMassEmittedCallbacks; num6++)
				{
					Sim.MassEmittedCallback massEmittedCallback = ptr2->massEmittedCallbacks[num6];
					handle3.index = massEmittedCallback.callbackIdx;
					if (this.massEmitCallbackManager.IsVersionValid(handle3))
					{
						Game.ComplexCallbackInfo<Sim.MassEmittedCallback> item = this.massEmitCallbackManager.GetItem(handle3);
						if (item.cb != null)
						{
							item.cb(massEmittedCallback, item.callbackData);
						}
					}
				}
				int numDiseaseConsumptionCallbacks = ptr2->numDiseaseConsumptionCallbacks;
				HandleVector<Game.ComplexCallbackInfo<Sim.DiseaseConsumptionCallback>>.Handle handle4 = default(HandleVector<Game.ComplexCallbackInfo<Sim.DiseaseConsumptionCallback>>.Handle);
				for (int num7 = 0; num7 < numDiseaseConsumptionCallbacks; num7++)
				{
					Sim.DiseaseConsumptionCallback diseaseConsumptionCallback = ptr2->diseaseConsumptionCallbacks[num7];
					handle4.index = diseaseConsumptionCallback.callbackIdx;
					if (this.diseaseConsumptionCallbackManager.IsVersionValid(handle4))
					{
						Game.ComplexCallbackInfo<Sim.DiseaseConsumptionCallback> item2 = this.diseaseConsumptionCallbackManager.GetItem(handle4);
						if (item2.cb != null)
						{
							item2.cb(diseaseConsumptionCallback, item2.callbackData);
						}
					}
				}
				int numComponentStateChangedMessages = ptr2->numComponentStateChangedMessages;
				HandleVector<Game.ComplexCallbackInfo<int>>.Handle handle5 = default(HandleVector<Game.ComplexCallbackInfo<int>>.Handle);
				for (int num8 = 0; num8 < numComponentStateChangedMessages; num8++)
				{
					Sim.ComponentStateChangedMessage componentStateChangedMessage = ptr2->componentStateChangedMessages[num8];
					handle5.index = componentStateChangedMessage.callbackIdx;
					if (this.simComponentCallbackManager.IsVersionValid(handle5))
					{
						Game.ComplexCallbackInfo<int> complexCallbackInfo2 = this.simComponentCallbackManager.Release(handle5, "component state changed cb");
						if (complexCallbackInfo2.cb != null)
						{
							complexCallbackInfo2.cb(componentStateChangedMessage.simHandle, complexCallbackInfo2.callbackData);
						}
					}
				}
				int numRadiationConsumedCallbacks = ptr2->numRadiationConsumedCallbacks;
				HandleVector<Game.ComplexCallbackInfo<Sim.ConsumedRadiationCallback>>.Handle handle6 = default(HandleVector<Game.ComplexCallbackInfo<Sim.ConsumedRadiationCallback>>.Handle);
				for (int num9 = 0; num9 < numRadiationConsumedCallbacks; num9++)
				{
					Sim.ConsumedRadiationCallback consumedRadiationCallback = ptr2->radiationConsumedCallbacks[num9];
					handle6.index = consumedRadiationCallback.callbackIdx;
					Game.ComplexCallbackInfo<Sim.ConsumedRadiationCallback> complexCallbackInfo3 = this.radiationConsumedCallbackManager.Release(handle6, "radiationConsumedCB");
					if (complexCallbackInfo3.cb != null)
					{
						complexCallbackInfo3.cb(consumedRadiationCallback, complexCallbackInfo3.callbackData);
					}
				}
				int numElementChunkMeltedInfos = ptr2->numElementChunkMeltedInfos;
				for (int num10 = 0; num10 < numElementChunkMeltedInfos; num10++)
				{
					SimTemperatureTransfer.DoOreMeltTransition(ptr2->elementChunkMeltedInfos[num10].handle);
				}
				int numBuildingOverheatInfos = ptr2->numBuildingOverheatInfos;
				for (int num11 = 0; num11 < numBuildingOverheatInfos; num11++)
				{
					StructureTemperatureComponents.DoOverheat(ptr2->buildingOverheatInfos[num11].handle);
				}
				int numBuildingNoLongerOverheatedInfos = ptr2->numBuildingNoLongerOverheatedInfos;
				for (int num12 = 0; num12 < numBuildingNoLongerOverheatedInfos; num12++)
				{
					StructureTemperatureComponents.DoNoLongerOverheated(ptr2->buildingNoLongerOverheatedInfos[num12].handle);
				}
				int numBuildingMeltedInfos = ptr2->numBuildingMeltedInfos;
				for (int num13 = 0; num13 < numBuildingMeltedInfos; num13++)
				{
					StructureTemperatureComponents.DoStateTransition(ptr2->buildingMeltedInfos[num13].handle);
				}
				int numCellMeltedInfos = ptr2->numCellMeltedInfos;
				for (int num14 = 0; num14 < numCellMeltedInfos; num14++)
				{
					int gameCell = ptr2->cellMeltedInfos[num14].gameCell;
					GameObject gameObject = Grid.Objects[gameCell, 9];
					if (gameObject != null)
					{
						global::Util.KDestroyGameObject(gameObject);
					}
				}
				if (dt > 0f)
				{
					this.conduitTemperatureManager.Sim200ms(0.2f);
					this.conduitDiseaseManager.Sim200ms(0.2f);
					this.gasConduitFlow.Sim200ms(0.2f);
					this.liquidConduitFlow.Sim200ms(0.2f);
					this.solidConduitFlow.Sim200ms(0.2f);
					this.accumulators.Sim200ms(0.2f);
					this.plantElementAbsorbers.Sim200ms(0.2f);
				}
				Sim.DebugProperties debugProperties;
				debugProperties.buildingTemperatureScale = 100f;
				debugProperties.buildingToBuildingTemperatureScale = 0.001f;
				debugProperties.biomeTemperatureLerpRate = 0.001f;
				debugProperties.isDebugEditing = ((DebugPaintElementScreen.Instance != null && DebugPaintElementScreen.Instance.gameObject.activeSelf) ? 1 : 0);
				debugProperties.pad0 = (debugProperties.pad1 = (debugProperties.pad2 = 0));
				SimMessages.SetDebugProperties(debugProperties);
				if (dt > 0f)
				{
					if (this.circuitManager != null)
					{
						this.circuitManager.Sim200msFirst(dt);
					}
					if (this.energySim != null)
					{
						this.energySim.EnergySim200ms(dt);
					}
					if (this.circuitManager != null)
					{
						this.circuitManager.Sim200msLast(dt);
					}
				}
				ptr = ptr2;
			}
		}
		return ptr;
	}

	// Token: 0x060034EA RID: 13546 RVA: 0x0011E20C File Offset: 0x0011C40C
	public void AddSolidChangedFilter(int cell)
	{
		this.solidChangedFilter.Add(cell);
	}

	// Token: 0x060034EB RID: 13547 RVA: 0x0011E21B File Offset: 0x0011C41B
	public void RemoveSolidChangedFilter(int cell)
	{
		this.solidChangedFilter.Remove(cell);
	}

	// Token: 0x060034EC RID: 13548 RVA: 0x0011E22A File Offset: 0x0011C42A
	public void SetIsLoading()
	{
		this.isLoading = true;
	}

	// Token: 0x060034ED RID: 13549 RVA: 0x0011E233 File Offset: 0x0011C433
	public bool IsLoading()
	{
		return this.isLoading;
	}

	// Token: 0x060034EE RID: 13550 RVA: 0x0011E23C File Offset: 0x0011C43C
	private void ShowDebugCellInfo()
	{
		int mouseCell = DebugHandler.GetMouseCell();
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(mouseCell, out num, out num2);
		string text = string.Concat(new string[]
		{
			mouseCell.ToString(),
			" (",
			num.ToString(),
			", ",
			num2.ToString(),
			")"
		});
		DebugText.Instance.Draw(text, Grid.CellToPosCCC(mouseCell, Grid.SceneLayer.Move), Color.white);
	}

	// Token: 0x060034EF RID: 13551 RVA: 0x0011E2B7 File Offset: 0x0011C4B7
	public void ForceSimStep()
	{
		DebugUtil.LogArgs(new object[] { "Force-stepping the sim" });
		this.simDt = 0.2f;
	}

	// Token: 0x060034F0 RID: 13552 RVA: 0x0011E2D8 File Offset: 0x0011C4D8
	private void Update()
	{
		if (this.isLoading)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		if (global::Debug.developerConsoleVisible)
		{
			global::Debug.developerConsoleVisible = false;
		}
		if (DebugHandler.DebugCellInfo)
		{
			this.ShowDebugCellInfo();
		}
		this.gasConduitSystem.Update();
		this.liquidConduitSystem.Update();
		this.solidConduitSystem.Update();
		this.circuitManager.RenderEveryTick(deltaTime);
		this.logicCircuitManager.RenderEveryTick(deltaTime);
		this.solidConduitFlow.RenderEveryTick(deltaTime);
		Pathfinding.Instance.RenderEveryTick();
		Singleton<CellChangeMonitor>.Instance.RenderEveryTick();
		this.SimEveryTick(deltaTime);
	}

	// Token: 0x060034F1 RID: 13553 RVA: 0x0011E370 File Offset: 0x0011C570
	private void SimEveryTick(float dt)
	{
		dt = Mathf.Min(dt, 0.2f);
		this.simDt += dt;
		if (this.simDt >= 0.016666668f)
		{
			do
			{
				this.simSubTick++;
				this.simSubTick %= 12;
				if (this.simSubTick == 0)
				{
					this.hasFirstSimTickRun = true;
					this.UnsafeSim200ms(0.2f);
				}
				if (this.hasFirstSimTickRun)
				{
					Singleton<StateMachineUpdater>.Instance.AdvanceOneSimSubTick();
				}
				this.simDt -= 0.016666668f;
			}
			while (this.simDt >= 0.016666668f);
			return;
		}
		this.UnsafeSim200ms(0f);
	}

	// Token: 0x060034F2 RID: 13554 RVA: 0x0011E41C File Offset: 0x0011C61C
	private unsafe void UnsafeSim200ms(float dt)
	{
		this.simActiveRegions.Clear();
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			if (worldContainer.IsDiscovered)
			{
				Game.SimActiveRegion simActiveRegion = new Game.SimActiveRegion();
				simActiveRegion.region = new Pair<Vector2I, Vector2I>(worldContainer.WorldOffset, worldContainer.WorldOffset + worldContainer.WorldSize);
				simActiveRegion.currentSunlightIntensity = worldContainer.currentSunlightIntensity;
				simActiveRegion.currentCosmicRadiationIntensity = worldContainer.currentCosmicIntensity;
				this.simActiveRegions.Add(simActiveRegion);
			}
		}
		global::Debug.Assert(this.simActiveRegions.Count > 0, "Cannot send a frame to the sim with zero active regions");
		SimMessages.NewGameFrame(dt, this.simActiveRegions);
		Sim.GameDataUpdate* ptr = this.StepTheSim(dt);
		if (ptr == null)
		{
			global::Debug.LogError("UNEXPECTED!");
			return;
		}
		if (ptr->numFramesProcessed <= 0)
		{
			return;
		}
		this.gameSolidInfo.AddRange(this.solidInfo);
		this.world.UpdateCellInfo(this.gameSolidInfo, this.callbackInfo, ptr->numSolidSubstanceChangeInfo, ptr->solidSubstanceChangeInfo, ptr->numLiquidChangeInfo, ptr->liquidChangeInfo);
		this.gameSolidInfo.Clear();
		this.solidInfo.Clear();
		this.callbackInfo.Clear();
		this.callbackManagerManuallyReleasedHandles.Clear();
		Pathfinding.Instance.UpdateNavGrids(false);
	}

	// Token: 0x060034F3 RID: 13555 RVA: 0x0011E584 File Offset: 0x0011C784
	private void LateUpdateComponents()
	{
		this.UpdateOverlayScreen();
	}

	// Token: 0x060034F4 RID: 13556 RVA: 0x0011E58C File Offset: 0x0011C78C
	private void OnAddBuildingCellVisualizer(BuildingCellVisualizer building_cell_visualizer)
	{
		this.lastDrawnOverlayMode = default(HashedString);
		if (PlayerController.Instance != null)
		{
			BuildTool buildTool = PlayerController.Instance.ActiveTool as BuildTool;
			if (buildTool != null && buildTool.visualizer == building_cell_visualizer.gameObject)
			{
				this.previewVisualizer = building_cell_visualizer;
			}
		}
	}

	// Token: 0x060034F5 RID: 13557 RVA: 0x0011E5E5 File Offset: 0x0011C7E5
	private void OnRemoveBuildingCellVisualizer(BuildingCellVisualizer building_cell_visualizer)
	{
		if (this.previewVisualizer == building_cell_visualizer)
		{
			this.previewVisualizer = null;
		}
	}

	// Token: 0x060034F6 RID: 13558 RVA: 0x0011E5FC File Offset: 0x0011C7FC
	private void UpdateOverlayScreen()
	{
		if (OverlayScreen.Instance == null)
		{
			return;
		}
		HashedString mode = OverlayScreen.Instance.GetMode();
		if (this.previewVisualizer != null)
		{
			this.previewVisualizer.DisableIcons();
			this.previewVisualizer.DrawIcons(mode);
		}
		if (mode == this.lastDrawnOverlayMode)
		{
			return;
		}
		foreach (BuildingCellVisualizer buildingCellVisualizer in Components.BuildingCellVisualizers.Items)
		{
			buildingCellVisualizer.DisableIcons();
			buildingCellVisualizer.DrawIcons(mode);
		}
		this.lastDrawnOverlayMode = mode;
	}

	// Token: 0x060034F7 RID: 13559 RVA: 0x0011E6AC File Offset: 0x0011C8AC
	public void ForceOverlayUpdate(bool clearLastMode = false)
	{
		this.previousOverlayMode = OverlayModes.None.ID;
		if (clearLastMode)
		{
			this.lastDrawnOverlayMode = OverlayModes.None.ID;
		}
	}

	// Token: 0x060034F8 RID: 13560 RVA: 0x0011E6C8 File Offset: 0x0011C8C8
	private void LateUpdate()
	{
		if (Time.timeScale == 0f && !this.IsPaused)
		{
			this.IsPaused = true;
			base.Trigger(-1788536802, this.IsPaused);
		}
		else if (Time.timeScale != 0f && this.IsPaused)
		{
			this.IsPaused = false;
			base.Trigger(-1788536802, this.IsPaused);
		}
		if (Input.GetMouseButton(0))
		{
			this.VisualTunerElement = null;
			int mouseCell = DebugHandler.GetMouseCell();
			if (Grid.IsValidCell(mouseCell))
			{
				Element element = Grid.Element[mouseCell];
				this.VisualTunerElement = element;
			}
		}
		this.gasConduitSystem.Update();
		this.liquidConduitSystem.Update();
		this.solidConduitSystem.Update();
		HashedString mode = SimDebugView.Instance.GetMode();
		if (mode != this.previousOverlayMode)
		{
			this.previousOverlayMode = mode;
			if (mode == OverlayModes.LiquidConduits.ID)
			{
				this.liquidFlowVisualizer.ColourizePipeContents(true, true);
				this.gasFlowVisualizer.ColourizePipeContents(false, true);
				this.solidFlowVisualizer.ColourizePipeContents(false, true);
			}
			else if (mode == OverlayModes.GasConduits.ID)
			{
				this.liquidFlowVisualizer.ColourizePipeContents(false, true);
				this.gasFlowVisualizer.ColourizePipeContents(true, true);
				this.solidFlowVisualizer.ColourizePipeContents(false, true);
			}
			else if (mode == OverlayModes.SolidConveyor.ID)
			{
				this.liquidFlowVisualizer.ColourizePipeContents(false, true);
				this.gasFlowVisualizer.ColourizePipeContents(false, true);
				this.solidFlowVisualizer.ColourizePipeContents(true, true);
			}
			else
			{
				this.liquidFlowVisualizer.ColourizePipeContents(false, false);
				this.gasFlowVisualizer.ColourizePipeContents(false, false);
				this.solidFlowVisualizer.ColourizePipeContents(false, false);
			}
		}
		this.gasFlowVisualizer.Render(this.gasFlowPos.z, 0, this.gasConduitFlow.ContinuousLerpPercent, mode == OverlayModes.GasConduits.ID && this.gasConduitFlow.DiscreteLerpPercent != this.previousGasConduitFlowDiscreteLerpPercent);
		this.liquidFlowVisualizer.Render(this.liquidFlowPos.z, 0, this.liquidConduitFlow.ContinuousLerpPercent, mode == OverlayModes.LiquidConduits.ID && this.liquidConduitFlow.DiscreteLerpPercent != this.previousLiquidConduitFlowDiscreteLerpPercent);
		this.solidFlowVisualizer.Render(this.solidFlowPos.z, 0, this.solidConduitFlow.ContinuousLerpPercent, mode == OverlayModes.SolidConveyor.ID && this.solidConduitFlow.DiscreteLerpPercent != this.previousSolidConduitFlowDiscreteLerpPercent);
		this.previousGasConduitFlowDiscreteLerpPercent = ((mode == OverlayModes.GasConduits.ID) ? this.gasConduitFlow.DiscreteLerpPercent : (-1f));
		this.previousLiquidConduitFlowDiscreteLerpPercent = ((mode == OverlayModes.LiquidConduits.ID) ? this.liquidConduitFlow.DiscreteLerpPercent : (-1f));
		this.previousSolidConduitFlowDiscreteLerpPercent = ((mode == OverlayModes.SolidConveyor.ID) ? this.solidConduitFlow.DiscreteLerpPercent : (-1f));
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.transform.GetPosition().z));
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.GetPosition().z));
		Shader.SetGlobalVector("_WsToCs", new Vector4(vector.x / (float)Grid.WidthInCells, vector.y / (float)Grid.HeightInCells, (vector2.x - vector.x) / (float)Grid.WidthInCells, (vector2.y - vector.y) / (float)Grid.HeightInCells));
		WorldContainer activeWorld = ClusterManager.Instance.activeWorld;
		Vector2I worldOffset = activeWorld.WorldOffset;
		Vector2I worldSize = activeWorld.WorldSize;
		Vector4 vector3 = new Vector4((vector.x - (float)worldOffset.x) / (float)worldSize.x, (vector.y - (float)worldOffset.y) / (float)worldSize.y, (vector2.x - vector.x) / (float)worldSize.x, (vector2.y - vector.y) / (float)worldSize.y);
		Shader.SetGlobalVector("_WsToCcs", vector3);
		if (this.drawStatusItems)
		{
			this.statusItemRenderer.RenderEveryTick();
			this.prioritizableRenderer.RenderEveryTick();
		}
		this.LateUpdateComponents();
		Singleton<StateMachineUpdater>.Instance.Render(Time.unscaledDeltaTime);
		Singleton<StateMachineUpdater>.Instance.RenderEveryTick(Time.unscaledDeltaTime);
		if (SelectTool.Instance != null && SelectTool.Instance.selected != null)
		{
			Navigator component = SelectTool.Instance.selected.GetComponent<Navigator>();
			if (component != null)
			{
				component.DrawPath();
			}
		}
		KFMOD.RenderEveryTick(Time.deltaTime);
		if (GenericGameSettings.instance.performanceCapture.waitTime != 0f)
		{
			this.UpdatePerformanceCapture();
		}
	}

	// Token: 0x060034F9 RID: 13561 RVA: 0x0011EB9C File Offset: 0x0011CD9C
	private void UpdatePerformanceCapture()
	{
		if (this.IsPaused && SpeedControlScreen.Instance != null)
		{
			SpeedControlScreen.Instance.Unpause(true);
		}
		if (Time.timeSinceLevelLoad < GenericGameSettings.instance.performanceCapture.waitTime)
		{
			return;
		}
		uint num = 544519U;
		string text = System.DateTime.Now.ToShortDateString();
		string text2 = System.DateTime.Now.ToShortTimeString();
		string fileName = Path.GetFileName(GenericGameSettings.instance.performanceCapture.saveGame);
		string text3 = "Version,Date,Time,SaveGame";
		string text4 = string.Format("{0},{1},{2},{3}", new object[] { num, text, text2, fileName });
		float num2 = 0.1f;
		if (GenericGameSettings.instance.performanceCapture.gcStats)
		{
			global::Debug.Log("Begin GC profiling...");
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			GC.Collect();
			num2 = Time.realtimeSinceStartup - realtimeSinceStartup;
			global::Debug.Log("\tGC.Collect() took " + num2.ToString() + " seconds");
			MemorySnapshot memorySnapshot = new MemorySnapshot();
			string text5 = "{0},{1},{2},{3}";
			string text6 = "./memory/GCTypeMetrics.csv";
			if (!File.Exists(text6))
			{
				using (StreamWriter streamWriter = new StreamWriter(text6))
				{
					streamWriter.WriteLine(string.Format(text5, new object[] { text3, "Type", "Instances", "References" }));
				}
			}
			using (StreamWriter streamWriter2 = new StreamWriter(text6, true))
			{
				foreach (MemorySnapshot.TypeData typeData in memorySnapshot.types.Values)
				{
					streamWriter2.WriteLine(string.Format(text5, new object[]
					{
						text4,
						"\"" + typeData.type.ToString() + "\"",
						typeData.instanceCount,
						typeData.refCount
					}));
				}
			}
			global::Debug.Log("...end GC profiling");
		}
		float fps = Global.Instance.GetComponent<PerformanceMonitor>().FPS;
		Directory.CreateDirectory("./memory");
		string text7 = "{0},{1},{2}";
		string text8 = "./memory/GeneralMetrics.csv";
		if (!File.Exists(text8))
		{
			using (StreamWriter streamWriter3 = new StreamWriter(text8))
			{
				streamWriter3.WriteLine(string.Format(text7, text3, "GCDuration", "FPS"));
			}
		}
		using (StreamWriter streamWriter4 = new StreamWriter(text8, true))
		{
			streamWriter4.WriteLine(string.Format(text7, text4, num2, fps));
		}
		GenericGameSettings.instance.performanceCapture.waitTime = 0f;
		App.Quit();
	}

	// Token: 0x060034FA RID: 13562 RVA: 0x0011EEA4 File Offset: 0x0011D0A4
	public void Reset(GameSpawnData gsd, Vector2I world_offset)
	{
		using (new KProfiler.Region("World.Reset", null))
		{
			if (gsd != null)
			{
				foreach (KeyValuePair<Vector2I, bool> keyValuePair in gsd.preventFoWReveal)
				{
					if (keyValuePair.Value)
					{
						Vector2I vector2I = new Vector2I(keyValuePair.Key.X + world_offset.X, keyValuePair.Key.Y + world_offset.Y);
						Grid.PreventFogOfWarReveal[Grid.PosToCell(vector2I)] = keyValuePair.Value;
					}
				}
			}
		}
	}

	// Token: 0x060034FB RID: 13563 RVA: 0x0011EF7C File Offset: 0x0011D17C
	private void OnApplicationQuit()
	{
		Game.quitting = true;
		Sim.Shutdown();
		AudioMixer.Destroy();
		if (this.screenMgr != null && this.screenMgr.gameObject != null)
		{
			UnityEngine.Object.Destroy(this.screenMgr.gameObject);
		}
		Console.WriteLine("Game.OnApplicationQuit()");
	}

	// Token: 0x060034FC RID: 13564 RVA: 0x0011EFD4 File Offset: 0x0011D1D4
	private void InitializeFXSpawners()
	{
		for (int i = 0; i < this.fxSpawnData.Length; i++)
		{
			int fx_idx = i;
			this.fxSpawnData[fx_idx].fxPrefab.SetActive(false);
			ushort fx_mask = (ushort)(1 << fx_idx);
			Action<SpawnFXHashes, GameObject> destroyer = delegate(SpawnFXHashes fxid, GameObject go)
			{
				if (!Game.IsQuitting())
				{
					int num = Grid.PosToCell(go);
					ushort[] array = this.activeFX;
					int num2 = num;
					array[num2] &= ~fx_mask;
					go.GetComponent<KAnimControllerBase>().enabled = false;
					this.fxPools[(int)fxid].ReleaseInstance(go);
				}
			};
			Func<GameObject> func = delegate
			{
				GameObject gameObject = GameUtil.KInstantiate(this.fxSpawnData[fx_idx].fxPrefab, Grid.SceneLayer.Front, null, 0);
				KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
				component.enabled = false;
				gameObject.SetActive(true);
				component.onDestroySelf = delegate(GameObject go)
				{
					destroyer(this.fxSpawnData[fx_idx].id, go);
				};
				return gameObject;
			};
			GameObjectPool pool = new GameObjectPool(func, this.fxSpawnData[fx_idx].initialCount);
			this.fxPools[(int)this.fxSpawnData[fx_idx].id] = pool;
			this.fxSpawner[(int)this.fxSpawnData[fx_idx].id] = delegate(Vector3 pos, float rotation)
			{
				GameScheduler.Instance.Schedule("SpawnFX", 0f, delegate(object obj)
				{
					int num3 = Grid.PosToCell(pos);
					if ((this.activeFX[num3] & fx_mask) == 0)
					{
						ushort[] array2 = this.activeFX;
						int num4 = num3;
						array2[num4] |= fx_mask;
						GameObject instance = pool.GetInstance();
						Game.SpawnPoolData spawnPoolData = this.fxSpawnData[fx_idx];
						Quaternion quaternion = Quaternion.identity;
						bool flag = false;
						string text = spawnPoolData.initialAnim;
						Game.SpawnRotationConfig rotationConfig = spawnPoolData.rotationConfig;
						if (rotationConfig != Game.SpawnRotationConfig.Normal)
						{
							if (rotationConfig == Game.SpawnRotationConfig.StringName)
							{
								int num5 = (int)(rotation / 90f);
								if (num5 < 0)
								{
									num5 += spawnPoolData.rotationData.Length;
								}
								text = spawnPoolData.rotationData[num5].animName;
								flag = spawnPoolData.rotationData[num5].flip;
							}
						}
						else
						{
							quaternion = Quaternion.Euler(0f, 0f, rotation);
						}
						pos += spawnPoolData.spawnOffset;
						Vector2 vector = UnityEngine.Random.insideUnitCircle;
						vector.x *= spawnPoolData.spawnRandomOffset.x;
						vector.y *= spawnPoolData.spawnRandomOffset.y;
						vector = quaternion * vector;
						pos.x += vector.x;
						pos.y += vector.y;
						instance.transform.SetPosition(pos);
						instance.transform.rotation = quaternion;
						KBatchedAnimController component2 = instance.GetComponent<KBatchedAnimController>();
						component2.FlipX = flag;
						component2.TintColour = spawnPoolData.colour;
						component2.Play(text, KAnim.PlayMode.Once, 1f, 0f);
						component2.enabled = true;
					}
				}, null, null);
			};
		}
	}

	// Token: 0x060034FD RID: 13565 RVA: 0x0011F0D4 File Offset: 0x0011D2D4
	public void SpawnFX(SpawnFXHashes fx_id, int cell, float rotation)
	{
		Vector3 vector = Grid.CellToPosCBC(cell, Grid.SceneLayer.Front);
		if (CameraController.Instance.IsVisiblePos(vector))
		{
			this.fxSpawner[(int)fx_id](vector, rotation);
		}
	}

	// Token: 0x060034FE RID: 13566 RVA: 0x0011F10A File Offset: 0x0011D30A
	public void SpawnFX(SpawnFXHashes fx_id, Vector3 pos, float rotation)
	{
		this.fxSpawner[(int)fx_id](pos, rotation);
	}

	// Token: 0x060034FF RID: 13567 RVA: 0x0011F11F File Offset: 0x0011D31F
	public static void SaveSettings(BinaryWriter writer)
	{
		Serializer.Serialize(new Game.Settings(Game.Instance), writer);
	}

	// Token: 0x06003500 RID: 13568 RVA: 0x0011F134 File Offset: 0x0011D334
	public static void LoadSettings(Deserializer deserializer)
	{
		Game.Settings settings = new Game.Settings();
		deserializer.Deserialize(settings);
		KPlayerPrefs.SetInt(Game.NextUniqueIDKey, settings.nextUniqueID);
		KleiMetrics.SetGameID(settings.gameID);
	}

	// Token: 0x06003501 RID: 13569 RVA: 0x0011F16C File Offset: 0x0011D36C
	public void Save(BinaryWriter writer)
	{
		Game.GameSaveData gameSaveData = new Game.GameSaveData();
		gameSaveData.gasConduitFlow = this.gasConduitFlow;
		gameSaveData.liquidConduitFlow = this.liquidConduitFlow;
		gameSaveData.fallingWater = this.world.GetComponent<FallingWater>();
		gameSaveData.unstableGround = this.world.GetComponent<UnstableGroundManager>();
		gameSaveData.worldDetail = SaveLoader.Instance.clusterDetailSave;
		gameSaveData.debugWasUsed = this.debugWasUsed;
		gameSaveData.customGameSettings = CustomGameSettings.Instance;
		gameSaveData.storySetings = StoryManager.Instance;
		gameSaveData.autoPrioritizeRoles = this.autoPrioritizeRoles;
		gameSaveData.advancedPersonalPriorities = this.advancedPersonalPriorities;
		gameSaveData.savedInfo = this.savedInfo;
		global::Debug.Assert(gameSaveData.worldDetail != null, "World detail null");
		gameSaveData.dateGenerated = this.dateGenerated;
		if (!this.changelistsPlayedOn.Contains(544519U))
		{
			this.changelistsPlayedOn.Add(544519U);
		}
		gameSaveData.changelistsPlayedOn = this.changelistsPlayedOn;
		if (this.OnSave != null)
		{
			this.OnSave(gameSaveData);
		}
		Serializer.Serialize(gameSaveData, writer);
	}

	// Token: 0x06003502 RID: 13570 RVA: 0x0011F278 File Offset: 0x0011D478
	public void Load(Deserializer deserializer)
	{
		Game.GameSaveData gameSaveData = new Game.GameSaveData();
		gameSaveData.gasConduitFlow = this.gasConduitFlow;
		gameSaveData.liquidConduitFlow = this.liquidConduitFlow;
		gameSaveData.fallingWater = this.world.GetComponent<FallingWater>();
		gameSaveData.unstableGround = this.world.GetComponent<UnstableGroundManager>();
		gameSaveData.worldDetail = new WorldDetailSave();
		gameSaveData.customGameSettings = CustomGameSettings.Instance;
		gameSaveData.storySetings = StoryManager.Instance;
		deserializer.Deserialize(gameSaveData);
		this.gasConduitFlow = gameSaveData.gasConduitFlow;
		this.liquidConduitFlow = gameSaveData.liquidConduitFlow;
		this.debugWasUsed = gameSaveData.debugWasUsed;
		this.autoPrioritizeRoles = gameSaveData.autoPrioritizeRoles;
		this.advancedPersonalPriorities = gameSaveData.advancedPersonalPriorities;
		this.dateGenerated = gameSaveData.dateGenerated;
		this.changelistsPlayedOn = gameSaveData.changelistsPlayedOn ?? new List<uint>();
		if (gameSaveData.dateGenerated.IsNullOrWhiteSpace())
		{
			this.dateGenerated = "Before U41 (Feb 2022)";
		}
		DebugUtil.LogArgs(new object[] { "SAVEINFO" });
		DebugUtil.LogArgs(new object[] { " - Generated: " + this.dateGenerated });
		DebugUtil.LogArgs(new object[] { " - Played on: " + string.Join<uint>(", ", this.changelistsPlayedOn) });
		DebugUtil.LogArgs(new object[] { " - Debug was used: " + Game.Instance.debugWasUsed.ToString() });
		this.savedInfo = gameSaveData.savedInfo;
		this.savedInfo.InitializeEmptyVariables();
		CustomGameSettings.Instance.Print();
		KCrashReporter.debugWasUsed = this.debugWasUsed;
		SaveLoader.Instance.SetWorldDetail(gameSaveData.worldDetail);
		if (this.OnLoad != null)
		{
			this.OnLoad(gameSaveData);
		}
	}

	// Token: 0x06003503 RID: 13571 RVA: 0x0011F433 File Offset: 0x0011D633
	public void SetAutoSaveCallbacks(Game.SavingPreCB activatePreCB, Game.SavingActiveCB activateActiveCB, Game.SavingPostCB activatePostCB)
	{
		this.activatePreCB = activatePreCB;
		this.activateActiveCB = activateActiveCB;
		this.activatePostCB = activatePostCB;
	}

	// Token: 0x06003504 RID: 13572 RVA: 0x0011F44A File Offset: 0x0011D64A
	public void StartDelayedInitialSave()
	{
		base.StartCoroutine(this.DelayedInitialSave());
	}

	// Token: 0x06003505 RID: 13573 RVA: 0x0011F459 File Offset: 0x0011D659
	private IEnumerator DelayedInitialSave()
	{
		int num;
		for (int i = 0; i < 1; i = num)
		{
			yield return null;
			num = i + 1;
		}
		if (GenericGameSettings.instance.devAutoWorldGenActive)
		{
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				worldContainer.SetDiscovered(true);
			}
			SaveGame.Instance.worldGenSpawner.SpawnEverything();
			SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>().DEBUG_REVEAL_ENTIRE_MAP();
			if (CameraController.Instance != null)
			{
				CameraController.Instance.EnableFreeCamera(true);
			}
			for (int num2 = 0; num2 != Grid.WidthInCells * Grid.HeightInCells; num2++)
			{
				Grid.Reveal(num2, byte.MaxValue, false);
			}
			GenericGameSettings.instance.devAutoWorldGenActive = false;
		}
		SaveLoader.Instance.InitialSave();
		yield break;
	}

	// Token: 0x06003506 RID: 13574 RVA: 0x0011F464 File Offset: 0x0011D664
	public void StartDelayedSave(string filename, bool isAutoSave = false, bool updateSavePointer = true)
	{
		if (this.activatePreCB != null)
		{
			this.activatePreCB(delegate
			{
				this.StartCoroutine(this.DelayedSave(filename, isAutoSave, updateSavePointer));
			});
			return;
		}
		base.StartCoroutine(this.DelayedSave(filename, isAutoSave, updateSavePointer));
	}

	// Token: 0x06003507 RID: 13575 RVA: 0x0011F4D2 File Offset: 0x0011D6D2
	private IEnumerator DelayedSave(string filename, bool isAutoSave, bool updateSavePointer)
	{
		while (PlayerController.Instance.IsDragging())
		{
			yield return null;
		}
		PlayerController.Instance.CancelDragging();
		PlayerController.Instance.AllowDragging(false);
		int num;
		for (int i = 0; i < 1; i = num)
		{
			yield return null;
			num = i + 1;
		}
		if (this.activateActiveCB != null)
		{
			this.activateActiveCB();
			for (int i = 0; i < 1; i = num)
			{
				yield return null;
				num = i + 1;
			}
		}
		SaveLoader.Instance.Save(filename, isAutoSave, updateSavePointer);
		if (this.activatePostCB != null)
		{
			this.activatePostCB();
		}
		for (int i = 0; i < 5; i = num)
		{
			yield return null;
			num = i + 1;
		}
		PlayerController.Instance.AllowDragging(true);
		yield break;
	}

	// Token: 0x06003508 RID: 13576 RVA: 0x0011F4F6 File Offset: 0x0011D6F6
	public void StartDelayed(int tick_delay, System.Action action)
	{
		base.StartCoroutine(this.DelayedExecutor(tick_delay, action));
	}

	// Token: 0x06003509 RID: 13577 RVA: 0x0011F507 File Offset: 0x0011D707
	private IEnumerator DelayedExecutor(int tick_delay, System.Action action)
	{
		int num;
		for (int i = 0; i < tick_delay; i = num)
		{
			yield return null;
			num = i + 1;
		}
		action();
		yield break;
	}

	// Token: 0x0600350A RID: 13578 RVA: 0x0011F520 File Offset: 0x0011D720
	private void LoadEventHashes()
	{
		foreach (object obj in Enum.GetValues(typeof(GameHashes)))
		{
			GameHashes gameHashes = (GameHashes)obj;
			HashCache.Get().Add((int)gameHashes, gameHashes.ToString());
		}
		foreach (object obj2 in Enum.GetValues(typeof(UtilHashes)))
		{
			UtilHashes utilHashes = (UtilHashes)obj2;
			HashCache.Get().Add((int)utilHashes, utilHashes.ToString());
		}
		foreach (object obj3 in Enum.GetValues(typeof(UIHashes)))
		{
			UIHashes uihashes = (UIHashes)obj3;
			HashCache.Get().Add((int)uihashes, uihashes.ToString());
		}
	}

	// Token: 0x0600350B RID: 13579 RVA: 0x0011F65C File Offset: 0x0011D85C
	public void StopFE()
	{
		if (SteamUGCService.Instance)
		{
			SteamUGCService.Instance.enabled = false;
		}
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndSnapshot, STOP_MODE.ALLOWFADEOUT);
		if (MusicManager.instance.SongIsPlaying("Music_FrontEnd"))
		{
			MusicManager.instance.StopSong("Music_FrontEnd", true, STOP_MODE.ALLOWFADEOUT);
		}
		MainMenu.Instance.StopMainMenuMusic();
	}

	// Token: 0x0600350C RID: 13580 RVA: 0x0011F6C4 File Offset: 0x0011D8C4
	public void StartBE()
	{
		Resources.UnloadUnusedAssets();
		if (TimeOfDay.Instance != null && !MusicManager.instance.SongIsPlaying("Stinger_Loop_Night") && TimeOfDay.Instance.GetCurrentTimeRegion() == TimeOfDay.TimeRegion.Night)
		{
			MusicManager.instance.PlaySong("Stinger_Loop_Night", false);
			MusicManager.instance.SetSongParameter("Stinger_Loop_Night", "Music_PlayStinger", 0f, true);
		}
		AudioMixer.instance.Reset();
		AudioMixer.instance.StartPersistentSnapshots();
		if (MusicManager.instance.ShouldPlayDynamicMusicLoadedGame())
		{
			MusicManager.instance.PlayDynamicMusic();
		}
	}

	// Token: 0x0600350D RID: 13581 RVA: 0x0011F758 File Offset: 0x0011D958
	public void StopBE()
	{
		if (SteamUGCService.Instance)
		{
			SteamUGCService.Instance.enabled = true;
		}
		LoopingSoundManager loopingSoundManager = LoopingSoundManager.Get();
		if (loopingSoundManager != null)
		{
			loopingSoundManager.StopAllSounds();
		}
		MusicManager.instance.KillAllSongs(STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.StopPersistentSnapshots();
		foreach (List<SaveLoadRoot> list in SaveLoader.Instance.saveManager.GetLists().Values)
		{
			foreach (SaveLoadRoot saveLoadRoot in list)
			{
				if (saveLoadRoot.gameObject != null)
				{
					global::Util.KDestroyGameObject(saveLoadRoot.gameObject);
				}
			}
		}
		base.GetComponent<EntombedItemVisualizer>().Clear();
		SimTemperatureTransfer.ClearInstanceMap();
		StructureTemperatureComponents.ClearInstanceMap();
		ElementConsumer.ClearInstanceMap();
		KComponentSpawn.instance.comps.Clear();
		KInputHandler.Remove(Global.GetInputManager().GetDefaultController(), this.cameraController);
		KInputHandler.Remove(Global.GetInputManager().GetDefaultController(), this.playerController);
		Sim.Shutdown();
		SimAndRenderScheduler.instance.Reset();
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x0600350E RID: 13582 RVA: 0x0011F8A8 File Offset: 0x0011DAA8
	public void SetStatusItemOffset(Transform transform, Vector3 offset)
	{
		this.statusItemRenderer.SetOffset(transform, offset);
	}

	// Token: 0x0600350F RID: 13583 RVA: 0x0011F8B7 File Offset: 0x0011DAB7
	public void AddStatusItem(Transform transform, StatusItem status_item)
	{
		this.statusItemRenderer.Add(transform, status_item);
	}

	// Token: 0x06003510 RID: 13584 RVA: 0x0011F8C6 File Offset: 0x0011DAC6
	public void RemoveStatusItem(Transform transform, StatusItem status_item)
	{
		this.statusItemRenderer.Remove(transform, status_item);
	}

	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x06003511 RID: 13585 RVA: 0x0011F8D5 File Offset: 0x0011DAD5
	public float LastTimeWorkStarted
	{
		get
		{
			return this.lastTimeWorkStarted;
		}
	}

	// Token: 0x06003512 RID: 13586 RVA: 0x0011F8DD File Offset: 0x0011DADD
	public void StartedWork()
	{
		this.lastTimeWorkStarted = Time.time;
	}

	// Token: 0x06003513 RID: 13587 RVA: 0x0011F8EA File Offset: 0x0011DAEA
	private void SpawnOxygenBubbles(Vector3 position, float angle)
	{
	}

	// Token: 0x06003514 RID: 13588 RVA: 0x0011F8EC File Offset: 0x0011DAEC
	public void ManualReleaseHandle(HandleVector<Game.CallbackInfo>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return;
		}
		this.callbackManagerManuallyReleasedHandles.Add(handle.index);
		this.callbackManager.Release(handle);
	}

	// Token: 0x06003515 RID: 13589 RVA: 0x0011F917 File Offset: 0x0011DB17
	private bool IsManuallyReleasedHandle(HandleVector<Game.CallbackInfo>.Handle handle)
	{
		return !this.callbackManager.IsVersionValid(handle) && this.callbackManagerManuallyReleasedHandles.Contains(handle.index);
	}

	// Token: 0x06003516 RID: 13590 RVA: 0x0011F93E File Offset: 0x0011DB3E
	[ContextMenu("Print")]
	private void Print()
	{
		Console.WriteLine("This is a console writeline test");
		global::Debug.Log("This is a debug log test");
	}

	// Token: 0x06003517 RID: 13591 RVA: 0x0011F954 File Offset: 0x0011DB54
	private void DestroyInstances()
	{
		KMonoBehaviour.lastGameObject = null;
		KMonoBehaviour.lastObj = null;
		Db.Get().ResetProblematicDbs();
		GridSettings.ClearGrid();
		StateMachineManager.ResetParameters();
		ChoreTable.Instance.ResetParameters();
		BubbleManager.DestroyInstance();
		AmbientSoundManager.Destroy();
		AutoDisinfectableManager.DestroyInstance();
		BuildMenu.DestroyInstance();
		CancelTool.DestroyInstance();
		ClearTool.DestroyInstance();
		ChoreGroupManager.DestroyInstance();
		CO2Manager.DestroyInstance();
		ConsumerManager.DestroyInstance();
		CopySettingsTool.DestroyInstance();
		global::DateTime.DestroyInstance();
		DebugBaseTemplateButton.DestroyInstance();
		DebugPaintElementScreen.DestroyInstance();
		DetailsScreen.DestroyInstance();
		DietManager.DestroyInstance();
		DebugText.DestroyInstance();
		FactionManager.DestroyInstance();
		EmptyPipeTool.DestroyInstance();
		FetchListStatusItemUpdater.DestroyInstance();
		FishOvercrowingManager.DestroyInstance();
		FallingWater.DestroyInstance();
		GridCompositor.DestroyInstance();
		Infrared.DestroyInstance();
		KPrefabIDTracker.DestroyInstance();
		ManagementMenu.DestroyInstance();
		ClusterMapScreen.DestroyInstance();
		Messenger.DestroyInstance();
		LoopingSoundManager.DestroyInstance();
		MeterScreen.DestroyInstance();
		MinionGroupProber.DestroyInstance();
		NavPathDrawer.DestroyInstance();
		MinionIdentity.DestroyStatics();
		PathFinder.DestroyStatics();
		Pathfinding.DestroyInstance();
		PrebuildTool.DestroyInstance();
		PrioritizeTool.DestroyInstance();
		SelectTool.DestroyInstance();
		PopFXManager.DestroyInstance();
		ProgressBarsConfig.DestroyInstance();
		PropertyTextures.DestroyInstance();
		RationTracker.DestroyInstance();
		ReportManager.DestroyInstance();
		Research.DestroyInstance();
		RootMenu.DestroyInstance();
		SaveLoader.DestroyInstance();
		Scenario.DestroyInstance();
		SimDebugView.DestroyInstance();
		SpriteSheetAnimManager.DestroyInstance();
		ScheduleManager.DestroyInstance();
		Sounds.DestroyInstance();
		ToolMenu.DestroyInstance();
		WorldDamage.DestroyInstance();
		WaterCubes.DestroyInstance();
		WireBuildTool.DestroyInstance();
		VisibilityTester.DestroyInstance();
		Traces.DestroyInstance();
		TopLeftControlScreen.DestroyInstance();
		UtilityBuildTool.DestroyInstance();
		ReportScreen.DestroyInstance();
		ChorePreconditions.DestroyInstance();
		SandboxBrushTool.DestroyInstance();
		SandboxHeatTool.DestroyInstance();
		SandboxStressTool.DestroyInstance();
		SandboxCritterTool.DestroyInstance();
		SandboxClearFloorTool.DestroyInstance();
		GameScreenManager.DestroyInstance();
		GameScheduler.DestroyInstance();
		NavigationReservations.DestroyInstance();
		Tutorial.DestroyInstance();
		CameraController.DestroyInstance();
		CellEventLogger.DestroyInstance();
		GameFlowManager.DestroyInstance();
		Immigration.DestroyInstance();
		BuildTool.DestroyInstance();
		DebugTool.DestroyInstance();
		DeconstructTool.DestroyInstance();
		DisconnectTool.DestroyInstance();
		DigTool.DestroyInstance();
		DisinfectTool.DestroyInstance();
		HarvestTool.DestroyInstance();
		MopTool.DestroyInstance();
		MoveToLocationTool.DestroyInstance();
		PlaceTool.DestroyInstance();
		SpacecraftManager.DestroyInstance();
		GameplayEventManager.DestroyInstance();
		BuildingInventory.DestroyInstance();
		PlantSubSpeciesCatalog.DestroyInstance();
		SandboxDestroyerTool.DestroyInstance();
		SandboxFOWTool.DestroyInstance();
		SandboxFloodTool.DestroyInstance();
		SandboxSprinkleTool.DestroyInstance();
		StampTool.DestroyInstance();
		OnDemandUpdater.DestroyInstance();
		HoverTextScreen.DestroyInstance();
		ImmigrantScreen.DestroyInstance();
		OverlayMenu.DestroyInstance();
		NameDisplayScreen.DestroyInstance();
		PlanScreen.DestroyInstance();
		ResourceCategoryScreen.DestroyInstance();
		ResourceRemainingDisplayScreen.DestroyInstance();
		SandboxToolParameterMenu.DestroyInstance();
		SpeedControlScreen.DestroyInstance();
		Vignette.DestroyInstance();
		PlayerController.DestroyInstance();
		NotificationScreen.DestroyInstance();
		BuildingCellVisualizerResources.DestroyInstance();
		PauseScreen.DestroyInstance();
		SaveLoadRoot.DestroyStatics();
		KTime.DestroyInstance();
		DemoTimer.DestroyInstance();
		UIScheduler.DestroyInstance();
		SaveGame.DestroyInstance();
		GameClock.DestroyInstance();
		TimeOfDay.DestroyInstance();
		DeserializeWarnings.DestroyInstance();
		UISounds.DestroyInstance();
		RenderTextureDestroyer.DestroyInstance();
		HoverTextHelper.DestroyStatics();
		LoadScreen.DestroyInstance();
		LoadingOverlay.DestroyInstance();
		SimAndRenderScheduler.DestroyInstance();
		Singleton<CellChangeMonitor>.DestroyInstance();
		Singleton<StateMachineManager>.Instance.Clear();
		Singleton<StateMachineUpdater>.Instance.Clear();
		UpdateObjectCountParameter.Clear();
		MaterialSelectionPanel.ClearStatics();
		StarmapScreen.DestroyInstance();
		ClusterNameDisplayScreen.DestroyInstance();
		ClusterManager.DestroyInstance();
		ClusterGrid.DestroyInstance();
		PathFinderQueries.Reset();
		KBatchedAnimUpdater instance = Singleton<KBatchedAnimUpdater>.Instance;
		if (instance != null)
		{
			instance.InitializeGrid();
		}
		GlobalChoreProvider.DestroyInstance();
		WorldSelector.DestroyInstance();
		ColonyDiagnosticUtility.DestroyInstance();
		DiscoveredResources.DestroyInstance();
		ClusterMapSelectTool.DestroyInstance();
		StoryManager.DestroyInstance();
		Game.Instance = null;
		Grid.OnReveal = null;
		this.VisualTunerElement = null;
		Assets.ClearOnAddPrefab();
		KMonoBehaviour.lastGameObject = null;
		KMonoBehaviour.lastObj = null;
		(KComponentSpawn.instance.comps as GameComps).Clear();
	}

	// Token: 0x040020B8 RID: 8376
	private static readonly string NextUniqueIDKey = "NextUniqueID";

	// Token: 0x040020B9 RID: 8377
	public static string clusterId = null;

	// Token: 0x040020BA RID: 8378
	private PlayerController playerController;

	// Token: 0x040020BB RID: 8379
	private CameraController cameraController;

	// Token: 0x040020BC RID: 8380
	public Action<Game.GameSaveData> OnSave;

	// Token: 0x040020BD RID: 8381
	public Action<Game.GameSaveData> OnLoad;

	// Token: 0x040020BE RID: 8382
	[NonSerialized]
	public bool baseAlreadyCreated;

	// Token: 0x040020BF RID: 8383
	[NonSerialized]
	public bool autoPrioritizeRoles;

	// Token: 0x040020C0 RID: 8384
	[NonSerialized]
	public bool advancedPersonalPriorities;

	// Token: 0x040020C1 RID: 8385
	public Game.SavedInfo savedInfo;

	// Token: 0x040020C2 RID: 8386
	public static bool quitting = false;

	// Token: 0x040020C4 RID: 8388
	public AssignmentManager assignmentManager;

	// Token: 0x040020C5 RID: 8389
	public GameObject playerPrefab;

	// Token: 0x040020C6 RID: 8390
	public GameObject screenManagerPrefab;

	// Token: 0x040020C7 RID: 8391
	public GameObject cameraControllerPrefab;

	// Token: 0x040020C9 RID: 8393
	private static Camera m_CachedCamera = null;

	// Token: 0x040020CA RID: 8394
	public GameObject tempIntroScreenPrefab;

	// Token: 0x040020CB RID: 8395
	public static int BlockSelectionLayerMask;

	// Token: 0x040020CC RID: 8396
	public static int PickupableLayer;

	// Token: 0x040020CD RID: 8397
	public Element VisualTunerElement;

	// Token: 0x040020CE RID: 8398
	public float currentFallbackSunlightIntensity;

	// Token: 0x040020CF RID: 8399
	public RoomProber roomProber;

	// Token: 0x040020D0 RID: 8400
	public FetchManager fetchManager;

	// Token: 0x040020D1 RID: 8401
	public EdiblesManager ediblesManager;

	// Token: 0x040020D2 RID: 8402
	public SpacecraftManager spacecraftManager;

	// Token: 0x040020D3 RID: 8403
	public UserMenu userMenu;

	// Token: 0x040020D4 RID: 8404
	public Unlocks unlocks;

	// Token: 0x040020D5 RID: 8405
	public Timelapser timelapser;

	// Token: 0x040020D6 RID: 8406
	private bool sandboxModeActive;

	// Token: 0x040020D7 RID: 8407
	public HandleVector<Game.CallbackInfo> callbackManager = new HandleVector<Game.CallbackInfo>(256);

	// Token: 0x040020D8 RID: 8408
	public List<int> callbackManagerManuallyReleasedHandles = new List<int>();

	// Token: 0x040020D9 RID: 8409
	public Game.ComplexCallbackHandleVector<int> simComponentCallbackManager = new Game.ComplexCallbackHandleVector<int>(256);

	// Token: 0x040020DA RID: 8410
	public Game.ComplexCallbackHandleVector<Sim.MassConsumedCallback> massConsumedCallbackManager = new Game.ComplexCallbackHandleVector<Sim.MassConsumedCallback>(64);

	// Token: 0x040020DB RID: 8411
	public Game.ComplexCallbackHandleVector<Sim.MassEmittedCallback> massEmitCallbackManager = new Game.ComplexCallbackHandleVector<Sim.MassEmittedCallback>(64);

	// Token: 0x040020DC RID: 8412
	public Game.ComplexCallbackHandleVector<Sim.DiseaseConsumptionCallback> diseaseConsumptionCallbackManager = new Game.ComplexCallbackHandleVector<Sim.DiseaseConsumptionCallback>(64);

	// Token: 0x040020DD RID: 8413
	public Game.ComplexCallbackHandleVector<Sim.ConsumedRadiationCallback> radiationConsumedCallbackManager = new Game.ComplexCallbackHandleVector<Sim.ConsumedRadiationCallback>(256);

	// Token: 0x040020DE RID: 8414
	[NonSerialized]
	public Player LocalPlayer;

	// Token: 0x040020DF RID: 8415
	[SerializeField]
	public TextAsset maleNamesFile;

	// Token: 0x040020E0 RID: 8416
	[SerializeField]
	public TextAsset femaleNamesFile;

	// Token: 0x040020E1 RID: 8417
	[NonSerialized]
	public World world;

	// Token: 0x040020E2 RID: 8418
	[NonSerialized]
	public CircuitManager circuitManager;

	// Token: 0x040020E3 RID: 8419
	[NonSerialized]
	public EnergySim energySim;

	// Token: 0x040020E4 RID: 8420
	[NonSerialized]
	public LogicCircuitManager logicCircuitManager;

	// Token: 0x040020E5 RID: 8421
	private GameScreenManager screenMgr;

	// Token: 0x040020E6 RID: 8422
	public UtilityNetworkManager<FlowUtilityNetwork, Vent> gasConduitSystem;

	// Token: 0x040020E7 RID: 8423
	public UtilityNetworkManager<FlowUtilityNetwork, Vent> liquidConduitSystem;

	// Token: 0x040020E8 RID: 8424
	public UtilityNetworkManager<ElectricalUtilityNetwork, Wire> electricalConduitSystem;

	// Token: 0x040020E9 RID: 8425
	public UtilityNetworkManager<LogicCircuitNetwork, LogicWire> logicCircuitSystem;

	// Token: 0x040020EA RID: 8426
	public UtilityNetworkTubesManager travelTubeSystem;

	// Token: 0x040020EB RID: 8427
	public UtilityNetworkManager<FlowUtilityNetwork, SolidConduit> solidConduitSystem;

	// Token: 0x040020EC RID: 8428
	public ConduitFlow gasConduitFlow;

	// Token: 0x040020ED RID: 8429
	public ConduitFlow liquidConduitFlow;

	// Token: 0x040020EE RID: 8430
	public SolidConduitFlow solidConduitFlow;

	// Token: 0x040020EF RID: 8431
	public Accumulators accumulators;

	// Token: 0x040020F0 RID: 8432
	public PlantElementAbsorbers plantElementAbsorbers;

	// Token: 0x040020F1 RID: 8433
	public Game.TemperatureOverlayModes temperatureOverlayMode;

	// Token: 0x040020F2 RID: 8434
	public bool showExpandedTemperatures;

	// Token: 0x040020F3 RID: 8435
	public List<Tag> tileOverlayFilters = new List<Tag>();

	// Token: 0x040020F4 RID: 8436
	public bool showGasConduitDisease;

	// Token: 0x040020F5 RID: 8437
	public bool showLiquidConduitDisease;

	// Token: 0x040020F6 RID: 8438
	public ConduitFlowVisualizer gasFlowVisualizer;

	// Token: 0x040020F7 RID: 8439
	public ConduitFlowVisualizer liquidFlowVisualizer;

	// Token: 0x040020F8 RID: 8440
	public SolidConduitFlowVisualizer solidFlowVisualizer;

	// Token: 0x040020F9 RID: 8441
	public ConduitTemperatureManager conduitTemperatureManager;

	// Token: 0x040020FA RID: 8442
	public ConduitDiseaseManager conduitDiseaseManager;

	// Token: 0x040020FB RID: 8443
	public MingleCellTracker mingleCellTracker;

	// Token: 0x040020FC RID: 8444
	private int simSubTick;

	// Token: 0x040020FD RID: 8445
	private bool hasFirstSimTickRun;

	// Token: 0x040020FE RID: 8446
	private float simDt;

	// Token: 0x040020FF RID: 8447
	public string dateGenerated;

	// Token: 0x04002100 RID: 8448
	public List<uint> changelistsPlayedOn;

	// Token: 0x04002101 RID: 8449
	[SerializeField]
	public Game.ConduitVisInfo liquidConduitVisInfo;

	// Token: 0x04002102 RID: 8450
	[SerializeField]
	public Game.ConduitVisInfo gasConduitVisInfo;

	// Token: 0x04002103 RID: 8451
	[SerializeField]
	public Game.ConduitVisInfo solidConduitVisInfo;

	// Token: 0x04002104 RID: 8452
	[SerializeField]
	private Material liquidFlowMaterial;

	// Token: 0x04002105 RID: 8453
	[SerializeField]
	private Material gasFlowMaterial;

	// Token: 0x04002106 RID: 8454
	[SerializeField]
	private Color flowColour;

	// Token: 0x04002107 RID: 8455
	private Vector3 gasFlowPos;

	// Token: 0x04002108 RID: 8456
	private Vector3 liquidFlowPos;

	// Token: 0x04002109 RID: 8457
	private Vector3 solidFlowPos;

	// Token: 0x0400210A RID: 8458
	public bool drawStatusItems = true;

	// Token: 0x0400210B RID: 8459
	private List<SolidInfo> solidInfo = new List<SolidInfo>();

	// Token: 0x0400210C RID: 8460
	private List<Klei.CallbackInfo> callbackInfo = new List<Klei.CallbackInfo>();

	// Token: 0x0400210D RID: 8461
	private List<SolidInfo> gameSolidInfo = new List<SolidInfo>();

	// Token: 0x0400210E RID: 8462
	private bool IsPaused;

	// Token: 0x0400210F RID: 8463
	private HashSet<int> solidChangedFilter = new HashSet<int>();

	// Token: 0x04002110 RID: 8464
	private HashedString lastDrawnOverlayMode;

	// Token: 0x04002111 RID: 8465
	private BuildingCellVisualizer previewVisualizer;

	// Token: 0x04002114 RID: 8468
	public SafetyConditions safetyConditions = new SafetyConditions();

	// Token: 0x04002115 RID: 8469
	public SimData simData = new SimData();

	// Token: 0x04002116 RID: 8470
	[MyCmpGet]
	private GameScenePartitioner gameScenePartitioner;

	// Token: 0x04002117 RID: 8471
	private bool gameStarted;

	// Token: 0x04002118 RID: 8472
	private static readonly EventSystem.IntraObjectHandler<Game> MarkStatusItemRendererDirtyDelegate = new EventSystem.IntraObjectHandler<Game>(delegate(Game component, object data)
	{
		component.MarkStatusItemRendererDirty(data);
	});

	// Token: 0x04002119 RID: 8473
	private static readonly EventSystem.IntraObjectHandler<Game> ActiveWorldChangedDelegate = new EventSystem.IntraObjectHandler<Game>(delegate(Game component, object data)
	{
		component.ForceOverlayUpdate(true);
	});

	// Token: 0x0400211A RID: 8474
	private ushort[] activeFX;

	// Token: 0x0400211B RID: 8475
	public bool debugWasUsed;

	// Token: 0x0400211C RID: 8476
	private bool isLoading;

	// Token: 0x0400211D RID: 8477
	private List<Game.SimActiveRegion> simActiveRegions = new List<Game.SimActiveRegion>();

	// Token: 0x0400211E RID: 8478
	private HashedString previousOverlayMode = OverlayModes.None.ID;

	// Token: 0x0400211F RID: 8479
	private float previousGasConduitFlowDiscreteLerpPercent = -1f;

	// Token: 0x04002120 RID: 8480
	private float previousLiquidConduitFlowDiscreteLerpPercent = -1f;

	// Token: 0x04002121 RID: 8481
	private float previousSolidConduitFlowDiscreteLerpPercent = -1f;

	// Token: 0x04002122 RID: 8482
	[SerializeField]
	private Game.SpawnPoolData[] fxSpawnData;

	// Token: 0x04002123 RID: 8483
	private Dictionary<int, Action<Vector3, float>> fxSpawner = new Dictionary<int, Action<Vector3, float>>();

	// Token: 0x04002124 RID: 8484
	private Dictionary<int, GameObjectPool> fxPools = new Dictionary<int, GameObjectPool>();

	// Token: 0x04002125 RID: 8485
	private Game.SavingPreCB activatePreCB;

	// Token: 0x04002126 RID: 8486
	private Game.SavingActiveCB activateActiveCB;

	// Token: 0x04002127 RID: 8487
	private Game.SavingPostCB activatePostCB;

	// Token: 0x04002128 RID: 8488
	[SerializeField]
	public Game.UIColours uiColours = new Game.UIColours();

	// Token: 0x04002129 RID: 8489
	private float lastTimeWorkStarted = float.NegativeInfinity;

	// Token: 0x02001475 RID: 5237
	[Serializable]
	public struct SavedInfo
	{
		// Token: 0x06008114 RID: 33044 RVA: 0x002E0C55 File Offset: 0x002DEE55
		[OnDeserialized]
		private void OnDeserialized()
		{
			this.InitializeEmptyVariables();
		}

		// Token: 0x06008115 RID: 33045 RVA: 0x002E0C5D File Offset: 0x002DEE5D
		public void InitializeEmptyVariables()
		{
			if (this.creaturePoopAmount == null)
			{
				this.creaturePoopAmount = new Dictionary<Tag, float>();
			}
			if (this.powerCreatedbyGeneratorType == null)
			{
				this.powerCreatedbyGeneratorType = new Dictionary<Tag, float>();
			}
		}

		// Token: 0x0400636E RID: 25454
		public bool discoveredSurface;

		// Token: 0x0400636F RID: 25455
		public bool discoveredOilField;

		// Token: 0x04006370 RID: 25456
		public bool curedDisease;

		// Token: 0x04006371 RID: 25457
		public bool blockedCometWithBunkerDoor;

		// Token: 0x04006372 RID: 25458
		public Dictionary<Tag, float> creaturePoopAmount;

		// Token: 0x04006373 RID: 25459
		public Dictionary<Tag, float> powerCreatedbyGeneratorType;
	}

	// Token: 0x02001476 RID: 5238
	public struct CallbackInfo
	{
		// Token: 0x06008116 RID: 33046 RVA: 0x002E0C85 File Offset: 0x002DEE85
		public CallbackInfo(System.Action cb, bool manually_release = false)
		{
			this.cb = cb;
			this.manuallyRelease = manually_release;
		}

		// Token: 0x04006374 RID: 25460
		public System.Action cb;

		// Token: 0x04006375 RID: 25461
		public bool manuallyRelease;
	}

	// Token: 0x02001477 RID: 5239
	public struct ComplexCallbackInfo<DataType>
	{
		// Token: 0x06008117 RID: 33047 RVA: 0x002E0C95 File Offset: 0x002DEE95
		public ComplexCallbackInfo(Action<DataType, object> cb, object callback_data, string debug_info)
		{
			this.cb = cb;
			this.debugInfo = debug_info;
			this.callbackData = callback_data;
		}

		// Token: 0x04006376 RID: 25462
		public Action<DataType, object> cb;

		// Token: 0x04006377 RID: 25463
		public object callbackData;

		// Token: 0x04006378 RID: 25464
		public string debugInfo;
	}

	// Token: 0x02001478 RID: 5240
	public class ComplexCallbackHandleVector<DataType>
	{
		// Token: 0x06008118 RID: 33048 RVA: 0x002E0CAC File Offset: 0x002DEEAC
		public ComplexCallbackHandleVector(int initial_size)
		{
			this.baseMgr = new HandleVector<Game.ComplexCallbackInfo<DataType>>(initial_size);
		}

		// Token: 0x06008119 RID: 33049 RVA: 0x002E0CCB File Offset: 0x002DEECB
		public HandleVector<Game.ComplexCallbackInfo<DataType>>.Handle Add(Action<DataType, object> cb, object callback_data, string debug_info)
		{
			return this.baseMgr.Add(new Game.ComplexCallbackInfo<DataType>(cb, callback_data, debug_info));
		}

		// Token: 0x0600811A RID: 33050 RVA: 0x002E0CE0 File Offset: 0x002DEEE0
		public Game.ComplexCallbackInfo<DataType> GetItem(HandleVector<Game.ComplexCallbackInfo<DataType>>.Handle handle)
		{
			Game.ComplexCallbackInfo<DataType> item;
			try
			{
				item = this.baseMgr.GetItem(handle);
			}
			catch (Exception ex)
			{
				byte b;
				int num;
				this.baseMgr.UnpackHandleUnchecked(handle, out b, out num);
				string text = null;
				if (this.releaseInfo.TryGetValue(num, out text))
				{
					KCrashReporter.Assert(false, "Trying to get data for handle that was already released by " + text);
				}
				else
				{
					KCrashReporter.Assert(false, "Trying to get data for handle that was released ...... magically");
				}
				throw ex;
			}
			return item;
		}

		// Token: 0x0600811B RID: 33051 RVA: 0x002E0D50 File Offset: 0x002DEF50
		public Game.ComplexCallbackInfo<DataType> Release(HandleVector<Game.ComplexCallbackInfo<DataType>>.Handle handle, string release_info)
		{
			Game.ComplexCallbackInfo<DataType> complexCallbackInfo;
			try
			{
				byte b;
				int num;
				this.baseMgr.UnpackHandle(handle, out b, out num);
				this.releaseInfo[num] = release_info;
				complexCallbackInfo = this.baseMgr.Release(handle);
			}
			catch (Exception ex)
			{
				byte b;
				int num;
				this.baseMgr.UnpackHandleUnchecked(handle, out b, out num);
				string text = null;
				if (this.releaseInfo.TryGetValue(num, out text))
				{
					KCrashReporter.Assert(false, release_info + "is trying to release handle but it was already released by " + text);
				}
				else
				{
					KCrashReporter.Assert(false, release_info + "is trying to release a handle that was already released by some unknown thing");
				}
				throw ex;
			}
			return complexCallbackInfo;
		}

		// Token: 0x0600811C RID: 33052 RVA: 0x002E0DE4 File Offset: 0x002DEFE4
		public void Clear()
		{
			this.baseMgr.Clear();
		}

		// Token: 0x0600811D RID: 33053 RVA: 0x002E0DF1 File Offset: 0x002DEFF1
		public bool IsVersionValid(HandleVector<Game.ComplexCallbackInfo<DataType>>.Handle handle)
		{
			return this.baseMgr.IsVersionValid(handle);
		}

		// Token: 0x04006379 RID: 25465
		private HandleVector<Game.ComplexCallbackInfo<DataType>> baseMgr;

		// Token: 0x0400637A RID: 25466
		private Dictionary<int, string> releaseInfo = new Dictionary<int, string>();
	}

	// Token: 0x02001479 RID: 5241
	public enum TemperatureOverlayModes
	{
		// Token: 0x0400637C RID: 25468
		AbsoluteTemperature,
		// Token: 0x0400637D RID: 25469
		AdaptiveTemperature,
		// Token: 0x0400637E RID: 25470
		HeatFlow,
		// Token: 0x0400637F RID: 25471
		StateChange
	}

	// Token: 0x0200147A RID: 5242
	[Serializable]
	public class ConduitVisInfo
	{
		// Token: 0x04006380 RID: 25472
		public GameObject prefab;

		// Token: 0x04006381 RID: 25473
		[Header("Main View")]
		public Color32 tint;

		// Token: 0x04006382 RID: 25474
		public Color32 insulatedTint;

		// Token: 0x04006383 RID: 25475
		public Color32 radiantTint;

		// Token: 0x04006384 RID: 25476
		[Header("Overlay")]
		public string overlayTintName;

		// Token: 0x04006385 RID: 25477
		public string overlayInsulatedTintName;

		// Token: 0x04006386 RID: 25478
		public string overlayRadiantTintName;

		// Token: 0x04006387 RID: 25479
		public Vector2 overlayMassScaleRange = new Vector2f(1f, 1000f);

		// Token: 0x04006388 RID: 25480
		public Vector2 overlayMassScaleValues = new Vector2f(0.1f, 1f);
	}

	// Token: 0x0200147B RID: 5243
	private class WorldRegion
	{
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600811F RID: 33055 RVA: 0x002E0E3B File Offset: 0x002DF03B
		public Vector2I regionMin
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06008120 RID: 33056 RVA: 0x002E0E43 File Offset: 0x002DF043
		public Vector2I regionMax
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x06008121 RID: 33057 RVA: 0x002E0E4C File Offset: 0x002DF04C
		public void UpdateGameActiveRegion(int x0, int y0, int x1, int y1)
		{
			this.min.x = Mathf.Max(0, x0);
			this.min.y = Mathf.Max(0, y0);
			this.max.x = Mathf.Max(x1, this.regionMax.x);
			this.max.y = Mathf.Max(y1, this.regionMax.y);
		}

		// Token: 0x06008122 RID: 33058 RVA: 0x002E0EB6 File Offset: 0x002DF0B6
		public void UpdateGameActiveRegion(Vector2I simActiveRegionMin, Vector2I simActiveRegionMax)
		{
			this.min = simActiveRegionMin;
			this.max = simActiveRegionMax;
		}

		// Token: 0x04006389 RID: 25481
		private Vector2I min;

		// Token: 0x0400638A RID: 25482
		private Vector2I max;

		// Token: 0x0400638B RID: 25483
		public bool isActive;
	}

	// Token: 0x0200147C RID: 5244
	public class SimActiveRegion
	{
		// Token: 0x06008124 RID: 33060 RVA: 0x002E0ECE File Offset: 0x002DF0CE
		public SimActiveRegion()
		{
			this.region = default(Pair<Vector2I, Vector2I>);
			this.currentSunlightIntensity = (float)FIXEDTRAITS.SUNLIGHT.DEFAULT_VALUE;
			this.currentCosmicRadiationIntensity = (float)FIXEDTRAITS.COSMICRADIATION.DEFAULT_VALUE;
		}

		// Token: 0x0400638C RID: 25484
		public Pair<Vector2I, Vector2I> region;

		// Token: 0x0400638D RID: 25485
		public float currentSunlightIntensity;

		// Token: 0x0400638E RID: 25486
		public float currentCosmicRadiationIntensity;
	}

	// Token: 0x0200147D RID: 5245
	private enum SpawnRotationConfig
	{
		// Token: 0x04006390 RID: 25488
		Normal,
		// Token: 0x04006391 RID: 25489
		StringName
	}

	// Token: 0x0200147E RID: 5246
	[Serializable]
	private struct SpawnRotationData
	{
		// Token: 0x04006392 RID: 25490
		public string animName;

		// Token: 0x04006393 RID: 25491
		public bool flip;
	}

	// Token: 0x0200147F RID: 5247
	[Serializable]
	private struct SpawnPoolData
	{
		// Token: 0x04006394 RID: 25492
		[HashedEnum]
		public SpawnFXHashes id;

		// Token: 0x04006395 RID: 25493
		public int initialCount;

		// Token: 0x04006396 RID: 25494
		public Color32 colour;

		// Token: 0x04006397 RID: 25495
		public GameObject fxPrefab;

		// Token: 0x04006398 RID: 25496
		public string initialAnim;

		// Token: 0x04006399 RID: 25497
		public Vector3 spawnOffset;

		// Token: 0x0400639A RID: 25498
		public Vector2 spawnRandomOffset;

		// Token: 0x0400639B RID: 25499
		public Game.SpawnRotationConfig rotationConfig;

		// Token: 0x0400639C RID: 25500
		public Game.SpawnRotationData[] rotationData;
	}

	// Token: 0x02001480 RID: 5248
	[Serializable]
	private class Settings
	{
		// Token: 0x06008125 RID: 33061 RVA: 0x002E0EFA File Offset: 0x002DF0FA
		public Settings(Game game)
		{
			this.nextUniqueID = KPrefabID.NextUniqueID;
			this.gameID = KleiMetrics.GameID();
		}

		// Token: 0x06008126 RID: 33062 RVA: 0x002E0F18 File Offset: 0x002DF118
		public Settings()
		{
		}

		// Token: 0x0400639D RID: 25501
		public int nextUniqueID;

		// Token: 0x0400639E RID: 25502
		public int gameID;
	}

	// Token: 0x02001481 RID: 5249
	public class GameSaveData
	{
		// Token: 0x0400639F RID: 25503
		public ConduitFlow gasConduitFlow;

		// Token: 0x040063A0 RID: 25504
		public ConduitFlow liquidConduitFlow;

		// Token: 0x040063A1 RID: 25505
		public FallingWater fallingWater;

		// Token: 0x040063A2 RID: 25506
		public UnstableGroundManager unstableGround;

		// Token: 0x040063A3 RID: 25507
		public WorldDetailSave worldDetail;

		// Token: 0x040063A4 RID: 25508
		public CustomGameSettings customGameSettings;

		// Token: 0x040063A5 RID: 25509
		public StoryManager storySetings;

		// Token: 0x040063A6 RID: 25510
		public bool debugWasUsed;

		// Token: 0x040063A7 RID: 25511
		public bool autoPrioritizeRoles;

		// Token: 0x040063A8 RID: 25512
		public bool advancedPersonalPriorities;

		// Token: 0x040063A9 RID: 25513
		public Game.SavedInfo savedInfo;

		// Token: 0x040063AA RID: 25514
		public string dateGenerated;

		// Token: 0x040063AB RID: 25515
		public List<uint> changelistsPlayedOn;
	}

	// Token: 0x02001482 RID: 5250
	// (Invoke) Token: 0x06008129 RID: 33065
	public delegate void CansaveCB();

	// Token: 0x02001483 RID: 5251
	// (Invoke) Token: 0x0600812D RID: 33069
	public delegate void SavingPreCB(Game.CansaveCB cb);

	// Token: 0x02001484 RID: 5252
	// (Invoke) Token: 0x06008131 RID: 33073
	public delegate void SavingActiveCB();

	// Token: 0x02001485 RID: 5253
	// (Invoke) Token: 0x06008135 RID: 33077
	public delegate void SavingPostCB();

	// Token: 0x02001486 RID: 5254
	[Serializable]
	public struct LocationColours
	{
		// Token: 0x040063AC RID: 25516
		public Color unreachable;

		// Token: 0x040063AD RID: 25517
		public Color invalidLocation;

		// Token: 0x040063AE RID: 25518
		public Color validLocation;

		// Token: 0x040063AF RID: 25519
		public Color requiresRole;

		// Token: 0x040063B0 RID: 25520
		public Color unreachable_requiresRole;
	}

	// Token: 0x02001487 RID: 5255
	[Serializable]
	public class UIColours
	{
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06008138 RID: 33080 RVA: 0x002E0F28 File Offset: 0x002DF128
		public Game.LocationColours Dig
		{
			get
			{
				return this.digColours;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06008139 RID: 33081 RVA: 0x002E0F30 File Offset: 0x002DF130
		public Game.LocationColours Build
		{
			get
			{
				return this.buildColours;
			}
		}

		// Token: 0x040063B1 RID: 25521
		[SerializeField]
		private Game.LocationColours digColours;

		// Token: 0x040063B2 RID: 25522
		[SerializeField]
		private Game.LocationColours buildColours;
	}
}
