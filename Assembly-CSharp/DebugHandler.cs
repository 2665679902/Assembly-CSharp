using System;
using System.Collections.Generic;
using System.IO;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x020006FA RID: 1786
public class DebugHandler : IInputHandler
{
	// Token: 0x17000380 RID: 896
	// (get) Token: 0x060030C5 RID: 12485 RVA: 0x00102474 File Offset: 0x00100674
	// (set) Token: 0x060030C6 RID: 12486 RVA: 0x0010247B File Offset: 0x0010067B
	public static bool NotificationsDisabled { get; private set; }

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x060030C7 RID: 12487 RVA: 0x00102483 File Offset: 0x00100683
	// (set) Token: 0x060030C8 RID: 12488 RVA: 0x0010248A File Offset: 0x0010068A
	public static bool enabled { get; private set; }

	// Token: 0x060030C9 RID: 12489 RVA: 0x00102494 File Offset: 0x00100694
	public DebugHandler()
	{
		DebugHandler.enabled = File.Exists(Path.Combine(Application.dataPath, "debug_enable.txt"));
		DebugHandler.enabled = DebugHandler.enabled || File.Exists(Path.Combine(Application.dataPath, "../debug_enable.txt"));
		DebugHandler.enabled = DebugHandler.enabled || GenericGameSettings.instance.debugEnable;
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x060030CA RID: 12490 RVA: 0x001024FC File Offset: 0x001006FC
	public string handlerName
	{
		get
		{
			return "DebugHandler";
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x060030CB RID: 12491 RVA: 0x00102503 File Offset: 0x00100703
	// (set) Token: 0x060030CC RID: 12492 RVA: 0x0010250B File Offset: 0x0010070B
	public KInputHandler inputHandler { get; set; }

	// Token: 0x060030CD RID: 12493 RVA: 0x00102514 File Offset: 0x00100714
	public static int GetMouseCell()
	{
		Vector3 mousePos = KInputManager.GetMousePos();
		mousePos.z = -Camera.main.transform.GetPosition().z - Grid.CellSizeInMeters;
		return Grid.PosToCell(Camera.main.ScreenToWorldPoint(mousePos));
	}

	// Token: 0x060030CE RID: 12494 RVA: 0x0010255C File Offset: 0x0010075C
	public static Vector3 GetMousePos()
	{
		Vector3 mousePos = KInputManager.GetMousePos();
		mousePos.z = -Camera.main.transform.GetPosition().z - Grid.CellSizeInMeters;
		return Camera.main.ScreenToWorldPoint(mousePos);
	}

	// Token: 0x060030CF RID: 12495 RVA: 0x0010259C File Offset: 0x0010079C
	private void SpawnMinion(bool addAtmoSuit = false)
	{
		if (Immigration.Instance == null)
		{
			return;
		}
		if (!Grid.IsValidBuildingCell(DebugHandler.GetMouseCell()))
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.DEBUG_TOOLS.INVALID_LOCATION, null, DebugHandler.GetMousePos(), 1.5f, false, true);
			return;
		}
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(MinionConfig.ID), null, null);
		gameObject.name = Assets.GetPrefab(MinionConfig.ID).name;
		Immigration.Instance.ApplyDefaultPersonalPriorities(gameObject);
		Vector3 vector = Grid.CellToPosCBC(DebugHandler.GetMouseCell(), Grid.SceneLayer.Move);
		gameObject.transform.SetLocalPosition(vector);
		gameObject.SetActive(true);
		new MinionStartingStats(false, null, null, true).Apply(gameObject);
		if (addAtmoSuit)
		{
			GameObject gameObject2 = GameUtil.KInstantiate(Assets.GetPrefab("Atmo_Suit"), vector, Grid.SceneLayer.Creatures, null, 0);
			gameObject2.SetActive(true);
			SuitTank component = gameObject2.GetComponent<SuitTank>();
			GameObject gameObject3 = GameUtil.KInstantiate(Assets.GetPrefab(GameTags.Oxygen), vector, Grid.SceneLayer.Ore, null, 0);
			gameObject3.GetComponent<PrimaryElement>().Units = component.capacity;
			gameObject3.SetActive(true);
			component.storage.Store(gameObject3, true, false, true, false);
			Equippable component2 = gameObject2.GetComponent<Equippable>();
			gameObject.GetComponent<MinionIdentity>().ValidateProxy();
			Equipment component3 = gameObject.GetComponent<MinionIdentity>().assignableProxy.Get().GetComponent<Equipment>();
			component2.Assign(component3.GetComponent<IAssignableIdentity>());
			gameObject2.GetComponent<EquippableWorkable>().CancelChore("Debug Handler");
			component3.Equip(component2);
		}
		gameObject.GetMyWorld().SetDupeVisited();
	}

	// Token: 0x060030D0 RID: 12496 RVA: 0x00102722 File Offset: 0x00100922
	public static void SetDebugEnabled(bool debugEnabled)
	{
		DebugHandler.enabled = debugEnabled;
	}

	// Token: 0x060030D1 RID: 12497 RVA: 0x0010272A File Offset: 0x0010092A
	public static void ToggleDisableNotifications()
	{
		DebugHandler.NotificationsDisabled = !DebugHandler.NotificationsDisabled;
	}

	// Token: 0x060030D2 RID: 12498 RVA: 0x0010273C File Offset: 0x0010093C
	private string GetScreenshotFileName()
	{
		string activeSaveFilePath = SaveLoader.GetActiveSaveFilePath();
		string text = Path.Combine(Path.GetDirectoryName(activeSaveFilePath), "screenshot");
		string fileName = Path.GetFileName(activeSaveFilePath);
		Directory.CreateDirectory(text);
		return Path.ChangeExtension(Path.Combine(text, fileName), ".png");
	}

	// Token: 0x060030D3 RID: 12499 RVA: 0x00102780 File Offset: 0x00100980
	public void OnKeyDown(KButtonEvent e)
	{
		if (!DebugHandler.enabled)
		{
			return;
		}
		if (e.TryConsume(global::Action.DebugSpawnMinion))
		{
			this.SpawnMinion(false);
		}
		else if (e.TryConsume(global::Action.DebugSpawnMinionAtmoSuit))
		{
			this.SpawnMinion(true);
		}
		else if (e.TryConsume(global::Action.DebugCheerEmote))
		{
			for (int i = 0; i < Components.MinionIdentities.Count; i++)
			{
				new EmoteChore(Components.MinionIdentities[i].GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, "anim_cheer_kanim", new HashedString[] { "cheer_pre", "cheer_loop", "cheer_pst" }, null);
				new EmoteChore(Components.MinionIdentities[i].GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, "anim_cheer_kanim", new HashedString[] { "cheer_pre", "cheer_loop", "cheer_pst" }, null);
			}
		}
		else if (e.TryConsume(global::Action.DebugSpawnStressTest))
		{
			for (int j = 0; j < 60; j++)
			{
				this.SpawnMinion(false);
			}
		}
		else if (e.TryConsume(global::Action.DebugSuperTestMode))
		{
			if (!this.superTestMode)
			{
				Time.timeScale = 15f;
				this.superTestMode = true;
			}
			else
			{
				Time.timeScale = 1f;
				this.superTestMode = false;
			}
		}
		else if (e.TryConsume(global::Action.DebugUltraTestMode))
		{
			if (!this.ultraTestMode)
			{
				Time.timeScale = 30f;
				this.ultraTestMode = true;
			}
			else
			{
				Time.timeScale = 1f;
				this.ultraTestMode = false;
			}
		}
		else if (e.TryConsume(global::Action.DebugSlowTestMode))
		{
			if (!this.slowTestMode)
			{
				Time.timeScale = 0.06f;
				this.slowTestMode = true;
			}
			else
			{
				Time.timeScale = 1f;
				this.slowTestMode = false;
			}
		}
		else if (e.TryConsume(global::Action.DebugDig))
		{
			SimMessages.Dig(DebugHandler.GetMouseCell(), -1, false);
		}
		else if (e.TryConsume(global::Action.DebugToggleFastWorkers))
		{
			Game.Instance.FastWorkersModeActive = !Game.Instance.FastWorkersModeActive;
		}
		else if (e.TryConsume(global::Action.DebugInstantBuildMode))
		{
			DebugHandler.InstantBuildMode = !DebugHandler.InstantBuildMode;
			InterfaceTool.ToggleConfig(global::Action.DebugInstantBuildMode);
			if (Game.Instance == null)
			{
				return;
			}
			Game.Instance.Trigger(1557339983, null);
			if (PlanScreen.Instance != null)
			{
				PlanScreen.Instance.Refresh();
			}
			if (BuildMenu.Instance != null)
			{
				BuildMenu.Instance.Refresh();
			}
			if (OverlayMenu.Instance != null)
			{
				OverlayMenu.Instance.Refresh();
			}
			if (ConsumerManager.instance != null)
			{
				ConsumerManager.instance.RefreshDiscovered(null);
			}
			if (ManagementMenu.Instance != null)
			{
				ManagementMenu.Instance.CheckResearch(null);
				ManagementMenu.Instance.CheckSkills(null);
				ManagementMenu.Instance.CheckStarmap(null);
			}
			Game.Instance.Trigger(1594320620, "all_the_things");
		}
		else if (e.TryConsume(global::Action.DebugExplosion))
		{
			Vector3 mousePos = KInputManager.GetMousePos();
			mousePos.z = -Camera.main.transform.GetPosition().z - Grid.CellSizeInMeters;
			GameUtil.CreateExplosion(Camera.main.ScreenToWorldPoint(mousePos));
		}
		else if (e.TryConsume(global::Action.DebugLockCursor))
		{
			if (GenericGameSettings.instance.developerDebugEnable)
			{
				KInputManager.isMousePosLocked = !KInputManager.isMousePosLocked;
				KInputManager.lockedMousePos = KInputManager.GetMousePos();
			}
		}
		else
		{
			if (e.TryConsume(global::Action.DebugDiscoverAllElements))
			{
				if (!(DiscoveredResources.Instance != null))
				{
					goto IL_B34;
				}
				using (List<Element>.Enumerator enumerator = ElementLoader.elements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Element element = enumerator.Current;
						DiscoveredResources.Instance.Discover(element.tag, element.GetMaterialCategoryTag());
					}
					goto IL_B34;
				}
			}
			if (e.TryConsume(global::Action.DebugToggleUI))
			{
				DebugHandler.ToggleScreenshotMode();
			}
			else if (e.TryConsume(global::Action.SreenShot1x))
			{
				ScreenCapture.CaptureScreenshot(this.GetScreenshotFileName(), 1);
			}
			else if (e.TryConsume(global::Action.SreenShot2x))
			{
				ScreenCapture.CaptureScreenshot(this.GetScreenshotFileName(), 2);
			}
			else if (e.TryConsume(global::Action.SreenShot8x))
			{
				ScreenCapture.CaptureScreenshot(this.GetScreenshotFileName(), 8);
			}
			else if (e.TryConsume(global::Action.SreenShot32x))
			{
				ScreenCapture.CaptureScreenshot(this.GetScreenshotFileName(), 32);
			}
			else if (e.TryConsume(global::Action.DebugCellInfo))
			{
				DebugHandler.DebugCellInfo = !DebugHandler.DebugCellInfo;
			}
			else if (e.TryConsume(global::Action.DebugToggle))
			{
				if (Game.Instance != null)
				{
					SaveGame.Instance.worldGenSpawner.SpawnEverything();
				}
				InterfaceTool.ToggleConfig(global::Action.DebugToggle);
				if (DebugPaintElementScreen.Instance != null)
				{
					bool activeSelf = DebugPaintElementScreen.Instance.gameObject.activeSelf;
					DebugPaintElementScreen.Instance.gameObject.SetActive(!activeSelf);
					if (DebugElementMenu.Instance && DebugElementMenu.Instance.root.activeSelf)
					{
						DebugElementMenu.Instance.root.SetActive(false);
					}
					DebugBaseTemplateButton.Instance.gameObject.SetActive(!activeSelf);
					PropertyTextures.FogOfWarScale = (float)((!activeSelf) ? 1 : 0);
					if (CameraController.Instance != null)
					{
						CameraController.Instance.EnableFreeCamera(!activeSelf);
					}
					DebugHandler.RevealFogOfWar = !DebugHandler.RevealFogOfWar;
					Game.Instance.Trigger(-1991583975, null);
				}
			}
			else if (e.TryConsume(global::Action.DebugCollectGarbage))
			{
				GC.Collect();
			}
			else if (e.TryConsume(global::Action.DebugInvincible))
			{
				DebugHandler.InvincibleMode = !DebugHandler.InvincibleMode;
			}
			else if (e.TryConsume(global::Action.DebugVisualTest))
			{
				Scenario.Instance.SetupVisualTest();
			}
			else if (e.TryConsume(global::Action.DebugGameplayTest))
			{
				Scenario.Instance.SetupGameplayTest();
			}
			else if (e.TryConsume(global::Action.DebugElementTest))
			{
				Scenario.Instance.SetupElementTest();
			}
			else if (e.TryConsume(global::Action.ToggleProfiler))
			{
				Sim.SIM_HandleMessage(-409964931, 0, null);
			}
			else if (e.TryConsume(global::Action.DebugRefreshNavCell))
			{
				Pathfinding.Instance.RefreshNavCell(DebugHandler.GetMouseCell());
			}
			else if (e.TryConsume(global::Action.DebugToggleSelectInEditor))
			{
				DebugHandler.SetSelectInEditor(!DebugHandler.SelectInEditor);
			}
			else
			{
				if (e.TryConsume(global::Action.DebugGotoTarget))
				{
					global::Debug.Log("Debug GoTo");
					Game.Instance.Trigger(775300118, null);
					using (List<Brain>.Enumerator enumerator2 = Components.Brains.Items.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Brain brain = enumerator2.Current;
							DebugGoToMonitor.Instance smi = brain.GetSMI<DebugGoToMonitor.Instance>();
							if (smi != null)
							{
								smi.GoToCursor();
							}
							CreatureDebugGoToMonitor.Instance smi2 = brain.GetSMI<CreatureDebugGoToMonitor.Instance>();
							if (smi2 != null)
							{
								smi2.GoToCursor();
							}
						}
						goto IL_B34;
					}
				}
				if (e.TryConsume(global::Action.DebugTeleport))
				{
					if (SelectTool.Instance == null)
					{
						return;
					}
					KSelectable selected = SelectTool.Instance.selected;
					if (selected != null)
					{
						int mouseCell = DebugHandler.GetMouseCell();
						if (!Grid.IsValidBuildingCell(mouseCell))
						{
							PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.DEBUG_TOOLS.INVALID_LOCATION, null, DebugHandler.GetMousePos(), 1.5f, false, true);
							return;
						}
						selected.transform.SetPosition(Grid.CellToPosCBC(mouseCell, Grid.SceneLayer.Move));
					}
				}
				else if (!e.TryConsume(global::Action.DebugPlace) && !e.TryConsume(global::Action.DebugSelectMaterial))
				{
					if (e.TryConsume(global::Action.DebugNotification))
					{
						if (GenericGameSettings.instance.developerDebugEnable)
						{
							Tutorial.Instance.DebugNotification();
						}
					}
					else if (e.TryConsume(global::Action.DebugNotificationMessage))
					{
						if (GenericGameSettings.instance.developerDebugEnable)
						{
							Tutorial.Instance.DebugNotificationMessage();
						}
					}
					else if (e.TryConsume(global::Action.DebugSuperSpeed))
					{
						if (SpeedControlScreen.Instance != null)
						{
							SpeedControlScreen.Instance.ToggleRidiculousSpeed();
						}
					}
					else if (e.TryConsume(global::Action.DebugGameStep))
					{
						if (SpeedControlScreen.Instance != null)
						{
							SpeedControlScreen.Instance.DebugStepFrame();
						}
					}
					else if (e.TryConsume(global::Action.DebugSimStep))
					{
						Game.Instance.ForceSimStep();
					}
					else if (e.TryConsume(global::Action.DebugToggleMusic))
					{
						AudioDebug.Get().ToggleMusic();
					}
					else if (e.TryConsume(global::Action.DebugTileTest))
					{
						Scenario.Instance.SetupTileTest();
					}
					else if (e.TryConsume(global::Action.DebugForceLightEverywhere))
					{
						PropertyTextures.instance.ForceLightEverywhere = !PropertyTextures.instance.ForceLightEverywhere;
					}
					else if (e.TryConsume(global::Action.DebugPathFinding))
					{
						DebugHandler.DebugPathFinding = !DebugHandler.DebugPathFinding;
						global::Debug.Log("DebugPathFinding=" + DebugHandler.DebugPathFinding.ToString());
					}
					else if (!e.TryConsume(global::Action.DebugFocus))
					{
						if (e.TryConsume(global::Action.DebugReportBug))
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								int num = 0;
								string validSaveFilename;
								for (;;)
								{
									validSaveFilename = SaveScreen.GetValidSaveFilename("bug_report_savefile_" + num.ToString());
									if (!File.Exists(validSaveFilename))
									{
										break;
									}
									num++;
								}
								string text = "No save file (front end)";
								if (SaveLoader.Instance != null)
								{
									text = SaveLoader.Instance.Save(validSaveFilename, false, false);
								}
								KCrashReporter.ReportBug("Bug Report", text, GameObject.Find("ScreenSpaceOverlayCanvas"));
							}
							else
							{
								global::Debug.Log("Debug crash keys are not enabled.");
							}
						}
						else if (e.TryConsume(global::Action.DebugTriggerException))
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								throw new ArgumentException("My test exception");
							}
						}
						else if (e.TryConsume(global::Action.DebugTriggerError))
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								global::Debug.LogError("Oooops! Testing error!");
							}
						}
						else if (e.TryConsume(global::Action.DebugDumpGCRoots))
						{
							GarbageProfiler.DebugDumpRootItems();
						}
						else if (e.TryConsume(global::Action.DebugDumpGarbageReferences))
						{
							GarbageProfiler.DebugDumpGarbageStats();
						}
						else if (e.TryConsume(global::Action.DebugDumpEventData))
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								KObjectManager.Instance.DumpEventData();
							}
						}
						else if (e.TryConsume(global::Action.DebugDumpSceneParitionerLeakData))
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
							}
						}
						else if (e.TryConsume(global::Action.DebugCrashSim))
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								Sim.SIM_DebugCrash();
							}
						}
						else if (e.TryConsume(global::Action.DebugNextCall))
						{
							DebugHandler.DebugNextCall = true;
						}
						else if (e.TryConsume(global::Action.DebugTogglePersonalPriorityComparison))
						{
							Chore.ENABLE_PERSONAL_PRIORITIES = !Chore.ENABLE_PERSONAL_PRIORITIES;
						}
						else if (e.TryConsume(global::Action.DebugToggleClusterFX))
						{
							CameraController.Instance.ToggleClusterFX();
						}
					}
				}
			}
		}
		IL_B34:
		if (e.Consumed && Game.Instance != null)
		{
			Game.Instance.debugWasUsed = true;
			KCrashReporter.debugWasUsed = true;
		}
	}

	// Token: 0x060030D4 RID: 12500 RVA: 0x00103304 File Offset: 0x00101504
	public static void SetSelectInEditor(bool select_in_editor)
	{
	}

	// Token: 0x060030D5 RID: 12501 RVA: 0x00103308 File Offset: 0x00101508
	public static void ToggleScreenshotMode()
	{
		DebugHandler.ScreenshotMode = !DebugHandler.ScreenshotMode;
		DebugHandler.UpdateUI();
		if (CameraController.Instance != null)
		{
			CameraController.Instance.EnableFreeCamera(DebugHandler.ScreenshotMode);
		}
		if (KScreenManager.Instance != null)
		{
			KScreenManager.Instance.DisableInput(DebugHandler.ScreenshotMode);
		}
	}

	// Token: 0x060030D6 RID: 12502 RVA: 0x00103360 File Offset: 0x00101560
	public static void SetTimelapseMode(bool enabled, int world_id = 0)
	{
		DebugHandler.TimelapseMode = enabled;
		if (enabled)
		{
			DebugHandler.activeWorldBeforeOverride = ClusterManager.Instance.activeWorldId;
			ClusterManager.Instance.TimelapseModeOverrideActiveWorld(world_id);
		}
		else
		{
			ClusterManager.Instance.TimelapseModeOverrideActiveWorld(DebugHandler.activeWorldBeforeOverride);
		}
		World.Instance.zoneRenderData.OnActiveWorldChanged();
		DebugHandler.UpdateUI();
	}

	// Token: 0x060030D7 RID: 12503 RVA: 0x001033B8 File Offset: 0x001015B8
	private static void UpdateUI()
	{
		DebugHandler.HideUI = DebugHandler.TimelapseMode || DebugHandler.ScreenshotMode;
		float num = (DebugHandler.HideUI ? 0f : 1f);
		GameScreenManager.Instance.ssHoverTextCanvas.GetComponent<CanvasGroup>().alpha = num;
		GameScreenManager.Instance.ssCameraCanvas.GetComponent<CanvasGroup>().alpha = num;
		GameScreenManager.Instance.ssOverlayCanvas.GetComponent<CanvasGroup>().alpha = num;
		GameScreenManager.Instance.worldSpaceCanvas.GetComponent<CanvasGroup>().alpha = num;
		GameScreenManager.Instance.screenshotModeCanvas.GetComponent<CanvasGroup>().alpha = 1f - num;
	}

	// Token: 0x04001D6B RID: 7531
	public static bool InstantBuildMode;

	// Token: 0x04001D6C RID: 7532
	public static bool InvincibleMode;

	// Token: 0x04001D6D RID: 7533
	public static bool SelectInEditor;

	// Token: 0x04001D6E RID: 7534
	public static bool DebugPathFinding;

	// Token: 0x04001D6F RID: 7535
	public static bool ScreenshotMode;

	// Token: 0x04001D70 RID: 7536
	public static bool TimelapseMode;

	// Token: 0x04001D71 RID: 7537
	public static bool HideUI;

	// Token: 0x04001D72 RID: 7538
	public static bool DebugCellInfo;

	// Token: 0x04001D73 RID: 7539
	public static bool DebugNextCall;

	// Token: 0x04001D74 RID: 7540
	public static bool RevealFogOfWar;

	// Token: 0x04001D78 RID: 7544
	private bool superTestMode;

	// Token: 0x04001D79 RID: 7545
	private bool ultraTestMode;

	// Token: 0x04001D7A RID: 7546
	private bool slowTestMode;

	// Token: 0x04001D7B RID: 7547
	private static int activeWorldBeforeOverride = -1;

	// Token: 0x0200141A RID: 5146
	public enum PaintMode
	{
		// Token: 0x04006289 RID: 25225
		None,
		// Token: 0x0400628A RID: 25226
		Element,
		// Token: 0x0400628B RID: 25227
		Hot,
		// Token: 0x0400628C RID: 25228
		Cold
	}
}
