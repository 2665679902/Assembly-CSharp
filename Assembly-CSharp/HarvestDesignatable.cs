using System;
using KSerialization;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007A5 RID: 1957
[AddComponentMenu("KMonoBehaviour/scripts/HarvestDesignatable")]
public class HarvestDesignatable : KMonoBehaviour
{
	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x06003734 RID: 14132 RVA: 0x0013338C File Offset: 0x0013158C
	public bool InPlanterBox
	{
		get
		{
			return this.isInPlanterBox;
		}
	}

	// Token: 0x17000409 RID: 1033
	// (get) Token: 0x06003735 RID: 14133 RVA: 0x00133394 File Offset: 0x00131594
	// (set) Token: 0x06003736 RID: 14134 RVA: 0x0013339C File Offset: 0x0013159C
	public bool MarkedForHarvest
	{
		get
		{
			return this.isMarkedForHarvest;
		}
		set
		{
			this.isMarkedForHarvest = value;
		}
	}

	// Token: 0x1700040A RID: 1034
	// (get) Token: 0x06003737 RID: 14135 RVA: 0x001333A5 File Offset: 0x001315A5
	public bool HarvestWhenReady
	{
		get
		{
			return this.harvestWhenReady;
		}
	}

	// Token: 0x06003738 RID: 14136 RVA: 0x001333AD File Offset: 0x001315AD
	protected HarvestDesignatable()
	{
		this.onEnableOverlayDelegate = new Action<object>(this.OnEnableOverlay);
	}

	// Token: 0x06003739 RID: 14137 RVA: 0x001333D5 File Offset: 0x001315D5
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<HarvestDesignatable>(1309017699, HarvestDesignatable.SetInPlanterBoxTrueDelegate);
	}

	// Token: 0x0600373A RID: 14138 RVA: 0x001333F0 File Offset: 0x001315F0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.isMarkedForHarvest)
		{
			this.MarkForHarvest();
		}
		Components.HarvestDesignatables.Add(this);
		base.Subscribe<HarvestDesignatable>(493375141, HarvestDesignatable.OnRefreshUserMenuDelegate);
		base.Subscribe<HarvestDesignatable>(2127324410, HarvestDesignatable.OnCancelDelegate);
		Game.Instance.Subscribe(1248612973, this.onEnableOverlayDelegate);
		Game.Instance.Subscribe(1798162660, this.onEnableOverlayDelegate);
		Game.Instance.Subscribe(2015652040, new Action<object>(this.OnDisableOverlay));
		this.area = base.GetComponent<OccupyArea>();
	}

	// Token: 0x0600373B RID: 14139 RVA: 0x00133494 File Offset: 0x00131694
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.HarvestDesignatables.Remove(this);
		this.DestroyOverlayIcon();
		Game.Instance.Unsubscribe(1248612973, this.onEnableOverlayDelegate);
		Game.Instance.Unsubscribe(2015652040, new Action<object>(this.OnDisableOverlay));
		Game.Instance.Unsubscribe(1798162660, this.onEnableOverlayDelegate);
	}

	// Token: 0x0600373C RID: 14140 RVA: 0x001334FD File Offset: 0x001316FD
	private void DestroyOverlayIcon()
	{
		if (this.HarvestWhenReadyOverlayIcon != null)
		{
			UnityEngine.Object.Destroy(this.HarvestWhenReadyOverlayIcon.gameObject);
			this.HarvestWhenReadyOverlayIcon = null;
		}
	}

	// Token: 0x0600373D RID: 14141 RVA: 0x00133524 File Offset: 0x00131724
	private void CreateOverlayIcon()
	{
		if (this.HarvestWhenReadyOverlayIcon != null)
		{
			return;
		}
		if (base.GetComponent<AttackableBase>() == null)
		{
			this.HarvestWhenReadyOverlayIcon = Util.KInstantiate(Assets.UIPrefabs.HarvestWhenReadyOverlayIcon, GameScreenManager.Instance.worldSpaceCanvas, null).GetComponent<RectTransform>();
			Extents extents = base.GetComponent<OccupyArea>().GetExtents();
			Vector3 vector;
			if (base.GetComponent<KPrefabID>().HasTag(GameTags.Hanging))
			{
				vector = new Vector3((float)(extents.x + extents.width / 2) + 0.5f, (float)(extents.y + extents.height));
			}
			else
			{
				vector = new Vector3((float)(extents.x + extents.width / 2) + 0.5f, (float)extents.y);
			}
			this.HarvestWhenReadyOverlayIcon.transform.SetPosition(vector);
			this.RefreshOverlayIcon(null);
		}
	}

	// Token: 0x0600373E RID: 14142 RVA: 0x001335FE File Offset: 0x001317FE
	private void OnDisableOverlay(object data)
	{
		this.DestroyOverlayIcon();
	}

	// Token: 0x0600373F RID: 14143 RVA: 0x00133606 File Offset: 0x00131806
	private void OnEnableOverlay(object data)
	{
		if ((HashedString)data == OverlayModes.Harvest.ID)
		{
			this.CreateOverlayIcon();
			return;
		}
		this.DestroyOverlayIcon();
	}

	// Token: 0x06003740 RID: 14144 RVA: 0x00133628 File Offset: 0x00131828
	private void RefreshOverlayIcon(object data = null)
	{
		if (this.HarvestWhenReadyOverlayIcon != null)
		{
			if (Grid.IsVisible(Grid.PosToCell(base.gameObject)) || (CameraController.Instance != null && CameraController.Instance.FreeCameraEnabled))
			{
				if (!this.HarvestWhenReadyOverlayIcon.gameObject.activeSelf)
				{
					this.HarvestWhenReadyOverlayIcon.gameObject.SetActive(true);
				}
			}
			else if (this.HarvestWhenReadyOverlayIcon.gameObject.activeSelf)
			{
				this.HarvestWhenReadyOverlayIcon.gameObject.SetActive(false);
			}
			HierarchyReferences component = this.HarvestWhenReadyOverlayIcon.GetComponent<HierarchyReferences>();
			if (this.harvestWhenReady)
			{
				Image image = (Image)component.GetReference("On");
				image.gameObject.SetActive(true);
				image.color = GlobalAssets.Instance.colorSet.harvestEnabled;
				component.GetReference("Off").gameObject.SetActive(false);
				return;
			}
			component.GetReference("On").gameObject.SetActive(false);
			Image image2 = (Image)component.GetReference("Off");
			image2.gameObject.SetActive(true);
			image2.color = GlobalAssets.Instance.colorSet.harvestDisabled;
		}
	}

	// Token: 0x06003741 RID: 14145 RVA: 0x00133764 File Offset: 0x00131964
	public bool CanBeHarvested()
	{
		Harvestable component = base.GetComponent<Harvestable>();
		return !(component != null) || component.CanBeHarvested;
	}

	// Token: 0x06003742 RID: 14146 RVA: 0x00133789 File Offset: 0x00131989
	public void SetInPlanterBox(bool state)
	{
		if (state)
		{
			if (!this.isInPlanterBox)
			{
				this.isInPlanterBox = true;
				this.SetHarvestWhenReady(this.defaultHarvestStateWhenPlanted);
				return;
			}
		}
		else
		{
			this.isInPlanterBox = false;
		}
	}

	// Token: 0x06003743 RID: 14147 RVA: 0x001337B4 File Offset: 0x001319B4
	public void SetHarvestWhenReady(bool state)
	{
		this.harvestWhenReady = state;
		if (this.harvestWhenReady && this.CanBeHarvested() && !this.isMarkedForHarvest)
		{
			this.MarkForHarvest();
		}
		if (this.isMarkedForHarvest && !this.harvestWhenReady)
		{
			this.OnCancel(null);
			if (this.CanBeHarvested() && this.isInPlanterBox)
			{
				base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.NotMarkedForHarvest, this);
			}
		}
		base.Trigger(-266953818, null);
		this.RefreshOverlayIcon(null);
	}

	// Token: 0x06003744 RID: 14148 RVA: 0x0013383C File Offset: 0x00131A3C
	protected virtual void OnCancel(object data = null)
	{
	}

	// Token: 0x06003745 RID: 14149 RVA: 0x00133840 File Offset: 0x00131A40
	public virtual void MarkForHarvest()
	{
		if (!this.CanBeHarvested())
		{
			return;
		}
		this.isMarkedForHarvest = true;
		Harvestable component = base.GetComponent<Harvestable>();
		if (component != null)
		{
			component.OnMarkedForHarvest();
		}
	}

	// Token: 0x06003746 RID: 14150 RVA: 0x00133873 File Offset: 0x00131A73
	protected virtual void OnClickHarvestWhenReady()
	{
		this.SetHarvestWhenReady(true);
	}

	// Token: 0x06003747 RID: 14151 RVA: 0x0013387C File Offset: 0x00131A7C
	protected virtual void OnClickCancelHarvestWhenReady()
	{
		this.SetHarvestWhenReady(false);
	}

	// Token: 0x06003748 RID: 14152 RVA: 0x00133888 File Offset: 0x00131A88
	public virtual void OnRefreshUserMenu(object data)
	{
		if (this.showUserMenuButtons)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = (this.harvestWhenReady ? new KIconButtonMenu.ButtonInfo("action_harvest", UI.USERMENUACTIONS.CANCEL_HARVEST_WHEN_READY.NAME, delegate
			{
				this.OnClickCancelHarvestWhenReady();
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.GAMEOBJECTEFFECTS.PLANT_DO_NOT_HARVEST, base.transform, 1.5f, false);
			}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCEL_HARVEST_WHEN_READY.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_harvest", UI.USERMENUACTIONS.HARVEST_WHEN_READY.NAME, delegate
			{
				this.OnClickHarvestWhenReady();
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, UI.GAMEOBJECTEFFECTS.PLANT_MARK_FOR_HARVEST, base.transform, 1.5f, false);
			}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.HARVEST_WHEN_READY.TOOLTIP, true));
			Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
		}
	}

	// Token: 0x04002505 RID: 9477
	public bool defaultHarvestStateWhenPlanted = true;

	// Token: 0x04002506 RID: 9478
	public OccupyArea area;

	// Token: 0x04002507 RID: 9479
	[Serialize]
	protected bool isMarkedForHarvest;

	// Token: 0x04002508 RID: 9480
	[Serialize]
	private bool isInPlanterBox;

	// Token: 0x04002509 RID: 9481
	public bool showUserMenuButtons = true;

	// Token: 0x0400250A RID: 9482
	[Serialize]
	protected bool harvestWhenReady;

	// Token: 0x0400250B RID: 9483
	public RectTransform HarvestWhenReadyOverlayIcon;

	// Token: 0x0400250C RID: 9484
	private Action<object> onEnableOverlayDelegate;

	// Token: 0x0400250D RID: 9485
	private static readonly EventSystem.IntraObjectHandler<HarvestDesignatable> OnCancelDelegate = new EventSystem.IntraObjectHandler<HarvestDesignatable>(delegate(HarvestDesignatable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x0400250E RID: 9486
	private static readonly EventSystem.IntraObjectHandler<HarvestDesignatable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<HarvestDesignatable>(delegate(HarvestDesignatable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x0400250F RID: 9487
	private static readonly EventSystem.IntraObjectHandler<HarvestDesignatable> SetInPlanterBoxTrueDelegate = new EventSystem.IntraObjectHandler<HarvestDesignatable>(delegate(HarvestDesignatable component, object data)
	{
		component.SetInPlanterBox(true);
	});
}
