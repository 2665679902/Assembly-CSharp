using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C35 RID: 3125
public class WorldSelector : KScreen, ISim4000ms
{
	// Token: 0x060062CE RID: 25294 RVA: 0x00247A60 File Offset: 0x00245C60
	public static void DestroyInstance()
	{
		WorldSelector.Instance = null;
	}

	// Token: 0x060062CF RID: 25295 RVA: 0x00247A68 File Offset: 0x00245C68
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		WorldSelector.Instance = this;
	}

	// Token: 0x060062D0 RID: 25296 RVA: 0x00247A78 File Offset: 0x00245C78
	protected override void OnSpawn()
	{
		if (!DlcManager.FeatureClusterSpaceEnabled())
		{
			this.Deactivate();
			return;
		}
		base.OnSpawn();
		this.worldRows = new Dictionary<int, MultiToggle>();
		this.SpawnToggles();
		this.RefreshToggles();
		Game.Instance.Subscribe(1983128072, delegate(object data)
		{
			this.RefreshToggles();
		});
		Game.Instance.Subscribe(-521212405, delegate(object data)
		{
			this.RefreshToggles();
		});
		Game.Instance.Subscribe(880851192, delegate(object data)
		{
			this.SortRows();
		});
		ClusterManager.Instance.Subscribe(-1280433810, delegate(object data)
		{
			this.AddWorld(data);
		});
		ClusterManager.Instance.Subscribe(-1078710002, delegate(object data)
		{
			this.RemoveWorld(data);
		});
		ClusterManager.Instance.Subscribe(1943181844, delegate(object data)
		{
			this.RefreshToggles();
		});
	}

	// Token: 0x060062D1 RID: 25297 RVA: 0x00247B58 File Offset: 0x00245D58
	private void SpawnToggles()
	{
		foreach (KeyValuePair<int, MultiToggle> keyValuePair in this.worldRows)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.worldRows.Clear();
		foreach (int num in ClusterManager.Instance.GetWorldIDsSorted())
		{
			MultiToggle component = Util.KInstantiateUI(this.worldRowPrefab, this.worldRowContainer, false).GetComponent<MultiToggle>();
			this.worldRows.Add(num, component);
			this.previousWorldDiagnosticStatus.Add(num, ColonyDiagnostic.DiagnosticResult.Opinion.Normal);
			int id = num;
			MultiToggle multiToggle = component;
			multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
			{
				this.OnWorldRowClicked(id);
			}));
			component.GetComponentInChildren<AlertVignette>().worldID = num;
		}
	}

	// Token: 0x060062D2 RID: 25298 RVA: 0x00247C7C File Offset: 0x00245E7C
	private void AddWorld(object data)
	{
		int num = (int)data;
		MultiToggle component = Util.KInstantiateUI(this.worldRowPrefab, this.worldRowContainer, false).GetComponent<MultiToggle>();
		this.worldRows.Add(num, component);
		this.previousWorldDiagnosticStatus.Add(num, ColonyDiagnostic.DiagnosticResult.Opinion.Normal);
		int id = num;
		MultiToggle multiToggle = component;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			this.OnWorldRowClicked(id);
		}));
		component.GetComponentInChildren<AlertVignette>().worldID = num;
		this.RefreshToggles();
	}

	// Token: 0x060062D3 RID: 25299 RVA: 0x00247D0C File Offset: 0x00245F0C
	private void RemoveWorld(object data)
	{
		int num = (int)data;
		MultiToggle multiToggle;
		if (this.worldRows.TryGetValue(num, out multiToggle))
		{
			multiToggle.DeleteObject();
		}
		this.worldRows.Remove(num);
		this.previousWorldDiagnosticStatus.Remove(num);
		this.RefreshToggles();
	}

	// Token: 0x060062D4 RID: 25300 RVA: 0x00247D58 File Offset: 0x00245F58
	public void OnWorldRowClicked(int id)
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(id);
		if (world != null && world.IsDiscovered)
		{
			CameraController.Instance.ActiveWorldStarWipe(id, null);
		}
	}

	// Token: 0x060062D5 RID: 25301 RVA: 0x00247D90 File Offset: 0x00245F90
	private void RefreshToggles()
	{
		foreach (KeyValuePair<int, MultiToggle> keyValuePair in this.worldRows)
		{
			WorldContainer world = ClusterManager.Instance.GetWorld(keyValuePair.Key);
			ClusterGridEntity component = world.GetComponent<ClusterGridEntity>();
			HierarchyReferences component2 = keyValuePair.Value.GetComponent<HierarchyReferences>();
			if (world != null)
			{
				component2.GetReference<Image>("Icon").sprite = component.GetUISprite();
				component2.GetReference<LocText>("Label").SetText(world.GetComponent<ClusterGridEntity>().Name);
			}
			else
			{
				component2.GetReference<Image>("Icon").sprite = Assets.GetSprite("unknown_far");
			}
			if (keyValuePair.Key == CameraController.Instance.cameraActiveCluster)
			{
				keyValuePair.Value.ChangeState(1);
				keyValuePair.Value.gameObject.SetActive(true);
			}
			else if (world != null && world.IsDiscovered)
			{
				keyValuePair.Value.ChangeState(0);
				keyValuePair.Value.gameObject.SetActive(true);
			}
			else
			{
				keyValuePair.Value.ChangeState(0);
				keyValuePair.Value.gameObject.SetActive(false);
			}
			this.RefreshToggleTooltips();
			keyValuePair.Value.GetComponentInChildren<AlertVignette>().worldID = keyValuePair.Key;
		}
		this.RefreshWorldStatus();
		this.SortRows();
	}

	// Token: 0x060062D6 RID: 25302 RVA: 0x00247F24 File Offset: 0x00246124
	private void RefreshWorldStatus()
	{
		foreach (KeyValuePair<int, MultiToggle> keyValuePair in this.worldRows)
		{
			if (!this.worldStatusIcons.ContainsKey(keyValuePair.Key))
			{
				this.worldStatusIcons.Add(keyValuePair.Key, new List<GameObject>());
			}
			foreach (GameObject gameObject in this.worldStatusIcons[keyValuePair.Key])
			{
				Util.KDestroyGameObject(gameObject);
			}
			LocText reference = keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference<LocText>("StatusLabel");
			reference.SetText(ClusterManager.Instance.GetWorld(keyValuePair.Key).GetStatus());
			reference.color = ColonyDiagnosticScreen.GetDiagnosticIndicationColor(ColonyDiagnosticUtility.Instance.GetWorldDiagnosticResult(keyValuePair.Key));
		}
	}

	// Token: 0x060062D7 RID: 25303 RVA: 0x0024803C File Offset: 0x0024623C
	private void RefreshToggleTooltips()
	{
		int num = 0;
		List<int> discoveredAsteroidIDsSorted = ClusterManager.Instance.GetDiscoveredAsteroidIDsSorted();
		foreach (KeyValuePair<int, MultiToggle> keyValuePair in this.worldRows)
		{
			ClusterGridEntity component = ClusterManager.Instance.GetWorld(keyValuePair.Key).GetComponent<ClusterGridEntity>();
			ToolTip component2 = keyValuePair.Value.GetComponent<ToolTip>();
			component2.ClearMultiStringTooltip();
			WorldContainer world = ClusterManager.Instance.GetWorld(keyValuePair.Key);
			if (world != null)
			{
				component2.AddMultiStringTooltip(component.Name, this.titleTextSetting);
				if (!world.IsModuleInterior)
				{
					int num2 = discoveredAsteroidIDsSorted.IndexOf(world.id);
					if (num2 != -1 && num2 <= 9)
					{
						component2.AddMultiStringTooltip(" ", this.bodyTextSetting);
						if (KInputManager.currentControllerIsGamepad)
						{
							component2.AddMultiStringTooltip(UI.FormatAsHotkey(GameUtil.GetActionString(this.IdxToHotkeyAction(num2))), this.bodyTextSetting);
						}
						else
						{
							component2.AddMultiStringTooltip(UI.FormatAsHotkey("[" + GameUtil.GetActionString(this.IdxToHotkeyAction(num2)) + "]"), this.bodyTextSetting);
						}
					}
				}
			}
			else
			{
				component2.AddMultiStringTooltip(UI.CLUSTERMAP.UNKNOWN_DESTINATION, this.titleTextSetting);
			}
			if (ColonyDiagnosticUtility.Instance.GetWorldDiagnosticResult(world.id) < ColonyDiagnostic.DiagnosticResult.Opinion.Normal)
			{
				component2.AddMultiStringTooltip(ColonyDiagnosticUtility.Instance.GetWorldDiagnosticResultTooltip(world.id), this.bodyTextSetting);
			}
			num++;
		}
	}

	// Token: 0x060062D8 RID: 25304 RVA: 0x002481EC File Offset: 0x002463EC
	private void SortRows()
	{
		List<KeyValuePair<int, MultiToggle>> list = this.worldRows.ToList<KeyValuePair<int, MultiToggle>>();
		list.Sort(delegate(KeyValuePair<int, MultiToggle> x, KeyValuePair<int, MultiToggle> y)
		{
			float num = (ClusterManager.Instance.GetWorld(x.Key).IsModuleInterior ? float.PositiveInfinity : ClusterManager.Instance.GetWorld(x.Key).DiscoveryTimestamp);
			float num2 = (ClusterManager.Instance.GetWorld(y.Key).IsModuleInterior ? float.PositiveInfinity : ClusterManager.Instance.GetWorld(y.Key).DiscoveryTimestamp);
			return num.CompareTo(num2);
		});
		for (int i = 0; i < list.Count; i++)
		{
			list[i].Value.transform.SetSiblingIndex(i);
		}
		foreach (KeyValuePair<int, MultiToggle> keyValuePair in list)
		{
			keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference<RectTransform>("Indent").anchoredPosition = Vector2.zero;
			keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference<RectTransform>("Status").anchoredPosition = Vector2.right * 24f;
			WorldContainer world = ClusterManager.Instance.GetWorld(keyValuePair.Key);
			if (world.ParentWorldId != world.id && world.ParentWorldId != (int)ClusterManager.INVALID_WORLD_IDX)
			{
				foreach (KeyValuePair<int, MultiToggle> keyValuePair2 in list)
				{
					if (keyValuePair2.Key == world.ParentWorldId)
					{
						int siblingIndex = keyValuePair2.Value.gameObject.transform.GetSiblingIndex();
						keyValuePair.Value.gameObject.transform.SetSiblingIndex(siblingIndex + 1);
						keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference<RectTransform>("Indent").anchoredPosition = Vector2.right * 32f;
						keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference<RectTransform>("Status").anchoredPosition = Vector2.right * -8f;
						break;
					}
				}
			}
		}
	}

	// Token: 0x060062D9 RID: 25305 RVA: 0x0024840C File Offset: 0x0024660C
	private global::Action IdxToHotkeyAction(int idx)
	{
		global::Action action;
		switch (idx)
		{
		case 0:
			action = global::Action.SwitchActiveWorld1;
			break;
		case 1:
			action = global::Action.SwitchActiveWorld2;
			break;
		case 2:
			action = global::Action.SwitchActiveWorld3;
			break;
		case 3:
			action = global::Action.SwitchActiveWorld4;
			break;
		case 4:
			action = global::Action.SwitchActiveWorld5;
			break;
		case 5:
			action = global::Action.SwitchActiveWorld6;
			break;
		case 6:
			action = global::Action.SwitchActiveWorld7;
			break;
		case 7:
			action = global::Action.SwitchActiveWorld8;
			break;
		case 8:
			action = global::Action.SwitchActiveWorld9;
			break;
		case 9:
			action = global::Action.SwitchActiveWorld10;
			break;
		default:
			global::Debug.LogError("Action must be a SwitchActiveWorld Action");
			action = global::Action.SwitchActiveWorld1;
			break;
		}
		return action;
	}

	// Token: 0x060062DA RID: 25306 RVA: 0x002484AC File Offset: 0x002466AC
	public void Sim4000ms(float dt)
	{
		foreach (KeyValuePair<int, MultiToggle> keyValuePair in this.worldRows)
		{
			ColonyDiagnostic.DiagnosticResult.Opinion worldDiagnosticResult = ColonyDiagnosticUtility.Instance.GetWorldDiagnosticResult(keyValuePair.Key);
			ColonyDiagnosticScreen.SetIndication(worldDiagnosticResult, keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference("Indicator").gameObject);
			if (this.previousWorldDiagnosticStatus[keyValuePair.Key] > worldDiagnosticResult && ClusterManager.Instance.activeWorldId != keyValuePair.Key)
			{
				this.TriggerVisualNotification(keyValuePair.Key, worldDiagnosticResult);
			}
			this.previousWorldDiagnosticStatus[keyValuePair.Key] = worldDiagnosticResult;
		}
		this.RefreshWorldStatus();
		this.RefreshToggleTooltips();
	}

	// Token: 0x060062DB RID: 25307 RVA: 0x00248588 File Offset: 0x00246788
	public void TriggerVisualNotification(int worldID, ColonyDiagnostic.DiagnosticResult.Opinion result)
	{
		foreach (KeyValuePair<int, MultiToggle> keyValuePair in this.worldRows)
		{
			if (keyValuePair.Key == worldID)
			{
				KFMOD.PlayUISound(GlobalAssets.GetSound(ColonyDiagnosticScreen.notificationSoundsInactive[result], false));
				if (keyValuePair.Value.gameObject.activeInHierarchy)
				{
					keyValuePair.Value.StartCoroutine(this.VisualNotificationRoutine(keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference<RectTransform>("Content").gameObject, keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference<RectTransform>("Indicator"), keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference<RectTransform>("Spacer").gameObject));
				}
			}
		}
	}

	// Token: 0x060062DC RID: 25308 RVA: 0x00248670 File Offset: 0x00246870
	private IEnumerator VisualNotificationRoutine(GameObject contentGameObject, RectTransform indicator, GameObject spacer)
	{
		spacer.GetComponent<NotificationAnimator>().Begin(false);
		Vector2 defaultIndicatorSize = new Vector2(8f, 8f);
		float bounceDuration = 1.5f;
		for (float i = 0f; i < bounceDuration; i += Time.unscaledDeltaTime)
		{
			indicator.sizeDelta = defaultIndicatorSize + Vector2.one * (float)Mathf.RoundToInt(Mathf.Sin(6f * (3.1415927f * (i / bounceDuration))));
			yield return 0;
		}
		for (float i = 0f; i < bounceDuration; i += Time.unscaledDeltaTime)
		{
			indicator.sizeDelta = defaultIndicatorSize + Vector2.one * (float)Mathf.RoundToInt(Mathf.Sin(6f * (3.1415927f * (i / bounceDuration))));
			yield return 0;
		}
		for (float i = 0f; i < bounceDuration; i += Time.unscaledDeltaTime)
		{
			indicator.sizeDelta = defaultIndicatorSize + Vector2.one * (float)Mathf.RoundToInt(Mathf.Sin(6f * (3.1415927f * (i / bounceDuration))));
			yield return 0;
		}
		defaultIndicatorSize = new Vector2(8f, 8f);
		indicator.sizeDelta = defaultIndicatorSize;
		contentGameObject.rectTransform().localPosition = Vector2.zero;
		yield break;
	}

	// Token: 0x0400449F RID: 17567
	public static WorldSelector Instance;

	// Token: 0x040044A0 RID: 17568
	public Dictionary<int, MultiToggle> worldRows;

	// Token: 0x040044A1 RID: 17569
	public TextStyleSetting titleTextSetting;

	// Token: 0x040044A2 RID: 17570
	public TextStyleSetting bodyTextSetting;

	// Token: 0x040044A3 RID: 17571
	public GameObject worldRowPrefab;

	// Token: 0x040044A4 RID: 17572
	public GameObject worldRowContainer;

	// Token: 0x040044A5 RID: 17573
	private Dictionary<int, ColonyDiagnostic.DiagnosticResult.Opinion> previousWorldDiagnosticStatus = new Dictionary<int, ColonyDiagnostic.DiagnosticResult.Opinion>();

	// Token: 0x040044A6 RID: 17574
	private Dictionary<int, List<GameObject>> worldStatusIcons = new Dictionary<int, List<GameObject>>();
}
