using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.UI;

// Token: 0x02000A20 RID: 2592
public class ToolMenu : KScreen
{
	// Token: 0x06004E4C RID: 20044 RVA: 0x001BBF9D File Offset: 0x001BA19D
	public static void DestroyInstance()
	{
		ToolMenu.Instance = null;
	}

	// Token: 0x170005C9 RID: 1481
	// (get) Token: 0x06004E4D RID: 20045 RVA: 0x001BBFA5 File Offset: 0x001BA1A5
	public PriorityScreen PriorityScreen
	{
		get
		{
			return this.priorityScreen;
		}
	}

	// Token: 0x06004E4E RID: 20046 RVA: 0x001BBFAD File Offset: 0x001BA1AD
	public override float GetSortKey()
	{
		return 5f;
	}

	// Token: 0x06004E4F RID: 20047 RVA: 0x001BBFB4 File Offset: 0x001BA1B4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ToolMenu.Instance = this;
		Game.Instance.Subscribe(1798162660, new Action<object>(this.OnOverlayChanged));
		this.priorityScreen = Util.KInstantiateUI<PriorityScreen>(this.Prefab_priorityScreen.gameObject, base.gameObject, false);
		this.priorityScreen.InstantiateButtons(new Action<PrioritySetting>(this.OnPriorityClicked), false);
	}

	// Token: 0x06004E50 RID: 20048 RVA: 0x001BC01E File Offset: 0x001BA21E
	protected override void OnForcedCleanUp()
	{
		KInputManager.InputChange.RemoveListener(new UnityAction(this.OnInputChange));
		base.OnForcedCleanUp();
	}

	// Token: 0x06004E51 RID: 20049 RVA: 0x001BC03C File Offset: 0x001BA23C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Game.Instance.Unsubscribe(1798162660, new Action<object>(this.OnOverlayChanged));
		Game.Instance.Unsubscribe(this.refreshScaleHandle);
	}

	// Token: 0x06004E52 RID: 20050 RVA: 0x001BC070 File Offset: 0x001BA270
	private void OnOverlayChanged(object overlay_data)
	{
		HashedString hashedString = (HashedString)overlay_data;
		if (PlayerController.Instance.ActiveTool != null && PlayerController.Instance.ActiveTool.ViewMode != OverlayModes.None.ID && PlayerController.Instance.ActiveTool.ViewMode != hashedString)
		{
			this.ChooseCollection(null, true);
			this.ChooseTool(null);
		}
	}

	// Token: 0x06004E53 RID: 20051 RVA: 0x001BC0D8 File Offset: 0x001BA2D8
	protected override void OnSpawn()
	{
		this.activateOnSpawn = true;
		base.OnSpawn();
		this.CreateSandBoxTools();
		this.CreateBasicTools();
		this.rows.Add(this.sandboxTools);
		this.rows.Add(this.basicTools);
		this.rows.ForEach(delegate(List<ToolMenu.ToolCollection> row)
		{
			this.InstantiateCollectionsUI(row);
		});
		this.rows.ForEach(delegate(List<ToolMenu.ToolCollection> row)
		{
			this.BuildRowToggles(row);
		});
		this.rows.ForEach(delegate(List<ToolMenu.ToolCollection> row)
		{
			this.BuildToolToggles(row);
		});
		this.ChooseCollection(null, true);
		this.priorityScreen.gameObject.SetActive(false);
		this.ToggleSandboxUI(null);
		KInputManager.InputChange.AddListener(new UnityAction(this.OnInputChange));
		Game.Instance.Subscribe(-1948169901, new Action<object>(this.ToggleSandboxUI));
		this.ResetToolDisplayPlane();
		this.refreshScaleHandle = Game.Instance.Subscribe(-442024484, new Action<object>(this.RefreshScale));
		this.RefreshScale(null);
	}

	// Token: 0x06004E54 RID: 20052 RVA: 0x001BC1E8 File Offset: 0x001BA3E8
	private void RefreshScale(object data = null)
	{
		int num = 14;
		int num2 = 16;
		foreach (ToolMenu.ToolCollection toolCollection in this.sandboxTools)
		{
			LocText componentInChildren = toolCollection.toggle.GetComponentInChildren<LocText>();
			if (componentInChildren != null)
			{
				componentInChildren.fontSize = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? num2 : num);
			}
		}
		foreach (ToolMenu.ToolCollection toolCollection2 in this.basicTools)
		{
			LocText componentInChildren2 = toolCollection2.toggle.GetComponentInChildren<LocText>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.fontSize = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? num2 : num);
			}
		}
	}

	// Token: 0x06004E55 RID: 20053 RVA: 0x001BC2C4 File Offset: 0x001BA4C4
	public void OnInputChange()
	{
		this.rows.ForEach(delegate(List<ToolMenu.ToolCollection> row)
		{
			this.BuildRowToggles(row);
		});
		this.rows.ForEach(delegate(List<ToolMenu.ToolCollection> row)
		{
			this.BuildToolToggles(row);
		});
	}

	// Token: 0x06004E56 RID: 20054 RVA: 0x001BC2F4 File Offset: 0x001BA4F4
	private void ResetToolDisplayPlane()
	{
		this.toolEffectDisplayPlane = this.CreateToolDisplayPlane("Overlay", World.Instance.transform);
		this.toolEffectDisplayPlaneTexture = this.CreatePlaneTexture(out this.toolEffectDisplayBytes, Grid.WidthInCells, Grid.HeightInCells);
		this.toolEffectDisplayPlane.GetComponent<Renderer>().sharedMaterial = this.toolEffectDisplayMaterial;
		this.toolEffectDisplayPlane.GetComponent<Renderer>().sharedMaterial.mainTexture = this.toolEffectDisplayPlaneTexture;
		this.toolEffectDisplayPlane.transform.SetLocalPosition(new Vector3(Grid.WidthInMeters / 2f, Grid.HeightInMeters / 2f, -6f));
		this.RefreshToolDisplayPlaneColor();
	}

	// Token: 0x06004E57 RID: 20055 RVA: 0x001BC3A0 File Offset: 0x001BA5A0
	private GameObject CreateToolDisplayPlane(string layer, Transform parent)
	{
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
		gameObject.name = "toolEffectDisplayPlane";
		gameObject.SetLayerRecursively(LayerMask.NameToLayer(layer));
		UnityEngine.Object.Destroy(gameObject.GetComponent<Collider>());
		if (parent != null)
		{
			gameObject.transform.SetParent(parent);
		}
		gameObject.transform.SetPosition(Vector3.zero);
		gameObject.transform.localScale = new Vector3(Grid.WidthInMeters / -10f, 1f, Grid.HeightInMeters / -10f);
		gameObject.transform.eulerAngles = new Vector3(270f, 0f, 0f);
		gameObject.GetComponent<MeshRenderer>().reflectionProbeUsage = ReflectionProbeUsage.Off;
		return gameObject;
	}

	// Token: 0x06004E58 RID: 20056 RVA: 0x001BC453 File Offset: 0x001BA653
	private Texture2D CreatePlaneTexture(out byte[] textureBytes, int width, int height)
	{
		textureBytes = new byte[width * height * 4];
		return new Texture2D(width, height, TextureUtil.TextureFormatToGraphicsFormat(TextureFormat.RGBA32), TextureCreationFlags.None)
		{
			name = "toolEffectDisplayPlane",
			wrapMode = TextureWrapMode.Clamp,
			filterMode = FilterMode.Point
		};
	}

	// Token: 0x06004E59 RID: 20057 RVA: 0x001BC488 File Offset: 0x001BA688
	private void Update()
	{
		this.RefreshToolDisplayPlaneColor();
	}

	// Token: 0x06004E5A RID: 20058 RVA: 0x001BC490 File Offset: 0x001BA690
	private void RefreshToolDisplayPlaneColor()
	{
		if (PlayerController.Instance.ActiveTool == null || PlayerController.Instance.ActiveTool == SelectTool.Instance)
		{
			this.toolEffectDisplayPlane.SetActive(false);
			return;
		}
		PlayerController.Instance.ActiveTool.GetOverlayColorData(out this.colors);
		Array.Clear(this.toolEffectDisplayBytes, 0, this.toolEffectDisplayBytes.Length);
		if (this.colors != null)
		{
			foreach (ToolMenu.CellColorData cellColorData in this.colors)
			{
				if (Grid.IsValidCell(cellColorData.cell))
				{
					int num = cellColorData.cell * 4;
					if (num >= 0)
					{
						this.toolEffectDisplayBytes[num] = (byte)(Mathf.Min(cellColorData.color.r, 1f) * 255f);
						this.toolEffectDisplayBytes[num + 1] = (byte)(Mathf.Min(cellColorData.color.g, 1f) * 255f);
						this.toolEffectDisplayBytes[num + 2] = (byte)(Mathf.Min(cellColorData.color.b, 1f) * 255f);
						this.toolEffectDisplayBytes[num + 3] = (byte)(Mathf.Min(cellColorData.color.a, 1f) * 255f);
					}
				}
			}
		}
		if (!this.toolEffectDisplayPlane.activeSelf)
		{
			this.toolEffectDisplayPlane.SetActive(true);
		}
		this.toolEffectDisplayPlaneTexture.LoadRawTextureData(this.toolEffectDisplayBytes);
		this.toolEffectDisplayPlaneTexture.Apply();
	}

	// Token: 0x06004E5B RID: 20059 RVA: 0x001BC638 File Offset: 0x001BA838
	public void ToggleSandboxUI(object data = null)
	{
		this.ClearSelection();
		PlayerController.Instance.ActivateTool(SelectTool.Instance);
		this.sandboxTools[0].toggle.transform.parent.transform.parent.gameObject.SetActive(Game.Instance.SandboxModeActive);
	}

	// Token: 0x06004E5C RID: 20060 RVA: 0x001BC694 File Offset: 0x001BA894
	public static ToolMenu.ToolCollection CreateToolCollection(LocString collection_name, string icon_name, global::Action hotkey, string tool_name, LocString tooltip, bool largeIcon)
	{
		ToolMenu.ToolCollection toolCollection = new ToolMenu.ToolCollection(collection_name, icon_name, "", false, global::Action.NumActions, largeIcon);
		new ToolMenu.ToolInfo(collection_name, icon_name, hotkey, tool_name, toolCollection, tooltip, null, null);
		return toolCollection;
	}

	// Token: 0x06004E5D RID: 20061 RVA: 0x001BC6D8 File Offset: 0x001BA8D8
	private void CreateSandBoxTools()
	{
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.BRUSH.NAME, "brush", global::Action.SandboxBrush, "SandboxBrushTool", UI.SANDBOXTOOLS.SETTINGS.BRUSH.TOOLTIP, false));
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.SPRINKLE.NAME, "sprinkle", global::Action.SandboxSprinkle, "SandboxSprinkleTool", UI.SANDBOXTOOLS.SETTINGS.SPRINKLE.TOOLTIP, false));
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.FLOOD.NAME, "flood", global::Action.SandboxFlood, "SandboxFloodTool", UI.SANDBOXTOOLS.SETTINGS.FLOOD.TOOLTIP, false));
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.SAMPLE.NAME, "sample", global::Action.SandboxSample, "SandboxSampleTool", UI.SANDBOXTOOLS.SETTINGS.SAMPLE.TOOLTIP, false));
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.HEATGUN.NAME, "temperature", global::Action.SandboxHeatGun, "SandboxHeatTool", UI.SANDBOXTOOLS.SETTINGS.HEATGUN.TOOLTIP, false));
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.STRESSTOOL.NAME, "crew_state_happy", global::Action.SandboxStressTool, "SandboxStressTool", UI.SANDBOXTOOLS.SETTINGS.STRESS.TOOLTIP, false));
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.SPAWNER.NAME, "spawn", global::Action.SandboxSpawnEntity, "SandboxSpawnerTool", UI.SANDBOXTOOLS.SETTINGS.SPAWNER.TOOLTIP, false));
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.CLEAR_FLOOR.NAME, "clear_floor", global::Action.SandboxClearFloor, "SandboxClearFloorTool", UI.SANDBOXTOOLS.SETTINGS.CLEAR_FLOOR.TOOLTIP, false));
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.DESTROY.NAME, "destroy", global::Action.SandboxDestroy, "SandboxDestroyerTool", UI.SANDBOXTOOLS.SETTINGS.DESTROY.TOOLTIP, false));
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.FOW.NAME, "reveal", global::Action.SandboxReveal, "SandboxFOWTool", UI.SANDBOXTOOLS.SETTINGS.FOW.TOOLTIP, false));
		this.sandboxTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.SANDBOX.CRITTER.NAME, "critter", global::Action.SandboxCritterTool, "SandboxCritterTool", UI.SANDBOXTOOLS.SETTINGS.CRITTER.TOOLTIP, false));
	}

	// Token: 0x06004E5E RID: 20062 RVA: 0x001BC8B4 File Offset: 0x001BAAB4
	private void CreateBasicTools()
	{
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.DIG.NAME, "icon_action_dig", global::Action.Dig, "DigTool", UI.TOOLTIPS.DIGBUTTON, true));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.CANCEL.NAME, "icon_action_cancel", global::Action.BuildingCancel, "CancelTool", UI.TOOLTIPS.CANCELBUTTON, true));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.DECONSTRUCT.NAME, "icon_action_deconstruct", global::Action.BuildingDeconstruct, "DeconstructTool", UI.TOOLTIPS.DECONSTRUCTBUTTON, true));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.PRIORITIZE.NAME, "icon_action_prioritize", global::Action.Prioritize, "PrioritizeTool", UI.TOOLTIPS.PRIORITIZEBUTTON, true));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.DISINFECT.NAME, "icon_action_disinfect", global::Action.Disinfect, "DisinfectTool", UI.TOOLTIPS.DISINFECTBUTTON, false));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.MARKFORSTORAGE.NAME, "icon_action_store", global::Action.Clear, "ClearTool", UI.TOOLTIPS.CLEARBUTTON, false));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.ATTACK.NAME, "icon_action_attack", global::Action.Attack, "AttackTool", UI.TOOLTIPS.ATTACKBUTTON, false));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.MOP.NAME, "icon_action_mop", global::Action.Mop, "MopTool", UI.TOOLTIPS.MOPBUTTON, false));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.CAPTURE.NAME, "icon_action_capture", global::Action.Capture, "CaptureTool", UI.TOOLTIPS.CAPTUREBUTTON, false));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.HARVEST.NAME, "icon_action_harvest", global::Action.Harvest, "HarvestTool", UI.TOOLTIPS.HARVESTBUTTON, false));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.EMPTY_PIPE.NAME, "icon_action_empty_pipes", global::Action.EmptyPipe, "EmptyPipeTool", UI.TOOLS.EMPTY_PIPE.TOOLTIP, false));
		this.basicTools.Add(ToolMenu.CreateToolCollection(UI.TOOLS.DISCONNECT.NAME, "icon_action_disconnect", global::Action.Disconnect, "DisconnectTool", UI.TOOLS.DISCONNECT.TOOLTIP, false));
	}

	// Token: 0x06004E5F RID: 20063 RVA: 0x001BCABC File Offset: 0x001BACBC
	private void InstantiateCollectionsUI(IList<ToolMenu.ToolCollection> collections)
	{
		GameObject gameObject = Util.KInstantiateUI(this.prefabToolRow, base.gameObject, true);
		GameObject gameObject2 = Util.KInstantiateUI(this.largeToolSet, gameObject, true);
		GameObject gameObject3 = Util.KInstantiateUI(this.smallToolSet, gameObject, true);
		GameObject gameObject4 = Util.KInstantiateUI(this.smallToolBottomRow, gameObject3, true);
		GameObject gameObject5 = Util.KInstantiateUI(this.smallToolTopRow, gameObject3, true);
		GameObject gameObject6 = Util.KInstantiateUI(this.sandboxToolSet, gameObject, true);
		bool flag = true;
		int num = 0;
		for (int i = 0; i < collections.Count; i++)
		{
			GameObject gameObject7;
			if (collections == this.sandboxTools)
			{
				gameObject7 = gameObject6;
			}
			else if (collections[i].largeIcon)
			{
				gameObject7 = gameObject2;
			}
			else
			{
				gameObject7 = (flag ? gameObject5 : gameObject4);
				flag = !flag;
				num++;
			}
			ToolMenu.ToolCollection tc = collections[i];
			tc.toggle = Util.KInstantiateUI((collections[i].tools.Count > 1) ? this.collectionIconPrefab : ((collections == this.sandboxTools) ? this.sandboxToolIconPrefab : (collections[i].largeIcon ? this.toolIconLargePrefab : this.toolIconPrefab)), gameObject7, true);
			KToggle component = tc.toggle.GetComponent<KToggle>();
			component.soundPlayer.Enabled = false;
			component.onClick += delegate
			{
				if (this.currentlySelectedCollection == tc && tc.tools.Count >= 1)
				{
					KMonoBehaviour.PlaySound(GlobalAssets.GetSound(PlayerController.Instance.ActiveTool.GetDeactivateSound(), false));
				}
				this.ChooseCollection(tc, true);
			};
			if (tc.tools != null)
			{
				GameObject gameObject8;
				if (tc.tools.Count < this.smallCollectionMax)
				{
					gameObject8 = Util.KInstantiateUI(this.Prefab_collectionContainer, gameObject7, true);
					gameObject8.transform.SetSiblingIndex(gameObject8.transform.GetSiblingIndex() - 1);
					gameObject8.transform.localScale = Vector3.one;
					gameObject8.rectTransform().sizeDelta = new Vector2((float)(tc.tools.Count * 75), 50f);
					tc.MaskContainer = gameObject8.GetComponentInChildren<Mask>().gameObject;
					gameObject8.SetActive(false);
				}
				else
				{
					gameObject8 = Util.KInstantiateUI(this.Prefab_collectionContainerWindow, gameObject7, true);
					gameObject8.transform.localScale = Vector3.one;
					gameObject8.GetComponentInChildren<LocText>().SetText(tc.text.ToUpper());
					tc.MaskContainer = gameObject8.GetComponentInChildren<GridLayoutGroup>().gameObject;
					gameObject8.SetActive(false);
				}
				tc.UIMenuDisplay = gameObject8;
				Action<object> <>9__2;
				for (int j = 0; j < tc.tools.Count; j++)
				{
					ToolMenu.ToolInfo ti = tc.tools[j];
					GameObject gameObject9 = Util.KInstantiateUI((collections == this.sandboxTools) ? this.sandboxToolIconPrefab : (collections[i].largeIcon ? this.toolIconLargePrefab : this.toolIconPrefab), tc.MaskContainer, true);
					gameObject9.name = ti.text;
					ti.toggle = gameObject9.GetComponent<KToggle>();
					if (ti.collection.tools.Count > 1)
					{
						RectTransform rectTransform = ti.toggle.gameObject.GetComponentInChildren<SetTextStyleSetting>().rectTransform();
						if (gameObject9.name.Length > 12)
						{
							rectTransform.GetComponent<SetTextStyleSetting>().SetStyle(this.CategoryLabelTextStyle_LeftAlign);
							rectTransform.anchoredPosition = new Vector2(16f, rectTransform.anchoredPosition.y);
						}
					}
					ti.toggle.onClick += delegate
					{
						this.ChooseTool(ti);
					};
					ExpandRevealUIContent component2 = tc.UIMenuDisplay.GetComponent<ExpandRevealUIContent>();
					Action<object> action;
					if ((action = <>9__2) == null)
					{
						action = (<>9__2 = delegate(object s)
						{
							this.SetToggleState(tc.toggle.GetComponent<KToggle>(), false);
							tc.UIMenuDisplay.SetActive(false);
						});
					}
					component2.Collapse(action);
				}
			}
		}
		if (num > 0 && num % 2 == 0)
		{
			gameObject4.GetComponent<HorizontalLayoutGroup>().padding.left = 26;
			gameObject5.GetComponent<HorizontalLayoutGroup>().padding.right = 26;
		}
		if (gameObject2.transform.childCount == 0)
		{
			UnityEngine.Object.Destroy(gameObject2);
		}
		if (gameObject4.transform.childCount == 0 && gameObject5.transform.childCount == 0)
		{
			UnityEngine.Object.Destroy(gameObject3);
		}
		if (gameObject6.transform.childCount == 0)
		{
			UnityEngine.Object.Destroy(gameObject6);
		}
	}

	// Token: 0x06004E60 RID: 20064 RVA: 0x001BCF78 File Offset: 0x001BB178
	private void ChooseTool(ToolMenu.ToolInfo tool)
	{
		if (this.currentlySelectedTool == tool)
		{
			return;
		}
		if (this.currentlySelectedTool != tool)
		{
			this.currentlySelectedTool = tool;
			if (this.currentlySelectedTool != null && this.currentlySelectedTool.onSelectCallback != null)
			{
				this.currentlySelectedTool.onSelectCallback(this.currentlySelectedTool);
			}
		}
		if (this.currentlySelectedTool != null)
		{
			this.currentlySelectedCollection = this.currentlySelectedTool.collection;
			foreach (InterfaceTool interfaceTool in PlayerController.Instance.tools)
			{
				if (this.currentlySelectedTool.toolName == interfaceTool.name)
				{
					UISounds.PlaySound(UISounds.Sound.ClickObject);
					this.activeTool = interfaceTool;
					PlayerController.Instance.ActivateTool(interfaceTool);
					break;
				}
			}
		}
		else
		{
			PlayerController.Instance.ActivateTool(SelectTool.Instance);
		}
		this.rows.ForEach(delegate(List<ToolMenu.ToolCollection> row)
		{
			this.RefreshRowDisplay(row);
		});
	}

	// Token: 0x06004E61 RID: 20065 RVA: 0x001BD05C File Offset: 0x001BB25C
	private void RefreshRowDisplay(IList<ToolMenu.ToolCollection> row)
	{
		for (int i = 0; i < row.Count; i++)
		{
			ToolMenu.ToolCollection tc = row[i];
			if (this.currentlySelectedTool != null && this.currentlySelectedTool.collection == tc)
			{
				if (!tc.UIMenuDisplay.activeSelf || tc.UIMenuDisplay.GetComponent<ExpandRevealUIContent>().Collapsing)
				{
					if (tc.tools.Count > 1)
					{
						tc.UIMenuDisplay.SetActive(true);
						if (tc.tools.Count < this.smallCollectionMax)
						{
							float num = Mathf.Clamp(1f - (float)tc.tools.Count * 0.15f, 0.5f, 1f);
							tc.UIMenuDisplay.GetComponent<ExpandRevealUIContent>().speedScale = num;
						}
						tc.UIMenuDisplay.GetComponent<ExpandRevealUIContent>().Expand(delegate(object s)
						{
							this.SetToggleState(tc.toggle.GetComponent<KToggle>(), true);
						});
					}
					else
					{
						this.currentlySelectedTool = tc.tools[0];
					}
				}
			}
			else if (tc.UIMenuDisplay.activeSelf && !tc.UIMenuDisplay.GetComponent<ExpandRevealUIContent>().Collapsing && tc.tools.Count > 0)
			{
				tc.UIMenuDisplay.GetComponent<ExpandRevealUIContent>().Collapse(delegate(object s)
				{
					this.SetToggleState(tc.toggle.GetComponent<KToggle>(), false);
					tc.UIMenuDisplay.SetActive(false);
				});
			}
			for (int j = 0; j < tc.tools.Count; j++)
			{
				if (tc.tools[j] == this.currentlySelectedTool)
				{
					this.SetToggleState(tc.tools[j].toggle, true);
				}
				else
				{
					this.SetToggleState(tc.tools[j].toggle, false);
				}
			}
		}
	}

	// Token: 0x06004E62 RID: 20066 RVA: 0x001BD272 File Offset: 0x001BB472
	public void TurnLargeCollectionOff()
	{
		if (this.currentlySelectedCollection != null && this.currentlySelectedCollection.tools.Count > this.smallCollectionMax)
		{
			this.ChooseCollection(null, true);
		}
	}

	// Token: 0x06004E63 RID: 20067 RVA: 0x001BD29C File Offset: 0x001BB49C
	private void ChooseCollection(ToolMenu.ToolCollection collection, bool autoSelectTool = true)
	{
		if (collection == this.currentlySelectedCollection)
		{
			if (collection != null && collection.tools.Count > 1)
			{
				this.currentlySelectedCollection = null;
				if (this.currentlySelectedTool != null)
				{
					this.ChooseTool(null);
				}
			}
			else if (this.currentlySelectedTool != null && this.currentlySelectedCollection.tools.Contains(this.currentlySelectedTool) && this.currentlySelectedCollection.tools.Count == 1)
			{
				this.currentlySelectedCollection = null;
				this.ChooseTool(null);
			}
		}
		else
		{
			this.currentlySelectedCollection = collection;
		}
		this.rows.ForEach(delegate(List<ToolMenu.ToolCollection> row)
		{
			this.OpenOrCloseCollectionsInRow(row, true);
		});
	}

	// Token: 0x06004E64 RID: 20068 RVA: 0x001BD33C File Offset: 0x001BB53C
	private void OpenOrCloseCollectionsInRow(IList<ToolMenu.ToolCollection> row, bool autoSelectTool = true)
	{
		for (int i = 0; i < row.Count; i++)
		{
			ToolMenu.ToolCollection tc = row[i];
			if (this.currentlySelectedCollection == tc)
			{
				if ((this.currentlySelectedCollection.tools != null && this.currentlySelectedCollection.tools.Count == 1) || autoSelectTool)
				{
					this.ChooseTool(this.currentlySelectedCollection.tools[0]);
				}
			}
			else if (tc.UIMenuDisplay.activeSelf && !tc.UIMenuDisplay.GetComponent<ExpandRevealUIContent>().Collapsing)
			{
				tc.UIMenuDisplay.GetComponent<ExpandRevealUIContent>().Collapse(delegate(object s)
				{
					this.SetToggleState(tc.toggle.GetComponent<KToggle>(), false);
					tc.UIMenuDisplay.SetActive(false);
				});
			}
			this.SetToggleState(tc.toggle.GetComponent<KToggle>(), this.currentlySelectedCollection == tc);
		}
	}

	// Token: 0x06004E65 RID: 20069 RVA: 0x001BD436 File Offset: 0x001BB636
	private void SetToggleState(KToggle toggle, bool state)
	{
		if (state)
		{
			toggle.Select();
			toggle.isOn = true;
			return;
		}
		toggle.Deselect();
		toggle.isOn = false;
	}

	// Token: 0x06004E66 RID: 20070 RVA: 0x001BD456 File Offset: 0x001BB656
	public void ClearSelection()
	{
		if (this.currentlySelectedCollection != null)
		{
			this.ChooseCollection(null, true);
		}
		if (this.currentlySelectedTool != null)
		{
			this.ChooseTool(null);
		}
	}

	// Token: 0x06004E67 RID: 20071 RVA: 0x001BD478 File Offset: 0x001BB678
	public override void OnKeyDown(KButtonEvent e)
	{
		if (!e.Consumed)
		{
			if (e.IsAction(global::Action.ToggleSandboxTools))
			{
				if (Application.isEditor)
				{
					DebugUtil.LogArgs(new object[] { "Force-enabling sandbox mode because we're in editor." });
					SaveGame.Instance.sandboxEnabled = true;
				}
				if (SaveGame.Instance.sandboxEnabled)
				{
					Game.Instance.SandboxModeActive = !Game.Instance.SandboxModeActive;
				}
			}
			foreach (List<ToolMenu.ToolCollection> list in this.rows)
			{
				if (list != this.sandboxTools || Game.Instance.SandboxModeActive)
				{
					int i = 0;
					while (i < list.Count)
					{
						global::Action toolHotkey = list[i].hotkey;
						if (toolHotkey != global::Action.NumActions && e.IsAction(toolHotkey) && (this.currentlySelectedCollection == null || (this.currentlySelectedCollection != null && this.currentlySelectedCollection.tools.Find((ToolMenu.ToolInfo t) => GameInputMapping.CompareActionKeyCodes(t.hotkey, toolHotkey)) == null)))
						{
							if (this.currentlySelectedCollection != list[i])
							{
								this.ChooseCollection(list[i], false);
								this.ChooseTool(list[i].tools[0]);
								break;
							}
							if (this.currentlySelectedCollection.tools.Count <= 1)
							{
								break;
							}
							e.Consumed = true;
							this.ChooseCollection(null, true);
							this.ChooseTool(null);
							string sound = GlobalAssets.GetSound(PlayerController.Instance.ActiveTool.GetDeactivateSound(), false);
							if (sound != null)
							{
								KMonoBehaviour.PlaySound(sound);
								break;
							}
							break;
						}
						else
						{
							for (int j = 0; j < list[i].tools.Count; j++)
							{
								if ((this.currentlySelectedCollection == null && list[i].tools.Count == 1) || this.currentlySelectedCollection == list[i] || (this.currentlySelectedCollection != null && this.currentlySelectedCollection.tools.Count == 1 && list[i].tools.Count == 1))
								{
									global::Action hotkey = list[i].tools[j].hotkey;
									if (e.IsAction(hotkey) && e.TryConsume(hotkey))
									{
										if (list[i].tools.Count == 1 && this.currentlySelectedCollection != list[i])
										{
											this.ChooseCollection(list[i], false);
										}
										else if (this.currentlySelectedTool != list[i].tools[j])
										{
											this.ChooseTool(list[i].tools[j]);
										}
									}
									else if (GameInputMapping.CompareActionKeyCodes(e.GetAction(), hotkey))
									{
										e.Consumed = true;
									}
								}
							}
							i++;
						}
					}
				}
			}
			if ((this.currentlySelectedTool != null || this.currentlySelectedCollection != null) && !e.Consumed)
			{
				if (e.TryConsume(global::Action.Escape))
				{
					string sound2 = GlobalAssets.GetSound(PlayerController.Instance.ActiveTool.GetDeactivateSound(), false);
					if (sound2 != null)
					{
						KMonoBehaviour.PlaySound(sound2);
					}
					if (this.currentlySelectedCollection != null)
					{
						this.ChooseCollection(null, true);
					}
					if (this.currentlySelectedTool != null)
					{
						this.ChooseTool(null);
					}
					SelectTool.Instance.Activate();
				}
			}
			else if (!PlayerController.Instance.IsUsingDefaultTool() && !e.Consumed && e.TryConsume(global::Action.Escape))
			{
				SelectTool.Instance.Activate();
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06004E68 RID: 20072 RVA: 0x001BD834 File Offset: 0x001BBA34
	public override void OnKeyUp(KButtonEvent e)
	{
		if (!e.Consumed)
		{
			if ((this.currentlySelectedTool != null || this.currentlySelectedCollection != null) && !e.Consumed)
			{
				if (PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
				{
					string sound = GlobalAssets.GetSound(PlayerController.Instance.ActiveTool.GetDeactivateSound(), false);
					if (sound != null)
					{
						KMonoBehaviour.PlaySound(sound);
					}
					if (this.currentlySelectedCollection != null)
					{
						this.ChooseCollection(null, true);
					}
					if (this.currentlySelectedTool != null)
					{
						this.ChooseTool(null);
					}
					SelectTool.Instance.Activate();
				}
			}
			else if (!PlayerController.Instance.IsUsingDefaultTool() && !e.Consumed && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
			{
				SelectTool.Instance.Activate();
				string sound2 = GlobalAssets.GetSound(PlayerController.Instance.ActiveTool.GetDeactivateSound(), false);
				if (sound2 != null)
				{
					KMonoBehaviour.PlaySound(sound2);
				}
			}
		}
		base.OnKeyUp(e);
	}

	// Token: 0x06004E69 RID: 20073 RVA: 0x001BD914 File Offset: 0x001BBB14
	protected void BuildRowToggles(IList<ToolMenu.ToolCollection> row)
	{
		for (int i = 0; i < row.Count; i++)
		{
			ToolMenu.ToolCollection toolCollection = row[i];
			if (!(toolCollection.toggle == null))
			{
				GameObject toggle = toolCollection.toggle;
				Sprite sprite = Assets.GetSprite(toolCollection.icon);
				if (sprite != null)
				{
					toggle.transform.Find("FG").GetComponent<Image>().sprite = sprite;
				}
				Transform transform = toggle.transform.Find("Text");
				if (transform != null)
				{
					LocText component = transform.GetComponent<LocText>();
					if (component != null)
					{
						component.text = toolCollection.text;
					}
				}
				ToolTip component2 = toggle.GetComponent<ToolTip>();
				if (component2)
				{
					if (row[i].tools.Count == 1)
					{
						string text = GameUtil.ReplaceHotkeyString(row[i].tools[0].tooltip, row[i].tools[0].hotkey);
						component2.ClearMultiStringTooltip();
						component2.AddMultiStringTooltip(row[i].tools[0].text, this.TooltipHeader);
						component2.AddMultiStringTooltip(text, this.ToggleToolTipTextStyleSetting);
					}
					else
					{
						string text2 = row[i].tooltip;
						if (row[i].hotkey != global::Action.NumActions)
						{
							text2 = GameUtil.ReplaceHotkeyString(text2, row[i].hotkey);
						}
						component2.ClearMultiStringTooltip();
						component2.AddMultiStringTooltip(text2, this.ToggleToolTipTextStyleSetting);
					}
				}
			}
		}
	}

	// Token: 0x06004E6A RID: 20074 RVA: 0x001BDAB0 File Offset: 0x001BBCB0
	protected void BuildToolToggles(IList<ToolMenu.ToolCollection> row)
	{
		for (int i = 0; i < row.Count; i++)
		{
			ToolMenu.ToolCollection toolCollection = row[i];
			if (!(toolCollection.toggle == null))
			{
				for (int j = 0; j < toolCollection.tools.Count; j++)
				{
					GameObject gameObject = toolCollection.tools[j].toggle.gameObject;
					Sprite sprite = Assets.GetSprite(toolCollection.icon);
					if (sprite != null)
					{
						gameObject.transform.Find("FG").GetComponent<Image>().sprite = sprite;
					}
					Transform transform = gameObject.transform.Find("Text");
					if (transform != null)
					{
						LocText component = transform.GetComponent<LocText>();
						if (component != null)
						{
							component.text = toolCollection.tools[j].text;
						}
					}
					ToolTip component2 = gameObject.GetComponent<ToolTip>();
					if (component2)
					{
						string text = ((toolCollection.tools.Count > 1) ? GameUtil.ReplaceHotkeyString(toolCollection.tools[j].tooltip, toolCollection.hotkey, toolCollection.tools[j].hotkey) : GameUtil.ReplaceHotkeyString(toolCollection.tools[j].tooltip, toolCollection.tools[j].hotkey));
						component2.ClearMultiStringTooltip();
						component2.AddMultiStringTooltip(text, this.ToggleToolTipTextStyleSetting);
					}
				}
			}
		}
	}

	// Token: 0x06004E6B RID: 20075 RVA: 0x001BDC2C File Offset: 0x001BBE2C
	public bool HasUniqueKeyBindings()
	{
		bool flag = true;
		this.boundRootActions.Clear();
		foreach (List<ToolMenu.ToolCollection> list in this.rows)
		{
			foreach (ToolMenu.ToolCollection toolCollection in list)
			{
				if (this.boundRootActions.Contains(toolCollection.hotkey))
				{
					flag = false;
					break;
				}
				this.boundRootActions.Add(toolCollection.hotkey);
				this.boundSubgroupActions.Clear();
				foreach (ToolMenu.ToolInfo toolInfo in toolCollection.tools)
				{
					if (this.boundSubgroupActions.Contains(toolInfo.hotkey))
					{
						flag = false;
						break;
					}
					this.boundSubgroupActions.Add(toolInfo.hotkey);
				}
			}
		}
		return flag;
	}

	// Token: 0x06004E6C RID: 20076 RVA: 0x001BDD68 File Offset: 0x001BBF68
	private void OnPriorityClicked(PrioritySetting priority)
	{
		this.priorityScreen.SetScreenPriority(priority, false);
	}

	// Token: 0x040033BE RID: 13246
	public static ToolMenu Instance;

	// Token: 0x040033BF RID: 13247
	public GameObject Prefab_collectionContainer;

	// Token: 0x040033C0 RID: 13248
	public GameObject Prefab_collectionContainerWindow;

	// Token: 0x040033C1 RID: 13249
	public PriorityScreen Prefab_priorityScreen;

	// Token: 0x040033C2 RID: 13250
	public GameObject toolIconPrefab;

	// Token: 0x040033C3 RID: 13251
	public GameObject toolIconLargePrefab;

	// Token: 0x040033C4 RID: 13252
	public GameObject sandboxToolIconPrefab;

	// Token: 0x040033C5 RID: 13253
	public GameObject collectionIconPrefab;

	// Token: 0x040033C6 RID: 13254
	public GameObject prefabToolRow;

	// Token: 0x040033C7 RID: 13255
	public GameObject largeToolSet;

	// Token: 0x040033C8 RID: 13256
	public GameObject smallToolSet;

	// Token: 0x040033C9 RID: 13257
	public GameObject smallToolBottomRow;

	// Token: 0x040033CA RID: 13258
	public GameObject smallToolTopRow;

	// Token: 0x040033CB RID: 13259
	public GameObject sandboxToolSet;

	// Token: 0x040033CC RID: 13260
	private PriorityScreen priorityScreen;

	// Token: 0x040033CD RID: 13261
	public ToolParameterMenu toolParameterMenu;

	// Token: 0x040033CE RID: 13262
	public GameObject sandboxToolParameterMenu;

	// Token: 0x040033CF RID: 13263
	private GameObject toolEffectDisplayPlane;

	// Token: 0x040033D0 RID: 13264
	private Texture2D toolEffectDisplayPlaneTexture;

	// Token: 0x040033D1 RID: 13265
	public Material toolEffectDisplayMaterial;

	// Token: 0x040033D2 RID: 13266
	private byte[] toolEffectDisplayBytes;

	// Token: 0x040033D3 RID: 13267
	private List<List<ToolMenu.ToolCollection>> rows = new List<List<ToolMenu.ToolCollection>>();

	// Token: 0x040033D4 RID: 13268
	public List<ToolMenu.ToolCollection> basicTools = new List<ToolMenu.ToolCollection>();

	// Token: 0x040033D5 RID: 13269
	public List<ToolMenu.ToolCollection> sandboxTools = new List<ToolMenu.ToolCollection>();

	// Token: 0x040033D6 RID: 13270
	public ToolMenu.ToolCollection currentlySelectedCollection;

	// Token: 0x040033D7 RID: 13271
	public ToolMenu.ToolInfo currentlySelectedTool;

	// Token: 0x040033D8 RID: 13272
	public InterfaceTool activeTool;

	// Token: 0x040033D9 RID: 13273
	private Coroutine activeOpenAnimationRoutine;

	// Token: 0x040033DA RID: 13274
	private Coroutine activeCloseAnimationRoutine;

	// Token: 0x040033DB RID: 13275
	private HashSet<global::Action> boundRootActions = new HashSet<global::Action>();

	// Token: 0x040033DC RID: 13276
	private HashSet<global::Action> boundSubgroupActions = new HashSet<global::Action>();

	// Token: 0x040033DD RID: 13277
	private UnityAction inputChangeReceiver;

	// Token: 0x040033DE RID: 13278
	private int refreshScaleHandle = -1;

	// Token: 0x040033DF RID: 13279
	[SerializeField]
	public TextStyleSetting ToggleToolTipTextStyleSetting;

	// Token: 0x040033E0 RID: 13280
	[SerializeField]
	public TextStyleSetting CategoryLabelTextStyle_LeftAlign;

	// Token: 0x040033E1 RID: 13281
	[SerializeField]
	private TextStyleSetting TooltipHeader;

	// Token: 0x040033E2 RID: 13282
	private int smallCollectionMax = 5;

	// Token: 0x040033E3 RID: 13283
	private HashSet<ToolMenu.CellColorData> colors = new HashSet<ToolMenu.CellColorData>();

	// Token: 0x02001854 RID: 6228
	public class ToolInfo
	{
		// Token: 0x06008E13 RID: 36371 RVA: 0x0030BEC4 File Offset: 0x0030A0C4
		public ToolInfo(string text, string icon_name, global::Action hotkey, string ToolName, ToolMenu.ToolCollection toolCollection, string tooltip = "", Action<object> onSelectCallback = null, object toolData = null)
		{
			this.text = text;
			this.icon = icon_name;
			this.hotkey = hotkey;
			this.toolName = ToolName;
			this.collection = toolCollection;
			toolCollection.tools.Add(this);
			this.tooltip = tooltip;
			this.onSelectCallback = onSelectCallback;
			this.toolData = toolData;
		}

		// Token: 0x04007020 RID: 28704
		public string text;

		// Token: 0x04007021 RID: 28705
		public string icon;

		// Token: 0x04007022 RID: 28706
		public global::Action hotkey;

		// Token: 0x04007023 RID: 28707
		public string toolName;

		// Token: 0x04007024 RID: 28708
		public ToolMenu.ToolCollection collection;

		// Token: 0x04007025 RID: 28709
		public string tooltip;

		// Token: 0x04007026 RID: 28710
		public KToggle toggle;

		// Token: 0x04007027 RID: 28711
		public Action<object> onSelectCallback;

		// Token: 0x04007028 RID: 28712
		public object toolData;
	}

	// Token: 0x02001855 RID: 6229
	public class ToolCollection
	{
		// Token: 0x06008E14 RID: 36372 RVA: 0x0030BF21 File Offset: 0x0030A121
		public ToolCollection(string text, string icon_name, string tooltip = "", bool useInfoMenu = false, global::Action hotkey = global::Action.NumActions, bool largeIcon = false)
		{
			this.text = text;
			this.icon = icon_name;
			this.tooltip = tooltip;
			this.useInfoMenu = useInfoMenu;
			this.hotkey = hotkey;
			this.largeIcon = largeIcon;
		}

		// Token: 0x04007029 RID: 28713
		public string text;

		// Token: 0x0400702A RID: 28714
		public string icon;

		// Token: 0x0400702B RID: 28715
		public string tooltip;

		// Token: 0x0400702C RID: 28716
		public bool useInfoMenu;

		// Token: 0x0400702D RID: 28717
		public bool largeIcon;

		// Token: 0x0400702E RID: 28718
		public GameObject toggle;

		// Token: 0x0400702F RID: 28719
		public List<ToolMenu.ToolInfo> tools = new List<ToolMenu.ToolInfo>();

		// Token: 0x04007030 RID: 28720
		public GameObject UIMenuDisplay;

		// Token: 0x04007031 RID: 28721
		public GameObject MaskContainer;

		// Token: 0x04007032 RID: 28722
		public global::Action hotkey;
	}

	// Token: 0x02001856 RID: 6230
	public struct CellColorData
	{
		// Token: 0x06008E15 RID: 36373 RVA: 0x0030BF61 File Offset: 0x0030A161
		public CellColorData(int cell, Color color)
		{
			this.cell = cell;
			this.color = color;
		}

		// Token: 0x04007033 RID: 28723
		public int cell;

		// Token: 0x04007034 RID: 28724
		public Color color;
	}
}
