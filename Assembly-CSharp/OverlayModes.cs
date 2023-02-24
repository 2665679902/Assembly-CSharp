using System;
using System.Collections.Generic;
using FMOD.Studio;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A13 RID: 2579
public abstract class OverlayModes
{
	// Token: 0x0200182C RID: 6188
	public class GasConduits : OverlayModes.ConduitMode
	{
		// Token: 0x06008D09 RID: 36105 RVA: 0x0030495D File Offset: 0x00302B5D
		public override HashedString ViewMode()
		{
			return OverlayModes.GasConduits.ID;
		}

		// Token: 0x06008D0A RID: 36106 RVA: 0x00304964 File Offset: 0x00302B64
		public override string GetSoundName()
		{
			return "GasVent";
		}

		// Token: 0x06008D0B RID: 36107 RVA: 0x0030496B File Offset: 0x00302B6B
		public GasConduits()
			: base(OverlayScreen.GasVentIDs)
		{
		}

		// Token: 0x04006F5E RID: 28510
		public static readonly HashedString ID = "GasConduit";
	}

	// Token: 0x0200182D RID: 6189
	public class LiquidConduits : OverlayModes.ConduitMode
	{
		// Token: 0x06008D0D RID: 36109 RVA: 0x00304989 File Offset: 0x00302B89
		public override HashedString ViewMode()
		{
			return OverlayModes.LiquidConduits.ID;
		}

		// Token: 0x06008D0E RID: 36110 RVA: 0x00304990 File Offset: 0x00302B90
		public override string GetSoundName()
		{
			return "LiquidVent";
		}

		// Token: 0x06008D0F RID: 36111 RVA: 0x00304997 File Offset: 0x00302B97
		public LiquidConduits()
			: base(OverlayScreen.LiquidVentIDs)
		{
		}

		// Token: 0x04006F5F RID: 28511
		public static readonly HashedString ID = "LiquidConduit";
	}

	// Token: 0x0200182E RID: 6190
	public abstract class ConduitMode : OverlayModes.Mode
	{
		// Token: 0x06008D11 RID: 36113 RVA: 0x003049B8 File Offset: 0x00302BB8
		public ConduitMode(ICollection<Tag> ids)
		{
			this.objectTargetLayer = LayerMask.NameToLayer("MaskedOverlayBG");
			this.conduitTargetLayer = LayerMask.NameToLayer("MaskedOverlay");
			this.cameraLayerMask = LayerMask.GetMask(new string[] { "MaskedOverlay", "MaskedOverlayBG" });
			this.selectionMask = this.cameraLayerMask;
			this.targetIDs = ids;
		}

		// Token: 0x06008D12 RID: 36114 RVA: 0x00304A40 File Offset: 0x00302C40
		public override void Enable()
		{
			base.RegisterSaveLoadListeners();
			this.partition = OverlayModes.Mode.PopulatePartition<SaveLoadRoot>(this.targetIDs);
			Camera.main.cullingMask |= this.cameraLayerMask;
			SelectTool.Instance.SetLayerMask(this.selectionMask);
			GridCompositor.Instance.ToggleMinor(false);
			base.Enable();
		}

		// Token: 0x06008D13 RID: 36115 RVA: 0x00304A9C File Offset: 0x00302C9C
		protected override void OnSaveLoadRootRegistered(SaveLoadRoot item)
		{
			Tag saveLoadTag = item.GetComponent<KPrefabID>().GetSaveLoadTag();
			if (this.targetIDs.Contains(saveLoadTag))
			{
				this.partition.Add(item);
			}
		}

		// Token: 0x06008D14 RID: 36116 RVA: 0x00304AD0 File Offset: 0x00302CD0
		protected override void OnSaveLoadRootUnregistered(SaveLoadRoot item)
		{
			if (item == null || item.gameObject == null)
			{
				return;
			}
			if (this.layerTargets.Contains(item))
			{
				this.layerTargets.Remove(item);
			}
			this.partition.Remove(item);
		}

		// Token: 0x06008D15 RID: 36117 RVA: 0x00304B1C File Offset: 0x00302D1C
		public override void Disable()
		{
			foreach (SaveLoadRoot saveLoadRoot in this.layerTargets)
			{
				float defaultDepth = OverlayModes.Mode.GetDefaultDepth(saveLoadRoot);
				Vector3 position = saveLoadRoot.transform.GetPosition();
				position.z = defaultDepth;
				saveLoadRoot.transform.SetPosition(position);
				KBatchedAnimController[] componentsInChildren = saveLoadRoot.GetComponentsInChildren<KBatchedAnimController>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					this.TriggerResorting(componentsInChildren[i]);
				}
			}
			OverlayModes.Mode.ResetDisplayValues<SaveLoadRoot>(this.layerTargets);
			Camera.main.cullingMask &= ~this.cameraLayerMask;
			SelectTool.Instance.ClearLayerMask();
			base.UnregisterSaveLoadListeners();
			this.partition.Clear();
			this.layerTargets.Clear();
			GridCompositor.Instance.ToggleMinor(false);
			base.Disable();
		}

		// Token: 0x06008D16 RID: 36118 RVA: 0x00304C0C File Offset: 0x00302E0C
		public override void Update()
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			OverlayModes.Mode.RemoveOffscreenTargets<SaveLoadRoot>(this.layerTargets, vector2I, vector2I2, delegate(SaveLoadRoot root)
			{
				if (root == null)
				{
					return;
				}
				float defaultDepth = OverlayModes.Mode.GetDefaultDepth(root);
				Vector3 position = root.transform.GetPosition();
				position.z = defaultDepth;
				root.transform.SetPosition(position);
				KBatchedAnimController[] componentsInChildren = root.GetComponentsInChildren<KBatchedAnimController>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					this.TriggerResorting(componentsInChildren[i]);
				}
			});
			foreach (object obj in this.partition.GetAllIntersecting(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y)))
			{
				SaveLoadRoot saveLoadRoot = (SaveLoadRoot)obj;
				if (saveLoadRoot.GetComponent<Conduit>() != null)
				{
					base.AddTargetIfVisible<SaveLoadRoot>(saveLoadRoot, vector2I, vector2I2, this.layerTargets, this.conduitTargetLayer, null, null);
				}
				else
				{
					base.AddTargetIfVisible<SaveLoadRoot>(saveLoadRoot, vector2I, vector2I2, this.layerTargets, this.objectTargetLayer, delegate(SaveLoadRoot root)
					{
						Vector3 position2 = root.transform.GetPosition();
						float num2 = position2.z;
						KPrefabID component3 = root.GetComponent<KPrefabID>();
						if (component3 != null)
						{
							if (component3.HasTag(GameTags.OverlayInFrontOfConduits))
							{
								num2 = Grid.GetLayerZ((this.ViewMode() == OverlayModes.LiquidConduits.ID) ? Grid.SceneLayer.LiquidConduits : Grid.SceneLayer.GasConduits) - 0.2f;
							}
							else if (component3.HasTag(GameTags.OverlayBehindConduits))
							{
								num2 = Grid.GetLayerZ((this.ViewMode() == OverlayModes.LiquidConduits.ID) ? Grid.SceneLayer.LiquidConduits : Grid.SceneLayer.GasConduits) + 0.2f;
							}
						}
						position2.z = num2;
						root.transform.SetPosition(position2);
						KBatchedAnimController[] componentsInChildren2 = root.GetComponentsInChildren<KBatchedAnimController>();
						for (int j = 0; j < componentsInChildren2.Length; j++)
						{
							this.TriggerResorting(componentsInChildren2[j]);
						}
					}, null);
				}
			}
			GameObject gameObject = null;
			if (SelectTool.Instance != null && SelectTool.Instance.hover != null)
			{
				gameObject = SelectTool.Instance.hover.gameObject;
			}
			this.connectedNetworks.Clear();
			float num = 1f;
			if (gameObject != null)
			{
				IBridgedNetworkItem component = gameObject.GetComponent<IBridgedNetworkItem>();
				if (component != null)
				{
					int networkCell = component.GetNetworkCell();
					UtilityNetworkManager<FlowUtilityNetwork, Vent> utilityNetworkManager = ((this.ViewMode() == OverlayModes.LiquidConduits.ID) ? Game.Instance.liquidConduitSystem : Game.Instance.gasConduitSystem);
					this.visited.Clear();
					this.FindConnectedNetworks(networkCell, utilityNetworkManager, this.connectedNetworks, this.visited);
					this.visited.Clear();
					num = OverlayModes.ModeUtil.GetHighlightScale();
				}
			}
			Game.ConduitVisInfo conduitVisInfo = ((this.ViewMode() == OverlayModes.LiquidConduits.ID) ? Game.Instance.liquidConduitVisInfo : Game.Instance.gasConduitVisInfo);
			foreach (SaveLoadRoot saveLoadRoot2 in this.layerTargets)
			{
				if (!(saveLoadRoot2 == null) && saveLoadRoot2.GetComponent<IBridgedNetworkItem>() != null)
				{
					BuildingDef def = saveLoadRoot2.GetComponent<Building>().Def;
					Color32 color;
					if (def.ThermalConductivity == 1f)
					{
						color = GlobalAssets.Instance.colorSet.GetColorByName(conduitVisInfo.overlayTintName);
					}
					else if (def.ThermalConductivity < 1f)
					{
						color = GlobalAssets.Instance.colorSet.GetColorByName(conduitVisInfo.overlayInsulatedTintName);
					}
					else
					{
						color = GlobalAssets.Instance.colorSet.GetColorByName(conduitVisInfo.overlayRadiantTintName);
					}
					if (this.connectedNetworks.Count > 0)
					{
						IBridgedNetworkItem component2 = saveLoadRoot2.GetComponent<IBridgedNetworkItem>();
						if (component2 != null && component2.IsConnectedToNetworks(this.connectedNetworks))
						{
							color.r = (byte)((float)color.r * num);
							color.g = (byte)((float)color.g * num);
							color.b = (byte)((float)color.b * num);
						}
					}
					saveLoadRoot2.GetComponent<KBatchedAnimController>().TintColour = color;
				}
			}
		}

		// Token: 0x06008D17 RID: 36119 RVA: 0x00304F40 File Offset: 0x00303140
		private void TriggerResorting(KBatchedAnimController kbac)
		{
			if (kbac.enabled)
			{
				kbac.enabled = false;
				kbac.enabled = true;
			}
		}

		// Token: 0x06008D18 RID: 36120 RVA: 0x00304F58 File Offset: 0x00303158
		private void FindConnectedNetworks(int cell, IUtilityNetworkMgr mgr, ICollection<UtilityNetwork> networks, List<int> visited)
		{
			if (visited.Contains(cell))
			{
				return;
			}
			visited.Add(cell);
			UtilityNetwork networkForCell = mgr.GetNetworkForCell(cell);
			if (networkForCell != null)
			{
				networks.Add(networkForCell);
				UtilityConnections connections = mgr.GetConnections(cell, false);
				if ((connections & UtilityConnections.Right) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellRight(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Left) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellLeft(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Up) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellAbove(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Down) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellBelow(cell), mgr, networks, visited);
				}
				object endpoint = mgr.GetEndpoint(cell);
				if (endpoint != null)
				{
					FlowUtilityNetwork.NetworkItem networkItem = endpoint as FlowUtilityNetwork.NetworkItem;
					if (networkItem != null)
					{
						IBridgedNetworkItem component = networkItem.GameObject.GetComponent<IBridgedNetworkItem>();
						if (component != null)
						{
							component.AddNetworks(networks);
						}
					}
				}
			}
		}

		// Token: 0x04006F60 RID: 28512
		private UniformGrid<SaveLoadRoot> partition;

		// Token: 0x04006F61 RID: 28513
		private HashSet<SaveLoadRoot> layerTargets = new HashSet<SaveLoadRoot>();

		// Token: 0x04006F62 RID: 28514
		private HashSet<UtilityNetwork> connectedNetworks = new HashSet<UtilityNetwork>();

		// Token: 0x04006F63 RID: 28515
		private List<int> visited = new List<int>();

		// Token: 0x04006F64 RID: 28516
		private ICollection<Tag> targetIDs;

		// Token: 0x04006F65 RID: 28517
		private int objectTargetLayer;

		// Token: 0x04006F66 RID: 28518
		private int conduitTargetLayer;

		// Token: 0x04006F67 RID: 28519
		private int cameraLayerMask;

		// Token: 0x04006F68 RID: 28520
		private int selectionMask;
	}

	// Token: 0x0200182F RID: 6191
	public class Crop : OverlayModes.BasePlantMode
	{
		// Token: 0x06008D1B RID: 36123 RVA: 0x0030513C File Offset: 0x0030333C
		public override HashedString ViewMode()
		{
			return OverlayModes.Crop.ID;
		}

		// Token: 0x06008D1C RID: 36124 RVA: 0x00305143 File Offset: 0x00303343
		public override string GetSoundName()
		{
			return "Harvest";
		}

		// Token: 0x06008D1D RID: 36125 RVA: 0x0030514C File Offset: 0x0030334C
		public Crop(Canvas ui_root, GameObject harvestable_notification_prefab)
		{
			OverlayModes.ColorHighlightCondition[] array = new OverlayModes.ColorHighlightCondition[3];
			array[0] = new OverlayModes.ColorHighlightCondition((KMonoBehaviour h) => GlobalAssets.Instance.colorSet.cropHalted, delegate(KMonoBehaviour h)
			{
				WiltCondition component = h.GetComponent<WiltCondition>();
				return component != null && component.IsWilting();
			});
			array[1] = new OverlayModes.ColorHighlightCondition((KMonoBehaviour h) => GlobalAssets.Instance.colorSet.cropGrowing, (KMonoBehaviour h) => !(h as HarvestDesignatable).CanBeHarvested());
			array[2] = new OverlayModes.ColorHighlightCondition((KMonoBehaviour h) => GlobalAssets.Instance.colorSet.cropGrown, (KMonoBehaviour h) => (h as HarvestDesignatable).CanBeHarvested());
			this.highlightConditions = array;
			base..ctor(OverlayScreen.HarvestableIDs);
			this.uiRoot = ui_root;
			this.harvestableNotificationPrefab = harvestable_notification_prefab;
		}

		// Token: 0x06008D1E RID: 36126 RVA: 0x00305268 File Offset: 0x00303468
		public override List<LegendEntry> GetCustomLegendData()
		{
			return new List<LegendEntry>
			{
				new LegendEntry(UI.OVERLAYS.CROP.FULLY_GROWN, UI.OVERLAYS.CROP.TOOLTIPS.FULLY_GROWN, GlobalAssets.Instance.colorSet.cropGrown, null, null, true),
				new LegendEntry(UI.OVERLAYS.CROP.GROWING, UI.OVERLAYS.CROP.TOOLTIPS.GROWING, GlobalAssets.Instance.colorSet.cropGrowing, null, null, true),
				new LegendEntry(UI.OVERLAYS.CROP.GROWTH_HALTED, UI.OVERLAYS.CROP.TOOLTIPS.GROWTH_HALTED, GlobalAssets.Instance.colorSet.cropHalted, null, null, true)
			};
		}

		// Token: 0x06008D1F RID: 36127 RVA: 0x0030531C File Offset: 0x0030351C
		public override void Update()
		{
			this.updateCropInfo.Clear();
			this.freeHarvestableNotificationIdx = 0;
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			OverlayModes.Mode.RemoveOffscreenTargets<HarvestDesignatable>(this.layerTargets, vector2I, vector2I2, null);
			foreach (object obj in this.partition.GetAllIntersecting(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y)))
			{
				HarvestDesignatable harvestDesignatable = (HarvestDesignatable)obj;
				base.AddTargetIfVisible<HarvestDesignatable>(harvestDesignatable, vector2I, vector2I2, this.layerTargets, this.targetLayer, null, null);
			}
			foreach (HarvestDesignatable harvestDesignatable2 in this.layerTargets)
			{
				Vector2I vector2I3 = Grid.PosToXY(harvestDesignatable2.transform.GetPosition());
				if (vector2I <= vector2I3 && vector2I3 <= vector2I2)
				{
					this.AddCropUI(harvestDesignatable2);
				}
			}
			foreach (OverlayModes.Crop.UpdateCropInfo updateCropInfo in this.updateCropInfo)
			{
				updateCropInfo.harvestableUI.GetComponent<HarvestableOverlayWidget>().Refresh(updateCropInfo.harvestable);
			}
			for (int i = this.freeHarvestableNotificationIdx; i < this.harvestableNotificationList.Count; i++)
			{
				if (this.harvestableNotificationList[i].activeSelf)
				{
					this.harvestableNotificationList[i].SetActive(false);
				}
			}
			base.UpdateHighlightTypeOverlay<HarvestDesignatable>(vector2I, vector2I2, this.layerTargets, this.targetIDs, this.highlightConditions, OverlayModes.BringToFrontLayerSetting.Constant, this.targetLayer);
			base.Update();
		}

		// Token: 0x06008D20 RID: 36128 RVA: 0x0030550C File Offset: 0x0030370C
		public override void Disable()
		{
			this.DisableHarvestableUINotifications();
			base.Disable();
		}

		// Token: 0x06008D21 RID: 36129 RVA: 0x0030551C File Offset: 0x0030371C
		private void DisableHarvestableUINotifications()
		{
			this.freeHarvestableNotificationIdx = 0;
			foreach (GameObject gameObject in this.harvestableNotificationList)
			{
				gameObject.SetActive(false);
			}
			this.updateCropInfo.Clear();
		}

		// Token: 0x06008D22 RID: 36130 RVA: 0x00305580 File Offset: 0x00303780
		public GameObject GetFreeCropUI()
		{
			GameObject gameObject;
			if (this.freeHarvestableNotificationIdx < this.harvestableNotificationList.Count)
			{
				gameObject = this.harvestableNotificationList[this.freeHarvestableNotificationIdx];
				if (!gameObject.gameObject.activeSelf)
				{
					gameObject.gameObject.SetActive(true);
				}
				this.freeHarvestableNotificationIdx++;
			}
			else
			{
				gameObject = global::Util.KInstantiateUI(this.harvestableNotificationPrefab.gameObject, this.uiRoot.transform.gameObject, false);
				this.harvestableNotificationList.Add(gameObject);
				this.freeHarvestableNotificationIdx++;
			}
			return gameObject;
		}

		// Token: 0x06008D23 RID: 36131 RVA: 0x0030561C File Offset: 0x0030381C
		private void AddCropUI(HarvestDesignatable harvestable)
		{
			GameObject freeCropUI = this.GetFreeCropUI();
			OverlayModes.Crop.UpdateCropInfo updateCropInfo = new OverlayModes.Crop.UpdateCropInfo(harvestable, freeCropUI);
			Vector3 vector = Grid.CellToPos(Grid.PosToCell(harvestable), 0.5f, -1.25f, 0f);
			freeCropUI.GetComponent<RectTransform>().SetPosition(Vector3.up + vector);
			this.updateCropInfo.Add(updateCropInfo);
		}

		// Token: 0x04006F69 RID: 28521
		public static readonly HashedString ID = "Crop";

		// Token: 0x04006F6A RID: 28522
		private Canvas uiRoot;

		// Token: 0x04006F6B RID: 28523
		private List<OverlayModes.Crop.UpdateCropInfo> updateCropInfo = new List<OverlayModes.Crop.UpdateCropInfo>();

		// Token: 0x04006F6C RID: 28524
		private int freeHarvestableNotificationIdx;

		// Token: 0x04006F6D RID: 28525
		private List<GameObject> harvestableNotificationList = new List<GameObject>();

		// Token: 0x04006F6E RID: 28526
		private GameObject harvestableNotificationPrefab;

		// Token: 0x04006F6F RID: 28527
		private OverlayModes.ColorHighlightCondition[] highlightConditions;

		// Token: 0x020020E2 RID: 8418
		private struct UpdateCropInfo
		{
			// Token: 0x0600A568 RID: 42344 RVA: 0x00349E0F File Offset: 0x0034800F
			public UpdateCropInfo(HarvestDesignatable harvestable, GameObject harvestableUI)
			{
				this.harvestable = harvestable;
				this.harvestableUI = harvestableUI;
			}

			// Token: 0x0400927A RID: 37498
			public HarvestDesignatable harvestable;

			// Token: 0x0400927B RID: 37499
			public GameObject harvestableUI;
		}
	}

	// Token: 0x02001830 RID: 6192
	public class Harvest : OverlayModes.BasePlantMode
	{
		// Token: 0x06008D25 RID: 36133 RVA: 0x00305688 File Offset: 0x00303888
		public override HashedString ViewMode()
		{
			return OverlayModes.Harvest.ID;
		}

		// Token: 0x06008D26 RID: 36134 RVA: 0x0030568F File Offset: 0x0030388F
		public override string GetSoundName()
		{
			return "Harvest";
		}

		// Token: 0x06008D27 RID: 36135 RVA: 0x00305698 File Offset: 0x00303898
		public Harvest()
		{
			OverlayModes.ColorHighlightCondition[] array = new OverlayModes.ColorHighlightCondition[1];
			array[0] = new OverlayModes.ColorHighlightCondition((KMonoBehaviour harvestable) => new Color(0.65f, 0.65f, 0.65f, 0.65f), (KMonoBehaviour harvestable) => true);
			this.highlightConditions = array;
			base..ctor(OverlayScreen.HarvestableIDs);
		}

		// Token: 0x06008D28 RID: 36136 RVA: 0x00305704 File Offset: 0x00303904
		public override void Update()
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			OverlayModes.Mode.RemoveOffscreenTargets<HarvestDesignatable>(this.layerTargets, vector2I, vector2I2, null);
			foreach (object obj in this.partition.GetAllIntersecting(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y)))
			{
				HarvestDesignatable harvestDesignatable = (HarvestDesignatable)obj;
				base.AddTargetIfVisible<HarvestDesignatable>(harvestDesignatable, vector2I, vector2I2, this.layerTargets, this.targetLayer, null, null);
			}
			base.UpdateHighlightTypeOverlay<HarvestDesignatable>(vector2I, vector2I2, this.layerTargets, this.targetIDs, this.highlightConditions, OverlayModes.BringToFrontLayerSetting.Constant, this.targetLayer);
			base.Update();
		}

		// Token: 0x04006F70 RID: 28528
		public static readonly HashedString ID = "HarvestWhenReady";

		// Token: 0x04006F71 RID: 28529
		private OverlayModes.ColorHighlightCondition[] highlightConditions;
	}

	// Token: 0x02001831 RID: 6193
	public abstract class BasePlantMode : OverlayModes.Mode
	{
		// Token: 0x06008D2A RID: 36138 RVA: 0x003057F0 File Offset: 0x003039F0
		public BasePlantMode(ICollection<Tag> ids)
		{
			this.targetLayer = LayerMask.NameToLayer("MaskedOverlay");
			this.cameraLayerMask = LayerMask.GetMask(new string[] { "MaskedOverlay", "MaskedOverlayBG" });
			this.selectionMask = LayerMask.GetMask(new string[] { "MaskedOverlay" });
			this.targetIDs = ids;
		}

		// Token: 0x06008D2B RID: 36139 RVA: 0x0030585F File Offset: 0x00303A5F
		public override void Enable()
		{
			base.RegisterSaveLoadListeners();
			this.partition = OverlayModes.Mode.PopulatePartition<HarvestDesignatable>(this.targetIDs);
			Camera.main.cullingMask |= this.cameraLayerMask;
			SelectTool.Instance.SetLayerMask(this.selectionMask);
		}

		// Token: 0x06008D2C RID: 36140 RVA: 0x003058A0 File Offset: 0x00303AA0
		protected override void OnSaveLoadRootRegistered(SaveLoadRoot item)
		{
			Tag saveLoadTag = item.GetComponent<KPrefabID>().GetSaveLoadTag();
			if (!this.targetIDs.Contains(saveLoadTag))
			{
				return;
			}
			HarvestDesignatable component = item.GetComponent<HarvestDesignatable>();
			if (component == null)
			{
				return;
			}
			this.partition.Add(component);
		}

		// Token: 0x06008D2D RID: 36141 RVA: 0x003058E8 File Offset: 0x00303AE8
		protected override void OnSaveLoadRootUnregistered(SaveLoadRoot item)
		{
			if (item == null || item.gameObject == null)
			{
				return;
			}
			HarvestDesignatable component = item.GetComponent<HarvestDesignatable>();
			if (component == null)
			{
				return;
			}
			if (this.layerTargets.Contains(component))
			{
				this.layerTargets.Remove(component);
			}
			this.partition.Remove(component);
		}

		// Token: 0x06008D2E RID: 36142 RVA: 0x00305948 File Offset: 0x00303B48
		public override void Disable()
		{
			base.UnregisterSaveLoadListeners();
			base.DisableHighlightTypeOverlay<HarvestDesignatable>(this.layerTargets);
			Camera.main.cullingMask &= ~this.cameraLayerMask;
			this.partition.Clear();
			this.layerTargets.Clear();
			SelectTool.Instance.ClearLayerMask();
		}

		// Token: 0x04006F72 RID: 28530
		protected UniformGrid<HarvestDesignatable> partition;

		// Token: 0x04006F73 RID: 28531
		protected HashSet<HarvestDesignatable> layerTargets = new HashSet<HarvestDesignatable>();

		// Token: 0x04006F74 RID: 28532
		protected ICollection<Tag> targetIDs;

		// Token: 0x04006F75 RID: 28533
		protected int targetLayer;

		// Token: 0x04006F76 RID: 28534
		private int cameraLayerMask;

		// Token: 0x04006F77 RID: 28535
		private int selectionMask;
	}

	// Token: 0x02001832 RID: 6194
	public class Decor : OverlayModes.Mode
	{
		// Token: 0x06008D2F RID: 36143 RVA: 0x0030599F File Offset: 0x00303B9F
		public override HashedString ViewMode()
		{
			return OverlayModes.Decor.ID;
		}

		// Token: 0x06008D30 RID: 36144 RVA: 0x003059A6 File Offset: 0x00303BA6
		public override string GetSoundName()
		{
			return "Decor";
		}

		// Token: 0x06008D31 RID: 36145 RVA: 0x003059B0 File Offset: 0x00303BB0
		public override List<LegendEntry> GetCustomLegendData()
		{
			return new List<LegendEntry>
			{
				new LegendEntry(UI.OVERLAYS.DECOR.HIGHDECOR, UI.OVERLAYS.DECOR.TOOLTIPS.HIGHDECOR, GlobalAssets.Instance.colorSet.decorPositive, null, null, true),
				new LegendEntry(UI.OVERLAYS.DECOR.LOWDECOR, UI.OVERLAYS.DECOR.TOOLTIPS.LOWDECOR, GlobalAssets.Instance.colorSet.decorNegative, null, null, true)
			};
		}

		// Token: 0x06008D32 RID: 36146 RVA: 0x00305A30 File Offset: 0x00303C30
		public Decor()
		{
			OverlayModes.ColorHighlightCondition[] array = new OverlayModes.ColorHighlightCondition[1];
			array[0] = new OverlayModes.ColorHighlightCondition(delegate(KMonoBehaviour dp)
			{
				Color black = Color.black;
				Color color = Color.black;
				if (dp != null)
				{
					int num = Grid.PosToCell(CameraController.Instance.baseCamera.ScreenToWorldPoint(KInputManager.GetMousePos()));
					float num2 = (dp as DecorProvider).GetDecorForCell(num);
					if (num2 > 0f)
					{
						color = GlobalAssets.Instance.colorSet.decorHighlightPositive;
					}
					else if (num2 < 0f)
					{
						color = GlobalAssets.Instance.colorSet.decorHighlightNegative;
					}
					else if (dp.GetComponent<MonumentPart>() != null && dp.GetComponent<MonumentPart>().IsMonumentCompleted())
					{
						foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(dp.GetComponent<AttachableBuilding>()))
						{
							num2 = gameObject.GetComponent<DecorProvider>().GetDecorForCell(num);
							if (num2 > 0f)
							{
								color = GlobalAssets.Instance.colorSet.decorHighlightPositive;
								break;
							}
							if (num2 < 0f)
							{
								color = GlobalAssets.Instance.colorSet.decorHighlightNegative;
								break;
							}
						}
					}
				}
				return Color.Lerp(black, color, 0.85f);
			}, (KMonoBehaviour dp) => SelectToolHoverTextCard.highlightedObjects.Contains(dp.gameObject));
			this.highlightConditions = array;
			base..ctor();
			this.targetLayer = LayerMask.NameToLayer("MaskedOverlay");
			this.cameraLayerMask = LayerMask.GetMask(new string[] { "MaskedOverlay", "MaskedOverlayBG" });
		}

		// Token: 0x06008D33 RID: 36147 RVA: 0x00305AE8 File Offset: 0x00303CE8
		public override void Enable()
		{
			base.RegisterSaveLoadListeners();
			List<Tag> prefabTagsWithComponent = Assets.GetPrefabTagsWithComponent<DecorProvider>();
			this.targetIDs.UnionWith(prefabTagsWithComponent);
			foreach (Tag tag in new Tag[]
			{
				new Tag("Tile"),
				new Tag("MeshTile"),
				new Tag("InsulationTile"),
				new Tag("GasPermeableMembrane"),
				new Tag("CarpetTile")
			})
			{
				this.targetIDs.Remove(tag);
			}
			foreach (Tag tag2 in OverlayScreen.GasVentIDs)
			{
				this.targetIDs.Remove(tag2);
			}
			foreach (Tag tag3 in OverlayScreen.LiquidVentIDs)
			{
				this.targetIDs.Remove(tag3);
			}
			this.partition = OverlayModes.Mode.PopulatePartition<DecorProvider>(this.targetIDs);
			Camera.main.cullingMask |= this.cameraLayerMask;
		}

		// Token: 0x06008D34 RID: 36148 RVA: 0x00305C4C File Offset: 0x00303E4C
		public override void Update()
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			OverlayModes.Mode.RemoveOffscreenTargets<DecorProvider>(this.layerTargets, vector2I, vector2I2, null);
			this.partition.GetAllIntersecting(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y), this.workingTargets);
			for (int i = 0; i < this.workingTargets.Count; i++)
			{
				DecorProvider decorProvider = this.workingTargets[i];
				base.AddTargetIfVisible<DecorProvider>(decorProvider, vector2I, vector2I2, this.layerTargets, this.targetLayer, null, null);
			}
			base.UpdateHighlightTypeOverlay<DecorProvider>(vector2I, vector2I2, this.layerTargets, this.targetIDs, this.highlightConditions, OverlayModes.BringToFrontLayerSetting.Conditional, this.targetLayer);
			this.workingTargets.Clear();
		}

		// Token: 0x06008D35 RID: 36149 RVA: 0x00305D10 File Offset: 0x00303F10
		protected override void OnSaveLoadRootRegistered(SaveLoadRoot item)
		{
			Tag saveLoadTag = item.GetComponent<KPrefabID>().GetSaveLoadTag();
			if (this.targetIDs.Contains(saveLoadTag))
			{
				DecorProvider component = item.GetComponent<DecorProvider>();
				if (component != null)
				{
					this.partition.Add(component);
				}
			}
		}

		// Token: 0x06008D36 RID: 36150 RVA: 0x00305D54 File Offset: 0x00303F54
		protected override void OnSaveLoadRootUnregistered(SaveLoadRoot item)
		{
			if (item == null || item.gameObject == null)
			{
				return;
			}
			DecorProvider component = item.GetComponent<DecorProvider>();
			if (component != null)
			{
				if (this.layerTargets.Contains(component))
				{
					this.layerTargets.Remove(component);
				}
				this.partition.Remove(component);
			}
		}

		// Token: 0x06008D37 RID: 36151 RVA: 0x00305DB0 File Offset: 0x00303FB0
		public override void Disable()
		{
			base.DisableHighlightTypeOverlay<DecorProvider>(this.layerTargets);
			Camera.main.cullingMask &= ~this.cameraLayerMask;
			base.UnregisterSaveLoadListeners();
			this.partition.Clear();
			this.layerTargets.Clear();
		}

		// Token: 0x04006F78 RID: 28536
		public static readonly HashedString ID = "Decor";

		// Token: 0x04006F79 RID: 28537
		private UniformGrid<DecorProvider> partition;

		// Token: 0x04006F7A RID: 28538
		private HashSet<DecorProvider> layerTargets = new HashSet<DecorProvider>();

		// Token: 0x04006F7B RID: 28539
		private List<DecorProvider> workingTargets = new List<DecorProvider>();

		// Token: 0x04006F7C RID: 28540
		private HashSet<Tag> targetIDs = new HashSet<Tag>();

		// Token: 0x04006F7D RID: 28541
		private int targetLayer;

		// Token: 0x04006F7E RID: 28542
		private int cameraLayerMask;

		// Token: 0x04006F7F RID: 28543
		private OverlayModes.ColorHighlightCondition[] highlightConditions;
	}

	// Token: 0x02001833 RID: 6195
	public class Disease : OverlayModes.Mode
	{
		// Token: 0x06008D39 RID: 36153 RVA: 0x00305E10 File Offset: 0x00304010
		private static float CalculateHUE(Color32 colour)
		{
			byte b = Math.Max(colour.r, Math.Max(colour.g, colour.b));
			byte b2 = Math.Min(colour.r, Math.Min(colour.g, colour.b));
			float num = 0f;
			int num2 = (int)(b - b2);
			if (num2 == 0)
			{
				num = 0f;
			}
			else if (b == colour.r)
			{
				num = (float)(colour.g - colour.b) / (float)num2 % 6f;
			}
			else if (b == colour.g)
			{
				num = (float)(colour.b - colour.r) / (float)num2 + 2f;
			}
			else if (b == colour.b)
			{
				num = (float)(colour.r - colour.g) / (float)num2 + 4f;
			}
			return num;
		}

		// Token: 0x06008D3A RID: 36154 RVA: 0x00305ED4 File Offset: 0x003040D4
		public override HashedString ViewMode()
		{
			return OverlayModes.Disease.ID;
		}

		// Token: 0x06008D3B RID: 36155 RVA: 0x00305EDB File Offset: 0x003040DB
		public override string GetSoundName()
		{
			return "Disease";
		}

		// Token: 0x06008D3C RID: 36156 RVA: 0x00305EE4 File Offset: 0x003040E4
		public Disease(Canvas diseaseUIParent, GameObject diseaseOverlayPrefab)
		{
			this.diseaseUIParent = diseaseUIParent;
			this.diseaseOverlayPrefab = diseaseOverlayPrefab;
			this.legendFilters = this.CreateDefaultFilters();
			this.cameraLayerMask = LayerMask.GetMask(new string[] { "MaskedOverlay", "MaskedOverlayBG" });
		}

		// Token: 0x06008D3D RID: 36157 RVA: 0x00305F6C File Offset: 0x0030416C
		public override void Enable()
		{
			Infrared.Instance.SetMode(Infrared.Mode.Disease);
			CameraController.Instance.ToggleColouredOverlayView(true);
			Camera.main.cullingMask |= this.cameraLayerMask;
			base.RegisterSaveLoadListeners();
			foreach (DiseaseSourceVisualizer diseaseSourceVisualizer in Components.DiseaseSourceVisualizers.Items)
			{
				if (!(diseaseSourceVisualizer == null))
				{
					diseaseSourceVisualizer.Show(this.ViewMode());
				}
			}
		}

		// Token: 0x06008D3E RID: 36158 RVA: 0x00306004 File Offset: 0x00304204
		public override Dictionary<string, ToolParameterMenu.ToggleState> CreateDefaultFilters()
		{
			return new Dictionary<string, ToolParameterMenu.ToggleState>
			{
				{
					ToolParameterMenu.FILTERLAYERS.ALL,
					ToolParameterMenu.ToggleState.On
				},
				{
					ToolParameterMenu.FILTERLAYERS.LIQUIDCONDUIT,
					ToolParameterMenu.ToggleState.Off
				},
				{
					ToolParameterMenu.FILTERLAYERS.GASCONDUIT,
					ToolParameterMenu.ToggleState.Off
				}
			};
		}

		// Token: 0x06008D3F RID: 36159 RVA: 0x0030602F File Offset: 0x0030422F
		public override void OnFiltersChanged()
		{
			Game.Instance.showGasConduitDisease = base.InFilter(ToolParameterMenu.FILTERLAYERS.GASCONDUIT, this.legendFilters);
			Game.Instance.showLiquidConduitDisease = base.InFilter(ToolParameterMenu.FILTERLAYERS.LIQUIDCONDUIT, this.legendFilters);
		}

		// Token: 0x06008D40 RID: 36160 RVA: 0x00306068 File Offset: 0x00304268
		protected override void OnSaveLoadRootRegistered(SaveLoadRoot item)
		{
			if (item == null)
			{
				return;
			}
			KBatchedAnimController component = item.GetComponent<KBatchedAnimController>();
			if (component == null)
			{
				return;
			}
			InfraredVisualizerComponents.ClearOverlayColour(component);
		}

		// Token: 0x06008D41 RID: 36161 RVA: 0x00306096 File Offset: 0x00304296
		protected override void OnSaveLoadRootUnregistered(SaveLoadRoot item)
		{
		}

		// Token: 0x06008D42 RID: 36162 RVA: 0x00306098 File Offset: 0x00304298
		public override void Disable()
		{
			foreach (DiseaseSourceVisualizer diseaseSourceVisualizer in Components.DiseaseSourceVisualizers.Items)
			{
				if (!(diseaseSourceVisualizer == null))
				{
					diseaseSourceVisualizer.Show(OverlayModes.None.ID);
				}
			}
			base.UnregisterSaveLoadListeners();
			Camera.main.cullingMask &= ~this.cameraLayerMask;
			foreach (KMonoBehaviour kmonoBehaviour in this.layerTargets)
			{
				if (!(kmonoBehaviour == null))
				{
					float defaultDepth = OverlayModes.Mode.GetDefaultDepth(kmonoBehaviour);
					Vector3 position = kmonoBehaviour.transform.GetPosition();
					position.z = defaultDepth;
					kmonoBehaviour.transform.SetPosition(position);
					KBatchedAnimController component = kmonoBehaviour.GetComponent<KBatchedAnimController>();
					component.enabled = false;
					component.enabled = true;
				}
			}
			CameraController.Instance.ToggleColouredOverlayView(false);
			Infrared.Instance.SetMode(Infrared.Mode.Disabled);
			Game.Instance.showGasConduitDisease = false;
			Game.Instance.showLiquidConduitDisease = false;
			this.freeDiseaseUI = 0;
			foreach (OverlayModes.Disease.UpdateDiseaseInfo updateDiseaseInfo in this.updateDiseaseInfo)
			{
				updateDiseaseInfo.ui.gameObject.SetActive(false);
			}
			this.updateDiseaseInfo.Clear();
			this.privateTargets.Clear();
			this.layerTargets.Clear();
		}

		// Token: 0x06008D43 RID: 36163 RVA: 0x0030623C File Offset: 0x0030443C
		public override List<LegendEntry> GetCustomLegendData()
		{
			List<LegendEntry> list = new List<LegendEntry>();
			List<OverlayModes.Disease.DiseaseSortInfo> list2 = new List<OverlayModes.Disease.DiseaseSortInfo>();
			foreach (Klei.AI.Disease disease in Db.Get().Diseases.resources)
			{
				list2.Add(new OverlayModes.Disease.DiseaseSortInfo(disease));
			}
			list2.Sort((OverlayModes.Disease.DiseaseSortInfo a, OverlayModes.Disease.DiseaseSortInfo b) => a.sortkey.CompareTo(b.sortkey));
			foreach (OverlayModes.Disease.DiseaseSortInfo diseaseSortInfo in list2)
			{
				list.Add(new LegendEntry(diseaseSortInfo.disease.Name, diseaseSortInfo.disease.overlayLegendHovertext.ToString(), GlobalAssets.Instance.colorSet.GetColorByName(diseaseSortInfo.disease.overlayColourName), null, null, true));
			}
			return list;
		}

		// Token: 0x06008D44 RID: 36164 RVA: 0x00306354 File Offset: 0x00304554
		public GameObject GetFreeDiseaseUI()
		{
			GameObject gameObject;
			if (this.freeDiseaseUI < this.diseaseUIList.Count)
			{
				gameObject = this.diseaseUIList[this.freeDiseaseUI];
				gameObject.gameObject.SetActive(true);
				this.freeDiseaseUI++;
			}
			else
			{
				gameObject = global::Util.KInstantiateUI(this.diseaseOverlayPrefab, this.diseaseUIParent.transform.gameObject, false);
				this.diseaseUIList.Add(gameObject);
				this.freeDiseaseUI++;
			}
			return gameObject;
		}

		// Token: 0x06008D45 RID: 36165 RVA: 0x003063DC File Offset: 0x003045DC
		private void AddDiseaseUI(MinionIdentity target)
		{
			GameObject gameObject = this.GetFreeDiseaseUI();
			DiseaseOverlayWidget component = gameObject.GetComponent<DiseaseOverlayWidget>();
			AmountInstance amountInstance = target.GetComponent<Modifiers>().amounts.Get(Db.Get().Amounts.ImmuneLevel);
			OverlayModes.Disease.UpdateDiseaseInfo updateDiseaseInfo = new OverlayModes.Disease.UpdateDiseaseInfo(amountInstance, component);
			KAnimControllerBase component2 = target.GetComponent<KAnimControllerBase>();
			Vector3 vector = ((component2 != null) ? component2.GetWorldPivot() : (target.transform.GetPosition() + Vector3.down));
			gameObject.GetComponent<RectTransform>().SetPosition(vector);
			this.updateDiseaseInfo.Add(updateDiseaseInfo);
		}

		// Token: 0x06008D46 RID: 36166 RVA: 0x00306468 File Offset: 0x00304668
		public override void Update()
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			using (new KProfiler.Region("UpdateDiseaseCarriers", null))
			{
				this.queuedAdds.Clear();
				foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
				{
					if (!(minionIdentity == null))
					{
						Vector2I vector2I3 = Grid.PosToXY(minionIdentity.transform.GetPosition());
						if (vector2I <= vector2I3 && vector2I3 <= vector2I2 && !this.privateTargets.Contains(minionIdentity))
						{
							this.AddDiseaseUI(minionIdentity);
							this.queuedAdds.Add(minionIdentity);
						}
					}
				}
				foreach (KMonoBehaviour kmonoBehaviour in this.queuedAdds)
				{
					this.privateTargets.Add(kmonoBehaviour);
				}
				this.queuedAdds.Clear();
			}
			foreach (OverlayModes.Disease.UpdateDiseaseInfo updateDiseaseInfo in this.updateDiseaseInfo)
			{
				updateDiseaseInfo.ui.Refresh(updateDiseaseInfo.valueSrc);
			}
			bool flag = false;
			if (Game.Instance.showLiquidConduitDisease)
			{
				using (HashSet<Tag>.Enumerator enumerator4 = OverlayScreen.LiquidVentIDs.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						Tag tag = enumerator4.Current;
						if (!OverlayScreen.DiseaseIDs.Contains(tag))
						{
							OverlayScreen.DiseaseIDs.Add(tag);
							flag = true;
						}
					}
					goto IL_1F1;
				}
			}
			foreach (Tag tag2 in OverlayScreen.LiquidVentIDs)
			{
				if (OverlayScreen.DiseaseIDs.Contains(tag2))
				{
					OverlayScreen.DiseaseIDs.Remove(tag2);
					flag = true;
				}
			}
			IL_1F1:
			if (Game.Instance.showGasConduitDisease)
			{
				using (HashSet<Tag>.Enumerator enumerator4 = OverlayScreen.GasVentIDs.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						Tag tag3 = enumerator4.Current;
						if (!OverlayScreen.DiseaseIDs.Contains(tag3))
						{
							OverlayScreen.DiseaseIDs.Add(tag3);
							flag = true;
						}
					}
					goto IL_297;
				}
			}
			foreach (Tag tag4 in OverlayScreen.GasVentIDs)
			{
				if (OverlayScreen.DiseaseIDs.Contains(tag4))
				{
					OverlayScreen.DiseaseIDs.Remove(tag4);
					flag = true;
				}
			}
			IL_297:
			if (flag)
			{
				this.SetLayerZ(-50f);
			}
		}

		// Token: 0x06008D47 RID: 36167 RVA: 0x00306780 File Offset: 0x00304980
		private void SetLayerZ(float offset_z)
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			OverlayModes.Mode.ClearOutsideViewObjects<KMonoBehaviour>(this.layerTargets, vector2I, vector2I2, OverlayScreen.DiseaseIDs, delegate(KMonoBehaviour go)
			{
				if (go != null)
				{
					float defaultDepth2 = OverlayModes.Mode.GetDefaultDepth(go);
					Vector3 position2 = go.transform.GetPosition();
					position2.z = defaultDepth2;
					go.transform.SetPosition(position2);
					KBatchedAnimController component2 = go.GetComponent<KBatchedAnimController>();
					component2.enabled = false;
					component2.enabled = true;
				}
			});
			Dictionary<Tag, List<SaveLoadRoot>> lists = SaveLoader.Instance.saveManager.GetLists();
			foreach (Tag tag in OverlayScreen.DiseaseIDs)
			{
				List<SaveLoadRoot> list;
				if (lists.TryGetValue(tag, out list))
				{
					foreach (KMonoBehaviour kmonoBehaviour in list)
					{
						if (!(kmonoBehaviour == null) && !this.layerTargets.Contains(kmonoBehaviour))
						{
							Vector3 position = kmonoBehaviour.transform.GetPosition();
							if (Grid.IsVisible(Grid.PosToCell(position)) && vector2I <= position && position <= vector2I2)
							{
								float defaultDepth = OverlayModes.Mode.GetDefaultDepth(kmonoBehaviour);
								position.z = defaultDepth + offset_z;
								kmonoBehaviour.transform.SetPosition(position);
								KBatchedAnimController component = kmonoBehaviour.GetComponent<KBatchedAnimController>();
								component.enabled = false;
								component.enabled = true;
								this.layerTargets.Add(kmonoBehaviour);
							}
						}
					}
				}
			}
		}

		// Token: 0x04006F80 RID: 28544
		public static readonly HashedString ID = "Disease";

		// Token: 0x04006F81 RID: 28545
		private int cameraLayerMask;

		// Token: 0x04006F82 RID: 28546
		private int freeDiseaseUI;

		// Token: 0x04006F83 RID: 28547
		private List<GameObject> diseaseUIList = new List<GameObject>();

		// Token: 0x04006F84 RID: 28548
		private List<OverlayModes.Disease.UpdateDiseaseInfo> updateDiseaseInfo = new List<OverlayModes.Disease.UpdateDiseaseInfo>();

		// Token: 0x04006F85 RID: 28549
		private HashSet<KMonoBehaviour> layerTargets = new HashSet<KMonoBehaviour>();

		// Token: 0x04006F86 RID: 28550
		private HashSet<KMonoBehaviour> privateTargets = new HashSet<KMonoBehaviour>();

		// Token: 0x04006F87 RID: 28551
		private List<KMonoBehaviour> queuedAdds = new List<KMonoBehaviour>();

		// Token: 0x04006F88 RID: 28552
		private Canvas diseaseUIParent;

		// Token: 0x04006F89 RID: 28553
		private GameObject diseaseOverlayPrefab;

		// Token: 0x020020E6 RID: 8422
		private struct DiseaseSortInfo
		{
			// Token: 0x0600A579 RID: 42361 RVA: 0x0034A05A File Offset: 0x0034825A
			public DiseaseSortInfo(Klei.AI.Disease d)
			{
				this.disease = d;
				this.sortkey = OverlayModes.Disease.CalculateHUE(GlobalAssets.Instance.colorSet.GetColorByName(d.overlayColourName));
			}

			// Token: 0x04009289 RID: 37513
			public float sortkey;

			// Token: 0x0400928A RID: 37514
			public Klei.AI.Disease disease;
		}

		// Token: 0x020020E7 RID: 8423
		private struct UpdateDiseaseInfo
		{
			// Token: 0x0600A57A RID: 42362 RVA: 0x0034A083 File Offset: 0x00348283
			public UpdateDiseaseInfo(AmountInstance amount_inst, DiseaseOverlayWidget ui)
			{
				this.ui = ui;
				this.valueSrc = amount_inst;
			}

			// Token: 0x0400928B RID: 37515
			public DiseaseOverlayWidget ui;

			// Token: 0x0400928C RID: 37516
			public AmountInstance valueSrc;
		}
	}

	// Token: 0x02001834 RID: 6196
	public class Logic : OverlayModes.Mode
	{
		// Token: 0x06008D49 RID: 36169 RVA: 0x00306919 File Offset: 0x00304B19
		public override HashedString ViewMode()
		{
			return OverlayModes.Logic.ID;
		}

		// Token: 0x06008D4A RID: 36170 RVA: 0x00306920 File Offset: 0x00304B20
		public override string GetSoundName()
		{
			return "Logic";
		}

		// Token: 0x06008D4B RID: 36171 RVA: 0x00306928 File Offset: 0x00304B28
		public override List<LegendEntry> GetCustomLegendData()
		{
			return new List<LegendEntry>
			{
				new LegendEntry(UI.OVERLAYS.LOGIC.INPUT, UI.OVERLAYS.LOGIC.TOOLTIPS.INPUT, Color.white, null, Assets.GetSprite("logicInput"), true),
				new LegendEntry(UI.OVERLAYS.LOGIC.OUTPUT, UI.OVERLAYS.LOGIC.TOOLTIPS.OUTPUT, Color.white, null, Assets.GetSprite("logicOutput"), true),
				new LegendEntry(UI.OVERLAYS.LOGIC.RIBBON_INPUT, UI.OVERLAYS.LOGIC.TOOLTIPS.RIBBON_INPUT, Color.white, null, Assets.GetSprite("logic_ribbon_all_in"), true),
				new LegendEntry(UI.OVERLAYS.LOGIC.RIBBON_OUTPUT, UI.OVERLAYS.LOGIC.TOOLTIPS.RIBBON_OUTPUT, Color.white, null, Assets.GetSprite("logic_ribbon_all_out"), true),
				new LegendEntry(UI.OVERLAYS.LOGIC.RESET_UPDATE, UI.OVERLAYS.LOGIC.TOOLTIPS.RESET_UPDATE, Color.white, null, Assets.GetSprite("logicResetUpdate"), true),
				new LegendEntry(UI.OVERLAYS.LOGIC.CONTROL_INPUT, UI.OVERLAYS.LOGIC.TOOLTIPS.CONTROL_INPUT, Color.white, null, Assets.GetSprite("control_input_frame_legend"), true),
				new LegendEntry(UI.OVERLAYS.LOGIC.CIRCUIT_STATUS_HEADER, null, Color.white, null, null, false),
				new LegendEntry(UI.OVERLAYS.LOGIC.ONE, null, GlobalAssets.Instance.colorSet.logicOnText, null, null, true),
				new LegendEntry(UI.OVERLAYS.LOGIC.ZERO, null, GlobalAssets.Instance.colorSet.logicOffText, null, null, true),
				new LegendEntry(UI.OVERLAYS.LOGIC.DISCONNECTED, UI.OVERLAYS.LOGIC.TOOLTIPS.DISCONNECTED, GlobalAssets.Instance.colorSet.logicDisconnected, null, null, true)
			};
		}

		// Token: 0x06008D4C RID: 36172 RVA: 0x00306B28 File Offset: 0x00304D28
		public Logic(LogicModeUI ui_asset)
		{
			this.conduitTargetLayer = LayerMask.NameToLayer("MaskedOverlay");
			this.objectTargetLayer = LayerMask.NameToLayer("MaskedOverlayBG");
			this.cameraLayerMask = LayerMask.GetMask(new string[] { "MaskedOverlay", "MaskedOverlayBG" });
			this.selectionMask = this.cameraLayerMask;
			this.uiAsset = ui_asset;
		}

		// Token: 0x06008D4D RID: 36173 RVA: 0x00306C0C File Offset: 0x00304E0C
		public override void Enable()
		{
			Camera.main.cullingMask |= this.cameraLayerMask;
			SelectTool.Instance.SetLayerMask(this.selectionMask);
			base.RegisterSaveLoadListeners();
			this.gameObjPartition = OverlayModes.Mode.PopulatePartition<SaveLoadRoot>(OverlayModes.Logic.HighlightItemIDs);
			this.ioPartition = this.CreateLogicUIPartition();
			GridCompositor.Instance.ToggleMinor(true);
			LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
			logicCircuitManager.onElemAdded = (Action<ILogicUIElement>)Delegate.Combine(logicCircuitManager.onElemAdded, new Action<ILogicUIElement>(this.OnUIElemAdded));
			LogicCircuitManager logicCircuitManager2 = Game.Instance.logicCircuitManager;
			logicCircuitManager2.onElemRemoved = (Action<ILogicUIElement>)Delegate.Combine(logicCircuitManager2.onElemRemoved, new Action<ILogicUIElement>(this.OnUIElemRemoved));
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().TechFilterLogicOn);
		}

		// Token: 0x06008D4E RID: 36174 RVA: 0x00306CD8 File Offset: 0x00304ED8
		public override void Disable()
		{
			LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
			logicCircuitManager.onElemAdded = (Action<ILogicUIElement>)Delegate.Remove(logicCircuitManager.onElemAdded, new Action<ILogicUIElement>(this.OnUIElemAdded));
			LogicCircuitManager logicCircuitManager2 = Game.Instance.logicCircuitManager;
			logicCircuitManager2.onElemRemoved = (Action<ILogicUIElement>)Delegate.Remove(logicCircuitManager2.onElemRemoved, new Action<ILogicUIElement>(this.OnUIElemRemoved));
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().TechFilterLogicOn, STOP_MODE.ALLOWFADEOUT);
			foreach (SaveLoadRoot saveLoadRoot in this.gameObjTargets)
			{
				float defaultDepth = OverlayModes.Mode.GetDefaultDepth(saveLoadRoot);
				Vector3 position = saveLoadRoot.transform.GetPosition();
				position.z = defaultDepth;
				saveLoadRoot.transform.SetPosition(position);
				saveLoadRoot.GetComponent<KBatchedAnimController>().enabled = false;
				saveLoadRoot.GetComponent<KBatchedAnimController>().enabled = true;
			}
			OverlayModes.Mode.ResetDisplayValues<SaveLoadRoot>(this.gameObjTargets);
			OverlayModes.Mode.ResetDisplayValues<KBatchedAnimController>(this.wireControllers);
			OverlayModes.Mode.ResetDisplayValues<KBatchedAnimController>(this.ribbonControllers);
			this.ResetRibbonSymbolTints<KBatchedAnimController>(this.ribbonControllers);
			foreach (OverlayModes.Logic.BridgeInfo bridgeInfo in this.bridgeControllers)
			{
				if (bridgeInfo.controller != null)
				{
					OverlayModes.Mode.ResetDisplayValues(bridgeInfo.controller);
				}
			}
			foreach (OverlayModes.Logic.BridgeInfo bridgeInfo2 in this.ribbonBridgeControllers)
			{
				if (bridgeInfo2.controller != null)
				{
					this.ResetRibbonTint(bridgeInfo2.controller);
				}
			}
			Camera.main.cullingMask &= ~this.cameraLayerMask;
			SelectTool.Instance.ClearLayerMask();
			base.UnregisterSaveLoadListeners();
			foreach (OverlayModes.Logic.UIInfo uiinfo in this.uiInfo.GetDataList())
			{
				uiinfo.Release();
			}
			this.uiInfo.Clear();
			this.uiNodes.Clear();
			this.ioPartition.Clear();
			this.ioTargets.Clear();
			this.gameObjPartition.Clear();
			this.gameObjTargets.Clear();
			this.wireControllers.Clear();
			this.ribbonControllers.Clear();
			this.bridgeControllers.Clear();
			this.ribbonBridgeControllers.Clear();
			GridCompositor.Instance.ToggleMinor(false);
		}

		// Token: 0x06008D4F RID: 36175 RVA: 0x00306F94 File Offset: 0x00305194
		protected override void OnSaveLoadRootRegistered(SaveLoadRoot item)
		{
			Tag saveLoadTag = item.GetComponent<KPrefabID>().GetSaveLoadTag();
			if (OverlayModes.Logic.HighlightItemIDs.Contains(saveLoadTag))
			{
				this.gameObjPartition.Add(item);
			}
		}

		// Token: 0x06008D50 RID: 36176 RVA: 0x00306FC8 File Offset: 0x003051C8
		protected override void OnSaveLoadRootUnregistered(SaveLoadRoot item)
		{
			if (item == null || item.gameObject == null)
			{
				return;
			}
			if (this.gameObjTargets.Contains(item))
			{
				this.gameObjTargets.Remove(item);
			}
			this.gameObjPartition.Remove(item);
		}

		// Token: 0x06008D51 RID: 36177 RVA: 0x00307014 File Offset: 0x00305214
		private void OnUIElemAdded(ILogicUIElement elem)
		{
			this.ioPartition.Add(elem);
		}

		// Token: 0x06008D52 RID: 36178 RVA: 0x00307022 File Offset: 0x00305222
		private void OnUIElemRemoved(ILogicUIElement elem)
		{
			this.ioPartition.Remove(elem);
			if (this.ioTargets.Contains(elem))
			{
				this.ioTargets.Remove(elem);
				this.FreeUI(elem);
			}
		}

		// Token: 0x06008D53 RID: 36179 RVA: 0x00307054 File Offset: 0x00305254
		public override void Update()
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			Tag wire_id = TagManager.Create("LogicWire");
			Tag ribbon_id = TagManager.Create("LogicRibbon");
			Tag bridge_id = TagManager.Create("LogicWireBridge");
			Tag ribbon_bridge_id = TagManager.Create("LogicRibbonBridge");
			OverlayModes.Mode.RemoveOffscreenTargets<SaveLoadRoot>(this.gameObjTargets, vector2I, vector2I2, delegate(SaveLoadRoot root)
			{
				if (root == null)
				{
					return;
				}
				KPrefabID component7 = root.GetComponent<KPrefabID>();
				if (component7 != null)
				{
					Tag prefabTag = component7.PrefabTag;
					if (prefabTag == wire_id)
					{
						this.wireControllers.Remove(root.GetComponent<KBatchedAnimController>());
						return;
					}
					if (prefabTag == ribbon_id)
					{
						this.ResetRibbonTint(root.GetComponent<KBatchedAnimController>());
						this.ribbonControllers.Remove(root.GetComponent<KBatchedAnimController>());
						return;
					}
					if (prefabTag == bridge_id)
					{
						KBatchedAnimController controller2 = root.GetComponent<KBatchedAnimController>();
						this.bridgeControllers.RemoveWhere((OverlayModes.Logic.BridgeInfo x) => x.controller == controller2);
						return;
					}
					if (prefabTag == ribbon_bridge_id)
					{
						KBatchedAnimController controller = root.GetComponent<KBatchedAnimController>();
						this.ResetRibbonTint(controller);
						this.ribbonBridgeControllers.RemoveWhere((OverlayModes.Logic.BridgeInfo x) => x.controller == controller);
						return;
					}
					float defaultDepth = OverlayModes.Mode.GetDefaultDepth(root);
					Vector3 position = root.transform.GetPosition();
					position.z = defaultDepth;
					root.transform.SetPosition(position);
					root.GetComponent<KBatchedAnimController>().enabled = false;
					root.GetComponent<KBatchedAnimController>().enabled = true;
				}
			});
			OverlayModes.Mode.RemoveOffscreenTargets<ILogicUIElement>(this.ioTargets, this.workingIOTargets, vector2I, vector2I2, new Action<ILogicUIElement>(this.FreeUI), null);
			using (new KProfiler.Region("UpdateLogicOverlay", null))
			{
				Action<SaveLoadRoot> <>9__3;
				foreach (object obj in this.gameObjPartition.GetAllIntersecting(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y)))
				{
					SaveLoadRoot saveLoadRoot = (SaveLoadRoot)obj;
					if (saveLoadRoot != null)
					{
						KPrefabID component = saveLoadRoot.GetComponent<KPrefabID>();
						if (component.PrefabTag == wire_id || component.PrefabTag == bridge_id || component.PrefabTag == ribbon_id || component.PrefabTag == ribbon_bridge_id)
						{
							SaveLoadRoot saveLoadRoot2 = saveLoadRoot;
							Vector2I vector2I3 = vector2I;
							Vector2I vector2I4 = vector2I2;
							ICollection<SaveLoadRoot> collection = this.gameObjTargets;
							int num = this.conduitTargetLayer;
							Action<SaveLoadRoot> action;
							if ((action = <>9__3) == null)
							{
								action = (<>9__3 = delegate(SaveLoadRoot root)
								{
									if (root == null)
									{
										return;
									}
									KPrefabID component8 = root.GetComponent<KPrefabID>();
									if (OverlayModes.Logic.HighlightItemIDs.Contains(component8.PrefabTag))
									{
										if (component8.PrefabTag == wire_id)
										{
											this.wireControllers.Add(root.GetComponent<KBatchedAnimController>());
											return;
										}
										if (component8.PrefabTag == ribbon_id)
										{
											this.ribbonControllers.Add(root.GetComponent<KBatchedAnimController>());
											return;
										}
										if (component8.PrefabTag == bridge_id)
										{
											KBatchedAnimController component9 = root.GetComponent<KBatchedAnimController>();
											int networkCell2 = root.GetComponent<LogicUtilityNetworkLink>().GetNetworkCell();
											this.bridgeControllers.Add(new OverlayModes.Logic.BridgeInfo
											{
												cell = networkCell2,
												controller = component9
											});
											return;
										}
										if (component8.PrefabTag == ribbon_bridge_id)
										{
											KBatchedAnimController component10 = root.GetComponent<KBatchedAnimController>();
											int networkCell3 = root.GetComponent<LogicUtilityNetworkLink>().GetNetworkCell();
											this.ribbonBridgeControllers.Add(new OverlayModes.Logic.BridgeInfo
											{
												cell = networkCell3,
												controller = component10
											});
										}
									}
								});
							}
							base.AddTargetIfVisible<SaveLoadRoot>(saveLoadRoot2, vector2I3, vector2I4, collection, num, action, null);
						}
						else
						{
							base.AddTargetIfVisible<SaveLoadRoot>(saveLoadRoot, vector2I, vector2I2, this.gameObjTargets, this.objectTargetLayer, delegate(SaveLoadRoot root)
							{
								Vector3 position2 = root.transform.GetPosition();
								float num3 = position2.z;
								KPrefabID component11 = root.GetComponent<KPrefabID>();
								if (component11 != null)
								{
									if (component11.HasTag(GameTags.OverlayInFrontOfConduits))
									{
										num3 = Grid.GetLayerZ(Grid.SceneLayer.LogicWires) - 0.2f;
									}
									else if (component11.HasTag(GameTags.OverlayBehindConduits))
									{
										num3 = Grid.GetLayerZ(Grid.SceneLayer.LogicWires) + 0.2f;
									}
								}
								position2.z = num3;
								root.transform.SetPosition(position2);
								KBatchedAnimController component12 = root.GetComponent<KBatchedAnimController>();
								component12.enabled = false;
								component12.enabled = true;
							}, null);
						}
					}
				}
				foreach (object obj2 in this.ioPartition.GetAllIntersecting(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y)))
				{
					ILogicUIElement logicUIElement = (ILogicUIElement)obj2;
					if (logicUIElement != null)
					{
						base.AddTargetIfVisible<ILogicUIElement>(logicUIElement, vector2I, vector2I2, this.ioTargets, this.objectTargetLayer, new Action<ILogicUIElement>(this.AddUI), (KMonoBehaviour kcmp) => kcmp != null && OverlayModes.Logic.HighlightItemIDs.Contains(kcmp.GetComponent<KPrefabID>().PrefabTag));
					}
				}
				this.connectedNetworks.Clear();
				float num2 = 1f;
				GameObject gameObject = null;
				if (SelectTool.Instance != null && SelectTool.Instance.hover != null)
				{
					gameObject = SelectTool.Instance.hover.gameObject;
				}
				if (gameObject != null)
				{
					IBridgedNetworkItem component2 = gameObject.GetComponent<IBridgedNetworkItem>();
					if (component2 != null)
					{
						int networkCell = component2.GetNetworkCell();
						this.visited.Clear();
						this.FindConnectedNetworks(networkCell, Game.Instance.logicCircuitSystem, this.connectedNetworks, this.visited);
						this.visited.Clear();
						num2 = OverlayModes.ModeUtil.GetHighlightScale();
					}
				}
				LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
				Color32 logicOn = GlobalAssets.Instance.colorSet.logicOn;
				Color32 logicOff = GlobalAssets.Instance.colorSet.logicOff;
				logicOff.a = (logicOn.a = 0);
				foreach (KBatchedAnimController kbatchedAnimController in this.wireControllers)
				{
					if (!(kbatchedAnimController == null))
					{
						Color32 color = logicOff;
						LogicCircuitNetwork networkForCell = logicCircuitManager.GetNetworkForCell(Grid.PosToCell(kbatchedAnimController.transform.GetPosition()));
						if (networkForCell != null)
						{
							color = (networkForCell.IsBitActive(0) ? logicOn : logicOff);
						}
						if (this.connectedNetworks.Count > 0)
						{
							IBridgedNetworkItem component3 = kbatchedAnimController.GetComponent<IBridgedNetworkItem>();
							if (component3 != null && component3.IsConnectedToNetworks(this.connectedNetworks))
							{
								color.r = (byte)((float)color.r * num2);
								color.g = (byte)((float)color.g * num2);
								color.b = (byte)((float)color.b * num2);
							}
						}
						kbatchedAnimController.TintColour = color;
					}
				}
				foreach (KBatchedAnimController kbatchedAnimController2 in this.ribbonControllers)
				{
					if (!(kbatchedAnimController2 == null))
					{
						Color32 color2 = logicOff;
						Color32 color3 = logicOff;
						Color32 color4 = logicOff;
						Color32 color5 = logicOff;
						LogicCircuitNetwork networkForCell2 = logicCircuitManager.GetNetworkForCell(Grid.PosToCell(kbatchedAnimController2.transform.GetPosition()));
						if (networkForCell2 != null)
						{
							color2 = (networkForCell2.IsBitActive(0) ? logicOn : logicOff);
							color3 = (networkForCell2.IsBitActive(1) ? logicOn : logicOff);
							color4 = (networkForCell2.IsBitActive(2) ? logicOn : logicOff);
							color5 = (networkForCell2.IsBitActive(3) ? logicOn : logicOff);
						}
						if (this.connectedNetworks.Count > 0)
						{
							IBridgedNetworkItem component4 = kbatchedAnimController2.GetComponent<IBridgedNetworkItem>();
							if (component4 != null && component4.IsConnectedToNetworks(this.connectedNetworks))
							{
								color2.r = (byte)((float)color2.r * num2);
								color2.g = (byte)((float)color2.g * num2);
								color2.b = (byte)((float)color2.b * num2);
								color3.r = (byte)((float)color3.r * num2);
								color3.g = (byte)((float)color3.g * num2);
								color3.b = (byte)((float)color3.b * num2);
								color4.r = (byte)((float)color4.r * num2);
								color4.g = (byte)((float)color4.g * num2);
								color4.b = (byte)((float)color4.b * num2);
								color5.r = (byte)((float)color5.r * num2);
								color5.g = (byte)((float)color5.g * num2);
								color5.b = (byte)((float)color5.b * num2);
							}
						}
						kbatchedAnimController2.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_1_SYMBOL_NAME, color2);
						kbatchedAnimController2.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_2_SYMBOL_NAME, color3);
						kbatchedAnimController2.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_3_SYMBOL_NAME, color4);
						kbatchedAnimController2.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_4_SYMBOL_NAME, color5);
					}
				}
				foreach (OverlayModes.Logic.BridgeInfo bridgeInfo in this.bridgeControllers)
				{
					if (!(bridgeInfo.controller == null))
					{
						Color32 color6 = logicOff;
						LogicCircuitNetwork networkForCell3 = logicCircuitManager.GetNetworkForCell(bridgeInfo.cell);
						if (networkForCell3 != null)
						{
							color6 = (networkForCell3.IsBitActive(0) ? logicOn : logicOff);
						}
						if (this.connectedNetworks.Count > 0)
						{
							IBridgedNetworkItem component5 = bridgeInfo.controller.GetComponent<IBridgedNetworkItem>();
							if (component5 != null && component5.IsConnectedToNetworks(this.connectedNetworks))
							{
								color6.r = (byte)((float)color6.r * num2);
								color6.g = (byte)((float)color6.g * num2);
								color6.b = (byte)((float)color6.b * num2);
							}
						}
						bridgeInfo.controller.TintColour = color6;
					}
				}
				foreach (OverlayModes.Logic.BridgeInfo bridgeInfo2 in this.ribbonBridgeControllers)
				{
					if (!(bridgeInfo2.controller == null))
					{
						Color32 color7 = logicOff;
						Color32 color8 = logicOff;
						Color32 color9 = logicOff;
						Color32 color10 = logicOff;
						LogicCircuitNetwork networkForCell4 = logicCircuitManager.GetNetworkForCell(bridgeInfo2.cell);
						if (networkForCell4 != null)
						{
							color7 = (networkForCell4.IsBitActive(0) ? logicOn : logicOff);
							color8 = (networkForCell4.IsBitActive(1) ? logicOn : logicOff);
							color9 = (networkForCell4.IsBitActive(2) ? logicOn : logicOff);
							color10 = (networkForCell4.IsBitActive(3) ? logicOn : logicOff);
						}
						if (this.connectedNetworks.Count > 0)
						{
							IBridgedNetworkItem component6 = bridgeInfo2.controller.GetComponent<IBridgedNetworkItem>();
							if (component6 != null && component6.IsConnectedToNetworks(this.connectedNetworks))
							{
								color7.r = (byte)((float)color7.r * num2);
								color7.g = (byte)((float)color7.g * num2);
								color7.b = (byte)((float)color7.b * num2);
								color8.r = (byte)((float)color8.r * num2);
								color8.g = (byte)((float)color8.g * num2);
								color8.b = (byte)((float)color8.b * num2);
								color9.r = (byte)((float)color9.r * num2);
								color9.g = (byte)((float)color9.g * num2);
								color9.b = (byte)((float)color9.b * num2);
								color10.r = (byte)((float)color10.r * num2);
								color10.g = (byte)((float)color10.g * num2);
								color10.b = (byte)((float)color10.b * num2);
							}
						}
						bridgeInfo2.controller.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_1_SYMBOL_NAME, color7);
						bridgeInfo2.controller.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_2_SYMBOL_NAME, color8);
						bridgeInfo2.controller.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_3_SYMBOL_NAME, color9);
						bridgeInfo2.controller.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_4_SYMBOL_NAME, color10);
					}
				}
			}
			this.UpdateUI();
		}

		// Token: 0x06008D54 RID: 36180 RVA: 0x00307AAC File Offset: 0x00305CAC
		private void UpdateUI()
		{
			Color32 logicOn = GlobalAssets.Instance.colorSet.logicOn;
			Color32 logicOff = GlobalAssets.Instance.colorSet.logicOff;
			Color32 logicDisconnected = GlobalAssets.Instance.colorSet.logicDisconnected;
			logicOff.a = (logicOn.a = byte.MaxValue);
			foreach (OverlayModes.Logic.UIInfo uiinfo in this.uiInfo.GetDataList())
			{
				LogicCircuitNetwork networkForCell = Game.Instance.logicCircuitManager.GetNetworkForCell(uiinfo.cell);
				Color32 color = logicDisconnected;
				LogicControlInputUI component = uiinfo.instance.GetComponent<LogicControlInputUI>();
				if (component != null)
				{
					component.SetContent(networkForCell);
				}
				else if (uiinfo.bitDepth == 4)
				{
					LogicRibbonDisplayUI component2 = uiinfo.instance.GetComponent<LogicRibbonDisplayUI>();
					if (component2 != null)
					{
						component2.SetContent(networkForCell);
					}
				}
				else if (uiinfo.bitDepth == 1)
				{
					if (networkForCell != null)
					{
						color = (networkForCell.IsBitActive(0) ? logicOn : logicOff);
					}
					if (uiinfo.image.color != color)
					{
						uiinfo.image.color = color;
					}
				}
			}
		}

		// Token: 0x06008D55 RID: 36181 RVA: 0x00307C04 File Offset: 0x00305E04
		private void AddUI(ILogicUIElement ui_elem)
		{
			if (this.uiNodes.ContainsKey(ui_elem))
			{
				return;
			}
			HandleVector<int>.Handle handle = this.uiInfo.Allocate(new OverlayModes.Logic.UIInfo(ui_elem, this.uiAsset));
			this.uiNodes.Add(ui_elem, new OverlayModes.Logic.EventInfo
			{
				uiHandle = handle
			});
		}

		// Token: 0x06008D56 RID: 36182 RVA: 0x00307C58 File Offset: 0x00305E58
		private void FreeUI(ILogicUIElement item)
		{
			if (item == null)
			{
				return;
			}
			OverlayModes.Logic.EventInfo eventInfo;
			if (this.uiNodes.TryGetValue(item, out eventInfo))
			{
				this.uiInfo.GetData(eventInfo.uiHandle).Release();
				this.uiInfo.Free(eventInfo.uiHandle);
				this.uiNodes.Remove(item);
			}
		}

		// Token: 0x06008D57 RID: 36183 RVA: 0x00307CB4 File Offset: 0x00305EB4
		protected UniformGrid<ILogicUIElement> CreateLogicUIPartition()
		{
			UniformGrid<ILogicUIElement> uniformGrid = new UniformGrid<ILogicUIElement>(Grid.WidthInCells, Grid.HeightInCells, 8, 8);
			foreach (ILogicUIElement logicUIElement in Game.Instance.logicCircuitManager.GetVisElements())
			{
				if (logicUIElement != null)
				{
					uniformGrid.Add(logicUIElement);
				}
			}
			return uniformGrid;
		}

		// Token: 0x06008D58 RID: 36184 RVA: 0x00307D20 File Offset: 0x00305F20
		private bool IsBitActive(int value, int bit)
		{
			return (value & (1 << bit)) > 0;
		}

		// Token: 0x06008D59 RID: 36185 RVA: 0x00307D30 File Offset: 0x00305F30
		private void FindConnectedNetworks(int cell, IUtilityNetworkMgr mgr, ICollection<UtilityNetwork> networks, List<int> visited)
		{
			if (visited.Contains(cell))
			{
				return;
			}
			visited.Add(cell);
			UtilityNetwork networkForCell = mgr.GetNetworkForCell(cell);
			if (networkForCell != null)
			{
				networks.Add(networkForCell);
				UtilityConnections connections = mgr.GetConnections(cell, false);
				if ((connections & UtilityConnections.Right) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellRight(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Left) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellLeft(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Up) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellAbove(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Down) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellBelow(cell), mgr, networks, visited);
				}
			}
		}

		// Token: 0x06008D5A RID: 36186 RVA: 0x00307DC0 File Offset: 0x00305FC0
		private void ResetRibbonSymbolTints<T>(ICollection<T> targets) where T : MonoBehaviour
		{
			foreach (T t in targets)
			{
				if (!(t == null))
				{
					KBatchedAnimController component = t.GetComponent<KBatchedAnimController>();
					this.ResetRibbonTint(component);
				}
			}
		}

		// Token: 0x06008D5B RID: 36187 RVA: 0x00307E24 File Offset: 0x00306024
		private void ResetRibbonTint(KBatchedAnimController kbac)
		{
			if (kbac != null)
			{
				kbac.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_1_SYMBOL_NAME, Color.white);
				kbac.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_2_SYMBOL_NAME, Color.white);
				kbac.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_3_SYMBOL_NAME, Color.white);
				kbac.SetSymbolTint(OverlayModes.Logic.RIBBON_WIRE_4_SYMBOL_NAME, Color.white);
			}
		}

		// Token: 0x04006F8A RID: 28554
		public static readonly HashedString ID = "Logic";

		// Token: 0x04006F8B RID: 28555
		public static HashSet<Tag> HighlightItemIDs = new HashSet<Tag>();

		// Token: 0x04006F8C RID: 28556
		public static KAnimHashedString RIBBON_WIRE_1_SYMBOL_NAME = "wire1";

		// Token: 0x04006F8D RID: 28557
		public static KAnimHashedString RIBBON_WIRE_2_SYMBOL_NAME = "wire2";

		// Token: 0x04006F8E RID: 28558
		public static KAnimHashedString RIBBON_WIRE_3_SYMBOL_NAME = "wire3";

		// Token: 0x04006F8F RID: 28559
		public static KAnimHashedString RIBBON_WIRE_4_SYMBOL_NAME = "wire4";

		// Token: 0x04006F90 RID: 28560
		private int conduitTargetLayer;

		// Token: 0x04006F91 RID: 28561
		private int objectTargetLayer;

		// Token: 0x04006F92 RID: 28562
		private int cameraLayerMask;

		// Token: 0x04006F93 RID: 28563
		private int selectionMask;

		// Token: 0x04006F94 RID: 28564
		private UniformGrid<ILogicUIElement> ioPartition;

		// Token: 0x04006F95 RID: 28565
		private HashSet<ILogicUIElement> ioTargets = new HashSet<ILogicUIElement>();

		// Token: 0x04006F96 RID: 28566
		private HashSet<ILogicUIElement> workingIOTargets = new HashSet<ILogicUIElement>();

		// Token: 0x04006F97 RID: 28567
		private HashSet<KBatchedAnimController> wireControllers = new HashSet<KBatchedAnimController>();

		// Token: 0x04006F98 RID: 28568
		private HashSet<KBatchedAnimController> ribbonControllers = new HashSet<KBatchedAnimController>();

		// Token: 0x04006F99 RID: 28569
		private HashSet<UtilityNetwork> connectedNetworks = new HashSet<UtilityNetwork>();

		// Token: 0x04006F9A RID: 28570
		private List<int> visited = new List<int>();

		// Token: 0x04006F9B RID: 28571
		private HashSet<OverlayModes.Logic.BridgeInfo> bridgeControllers = new HashSet<OverlayModes.Logic.BridgeInfo>();

		// Token: 0x04006F9C RID: 28572
		private HashSet<OverlayModes.Logic.BridgeInfo> ribbonBridgeControllers = new HashSet<OverlayModes.Logic.BridgeInfo>();

		// Token: 0x04006F9D RID: 28573
		private UniformGrid<SaveLoadRoot> gameObjPartition;

		// Token: 0x04006F9E RID: 28574
		private HashSet<SaveLoadRoot> gameObjTargets = new HashSet<SaveLoadRoot>();

		// Token: 0x04006F9F RID: 28575
		private LogicModeUI uiAsset;

		// Token: 0x04006FA0 RID: 28576
		private Dictionary<ILogicUIElement, OverlayModes.Logic.EventInfo> uiNodes = new Dictionary<ILogicUIElement, OverlayModes.Logic.EventInfo>();

		// Token: 0x04006FA1 RID: 28577
		private KCompactedVector<OverlayModes.Logic.UIInfo> uiInfo = new KCompactedVector<OverlayModes.Logic.UIInfo>(0);

		// Token: 0x020020E9 RID: 8425
		private struct BridgeInfo
		{
			// Token: 0x04009290 RID: 37520
			public int cell;

			// Token: 0x04009291 RID: 37521
			public KBatchedAnimController controller;
		}

		// Token: 0x020020EA RID: 8426
		private struct EventInfo
		{
			// Token: 0x04009292 RID: 37522
			public HandleVector<int>.Handle uiHandle;
		}

		// Token: 0x020020EB RID: 8427
		private struct UIInfo
		{
			// Token: 0x0600A57F RID: 42367 RVA: 0x0034A110 File Offset: 0x00348310
			public UIInfo(ILogicUIElement ui_elem, LogicModeUI ui_data)
			{
				this.cell = ui_elem.GetLogicUICell();
				GameObject gameObject = null;
				Sprite sprite = null;
				this.bitDepth = 1;
				switch (ui_elem.GetLogicPortSpriteType())
				{
				case LogicPortSpriteType.Input:
					gameObject = ui_data.prefab;
					sprite = ui_data.inputSprite;
					break;
				case LogicPortSpriteType.Output:
					gameObject = ui_data.prefab;
					sprite = ui_data.outputSprite;
					break;
				case LogicPortSpriteType.ResetUpdate:
					gameObject = ui_data.prefab;
					sprite = ui_data.resetSprite;
					break;
				case LogicPortSpriteType.ControlInput:
					gameObject = ui_data.controlInputPrefab;
					break;
				case LogicPortSpriteType.RibbonInput:
					gameObject = ui_data.ribbonInputPrefab;
					this.bitDepth = 4;
					break;
				case LogicPortSpriteType.RibbonOutput:
					gameObject = ui_data.ribbonOutputPrefab;
					this.bitDepth = 4;
					break;
				}
				this.instance = global::Util.KInstantiate(gameObject, Grid.CellToPosCCC(this.cell, Grid.SceneLayer.Front), Quaternion.identity, GameScreenManager.Instance.worldSpaceCanvas, null, true, 0);
				this.instance.SetActive(true);
				this.image = this.instance.GetComponent<Image>();
				if (this.image != null)
				{
					this.image.raycastTarget = false;
					this.image.sprite = sprite;
				}
			}

			// Token: 0x0600A580 RID: 42368 RVA: 0x0034A220 File Offset: 0x00348420
			public void Release()
			{
				global::Util.KDestroyGameObject(this.instance);
			}

			// Token: 0x04009293 RID: 37523
			public GameObject instance;

			// Token: 0x04009294 RID: 37524
			public Image image;

			// Token: 0x04009295 RID: 37525
			public int cell;

			// Token: 0x04009296 RID: 37526
			public int bitDepth;
		}
	}

	// Token: 0x02001835 RID: 6197
	public enum BringToFrontLayerSetting
	{
		// Token: 0x04006FA3 RID: 28579
		None,
		// Token: 0x04006FA4 RID: 28580
		Constant,
		// Token: 0x04006FA5 RID: 28581
		Conditional
	}

	// Token: 0x02001836 RID: 6198
	public class ColorHighlightCondition
	{
		// Token: 0x06008D5D RID: 36189 RVA: 0x00307EDE File Offset: 0x003060DE
		public ColorHighlightCondition(Func<KMonoBehaviour, Color> highlight_color, Func<KMonoBehaviour, bool> highlight_condition)
		{
			this.highlight_color = highlight_color;
			this.highlight_condition = highlight_condition;
		}

		// Token: 0x04006FA6 RID: 28582
		public Func<KMonoBehaviour, Color> highlight_color;

		// Token: 0x04006FA7 RID: 28583
		public Func<KMonoBehaviour, bool> highlight_condition;
	}

	// Token: 0x02001837 RID: 6199
	public class None : OverlayModes.Mode
	{
		// Token: 0x06008D5E RID: 36190 RVA: 0x00307EF4 File Offset: 0x003060F4
		public override HashedString ViewMode()
		{
			return OverlayModes.None.ID;
		}

		// Token: 0x06008D5F RID: 36191 RVA: 0x00307EFB File Offset: 0x003060FB
		public override string GetSoundName()
		{
			return "Off";
		}

		// Token: 0x04006FA8 RID: 28584
		public static readonly HashedString ID = HashedString.Invalid;
	}

	// Token: 0x02001838 RID: 6200
	public class PathProber : OverlayModes.Mode
	{
		// Token: 0x06008D62 RID: 36194 RVA: 0x00307F16 File Offset: 0x00306116
		public override HashedString ViewMode()
		{
			return OverlayModes.PathProber.ID;
		}

		// Token: 0x06008D63 RID: 36195 RVA: 0x00307F1D File Offset: 0x0030611D
		public override string GetSoundName()
		{
			return "Off";
		}

		// Token: 0x04006FA9 RID: 28585
		public static readonly HashedString ID = "PathProber";
	}

	// Token: 0x02001839 RID: 6201
	public class Oxygen : OverlayModes.Mode
	{
		// Token: 0x06008D66 RID: 36198 RVA: 0x00307F3D File Offset: 0x0030613D
		public override HashedString ViewMode()
		{
			return OverlayModes.Oxygen.ID;
		}

		// Token: 0x06008D67 RID: 36199 RVA: 0x00307F44 File Offset: 0x00306144
		public override string GetSoundName()
		{
			return "Oxygen";
		}

		// Token: 0x06008D68 RID: 36200 RVA: 0x00307F4C File Offset: 0x0030614C
		public override void Enable()
		{
			base.Enable();
			int defaultLayerMask = SelectTool.Instance.GetDefaultLayerMask();
			int mask = LayerMask.GetMask(new string[] { "MaskedOverlay" });
			SelectTool.Instance.SetLayerMask(defaultLayerMask | mask);
		}

		// Token: 0x06008D69 RID: 36201 RVA: 0x00307F8B File Offset: 0x0030618B
		public override void Disable()
		{
			base.Disable();
			SelectTool.Instance.ClearLayerMask();
		}

		// Token: 0x04006FAA RID: 28586
		public static readonly HashedString ID = "Oxygen";
	}

	// Token: 0x0200183A RID: 6202
	public class Light : OverlayModes.Mode
	{
		// Token: 0x06008D6C RID: 36204 RVA: 0x00307FB6 File Offset: 0x003061B6
		public override HashedString ViewMode()
		{
			return OverlayModes.Light.ID;
		}

		// Token: 0x06008D6D RID: 36205 RVA: 0x00307FBD File Offset: 0x003061BD
		public override string GetSoundName()
		{
			return "Lights";
		}

		// Token: 0x04006FAB RID: 28587
		public static readonly HashedString ID = "Light";
	}

	// Token: 0x0200183B RID: 6203
	public class Priorities : OverlayModes.Mode
	{
		// Token: 0x06008D70 RID: 36208 RVA: 0x00307FDD File Offset: 0x003061DD
		public override HashedString ViewMode()
		{
			return OverlayModes.Priorities.ID;
		}

		// Token: 0x06008D71 RID: 36209 RVA: 0x00307FE4 File Offset: 0x003061E4
		public override string GetSoundName()
		{
			return "Priorities";
		}

		// Token: 0x04006FAC RID: 28588
		public static readonly HashedString ID = "Priorities";
	}

	// Token: 0x0200183C RID: 6204
	public class ThermalConductivity : OverlayModes.Mode
	{
		// Token: 0x06008D74 RID: 36212 RVA: 0x00308004 File Offset: 0x00306204
		public override HashedString ViewMode()
		{
			return OverlayModes.ThermalConductivity.ID;
		}

		// Token: 0x06008D75 RID: 36213 RVA: 0x0030800B File Offset: 0x0030620B
		public override string GetSoundName()
		{
			return "HeatFlow";
		}

		// Token: 0x04006FAD RID: 28589
		public static readonly HashedString ID = "ThermalConductivity";
	}

	// Token: 0x0200183D RID: 6205
	public class HeatFlow : OverlayModes.Mode
	{
		// Token: 0x06008D78 RID: 36216 RVA: 0x0030802B File Offset: 0x0030622B
		public override HashedString ViewMode()
		{
			return OverlayModes.HeatFlow.ID;
		}

		// Token: 0x06008D79 RID: 36217 RVA: 0x00308032 File Offset: 0x00306232
		public override string GetSoundName()
		{
			return "HeatFlow";
		}

		// Token: 0x04006FAE RID: 28590
		public static readonly HashedString ID = "HeatFlow";
	}

	// Token: 0x0200183E RID: 6206
	public class Rooms : OverlayModes.Mode
	{
		// Token: 0x06008D7C RID: 36220 RVA: 0x00308052 File Offset: 0x00306252
		public override HashedString ViewMode()
		{
			return OverlayModes.Rooms.ID;
		}

		// Token: 0x06008D7D RID: 36221 RVA: 0x00308059 File Offset: 0x00306259
		public override string GetSoundName()
		{
			return "Rooms";
		}

		// Token: 0x06008D7E RID: 36222 RVA: 0x00308060 File Offset: 0x00306260
		public override List<LegendEntry> GetCustomLegendData()
		{
			List<LegendEntry> list = new List<LegendEntry>();
			List<RoomType> list2 = new List<RoomType>(Db.Get().RoomTypes.resources);
			list2.Sort((RoomType a, RoomType b) => a.sortKey.CompareTo(b.sortKey));
			foreach (RoomType roomType in list2)
			{
				string text = roomType.GetCriteriaString();
				if (roomType.effects != null && roomType.effects.Length != 0)
				{
					text = text + "\n\n" + roomType.GetRoomEffectsString();
				}
				list.Add(new LegendEntry(roomType.Name + "\n" + roomType.effect, text, GlobalAssets.Instance.colorSet.GetColorByName(roomType.category.colorName), null, null, true));
			}
			return list;
		}

		// Token: 0x04006FAF RID: 28591
		public static readonly HashedString ID = "Rooms";
	}

	// Token: 0x0200183F RID: 6207
	public abstract class Mode
	{
		// Token: 0x06008D81 RID: 36225 RVA: 0x0030816D File Offset: 0x0030636D
		public static void Clear()
		{
			OverlayModes.Mode.workingTargets.Clear();
		}

		// Token: 0x06008D82 RID: 36226
		public abstract HashedString ViewMode();

		// Token: 0x06008D83 RID: 36227 RVA: 0x00308179 File Offset: 0x00306379
		public virtual void Enable()
		{
		}

		// Token: 0x06008D84 RID: 36228 RVA: 0x0030817B File Offset: 0x0030637B
		public virtual void Update()
		{
		}

		// Token: 0x06008D85 RID: 36229 RVA: 0x0030817D File Offset: 0x0030637D
		public virtual void Disable()
		{
		}

		// Token: 0x06008D86 RID: 36230 RVA: 0x0030817F File Offset: 0x0030637F
		public virtual List<LegendEntry> GetCustomLegendData()
		{
			return null;
		}

		// Token: 0x06008D87 RID: 36231 RVA: 0x00308182 File Offset: 0x00306382
		public virtual Dictionary<string, ToolParameterMenu.ToggleState> CreateDefaultFilters()
		{
			return null;
		}

		// Token: 0x06008D88 RID: 36232 RVA: 0x00308185 File Offset: 0x00306385
		public virtual void OnFiltersChanged()
		{
		}

		// Token: 0x06008D89 RID: 36233 RVA: 0x00308187 File Offset: 0x00306387
		public virtual void DisableOverlay()
		{
		}

		// Token: 0x06008D8A RID: 36234
		public abstract string GetSoundName();

		// Token: 0x06008D8B RID: 36235 RVA: 0x00308189 File Offset: 0x00306389
		protected bool InFilter(string layer, Dictionary<string, ToolParameterMenu.ToggleState> filter)
		{
			return (filter.ContainsKey(ToolParameterMenu.FILTERLAYERS.ALL) && filter[ToolParameterMenu.FILTERLAYERS.ALL] == ToolParameterMenu.ToggleState.On) || (filter.ContainsKey(layer) && filter[layer] == ToolParameterMenu.ToggleState.On);
		}

		// Token: 0x06008D8C RID: 36236 RVA: 0x003081BC File Offset: 0x003063BC
		public void RegisterSaveLoadListeners()
		{
			SaveManager saveManager = SaveLoader.Instance.saveManager;
			saveManager.onRegister += this.OnSaveLoadRootRegistered;
			saveManager.onUnregister += this.OnSaveLoadRootUnregistered;
		}

		// Token: 0x06008D8D RID: 36237 RVA: 0x003081ED File Offset: 0x003063ED
		public void UnregisterSaveLoadListeners()
		{
			SaveManager saveManager = SaveLoader.Instance.saveManager;
			saveManager.onRegister -= this.OnSaveLoadRootRegistered;
			saveManager.onUnregister -= this.OnSaveLoadRootUnregistered;
		}

		// Token: 0x06008D8E RID: 36238 RVA: 0x0030821E File Offset: 0x0030641E
		protected virtual void OnSaveLoadRootRegistered(SaveLoadRoot root)
		{
		}

		// Token: 0x06008D8F RID: 36239 RVA: 0x00308220 File Offset: 0x00306420
		protected virtual void OnSaveLoadRootUnregistered(SaveLoadRoot root)
		{
		}

		// Token: 0x06008D90 RID: 36240 RVA: 0x00308224 File Offset: 0x00306424
		protected void ProcessExistingSaveLoadRoots()
		{
			foreach (KeyValuePair<Tag, List<SaveLoadRoot>> keyValuePair in SaveLoader.Instance.saveManager.GetLists())
			{
				foreach (SaveLoadRoot saveLoadRoot in keyValuePair.Value)
				{
					this.OnSaveLoadRootRegistered(saveLoadRoot);
				}
			}
		}

		// Token: 0x06008D91 RID: 36241 RVA: 0x003082BC File Offset: 0x003064BC
		protected static UniformGrid<T> PopulatePartition<T>(ICollection<Tag> tags) where T : IUniformGridObject
		{
			Dictionary<Tag, List<SaveLoadRoot>> lists = SaveLoader.Instance.saveManager.GetLists();
			UniformGrid<T> uniformGrid = new UniformGrid<T>(Grid.WidthInCells, Grid.HeightInCells, 8, 8);
			foreach (Tag tag in tags)
			{
				List<SaveLoadRoot> list = null;
				if (lists.TryGetValue(tag, out list))
				{
					foreach (SaveLoadRoot saveLoadRoot in list)
					{
						T component = saveLoadRoot.GetComponent<T>();
						if (component != null)
						{
							uniformGrid.Add(component);
						}
					}
				}
			}
			return uniformGrid;
		}

		// Token: 0x06008D92 RID: 36242 RVA: 0x00308380 File Offset: 0x00306580
		protected static void ResetDisplayValues<T>(ICollection<T> targets) where T : MonoBehaviour
		{
			foreach (T t in targets)
			{
				if (!(t == null))
				{
					KBatchedAnimController component = t.GetComponent<KBatchedAnimController>();
					if (component != null)
					{
						OverlayModes.Mode.ResetDisplayValues(component);
					}
				}
			}
		}

		// Token: 0x06008D93 RID: 36243 RVA: 0x003083EC File Offset: 0x003065EC
		protected static void ResetDisplayValues(KBatchedAnimController controller)
		{
			controller.SetLayer(0);
			controller.HighlightColour = Color.clear;
			controller.TintColour = Color.white;
			controller.SetLayer(controller.GetComponent<KPrefabID>().defaultLayer);
		}

		// Token: 0x06008D94 RID: 36244 RVA: 0x00308428 File Offset: 0x00306628
		protected static void RemoveOffscreenTargets<T>(ICollection<T> targets, Vector2I min, Vector2I max, Action<T> on_removed = null) where T : KMonoBehaviour
		{
			OverlayModes.Mode.ClearOutsideViewObjects<T>(targets, min, max, null, delegate(T cmp)
			{
				if (cmp != null)
				{
					KBatchedAnimController component = cmp.GetComponent<KBatchedAnimController>();
					if (component != null)
					{
						OverlayModes.Mode.ResetDisplayValues(component);
					}
					if (on_removed != null)
					{
						on_removed(cmp);
					}
				}
			});
			OverlayModes.Mode.workingTargets.Clear();
		}

		// Token: 0x06008D95 RID: 36245 RVA: 0x00308464 File Offset: 0x00306664
		protected static void ClearOutsideViewObjects<T>(ICollection<T> targets, Vector2I vis_min, Vector2I vis_max, ICollection<Tag> item_ids, Action<T> on_remove) where T : KMonoBehaviour
		{
			OverlayModes.Mode.workingTargets.Clear();
			foreach (T t in targets)
			{
				if (!(t == null))
				{
					Vector2I vector2I = Grid.PosToXY(t.transform.GetPosition());
					if (!(vis_min <= vector2I) || !(vector2I <= vis_max) || t.gameObject.GetMyWorldId() != ClusterManager.Instance.activeWorldId)
					{
						OverlayModes.Mode.workingTargets.Add(t);
					}
					else
					{
						KPrefabID component = t.GetComponent<KPrefabID>();
						if (item_ids != null && !item_ids.Contains(component.PrefabTag) && t.gameObject.GetMyWorldId() != ClusterManager.Instance.activeWorldId)
						{
							OverlayModes.Mode.workingTargets.Add(t);
						}
					}
				}
			}
			foreach (KMonoBehaviour kmonoBehaviour in OverlayModes.Mode.workingTargets)
			{
				T t2 = (T)((object)kmonoBehaviour);
				if (!(t2 == null))
				{
					if (on_remove != null)
					{
						on_remove(t2);
					}
					targets.Remove(t2);
				}
			}
			OverlayModes.Mode.workingTargets.Clear();
		}

		// Token: 0x06008D96 RID: 36246 RVA: 0x003085D8 File Offset: 0x003067D8
		protected static void RemoveOffscreenTargets<T>(ICollection<T> targets, ICollection<T> working_targets, Vector2I vis_min, Vector2I vis_max, Action<T> on_removed = null, Func<T, bool> special_clear_condition = null) where T : IUniformGridObject
		{
			OverlayModes.Mode.ClearOutsideViewObjects<T>(targets, working_targets, vis_min, vis_max, delegate(T cmp)
			{
				if (cmp != null && on_removed != null)
				{
					on_removed(cmp);
				}
			});
			if (special_clear_condition != null)
			{
				working_targets.Clear();
				foreach (T t in targets)
				{
					if (special_clear_condition(t))
					{
						working_targets.Add(t);
					}
				}
				foreach (T t2 in working_targets)
				{
					if (t2 != null)
					{
						if (on_removed != null)
						{
							on_removed(t2);
						}
						targets.Remove(t2);
					}
				}
				working_targets.Clear();
			}
		}

		// Token: 0x06008D97 RID: 36247 RVA: 0x003086B4 File Offset: 0x003068B4
		protected static void ClearOutsideViewObjects<T>(ICollection<T> targets, ICollection<T> working_targets, Vector2I vis_min, Vector2I vis_max, Action<T> on_removed = null) where T : IUniformGridObject
		{
			working_targets.Clear();
			foreach (T t in targets)
			{
				if (t != null)
				{
					Vector2 vector = t.PosMin();
					Vector2 vector2 = t.PosMin();
					if (vector2.x < (float)vis_min.x || vector2.y < (float)vis_min.y || (float)vis_max.x < vector.x || (float)vis_max.y < vector.y)
					{
						working_targets.Add(t);
					}
				}
			}
			foreach (T t2 in working_targets)
			{
				if (t2 != null)
				{
					if (on_removed != null)
					{
						on_removed(t2);
					}
					targets.Remove(t2);
				}
			}
			working_targets.Clear();
		}

		// Token: 0x06008D98 RID: 36248 RVA: 0x003087B8 File Offset: 0x003069B8
		protected static float GetDefaultDepth(KMonoBehaviour cmp)
		{
			BuildingComplete component = cmp.GetComponent<BuildingComplete>();
			float num;
			if (component != null)
			{
				num = Grid.GetLayerZ(component.Def.SceneLayer);
			}
			else
			{
				num = Grid.GetLayerZ(Grid.SceneLayer.Creatures);
			}
			return num;
		}

		// Token: 0x06008D99 RID: 36249 RVA: 0x003087F4 File Offset: 0x003069F4
		protected void UpdateHighlightTypeOverlay<T>(Vector2I min, Vector2I max, ICollection<T> targets, ICollection<Tag> item_ids, OverlayModes.ColorHighlightCondition[] highlights, OverlayModes.BringToFrontLayerSetting bringToFrontSetting, int layer) where T : KMonoBehaviour
		{
			foreach (T t in targets)
			{
				if (!(t == null))
				{
					Vector3 position = t.transform.GetPosition();
					int num = Grid.PosToCell(position);
					if (Grid.IsValidCell(num) && Grid.IsVisible(num) && min <= position && position <= max)
					{
						KBatchedAnimController component = t.GetComponent<KBatchedAnimController>();
						if (!(component == null))
						{
							int num2 = 0;
							Color32 color = Color.clear;
							if (highlights != null)
							{
								foreach (OverlayModes.ColorHighlightCondition colorHighlightCondition in highlights)
								{
									if (colorHighlightCondition.highlight_condition(t))
									{
										color = colorHighlightCondition.highlight_color(t);
										num2 = layer;
										break;
									}
								}
							}
							if (bringToFrontSetting != OverlayModes.BringToFrontLayerSetting.Constant)
							{
								if (bringToFrontSetting == OverlayModes.BringToFrontLayerSetting.Conditional)
								{
									component.SetLayer(num2);
								}
							}
							else
							{
								component.SetLayer(layer);
							}
							component.HighlightColour = color;
						}
					}
				}
			}
		}

		// Token: 0x06008D9A RID: 36250 RVA: 0x0030894C File Offset: 0x00306B4C
		protected void DisableHighlightTypeOverlay<T>(ICollection<T> targets) where T : KMonoBehaviour
		{
			Color32 color = Color.clear;
			foreach (T t in targets)
			{
				if (!(t == null))
				{
					KBatchedAnimController component = t.GetComponent<KBatchedAnimController>();
					if (component != null)
					{
						component.HighlightColour = color;
						component.SetLayer(0);
					}
				}
			}
			targets.Clear();
		}

		// Token: 0x06008D9B RID: 36251 RVA: 0x003089D0 File Offset: 0x00306BD0
		protected void AddTargetIfVisible<T>(T instance, Vector2I vis_min, Vector2I vis_max, ICollection<T> targets, int layer, Action<T> on_added = null, Func<KMonoBehaviour, bool> should_add = null) where T : IUniformGridObject
		{
			if (instance.Equals(null))
			{
				return;
			}
			Vector2 vector = instance.PosMin();
			Vector2 vector2 = instance.PosMax();
			if (vector2.x < (float)vis_min.x || vector2.y < (float)vis_min.y || vector.x > (float)vis_max.x || vector.y > (float)vis_max.y)
			{
				return;
			}
			if (targets.Contains(instance))
			{
				return;
			}
			bool flag = false;
			int num = (int)vector.y;
			while ((float)num <= vector2.y)
			{
				int num2 = (int)vector.x;
				while ((float)num2 <= vector2.x)
				{
					int num3 = Grid.XYToCell(num2, num);
					if ((Grid.IsValidCell(num3) && Grid.Visible[num3] > 20 && (int)Grid.WorldIdx[num3] == ClusterManager.Instance.activeWorldId) || !PropertyTextures.IsFogOfWarEnabled)
					{
						flag = true;
						break;
					}
					num2++;
				}
				num++;
			}
			if (flag)
			{
				bool flag2 = true;
				KMonoBehaviour kmonoBehaviour = instance as KMonoBehaviour;
				if (kmonoBehaviour != null && should_add != null)
				{
					flag2 = should_add(kmonoBehaviour);
				}
				if (flag2)
				{
					if (kmonoBehaviour != null)
					{
						KBatchedAnimController component = kmonoBehaviour.GetComponent<KBatchedAnimController>();
						if (component != null)
						{
							component.SetLayer(layer);
						}
					}
					targets.Add(instance);
					if (on_added != null)
					{
						on_added(instance);
					}
				}
			}
		}

		// Token: 0x04006FB0 RID: 28592
		public Dictionary<string, ToolParameterMenu.ToggleState> legendFilters;

		// Token: 0x04006FB1 RID: 28593
		private static List<KMonoBehaviour> workingTargets = new List<KMonoBehaviour>();
	}

	// Token: 0x02001840 RID: 6208
	public class ModeUtil
	{
		// Token: 0x06008D9E RID: 36254 RVA: 0x00308B44 File Offset: 0x00306D44
		public static float GetHighlightScale()
		{
			return Mathf.SmoothStep(0.5f, 1f, Mathf.Abs(Mathf.Sin(Time.unscaledTime * 4f)));
		}
	}

	// Token: 0x02001841 RID: 6209
	public class Power : OverlayModes.Mode
	{
		// Token: 0x06008DA0 RID: 36256 RVA: 0x00308B72 File Offset: 0x00306D72
		public override HashedString ViewMode()
		{
			return OverlayModes.Power.ID;
		}

		// Token: 0x06008DA1 RID: 36257 RVA: 0x00308B79 File Offset: 0x00306D79
		public override string GetSoundName()
		{
			return "Power";
		}

		// Token: 0x06008DA2 RID: 36258 RVA: 0x00308B80 File Offset: 0x00306D80
		public Power(Canvas powerLabelParent, LocText powerLabelPrefab, BatteryUI batteryUIPrefab, Vector3 powerLabelOffset, Vector3 batteryUIOffset, Vector3 batteryUITransformerOffset, Vector3 batteryUISmallTransformerOffset)
		{
			this.powerLabelParent = powerLabelParent;
			this.powerLabelPrefab = powerLabelPrefab;
			this.batteryUIPrefab = batteryUIPrefab;
			this.powerLabelOffset = powerLabelOffset;
			this.batteryUIOffset = batteryUIOffset;
			this.batteryUITransformerOffset = batteryUITransformerOffset;
			this.batteryUISmallTransformerOffset = batteryUISmallTransformerOffset;
			this.targetLayer = LayerMask.NameToLayer("MaskedOverlay");
			this.cameraLayerMask = LayerMask.GetMask(new string[] { "MaskedOverlay", "MaskedOverlayBG" });
			this.selectionMask = this.cameraLayerMask;
		}

		// Token: 0x06008DA3 RID: 36259 RVA: 0x00308C68 File Offset: 0x00306E68
		public override void Enable()
		{
			Camera.main.cullingMask |= this.cameraLayerMask;
			SelectTool.Instance.SetLayerMask(this.selectionMask);
			base.RegisterSaveLoadListeners();
			this.partition = OverlayModes.Mode.PopulatePartition<SaveLoadRoot>(OverlayScreen.WireIDs);
			GridCompositor.Instance.ToggleMinor(true);
		}

		// Token: 0x06008DA4 RID: 36260 RVA: 0x00308CC0 File Offset: 0x00306EC0
		public override void Disable()
		{
			OverlayModes.Mode.ResetDisplayValues<SaveLoadRoot>(this.layerTargets);
			Camera.main.cullingMask &= ~this.cameraLayerMask;
			SelectTool.Instance.ClearLayerMask();
			base.UnregisterSaveLoadListeners();
			this.partition.Clear();
			this.layerTargets.Clear();
			this.privateTargets.Clear();
			this.queuedAdds.Clear();
			this.DisablePowerLabels();
			this.DisableBatteryUIs();
			GridCompositor.Instance.ToggleMinor(false);
		}

		// Token: 0x06008DA5 RID: 36261 RVA: 0x00308D44 File Offset: 0x00306F44
		protected override void OnSaveLoadRootRegistered(SaveLoadRoot item)
		{
			Tag saveLoadTag = item.GetComponent<KPrefabID>().GetSaveLoadTag();
			if (OverlayScreen.WireIDs.Contains(saveLoadTag))
			{
				this.partition.Add(item);
			}
		}

		// Token: 0x06008DA6 RID: 36262 RVA: 0x00308D78 File Offset: 0x00306F78
		protected override void OnSaveLoadRootUnregistered(SaveLoadRoot item)
		{
			if (item == null || item.gameObject == null)
			{
				return;
			}
			if (this.layerTargets.Contains(item))
			{
				this.layerTargets.Remove(item);
			}
			this.partition.Remove(item);
		}

		// Token: 0x06008DA7 RID: 36263 RVA: 0x00308DC4 File Offset: 0x00306FC4
		public override void Update()
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			OverlayModes.Mode.RemoveOffscreenTargets<SaveLoadRoot>(this.layerTargets, vector2I, vector2I2, null);
			using (new KProfiler.Region("UpdatePowerOverlay", null))
			{
				foreach (object obj in this.partition.GetAllIntersecting(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y)))
				{
					SaveLoadRoot saveLoadRoot = (SaveLoadRoot)obj;
					base.AddTargetIfVisible<SaveLoadRoot>(saveLoadRoot, vector2I, vector2I2, this.layerTargets, this.targetLayer, null, null);
				}
				this.connectedNetworks.Clear();
				float num = 1f;
				GameObject gameObject = null;
				if (SelectTool.Instance != null && SelectTool.Instance.hover != null)
				{
					gameObject = SelectTool.Instance.hover.gameObject;
				}
				if (gameObject != null)
				{
					IBridgedNetworkItem component = gameObject.GetComponent<IBridgedNetworkItem>();
					if (component != null)
					{
						int networkCell = component.GetNetworkCell();
						this.visited.Clear();
						this.FindConnectedNetworks(networkCell, Game.Instance.electricalConduitSystem, this.connectedNetworks, this.visited);
						this.visited.Clear();
						num = OverlayModes.ModeUtil.GetHighlightScale();
					}
				}
				CircuitManager circuitManager = Game.Instance.circuitManager;
				foreach (SaveLoadRoot saveLoadRoot2 in this.layerTargets)
				{
					if (!(saveLoadRoot2 == null))
					{
						IBridgedNetworkItem component2 = saveLoadRoot2.GetComponent<IBridgedNetworkItem>();
						if (component2 != null)
						{
							KAnimControllerBase component3 = (component2 as KMonoBehaviour).GetComponent<KBatchedAnimController>();
							int networkCell2 = component2.GetNetworkCell();
							UtilityNetwork networkForCell = Game.Instance.electricalConduitSystem.GetNetworkForCell(networkCell2);
							ushort num2 = ((networkForCell != null) ? ((ushort)networkForCell.id) : ushort.MaxValue);
							float wattsUsedByCircuit = circuitManager.GetWattsUsedByCircuit(num2);
							float num3 = circuitManager.GetMaxSafeWattageForCircuit(num2);
							num3 += POWER.FLOAT_FUDGE_FACTOR;
							float wattsNeededWhenActive = circuitManager.GetWattsNeededWhenActive(num2);
							Color32 color;
							if (wattsUsedByCircuit <= 0f)
							{
								color = GlobalAssets.Instance.colorSet.powerCircuitUnpowered;
							}
							else if (wattsUsedByCircuit > num3)
							{
								color = GlobalAssets.Instance.colorSet.powerCircuitOverloading;
							}
							else if (wattsNeededWhenActive > num3 && num3 > 0f && wattsUsedByCircuit / num3 >= 0.75f)
							{
								color = GlobalAssets.Instance.colorSet.powerCircuitStraining;
							}
							else
							{
								color = GlobalAssets.Instance.colorSet.powerCircuitSafe;
							}
							if (this.connectedNetworks.Count > 0 && component2.IsConnectedToNetworks(this.connectedNetworks))
							{
								color.r = (byte)((float)color.r * num);
								color.g = (byte)((float)color.g * num);
								color.b = (byte)((float)color.b * num);
							}
							component3.TintColour = color;
						}
					}
				}
			}
			this.queuedAdds.Clear();
			using (new KProfiler.Region("BatteryUI", null))
			{
				foreach (Battery battery in Components.Batteries.Items)
				{
					Vector2I vector2I3 = Grid.PosToXY(battery.transform.GetPosition());
					if (vector2I <= vector2I3 && vector2I3 <= vector2I2 && battery.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
					{
						SaveLoadRoot component4 = battery.GetComponent<SaveLoadRoot>();
						if (!this.privateTargets.Contains(component4))
						{
							this.AddBatteryUI(battery);
							this.queuedAdds.Add(component4);
						}
					}
				}
				foreach (Generator generator in Components.Generators.Items)
				{
					Vector2I vector2I4 = Grid.PosToXY(generator.transform.GetPosition());
					if (vector2I <= vector2I4 && vector2I4 <= vector2I2 && generator.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
					{
						SaveLoadRoot component5 = generator.GetComponent<SaveLoadRoot>();
						if (!this.privateTargets.Contains(component5))
						{
							this.privateTargets.Add(component5);
							if (generator.GetComponent<PowerTransformer>() == null)
							{
								this.AddPowerLabels(generator);
							}
						}
					}
				}
				foreach (EnergyConsumer energyConsumer in Components.EnergyConsumers.Items)
				{
					Vector2I vector2I5 = Grid.PosToXY(energyConsumer.transform.GetPosition());
					if (vector2I <= vector2I5 && vector2I5 <= vector2I2 && energyConsumer.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
					{
						SaveLoadRoot component6 = energyConsumer.GetComponent<SaveLoadRoot>();
						if (!this.privateTargets.Contains(component6))
						{
							this.privateTargets.Add(component6);
							this.AddPowerLabels(energyConsumer);
						}
					}
				}
			}
			foreach (SaveLoadRoot saveLoadRoot3 in this.queuedAdds)
			{
				this.privateTargets.Add(saveLoadRoot3);
			}
			this.queuedAdds.Clear();
			this.UpdatePowerLabels();
		}

		// Token: 0x06008DA8 RID: 36264 RVA: 0x003093DC File Offset: 0x003075DC
		private LocText GetFreePowerLabel()
		{
			LocText locText;
			if (this.freePowerLabelIdx < this.powerLabels.Count)
			{
				locText = this.powerLabels[this.freePowerLabelIdx];
				this.freePowerLabelIdx++;
			}
			else
			{
				locText = global::Util.KInstantiateUI<LocText>(this.powerLabelPrefab.gameObject, this.powerLabelParent.transform.gameObject, false);
				this.powerLabels.Add(locText);
				this.freePowerLabelIdx++;
			}
			return locText;
		}

		// Token: 0x06008DA9 RID: 36265 RVA: 0x00309460 File Offset: 0x00307660
		private void UpdatePowerLabels()
		{
			foreach (OverlayModes.Power.UpdatePowerInfo updatePowerInfo in this.updatePowerInfo)
			{
				KMonoBehaviour item = updatePowerInfo.item;
				LocText powerLabel = updatePowerInfo.powerLabel;
				LocText unitLabel = updatePowerInfo.unitLabel;
				Generator generator = updatePowerInfo.generator;
				IEnergyConsumer consumer = updatePowerInfo.consumer;
				if (updatePowerInfo.item == null || updatePowerInfo.item.gameObject.GetMyWorldId() != ClusterManager.Instance.activeWorldId)
				{
					powerLabel.gameObject.SetActive(false);
				}
				else
				{
					powerLabel.gameObject.SetActive(true);
					if (generator != null && consumer == null)
					{
						int num;
						if (generator.GetComponent<ManualGenerator>() == null)
						{
							generator.GetComponent<Operational>();
							num = Mathf.Max(0, Mathf.RoundToInt(generator.WattageRating));
						}
						else
						{
							num = Mathf.Max(0, Mathf.RoundToInt(generator.WattageRating));
						}
						powerLabel.text = ((num != 0) ? ("+" + num.ToString()) : num.ToString());
						BuildingEnabledButton component = item.GetComponent<BuildingEnabledButton>();
						Color color = ((component != null && !component.IsEnabled) ? GlobalAssets.Instance.colorSet.powerBuildingDisabled : GlobalAssets.Instance.colorSet.powerGenerator);
						powerLabel.color = color;
						unitLabel.color = color;
						BuildingCellVisualizer component2 = generator.GetComponent<BuildingCellVisualizer>();
						if (component2 != null)
						{
							Image outputIcon = component2.GetOutputIcon();
							if (outputIcon != null)
							{
								outputIcon.color = color;
							}
						}
					}
					if (consumer != null)
					{
						BuildingEnabledButton component3 = item.GetComponent<BuildingEnabledButton>();
						Color color2 = ((component3 != null && !component3.IsEnabled) ? GlobalAssets.Instance.colorSet.powerBuildingDisabled : GlobalAssets.Instance.colorSet.powerConsumer);
						int num2 = Mathf.Max(0, Mathf.RoundToInt(consumer.WattsNeededWhenActive));
						string text = num2.ToString();
						powerLabel.text = ((num2 != 0) ? ("-" + text) : text);
						powerLabel.color = color2;
						unitLabel.color = color2;
						Image inputIcon = item.GetComponentInChildren<BuildingCellVisualizer>().GetInputIcon();
						if (inputIcon != null)
						{
							inputIcon.color = color2;
						}
					}
				}
			}
			foreach (OverlayModes.Power.UpdateBatteryInfo updateBatteryInfo in this.updateBatteryInfo)
			{
				updateBatteryInfo.ui.SetContent(updateBatteryInfo.battery);
			}
		}

		// Token: 0x06008DAA RID: 36266 RVA: 0x0030973C File Offset: 0x0030793C
		private void AddPowerLabels(KMonoBehaviour item)
		{
			if (item.gameObject.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
			{
				IEnergyConsumer componentInChildren = item.gameObject.GetComponentInChildren<IEnergyConsumer>();
				Generator componentInChildren2 = item.gameObject.GetComponentInChildren<Generator>();
				if (componentInChildren != null || componentInChildren2 != null)
				{
					float num = -10f;
					if (componentInChildren2 != null)
					{
						LocText freePowerLabel = this.GetFreePowerLabel();
						freePowerLabel.gameObject.SetActive(true);
						freePowerLabel.gameObject.name = item.gameObject.name + "power label";
						LocText component = freePowerLabel.transform.GetChild(0).GetComponent<LocText>();
						component.gameObject.SetActive(true);
						freePowerLabel.enabled = true;
						component.enabled = true;
						Vector3 vector = Grid.CellToPos(componentInChildren2.PowerCell, 0.5f, 0f, 0f);
						freePowerLabel.rectTransform.SetPosition(vector + this.powerLabelOffset + Vector3.up * (num * 0.02f));
						if (componentInChildren != null && componentInChildren.PowerCell == componentInChildren2.PowerCell)
						{
							num -= 15f;
						}
						this.SetToolTip(freePowerLabel, UI.OVERLAYS.POWER.WATTS_GENERATED);
						this.updatePowerInfo.Add(new OverlayModes.Power.UpdatePowerInfo(item, freePowerLabel, component, componentInChildren2, null));
					}
					if (componentInChildren != null && componentInChildren.GetType() != typeof(Battery))
					{
						LocText freePowerLabel2 = this.GetFreePowerLabel();
						LocText component2 = freePowerLabel2.transform.GetChild(0).GetComponent<LocText>();
						freePowerLabel2.gameObject.SetActive(true);
						component2.gameObject.SetActive(true);
						freePowerLabel2.gameObject.name = item.gameObject.name + "power label";
						freePowerLabel2.enabled = true;
						component2.enabled = true;
						Vector3 vector2 = Grid.CellToPos(componentInChildren.PowerCell, 0.5f, 0f, 0f);
						freePowerLabel2.rectTransform.SetPosition(vector2 + this.powerLabelOffset + Vector3.up * (num * 0.02f));
						this.SetToolTip(freePowerLabel2, UI.OVERLAYS.POWER.WATTS_CONSUMED);
						this.updatePowerInfo.Add(new OverlayModes.Power.UpdatePowerInfo(item, freePowerLabel2, component2, null, componentInChildren));
					}
				}
			}
		}

		// Token: 0x06008DAB RID: 36267 RVA: 0x00309988 File Offset: 0x00307B88
		private void DisablePowerLabels()
		{
			this.freePowerLabelIdx = 0;
			foreach (LocText locText in this.powerLabels)
			{
				locText.gameObject.SetActive(false);
			}
			this.updatePowerInfo.Clear();
		}

		// Token: 0x06008DAC RID: 36268 RVA: 0x003099F0 File Offset: 0x00307BF0
		private void AddBatteryUI(Battery bat)
		{
			BatteryUI freeBatteryUI = this.GetFreeBatteryUI();
			freeBatteryUI.SetContent(bat);
			Vector3 vector = Grid.CellToPos(bat.PowerCell, 0.5f, 0f, 0f);
			bool flag = bat.powerTransformer != null;
			float num = 1f;
			Rotatable component = bat.GetComponent<Rotatable>();
			if (component != null && component.GetVisualizerFlipX())
			{
				num = -1f;
			}
			Vector3 vector2 = this.batteryUIOffset;
			if (flag)
			{
				vector2 = ((bat.GetComponent<Building>().Def.WidthInCells == 2) ? this.batteryUISmallTransformerOffset : this.batteryUITransformerOffset);
			}
			vector2.x *= num;
			freeBatteryUI.GetComponent<RectTransform>().SetPosition(Vector3.up + vector + vector2);
			this.updateBatteryInfo.Add(new OverlayModes.Power.UpdateBatteryInfo(bat, freeBatteryUI));
		}

		// Token: 0x06008DAD RID: 36269 RVA: 0x00309AC0 File Offset: 0x00307CC0
		private void SetToolTip(LocText label, string text)
		{
			ToolTip component = label.GetComponent<ToolTip>();
			if (component != null)
			{
				component.toolTip = text;
			}
		}

		// Token: 0x06008DAE RID: 36270 RVA: 0x00309AE4 File Offset: 0x00307CE4
		private void DisableBatteryUIs()
		{
			this.freeBatteryUIIdx = 0;
			foreach (BatteryUI batteryUI in this.batteryUIList)
			{
				batteryUI.gameObject.SetActive(false);
			}
			this.updateBatteryInfo.Clear();
		}

		// Token: 0x06008DAF RID: 36271 RVA: 0x00309B4C File Offset: 0x00307D4C
		private BatteryUI GetFreeBatteryUI()
		{
			BatteryUI batteryUI;
			if (this.freeBatteryUIIdx < this.batteryUIList.Count)
			{
				batteryUI = this.batteryUIList[this.freeBatteryUIIdx];
				batteryUI.gameObject.SetActive(true);
				this.freeBatteryUIIdx++;
			}
			else
			{
				batteryUI = global::Util.KInstantiateUI<BatteryUI>(this.batteryUIPrefab.gameObject, this.powerLabelParent.transform.gameObject, false);
				this.batteryUIList.Add(batteryUI);
				this.freeBatteryUIIdx++;
			}
			return batteryUI;
		}

		// Token: 0x06008DB0 RID: 36272 RVA: 0x00309BDC File Offset: 0x00307DDC
		private void FindConnectedNetworks(int cell, IUtilityNetworkMgr mgr, ICollection<UtilityNetwork> networks, List<int> visited)
		{
			if (visited.Contains(cell))
			{
				return;
			}
			visited.Add(cell);
			UtilityNetwork networkForCell = mgr.GetNetworkForCell(cell);
			if (networkForCell != null)
			{
				networks.Add(networkForCell);
				UtilityConnections connections = mgr.GetConnections(cell, false);
				if ((connections & UtilityConnections.Right) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellRight(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Left) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellLeft(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Up) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellAbove(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Down) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellBelow(cell), mgr, networks, visited);
				}
			}
		}

		// Token: 0x04006FB2 RID: 28594
		public static readonly HashedString ID = "Power";

		// Token: 0x04006FB3 RID: 28595
		private int targetLayer;

		// Token: 0x04006FB4 RID: 28596
		private int cameraLayerMask;

		// Token: 0x04006FB5 RID: 28597
		private int selectionMask;

		// Token: 0x04006FB6 RID: 28598
		private List<OverlayModes.Power.UpdatePowerInfo> updatePowerInfo = new List<OverlayModes.Power.UpdatePowerInfo>();

		// Token: 0x04006FB7 RID: 28599
		private List<OverlayModes.Power.UpdateBatteryInfo> updateBatteryInfo = new List<OverlayModes.Power.UpdateBatteryInfo>();

		// Token: 0x04006FB8 RID: 28600
		private Canvas powerLabelParent;

		// Token: 0x04006FB9 RID: 28601
		private LocText powerLabelPrefab;

		// Token: 0x04006FBA RID: 28602
		private Vector3 powerLabelOffset;

		// Token: 0x04006FBB RID: 28603
		private BatteryUI batteryUIPrefab;

		// Token: 0x04006FBC RID: 28604
		private Vector3 batteryUIOffset;

		// Token: 0x04006FBD RID: 28605
		private Vector3 batteryUITransformerOffset;

		// Token: 0x04006FBE RID: 28606
		private Vector3 batteryUISmallTransformerOffset;

		// Token: 0x04006FBF RID: 28607
		private int freePowerLabelIdx;

		// Token: 0x04006FC0 RID: 28608
		private int freeBatteryUIIdx;

		// Token: 0x04006FC1 RID: 28609
		private List<LocText> powerLabels = new List<LocText>();

		// Token: 0x04006FC2 RID: 28610
		private List<BatteryUI> batteryUIList = new List<BatteryUI>();

		// Token: 0x04006FC3 RID: 28611
		private UniformGrid<SaveLoadRoot> partition;

		// Token: 0x04006FC4 RID: 28612
		private List<SaveLoadRoot> queuedAdds = new List<SaveLoadRoot>();

		// Token: 0x04006FC5 RID: 28613
		private HashSet<SaveLoadRoot> layerTargets = new HashSet<SaveLoadRoot>();

		// Token: 0x04006FC6 RID: 28614
		private HashSet<SaveLoadRoot> privateTargets = new HashSet<SaveLoadRoot>();

		// Token: 0x04006FC7 RID: 28615
		private HashSet<UtilityNetwork> connectedNetworks = new HashSet<UtilityNetwork>();

		// Token: 0x04006FC8 RID: 28616
		private List<int> visited = new List<int>();

		// Token: 0x020020F3 RID: 8435
		private struct UpdatePowerInfo
		{
			// Token: 0x0600A593 RID: 42387 RVA: 0x0034A674 File Offset: 0x00348874
			public UpdatePowerInfo(KMonoBehaviour item, LocText power_label, LocText unit_label, Generator g, IEnergyConsumer c)
			{
				this.item = item;
				this.powerLabel = power_label;
				this.unitLabel = unit_label;
				this.generator = g;
				this.consumer = c;
			}

			// Token: 0x040092A6 RID: 37542
			public KMonoBehaviour item;

			// Token: 0x040092A7 RID: 37543
			public LocText powerLabel;

			// Token: 0x040092A8 RID: 37544
			public LocText unitLabel;

			// Token: 0x040092A9 RID: 37545
			public Generator generator;

			// Token: 0x040092AA RID: 37546
			public IEnergyConsumer consumer;
		}

		// Token: 0x020020F4 RID: 8436
		private struct UpdateBatteryInfo
		{
			// Token: 0x0600A594 RID: 42388 RVA: 0x0034A69B File Offset: 0x0034889B
			public UpdateBatteryInfo(Battery battery, BatteryUI ui)
			{
				this.battery = battery;
				this.ui = ui;
			}

			// Token: 0x040092AB RID: 37547
			public Battery battery;

			// Token: 0x040092AC RID: 37548
			public BatteryUI ui;
		}
	}

	// Token: 0x02001842 RID: 6210
	public class Radiation : OverlayModes.Mode
	{
		// Token: 0x06008DB2 RID: 36274 RVA: 0x00309C7A File Offset: 0x00307E7A
		public override HashedString ViewMode()
		{
			return OverlayModes.Radiation.ID;
		}

		// Token: 0x06008DB3 RID: 36275 RVA: 0x00309C81 File Offset: 0x00307E81
		public override string GetSoundName()
		{
			return "Radiation";
		}

		// Token: 0x06008DB4 RID: 36276 RVA: 0x00309C88 File Offset: 0x00307E88
		public override void Enable()
		{
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().TechFilterRadiationOn);
		}

		// Token: 0x06008DB5 RID: 36277 RVA: 0x00309C9F File Offset: 0x00307E9F
		public override void Disable()
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().TechFilterRadiationOn, STOP_MODE.ALLOWFADEOUT);
		}

		// Token: 0x04006FC9 RID: 28617
		public static readonly HashedString ID = "Radiation";
	}

	// Token: 0x02001843 RID: 6211
	public class SolidConveyor : OverlayModes.Mode
	{
		// Token: 0x06008DB8 RID: 36280 RVA: 0x00309CD0 File Offset: 0x00307ED0
		public override HashedString ViewMode()
		{
			return OverlayModes.SolidConveyor.ID;
		}

		// Token: 0x06008DB9 RID: 36281 RVA: 0x00309CD7 File Offset: 0x00307ED7
		public override string GetSoundName()
		{
			return "LiquidVent";
		}

		// Token: 0x06008DBA RID: 36282 RVA: 0x00309CE0 File Offset: 0x00307EE0
		public SolidConveyor()
		{
			this.targetLayer = LayerMask.NameToLayer("MaskedOverlay");
			this.cameraLayerMask = LayerMask.GetMask(new string[] { "MaskedOverlay", "MaskedOverlayBG" });
			this.selectionMask = this.cameraLayerMask;
		}

		// Token: 0x06008DBB RID: 36283 RVA: 0x00309D78 File Offset: 0x00307F78
		public override void Enable()
		{
			base.RegisterSaveLoadListeners();
			this.partition = OverlayModes.Mode.PopulatePartition<SaveLoadRoot>(this.targetIDs);
			Camera.main.cullingMask |= this.cameraLayerMask;
			SelectTool.Instance.SetLayerMask(this.selectionMask);
			GridCompositor.Instance.ToggleMinor(false);
			base.Enable();
		}

		// Token: 0x06008DBC RID: 36284 RVA: 0x00309DD4 File Offset: 0x00307FD4
		protected override void OnSaveLoadRootRegistered(SaveLoadRoot item)
		{
			Tag saveLoadTag = item.GetComponent<KPrefabID>().GetSaveLoadTag();
			if (this.targetIDs.Contains(saveLoadTag))
			{
				this.partition.Add(item);
			}
		}

		// Token: 0x06008DBD RID: 36285 RVA: 0x00309E08 File Offset: 0x00308008
		protected override void OnSaveLoadRootUnregistered(SaveLoadRoot item)
		{
			if (item == null || item.gameObject == null)
			{
				return;
			}
			if (this.layerTargets.Contains(item))
			{
				this.layerTargets.Remove(item);
			}
			this.partition.Remove(item);
		}

		// Token: 0x06008DBE RID: 36286 RVA: 0x00309E54 File Offset: 0x00308054
		public override void Disable()
		{
			OverlayModes.Mode.ResetDisplayValues<SaveLoadRoot>(this.layerTargets);
			Camera.main.cullingMask &= ~this.cameraLayerMask;
			SelectTool.Instance.ClearLayerMask();
			base.UnregisterSaveLoadListeners();
			this.partition.Clear();
			this.layerTargets.Clear();
			GridCompositor.Instance.ToggleMinor(false);
			base.Disable();
		}

		// Token: 0x06008DBF RID: 36287 RVA: 0x00309EBC File Offset: 0x003080BC
		public override void Update()
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			OverlayModes.Mode.RemoveOffscreenTargets<SaveLoadRoot>(this.layerTargets, vector2I, vector2I2, null);
			foreach (object obj in this.partition.GetAllIntersecting(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y)))
			{
				SaveLoadRoot saveLoadRoot = (SaveLoadRoot)obj;
				base.AddTargetIfVisible<SaveLoadRoot>(saveLoadRoot, vector2I, vector2I2, this.layerTargets, this.targetLayer, null, null);
			}
			GameObject gameObject = null;
			if (SelectTool.Instance != null && SelectTool.Instance.hover != null)
			{
				gameObject = SelectTool.Instance.hover.gameObject;
			}
			this.connectedNetworks.Clear();
			float num = 1f;
			if (gameObject != null)
			{
				SolidConduit component = gameObject.GetComponent<SolidConduit>();
				if (component != null)
				{
					int num2 = Grid.PosToCell(component);
					UtilityNetworkManager<FlowUtilityNetwork, SolidConduit> solidConduitSystem = Game.Instance.solidConduitSystem;
					this.visited.Clear();
					this.FindConnectedNetworks(num2, solidConduitSystem, this.connectedNetworks, this.visited);
					this.visited.Clear();
					num = OverlayModes.ModeUtil.GetHighlightScale();
				}
			}
			foreach (SaveLoadRoot saveLoadRoot2 in this.layerTargets)
			{
				if (!(saveLoadRoot2 == null))
				{
					Color32 color = this.tint_color;
					SolidConduit component2 = saveLoadRoot2.GetComponent<SolidConduit>();
					if (component2 != null)
					{
						if (this.connectedNetworks.Count > 0 && this.IsConnectedToNetworks(component2, this.connectedNetworks))
						{
							color.r = (byte)((float)color.r * num);
							color.g = (byte)((float)color.g * num);
							color.b = (byte)((float)color.b * num);
						}
						saveLoadRoot2.GetComponent<KBatchedAnimController>().TintColour = color;
					}
				}
			}
		}

		// Token: 0x06008DC0 RID: 36288 RVA: 0x0030A0E0 File Offset: 0x003082E0
		public bool IsConnectedToNetworks(SolidConduit conduit, ICollection<UtilityNetwork> networks)
		{
			UtilityNetwork network = conduit.GetNetwork();
			return networks.Contains(network);
		}

		// Token: 0x06008DC1 RID: 36289 RVA: 0x0030A0FC File Offset: 0x003082FC
		private void FindConnectedNetworks(int cell, IUtilityNetworkMgr mgr, ICollection<UtilityNetwork> networks, List<int> visited)
		{
			if (visited.Contains(cell))
			{
				return;
			}
			visited.Add(cell);
			UtilityNetwork networkForCell = mgr.GetNetworkForCell(cell);
			if (networkForCell != null)
			{
				networks.Add(networkForCell);
				UtilityConnections connections = mgr.GetConnections(cell, false);
				if ((connections & UtilityConnections.Right) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellRight(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Left) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellLeft(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Up) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellAbove(cell), mgr, networks, visited);
				}
				if ((connections & UtilityConnections.Down) != (UtilityConnections)0)
				{
					this.FindConnectedNetworks(Grid.CellBelow(cell), mgr, networks, visited);
				}
				object endpoint = mgr.GetEndpoint(cell);
				if (endpoint != null)
				{
					FlowUtilityNetwork.NetworkItem networkItem = endpoint as FlowUtilityNetwork.NetworkItem;
					if (networkItem != null)
					{
						GameObject gameObject = networkItem.GameObject;
						if (gameObject != null)
						{
							IBridgedNetworkItem component = gameObject.GetComponent<IBridgedNetworkItem>();
							if (component != null)
							{
								component.AddNetworks(networks);
							}
						}
					}
				}
			}
		}

		// Token: 0x04006FCA RID: 28618
		public static readonly HashedString ID = "SolidConveyor";

		// Token: 0x04006FCB RID: 28619
		private UniformGrid<SaveLoadRoot> partition;

		// Token: 0x04006FCC RID: 28620
		private HashSet<SaveLoadRoot> layerTargets = new HashSet<SaveLoadRoot>();

		// Token: 0x04006FCD RID: 28621
		private ICollection<Tag> targetIDs = OverlayScreen.SolidConveyorIDs;

		// Token: 0x04006FCE RID: 28622
		private Color32 tint_color = new Color32(201, 201, 201, 0);

		// Token: 0x04006FCF RID: 28623
		private HashSet<UtilityNetwork> connectedNetworks = new HashSet<UtilityNetwork>();

		// Token: 0x04006FD0 RID: 28624
		private List<int> visited = new List<int>();

		// Token: 0x04006FD1 RID: 28625
		private int targetLayer;

		// Token: 0x04006FD2 RID: 28626
		private int cameraLayerMask;

		// Token: 0x04006FD3 RID: 28627
		private int selectionMask;
	}

	// Token: 0x02001844 RID: 6212
	public class Sound : OverlayModes.Mode
	{
		// Token: 0x06008DC3 RID: 36291 RVA: 0x0030A1D6 File Offset: 0x003083D6
		public override HashedString ViewMode()
		{
			return OverlayModes.Sound.ID;
		}

		// Token: 0x06008DC4 RID: 36292 RVA: 0x0030A1DD File Offset: 0x003083DD
		public override string GetSoundName()
		{
			return "Sound";
		}

		// Token: 0x06008DC5 RID: 36293 RVA: 0x0030A1E4 File Offset: 0x003083E4
		public Sound()
		{
			OverlayModes.ColorHighlightCondition[] array = new OverlayModes.ColorHighlightCondition[1];
			array[0] = new OverlayModes.ColorHighlightCondition(delegate(KMonoBehaviour np)
			{
				Color black = Color.black;
				Color black2 = Color.black;
				float num = 0.8f;
				if (np != null)
				{
					int num2 = Grid.PosToCell(CameraController.Instance.baseCamera.ScreenToWorldPoint(KInputManager.GetMousePos()));
					if ((np as NoisePolluter).GetNoiseForCell(num2) < 36f)
					{
						num = 1f;
						black2 = new Color(0.4f, 0.4f, 0.4f);
					}
				}
				return Color.Lerp(black, black2, num);
			}, delegate(KMonoBehaviour np)
			{
				List<GameObject> highlightedObjects = SelectToolHoverTextCard.highlightedObjects;
				bool flag = false;
				for (int i = 0; i < highlightedObjects.Count; i++)
				{
					if (highlightedObjects[i] != null && highlightedObjects[i] == np.gameObject)
					{
						flag = true;
						break;
					}
				}
				return flag;
			});
			this.highlightConditions = array;
			base..ctor();
			this.targetLayer = LayerMask.NameToLayer("MaskedOverlay");
			this.cameraLayerMask = LayerMask.GetMask(new string[] { "MaskedOverlay", "MaskedOverlayBG" });
			List<Tag> prefabTagsWithComponent = Assets.GetPrefabTagsWithComponent<NoisePolluter>();
			this.targetIDs.UnionWith(prefabTagsWithComponent);
		}

		// Token: 0x06008DC6 RID: 36294 RVA: 0x0030A2A4 File Offset: 0x003084A4
		public override void Enable()
		{
			base.RegisterSaveLoadListeners();
			List<Tag> prefabTagsWithComponent = Assets.GetPrefabTagsWithComponent<NoisePolluter>();
			this.targetIDs.UnionWith(prefabTagsWithComponent);
			this.partition = OverlayModes.Mode.PopulatePartition<NoisePolluter>(this.targetIDs);
			Camera.main.cullingMask |= this.cameraLayerMask;
		}

		// Token: 0x06008DC7 RID: 36295 RVA: 0x0030A2F4 File Offset: 0x003084F4
		public override void Update()
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			OverlayModes.Mode.RemoveOffscreenTargets<NoisePolluter>(this.layerTargets, vector2I, vector2I2, null);
			foreach (object obj in this.partition.GetAllIntersecting(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y)))
			{
				NoisePolluter noisePolluter = (NoisePolluter)obj;
				base.AddTargetIfVisible<NoisePolluter>(noisePolluter, vector2I, vector2I2, this.layerTargets, this.targetLayer, null, null);
			}
			base.UpdateHighlightTypeOverlay<NoisePolluter>(vector2I, vector2I2, this.layerTargets, this.targetIDs, this.highlightConditions, OverlayModes.BringToFrontLayerSetting.Conditional, this.targetLayer);
		}

		// Token: 0x06008DC8 RID: 36296 RVA: 0x0030A3C4 File Offset: 0x003085C4
		protected override void OnSaveLoadRootRegistered(SaveLoadRoot item)
		{
			Tag saveLoadTag = item.GetComponent<KPrefabID>().GetSaveLoadTag();
			if (this.targetIDs.Contains(saveLoadTag))
			{
				NoisePolluter component = item.GetComponent<NoisePolluter>();
				this.partition.Add(component);
			}
		}

		// Token: 0x06008DC9 RID: 36297 RVA: 0x0030A400 File Offset: 0x00308600
		protected override void OnSaveLoadRootUnregistered(SaveLoadRoot item)
		{
			if (item == null || item.gameObject == null)
			{
				return;
			}
			NoisePolluter component = item.GetComponent<NoisePolluter>();
			if (this.layerTargets.Contains(component))
			{
				this.layerTargets.Remove(component);
			}
			this.partition.Remove(component);
		}

		// Token: 0x06008DCA RID: 36298 RVA: 0x0030A454 File Offset: 0x00308654
		public override void Disable()
		{
			base.DisableHighlightTypeOverlay<NoisePolluter>(this.layerTargets);
			Camera.main.cullingMask &= ~this.cameraLayerMask;
			base.UnregisterSaveLoadListeners();
			this.partition.Clear();
			this.layerTargets.Clear();
		}

		// Token: 0x04006FD4 RID: 28628
		public static readonly HashedString ID = "Sound";

		// Token: 0x04006FD5 RID: 28629
		private UniformGrid<NoisePolluter> partition;

		// Token: 0x04006FD6 RID: 28630
		private HashSet<NoisePolluter> layerTargets = new HashSet<NoisePolluter>();

		// Token: 0x04006FD7 RID: 28631
		private HashSet<Tag> targetIDs = new HashSet<Tag>();

		// Token: 0x04006FD8 RID: 28632
		private int targetLayer;

		// Token: 0x04006FD9 RID: 28633
		private int cameraLayerMask;

		// Token: 0x04006FDA RID: 28634
		private OverlayModes.ColorHighlightCondition[] highlightConditions;
	}

	// Token: 0x02001845 RID: 6213
	public class Suit : OverlayModes.Mode
	{
		// Token: 0x06008DCC RID: 36300 RVA: 0x0030A4B2 File Offset: 0x003086B2
		public override HashedString ViewMode()
		{
			return OverlayModes.Suit.ID;
		}

		// Token: 0x06008DCD RID: 36301 RVA: 0x0030A4B9 File Offset: 0x003086B9
		public override string GetSoundName()
		{
			return "SuitRequired";
		}

		// Token: 0x06008DCE RID: 36302 RVA: 0x0030A4C0 File Offset: 0x003086C0
		public Suit(Canvas ui_parent, GameObject overlay_prefab)
		{
			this.targetLayer = LayerMask.NameToLayer("MaskedOverlay");
			this.cameraLayerMask = LayerMask.GetMask(new string[] { "MaskedOverlay", "MaskedOverlayBG" });
			this.selectionMask = this.cameraLayerMask;
			this.targetIDs = OverlayScreen.SuitIDs;
			this.uiParent = ui_parent;
			this.overlayPrefab = overlay_prefab;
		}

		// Token: 0x06008DCF RID: 36303 RVA: 0x0030A540 File Offset: 0x00308740
		public override void Enable()
		{
			this.partition = new UniformGrid<SaveLoadRoot>(Grid.WidthInCells, Grid.HeightInCells, 8, 8);
			base.ProcessExistingSaveLoadRoots();
			base.RegisterSaveLoadListeners();
			Camera.main.cullingMask |= this.cameraLayerMask;
			SelectTool.Instance.SetLayerMask(this.selectionMask);
			GridCompositor.Instance.ToggleMinor(false);
			base.Enable();
		}

		// Token: 0x06008DD0 RID: 36304 RVA: 0x0030A5A8 File Offset: 0x003087A8
		public override void Disable()
		{
			base.UnregisterSaveLoadListeners();
			OverlayModes.Mode.ResetDisplayValues<SaveLoadRoot>(this.layerTargets);
			Camera.main.cullingMask &= ~this.cameraLayerMask;
			SelectTool.Instance.ClearLayerMask();
			this.partition.Clear();
			this.partition = null;
			this.layerTargets.Clear();
			for (int i = 0; i < this.uiList.Count; i++)
			{
				this.uiList[i].SetActive(false);
			}
			GridCompositor.Instance.ToggleMinor(false);
			base.Disable();
		}

		// Token: 0x06008DD1 RID: 36305 RVA: 0x0030A640 File Offset: 0x00308840
		protected override void OnSaveLoadRootRegistered(SaveLoadRoot item)
		{
			Tag saveLoadTag = item.GetComponent<KPrefabID>().GetSaveLoadTag();
			if (this.targetIDs.Contains(saveLoadTag))
			{
				this.partition.Add(item);
			}
		}

		// Token: 0x06008DD2 RID: 36306 RVA: 0x0030A674 File Offset: 0x00308874
		protected override void OnSaveLoadRootUnregistered(SaveLoadRoot item)
		{
			if (item == null || item.gameObject == null)
			{
				return;
			}
			if (this.layerTargets.Contains(item))
			{
				this.layerTargets.Remove(item);
			}
			this.partition.Remove(item);
		}

		// Token: 0x06008DD3 RID: 36307 RVA: 0x0030A6C0 File Offset: 0x003088C0
		private GameObject GetFreeUI()
		{
			GameObject gameObject;
			if (this.freeUiIdx >= this.uiList.Count)
			{
				gameObject = global::Util.KInstantiateUI(this.overlayPrefab, this.uiParent.transform.gameObject, false);
				this.uiList.Add(gameObject);
			}
			else
			{
				List<GameObject> list = this.uiList;
				int num = this.freeUiIdx;
				this.freeUiIdx = num + 1;
				gameObject = list[num];
			}
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
			return gameObject;
		}

		// Token: 0x06008DD4 RID: 36308 RVA: 0x0030A73C File Offset: 0x0030893C
		public override void Update()
		{
			this.freeUiIdx = 0;
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			OverlayModes.Mode.RemoveOffscreenTargets<SaveLoadRoot>(this.layerTargets, vector2I, vector2I2, null);
			foreach (object obj in this.partition.GetAllIntersecting(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y)))
			{
				SaveLoadRoot saveLoadRoot = (SaveLoadRoot)obj;
				base.AddTargetIfVisible<SaveLoadRoot>(saveLoadRoot, vector2I, vector2I2, this.layerTargets, this.targetLayer, null, null);
			}
			foreach (SaveLoadRoot saveLoadRoot2 in this.layerTargets)
			{
				if (!(saveLoadRoot2 == null))
				{
					saveLoadRoot2.GetComponent<KBatchedAnimController>().TintColour = Color.white;
					bool flag = false;
					if (saveLoadRoot2.GetComponent<KPrefabID>().HasTag(GameTags.Suit))
					{
						flag = true;
					}
					else
					{
						SuitLocker component = saveLoadRoot2.GetComponent<SuitLocker>();
						if (component != null)
						{
							flag = component.GetStoredOutfit() != null;
						}
					}
					if (flag)
					{
						this.GetFreeUI().GetComponent<RectTransform>().SetPosition(saveLoadRoot2.transform.GetPosition());
					}
				}
			}
			for (int i = this.freeUiIdx; i < this.uiList.Count; i++)
			{
				if (this.uiList[i].activeSelf)
				{
					this.uiList[i].SetActive(false);
				}
			}
		}

		// Token: 0x04006FDB RID: 28635
		public static readonly HashedString ID = "Suit";

		// Token: 0x04006FDC RID: 28636
		private UniformGrid<SaveLoadRoot> partition;

		// Token: 0x04006FDD RID: 28637
		private HashSet<SaveLoadRoot> layerTargets = new HashSet<SaveLoadRoot>();

		// Token: 0x04006FDE RID: 28638
		private ICollection<Tag> targetIDs;

		// Token: 0x04006FDF RID: 28639
		private List<GameObject> uiList = new List<GameObject>();

		// Token: 0x04006FE0 RID: 28640
		private int freeUiIdx;

		// Token: 0x04006FE1 RID: 28641
		private int targetLayer;

		// Token: 0x04006FE2 RID: 28642
		private int cameraLayerMask;

		// Token: 0x04006FE3 RID: 28643
		private int selectionMask;

		// Token: 0x04006FE4 RID: 28644
		private Canvas uiParent;

		// Token: 0x04006FE5 RID: 28645
		private GameObject overlayPrefab;
	}

	// Token: 0x02001846 RID: 6214
	public class Temperature : OverlayModes.Mode
	{
		// Token: 0x06008DD6 RID: 36310 RVA: 0x0030A909 File Offset: 0x00308B09
		public override HashedString ViewMode()
		{
			return OverlayModes.Temperature.ID;
		}

		// Token: 0x06008DD7 RID: 36311 RVA: 0x0030A910 File Offset: 0x00308B10
		public override string GetSoundName()
		{
			return "Temperature";
		}

		// Token: 0x06008DD8 RID: 36312 RVA: 0x0030A918 File Offset: 0x00308B18
		public Temperature()
		{
			this.legendFilters = this.CreateDefaultFilters();
		}

		// Token: 0x06008DD9 RID: 36313 RVA: 0x0030AE08 File Offset: 0x00309008
		public override void Enable()
		{
			base.Enable();
			int num = SimDebugView.Instance.temperatureThresholds.Length - 1;
			for (int i = 0; i < this.temperatureLegend.Count; i++)
			{
				this.temperatureLegend[i].colour = GlobalAssets.Instance.colorSet.GetColorByName(SimDebugView.Instance.temperatureThresholds[num - i].colorName);
				this.temperatureLegend[i].desc_arg = GameUtil.GetFormattedTemperature(SimDebugView.Instance.temperatureThresholds[num - i].value, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false);
			}
		}

		// Token: 0x06008DDA RID: 36314 RVA: 0x0030AEAD File Offset: 0x003090AD
		public override Dictionary<string, ToolParameterMenu.ToggleState> CreateDefaultFilters()
		{
			return new Dictionary<string, ToolParameterMenu.ToggleState>
			{
				{
					ToolParameterMenu.FILTERLAYERS.ABSOLUTETEMPERATURE,
					ToolParameterMenu.ToggleState.On
				},
				{
					ToolParameterMenu.FILTERLAYERS.HEATFLOW,
					ToolParameterMenu.ToggleState.Off
				},
				{
					ToolParameterMenu.FILTERLAYERS.STATECHANGE,
					ToolParameterMenu.ToggleState.Off
				}
			};
		}

		// Token: 0x06008DDB RID: 36315 RVA: 0x0030AED8 File Offset: 0x003090D8
		public override List<LegendEntry> GetCustomLegendData()
		{
			switch (Game.Instance.temperatureOverlayMode)
			{
			case Game.TemperatureOverlayModes.AbsoluteTemperature:
				return this.temperatureLegend;
			case Game.TemperatureOverlayModes.AdaptiveTemperature:
				return this.expandedTemperatureLegend;
			case Game.TemperatureOverlayModes.HeatFlow:
				return this.heatFlowLegend;
			case Game.TemperatureOverlayModes.StateChange:
				return this.stateChangeLegend;
			default:
				return this.temperatureLegend;
			}
		}

		// Token: 0x06008DDC RID: 36316 RVA: 0x0030AF2C File Offset: 0x0030912C
		public override void OnFiltersChanged()
		{
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.HEATFLOW, this.legendFilters))
			{
				Game.Instance.temperatureOverlayMode = Game.TemperatureOverlayModes.HeatFlow;
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.ABSOLUTETEMPERATURE, this.legendFilters))
			{
				Game.Instance.temperatureOverlayMode = Game.TemperatureOverlayModes.AbsoluteTemperature;
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.ADAPTIVETEMPERATURE, this.legendFilters))
			{
				Game.Instance.temperatureOverlayMode = Game.TemperatureOverlayModes.AdaptiveTemperature;
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.STATECHANGE, this.legendFilters))
			{
				Game.Instance.temperatureOverlayMode = Game.TemperatureOverlayModes.StateChange;
			}
			switch (Game.Instance.temperatureOverlayMode)
			{
			case Game.TemperatureOverlayModes.AbsoluteTemperature:
				Infrared.Instance.SetMode(Infrared.Mode.Infrared);
				CameraController.Instance.ToggleColouredOverlayView(true);
				return;
			case Game.TemperatureOverlayModes.AdaptiveTemperature:
				Infrared.Instance.SetMode(Infrared.Mode.Infrared);
				CameraController.Instance.ToggleColouredOverlayView(true);
				return;
			case Game.TemperatureOverlayModes.HeatFlow:
				Infrared.Instance.SetMode(Infrared.Mode.Disabled);
				CameraController.Instance.ToggleColouredOverlayView(false);
				return;
			case Game.TemperatureOverlayModes.StateChange:
				Infrared.Instance.SetMode(Infrared.Mode.Disabled);
				CameraController.Instance.ToggleColouredOverlayView(false);
				return;
			default:
				return;
			}
		}

		// Token: 0x06008DDD RID: 36317 RVA: 0x0030B02E File Offset: 0x0030922E
		public override void Disable()
		{
			Infrared.Instance.SetMode(Infrared.Mode.Disabled);
			CameraController.Instance.ToggleColouredOverlayView(false);
			base.Disable();
		}

		// Token: 0x04006FE6 RID: 28646
		public static readonly HashedString ID = "Temperature";

		// Token: 0x04006FE7 RID: 28647
		public List<LegendEntry> temperatureLegend = new List<LegendEntry>
		{
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.MAXHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.8901961f, 0.13725491f, 0.12941177f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.EXTREMEHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.9843137f, 0.3254902f, 0.3137255f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.VERYHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(1f, 0.6627451f, 0.14117648f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.HOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.9372549f, 1f, 0f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.TEMPERATE, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.23137255f, 0.99607843f, 0.2901961f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.COLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.12156863f, 0.6313726f, 1f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.VERYCOLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.16862746f, 0.79607844f, 1f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.EXTREMECOLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.5019608f, 0.99607843f, 0.9411765f), null, null, true)
		};

		// Token: 0x04006FE8 RID: 28648
		public List<LegendEntry> heatFlowLegend = new List<LegendEntry>
		{
			new LegendEntry(UI.OVERLAYS.HEATFLOW.HEATING, UI.OVERLAYS.HEATFLOW.TOOLTIPS.HEATING, new Color(0.9098039f, 0.25882354f, 0.14901961f), null, null, true),
			new LegendEntry(UI.OVERLAYS.HEATFLOW.NEUTRAL, UI.OVERLAYS.HEATFLOW.TOOLTIPS.NEUTRAL, new Color(0.30980393f, 0.30980393f, 0.30980393f), null, null, true),
			new LegendEntry(UI.OVERLAYS.HEATFLOW.COOLING, UI.OVERLAYS.HEATFLOW.TOOLTIPS.COOLING, new Color(0.2509804f, 0.6313726f, 0.90588236f), null, null, true)
		};

		// Token: 0x04006FE9 RID: 28649
		public List<LegendEntry> expandedTemperatureLegend = new List<LegendEntry>
		{
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.MAXHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.8901961f, 0.13725491f, 0.12941177f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.EXTREMEHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.9843137f, 0.3254902f, 0.3137255f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.VERYHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(1f, 0.6627451f, 0.14117648f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.HOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.9372549f, 1f, 0f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.TEMPERATE, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.23137255f, 0.99607843f, 0.2901961f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.COLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.12156863f, 0.6313726f, 1f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.VERYCOLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.16862746f, 0.79607844f, 1f), null, null, true),
			new LegendEntry(UI.OVERLAYS.TEMPERATURE.EXTREMECOLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0.5019608f, 0.99607843f, 0.9411765f), null, null, true)
		};

		// Token: 0x04006FEA RID: 28650
		public List<LegendEntry> stateChangeLegend = new List<LegendEntry>
		{
			new LegendEntry(UI.OVERLAYS.STATECHANGE.HIGHPOINT, UI.OVERLAYS.STATECHANGE.TOOLTIPS.HIGHPOINT, new Color(0.8901961f, 0.13725491f, 0.12941177f), null, null, true),
			new LegendEntry(UI.OVERLAYS.STATECHANGE.STABLE, UI.OVERLAYS.STATECHANGE.TOOLTIPS.STABLE, new Color(0.23137255f, 0.99607843f, 0.2901961f), null, null, true),
			new LegendEntry(UI.OVERLAYS.STATECHANGE.LOWPOINT, UI.OVERLAYS.STATECHANGE.TOOLTIPS.LOWPOINT, new Color(0.5019608f, 0.99607843f, 0.9411765f), null, null, true)
		};
	}

	// Token: 0x02001847 RID: 6215
	public class TileMode : OverlayModes.Mode
	{
		// Token: 0x06008DDF RID: 36319 RVA: 0x0030B05D File Offset: 0x0030925D
		public override HashedString ViewMode()
		{
			return OverlayModes.TileMode.ID;
		}

		// Token: 0x06008DE0 RID: 36320 RVA: 0x0030B064 File Offset: 0x00309264
		public override string GetSoundName()
		{
			return "SuitRequired";
		}

		// Token: 0x06008DE1 RID: 36321 RVA: 0x0030B06C File Offset: 0x0030926C
		public TileMode()
		{
			OverlayModes.ColorHighlightCondition[] array = new OverlayModes.ColorHighlightCondition[1];
			array[0] = new OverlayModes.ColorHighlightCondition(delegate(KMonoBehaviour primary_element)
			{
				Color color = Color.black;
				if (primary_element != null)
				{
					color = (primary_element as PrimaryElement).Element.substance.uiColour;
				}
				return color;
			}, (KMonoBehaviour primary_element) => primary_element.gameObject.GetComponent<KBatchedAnimController>().IsVisible());
			this.highlightConditions = array;
			base..ctor();
			this.targetLayer = LayerMask.NameToLayer("MaskedOverlay");
			this.cameraLayerMask = LayerMask.GetMask(new string[] { "MaskedOverlay", "MaskedOverlayBG" });
			this.legendFilters = this.CreateDefaultFilters();
		}

		// Token: 0x06008DE2 RID: 36322 RVA: 0x0030B124 File Offset: 0x00309324
		public override void Enable()
		{
			base.Enable();
			List<Tag> prefabTagsWithComponent = Assets.GetPrefabTagsWithComponent<PrimaryElement>();
			this.targetIDs.UnionWith(prefabTagsWithComponent);
			Camera.main.cullingMask |= this.cameraLayerMask;
			int defaultLayerMask = SelectTool.Instance.GetDefaultLayerMask();
			int mask = LayerMask.GetMask(new string[] { "MaskedOverlay" });
			SelectTool.Instance.SetLayerMask(defaultLayerMask | mask);
		}

		// Token: 0x06008DE3 RID: 36323 RVA: 0x0030B18C File Offset: 0x0030938C
		public override void Update()
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			Grid.GetVisibleExtents(out vector2I, out vector2I2);
			OverlayModes.Mode.RemoveOffscreenTargets<PrimaryElement>(this.layerTargets, vector2I, vector2I2, null);
			int num = vector2I2.y - vector2I.y;
			int num2 = vector2I2.x - vector2I.x;
			Extents extents = new Extents(vector2I.x, vector2I.y, num2, num);
			List<ScenePartitionerEntry> list = new List<ScenePartitionerEntry>();
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.pickupablesLayer, list);
			foreach (ScenePartitionerEntry scenePartitionerEntry in list)
			{
				PrimaryElement component = ((Pickupable)scenePartitionerEntry.obj).gameObject.GetComponent<PrimaryElement>();
				if (component != null)
				{
					this.TryAddObject(component, vector2I, vector2I2);
				}
			}
			list.Clear();
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.completeBuildings, list);
			foreach (ScenePartitionerEntry scenePartitionerEntry2 in list)
			{
				BuildingComplete buildingComplete = (BuildingComplete)scenePartitionerEntry2.obj;
				PrimaryElement component2 = buildingComplete.gameObject.GetComponent<PrimaryElement>();
				if (component2 != null && buildingComplete.gameObject.layer == 0)
				{
					this.TryAddObject(component2, vector2I, vector2I2);
				}
			}
			base.UpdateHighlightTypeOverlay<PrimaryElement>(vector2I, vector2I2, this.layerTargets, this.targetIDs, this.highlightConditions, OverlayModes.BringToFrontLayerSetting.Conditional, this.targetLayer);
		}

		// Token: 0x06008DE4 RID: 36324 RVA: 0x0030B318 File Offset: 0x00309518
		private void TryAddObject(PrimaryElement pe, Vector2I min, Vector2I max)
		{
			Element element = pe.Element;
			foreach (Tag tag in Game.Instance.tileOverlayFilters)
			{
				if (element.HasTag(tag))
				{
					base.AddTargetIfVisible<PrimaryElement>(pe, min, max, this.layerTargets, this.targetLayer, null, null);
					break;
				}
			}
		}

		// Token: 0x06008DE5 RID: 36325 RVA: 0x0030B394 File Offset: 0x00309594
		public override void Disable()
		{
			base.Disable();
			base.DisableHighlightTypeOverlay<PrimaryElement>(this.layerTargets);
			Camera.main.cullingMask &= ~this.cameraLayerMask;
			this.layerTargets.Clear();
			SelectTool.Instance.ClearLayerMask();
		}

		// Token: 0x06008DE6 RID: 36326 RVA: 0x0030B3E0 File Offset: 0x003095E0
		public override Dictionary<string, ToolParameterMenu.ToggleState> CreateDefaultFilters()
		{
			return new Dictionary<string, ToolParameterMenu.ToggleState>
			{
				{
					ToolParameterMenu.FILTERLAYERS.ALL,
					ToolParameterMenu.ToggleState.On
				},
				{
					ToolParameterMenu.FILTERLAYERS.METAL,
					ToolParameterMenu.ToggleState.Off
				},
				{
					ToolParameterMenu.FILTERLAYERS.BUILDABLE,
					ToolParameterMenu.ToggleState.Off
				},
				{
					ToolParameterMenu.FILTERLAYERS.FILTER,
					ToolParameterMenu.ToggleState.Off
				},
				{
					ToolParameterMenu.FILTERLAYERS.CONSUMABLEORE,
					ToolParameterMenu.ToggleState.Off
				},
				{
					ToolParameterMenu.FILTERLAYERS.ORGANICS,
					ToolParameterMenu.ToggleState.Off
				},
				{
					ToolParameterMenu.FILTERLAYERS.FARMABLE,
					ToolParameterMenu.ToggleState.Off
				},
				{
					ToolParameterMenu.FILTERLAYERS.GAS,
					ToolParameterMenu.ToggleState.Off
				},
				{
					ToolParameterMenu.FILTERLAYERS.LIQUID,
					ToolParameterMenu.ToggleState.Off
				}
			};
		}

		// Token: 0x06008DE7 RID: 36327 RVA: 0x0030B460 File Offset: 0x00309660
		public override void OnFiltersChanged()
		{
			Game.Instance.tileOverlayFilters.Clear();
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.METAL, this.legendFilters))
			{
				Game.Instance.tileOverlayFilters.Add(GameTags.Metal);
				Game.Instance.tileOverlayFilters.Add(GameTags.RefinedMetal);
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.BUILDABLE, this.legendFilters))
			{
				Game.Instance.tileOverlayFilters.Add(GameTags.BuildableRaw);
				Game.Instance.tileOverlayFilters.Add(GameTags.BuildableProcessed);
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.FILTER, this.legendFilters))
			{
				Game.Instance.tileOverlayFilters.Add(GameTags.Filter);
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.LIQUIFIABLE, this.legendFilters))
			{
				Game.Instance.tileOverlayFilters.Add(GameTags.Liquifiable);
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.LIQUID, this.legendFilters))
			{
				Game.Instance.tileOverlayFilters.Add(GameTags.Liquid);
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.CONSUMABLEORE, this.legendFilters))
			{
				Game.Instance.tileOverlayFilters.Add(GameTags.ConsumableOre);
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.ORGANICS, this.legendFilters))
			{
				Game.Instance.tileOverlayFilters.Add(GameTags.Organics);
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.FARMABLE, this.legendFilters))
			{
				Game.Instance.tileOverlayFilters.Add(GameTags.Farmable);
				Game.Instance.tileOverlayFilters.Add(GameTags.Agriculture);
			}
			if (base.InFilter(ToolParameterMenu.FILTERLAYERS.GAS, this.legendFilters))
			{
				Game.Instance.tileOverlayFilters.Add(GameTags.Breathable);
				Game.Instance.tileOverlayFilters.Add(GameTags.Unbreathable);
			}
			base.DisableHighlightTypeOverlay<PrimaryElement>(this.layerTargets);
			this.layerTargets.Clear();
			Game.Instance.ForceOverlayUpdate(false);
		}

		// Token: 0x04006FEB RID: 28651
		public static readonly HashedString ID = "TileMode";

		// Token: 0x04006FEC RID: 28652
		private HashSet<PrimaryElement> layerTargets = new HashSet<PrimaryElement>();

		// Token: 0x04006FED RID: 28653
		private HashSet<Tag> targetIDs = new HashSet<Tag>();

		// Token: 0x04006FEE RID: 28654
		private int targetLayer;

		// Token: 0x04006FEF RID: 28655
		private int cameraLayerMask;

		// Token: 0x04006FF0 RID: 28656
		private OverlayModes.ColorHighlightCondition[] highlightConditions;
	}
}
