using System;
using System.Collections.Generic;
using System.Linq;
using Klei.CustomSettings;
using ProcGen;
using UnityEngine;

// Token: 0x02000A8A RID: 2698
[AddComponentMenu("KMonoBehaviour/scripts/DestinationSelectPanel")]
public class DestinationSelectPanel : KMonoBehaviour
{
	// Token: 0x17000626 RID: 1574
	// (get) Token: 0x0600528C RID: 21132 RVA: 0x001DD42A File Offset: 0x001DB62A
	// (set) Token: 0x0600528D RID: 21133 RVA: 0x001DD431 File Offset: 0x001DB631
	public static int ChosenClusterCategorySetting
	{
		get
		{
			return DestinationSelectPanel.chosenClusterCategorySetting;
		}
		set
		{
			DestinationSelectPanel.chosenClusterCategorySetting = value;
		}
	}

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x0600528E RID: 21134 RVA: 0x001DD43C File Offset: 0x001DB63C
	// (remove) Token: 0x0600528F RID: 21135 RVA: 0x001DD474 File Offset: 0x001DB674
	public event Action<ColonyDestinationAsteroidBeltData> OnAsteroidClicked;

	// Token: 0x17000627 RID: 1575
	// (get) Token: 0x06005290 RID: 21136 RVA: 0x001DD4AC File Offset: 0x001DB6AC
	private float min
	{
		get
		{
			return this.asteroidContainer.rect.x + this.offset;
		}
	}

	// Token: 0x17000628 RID: 1576
	// (get) Token: 0x06005291 RID: 21137 RVA: 0x001DD4D4 File Offset: 0x001DB6D4
	private float max
	{
		get
		{
			return this.min + this.asteroidContainer.rect.width;
		}
	}

	// Token: 0x06005292 RID: 21138 RVA: 0x001DD4FC File Offset: 0x001DB6FC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.dragTarget.onBeginDrag += this.BeginDrag;
		this.dragTarget.onDrag += this.Drag;
		this.dragTarget.onEndDrag += this.EndDrag;
		MultiToggle multiToggle = this.leftArrowButton;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(this.ClickLeft));
		MultiToggle multiToggle2 = this.rightArrowButton;
		multiToggle2.onClick = (System.Action)Delegate.Combine(multiToggle2.onClick, new System.Action(this.ClickRight));
	}

	// Token: 0x06005293 RID: 21139 RVA: 0x001DD5A2 File Offset: 0x001DB7A2
	private void BeginDrag()
	{
		this.dragStartPos = KInputManager.GetMousePos();
		this.dragLastPos = this.dragStartPos;
		this.isDragging = true;
		KFMOD.PlayUISound(GlobalAssets.GetSound("DestinationSelect_Scroll_Start", false));
	}

	// Token: 0x06005294 RID: 21140 RVA: 0x001DD5D8 File Offset: 0x001DB7D8
	private void Drag()
	{
		Vector2 vector = KInputManager.GetMousePos();
		float num = vector.x - this.dragLastPos.x;
		this.dragLastPos = vector;
		this.offset += num;
		int num2 = this.selectedIndex;
		this.selectedIndex = Mathf.RoundToInt(-this.offset / this.asteroidXSeparation);
		this.selectedIndex = Mathf.Clamp(this.selectedIndex, 0, this.clusterStartWorlds.Count - 1);
		if (num2 != this.selectedIndex)
		{
			this.OnAsteroidClicked(this.asteroidData[this.clusterKeys[this.selectedIndex]]);
			KFMOD.PlayUISound(GlobalAssets.GetSound("DestinationSelect_Scroll", false));
		}
	}

	// Token: 0x06005295 RID: 21141 RVA: 0x001DD695 File Offset: 0x001DB895
	private void EndDrag()
	{
		this.Drag();
		this.isDragging = false;
		KFMOD.PlayUISound(GlobalAssets.GetSound("DestinationSelect_Scroll_Stop", false));
	}

	// Token: 0x06005296 RID: 21142 RVA: 0x001DD6B4 File Offset: 0x001DB8B4
	private void ClickLeft()
	{
		this.selectedIndex = Mathf.Clamp(this.selectedIndex - 1, 0, this.clusterKeys.Count - 1);
		this.OnAsteroidClicked(this.asteroidData[this.clusterKeys[this.selectedIndex]]);
	}

	// Token: 0x06005297 RID: 21143 RVA: 0x001DD70C File Offset: 0x001DB90C
	private void ClickRight()
	{
		this.selectedIndex = Mathf.Clamp(this.selectedIndex + 1, 0, this.clusterKeys.Count - 1);
		this.OnAsteroidClicked(this.asteroidData[this.clusterKeys[this.selectedIndex]]);
	}

	// Token: 0x06005298 RID: 21144 RVA: 0x001DD761 File Offset: 0x001DB961
	public void Init()
	{
		this.clusterKeys = new List<string>();
		this.clusterStartWorlds = new Dictionary<string, string>();
		this.UpdateDisplayedClusters();
	}

	// Token: 0x06005299 RID: 21145 RVA: 0x001DD780 File Offset: 0x001DB980
	private void Update()
	{
		if (!this.isDragging)
		{
			float num = this.offset + (float)this.selectedIndex * this.asteroidXSeparation;
			float num2 = 0f;
			if (num != 0f)
			{
				num2 = -num;
			}
			num2 = Mathf.Clamp(num2, -this.asteroidXSeparation * 2f, this.asteroidXSeparation * 2f);
			if (num2 != 0f)
			{
				float num3 = this.centeringSpeed * Time.unscaledDeltaTime;
				float num4 = num2 * this.centeringSpeed * Time.unscaledDeltaTime;
				if (num4 > 0f && num4 < num3)
				{
					num4 = Mathf.Min(num3, num2);
				}
				else if (num4 < 0f && num4 > -num3)
				{
					num4 = Mathf.Max(-num3, num2);
				}
				this.offset += num4;
			}
		}
		float x = this.asteroidContainer.rect.min.x;
		float x2 = this.asteroidContainer.rect.max.x;
		this.offset = Mathf.Clamp(this.offset, (float)(-(float)(this.clusterStartWorlds.Count - 1)) * this.asteroidXSeparation + x, x2);
		this.RePlaceAsteroids();
		for (int i = 0; i < this.moonContainer.transform.childCount; i++)
		{
			this.moonContainer.transform.GetChild(i).GetChild(0).SetLocalPosition(new Vector3(0f, 1.5f + 3f * Mathf.Sin(((float)i + Time.realtimeSinceStartup) * 1.25f), 0f));
		}
	}

	// Token: 0x0600529A RID: 21146 RVA: 0x001DD91C File Offset: 0x001DBB1C
	public void UpdateDisplayedClusters()
	{
		this.clusterKeys.Clear();
		this.clusterStartWorlds.Clear();
		this.asteroidData.Clear();
		foreach (KeyValuePair<string, ClusterLayout> keyValuePair in SettingsCache.clusterLayouts.clusterCache)
		{
			if ((!DlcManager.FeatureClusterSpaceEnabled() || !(keyValuePair.Key == "clusters/SandstoneDefault")) && keyValuePair.Value.clusterCategory == DestinationSelectPanel.ChosenClusterCategorySetting)
			{
				this.clusterKeys.Add(keyValuePair.Key);
				ColonyDestinationAsteroidBeltData colonyDestinationAsteroidBeltData = new ColonyDestinationAsteroidBeltData(keyValuePair.Value.GetStartWorld(), 0, keyValuePair.Key);
				this.asteroidData[keyValuePair.Key] = colonyDestinationAsteroidBeltData;
				this.clusterStartWorlds.Add(keyValuePair.Key, keyValuePair.Value.GetStartWorld());
			}
		}
		this.clusterKeys.Sort((string a, string b) => SettingsCache.clusterLayouts.clusterCache[a].menuOrder.CompareTo(SettingsCache.clusterLayouts.clusterCache[b].menuOrder));
	}

	// Token: 0x0600529B RID: 21147 RVA: 0x001DDA48 File Offset: 0x001DBC48
	[ContextMenu("RePlaceAsteroids")]
	public void RePlaceAsteroids()
	{
		this.BeginAsteroidDrawing();
		for (int i = 0; i < this.clusterKeys.Count; i++)
		{
			float num = this.offset + (float)i * this.asteroidXSeparation;
			string text = this.clusterKeys[i];
			float iconScale = this.asteroidData[text].GetStartWorld.iconScale;
			this.GetAsteroid(text, (i == this.selectedIndex) ? (this.asteroidFocusScale * iconScale) : iconScale).transform.SetLocalPosition(new Vector3(num, (i == this.selectedIndex) ? (5f + 10f * Mathf.Sin(Time.realtimeSinceStartup * 1f)) : 0f, 0f));
		}
		this.EndAsteroidDrawing();
	}

	// Token: 0x0600529C RID: 21148 RVA: 0x001DDB0F File Offset: 0x001DBD0F
	private void BeginAsteroidDrawing()
	{
		this.numAsteroids = 0;
	}

	// Token: 0x0600529D RID: 21149 RVA: 0x001DDB18 File Offset: 0x001DBD18
	private void ShowMoons(ColonyDestinationAsteroidBeltData asteroid)
	{
		if (asteroid.worlds.Count > 0)
		{
			while (this.moonContainer.transform.childCount < asteroid.worlds.Count)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.moonPrefab, this.moonContainer.transform);
			}
			for (int i = 0; i < asteroid.worlds.Count; i++)
			{
				KBatchedAnimController componentInChildren = this.moonContainer.transform.GetChild(i).GetComponentInChildren<KBatchedAnimController>();
				int num = (-1 + i + asteroid.worlds.Count / 2) % asteroid.worlds.Count;
				ProcGen.World world = asteroid.worlds[num];
				KAnimFile anim = Assets.GetAnim(world.asteroidIcon.IsNullOrWhiteSpace() ? AsteroidGridEntity.DEFAULT_ASTEROID_ICON_ANIM : world.asteroidIcon);
				if (anim != null)
				{
					componentInChildren.SetVisiblity(true);
					componentInChildren.SwapAnims(new KAnimFile[] { anim });
					componentInChildren.initialMode = KAnim.PlayMode.Loop;
					componentInChildren.initialAnim = "idle_loop";
					componentInChildren.gameObject.SetActive(true);
					if (componentInChildren.HasAnimation(componentInChildren.initialAnim))
					{
						componentInChildren.Play(componentInChildren.initialAnim, KAnim.PlayMode.Loop, 1f, 0f);
					}
					componentInChildren.transform.parent.gameObject.SetActive(true);
				}
			}
			for (int j = asteroid.worlds.Count; j < this.moonContainer.transform.childCount; j++)
			{
				KBatchedAnimController componentInChildren2 = this.moonContainer.transform.GetChild(j).GetComponentInChildren<KBatchedAnimController>();
				if (componentInChildren2 != null)
				{
					componentInChildren2.SetVisiblity(false);
				}
				this.moonContainer.transform.GetChild(j).gameObject.SetActive(false);
			}
			return;
		}
		KBatchedAnimController[] componentsInChildren = this.moonContainer.GetComponentsInChildren<KBatchedAnimController>();
		for (int k = 0; k < componentsInChildren.Length; k++)
		{
			componentsInChildren[k].SetVisiblity(false);
		}
	}

	// Token: 0x0600529E RID: 21150 RVA: 0x001DDD14 File Offset: 0x001DBF14
	private DestinationAsteroid2 GetAsteroid(string name, float scale)
	{
		DestinationAsteroid2 destinationAsteroid;
		if (this.numAsteroids < this.asteroids.Count)
		{
			destinationAsteroid = this.asteroids[this.numAsteroids];
		}
		else
		{
			destinationAsteroid = global::Util.KInstantiateUI<DestinationAsteroid2>(this.asteroidPrefab, this.asteroidContainer.gameObject, false);
			destinationAsteroid.OnClicked += this.OnAsteroidClicked;
			this.asteroids.Add(destinationAsteroid);
		}
		destinationAsteroid.SetAsteroid(this.asteroidData[name]);
		this.asteroidData[name].TargetScale = scale;
		this.asteroidData[name].Scale += (this.asteroidData[name].TargetScale - this.asteroidData[name].Scale) * this.focusScaleSpeed * Time.unscaledDeltaTime;
		destinationAsteroid.transform.localScale = Vector3.one * this.asteroidData[name].Scale;
		this.numAsteroids++;
		return destinationAsteroid;
	}

	// Token: 0x0600529F RID: 21151 RVA: 0x001DDE1C File Offset: 0x001DC01C
	private void EndAsteroidDrawing()
	{
		for (int i = 0; i < this.asteroids.Count; i++)
		{
			this.asteroids[i].gameObject.SetActive(i < this.numAsteroids);
		}
	}

	// Token: 0x060052A0 RID: 21152 RVA: 0x001DDE5E File Offset: 0x001DC05E
	public ColonyDestinationAsteroidBeltData SelectCluster(string name, int seed)
	{
		this.selectedIndex = this.clusterKeys.IndexOf(name);
		this.asteroidData[name].ReInitialize(seed);
		return this.asteroidData[name];
	}

	// Token: 0x060052A1 RID: 21153 RVA: 0x001DDE90 File Offset: 0x001DC090
	public string GetDefaultAsteroid()
	{
		return this.clusterKeys.First<string>();
	}

	// Token: 0x060052A2 RID: 21154 RVA: 0x001DDEA0 File Offset: 0x001DC0A0
	public ColonyDestinationAsteroidBeltData SelectDefaultAsteroid(int seed)
	{
		this.selectedIndex = 0;
		string text = this.asteroidData.Keys.First<string>();
		this.asteroidData[text].ReInitialize(seed);
		return this.asteroidData[text];
	}

	// Token: 0x060052A3 RID: 21155 RVA: 0x001DDEE4 File Offset: 0x001DC0E4
	public void ScrollLeft()
	{
		int num = Mathf.Max(this.selectedIndex - 1, 0);
		this.OnAsteroidClicked(this.asteroidData[this.clusterKeys[num]]);
	}

	// Token: 0x060052A4 RID: 21156 RVA: 0x001DDF24 File Offset: 0x001DC124
	public void ScrollRight()
	{
		int num = Mathf.Min(this.selectedIndex + 1, this.clusterStartWorlds.Count - 1);
		this.OnAsteroidClicked(this.asteroidData[this.clusterKeys[num]]);
	}

	// Token: 0x060052A5 RID: 21157 RVA: 0x001DDF70 File Offset: 0x001DC170
	private void DebugCurrentSetting()
	{
		ColonyDestinationAsteroidBeltData colonyDestinationAsteroidBeltData = this.asteroidData[this.clusterKeys[this.selectedIndex]];
		string text = "{world}: {seed} [{traits}] {{settings}}";
		string startWorldName = colonyDestinationAsteroidBeltData.startWorldName;
		string text2 = colonyDestinationAsteroidBeltData.seed.ToString();
		text = text.Replace("{world}", startWorldName);
		text = text.Replace("{seed}", text2);
		List<AsteroidDescriptor> traitDescriptors = colonyDestinationAsteroidBeltData.GetTraitDescriptors();
		string[] array = new string[traitDescriptors.Count];
		for (int i = 0; i < traitDescriptors.Count; i++)
		{
			array[i] = traitDescriptors[i].text;
		}
		string text3 = string.Join(", ", array);
		text = text.Replace("{traits}", text3);
		CustomGameSettings.CustomGameMode customGameMode = CustomGameSettings.Instance.customGameMode;
		if (customGameMode != CustomGameSettings.CustomGameMode.Survival)
		{
			if (customGameMode != CustomGameSettings.CustomGameMode.Nosweat)
			{
				if (customGameMode == CustomGameSettings.CustomGameMode.Custom)
				{
					List<string> list = new List<string>();
					foreach (KeyValuePair<string, SettingConfig> keyValuePair in CustomGameSettings.Instance.QualitySettings)
					{
						if (keyValuePair.Value.coordinate_dimension >= 0L && keyValuePair.Value.coordinate_dimension_width >= 0L)
						{
							SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(keyValuePair.Key);
							if (currentQualitySetting.id != keyValuePair.Value.GetDefaultLevelId())
							{
								list.Add(string.Format("{0}={1}", keyValuePair.Value.label, currentQualitySetting.label));
							}
						}
					}
					text = text.Replace("{settings}", string.Join(", ", list.ToArray()));
				}
			}
			else
			{
				text = text.Replace("{settings}", "Nosweat");
			}
		}
		else
		{
			text = text.Replace("{settings}", "Survival");
		}
		global::Debug.Log(text);
	}

	// Token: 0x040037C7 RID: 14279
	[SerializeField]
	private GameObject asteroidPrefab;

	// Token: 0x040037C8 RID: 14280
	[SerializeField]
	private KButtonDrag dragTarget;

	// Token: 0x040037C9 RID: 14281
	[SerializeField]
	private MultiToggle leftArrowButton;

	// Token: 0x040037CA RID: 14282
	[SerializeField]
	private MultiToggle rightArrowButton;

	// Token: 0x040037CB RID: 14283
	[SerializeField]
	private RectTransform asteroidContainer;

	// Token: 0x040037CC RID: 14284
	[SerializeField]
	private float asteroidFocusScale = 2f;

	// Token: 0x040037CD RID: 14285
	[SerializeField]
	private float asteroidXSeparation = 240f;

	// Token: 0x040037CE RID: 14286
	[SerializeField]
	private float focusScaleSpeed = 0.5f;

	// Token: 0x040037CF RID: 14287
	[SerializeField]
	private float centeringSpeed = 0.5f;

	// Token: 0x040037D0 RID: 14288
	[SerializeField]
	private GameObject moonContainer;

	// Token: 0x040037D1 RID: 14289
	[SerializeField]
	private GameObject moonPrefab;

	// Token: 0x040037D2 RID: 14290
	private static int chosenClusterCategorySetting;

	// Token: 0x040037D4 RID: 14292
	private float offset;

	// Token: 0x040037D5 RID: 14293
	private int selectedIndex = -1;

	// Token: 0x040037D6 RID: 14294
	private List<DestinationAsteroid2> asteroids = new List<DestinationAsteroid2>();

	// Token: 0x040037D7 RID: 14295
	private int numAsteroids;

	// Token: 0x040037D8 RID: 14296
	private List<string> clusterKeys;

	// Token: 0x040037D9 RID: 14297
	private Dictionary<string, string> clusterStartWorlds;

	// Token: 0x040037DA RID: 14298
	private Dictionary<string, ColonyDestinationAsteroidBeltData> asteroidData = new Dictionary<string, ColonyDestinationAsteroidBeltData>();

	// Token: 0x040037DB RID: 14299
	private Vector2 dragStartPos;

	// Token: 0x040037DC RID: 14300
	private Vector2 dragLastPos;

	// Token: 0x040037DD RID: 14301
	private bool isDragging;

	// Token: 0x040037DE RID: 14302
	private const string debugFmt = "{world}: {seed} [{traits}] {{settings}}";
}
