using System;
using UnityEngine;

// Token: 0x0200090C RID: 2316
[AddComponentMenu("KMonoBehaviour/scripts/ScreenPrefabs")]
public class ScreenPrefabs : KMonoBehaviour
{
	// Token: 0x170004CA RID: 1226
	// (get) Token: 0x0600438B RID: 17291 RVA: 0x0017DAF5 File Offset: 0x0017BCF5
	// (set) Token: 0x0600438C RID: 17292 RVA: 0x0017DAFC File Offset: 0x0017BCFC
	public static ScreenPrefabs Instance { get; private set; }

	// Token: 0x0600438D RID: 17293 RVA: 0x0017DB04 File Offset: 0x0017BD04
	protected override void OnPrefabInit()
	{
		ScreenPrefabs.Instance = this;
	}

	// Token: 0x0600438E RID: 17294 RVA: 0x0017DB0C File Offset: 0x0017BD0C
	public void ConfirmDoAction(string message, System.Action action, Transform parent)
	{
		((ConfirmDialogScreen)KScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, parent.gameObject)).PopupConfirmDialog(message, action, delegate
		{
		}, null, null, null, null, null, null);
	}

	// Token: 0x04002CFF RID: 11519
	public ControlsScreen ControlsScreen;

	// Token: 0x04002D00 RID: 11520
	public Hud HudScreen;

	// Token: 0x04002D01 RID: 11521
	public HoverTextScreen HoverTextScreen;

	// Token: 0x04002D02 RID: 11522
	public OverlayScreen OverlayScreen;

	// Token: 0x04002D03 RID: 11523
	public TileScreen TileScreen;

	// Token: 0x04002D04 RID: 11524
	public SpeedControlScreen SpeedControlScreen;

	// Token: 0x04002D05 RID: 11525
	public ManagementMenu ManagementMenu;

	// Token: 0x04002D06 RID: 11526
	public ToolTipScreen ToolTipScreen;

	// Token: 0x04002D07 RID: 11527
	public DebugPaintElementScreen DebugPaintElementScreen;

	// Token: 0x04002D08 RID: 11528
	public UserMenuScreen UserMenuScreen;

	// Token: 0x04002D09 RID: 11529
	public KButtonMenu OwnerScreen;

	// Token: 0x04002D0A RID: 11530
	public EnergyInfoScreen EnergyInfoScreen;

	// Token: 0x04002D0B RID: 11531
	public KButtonMenu ButtonGrid;

	// Token: 0x04002D0C RID: 11532
	public NameDisplayScreen NameDisplayScreen;

	// Token: 0x04002D0D RID: 11533
	public ConfirmDialogScreen ConfirmDialogScreen;

	// Token: 0x04002D0E RID: 11534
	public CustomizableDialogScreen CustomizableDialogScreen;

	// Token: 0x04002D0F RID: 11535
	public SpriteListDialogScreen SpriteListDialogScreen;

	// Token: 0x04002D10 RID: 11536
	public InfoDialogScreen InfoDialogScreen;

	// Token: 0x04002D11 RID: 11537
	public StoryMessageScreen StoryMessageScreen;

	// Token: 0x04002D12 RID: 11538
	public SubSpeciesInfoScreen SubSpeciesInfoScreen;

	// Token: 0x04002D13 RID: 11539
	public EventInfoScreen eventInfoScreen;

	// Token: 0x04002D14 RID: 11540
	public FileNameDialog FileNameDialog;

	// Token: 0x04002D15 RID: 11541
	public TagFilterScreen TagFilterScreen;

	// Token: 0x04002D16 RID: 11542
	public ResearchScreen ResearchScreen;

	// Token: 0x04002D17 RID: 11543
	public MessageDialogFrame MessageDialogFrame;

	// Token: 0x04002D18 RID: 11544
	public ResourceCategoryScreen ResourceCategoryScreen;

	// Token: 0x04002D19 RID: 11545
	public ColonyDiagnosticScreen ColonyDiagnosticScreen;

	// Token: 0x04002D1A RID: 11546
	public LanguageOptionsScreen languageOptionsScreen;

	// Token: 0x04002D1B RID: 11547
	public ModsScreen modsMenu;

	// Token: 0x04002D1C RID: 11548
	public RailModUploadScreen RailModUploadMenu;

	// Token: 0x04002D1D RID: 11549
	public GameObject GameOverScreen;

	// Token: 0x04002D1E RID: 11550
	public GameObject VictoryScreen;

	// Token: 0x04002D1F RID: 11551
	public GameObject StatusItemIndicatorScreen;

	// Token: 0x04002D20 RID: 11552
	public GameObject CollapsableContentPanel;

	// Token: 0x04002D21 RID: 11553
	public GameObject DescriptionLabel;

	// Token: 0x04002D22 RID: 11554
	public LoadingOverlay loadingOverlay;

	// Token: 0x04002D23 RID: 11555
	public LoadScreen LoadScreen;

	// Token: 0x04002D24 RID: 11556
	public InspectSaveScreen InspectSaveScreen;

	// Token: 0x04002D25 RID: 11557
	public OptionsMenuScreen OptionsScreen;

	// Token: 0x04002D26 RID: 11558
	public WorldGenScreen WorldGenScreen;

	// Token: 0x04002D27 RID: 11559
	public ModeSelectScreen ModeSelectScreen;

	// Token: 0x04002D28 RID: 11560
	public ColonyDestinationSelectScreen ColonyDestinationSelectScreen;

	// Token: 0x04002D29 RID: 11561
	public RetiredColonyInfoScreen RetiredColonyInfoScreen;

	// Token: 0x04002D2A RID: 11562
	public VideoScreen VideoScreen;

	// Token: 0x04002D2B RID: 11563
	public ComicViewer ComicViewer;

	// Token: 0x04002D2C RID: 11564
	public GameObject OldVersionWarningScreen;

	// Token: 0x04002D2D RID: 11565
	[Header("Klei Items")]
	public GameObject KleiItemDropScreen;

	// Token: 0x04002D2E RID: 11566
	public GameObject LockerMenuScreen;

	// Token: 0x04002D2F RID: 11567
	public GameObject LockerNavigator;

	// Token: 0x04002D30 RID: 11568
	[Header("Main Menu")]
	public GameObject MainMenuForVanilla;

	// Token: 0x04002D31 RID: 11569
	public GameObject MainMenuForSpacedOut;

	// Token: 0x04002D32 RID: 11570
	public GameObject MainMenuIntroShort;

	// Token: 0x04002D33 RID: 11571
	public GameObject MainMenuHealthyGameMessage;
}
