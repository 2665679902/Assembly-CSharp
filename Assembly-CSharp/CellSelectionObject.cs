using System;
using System.Collections.Generic;
using ProcGen;
using UnityEngine;

// Token: 0x0200067D RID: 1661
[AddComponentMenu("KMonoBehaviour/scripts/CellSelectionObject")]
public class CellSelectionObject : KMonoBehaviour
{
	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000EA80F File Offset: 0x000E8A0F
	public int SelectedCell
	{
		get
		{
			return this.selectedCell;
		}
	}

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000EA817 File Offset: 0x000E8A17
	public float FlowRate
	{
		get
		{
			return Grid.AccumulatedFlow[this.selectedCell] / 3f;
		}
	}

	// Token: 0x06002CC0 RID: 11456 RVA: 0x000EA830 File Offset: 0x000E8A30
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.mCollider = base.GetComponent<KBoxCollider2D>();
		this.mCollider.size = new Vector2(1.1f, 1.1f);
		this.mSelectable = base.GetComponent<KSelectable>();
		this.SelectedDisplaySprite.transform.localScale = Vector3.one * 0.390625f;
		this.SelectedDisplaySprite.GetComponent<SpriteRenderer>().sprite = this.Sprite_Hover;
		base.Subscribe(Game.Instance.gameObject, 493375141, new Action<object>(this.ForceRefreshUserMenu));
		this.overlayFilterMap.Add(OverlayModes.Oxygen.ID, () => Grid.Element[this.mouseCell].IsGas);
		this.overlayFilterMap.Add(OverlayModes.GasConduits.ID, () => Grid.Element[this.mouseCell].IsGas);
		this.overlayFilterMap.Add(OverlayModes.LiquidConduits.ID, () => Grid.Element[this.mouseCell].IsLiquid);
		if (CellSelectionObject.selectionObjectA == null)
		{
			CellSelectionObject.selectionObjectA = this;
			return;
		}
		if (CellSelectionObject.selectionObjectB == null)
		{
			CellSelectionObject.selectionObjectB = this;
			return;
		}
		global::Debug.LogError("CellSelectionObjects not properly cleaned up.");
	}

	// Token: 0x06002CC1 RID: 11457 RVA: 0x000EA952 File Offset: 0x000E8B52
	protected override void OnCleanUp()
	{
		CellSelectionObject.selectionObjectA = null;
		CellSelectionObject.selectionObjectB = null;
		base.OnCleanUp();
	}

	// Token: 0x06002CC2 RID: 11458 RVA: 0x000EA966 File Offset: 0x000E8B66
	public static bool IsSelectionObject(GameObject testObject)
	{
		return testObject == CellSelectionObject.selectionObjectA.gameObject || testObject == CellSelectionObject.selectionObjectB.gameObject;
	}

	// Token: 0x06002CC3 RID: 11459 RVA: 0x000EA98C File Offset: 0x000E8B8C
	private void OnApplicationFocus(bool focusStatus)
	{
		this.isAppFocused = focusStatus;
	}

	// Token: 0x06002CC4 RID: 11460 RVA: 0x000EA998 File Offset: 0x000E8B98
	private void Update()
	{
		if (!this.isAppFocused || SelectTool.Instance == null)
		{
			return;
		}
		if (Game.Instance == null || !Game.Instance.GameStarted())
		{
			return;
		}
		this.SelectedDisplaySprite.SetActive(PlayerController.Instance.IsUsingDefaultTool() && !DebugHandler.HideUI);
		if (SelectTool.Instance.selected != this.mSelectable)
		{
			this.mouseCell = Grid.PosToCell(CameraController.Instance.baseCamera.ScreenToWorldPoint(KInputManager.GetMousePos()));
			if (Grid.IsValidCell(this.mouseCell) && Grid.IsVisible(this.mouseCell))
			{
				bool flag = true;
				foreach (KeyValuePair<HashedString, Func<bool>> keyValuePair in this.overlayFilterMap)
				{
					if (keyValuePair.Value == null)
					{
						global::Debug.LogWarning("Filter value is null");
					}
					else if (OverlayScreen.Instance == null)
					{
						global::Debug.LogWarning("Overlay screen Instance is null");
					}
					else if (OverlayScreen.Instance.GetMode() == keyValuePair.Key)
					{
						flag = false;
						if (base.gameObject.layer != LayerMask.NameToLayer("MaskedOverlay"))
						{
							base.gameObject.layer = LayerMask.NameToLayer("MaskedOverlay");
						}
						if (!keyValuePair.Value())
						{
							this.SelectedDisplaySprite.SetActive(false);
							return;
						}
						break;
					}
				}
				if (flag && base.gameObject.layer != LayerMask.NameToLayer("Default"))
				{
					base.gameObject.layer = LayerMask.NameToLayer("Default");
				}
				Vector3 vector = Grid.CellToPos(this.mouseCell, 0f, 0f, 0f) + this.offset;
				vector.z = this.zDepth;
				base.transform.SetPosition(vector);
				this.mSelectable.SetName(Grid.Element[this.mouseCell].name);
			}
			if (SelectTool.Instance.hover != this.mSelectable)
			{
				this.SelectedDisplaySprite.SetActive(false);
			}
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer >= 0.5f)
		{
			this.updateTimer = 0f;
			if (SelectTool.Instance.selected == this.mSelectable)
			{
				this.UpdateValues();
			}
		}
	}

	// Token: 0x06002CC5 RID: 11461 RVA: 0x000EAC20 File Offset: 0x000E8E20
	public void UpdateValues()
	{
		if (!Grid.IsValidCell(this.selectedCell))
		{
			return;
		}
		this.Mass = Grid.Mass[this.selectedCell];
		this.element = Grid.Element[this.selectedCell];
		this.ElementName = this.element.name;
		this.state = this.element.state;
		this.tags = this.element.GetMaterialCategoryTag();
		this.temperature = Grid.Temperature[this.selectedCell];
		this.diseaseIdx = Grid.DiseaseIdx[this.selectedCell];
		this.diseaseCount = Grid.DiseaseCount[this.selectedCell];
		this.mSelectable.SetName(Grid.Element[this.selectedCell].name);
		DetailsScreen.Instance.Trigger(-1514841199, null);
		this.UpdateStatusItem();
		int num = Grid.CellAbove(this.selectedCell);
		bool flag = this.element.IsLiquid && Grid.IsValidCell(num) && (Grid.Element[num].IsGas || Grid.Element[num].IsVacuum);
		if (this.element.sublimateId != (SimHashes)0 && (this.element.IsSolid || flag))
		{
			this.mSelectable.AddStatusItem(Db.Get().MiscStatusItems.SublimationEmitting, this);
			bool flag2;
			bool flag3;
			GameUtil.IsEmissionBlocked(this.selectedCell, out flag2, out flag3);
			if (flag2)
			{
				this.mSelectable.AddStatusItem(Db.Get().MiscStatusItems.SublimationBlocked, this);
				this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.SublimationOverpressure, false);
			}
			else if (flag3)
			{
				this.mSelectable.AddStatusItem(Db.Get().MiscStatusItems.SublimationOverpressure, this);
				this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.SublimationBlocked, false);
			}
			else
			{
				this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.SublimationOverpressure, false);
				this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.SublimationBlocked, false);
			}
		}
		else
		{
			this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.SublimationEmitting, false);
			this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.SublimationBlocked, false);
			this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.SublimationOverpressure, false);
		}
		if (Game.Instance.GetComponent<EntombedItemVisualizer>().IsEntombedItem(this.selectedCell))
		{
			this.mSelectable.AddStatusItem(Db.Get().MiscStatusItems.BuriedItem, this);
		}
		else
		{
			this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.BuriedItem, true);
		}
		bool flag4 = CellSelectionObject.IsExposedToSpace(this.selectedCell);
		this.mSelectable.ToggleStatusItem(Db.Get().MiscStatusItems.Space, flag4, null);
	}

	// Token: 0x06002CC6 RID: 11462 RVA: 0x000EAF22 File Offset: 0x000E9122
	public static bool IsExposedToSpace(int cell)
	{
		return Game.Instance.world.zoneRenderData.GetSubWorldZoneType(cell) == SubWorld.ZoneType.Space && Grid.Objects[cell, 2] == null;
	}

	// Token: 0x06002CC7 RID: 11463 RVA: 0x000EAF50 File Offset: 0x000E9150
	private void UpdateStatusItem()
	{
		if (this.element.id == SimHashes.Vacuum || this.element.id == SimHashes.Void)
		{
			this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.ElementalCategory, true);
			this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.ElementalTemperature, true);
			this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.ElementalMass, true);
			this.mSelectable.RemoveStatusItem(Db.Get().MiscStatusItems.ElementalDisease, true);
			return;
		}
		if (!this.mSelectable.HasStatusItem(Db.Get().MiscStatusItems.ElementalCategory))
		{
			Func<Element> func = () => this.element;
			this.mSelectable.AddStatusItem(Db.Get().MiscStatusItems.ElementalCategory, func);
		}
		if (!this.mSelectable.HasStatusItem(Db.Get().MiscStatusItems.ElementalTemperature))
		{
			this.mSelectable.AddStatusItem(Db.Get().MiscStatusItems.ElementalTemperature, this);
		}
		if (!this.mSelectable.HasStatusItem(Db.Get().MiscStatusItems.ElementalMass))
		{
			this.mSelectable.AddStatusItem(Db.Get().MiscStatusItems.ElementalMass, this);
		}
		if (!this.mSelectable.HasStatusItem(Db.Get().MiscStatusItems.ElementalDisease))
		{
			this.mSelectable.AddStatusItem(Db.Get().MiscStatusItems.ElementalDisease, this);
		}
	}

	// Token: 0x06002CC8 RID: 11464 RVA: 0x000EB0E0 File Offset: 0x000E92E0
	public void OnObjectSelected(object o)
	{
		this.SelectedDisplaySprite.GetComponent<SpriteRenderer>().sprite = this.Sprite_Hover;
		this.UpdateStatusItem();
		if (SelectTool.Instance.selected == this.mSelectable)
		{
			this.selectedCell = Grid.PosToCell(base.gameObject);
			this.UpdateValues();
			Vector3 vector = Grid.CellToPos(this.selectedCell, 0f, 0f, 0f) + this.offset;
			vector.z = this.zDepthSelected;
			base.transform.SetPosition(vector);
			this.SelectedDisplaySprite.GetComponent<SpriteRenderer>().sprite = this.Sprite_Selected;
		}
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x000EB18D File Offset: 0x000E938D
	public string MassString()
	{
		return string.Format("{0:0.00}", this.Mass);
	}

	// Token: 0x06002CCA RID: 11466 RVA: 0x000EB1A4 File Offset: 0x000E93A4
	private void ForceRefreshUserMenu(object data)
	{
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x04001AA7 RID: 6823
	private static CellSelectionObject selectionObjectA;

	// Token: 0x04001AA8 RID: 6824
	private static CellSelectionObject selectionObjectB;

	// Token: 0x04001AA9 RID: 6825
	[HideInInspector]
	public CellSelectionObject alternateSelectionObject;

	// Token: 0x04001AAA RID: 6826
	private float zDepth = Grid.GetLayerZ(Grid.SceneLayer.WorldSelection) - 0.5f;

	// Token: 0x04001AAB RID: 6827
	private float zDepthSelected = Grid.GetLayerZ(Grid.SceneLayer.WorldSelection);

	// Token: 0x04001AAC RID: 6828
	private KBoxCollider2D mCollider;

	// Token: 0x04001AAD RID: 6829
	private KSelectable mSelectable;

	// Token: 0x04001AAE RID: 6830
	private Vector3 offset = new Vector3(0.5f, 0.5f, 0f);

	// Token: 0x04001AAF RID: 6831
	public GameObject SelectedDisplaySprite;

	// Token: 0x04001AB0 RID: 6832
	public Sprite Sprite_Selected;

	// Token: 0x04001AB1 RID: 6833
	public Sprite Sprite_Hover;

	// Token: 0x04001AB2 RID: 6834
	public int mouseCell;

	// Token: 0x04001AB3 RID: 6835
	private int selectedCell;

	// Token: 0x04001AB4 RID: 6836
	public string ElementName;

	// Token: 0x04001AB5 RID: 6837
	public Element element;

	// Token: 0x04001AB6 RID: 6838
	public Element.State state;

	// Token: 0x04001AB7 RID: 6839
	public float Mass;

	// Token: 0x04001AB8 RID: 6840
	public float temperature;

	// Token: 0x04001AB9 RID: 6841
	public Tag tags;

	// Token: 0x04001ABA RID: 6842
	public byte diseaseIdx;

	// Token: 0x04001ABB RID: 6843
	public int diseaseCount;

	// Token: 0x04001ABC RID: 6844
	private float updateTimer;

	// Token: 0x04001ABD RID: 6845
	private Dictionary<HashedString, Func<bool>> overlayFilterMap = new Dictionary<HashedString, Func<bool>>();

	// Token: 0x04001ABE RID: 6846
	private bool isAppFocused = true;
}
