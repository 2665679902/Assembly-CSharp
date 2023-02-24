using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000A1A RID: 2586
public class RootMenu : KScreen
{
	// Token: 0x06004DEB RID: 19947 RVA: 0x001B820B File Offset: 0x001B640B
	public static void DestroyInstance()
	{
		RootMenu.Instance = null;
	}

	// Token: 0x170005C6 RID: 1478
	// (get) Token: 0x06004DEC RID: 19948 RVA: 0x001B8213 File Offset: 0x001B6413
	// (set) Token: 0x06004DED RID: 19949 RVA: 0x001B821A File Offset: 0x001B641A
	public static RootMenu Instance { get; private set; }

	// Token: 0x06004DEE RID: 19950 RVA: 0x001B8222 File Offset: 0x001B6422
	public override float GetSortKey()
	{
		return -1f;
	}

	// Token: 0x06004DEF RID: 19951 RVA: 0x001B822C File Offset: 0x001B642C
	protected override void OnPrefabInit()
	{
		RootMenu.Instance = this;
		base.Subscribe(Game.Instance.gameObject, -1503271301, new Action<object>(this.OnSelectObject));
		base.Subscribe(Game.Instance.gameObject, 288942073, new Action<object>(this.OnUIClear));
		base.Subscribe(Game.Instance.gameObject, -809948329, new Action<object>(this.OnBuildingStatechanged));
		base.OnPrefabInit();
	}

	// Token: 0x06004DF0 RID: 19952 RVA: 0x001B82AC File Offset: 0x001B64AC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.detailsScreen = Util.KInstantiateUI(this.detailsScreenPrefab, base.gameObject, true).GetComponent<DetailsScreen>();
		this.detailsScreen.gameObject.SetActive(true);
		this.userMenuParent = this.detailsScreen.UserMenuPanel.gameObject;
		this.userMenu = Util.KInstantiateUI(this.userMenuPrefab.gameObject, this.userMenuParent, false).GetComponent<UserMenuScreen>();
		this.detailsScreen.gameObject.SetActive(false);
		this.userMenu.gameObject.SetActive(false);
	}

	// Token: 0x06004DF1 RID: 19953 RVA: 0x001B8347 File Offset: 0x001B6547
	private void OnClickCommon()
	{
		this.CloseSubMenus();
	}

	// Token: 0x06004DF2 RID: 19954 RVA: 0x001B834F File Offset: 0x001B654F
	public void AddSubMenu(KScreen sub_menu)
	{
		if (sub_menu.activateOnSpawn)
		{
			sub_menu.Show(true);
		}
		this.subMenus.Add(sub_menu);
	}

	// Token: 0x06004DF3 RID: 19955 RVA: 0x001B836C File Offset: 0x001B656C
	public void RemoveSubMenu(KScreen sub_menu)
	{
		this.subMenus.Remove(sub_menu);
	}

	// Token: 0x06004DF4 RID: 19956 RVA: 0x001B837C File Offset: 0x001B657C
	private void CloseSubMenus()
	{
		foreach (KScreen kscreen in this.subMenus)
		{
			if (kscreen != null)
			{
				if (kscreen.activateOnSpawn)
				{
					kscreen.gameObject.SetActive(false);
				}
				else
				{
					kscreen.Deactivate();
				}
			}
		}
		this.subMenus.Clear();
	}

	// Token: 0x06004DF5 RID: 19957 RVA: 0x001B83F8 File Offset: 0x001B65F8
	private void OnSelectObject(object data)
	{
		GameObject gameObject = (GameObject)data;
		bool flag = false;
		if (gameObject != null)
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component != null && !component.IsInitialized())
			{
				return;
			}
			flag = component != null || CellSelectionObject.IsSelectionObject(gameObject);
		}
		if (gameObject != this.selectedGO)
		{
			this.selectedGO = null;
			this.CloseSubMenus();
			if (flag)
			{
				this.selectedGO = gameObject;
				this.AddSubMenu(this.detailsScreen);
				this.AddSubMenu(this.userMenu);
			}
			this.userMenu.SetSelected(this.selectedGO);
		}
		this.Refresh();
	}

	// Token: 0x06004DF6 RID: 19958 RVA: 0x001B8497 File Offset: 0x001B6697
	public void Refresh()
	{
		if (this.selectedGO == null)
		{
			return;
		}
		this.detailsScreen.Refresh(this.selectedGO);
		this.userMenu.Refresh(this.selectedGO);
	}

	// Token: 0x06004DF7 RID: 19959 RVA: 0x001B84CC File Offset: 0x001B66CC
	private void OnBuildingStatechanged(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == this.selectedGO)
		{
			this.OnSelectObject(gameObject);
		}
	}

	// Token: 0x06004DF8 RID: 19960 RVA: 0x001B84F8 File Offset: 0x001B66F8
	public override void OnKeyDown(KButtonEvent e)
	{
		if (!e.Consumed && e.TryConsume(global::Action.Escape) && SelectTool.Instance.enabled)
		{
			if (!this.canTogglePauseScreen)
			{
				return;
			}
			if (this.AreSubMenusOpen())
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Back", false));
				this.CloseSubMenus();
				SelectTool.Instance.Select(null, false);
			}
			else if (e.IsAction(global::Action.Escape))
			{
				if (!SelectTool.Instance.enabled)
				{
					KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
				}
				if (PlayerController.Instance.IsUsingDefaultTool())
				{
					if (SelectTool.Instance.selected != null)
					{
						SelectTool.Instance.Select(null, false);
					}
					else
					{
						CameraController.Instance.ForcePanningState(false);
						this.TogglePauseScreen();
					}
				}
				else
				{
					Game.Instance.Trigger(288942073, null);
				}
				ToolMenu.Instance.ClearSelection();
				SelectTool.Instance.Activate();
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06004DF9 RID: 19961 RVA: 0x001B85FA File Offset: 0x001B67FA
	public override void OnKeyUp(KButtonEvent e)
	{
		base.OnKeyUp(e);
		if (!e.Consumed && e.TryConsume(global::Action.AlternateView) && this.tileScreenInst != null)
		{
			this.tileScreenInst.Deactivate();
			this.tileScreenInst = null;
		}
	}

	// Token: 0x06004DFA RID: 19962 RVA: 0x001B8635 File Offset: 0x001B6835
	public void TogglePauseScreen()
	{
		PauseScreen.Instance.Show(true);
	}

	// Token: 0x06004DFB RID: 19963 RVA: 0x001B8642 File Offset: 0x001B6842
	public void ExternalClose()
	{
		this.OnClickCommon();
	}

	// Token: 0x06004DFC RID: 19964 RVA: 0x001B864A File Offset: 0x001B684A
	private void OnUIClear(object data)
	{
		this.CloseSubMenus();
		SelectTool.Instance.Select(null, true);
		if (UnityEngine.EventSystems.EventSystem.current != null)
		{
			UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
			return;
		}
		global::Debug.LogWarning("OnUIClear() Event system is null");
	}

	// Token: 0x06004DFD RID: 19965 RVA: 0x001B8681 File Offset: 0x001B6881
	protected override void OnActivate()
	{
		base.OnActivate();
	}

	// Token: 0x06004DFE RID: 19966 RVA: 0x001B8689 File Offset: 0x001B6889
	private bool AreSubMenusOpen()
	{
		return this.subMenus.Count > 0;
	}

	// Token: 0x06004DFF RID: 19967 RVA: 0x001B869C File Offset: 0x001B689C
	private KToggleMenu.ToggleInfo[] GetFillers()
	{
		HashSet<Tag> hashSet = new HashSet<Tag>();
		List<KToggleMenu.ToggleInfo> list = new List<KToggleMenu.ToggleInfo>();
		foreach (Pickupable pickupable in Components.Pickupables.Items)
		{
			KPrefabID kprefabID = pickupable.KPrefabID;
			if (kprefabID.HasTag(GameTags.Filler) && hashSet.Add(kprefabID.PrefabTag))
			{
				string text = kprefabID.GetComponent<PrimaryElement>().Element.id.ToString();
				list.Add(new KToggleMenu.ToggleInfo(text, null, global::Action.NumActions));
			}
		}
		return list.ToArray();
	}

	// Token: 0x06004E00 RID: 19968 RVA: 0x001B8750 File Offset: 0x001B6950
	public bool IsBuildingChorePanelActive()
	{
		return this.detailsScreen != null && this.detailsScreen.GetActiveTab() is BuildingChoresPanel;
	}

	// Token: 0x0400337B RID: 13179
	private DetailsScreen detailsScreen;

	// Token: 0x0400337C RID: 13180
	private UserMenuScreen userMenu;

	// Token: 0x0400337D RID: 13181
	[SerializeField]
	private GameObject detailsScreenPrefab;

	// Token: 0x0400337E RID: 13182
	[SerializeField]
	private UserMenuScreen userMenuPrefab;

	// Token: 0x0400337F RID: 13183
	private GameObject userMenuParent;

	// Token: 0x04003380 RID: 13184
	[SerializeField]
	private TileScreen tileScreen;

	// Token: 0x04003382 RID: 13186
	public KScreen buildMenu;

	// Token: 0x04003383 RID: 13187
	private List<KScreen> subMenus = new List<KScreen>();

	// Token: 0x04003384 RID: 13188
	private TileScreen tileScreenInst;

	// Token: 0x04003385 RID: 13189
	public bool canTogglePauseScreen = true;

	// Token: 0x04003386 RID: 13190
	public GameObject selectedGO;
}
