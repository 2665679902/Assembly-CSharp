using System;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x02000A4F RID: 2639
public class BuildingGroupScreen : KScreen
{
	// Token: 0x170005D4 RID: 1492
	// (get) Token: 0x06005021 RID: 20513 RVA: 0x001CB26D File Offset: 0x001C946D
	public static bool SearchIsEmpty
	{
		get
		{
			return BuildingGroupScreen.Instance == null || BuildingGroupScreen.Instance.inputField.text.IsNullOrWhiteSpace();
		}
	}

	// Token: 0x170005D5 RID: 1493
	// (get) Token: 0x06005022 RID: 20514 RVA: 0x001CB292 File Offset: 0x001C9492
	public static bool IsEditing
	{
		get
		{
			return !(BuildingGroupScreen.Instance == null) && BuildingGroupScreen.Instance.isEditing;
		}
	}

	// Token: 0x06005023 RID: 20515 RVA: 0x001CB2AD File Offset: 0x001C94AD
	protected override void OnPrefabInit()
	{
		BuildingGroupScreen.Instance = this;
		base.OnPrefabInit();
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005024 RID: 20516 RVA: 0x001CB2C4 File Offset: 0x001C94C4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KInputTextField kinputTextField = this.inputField;
		kinputTextField.onFocus = (System.Action)Delegate.Combine(kinputTextField.onFocus, new System.Action(delegate
		{
			base.isEditing = true;
			UISounds.PlaySound(UISounds.Sound.ClickHUD);
			this.ConfigurePlanScreenForSearch();
		}));
		this.inputField.onEndEdit.AddListener(delegate(string value)
		{
			base.isEditing = false;
		});
		this.inputField.onValueChanged.AddListener(delegate(string value)
		{
			PlanScreen.Instance.RefreshCategoryPanelTitle();
		});
		this.inputField.placeholder.GetComponent<TextMeshProUGUI>().text = UI.BUILDMENU.SEARCH_TEXT_PLACEHOLDER;
		this.clearButton.onClick += this.ClearSearch;
	}

	// Token: 0x06005025 RID: 20517 RVA: 0x001CB37F File Offset: 0x001C957F
	protected override void OnActivate()
	{
		base.OnActivate();
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005026 RID: 20518 RVA: 0x001CB38E File Offset: 0x001C958E
	public void ClearSearch()
	{
		this.inputField.text = "";
	}

	// Token: 0x06005027 RID: 20519 RVA: 0x001CB3A0 File Offset: 0x001C95A0
	private void ConfigurePlanScreenForSearch()
	{
		PlanScreen.Instance.SoftCloseRecipe();
		PlanScreen.Instance.ClearSelection();
		PlanScreen.Instance.ForceRefreshAllBuildingToggles();
		PlanScreen.Instance.ConfigurePanelSize(null);
	}

	// Token: 0x040035D6 RID: 13782
	public static BuildingGroupScreen Instance;

	// Token: 0x040035D7 RID: 13783
	public KInputTextField inputField;

	// Token: 0x040035D8 RID: 13784
	[SerializeField]
	public KButton clearButton;
}
