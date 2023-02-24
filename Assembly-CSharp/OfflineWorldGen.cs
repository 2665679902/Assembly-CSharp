using System;
using System.Collections.Generic;
using System.Threading;
using Klei.CustomSettings;
using ProcGenGame;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x02000C38 RID: 3128
[AddComponentMenu("KMonoBehaviour/scripts/OfflineWorldGen")]
public class OfflineWorldGen : KMonoBehaviour
{
	// Token: 0x060062ED RID: 25325 RVA: 0x00248F30 File Offset: 0x00247130
	private void TrackProgress(string text)
	{
		if (this.trackProgress)
		{
			global::Debug.Log(text);
		}
	}

	// Token: 0x060062EE RID: 25326 RVA: 0x00248F40 File Offset: 0x00247140
	public static bool CanLoadSave()
	{
		bool flag = WorldGen.CanLoad(SaveLoader.GetActiveSaveFilePath());
		if (!flag)
		{
			SaveLoader.SetActiveSaveFilePath(null);
			flag = WorldGen.CanLoad(WorldGen.GetSIMSaveFilename(0));
		}
		return flag;
	}

	// Token: 0x060062EF RID: 25327 RVA: 0x00248F70 File Offset: 0x00247170
	public void Generate()
	{
		this.doWorldGen = !OfflineWorldGen.CanLoadSave();
		this.updateText.gameObject.SetActive(false);
		this.percentText.gameObject.SetActive(false);
		this.doWorldGen |= this.debug;
		if (this.doWorldGen)
		{
			this.seedText.text = string.Format(UI.WORLDGEN.USING_PLAYER_SEED, this.seed);
			this.titleText.text = UI.FRONTEND.WORLDGENSCREEN.TITLE.ToString();
			this.mainText.text = UI.WORLDGEN.CHOOSEWORLDSIZE.ToString();
			for (int i = 0; i < this.validDimensions.Length; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab);
				gameObject.SetActive(true);
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.SetParent(this.buttonRoot);
				component.localScale = Vector3.one;
				TMP_Text componentInChildren = gameObject.GetComponentInChildren<LocText>();
				OfflineWorldGen.ValidDimensions validDimensions = this.validDimensions[i];
				componentInChildren.text = validDimensions.name.ToString();
				int idx = i;
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.DoWorldGen(idx);
					this.ToggleGenerationUI();
				};
			}
			if (this.validDimensions.Length == 1)
			{
				this.DoWorldGen(0);
				this.ToggleGenerationUI();
			}
			ScreenResize instance = ScreenResize.Instance;
			instance.OnResize = (System.Action)Delegate.Combine(instance.OnResize, new System.Action(this.OnResize));
			this.OnResize();
		}
		else
		{
			this.titleText.text = UI.FRONTEND.WORLDGENSCREEN.LOADINGGAME.ToString();
			this.mainText.gameObject.SetActive(false);
			this.currentConvertedCurrentStage = UI.WORLDGEN.COMPLETE.key;
			this.currentPercent = 1f;
			this.updateText.gameObject.SetActive(false);
			this.percentText.gameObject.SetActive(false);
			this.RemoveButtons();
		}
		this.buttonPrefab.SetActive(false);
	}

	// Token: 0x060062F0 RID: 25328 RVA: 0x00249170 File Offset: 0x00247370
	private void OnResize()
	{
		float canvasScale = base.GetComponentInParent<KCanvasScaler>().GetCanvasScale();
		if (this.asteriodAnim != null)
		{
			this.asteriodAnim.animScale = 0.005f * (1f / canvasScale);
		}
	}

	// Token: 0x060062F1 RID: 25329 RVA: 0x002491B8 File Offset: 0x002473B8
	private void ToggleGenerationUI()
	{
		this.percentText.gameObject.SetActive(false);
		this.updateText.gameObject.SetActive(true);
		this.titleText.text = UI.FRONTEND.WORLDGENSCREEN.GENERATINGWORLD.ToString();
		if (this.titleText != null && this.titleText.gameObject != null)
		{
			this.titleText.gameObject.SetActive(false);
		}
		if (this.buttonRoot != null && this.buttonRoot.gameObject != null)
		{
			this.buttonRoot.gameObject.SetActive(false);
		}
	}

	// Token: 0x060062F2 RID: 25330 RVA: 0x00249260 File Offset: 0x00247460
	private bool UpdateProgress(StringKey stringKeyRoot, float completePercent, WorldGenProgressStages.Stages stage)
	{
		if (this.currentStage != stage)
		{
			this.currentStage = stage;
		}
		if (this.currentStringKeyRoot.Hash != stringKeyRoot.Hash)
		{
			this.currentConvertedCurrentStage = stringKeyRoot;
			this.currentStringKeyRoot = stringKeyRoot;
		}
		else
		{
			int num = (int)completePercent * 10;
			LocString locString = this.convertList.Find((LocString s) => s.key.Hash == stringKeyRoot.Hash);
			if (num != 0 && locString != null)
			{
				this.currentConvertedCurrentStage = new StringKey(locString.key.String + num.ToString());
			}
		}
		float num2 = 0f;
		float num3 = 0f;
		float num4 = WorldGenProgressStages.StageWeights[(int)stage].Value * completePercent;
		for (int i = 0; i < WorldGenProgressStages.StageWeights.Length; i++)
		{
			num3 += WorldGenProgressStages.StageWeights[i].Value;
			if (i < (int)this.currentStage)
			{
				num2 += WorldGenProgressStages.StageWeights[i].Value;
			}
		}
		float num5 = (num2 + num4) / num3;
		this.currentPercent = num5;
		return !this.shouldStop;
	}

	// Token: 0x060062F3 RID: 25331 RVA: 0x00249388 File Offset: 0x00247588
	private void Update()
	{
		if (this.loadTriggered)
		{
			return;
		}
		if (this.currentConvertedCurrentStage.String == null)
		{
			return;
		}
		this.errorMutex.WaitOne();
		int count = this.errors.Count;
		this.errorMutex.ReleaseMutex();
		if (count > 0)
		{
			this.DoExitFlow();
			return;
		}
		this.updateText.text = Strings.Get(this.currentConvertedCurrentStage.String);
		if (!this.debug && this.currentConvertedCurrentStage.Hash == UI.WORLDGEN.COMPLETE.key.Hash && this.currentPercent >= 1f && this.clusterLayout.IsGenerationComplete)
		{
			if (KCrashReporter.terminateOnError && KCrashReporter.hasCrash)
			{
				return;
			}
			this.percentText.text = "";
			this.loadTriggered = true;
			App.LoadScene(this.mainGameLevel);
			return;
		}
		else
		{
			if (this.currentPercent < 0f)
			{
				this.DoExitFlow();
				return;
			}
			if (this.currentPercent > 0f && !this.percentText.gameObject.activeSelf)
			{
				this.percentText.gameObject.SetActive(false);
			}
			this.percentText.text = GameUtil.GetFormattedPercent(this.currentPercent * 100f, GameUtil.TimeSlice.None);
			this.meterAnim.SetPositionPercent(this.currentPercent);
			return;
		}
	}

	// Token: 0x060062F4 RID: 25332 RVA: 0x002494DC File Offset: 0x002476DC
	private void DisplayErrors()
	{
		this.errorMutex.WaitOne();
		if (this.errors.Count > 0)
		{
			foreach (OfflineWorldGen.ErrorInfo errorInfo in this.errors)
			{
				Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, FrontEndManager.Instance.gameObject, true).PopupConfirmDialog(errorInfo.errorDesc, new System.Action(this.OnConfirmExit), null, null, null, null, null, null, null);
			}
		}
		this.errorMutex.ReleaseMutex();
	}

	// Token: 0x060062F5 RID: 25333 RVA: 0x0024958C File Offset: 0x0024778C
	private void DoExitFlow()
	{
		if (this.startedExitFlow)
		{
			return;
		}
		this.startedExitFlow = true;
		this.percentText.text = UI.WORLDGEN.RESTARTING.ToString();
		this.loadTriggered = true;
		Sim.Shutdown();
		this.DisplayErrors();
	}

	// Token: 0x060062F6 RID: 25334 RVA: 0x002495C5 File Offset: 0x002477C5
	private void OnConfirmExit()
	{
		App.LoadScene(this.frontendGameLevel);
	}

	// Token: 0x060062F7 RID: 25335 RVA: 0x002495D4 File Offset: 0x002477D4
	private void RemoveButtons()
	{
		for (int i = this.buttonRoot.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(this.buttonRoot.GetChild(i).gameObject);
		}
	}

	// Token: 0x060062F8 RID: 25336 RVA: 0x0024960F File Offset: 0x0024780F
	private void DoWorldGen(int selectedDimension)
	{
		this.RemoveButtons();
		this.DoWorldGenInitialize();
	}

	// Token: 0x060062F9 RID: 25337 RVA: 0x00249620 File Offset: 0x00247820
	private void DoWorldGenInitialize()
	{
		string text = "";
		Func<int, WorldGen, bool> func = null;
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.WorldgenSeed);
		this.seed = int.Parse(currentQualitySetting.id);
		text = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ClusterLayout).id;
		List<string> list = new List<string>();
		foreach (string text2 in CustomGameSettings.Instance.GetCurrentStories())
		{
			list.Add(Db.Get().Stories.Get(text2).worldgenStoryTraitKey);
		}
		this.clusterLayout = new Cluster(text, this.seed, list, true, false);
		this.clusterLayout.ShouldSkipWorldCallback = func;
		this.clusterLayout.Generate(new WorldGen.OfflineCallbackFunction(this.UpdateProgress), new Action<OfflineWorldGen.ErrorInfo>(this.OnError), this.seed, this.seed, this.seed, this.seed, true, false);
	}

	// Token: 0x060062FA RID: 25338 RVA: 0x00249730 File Offset: 0x00247930
	private void OnError(OfflineWorldGen.ErrorInfo error)
	{
		this.errorMutex.WaitOne();
		this.errors.Add(error);
		this.errorMutex.ReleaseMutex();
	}

	// Token: 0x040044AB RID: 17579
	[SerializeField]
	private RectTransform buttonRoot;

	// Token: 0x040044AC RID: 17580
	[SerializeField]
	private GameObject buttonPrefab;

	// Token: 0x040044AD RID: 17581
	[SerializeField]
	private RectTransform chooseLocationPanel;

	// Token: 0x040044AE RID: 17582
	[SerializeField]
	private GameObject locationButtonPrefab;

	// Token: 0x040044AF RID: 17583
	private const float baseScale = 0.005f;

	// Token: 0x040044B0 RID: 17584
	private Mutex errorMutex = new Mutex();

	// Token: 0x040044B1 RID: 17585
	private List<OfflineWorldGen.ErrorInfo> errors = new List<OfflineWorldGen.ErrorInfo>();

	// Token: 0x040044B2 RID: 17586
	private OfflineWorldGen.ValidDimensions[] validDimensions = new OfflineWorldGen.ValidDimensions[]
	{
		new OfflineWorldGen.ValidDimensions
		{
			width = 256,
			height = 384,
			name = UI.FRONTEND.WORLDGENSCREEN.SIZES.STANDARD.key
		}
	};

	// Token: 0x040044B3 RID: 17587
	public string frontendGameLevel = "frontend";

	// Token: 0x040044B4 RID: 17588
	public string mainGameLevel = "backend";

	// Token: 0x040044B5 RID: 17589
	private bool shouldStop;

	// Token: 0x040044B6 RID: 17590
	private StringKey currentConvertedCurrentStage;

	// Token: 0x040044B7 RID: 17591
	private float currentPercent;

	// Token: 0x040044B8 RID: 17592
	public bool debug;

	// Token: 0x040044B9 RID: 17593
	private bool trackProgress = true;

	// Token: 0x040044BA RID: 17594
	private bool doWorldGen;

	// Token: 0x040044BB RID: 17595
	[SerializeField]
	private LocText titleText;

	// Token: 0x040044BC RID: 17596
	[SerializeField]
	private LocText mainText;

	// Token: 0x040044BD RID: 17597
	[SerializeField]
	private LocText updateText;

	// Token: 0x040044BE RID: 17598
	[SerializeField]
	private LocText percentText;

	// Token: 0x040044BF RID: 17599
	[SerializeField]
	private LocText seedText;

	// Token: 0x040044C0 RID: 17600
	[SerializeField]
	private KBatchedAnimController meterAnim;

	// Token: 0x040044C1 RID: 17601
	[SerializeField]
	private KBatchedAnimController asteriodAnim;

	// Token: 0x040044C2 RID: 17602
	private Cluster clusterLayout;

	// Token: 0x040044C3 RID: 17603
	private StringKey currentStringKeyRoot;

	// Token: 0x040044C4 RID: 17604
	private List<LocString> convertList = new List<LocString>
	{
		UI.WORLDGEN.SETTLESIM,
		UI.WORLDGEN.BORDERS,
		UI.WORLDGEN.PROCESSING,
		UI.WORLDGEN.COMPLETELAYOUT,
		UI.WORLDGEN.WORLDLAYOUT,
		UI.WORLDGEN.GENERATENOISE,
		UI.WORLDGEN.BUILDNOISESOURCE,
		UI.WORLDGEN.GENERATESOLARSYSTEM
	};

	// Token: 0x040044C5 RID: 17605
	private WorldGenProgressStages.Stages currentStage;

	// Token: 0x040044C6 RID: 17606
	private bool loadTriggered;

	// Token: 0x040044C7 RID: 17607
	private bool startedExitFlow;

	// Token: 0x040044C8 RID: 17608
	private int seed;

	// Token: 0x02001AC6 RID: 6854
	public struct ErrorInfo
	{
		// Token: 0x040078B2 RID: 30898
		public string errorDesc;

		// Token: 0x040078B3 RID: 30899
		public Exception exception;
	}

	// Token: 0x02001AC7 RID: 6855
	[Serializable]
	private struct ValidDimensions
	{
		// Token: 0x040078B4 RID: 30900
		public int width;

		// Token: 0x040078B5 RID: 30901
		public int height;

		// Token: 0x040078B6 RID: 30902
		public StringKey name;
	}
}
