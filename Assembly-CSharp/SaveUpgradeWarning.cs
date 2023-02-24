using System;
using System.Collections;
using System.Collections.Generic;
using Database;
using Klei.AI;
using Klei.CustomSettings;
using STRINGS;
using UnityEngine;

// Token: 0x020008FF RID: 2303
[AddComponentMenu("KMonoBehaviour/scripts/SaveUpgradeWarning")]
public class SaveUpgradeWarning : KMonoBehaviour
{
	// Token: 0x060042C9 RID: 17097 RVA: 0x001793C4 File Offset: 0x001775C4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Game game = this.game;
		game.OnLoad = (Action<Game.GameSaveData>)Delegate.Combine(game.OnLoad, new Action<Game.GameSaveData>(this.OnLoad));
	}

	// Token: 0x060042CA RID: 17098 RVA: 0x001793F3 File Offset: 0x001775F3
	protected override void OnCleanUp()
	{
		Game game = this.game;
		game.OnLoad = (Action<Game.GameSaveData>)Delegate.Remove(game.OnLoad, new Action<Game.GameSaveData>(this.OnLoad));
		base.OnCleanUp();
	}

	// Token: 0x060042CB RID: 17099 RVA: 0x00179424 File Offset: 0x00177624
	private void OnLoad(Game.GameSaveData data)
	{
		List<SaveUpgradeWarning.Upgrade> list = new List<SaveUpgradeWarning.Upgrade>
		{
			new SaveUpgradeWarning.Upgrade(7, 5, new System.Action(this.SuddenMoraleHelper)),
			new SaveUpgradeWarning.Upgrade(7, 13, new System.Action(this.BedAndBathHelper)),
			new SaveUpgradeWarning.Upgrade(7, 16, new System.Action(this.NewAutomationWarning))
		};
		if (DlcManager.IsPureVanilla())
		{
			list.Add(new SaveUpgradeWarning.Upgrade(7, 25, new System.Action(this.MergedownWarning)));
		}
		foreach (SaveUpgradeWarning.Upgrade upgrade in list)
		{
			if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(upgrade.major, upgrade.minor))
			{
				upgrade.action();
			}
		}
	}

	// Token: 0x060042CC RID: 17100 RVA: 0x0017950C File Offset: 0x0017770C
	private void SuddenMoraleHelper()
	{
		Effect morale_effect = Db.Get().effects.Get("SuddenMoraleHelper");
		CustomizableDialogScreen screen = Util.KInstantiateUI<CustomizableDialogScreen>(ScreenPrefabs.Instance.CustomizableDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, true);
		screen.AddOption(UI.FRONTEND.SAVEUPGRADEWARNINGS.SUDDENMORALEHELPER_BUFF, delegate
		{
			foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
			{
				minionIdentity.GetComponent<Effects>().Add(morale_effect, true);
			}
			screen.Deactivate();
		});
		screen.AddOption(UI.FRONTEND.SAVEUPGRADEWARNINGS.SUDDENMORALEHELPER_DISABLE, delegate
		{
			SettingConfig morale = CustomGameSettingConfigs.Morale;
			CustomGameSettings.Instance.customGameMode = CustomGameSettings.CustomGameMode.Custom;
			CustomGameSettings.Instance.SetQualitySetting(morale, morale.GetLevel("Disabled").id);
			screen.Deactivate();
		});
		screen.PopupConfirmDialog(string.Format(UI.FRONTEND.SAVEUPGRADEWARNINGS.SUDDENMORALEHELPER, Mathf.RoundToInt(morale_effect.duration / 600f)), UI.FRONTEND.SAVEUPGRADEWARNINGS.SUDDENMORALEHELPER_TITLE, null);
	}

	// Token: 0x060042CD RID: 17101 RVA: 0x001795E8 File Offset: 0x001777E8
	private void BedAndBathHelper()
	{
		if (SaveGame.Instance == null)
		{
			return;
		}
		ColonyAchievementTracker component = SaveGame.Instance.GetComponent<ColonyAchievementTracker>();
		if (component == null)
		{
			return;
		}
		ColonyAchievement basicComforts = Db.Get().ColonyAchievements.BasicComforts;
		ColonyAchievementStatus colonyAchievementStatus = null;
		if (component.achievements.TryGetValue(basicComforts.Id, out colonyAchievementStatus))
		{
			colonyAchievementStatus.failed = false;
		}
	}

	// Token: 0x060042CE RID: 17102 RVA: 0x00179648 File Offset: 0x00177848
	private void NewAutomationWarning()
	{
		SpriteListDialogScreen screen = Util.KInstantiateUI<SpriteListDialogScreen>(ScreenPrefabs.Instance.SpriteListDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, true);
		screen.AddOption(UI.CONFIRMDIALOG.OK, delegate
		{
			screen.Deactivate();
		});
		string[] array = SaveUpgradeWarning.buildingIDsWithNewPorts;
		for (int i = 0; i < array.Length; i++)
		{
			BuildingDef buildingDef = Assets.GetBuildingDef(array[i]);
			screen.AddSprite(buildingDef.GetUISprite("ui", false), buildingDef.Name, -1f, -1f);
		}
		screen.PopupConfirmDialog(UI.FRONTEND.SAVEUPGRADEWARNINGS.NEWAUTOMATIONWARNING, UI.FRONTEND.SAVEUPGRADEWARNINGS.NEWAUTOMATIONWARNING_TITLE);
		base.StartCoroutine(this.SendAutomationWarningNotifications());
	}

	// Token: 0x060042CF RID: 17103 RVA: 0x00179716 File Offset: 0x00177916
	private IEnumerator SendAutomationWarningNotifications()
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		if (Components.BuildingCompletes.Count == 0)
		{
			global::Debug.LogWarning("Could not send automation warnings because buildings have not yet loaded");
		}
		using (IEnumerator enumerator = Components.BuildingCompletes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				BuildingComplete buildingComplete = (BuildingComplete)obj;
				foreach (string text in SaveUpgradeWarning.buildingIDsWithNewPorts)
				{
					BuildingDef buildingDef = Assets.GetBuildingDef(text);
					if (buildingComplete.Def == buildingDef)
					{
						List<ILogicUIElement> list = new List<ILogicUIElement>();
						LogicPorts component = buildingComplete.GetComponent<LogicPorts>();
						if (component.outputPorts != null)
						{
							list.AddRange(component.outputPorts);
						}
						if (component.inputPorts != null)
						{
							list.AddRange(component.inputPorts);
						}
						foreach (ILogicUIElement logicUIElement in list)
						{
							if (Grid.Objects[logicUIElement.GetLogicUICell(), 31] != null)
							{
								global::Debug.Log("Triggering automation warning for building of type " + text);
								GenericMessage genericMessage = new GenericMessage(MISC.NOTIFICATIONS.NEW_AUTOMATION_WARNING.NAME, MISC.NOTIFICATIONS.NEW_AUTOMATION_WARNING.TOOLTIP, MISC.NOTIFICATIONS.NEW_AUTOMATION_WARNING.TOOLTIP, buildingComplete);
								Messenger.Instance.QueueMessage(genericMessage);
							}
						}
					}
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x060042D0 RID: 17104 RVA: 0x00179720 File Offset: 0x00177920
	private void MergedownWarning()
	{
		SpriteListDialogScreen screen = Util.KInstantiateUI<SpriteListDialogScreen>(ScreenPrefabs.Instance.SpriteListDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, true);
		screen.AddOption(UI.DEVELOPMENTBUILDS.FULL_PATCH_NOTES, delegate
		{
			App.OpenWebURL("https://forums.kleientertainment.com/game-updates/oni-alpha/");
		});
		screen.AddOption(UI.CONFIRMDIALOG.OK, delegate
		{
			screen.Deactivate();
		});
		screen.AddSprite(Assets.GetSprite("upgrade_mergedown_fridge"), UI.FRONTEND.SAVEUPGRADEWARNINGS.MERGEDOWNCHANGES_FOOD, 150f, 120f);
		screen.AddSprite(Assets.GetSprite("upgrade_mergedown_deodorizer"), UI.FRONTEND.SAVEUPGRADEWARNINGS.MERGEDOWNCHANGES_AIRFILTER, 150f, 120f);
		screen.AddSprite(Assets.GetSprite("upgrade_mergedown_steamturbine"), UI.FRONTEND.SAVEUPGRADEWARNINGS.MERGEDOWNCHANGES_SIMULATION, 150f, 120f);
		screen.AddSprite(Assets.GetSprite("upgrade_mergedown_oxygen_meter"), UI.FRONTEND.SAVEUPGRADEWARNINGS.MERGEDOWNCHANGES_BUILDINGS, 150f, 120f);
		screen.PopupConfirmDialog(UI.FRONTEND.SAVEUPGRADEWARNINGS.MERGEDOWNCHANGES, UI.FRONTEND.SAVEUPGRADEWARNINGS.MERGEDOWNCHANGES_TITLE);
		base.StartCoroutine(this.SendAutomationWarningNotifications());
	}

	// Token: 0x04002C98 RID: 11416
	[MyCmpReq]
	private Game game;

	// Token: 0x04002C99 RID: 11417
	private static string[] buildingIDsWithNewPorts = new string[] { "LiquidVent", "GasVent", "GasVentHighPressure", "SolidVent", "LiquidReservoir", "GasReservoir" };

	// Token: 0x020016D6 RID: 5846
	private struct Upgrade
	{
		// Token: 0x060088BF RID: 35007 RVA: 0x002F68DE File Offset: 0x002F4ADE
		public Upgrade(int major, int minor, System.Action action)
		{
			this.major = major;
			this.minor = minor;
			this.action = action;
		}

		// Token: 0x04006B24 RID: 27428
		public int major;

		// Token: 0x04006B25 RID: 27429
		public int minor;

		// Token: 0x04006B26 RID: 27430
		public System.Action action;
	}
}
